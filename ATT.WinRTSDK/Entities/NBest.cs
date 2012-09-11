// <copyright file="NBest.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object for the speech API, this contains data about recognized text.
	/// </summary>
	[DataContract]
	public class NBest : ResponseEntityBase
	{
		/// <summary>
		/// Gets or sets confidence as a number.
		/// </summary>
		[DataMember(Name = "Confidence")]
		public decimal Confidence { get; set; }

		/// <summary>
		/// Gets or sets grade value.
		/// </summary>
		[DataMember(Name = "Grade")]
		public string Grade { get; set; }

		/// <summary>
		/// Gets or sets hypothesis.
		/// </summary>
		[DataMember(Name = "Hypothesis")]
		public string Hypothesis { get; set; }

		/// <summary>
		/// Gets or sets language identifier.
		/// </summary>
		[DataMember(Name = "LanguageId")]
		public string LanguageId { get; set; }

		/// <summary>
		/// Gets or sets resulting text.
		/// </summary>
		[DataMember(Name = "ResultText")]
		public string ResultText { get; set; }

		/// <summary>
		/// Gets or sets array of words.
		/// </summary>
		[DataMember(Name = "Words")]
		// Ignore CodeIt.Right rule for this line
		[System.Diagnostics.CodeAnalysis.SuppressMessage("SubMain.CodeItRight.Rules.Performance", "PE00005")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public string[] Words { get; set; }

		/// <summary>
		/// Gets or sets array of word scores.
		/// </summary>
		[DataMember(Name = "WordScores")]
		// Ignore CodeIt.Right rule for this line
		[System.Diagnostics.CodeAnalysis.SuppressMessage("SubMain.CodeItRight.Rules.Performance", "PE00005")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public decimal[] WordScores { get; set; }
	}
}
