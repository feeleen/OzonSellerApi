using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Model
{
	public class ApiMethodParamsBase : EntityBase
	{

	}

	/// <summary>
	/// Adds root "Filter" element into json request
	/// </summary>
	public class ApiMethodParamsWithFilter<TFilter> : ApiMethodParamsBase
		where TFilter : RequestFilterBase
	{
		public TFilter Filter { get; set; }

	}

	public class DeliveryMethodListParameters : ApiMethodParamsWithFilter<DeliveryMethodListFilter>
	{
		[JsonProperty("offset")]
		public int Offset { get; set; }

		[JsonProperty("limit")]
		public int Limit { get; set; }
	}

	public class ApiMethodParamsSimple : ApiMethodParamsBase
	{
		public int Page { get; set; }

		public int PageSize { get; set; }
	}
}
