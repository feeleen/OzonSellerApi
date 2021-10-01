using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.Model
{
	public class EntityBase
	{
	}

	public class IDEntity : EntityBase
	{
		public virtual long ID { get; set; }
	}
}
