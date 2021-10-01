using Microsoft.VisualStudio.TestTools.UnitTesting;
using OzonSellerApi;
using OzonSellerApi.Enums;
using System.Threading.Tasks;

namespace Tests
{
	[TestClass]
	public class MainTest
	{
		public static OzonSellerApiService ApiService { get { return OzonSellerApiService.Instance; } }

		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context)
		{
			ApiService.Configure("https://api-seller.ozon.ru", "<your ApiKey>", "<your ClientId>");
		}

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

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
