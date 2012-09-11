// <copyright file="WapPushService.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ATT.Utility;
using ATT.WinRTSDK.Entities;

namespace ATT.WinRTSDK
{
	/// <summary>
	/// This class implements a .Net wrapper for  AT&amp;T WAP Push REST API service.
	/// </summary>
	public class WapPushService : AttRestService
	{
		private const string SendRelativeUrl = "/1/messages/outbox/wapPush";

		/// <summary>
		/// The constructor creates an instance of the <see cref="WapPushService"/>.
		/// </summary>
		/// <param name="settings">Passing a standard AT&amp;T service settings object.</param>
		public WapPushService(AttServiceSettings settings)
			: base(settings)
		{
		}

		/// <summary>
		/// Gets WAP push service scope, which will be "WAP"
		/// </summary>
		/// <returns>WAP push service scope.</returns>
		protected override string GetScope()
		{
			return "WAP ";
		}

		private string BuildServiceInitiation(string alertText, string url)
		{
			//TODO: use string builder here
			return 
				(
					(
						(
							(
								(
									(
										(String.Empty + "Content-Disposition: form-data; name=\"PushContent\"\n") 
										+ "Content-Type: text/vnd.wap.si\n" 
										+ "Content-Length: 20\n"
									) 
									+ "X-Wap-Application-Id: x-wap-application:wml.ua\n\n" 
									+ "<?xml version='1.0'?>\n"
								) 
								+ "<!DOCTYPE si PUBLIC \"-//WAPFORUM//DTD SI 1.0//EN\" \"http://www.wapforum.org/DTD/si.dtd\">\n" 
								+ "<si>\n"
							) 
							+ "<indication href=\"" 
							+ url 
							+ "\" action=\"signal-medium\" si-id=\"6532\">\n"
						)
						+ alertText
					) 
					+ "\n</indication>" 
					+ "\n</si>"
				);
		}

		private string BuildWapPushData(string boundary, string parameters, string wapData)
		{
			//TODO: use string builder here
			return (((((((((String.Empty + "--" + boundary + "\r\n") + "Content-type: application/x-www-form-urlencoded; charset=UTF-8\r\n") + "Content-Transfer-Encoding: 8bit\r\n" + "Content-ID: <startpart>\r\n") + "Content-Disposition: form-data; name=\"root-fields\"\r\n\r\n" + parameters.ToString() + "\r\n") + "--" + boundary + "\r\n") + "Content-Disposition: attachment; name=Push.txt\r\n\r\n" + "Content-Type: text/plain\r\n") + "Content-ID: <Push.txt>\r\n" + "Content-Transfer-Encoding: binary\r\n") + wapData + "\r\n") + "--" + boundary + "--\r\n");
		}

		private MediaTypeHeaderValue CreateContentTypeHeader(string boundary)
		{
			var contentHeader = new MediaTypeWithQualityHeaderValue("multipart/form-data");
			contentHeader.Parameters.Add(new NameValueHeaderValue("type", "\"application/x-www-form-urlencoded\""));
			contentHeader.Parameters.Add(new NameValueHeaderValue("start", "\"\""));
			contentHeader.Parameters.Add(new NameValueHeaderValue("boundary", "\"" + boundary + "\""));

			return contentHeader;
		}

		/// <summary>
		/// Sends new WAP Push via REST to AT&amp;T.
		/// </summary>
		/// <param name="phoneNumber">Phone number to send WAP Push to.</param>
		/// <param name="url">Service URL.</param>
		/// <param name="alertText">Alert text.</param>
		/// <returns>Instance of <see cref="WapPushResponse"/> with sent WAP Push response information.</returns>
		public async Task<WapPushResponse> Send(string phoneNumber, string url, string alertText)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => phoneNumber);
			Argument.ExpectNotNullOrWhiteSpace(() => url);

			string wapData = BuildServiceInitiation(alertText, url);
			string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);

			CreateContentTypeHeader(boundary);
			string parameters = phoneNumber + "subject=" + Uri.EscapeUriString("Title") + "&priority=High&content-type=" + Uri.EscapeUriString("application/xml");
			string body = BuildWapPushData(boundary, parameters, wapData);
			var content = new StringContent(body);
			content.Headers.ContentType = CreateContentTypeHeader(boundary);

			string strResponse = await SendContentRequest(HttpMethod.Post, SendRelativeUrl, content);
			WapPushResponse response = WapPushResponse.Parse(strResponse);
			return response;
		}
	}
}
