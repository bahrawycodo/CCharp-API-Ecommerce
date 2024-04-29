using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context,ILoggerFactory loggerFactory)
        {
			try
			{
				if(context.ProductBrands is not null && !context.ProductBrands.Any())
				{
					var brandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
					if (brands is not null)
					{
						await context.ProductBrands.AddRangeAsync(brands);
						await context.SaveChangesAsync();
					}
                }
                if (context.ProductTypes is not null && !context.ProductTypes.Any())
				{
					var typeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
					if (types is not null)
					{
						await context.ProductTypes.AddRangeAsync(types);
						await context.SaveChangesAsync();
					}
                }    
				if (context.DeliveryMethods is not null && !context.DeliveryMethods.Any())
				{
					var deliveryData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
					var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
					if (methods is not null)
					{
						await context.DeliveryMethods.AddRangeAsync(methods);
						await context.SaveChangesAsync();
					}
                }
                if (context.Products is not null && !context.Products.Any())
				{
					var productData = File.ReadAllText("../Store.Repository/SeedData/products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productData);
					if (products is not null)
					{
						await context.Products.AddRangeAsync(products);
						await context.SaveChangesAsync();
					}
                }
            }
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<StoreContextSeed>();
				logger.LogError(ex.Message);
			}
        }
    }
}
