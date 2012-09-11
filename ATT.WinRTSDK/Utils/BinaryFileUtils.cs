// <copyright file="BinaryFileUtils.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// This is a utility class for working with binary files.
	/// </summary>
	public static class BinaryFileUtils
	{
		/// <summary>
		/// Combines list of byte arrays to the single array.
		/// </summary>
		/// <param name="arrays">Arrays to be combined</param>
		/// <returns>Combined array</returns>
		public static byte[] CombineByteArrays(params byte[][] arrays)
		{
			byte[] rv = new byte[arrays.Sum(a => a.Length)];
			int offset = 0;
			foreach (byte[] array in arrays)
			{
				System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
				offset += array.Length;
			}

			return rv;
		}

		/// <summary>
		/// Reads a binary file, returning a byte array.
		/// </summary>
		/// <param name="file">File to read</param>
		/// <returns>Returns Task as a result of asynchronous operation.
		/// Task result is full file content</returns>
		public static async Task<byte[]> ReadAllBytes(StorageFile file)
		{
			IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
			byte[] buffBytes = new byte[stream.Size];
			IBuffer buffer = buffBytes.AsBuffer();
			await stream.ReadAsync(buffer, (uint)stream.Size, InputStreamOptions.ReadAhead);
			return buffBytes;
		}
	}
}
