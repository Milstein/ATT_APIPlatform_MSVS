// <copyright file="OAuthTokenRaw.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Runtime.Serialization;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents authorization token parsed from JSON formatted text.
	/// </summary>
	[DataContract]
	public class OAuthTokenRaw : ResponseEntityBase
	{
		/// <summary>
		/// Creates instance of <see cref="OAuthTokenRaw"/>.
		/// </summary>
		public OAuthTokenRaw()
		{
		}

		/// <summary>
		/// Creates instance of <see cref="OAuthTokenRaw"/>.
		/// </summary>
		/// <param name="accessToken">Access token</param>
		/// <param name="refreshToken">Refresh token</param>
		/// <param name="expiresIn">Expires in</param>
		public OAuthTokenRaw(string accessToken, string refreshToken, string expiresIn)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			ExpiresIn = expiresIn;
		}

		/// <summary>
		/// Gets or sets access token value.
		/// </summary>
		[DataMember(Name = "access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		/// Gets or sets number of expiration seconds.
		/// </summary>
		[DataMember(Name = "expires_in")]
		public string ExpiresIn { get; set; }

		/// <summary>
		/// Gets or sets refresh token value.
		/// </summary>
		[DataMember(Name = "refresh_token")]
		public string RefreshToken { get; set; }
	}
}
