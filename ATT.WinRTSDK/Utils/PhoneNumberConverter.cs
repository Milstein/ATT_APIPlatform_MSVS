// <copyright file="PhoneNumberConverter.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Text.RegularExpressions;
using ATT.Utility;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// Utility class for converting phone numbers.
	/// </summary>
	public static class PhoneNumberConverter
	{
		private const string IsdnFormatPattern = @"^\d{3}-\d{3}-\d{4}$";

		/// <summary>
		/// Convert phone number to ISDN format.  Removes dashes, removes "+" from country code, etc.
		/// </summary>
		/// <param name="numberToConvert">Phone number to convert.</param>
		/// <returns>Converted phone number.</returns>
		/// <exception cref="System.ArgumentNullException">numberToConvert is null.</exception>
		/// <exception cref="System.ArgumentException">numberToConvert is in incorrect format</exception>
		public static string ConvertToIsdn(string numberToConvert)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => numberToConvert);

			string str2 = Regex.IsMatch(numberToConvert, IsdnFormatPattern)
								? numberToConvert.Replace("-", String.Empty)
								: numberToConvert;
			long result;
			if ((str2.Length == 0x10) && str2.StartsWith("tel:+1", StringComparison.CurrentCulture))
			{
				str2 = str2.Substring(6, 10);
			}
			else if ((str2.Length == 15) && str2.StartsWith("tel:1", StringComparison.CurrentCulture))
			{
				str2 = str2.Substring(5, 10);
			}
			else if ((str2.Length == 14) && str2.StartsWith("tel:", StringComparison.CurrentCulture))
			{
				str2 = str2.Substring(4, 10);
			}
			else if ((str2.Length == 12) && str2.StartsWith("+1", StringComparison.CurrentCulture))
			{
				str2 = str2.Substring(2, 10);
			}
			else if ((str2.Length == 11) && str2.StartsWith("1", StringComparison.CurrentCulture))
			{
				str2 = str2.Substring(1, 10);
			}
			if ((str2.Length != 10) || !long.TryParse(str2, out result))
			{
				throw new ArgumentException("Invalid phone number");
			}

			numberToConvert = str2.Replace("-", String.Empty);

			return (numberToConvert.StartsWith("tel:", StringComparison.CurrentCulture) ? numberToConvert : ("tel:" + numberToConvert));
		}
	}
}
