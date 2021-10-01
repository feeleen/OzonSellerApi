using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Model
{
	public class Category : IDEntity
	{
		[JsonProperty("categoty_id")]
		public override long ID { get; set; }
		public string Title { get; set; }
		public Category[] Children { get; set; }
	}

	public class Warehouse : IDEntity
	{
		[JsonProperty("warehouse_id")]
		public override long ID { get; set; }
		public string Name { get; set; }

		[JsonProperty("is_rfbs")]
		public bool IsRFBS { get; set; }
	}

	public class DeliveryMethod : IDEntity
	{
		[JsonProperty("company_id")]
		public long CompanyID { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("cutoff")]
		public string Cutoff { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("provider_id")]
		public long ProviderID { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("template_id")]
		public long TemplateID { get; set; }

		[JsonProperty("updated_at")]
		public DateTime UpdatedAt { get; set; }

		[JsonProperty("warehouse_id")]
		public long WarehouseID { get; set; }
	}

}
