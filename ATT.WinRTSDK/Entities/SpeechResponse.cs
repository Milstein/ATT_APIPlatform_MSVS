// <copyright file="SpeechResponse.cs" company="AT&amp;T">
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
	/// Domain object with wraps/represents a speech response.
	/// </summary>
	public class SpeechResponse
	{
		/// <summary>
		/// Gets or sets speech response recognition.
		/// </summary>
		public Recognition Recognition { get; private set; }

		/// <summary>
		/// Creates instance of <see cref="SpeechResponse"/>
		/// </summary>
		/// <param name="recognition">Speech response recognition.</param>
		private SpeechResponse(Recognition recognition)
		{
			Recognition = recognition;
		}

		/// <summary>
		/// Parses speech response from input JSON formatted text message.
		/// </summary>
		/// <param name="jsonInput">Input JSON formatted text message</param>
		/// <returns>Parsed speech response entity</returns>
		/// <exception cref="System.ArgumentNullException">jsonInput is null</exception>
		public static SpeechResponse Parse(string jsonInput)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => jsonInput);
			SpeechResponseRaw raw = JsonParser<SpeechResponseRaw>.DeserializeFromJson(jsonInput);		   
			return new SpeechResponse(raw.Recognition);
		}
	}
}
