// <copyright file="HttpHelper.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using ATT.Utility;
using ATT.WinRTSDK.Entities;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// The utility class realizes the instance for sending http requests.
	/// </summary>
	internal class HttpHelper
	{
		private const int MaxCharacterLength = 32766;

		private readonly Uri _endPoint;

		/// <summary>
		/// Converts a string to its escaped representation.
		/// </summary>
		/// <param name="stringToEscape">The string to escape.</param>
		/// <returns>A System.String that contains the escaped representation of stringToEscape.</returns>
		public static string EscapeDataString(string stringToEscape)
		{			
			if (stringToEscape == null)
				return String.Empty;

			if (stringToEscape.Length > MaxCharacterLength)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(Uri.EscapeDataString(stringToEscape.Substring(0, MaxCharacterLength)));
				sb.Append(EscapeDataString(stringToEscape.Substring(MaxCharacterLength)));

				return sb.ToString();
			}
			else
			{
				return Uri.EscapeDataString(stringToEscape);
			}		
		}

		/// <summary>
		/// Creates instance of <see cref="HttpHelper"/>
		/// </summary>
		/// <param name="endPoint">Service endpoint</param>
		/// <exception cref="System.ArgumentNullException">endPoint is null.</exception>
		public HttpHelper(Uri endPoint)
		{
			Argument.ExpectNotNull(() => endPoint);

			_endPoint = endPoint;
		}

		/// <summary>
		/// Creates HTTP client.
		/// </summary>
		/// <returns><see cref="HttpClient"/>Instance of HTTP client</returns>
		private HttpClient CreateHttpClient()
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		/// <summary>
		/// Creates HTTP client by access token.
		/// </summary>
		/// <param name="accessToken">Access token</param>
		/// <returns>Instance of HTTP client</returns>
		private HttpClient CreateHttpClient(string accessToken)
		{
			HttpClient client = CreateHttpClient();
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
			return client;
		}

		/// <summary>
		/// Creates HTTP content by request and media type.
		/// </summary>
		/// <param name="requestBody">HTTP request body</param>
		/// <param name="contentHeader">Content header</param>
		/// <returns><see cref="HttpContent"/>Instance of HTTP content</returns>
		private HttpContent CreateContent(string requestBody, MediaTypeHeaderValue contentHeader)
		{
			HttpContent httpContent = null;
			if (!String.IsNullOrWhiteSpace(requestBody))
			{
				httpContent = new StringContent(requestBody);
				httpContent.Headers.ContentType = contentHeader;
			}

			return httpContent;
		}

		/// <summary>
		/// Gets HTTP response formatted to string.
		/// </summary>
		/// <param name="response"><see cref="HttpResponseMessage"/>Instance of HTTP response</param>
		/// <returns>Returns Task as a result of asynchronous operation.
		/// The Task result is HTTP response string value.</returns>
		private async Task<string> GetResponse(HttpResponseMessage response)
		{
			var streamTask = (StreamContent)response.Content;
			var text = new StreamReader(await streamTask.ReadAsStreamAsync());
			var strResponse = text.ReadToEnd();
			return strResponse;
		}

		/// <summary>
		/// Sends HTTP request with raw string content.
		/// </summary>
		/// <param name="httpMethod"><see cref="HttpMethod"/>HTTP method to use in the request</param>
		/// <param name="relativeUri">Service relative URI</param>
		/// <param name="requestBody">Request body to send</param>
		/// <param name="accessToken">Access token</param>
		/// <param name="bodyMediaType">Request body media type</param>
		/// <returns>Returns Task as a result of asynchronous operation.
		/// The Task result is the result of HTTP request sending.</returns>
		public async Task<string> SendRequest(HttpMethod httpMethod, string relativeUri, string requestBody, string accessToken, string bodyMediaType = "application/json")
		{
			HttpContent content = CreateContent(requestBody, new MediaTypeHeaderValue(bodyMediaType));
			return await SendRequest(httpMethod, relativeUri, accessToken, content);
		}

		/// <summary>
		/// Sends HTTP request with raw string content.
		/// </summary>
		/// <param name="httpMethod"><see cref="HttpMethod"/>HTTP method to use in the request</param>
		/// <param name="relativeUri">Service relative URI</param>
		/// <param name="accessToken">Access token</param>
		/// <param name="content"><see cref="HttpContent"/>HTTP content</param>
		/// <returns>Returns Task as a result of asynchronous operation.
		/// The Task result is the result of HTTP request sending.</returns>
		/// <exception cref="System.ArgumentNullException">relativeUri is null.</exception>
		/// <exception cref="ATT.WinRTSDK.Entities.InvalidResponseException">HTTP request could not be sent.</exception>
		// Ignore CodeIt.Right rule for this line
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public async Task<string> SendRequest(HttpMethod httpMethod, string relativeUri, string accessToken, HttpContent content)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => relativeUri);

			var requestUri = new Uri(_endPoint, relativeUri);
			try
			{
				var client = String.IsNullOrEmpty(accessToken) ? CreateHttpClient() : CreateHttpClient(accessToken);
				if (httpMethod == HttpMethod.Post)
				{
					return await GetResponse(await client.PostAsync(requestUri, content));
				}
				if (httpMethod == HttpMethod.Get)
				{
					return await GetResponse(await client.GetAsync(requestUri));
				}
				
				return null;
			}
			catch (WebException ex)
			{
				var invalidResponseBody = String.Empty;
				try
				{
					using (StreamReader responseReader = new StreamReader(ex.Response.GetResponseStream()))
					{
						invalidResponseBody = responseReader.ReadToEnd();
					}
				}
				catch(Exception)
				{
					throw new InvalidResponseException("Failed: " + ex.Message, ex);
				}
				throw new InvalidResponseException("Failed: " + invalidResponseBody, ex, HttpStatusCode.BadRequest, invalidResponseBody);
			}
		}
	}
}
