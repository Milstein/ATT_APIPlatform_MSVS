// <copyright file="ScopeType.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

namespace ATT.WinRTSDK
{
	/// <summary>
	/// Specifies scope types of sending requests on AT&amp;T services,  As of September, 2012, this can include Payment, SMS, MMS, TerminalLocation, WAPPush, MOBO, MIM and Speech. 
	/// </summary>
	public enum ScopeType
	{
		/// <summary>
		/// Payment
		/// </summary>
		Payment,

		/// <summary>
		/// MSM message
		/// </summary>
		SMS,

		/// <summary>
		/// MMS message
		/// </summary>
		MMS,

		/// <summary>
		/// Terminal location
		/// </summary>
		TerminalLocation,

		/// <summary>
		/// WAP push
		/// </summary>
		WapPush,

		/// <summary>
		/// MOBO
		/// </summary>
		MOBO,

		/// <summary>
		/// MIM
		/// </summary>
		MIM,

		/// <summary>
		/// Speech
		/// </summary>
		Speech
	}
}
