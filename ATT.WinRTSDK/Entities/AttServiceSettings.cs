// <copyright file="AttServiceSettings.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using ATT.Utility;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that contains client data for an ATT service, including client identifier, secret key, and service endpoint.
	/// </summary>
	public class AttServiceSettings
	{
		/// <summary>
		/// Creates instance of <see cref="AttServiceSettings"/>
		/// </summary>
		/// <param name="clientId">Client identifier</param>
		/// <param name="clientSecret">Client secret key</param>
		/// <param name="serviceEndPoint">Service endpoint</param>
		/// <exception cref="System.ArgumentNullException">Will throw and exception with an input parameter - clientId, clientSecret, or serviceEndPoint, is null.</exception>
		public AttServiceSettings(string clientId, string clientSecret, Uri serviceEndPoint)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => clientId);
			Argument.ExpectNotNullOrWhiteSpace(() => clientSecret);
			Argument.ExpectNotNull(() => serviceEndPoint);

			ClientId = clientId;
			ClientSecret = clientSecret;
			EndPoint = serviceEndPoint;
		}

		/// <summary>
		/// Client identifier
		/// </summary>
		public string ClientId { get; private set; }

		/// <summary>
		/// Client secret key
		/// </summary>
		public string ClientSecret { get; private set; }

		/// <summary>
		/// Service endpoint
		/// </summary>
		public Uri EndPoint { get; private set; }
	}
}
