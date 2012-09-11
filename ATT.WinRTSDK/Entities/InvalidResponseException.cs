// <copyright file="InvalidResponseException.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Net;
using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// Represents invalid response exception.
	/// </summary>
	[DataContract]
	public class InvalidResponseException : Exception
	{
		/// <summary>
		/// Creates instance of <see cref="InvalidResponseException"/> with status code zero.
		/// </summary>
		public InvalidResponseException()
			: this(null, null, (HttpStatusCode)0, null)
		{
		}

		/// <summary>
		/// Creates instance of <see cref="InvalidResponseException"/> with an invalidResponseEntity.
		/// </summary>
		/// <param name="invalidResponseEntity"><see cref="ResponseEntityBase"/>Invalid response entity</param>
		public InvalidResponseException(ResponseEntityBase invalidResponseEntity)
			: this(invalidResponseEntity.ToString())
		{
			InvalidResponseEntity = invalidResponseEntity;
		}

		/// <summary>
		/// Creates instance of <see cref="InvalidResponseException"/> with a string.
		/// </summary>
		/// <param name="message">Error message</param>
		public InvalidResponseException(string message)
			: this(message, null, HttpStatusCode.BadRequest, null)
		{
		}

		/// <summary>
		/// Creates instance of <see cref="InvalidResponseException"/> with an exception passed in.
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="ex">Original exception</param>
		public InvalidResponseException(string message, Exception ex)
			: this(message, ex, HttpStatusCode.BadRequest, null)
		{
		}

		/// <summary>
		/// Creates instance of <see cref="InvalidResponseException"/>
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="ex">Original exception</param>
		/// <param name="status"><see cref="HttpStatusCode"/>Response status</param>
		/// <param name="body">Invalid response body</param>
		public InvalidResponseException(string message, Exception ex, HttpStatusCode status, string body)
			: base(message, ex)
		{
			Status = status;
			Body = body;
		}

		/// <summary>
		/// Gets or sets invalid response body.
		/// </summary>
		[DataMember]
		public string Body { get; set; }

		/// <summary>
		/// Gets or sets invalid response entity.
		/// </summary>
		[DataMember]
		public ResponseEntityBase InvalidResponseEntity { get; set; }

		/// <summary>
		/// Gets or sets error status.
		/// </summary>
		public HttpStatusCode Status { get; set; }
	}
}
