using Newtonsoft.Json;
using OzonSellerApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Model
{
	public class RequestFilterBase
	{
	}


	public class DeliveryMethodListFilter : RequestFilterBase
	{
		[JsonProperty("provider_id")]
		public long? ProviderId { get; set; }

		[JsonProperty("status")]
		public DeliveryMethodStatus Status { get; set; }

		[JsonProperty("warehouse_id")]
		public long? WarehouseId { get; set; }
	}

}
