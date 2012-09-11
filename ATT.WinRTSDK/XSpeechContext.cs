// <copyright file="XSpeechContext.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

namespace ATT.WinRTSDK
{
	/// <summary>
	/// Specifies speech content of audio file which is used for recognizing audio file to text message.
	/// </summary>
	public enum XSpeechContext
	{
		/// <summary>
		/// Generic search.
		/// </summary>
		Generic,

		/// <summary>
		/// UVerseEPG.
		/// </summary>
		UVerseEPG,

		/// <summary>
		/// Business search.
		/// </summary>
		BusinessSearch,

		/// <summary>
		/// Web search.
		/// </summary>
		WebSearch,

		/// <summary>
		/// SMS.
		/// </summary>
		SMS,

		/// <summary>
		/// Voice mail.
		/// </summary>
		VoiceMail,

		/// <summary>
		/// Question&amp;Answer.
		/// </summary>
		QuestionAndAnswer
	}
}
