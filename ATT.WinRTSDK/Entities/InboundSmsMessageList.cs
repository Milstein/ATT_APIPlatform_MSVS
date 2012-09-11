// <copyright file="InboundSmsMessageList.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents an inbound message list.
	/// </summary>
	[DataContract]
	public class InboundSmsMessageList : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets list of inbound SMS messages.
		/// </summary>
		[DataMember(Name = "InboundSmsMessage")]
		public List<InboundSmsMessage> InboundSmsMessage { get; set; }

		/// <summary>
		/// Gets or sets the number of messages in the message list.
		/// </summary>
		[DataMember(Name = "NumberOfMessagesInThisBatch")]
		public int NumberOfMessagesInThisBatch { get; set; }

		/// <summary>
		/// Gets or sets resource URL.
		/// </summary>
		[DataMember(Name = "ResourceUrl")]
		public string ResourceUrl { get; set; }

		/// <summary>
		/// Gets or sets total number of pending messages.
		/// </summary>
		[DataMember(Name = "TotalNumberOfPendingMessages")]
		public int TotalNumberOfPendingMessages { get; set; }
	}
}