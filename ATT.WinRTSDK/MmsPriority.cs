// <copyright file="MmsPriority.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

namespace ATT.WinRTSDK
{
	/// <summary>
	/// Specifies MMS priority, which is used for sending MMS.  Priorities are Default, Low, Normal, and High.
	/// </summary>
	public enum MmsPriority
	{
		/// <summary>
		/// Default priority.
		/// </summary>
		Default,

		/// <summary>
		/// Low priority.
		/// </summary>
		Low,

		/// <summary>
		/// Normal priority.
		/// </summary>
		Normal,

		/// <summary>
		/// High priority.
		/// </summary>
		High
	}
}
