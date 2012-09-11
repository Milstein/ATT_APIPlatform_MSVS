// <copyright file="ServiceException.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// This class wraps the data in a service exception.
	/// </summary>
	[DataContract]
	public class ServiceExceptionEntity
	{
		private string _messageId = String.Empty;
		private string _text = String.Empty;
		private string _variables = String.Empty;

		/// <summary>
		/// Gets or sets message identifier.
		/// </summary>
		[DataMember(Name = "MessageId")]
		public string MessageId 
		{
			get { return _messageId; }
			set { _messageId = value;  }
		}

		/// <summary>
		/// Gets or sets message identifier.
		/// </summary>
		[DataMember(Name = "messageId")]
		public string MessageIdLowMoniker 
		{
			get { return _messageId; }
			set { _messageId = value;  }
		}

		/// <summary>
		/// Gets or sets service exception text.
		/// </summary>
		[DataMember(Name = "Text")]
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		/// <summary>
		/// Gets or sets service exception text.
		/// </summary>
		[DataMember(Name = "text")]
		public string TextLowMoniker
		{
			get { return _text; }
			set { _text = value; }
		}

		/// <summary>
		/// Gets or sets service exception variables.
		/// </summary>
		[DataMember(Name = "Variables")]
		public string Variables
		{
			get { return _variables; }
			set { _variables = value; }
		}

		/// <summary>
		/// Gets or sets service exception variables.
		/// </summary>
		[DataMember(Name = "variables")]
		public string VariablesLowMoniker
		{
			get { return _variables; }
			set { _variables = value; }
		}
	}
}
