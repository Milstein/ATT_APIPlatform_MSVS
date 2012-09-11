// <copyright file="OAuthToken.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Globalization;
using ATT.Utility;
using ATT.WinRTSDK.Entities;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK.Entities
{
	/// <summary>
	/// A domain object that encapsulates and represents an authorization token instance.
	/// </summary>
	public class OAuthToken
	{
		/// <summary>
		/// Creates instance of <see cref="OAuthToken"/>.
		/// </summary>
		/// <param name="accessToken">Access token</param>
		/// <param name="refreshToken">Refresh token</param>
		/// <param name="expiresIn">Expiration</param>
		/// <exception cref="System.ArgumentNullException">accessToken is null or refreshToken is null or expiresIn is null</exception>
		/// <exception cref="System.ArgumentException">expiresIn cannot be parsed to long</exception>
		private OAuthToken(string accessToken, string refreshToken, string expiresIn)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => accessToken);
			Argument.ExpectNotNullOrWhiteSpace(() => refreshToken);
			Argument.ExpectNotNullOrWhiteSpace(() => expiresIn);

			try
			{
				ExpiresIn = long.Parse(expiresIn, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new ArgumentException("unable to parse expiresIn: " + expiresIn);
			}

			AccessToken = accessToken;
			RefreshToken = refreshToken;
			CreationTime = DateTime.Now;
			Expiration = CreationTime.AddSeconds(ExpiresIn == 0L ? 3153600000 : ExpiresIn);
		}

		/// <summary>
		/// Gets access token.
		/// </summary>
		public string AccessToken { get; private set; }

		/// <summary>
		/// Gets creation time.
		/// </summary>
		public DateTime CreationTime { get; private set; }

		/// <summary>
		/// Gets expiration date&amp;time.
		/// </summary>
		public DateTime Expiration { get; private set; }

		/// <summary>
		/// Gets the number of seconds for expiration time, from creation time.
		/// </summary>
		public long ExpiresIn { get; private set; }

		/// <summary>
		/// Gets refresh token.
		/// </summary>
		public string RefreshToken { get; private set; }

		/// <summary>
		/// Gets whether authorization token is expired.
		/// </summary>
		/// <returns></returns>
		public bool IsExpired()
		{
			return (DateTime.Now > Expiration);
		}

		/// <summary>
		/// Parses JSON formated text input to <see cref="OAuthToken"/> authorization token entity.
		/// </summary>
		/// <param name="jsonInput">JSON formated text input</param>
		/// <returns><see cref="OAuthToken"/>Authorization token entity</returns>
		public static OAuthToken ParseJson(string jsonInput)
		{
			OAuthTokenRaw raw = JsonParser<OAuthTokenRaw>.DeserializeFromJson(jsonInput); 
			return new OAuthToken(raw.AccessToken, raw.RefreshToken, raw.ExpiresIn);
		}

		/// <summary>
		/// Returns a string that represents the current authorization token.
		/// </summary>
		/// <returns>A string that represents the current authorization token.</returns>
		public override string ToString()
		{
			return String.Format(CultureInfo.InvariantCulture, "OAuthToken: AccessToken={0}, RefreshToken={1}, ExpiresIn={2}, Expiration={3}",
									AccessToken, 
									RefreshToken, 
									ExpiresIn, 
									Expiration);
		}
	}
}
