// <copyright file="ResourceReferenceRaw.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// Represents a raw resource reference, parsed from JSON formatted text.
	/// </summary>
	[DataContract]
	public class ResourceReferenceRaw : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets resource URL.
		/// </summary>
		[DataMember(Name = "ResourceUrl")]
		public string ResourceUrl { get; set; }
	}
}
