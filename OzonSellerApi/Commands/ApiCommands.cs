using OzonSellerApi.Commands;
using OzonSellerApi.Enums;
using OzonSellerApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzonSellerApi.ApiCommands
{
	[ApiCommand(Url = "/categories/tree", SchemaVersion = SchemaVersion.v1)]
	public class GetCategoryTreeCommand : ApiCommandBase<List<Category>>
	{
	}

	[ApiCommand(Url = "/warehouse/list")]
	public class GetWarehouseListCommand : ApiCommandBase<List<Warehouse>>
	{
	}

	[ApiCommand(Url = "/delivery-method/list")]
	public class GetDeliveryMethodListCommand : ApiCommandBase<List<DeliveryMethod>>
	{
	}
}
