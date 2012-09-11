// <copyright file="RequestError.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents a request error.
	/// </summary>
	[DataContract]
	public class RequestError
	{
		private ServiceExceptionEntity _serviceException = null;

		/// <summary>
		/// Gets or sets policy exception.
		/// </summary>
		[DataMember(Name = "PolicyException")]
		public PolicyExceptionEntity PolicyException { get; set; }

		/// <summary>
		/// Gets or sets service exception.
		/// </summary>
		[DataMember(Name = "ServiceException")]
		public ServiceExceptionEntity ServiceException 
		{
			get { return _serviceException; }
			set { _serviceException = value; } 
		}

		/// <summary>
		/// Gets or sets service exception.
		/// </summary>
		[DataMember(Name = "serviceException")]
		public ServiceExceptionEntity ServiceExceptionLowMoniker
		{
			get { return _serviceException; }
			set { _serviceException = value; }
		}
	}
}
