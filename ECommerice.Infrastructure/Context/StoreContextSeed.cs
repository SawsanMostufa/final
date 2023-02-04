using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerice.Core.Entities;
using ECommerice.Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace ECommerice.Infrastructure.Context
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory )
        {
            try
            {
               if(!context.Category.Any()){
                var typesData = File.ReadAllText("../ECommerice.Infrastructure/SeedData/category.json");
                var types = JsonSerializer.Deserialize<List<Category>>(typesData);

                foreach(var type in types)
                {
                    context.Category.Add(type);
                }
                //context.ProductType.AddRange(types);
                await context.SaveChangesAsync();
              }

               if(!context.Product.Any()){
                var productsData = File.ReadAllText("../ECommerice.Infrastructure/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                foreach(var pro in products)
                {
                    context.Product.Add(pro);
                }
                //context.Product.AddRange(products);
                await context.SaveChangesAsync();
              }

                if (!context.DeliveryMethod.Any())
                {
                    var dMethod = File.ReadAllText("../ECommerice.Infrastructure/SeedData/delivery.json");
                    var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(dMethod);

                    foreach (var dm in deliveryMethod)
                    {
                        context.DeliveryMethod.Add(dm);
                    }
                    await context.SaveChangesAsync();
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