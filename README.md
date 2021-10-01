[![Build status](https://ci.appveyor.com/api/projects/status/r23jiolwgvqqbb9e?svg=true)](https://ci.appveyor.com/project/feeleen/ozonsellerapi)

# OzonSellerApi for .NET

Target Framework .NET 5.0

Initialization:
```cs

var ApiService = OzonSellerApiService.Instance;
ApiService.Configure("https://api-seller.ozon.ru", "<your ApiKey>", "<your ClientId>");

```

Usage examples:

```cs
[TestMethod]
public async Task TestGetDeliveryMethodList()
{
	var result = await ApiService.GetDeliveryMethodListAsync(
			new DeliveryMethodListParameters()
			{
				Limit = 1,
				Offset = 0,
				Filter = new DeliveryMethodListFilter() 
				{ 
					ProviderId = 0, 
					Status = DeliveryMethodStatus.ACTIVE, 
					WarehouseId = null 
				}
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


Command description:

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

There are still a lot of things to do 
