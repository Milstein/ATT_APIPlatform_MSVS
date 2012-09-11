// <copyright file="ResponseEntityBase.cs" company="AT&amp;T">
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
	/// The base class for HTTP responses; specific classes will inherit from this.
	/// </summary>
	[DataContract]
	public abstract class ResponseEntityBase
	{
		private RequestError _requestError = null;

		/// <summary>
		/// Gets or sets response error.
		/// </summary>
		[DataMember(Name = "error")]
		public string BadRequest { get; set; }

		/// <summary>
		/// Gets or sets request error.
		/// </summary>
		[DataMember(Name = "RequestError")]
		public RequestError RequestError 
		{
			get { return _requestError; }
			set { _requestError = value; } 
		}

		/// <summary>
		/// Gets or sets request error.
		/// </summary>
		[DataMember(Name = "requestError")]
		public RequestError RequestErrorLowMoniker
		{
			get { return _requestError; }
			set { _requestError = value; }
		}

		/// <summary>
		/// Returns a string that represents the current response.
		/// </summary>
		/// <returns>A string that represents the current response.</returns>
		public override string ToString()
		{
			if (BadRequest != null)
			{
				return BadRequest;
			}

			if (RequestError != null && RequestError.ServiceException != null)
			{
				return RequestError.ServiceException.Text;
			}

			if (RequestError != null && RequestError.PolicyException != null)
			{
				return RequestError.PolicyException.Text;
			}

			return String.Empty;
		}
	}
}
