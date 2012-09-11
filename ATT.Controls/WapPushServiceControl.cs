// <copyright file="WapPushServiceControl.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using Windows.UI.Xaml;
using ATT.Controls.Presenters;
using ATT.Services.Impl;

namespace ATT.Controls
{
	/// <summary>
	/// Control that allows a user to send a WAP Push message to a single mobile device
	/// </summary>
	public sealed class WapPushServiceControl : ControlWithPresenter
	{
		#region Dependency properties

		/// <summary>
		/// Style for textBox with phone numbers
		/// </summary>
		public static readonly DependencyProperty PhoneNumberStyleProperty = DependencyProperty.Register("PhoneNumberStyle", typeof(Style), typeof(WapPushServiceControl), new PropertyMetadata(null));

		/// <summary>
		/// Style for textBox with url
		/// </summary>
		public static readonly DependencyProperty UrlStyleProperty = DependencyProperty.Register("UrlStyle", typeof(Style), typeof(WapPushServiceControl), new PropertyMetadata(null));

		/// <summary>
		/// Style for textBox with alert
		/// </summary>
		public static readonly DependencyProperty AlertTextStyleProperty = DependencyProperty.Register("AlertTextStyle", typeof(Style), typeof(WapPushServiceControl), new PropertyMetadata(null));

		/// <summary>
		/// Text phone number field
		/// </summary>
		public static readonly DependencyProperty TextPhoneNumberProperty = DependencyProperty.Register("TextPhoneNumber", typeof(string), typeof(WapPushServiceControl), new PropertyMetadata(""));

		/// <summary>
		/// URL
		/// </summary>
		public static readonly DependencyProperty UrlProperty = DependencyProperty.Register("Url", typeof(string), typeof(WapPushServiceControl), new PropertyMetadata(""));

		/// <summary>
		/// Alert text
		/// </summary>
		public static readonly DependencyProperty AlertTextProperty = DependencyProperty.Register("AlertText", typeof(string), typeof(WapPushServiceControl), new PropertyMetadata(""));

		#endregion

		/// <summary>
		/// Creates and initializes presenter instance for WAP Push Service control.
		/// </summary>
		/// <returns>Returns created presenter instance.</returns>
		protected override PresenterBase InitializePresenter()
		{
			var wapSrv = new AttWapPushService(Endpoint, ApiKeyConfigured, SecretKeyConfigured);
			return new WapPushServiceControlPresenter(wapSrv); 
		}

		/// <summary>
		/// Gets or sets style for textBox with phone numbers
		/// </summary>
		public Style PhoneNumberStyle
		{
			get { return (Style)GetValue(PhoneNumberStyleProperty); }
			set { SetValue(PhoneNumberStyleProperty, value); }
		}

		/// <summary>
		/// Gets or sets style for url textBox
		/// </summary>
		public Style UrlStyle
		{
			get { return (Style)GetValue(UrlStyleProperty); }
			set { SetValue(UrlStyleProperty, value); }
		}

		/// <summary>
		/// Gets or sets style for alert textBox
		/// </summary>
		public Style AlertTextStyle
		{
			get { return (Style)GetValue(AlertTextStyleProperty); }
			set { SetValue(AlertTextStyleProperty, value); }
		}

		/// <summary>
		/// Gets or sets text phone number field
		/// </summary>
		public string TextPhoneNumber
		{
			get { return GetValue(TextPhoneNumberProperty).ToString(); }
			set { SetValue(TextPhoneNumberProperty, value); }
		}

		/// <summary>
		/// Gets or sets URL
		/// </summary>
		public string Url
		{
			get { return GetValue(UrlProperty).ToString(); }
			set { SetValue(UrlProperty, value); }
		}

		/// <summary>
		/// Gets or sets alert text
		/// </summary>
		public string AlertText
		{
			get { return GetValue(AlertTextProperty).ToString(); }
			set { SetValue(AlertTextProperty, value); }
		}
	}
}
