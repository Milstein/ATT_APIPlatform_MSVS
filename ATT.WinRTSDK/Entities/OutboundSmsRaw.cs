// <copyright file="OutboundSmsRaw.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	///  A domain object that encapsulates and represents an outbound (raw) SMS message
	/// </summary>
	[DataContract]
	public class OutboundSmsRaw : ResponseEntityBase
	{
		/// <summary>
		/// Creates instance of <see cref="OutboundSmsRaw"/>
		/// </summary>
		public OutboundSmsRaw()
		{
		}

		/// <summary>
		/// Creates instance of <see cref="OutboundSmsRaw"/>.
		/// </summary>
		/// <param name="address">SMS address</param>
		/// <param name="message">SMS message</param>
		public OutboundSmsRaw(string address, string message)
		{
			Address = address;
			Message = message;
		}

		/// <summary>
		/// Gets or sets SMS address.
		/// </summary>
		[DataMember(Name = "Address")]
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets SMS message.
		/// </summary>
		[DataMember(Name = "Message")]
		public string Message { get; set; }
	}
}
