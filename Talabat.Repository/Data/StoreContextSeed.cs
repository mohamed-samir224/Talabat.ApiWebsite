using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext dbContext)
        {
            if (dbContext.productBrands.Count() == 0)
            {
                var ProductBrand = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrand);

                if (Brands?.Count() > 0)
                {
                    foreach (var Brand in Brands)
                    {

                        dbContext.Set<ProductBrand>().Add(Brand);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (dbContext.productCategories.Count() == 0)
            {
                var ProductCategory = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(ProductCategory);

                if (Categories?.Count() > 0)
                {
                    foreach (var Category in Categories)
                    {

                        dbContext.Set<ProductCategory>().Add(Category);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (dbContext.products.Count() == 0)
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Products?.Count() > 0)
                {
                    foreach (var Product in Products)
                    {

                        dbContext.Set<Product>().Add(Product);

                    }
                    await dbContext.SaveChangesAsync();


                }
            }

			if (dbContext.DeliveryMethods.Count() == 0)
			{
				var deliveryMethods = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");

				var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethods);

				if (DeliveryMethods?.Count() > 0)
				{
					foreach (var deliverymethod in DeliveryMethods)
					{

						dbContext.Set<DeliveryMethod>().Add(deliverymethod);

					}
					await dbContext.SaveChangesAsync();


				}
			}



		}

    }
}
