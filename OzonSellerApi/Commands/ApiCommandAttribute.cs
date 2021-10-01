using OzonSellerApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Commands
{
	internal class ApiCommandAttribute : Attribute
	{
		public string Url { get; set; }
		public HttpMethod Method { get; set; } = HttpMethod.Post;

		public SchemaVersion SchemaVersion { get; set; } = SchemaVersion.v1; // default
		public string ModelName { get; set; }
	}
}
