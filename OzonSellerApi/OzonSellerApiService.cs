using OzonSellerApi.ApiCommands;
using OzonSellerApi.Interfaces;
using OzonSellerApi.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi
{
	public class OzonSellerApiService
	{
		private static readonly Lazy<OzonSellerApiService> lazy = new Lazy<OzonSellerApiService>(() => new OzonSellerApiService());
		public static OzonSellerApiService Instance => lazy.Value;
		public IApiConnection Connection { get; protected set; }

		private OzonSellerApiService()
		{
			Connection = new ApiConnection();
		}

		public void Configure(string baseApiUrl = null, string apiKey = null, string clientId = null)
		{
			Instance.Connection.Configure(baseApiUrl, apiKey, clientId);
		}

		public async Task<List<Warehouse>> GetWarehouseListAsync()
		{
			var cmd = new GetWarehouseListCommand { Connection = Connection };
			var result = await cmd.ExecuteAsync();

			return result;
		}
		
		public async Task<List<DeliveryMethod>> GetDeliveryMethodListAsync(DeliveryMethodListParameters pars)
		{
			var cmd = new GetDeliveryMethodListCommand { Connection = Connection };
			var result = await cmd.ExecuteAsync(pars);

			return result;
		}
	}
}
