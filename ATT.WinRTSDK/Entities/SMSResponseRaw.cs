// <copyright file="SMSResponseRaw.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// This object wrap the raw SMS response parsed from JSON formatted text.
	/// </summary>
	[DataContract]
	public class SmsResponseRaw : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets SMS response identifier.
		/// </summary>
		[DataMember(Name = "Id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets SMS resource reference.
		/// </summary>
		[DataMember(Name = "ResourceReference")]
		public ResourceReferenceRaw ResourceReference { get; set; }
	}
}
