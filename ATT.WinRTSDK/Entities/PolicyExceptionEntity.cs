// <copyright file="PolicyException.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object which wraps/represents policy exception parsed from JSON formatted text.
	/// </summary>
	[DataContract]
	public class PolicyExceptionEntity
	{
		/// <summary>
		/// Gets or sets message identifier.
		/// </summary>
		[DataMember(Name = "MessageId")]
		public string MessageId { get; set; }

		/// <summary>
		/// Gets or sets policy exception text.
		/// </summary>
		[DataMember(Name = "Text")]
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets variables.
		/// </summary>
		[DataMember(Name = "Variables")]
		public string Variables { get; set; } 
	}
}
