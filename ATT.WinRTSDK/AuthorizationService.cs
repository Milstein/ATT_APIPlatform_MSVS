// <copyright file="AuthorizationService.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Net.Http;
using System.Threading.Tasks;
using ATT.Utility;
using ATT.WinRTSDK.Entities;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK
{
	/// <summary>
	/// This class implements the AT&amp;T Authorization service.
	/// </summary>
	public class AuthorizationService
	{
		/// <summary>
		/// raise when authorization failed
		/// </summary>
		public static event EventHandler<EventArgs> AuthorizationFailed;

		private readonly AttServiceSettings _settings;
		private const int ExpiredSeconds = -86400;

		/// <summary>
		/// Creates instance of <see cref="AuthorizationService"/>.
		/// </summary>
		/// <param name="settings"><see cref="AttServiceSettings"/>Service settings.</param>
		/// <exception cref="System.ArgumentNullException">Throws exception when settings is null.</exception>
		public AuthorizationService(AttServiceSettings settings)
		{
			Argument.ExpectNotNull(() => settings);
			_settings = settings;
		}

		/// <summary>
		/// Passes in client credential. If they have not expired, return them; otherwise, get and return a new set of client credentials.
		/// </summary>
		/// <param name="clientCredential">Client credentials.</param>
		/// <param name="scopeList">Scope list.</param>
		/// <returns></returns>
		public async Task<OAuthToken> GetClientCredentials(OAuthToken clientCredential, string scopeList)
		{
			if (clientCredential != null)
			{
				if (!clientCredential.IsExpired())
				{
					return clientCredential;
				}
				if (clientCredential.CreationTime > DateTime.Now.AddSeconds(ExpiredSeconds))
				{
					return await GetRefreshedClientCredential(clientCredential.RefreshToken);
				}
			}

			return await GetNewClientCredential(scopeList);
		}

		private async Task<OAuthToken> GetRefreshedClientCredential(string refreshToken)
		{
			var parameters = new HttpPropertyList();
			parameters["client_id"] = _settings.ClientId;
			parameters["client_secret"] = _settings.ClientSecret;
			parameters["grant_type"] = "refresh_token";
			parameters["refresh_token"] = refreshToken;
			
			return await Send(parameters);
		}

		private async Task<OAuthToken> GetNewClientCredential(string scopeList)
		{
			var parameters = new HttpPropertyList();
			parameters["client_id"] = _settings.ClientId;
			parameters["client_secret"] = _settings.ClientSecret;
			parameters["grant_type"] = "client_credentials";
			parameters["scope"] = scopeList;
			
			return await Send(parameters);
		}

		private async Task<OAuthToken> Send(HttpPropertyList parameters)
		{
			OAuthToken token = null;
			try
			{
				string body = parameters.FormEncode();
				string request = await new HttpHelper(_settings.EndPoint)
									.SendRequest(HttpMethod.Post, "/oauth/access_token", body, null, "application/x-www-form-urlencoded");

				token = OAuthToken.ParseJson(request);
			}
			catch (InvalidResponseException ex)
			{
				if (ex.InvalidResponseEntity.BadRequest == "invalid_client")
				{
					OnAuthorizationFailed();
				}
				throw;
			}
			
			return token;			
		}	

		private void OnAuthorizationFailed()
		{
			if (AuthorizationFailed != null)
			{
				AuthorizationFailed.Invoke(this, new EventArgs());
			}
		}
	}
}
