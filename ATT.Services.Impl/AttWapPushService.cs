// <copyright file="AttWapPushService.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.Threading.Tasks;
using ATT.WinRTSDK;
using ATT.WinRTSDK.Entities;

namespace ATT.Services.Impl
{
	/// <summary>
	/// ATT WAP Push service implementation.
	/// </summary>
	public class AttWapPushService : AttApiService, IWapPushService
	{
		/// <summary>
		/// Creates instance of <see cref="AttWapPushService"/>
		/// </summary>
		/// <param name="endPoint">ATT Service endpoint.</param>
		/// <param name="apiKey">API key.</param>
		/// <param name="secretKey">Secret key.</param>
		public AttWapPushService(string endPoint, string apiKey, string secretKey)
			:base(endPoint, apiKey, secretKey)
		{
		}

		/// <summary>
		/// Sends WAP Push to ATamp;T service.
		/// </summary>
		/// <param name="phoneNumber">Phone number to send WAP Push to.</param>
		/// <param name="url">Service URL.</param>
		/// <param name="alertText">Alert text.</param>
		/// <returns>Returns Task as a result of asynchronous operation. Task result is response identifier.</returns>
		public async Task<string> Send(string phoneNumber, string url, string alertText)
		{
			var service = new WapPushService(Settings);
			WapPushResponse resp = await service.Send(phoneNumber, url, alertText);
			return resp.Id;
		}
	}
}
