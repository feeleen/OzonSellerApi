using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OzonSellerApi.Model
{
	public class ApiError
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("data")]
		public ApiErrorData[] Data { get; set; }

		public override string ToString()
		{
			return $"Error № {Code}. Message {Message}. {string.Join(", ", Data?.Select(item => item?.ToString()))}";
		}
	}

	public class ApiErrorData
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		public override string ToString()
		{
			return $"Code: {Code}. Message: {Message}. Name: {Name}. Value: {Value}";
		}
	}
}
