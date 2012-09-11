// <copyright file="SmsService.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ATT.Utility;
using ATT.WinRTSDK.Entities;
using ATT.WinRTSDK.Utils;

namespace ATT.WinRTSDK
{
	/// <summary>
	/// This class wraps the AT&amp;T REST API, creating a straightforward was to call REST in any .Net language.
	/// </summary>
	public class SmsService : AttRestService
	{
		private const string SendRelativeUrl = "/rest/sms/2/messaging/outbox";
		private const string GetSmsRelativeUrl = "rest/sms/2/messaging/inbox?RegistrationID={0}&access_token={1}";

		/// <summary>
		/// The constructor creates a new instance of <see cref="SmsService"/>.
		/// </summary>
		/// <param name="settings">Instance of <see cref="AttServiceSettings"/> containing ATT API settings.</param>
		public SmsService(AttServiceSettings settings)
			: base(settings)
		{
		}

		/// <summary>
		/// Gets SMS service scope - that is, returns the type of service this object interfaces with.  In this case, it is SMS.
		/// </summary>
		/// <returns>SMS service scope.</returns>
		protected override string GetScope()
		{
			return "SMS";
		}
		
		/// <summary>
		/// Sends the SMS message.
		/// </summary>
		/// <param name="phoneNumbers">Phone numbers to send SMS message to.</param>
		/// <param name="message">Message body to send.</param>
		/// <returns>Instance of <see cref="SmsResponse"/> with sent SMS response information.</returns>
		/// <exception cref="System.ArgumentNullException">phoneNumbers is null.</exception>
		/// <exception cref="System.ArgumentException">phoneNumbers count equals zero.</exception>
		public async Task<SmsResponse> SendSms(IEnumerable<string> phoneNumbers, string message)
		{
			Argument.ExpectNotNull(() => phoneNumbers);
			Argument.Expect(() => phoneNumbers.Any(), "phoneNumbers", "at least one phone number required");

			List<string> isdnAddresses = phoneNumbers
											.Select(a => PhoneNumberConverter.ConvertToIsdn(a))
											.ToList();

			var body = String.Empty;

			if (isdnAddresses.Count == 1)
			{
				var raw = new OutboundSmsRaw(isdnAddresses[0], message);
				body = JsonParser<OutboundSmsRaw>.SerializeToJson(raw);
			}
			else
			{
				var numbers = new OutboundSms(isdnAddresses, message);
				body = JsonParser<OutboundSms>.SerializeToJson(numbers);
			}

			return SmsResponse.Parse(await SendRawRequest(HttpMethod.Post, SendRelativeUrl, body));
		}

		/// <summary>
		/// Gets status of a previously sent (or attempted) SMS message.
		/// </summary>
		/// <param name="smsId">Identifier of SMS message to get status for.</param>
		/// <returns>Instance of <see cref="DeliveryInfoList"/> with the information about SMS message status.</returns>
		/// <exception cref="System.ArgumentNullException">smsId is null.</exception>
		public async Task<DeliveryInfoList> GetSmsStatus(string smsId)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => smsId);

			string relativeUri = String.Format(CultureInfo.InvariantCulture, "{0}/{1}", SendRelativeUrl,  smsId);
			string strResponse = await SendRawRequest(HttpMethod.Get, relativeUri, null);

			return JsonParser<DeliveryInfoList>.DeserializeFromJson(strResponse);
		}

		/// <summary>
		/// Gets SMS messages received for a specific short code.
		/// </summary>
		/// <param name="shortCode">Short code to get SMS message for.</param>
		/// <returns>instance of <see cref="InboundSmsMessageList"/>Returns task as a result of asynchronous operation. Task result is list of inbound SMS messages.</returns>
		/// <exception cref="System.ArgumentNullException">shortCode is null.</exception>
		public async Task<InboundSmsMessageList> GetInboundSmsMessages(string shortCode)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => shortCode);

			var accessToken = await GetClientCredential();
			string relativeUri = String.Format(CultureInfo.InvariantCulture, GetSmsRelativeUrl, shortCode, accessToken);

			return InboundSms.ParseJson(await SendRawRequest(HttpMethod.Get, relativeUri, null));
		}
	}
}
