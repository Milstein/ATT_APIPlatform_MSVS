// <copyright file="InboundSmsMessage.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents an inbound SMS message.
	/// </summary>
	[DataContract]
	public class InboundSmsMessage : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets message.
		/// </summary>
		[DataMember(Name = "Message")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets message identifier.
		/// </summary>
		[DataMember(Name = "MessageId")]
		public string MessageId { get; set; }

		/// <summary>
		/// Gets or sets sender address.
		/// </summary>
		[DataMember(Name = "SenderAddress")]
		public string SenderAddress { get; set; }

		/// <summary>
		/// Returns a string that represents the current inbound SMS message.
		/// </summary>
		/// <returns>A string that represents the current inbound SMS message.</returns>
		public override string ToString()
		{
			return String.Format(CultureInfo.InvariantCulture, "{0} => {1} ({2})", SenderAddress, Message, MessageId);
		}
	}
}