using NamaaTalabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NamaaTalabat.Repository.Data
{
    public class DataSeedContext
    {
        public static void SeedDatabase(StoreContext _dbContext)
        {
            var brands = File.ReadAllText("../NamaaTalabat.Repository/Data/DataSeed/brands.json");
            var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brands);
           
            var CategoryList = File.ReadAllText("../NamaaTalabat.Repository/Data/DataSeed/categories.json");          
            var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryList);

            var ProductList = File.ReadAllText("../NamaaTalabat.Repository/Data/DataSeed/products.json");       
            var products = JsonSerializer.Deserialize<List<Product>>(ProductList);

            if (!(_dbContext.Set<ProductBrand>().Any()))
            {
                foreach (var br in brand)
                {
                    _dbContext.Set<ProductBrand>().Add(br);
                }
                _dbContext.SaveChanges();

            }

            if (!(_dbContext.Set<ProductCategory>().Any()))
            {


                foreach (var br in Categories)
                {
                    _dbContext.Set<ProductCategory>().Add(br);
                }
                _dbContext.SaveChanges();

            }
            if (!(_dbContext.Set<Product>().Any()))
            {
                foreach (var br in products)
                {
                    _dbContext.Set<Product>().Add(br);
                }
                _dbContext.SaveChanges();

            }
            //}



        }
    }
}
