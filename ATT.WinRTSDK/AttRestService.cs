// <copyright file="AttRestService.cs" company="AT&amp;T">
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
	/// Base class for ATT API REST service wrappers.  Other services inherit from and extend this class.
	/// </summary>
	public abstract class AttRestService
	{
		private readonly AuthorizationService _authService;
		private readonly AttServiceSettings _settings;

		/// <summary>
		/// ATT service settings
		/// </summary>
		protected AttServiceSettings Settings
		{
			get
			{
				return _settings;
			}
		}

		private OAuthToken _clientCredential;

		/// <summary>
		/// The constructor creates an instance of <see cref="AttRestService"/>.
		/// </summary>
		/// <param name="settings"><see cref="AttServiceSettings"/>ATT service settings.</param>
		/// <exception cref="System.ArgumentNullException">The constructor will throw an exception when the setting variable is null.</exception>
		protected AttRestService(AttServiceSettings settings)
		{
			Argument.ExpectNotNull(() => settings);

			_settings = settings;
			_authService = new AuthorizationService(_settings);
		}

		/// <summary>
		/// Creates or refreshes current service client credentials used to perform a request.
		/// </summary>
		/// <returns></returns>
		protected async Task<OAuthToken> GetClientCredential()
		{
			return await _authService.GetClientCredentials(_clientCredential, GetScope());
		}

		/// <summary>
		/// Gets concrete service scope.  Abstract in the base class, this will return a string that is the type of service of the object; for example, "SMS", "MMS", and so on.
		/// </summary>
		protected abstract string GetScope();

		/// <summary>
		/// Sends HTTP request with raw string content.
		/// </summary>
		/// <param name="httpMethod"><see cref="HttpMethod"/>Http method to use in the request.</param>
		/// <param name="relativeUri">ATT service relative Uri.</param>
		/// <param name="requestBody">Request body to send.</param>
		/// <param name="bodyMediaType">Request body media type.</param>
		protected async Task<string> SendRawRequest(HttpMethod httpMethod, string relativeUri, string requestBody, string bodyMediaType = "application/json")
		{
			HttpHelper httpHelper = await PrepareRequest(_settings.EndPoint);
			return await httpHelper.SendRequest(httpMethod, relativeUri, requestBody, _clientCredential.AccessToken);
		}

		/// <summary>
		/// Sends HTTP request with specified <see cref="HttpContent"/> as request body.
		/// </summary>
		/// <param name="httpMethod"><see cref="HttpMethod"/>Http method to use in the request.</param>
		/// <param name="relativeUri">ATT service relative Uri.</param>
		/// <param name="content"><see cref="HttpContent"/> to use as request body.</param>
		protected async Task<string> SendContentRequest(HttpMethod httpMethod, string relativeUri, HttpContent content)
		{
			HttpHelper httpHelper = await PrepareRequest(_settings.EndPoint);
			return await httpHelper.SendRequest(httpMethod, relativeUri, _clientCredential.AccessToken, content);
		}		

		private async Task<HttpHelper> PrepareRequest(Uri endPoint)
		{
			_clientCredential = await GetClientCredential();
			return new HttpHelper(endPoint);
		}
	}
}
