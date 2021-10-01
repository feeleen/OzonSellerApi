using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Interfaces
{
	public interface IApiConnection
	{
		string BaseApiUrl { get; }
		string ClientID { get; }
		string ApiKey { get; }
		Task<HttpResponseMessage> PostRequestAsync(string jsonRequest, string apiRelativeUrl, HttpMethod method);

		void Configure(string baseApiUrl = null, string apiKey = null, string clientId = null);
	}
}
