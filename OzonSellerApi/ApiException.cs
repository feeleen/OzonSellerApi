using NLog;
using OzonSellerApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi
{
	public class ApiException : Exception
	{
		private readonly static ILogger logger = LogManager.GetCurrentClassLogger();

		public ApiError ApiError { get; }

		public HttpContent ResponseHttpContent { get; }

		public ApiException(string message) : base(message)
		{
		}

		public ApiException(string message, HttpContent responseHttpContent, ApiError apiError = null) : base(message)
		{
			ResponseHttpContent = responseHttpContent;
			ApiError = apiError;
		}

		public ApiException(string message, Exception ex, HttpContent responseHttpContent = null) : base(message, ex)
		{
			ResponseHttpContent = responseHttpContent;
		}

		private static string GetHeadersString(HttpContent responseHttpContent)
		{
			var headers = "";

			if (responseHttpContent != null)
				headers = String.Join(", ", responseHttpContent.Headers.Select(x => $"{x.Key} = {x.Value}, "));

			return headers;
		}
	}

}
