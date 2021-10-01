using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Model
{
	public class ApiResponseMessageBase
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("details")]
		public string[] Details { get; set; }


		[JsonProperty("error")]
		public ApiError Error { get; set; }
	}

	public class ApiSimpleResponseMessage<TOut> : ApiResponseMessageBase
		where TOut : System.Collections.IList
	{
		[JsonProperty("has_next")]
		public bool HasNext { get; set; }

		[JsonProperty("result")]
		public TOut Result { get; set; }
	}

}
