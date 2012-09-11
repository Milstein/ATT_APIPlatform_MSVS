// <copyright file="InboundSmsMessageListRoot.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents the root of inbound SMS messages list.
	/// </summary>
	[DataContract]
	public class InboundSmsMessageListRoot : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets list of inbound SMS messages.
		/// </summary>
		[DataMember(Name = "InboundSmsMessageList")]
		public InboundSmsMessageList InboundSmsMessageList { get; set; }
	}
}
