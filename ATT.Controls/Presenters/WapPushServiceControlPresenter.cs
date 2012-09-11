// <copyright file="WapPushServiceControlPresenter.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ATT.Services;
using ATT.Services.Impl.Delivery;

namespace ATT.Controls.Presenters
{
	/// <summary>
	/// Presenter for <see cref="WapPushServiceControl"/>
	/// </summary>
	public class WapPushServiceControlPresenter : SenderPresenterBase
	{
		private readonly IWapPushService _service;
		private string _url = String.Empty;

		/// <summary>
		/// Creates instance of <see cref="WapPushServiceControlPresenter"/>
		/// </summary>
		public WapPushServiceControlPresenter(IWapPushService service)
		{
			_service = service;
		}

		/// <summary>
		/// Send message
		/// </summary>
		protected override async Task Send()
		{
			IEnumerable<PhoneNumber> numbers = GetPhoneNumbers();
			if (numbers.Any())
			{
				await _service.Send(numbers.First().Number, Url, Message);
			}
		}

		/// <summary>
		/// Gets or sets Url to send wap push message to
		/// </summary>
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				if (_url != value)
				{
					_url = value;
					OnPropertyChanged(() => Url);
				}
			}
		}

		/// <summary>
		/// Creates new <see cref="MessageDeliveryListener"/>
		/// </summary>
		/// <returns>Instance of <see cref="MessageDeliveryListener"/></returns>
		protected override MessageDeliveryListener CreateMessageDeliveryListener()
		{
			return null;
		}

		/// <summary>
		/// Unload presenter. Release resources which was used in presenter
		/// </summary>
		public override void Unload()
		{
			base.Unload();
		}
	}
}
