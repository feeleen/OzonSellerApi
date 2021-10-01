Usage examples

```cs

public OzonSellerApiService ApiService { get { return OzonSellerApiService.Instance; } }

[TestMethod]
public async Task TestGetDeliveryMethodList()
{
	var result = await ApiService.GetDeliveryMethodListAsync(
			new OzonSellerApi.Model.DeliveryMethodListParameters()
			{
				Limit = 1,
				Offset = 0,
				Filter = new OzonSellerApi.Model.DeliveryMethodListFilter() { ProviderId = 0, Status = DeliveryMethodStatus.ACTIVE, WarehouseId = null }
			});

	Assert.IsTrue(result.Count >= 0);
}

[TestMethod]
public async Task TestWarehousesList()
{
	var result = await ApiService.GetWarehouseListAsync();

	Assert.IsTrue(result.Count >= 0);
}


```


Commands are desribed like this

```cs

[ApiGetCommand(Url = "/categories/tree", SchemaVersion = SchemaVersion.v1)]
public class GetCategoryTreeCommand : ApiCommandBase<List<Category>>
{
}

[ApiPostCommand(Url = "/warehouse/list")]
public class GetWarehouseListCommand : ApiCommandBase<List<Warehouse>>
{
}

[ApiPostCommand(Url = "/delivery-method/list")]
public class GetDeliveryMethodListCommand : ApiCommandBase<List<DeliveryMethod>>
{
}

```
