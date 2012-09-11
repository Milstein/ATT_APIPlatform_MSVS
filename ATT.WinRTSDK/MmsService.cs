// <copyright file="MmsService.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATT.Utility;
using ATT.WinRTSDK.Entities;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK
{
	/// <summary>
	/// This class implements a .Net wrapper for ATT MMS REST API service.
	/// </summary>
	public class MmsService : AttRestService
	{
		private const string SendRelativeUrl = "/rest/mms/2/messaging/outbox";
		
		/// <summary>
		/// The constructor, this creates an instance of <see cref="MmsService"/>.
		/// </summary>
		/// <param name="settings">AT&amp;T service settings.</param>
		public MmsService(AttServiceSettings settings)
			: base(settings)
		{
		}

		/// <summary>
		/// Gets MMS service scope.	- that is, returns the type of service this object interfaces with.  In this case, it is MMS
		/// </summary>
		/// <returns>MMS service scope.</returns>
		protected override string GetScope()
		{
			return "MMS";
		}

		private static string GetTextContent(string timeStampStr, string address, string subject)
		{
			var sb = new StringBuilder();
			sb.Append("--" + timeStampStr + "\r\n");
			sb.Append("Content-Type: application/x-www-form-urlencoded; charset=UTF-8\r\nContent-Transfer-Encoding: 8bit\r\nContent-Disposition: form-data; name=\"root-fields\"\r\nContent-ID: <startpart>\r\n\r\n");
			sb.Append(address);
			sb.Append("Subject=" + HttpHelper.EscapeDataString(subject) + "&Priority=" + "Normal");
			sb.Append("\r\n");

			return sb.ToString();
		}

		private MediaTypeHeaderValue CreateContentTypeHeader(string timeStampStr)
		{
			var contentHeader = new MediaTypeWithQualityHeaderValue("multipart/form-data");
			contentHeader.Parameters.Add(new NameValueHeaderValue("type", "\"application/x-www-form-urlencoded\""));
			contentHeader.Parameters.Add(new NameValueHeaderValue("start", "\"<startpart>\""));
			contentHeader.Parameters.Add(new NameValueHeaderValue("boundary", "\"" + timeStampStr + "\""));

			return contentHeader;
		}

		private static string CreateAttachmentPrefix(bool isFirstFile, string textContent, string fileName, string timeStampStr)
		{
			var timeStampDelimiter = "--" + timeStampStr + "\r\n";
			var attachmentPrefix = new StringBuilder();
			if (isFirstFile)
			{
				attachmentPrefix.Append(textContent);
			}

			attachmentPrefix.Append(timeStampDelimiter);
			string shortFileName = Path.GetFileName(fileName);
			attachmentPrefix.Append("Content-Disposition:attachment;name=\"" + shortFileName + "\"\r\n");

			string contentType = ContentTypeMapping.MapContentTypeFromExtension(Path.GetExtension(fileName));
			attachmentPrefix.Append("Content-Type:" + contentType + "\r\n");

			attachmentPrefix.Append("Content-ID:<" + shortFileName + ">\r\n");
			attachmentPrefix.Append("Content-Transfer-Encoding:binary\r\n\r\n");

			return attachmentPrefix.ToString();
		}

		private static async Task<byte[]> EncodeBinaryContent(IEnumerable<Windows.Storage.StorageFile> attachments, string textContent, string timeStampStr)
		{
			var encoding = new UTF8Encoding();
			byte[] buffer = null;
			bool isFirstFile = true;
			string currentFileName = String.Empty;

			try
			{
				foreach (var fileName in attachments)
				{
					currentFileName = fileName.Name;
					byte[] attachmentPrefixBytes = encoding.GetBytes(CreateAttachmentPrefix(isFirstFile, textContent, fileName.Path, timeStampStr));
					byte[] fileBytes = await BinaryFileUtils.ReadAllBytes(fileName);
					byte[] attachBuffer = BinaryFileUtils.CombineByteArrays(attachmentPrefixBytes, fileBytes);

					if (isFirstFile)
					{
						buffer = attachBuffer;
					}
					else
					{
						buffer = BinaryFileUtils.CombineByteArrays(buffer, attachBuffer);
					}

					isFirstFile = false;
				}
			}
			catch (FileNotFoundException ex)
			{
				throw new FileNotFoundException(string.Format("File '{0}' not found", currentFileName), ex);
			}

			return BinaryFileUtils.CombineByteArrays(buffer, encoding.GetBytes("\r\n--" + timeStampStr + "--\r\n"));
		}

		/// <summary>
		/// Sends new MMS message.
		/// </summary>
		/// <param name="phoneNumbers">Phone numbers to send MMS message to.</param>
		/// <param name="message">Message body to send.</param>
		/// <param name="attachments">List of files attached to MMS.</param>
		/// <param name="priority">MMS priority.</param>
		/// <returns>Instance of <see cref="MmsResponse"/> with sent MMS response information.</returns>
		/// <exception cref="System.ArgumentNullException">Throws exception when the message is null.</exception>
		public async Task<MmsResponse> SendMms(IEnumerable<string> phoneNumbers, string message, IEnumerable<Windows.Storage.StorageFile> attachments, MmsPriority priority = MmsPriority.Normal)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => message);
			Argument.ExpectNotNull(() => phoneNumbers);
			Argument.Expect(() => phoneNumbers.Any(), "phoneNumbers", "at least one phone number is required");

			var attachList = attachments == null
								? null 
								: attachments.ToList();

			var sb = new StringBuilder();
			foreach (string pn in phoneNumbers)
			{
				sb.AppendFormat(CultureInfo.InvariantCulture, "Address={0}&", Uri.EscapeUriString(PhoneNumberConverter.ConvertToIsdn(pn)));
			}

			return await SendMms(sb.ToString(), message, attachList, priority);
		}

		private async Task<MmsResponse> SendMms(string address, string message, List<Windows.Storage.StorageFile> attachments, MmsPriority priority = MmsPriority.Normal)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => message);
			Argument.ExpectNotNullOrWhiteSpace(() => address);

			string timeStampStr = "----------------------------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
			string textContent = GetTextContent(timeStampStr, address, message);

			HttpContent content;
			if (attachments != null && attachments.Count>0)
			{
				byte[] bytes = await EncodeBinaryContent(attachments, textContent, timeStampStr);
				content = new ByteArrayContent(bytes);
			}
			else
			{
				textContent = textContent + "--" + timeStampStr + "--\r\n";
				content = new StringContent(textContent);
			}
			content.Headers.ContentType = CreateContentTypeHeader(timeStampStr);
			
			var requestUri = new Uri(Settings.EndPoint, SendRelativeUrl);
			string strResponse = await SendContentRequest(HttpMethod.Post, requestUri.ToString(), content);

			return MmsResponse.Parse(strResponse); 
		}

		/// <summary>
		/// Gets status of previously sent MMS message.
		/// </summary>
		/// <param name="mmsId">Identifier of MMS message to get status for.</param>
		/// <returns>Instance of <see cref="DeliveryInfoList"/> with the information about MMS message status.</returns>
		/// <exception cref="System.ArgumentNullException">Throws exception when mmsId is null.</exception>
		public async Task<DeliveryInfoList> GetMmsStatus(string mmsId)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => mmsId);

			string relativeUri = String.Format(CultureInfo.InvariantCulture, "{0}/{1}", SendRelativeUrl, mmsId);

			string strResponse = await SendRawRequest(HttpMethod.Get, relativeUri, null);
			return JsonParser<DeliveryInfoList>.DeserializeFromJson(strResponse);
		}
	}
}
