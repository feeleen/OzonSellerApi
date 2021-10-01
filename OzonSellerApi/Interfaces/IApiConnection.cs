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
		string BaseApiUrl { get; set; }
		string ClientID { get; set; }
		string ApiKey { get; set; }
		Task<HttpResponseMessage> PostRequestAsync(string jsonRequest, string apiRelativeUrl, HttpMethod method);
	}
}
