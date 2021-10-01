using NLog;
using OzonSellerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi
{
	public class ApiConnection : IApiConnection
	{
		private readonly static ILogger logger = LogManager.GetCurrentClassLogger();

		HttpClient client = new HttpClient();

		/// <summary>
		/// for example api-seller.ozon.ru
		/// </summary>
		public string BaseApiUrl { get; set; }

		/// <summary>
		/// ApiKey ?
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// clientId ?
		/// </summary>
		public string ClientID { get; set; }


		public ApiConnection(string baseApiUrl = null, string apiKey = null, string clientId = null)
		{
			ServicePointManager.DefaultConnectionLimit = 5;
			
			client.BaseAddress = new Uri(baseApiUrl);

			//OZON specific:
			ClientID = clientId;
			ApiKey = apiKey;

			BaseApiUrl = baseApiUrl;
		}

		public async Task<HttpResponseMessage> PostRequestAsync(string jsonRequest, string apiRelativeUrl, HttpMethod method)
		{
			var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			HttpRequestMessage requestMessage = new HttpRequestMessage(method, apiRelativeUrl);
			requestMessage.Headers.Add("Client-Id", ClientID);
			requestMessage.Headers.Add("Api-Key", ApiKey);
			//requestMessage.Headers.Add("Host", "api-seller.ozon.ru");

			requestMessage.Content = httpContent;

			var response = await client.SendAsync(requestMessage);

			return response;
		}
	}
}
