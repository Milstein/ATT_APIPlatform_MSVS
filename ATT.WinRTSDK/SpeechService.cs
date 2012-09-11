// <copyright file="SpeechService.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Storage;
using ATT.Utility;
using ATT.WinRTSDK.Entities;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK
{
	/// <summary>
	/// This class implements a .net wrapper and API for the ATT Speech REST API.
	/// </summary>
	public class SpeechService : AttRestService
	{
		private const string SendRelativeUrl = "/rest/1/SpeechToText";

		/// <summary>
		/// The constructor creates a new instance of <see cref="SpeechService"/>.
		/// </summary>
		/// <param name="settings">Instance of <see cref="AttServiceSettings"/> containing ATT API settings.</param>
		public SpeechService(AttServiceSettings settings)
			: base(settings)
		{
		}

		/// <summary>
		/// Returns the service scope; in the case, "Speech"
		/// </summary>
		/// <returns>Speech service scope.</returns>
		protected override string GetScope()
		{
			return "Speech";
		}

		/// <summary>
		/// The main conversion API. Calling this function sends an audio file to AT&amp;T service for translation to text.
		/// </summary>
		/// <param name="attachment">Audio filename as it is stored on disk.</param>
		/// <param name="speechContext">Speech content.</param>
		/// <returns>Instance of <see cref="SpeechResponse"/> with sent speech response information.</returns>
		/// <exception cref="System.ArgumentNullException">Throws an exception when attachment is null.</exception>
		public async Task<SpeechResponse> SpeechToText(StorageFile attachment, XSpeechContext speechContext = XSpeechContext.Generic)
		{
			Argument.ExpectNotNull(() => attachment);

			byte[] audioFileBytes = await BinaryFileUtils.ReadAllBytes(attachment);

			var restEndPoint = new Uri(Settings.EndPoint, SendRelativeUrl);

			var content = new ByteArrayContent(audioFileBytes);
			string contentType = ContentTypeMapping.MapContentTypeFromExtension(attachment.FileType);
			content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
			content.Headers.Add("X-SpeechContext", Enum.GetName(typeof(XSpeechContext), speechContext));

			string strResponse = await SendContentRequest(HttpMethod.Post, restEndPoint.ToString(), content);
			return SpeechResponse.Parse(strResponse);
		}
	}
}
