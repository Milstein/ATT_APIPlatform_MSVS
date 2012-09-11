// <copyright file="MmsResponse.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using ATT.Utility;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object to encapsulate and represent an MMS response.
	/// </summary>
	public class MmsResponse
	{
		/// <summary>
		/// Creates instance of the <see cref="MmsResponse"/> object.
		/// </summary>
		/// <param name="id">MMS response identifier</param>
		/// <param name="resourceUrl">Resource URL</param>
		/// <exception cref="System.ArgumentNullException">id is null or resourceUrl is null</exception>
		private MmsResponse(string id, string resourceUrl)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => id);
			Argument.ExpectNotNullOrWhiteSpace(() => resourceUrl);

			Id = id;
			ResourceUrl = resourceUrl;
		}

		/// <summary>
		/// Gets MMS response identifier.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Gets resource URL.
		/// </summary>
		public string ResourceUrl { get; private set; }

		/// <summary>
		/// Parses JSON formatted input text to <see cref="MmsResponse"/> entity.
		/// </summary>
		/// <param name="jsonInput">JSON formatted input text</param>
		/// <returns>Parsed MMS response entity</returns>
		/// <exception cref="System.ArgumentException">jsonInput is null or empty string</exception>
		internal static MmsResponse Parse(string jsonInput)
		{
			if (String.IsNullOrEmpty(jsonInput))
			{
				throw new ArgumentException("Empty MMS Response");
			}
			
			MmsResponseRaw raw = JsonParser<MmsResponseRaw>.DeserializeFromJson(jsonInput);
			MmsResponse response = new MmsResponse(raw.Id, raw.ResourceReference.ResourceUrl);
			
			return response;
		}
	}
}
