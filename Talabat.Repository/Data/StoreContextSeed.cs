using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext DbContext)
        {
            if (!DbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                        await DbContext.Set<ProductBrand>().AddAsync(Brand);
                    await DbContext.SaveChangesAsync();
                }
            }

            if (!DbContext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                        await DbContext.Set<ProductType>().AddAsync(Type);
                    await DbContext.SaveChangesAsync();
                }

            }
            if (!DbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                        await DbContext.Set<Product>().AddAsync(Product);
                    await DbContext.SaveChangesAsync();
                }

            }
        }
    }
}
