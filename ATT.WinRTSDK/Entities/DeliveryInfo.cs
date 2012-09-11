// <copyright file="DeliveryInfo.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// Represents Delivery information.
	/// </summary>
	[DataContract]
	public class DeliveryInfo : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets delivery address.
		/// </summary>
		[DataMember(Name = "Address")]
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets delivery status.
		/// </summary>
		[DataMember(Name = "DeliveryStatus")]
		public string DeliveryStatus { get; set; }

		/// <summary>
		/// Gets or sets delivery identifier.
		/// </summary>
		[DataMember(Name = "Id")]
		public string Id { get; set; }
	}
}
