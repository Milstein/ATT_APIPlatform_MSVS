// <copyright file="ContentTypeMapping.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using ATT.Utility;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// Utility class for defining file types.
	/// </summary>
	public static class ContentTypeMapping
	{
		/// <summary>
		/// Set of known file extensions.
		/// </summary>
		private static readonly Dictionary<string, string> _knownExtensions =
			new Dictionary<string, string>
				{
					{ ".jpg", "image/jpeg" },
					{ ".bmp", "image/bmp" },
					{ ".mp3", "audio/mp3" },
					{ ".m4a", "audio/m4a" },
					{ ".gif", "image/gif" },
					{ ".3gp", "video/3gpp" },
					{ ".3g2", "video/3gpp2" },
					{ ".wmv", "video/x-ms-wmv" },
					{ ".m4v", "video/x-m4v" },
					{ ".amr", "audio/amr" },
					{ ".mp4", "video/mp4" },
					{ ".avi", "video/x-msvideo" },
					{ ".mov", "video/quicktime" },
					{ ".mpeg", "video/mpeg" },
					{ ".wav", "audio/wav" },
					{ ".aiff", "audio/x-aiff" },
					{ ".aifc", "audio/x-aifc" },
					{ ".midi", ".midi" },
					{ ".au", "audio/basic" },
					{ ".xwd", "image/x-xwindowdump" },
					{ ".png", "image/png" },
					{ ".tiff", "image/tiff" },
					{ ".tif", "image/tiff" },
					{ ".ief", "image/ief" },
					{ ".txt", "text/plain" },
					{ ".html", "text/html" },
					{ ".vcf", "text/x-vcard" },
					{ ".vcs", "text/x-vcalendar" },
					{ ".mid", "application/x-midi" },
					{ ".imy", "audio/iMelody" }
				};

		/// <summary>
		/// A simple lookup that takes file extension and returns content type
		/// </summary>
		/// <param name="extension">File extension</param>
		/// <returns>Name of file type</returns>
		/// <exception cref="System.ArgumentNullException">extension is null</exception>
		/// <exception cref="System.ArgumentException">extension is unknown file extension</exception>
		public static string MapContentTypeFromExtension(string extension)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => extension);
			extension = extension.ToLowerInvariant();

			if (!_knownExtensions.ContainsKey(extension))
			{
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "File extension '{0}' is not supported", extension));
			}

			return _knownExtensions[extension];
		}
	}
}
