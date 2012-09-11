// <copyright file="DeliveryStatus.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// Represents delivery status of HTTP request.
	/// </summary>
	[DataContract]
	// Ignore CodeIt.Right rule for this line
	[System.Diagnostics.CodeAnalysis.SuppressMessage("SubMain.CodeItRight.Rules.Performance", "PE00005")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
	public class DeliveryStatus : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets Delivery information list.
		/// </summary>
		[DataMember(Name = "DeliveryInfo")]
		public DeliveryInfo[] DeliveryInfoList { get; set; }

		/// <summary>
		/// Gets or sets resource URL.
		/// </summary>
		[DataMember(Name = "ResourceUrl")]
		public string ResourceUrl { get; set; }
	}
}
