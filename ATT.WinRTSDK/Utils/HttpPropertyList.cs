// <copyright file="HttpPropertyList.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// This utility class represents instance with the set of parameters for http request.
	/// </summary>
	public class HttpPropertyList
	{
		private readonly Dictionary<string, string> _properties = new Dictionary<string, string>();

		/// <summary>
		/// Map of parameters mapped by parameter names.
		/// </summary>
		public Dictionary<string, string> Properties { get { return _properties; } }

		/// <summary>
		/// Gets or sets the value associated with the specified property name.
		/// </summary>
		/// <param name="propName">Property name</param>
		/// <returns>The value associated with the property name</returns>
		/// <exception cref="System.ArgumentNullException">property name is null.</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and property name does not exist in the collection.</exception>
		public string this[string propName]
		{
			get { return _properties[propName]; }
			set { _properties[propName] = value; }
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("{");
			string prefix = String.Empty;
			foreach (var prop in Properties)
			{
				sb.Append(prefix + "\"" + prop.Key + "\"");
				sb.Append(":\"" + prop.Value + "\"");
				prefix = ", ";
			}
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an encoding string that represents the current object.
		/// </summary>
		/// <returns>An encoding string that represents the current object.</returns>
		public string FormEncode()
		{
			var sb = new StringBuilder();
			string prefix = String.Empty;
			foreach (var prop in Properties)
			{
				sb.Append(prefix + prop.Key);
				sb.Append("=" + prop.Value);
				prefix = "&";
			}
			return sb.ToString();
		}
	}
}
