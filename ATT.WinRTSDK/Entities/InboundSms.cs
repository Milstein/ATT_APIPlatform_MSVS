// <copyright file="InboundSms.cs" company="AT&amp;T">
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
	/// A domain object that encapsulates and represents an inbound SMS message.
	/// </summary>
	public static class InboundSms
	{
		/// <summary>
		/// Parses JSON formatted message to <see cref="InboundSmsMessageList"/> entity.
		/// </summary>
		/// <param name="jsonInput">Input JSON formatted message.</param>
		/// <returns>Parsed <see cref="InboundSmsMessageList"/> entity.</returns>
		/// <exception cref="System.ArgumentNullException">jsonInput is null.</exception>
		public static InboundSmsMessageList ParseJson(string jsonInput)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => jsonInput);
			return JsonParser<InboundSmsMessageListRoot>.DeserializeFromJson(jsonInput).InboundSmsMessageList;
		}
	}
}