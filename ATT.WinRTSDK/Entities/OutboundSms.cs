// <copyright file="OutboundSms.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ATT.Utility;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents an outbound SMS message.
	/// </summary>
	[DataContract]
	public class OutboundSms
	{
		/// <summary>
		/// Creates instance of <see cref="OutboundSms"/>.
		/// </summary>
		public OutboundSms()
		{ }

		/// <summary>
		/// Creates instance of <see cref="OutboundSms"/>.
		/// </summary>
		/// <param name="address">Set of SMS addresses</param>
		/// <param name="message">SMS message</param>
		/// <exception cref="System.ArgumentNullException">address is null</exception>
		public OutboundSms(IEnumerable<string> address, string message)
		{
			Argument.ExpectNotNull(() => address);

			Address = address.ToList();
			Message = message;
		}

		/// <summary>
		/// Gets or sets set of SMS addresses.
		/// </summary>
		[DataMember(Name = "Address")]
		public List<string> Address { get; set; }

		/// <summary>
		/// Gets or sets SMS message.
		/// </summary>
		[DataMember(Name = "Message")]
		public string Message { get; set; }
	}
}
