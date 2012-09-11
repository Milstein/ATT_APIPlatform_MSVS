// <copyright file="WapPushResponse.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using ATT.Utility;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// Domain object with wraps/represents an WAP push response.
	/// </summary>
	public class WapPushResponse
	{
		/// <summary>
		/// Creates instance of <see cref="WapPushResponse"/>.
		/// </summary>
		/// <param name="id">WAP push identifier</param>
		private WapPushResponse(string id)
		{
			Id = id;
		}

		/// <summary>
		/// Parses>WAP push response from input JSON formatted text message.
		/// </summary>
		/// <param name="jsonInput">input JSON formatted text message</param>
		/// <returns>Parsed WAP push response entity</returns>
		/// <exception cref="System.ArgumentNullException">jsonInput is null</exception>
		public static WapPushResponse Parse(string jsonInput)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => jsonInput);
			WapPushResponseRaw raw = JsonParser<WapPushResponseRaw>.DeserializeFromJson(jsonInput);
			return new WapPushResponse(raw.Id);
		}

		/// <summary>
		/// Gets or sets WAP push identifier.
		/// </summary>
		public string Id { get; private set; }
	}
}