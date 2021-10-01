using OzonSellerApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Commands
{
	internal class ApiCommandAttributeBase : Attribute
	{
		public string Url { get; set; }
		public virtual HttpMethod Method { get;}

		public SchemaVersion SchemaVersion { get; set; } = SchemaVersion.v1; // default
		public string ModelName { get; set; }
	}


	internal class ApiGetCommandAttribute : ApiCommandAttributeBase
	{
		public override HttpMethod Method { get => HttpMethod.Get; }
	}

	internal class ApiPostCommandAttribute : ApiCommandAttributeBase
	{
		public override HttpMethod Method { get => HttpMethod.Post; }
	}
}
