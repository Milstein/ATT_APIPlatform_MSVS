// <copyright file="JsonParser.cs" company="AT&amp;T">
// Licensed by AT&amp;T under 'Software Development Kit Tools Agreement.' 2012
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2012 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// </copyright>

using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using ATT.Utility;
using ATT.WinRTSDK.Entities;

namespace ATT.WinRTSDK.Utils
{
	/// <summary>
	/// Utility class for parsing data from/to JSON format
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class JsonParser<T>
	{
		/// <summary>
		/// Parses JSON formatted data.
		/// </summary>
		/// <param name="jsonInput">Input string in JSON format</param>
		/// <returns>Parsed data</returns>
		/// <exception cref="System.ArgumentNullException">jsonInput is null</exception>
		/// <exception cref="ATT.WinRTSDK.Entities.InvalidResponseException">jsonInput cannot be parsed</exception>
		public static T DeserializeFromJson(string jsonInput)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => jsonInput);

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput)))
			{
				var jsonSerializer = new DataContractJsonSerializer(typeof(T));
				object jsonOutput = jsonSerializer.ReadObject(stream);
				var entityBase = (ResponseEntityBase)jsonOutput;

				if (entityBase.BadRequest != null || entityBase.RequestError != null)
				{
					throw new InvalidResponseException(entityBase); 
				}

				return (T)jsonOutput;
			}
		}

		/// <summary>
		/// Serializes data to JSON formatted data.
		/// </summary>
		/// <param name="toJsonObject">Data object for serialization</param>
		/// <returns>JSON formatted data</returns>
		/// <exception cref="System.ArgumentNullException">toJsonObject is null</exception>
		public static string SerializeToJson(T toJsonObject)
		{
			Argument.ExpectNotNull(() => toJsonObject);

			using (var stream = new MemoryStream())
			using (var text = new StreamReader(stream))
			{
				var jsonSerializer = new DataContractJsonSerializer(typeof(T));

				jsonSerializer.WriteObject(stream, toJsonObject);
				stream.Position = 0;
				return text.ReadToEnd();
			}
		}
	}
}
