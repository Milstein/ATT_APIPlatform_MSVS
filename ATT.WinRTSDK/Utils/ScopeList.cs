// <copyright file="ScopeList.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using ATT.Utility;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// Utility class for Scope -- maintains a list of possible scopes.
	/// </summary>
	internal static class ScopeList
	{
		/// <summary>
		/// Scope list 
		/// </summary>
		public static string ScopesList(IEnumerable<ScopeType> scopes)
		{
			Argument.ExpectNotNull(() => scopes);

			var scopeNames = new List<string>();

			if (scopes.Contains(ScopeType.TerminalLocation))
			{
				scopeNames.Add("TL");
			}
			if (scopes.Contains(ScopeType.SMS))
			{
				scopeNames.Add("SMS");
			}
			if (scopes.Contains(ScopeType.MMS))
			{
				scopeNames.Add("MMS");
			}
			if (scopes.Contains(ScopeType.Payment))
			{
				scopeNames.Add("PAYMENT");
			}
			if (scopes.Contains(ScopeType.WapPush))
			{
				scopeNames.Add("WAP");
			}
			if (scopes.Contains(ScopeType.MOBO))
			{
				scopeNames.Add("MOBO");
			}
			if (scopes.Contains(ScopeType.MIM))
			{
				scopeNames.Add("MIM");
			}
			if (scopes.Contains(ScopeType.Speech))
			{
				scopeNames.Add("SPEECH");
			}

			return String.Join(",", scopeNames);
		}
	}
}
