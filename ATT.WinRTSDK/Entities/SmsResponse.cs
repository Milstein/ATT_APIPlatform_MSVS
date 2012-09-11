// <copyright file="SmsResponse.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using ATT.Utility;
using ATT.WinRTSDK.Entities;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// Domain object with wraps/represents an SMS response.
	/// </summary>
	public class SmsResponse
	{
		/// <summary>
		/// Creates instance of <see cref="SmsResponse"/>.
		/// </summary>
		/// <param name="id">SMS response identifier</param>
		/// <param name="resourceUrl">Resource URL</param>
		/// <exception cref="System.ArgumentNullException">id is null or resourceUrl is null</exception>
		private SmsResponse(string id, string resourceUrl)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => id);
			Argument.ExpectNotNullOrWhiteSpace(() => resourceUrl);

			Id = id;
			ResourceUrl = resourceUrl;
		}

		/// <summary>
		/// Gets SMS response identifier.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets resource URL.
		/// </summary>
		public string ResourceUrl { get; set; }

		/// <summary>
		/// Parses JSON formatted input text to <see cref="SmsResponse"/> entity.
		/// </summary>
		/// <param name="jsonInput">JSON formatted input text</param>
		/// <returns>Parsed SMS response entity</returns>
		/// <exception cref="System.ArgumentNullException">id is null or resourceUrl is null</exception>
		public static SmsResponse Parse(string jsonInput)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => jsonInput);

			SmsResponseRaw raw = JsonParser<SmsResponseRaw>.DeserializeFromJson(jsonInput);
			return new SmsResponse(raw.Id, raw.ResourceReference.ResourceUrl);
		}
	}
}
