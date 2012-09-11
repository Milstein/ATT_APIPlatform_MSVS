// <copyright file="Recognition.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents a speech recognition result.
	/// </summary>
	[DataContract]
	public class Recognition : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets array of <see cref="NBest"/>.
		/// </summary>
		[DataMember(Name = "NBest")]
		// Ignore CodeIt.Right rule for this line
		[System.Diagnostics.CodeAnalysis.SuppressMessage("SubMain.CodeItRight.Rules.Performance", "PE00005")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public NBest[] NBest { get; set; }

		/// <summary>
		/// Response identifier.
		/// </summary>
		[DataMember(Name = "ResponseId")]
		public string ResponseId { get; set; }
	}
}
