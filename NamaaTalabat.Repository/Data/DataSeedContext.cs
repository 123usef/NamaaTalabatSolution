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
            //var brands = File.ReadAllText("./Dataseed/brands.json");
            var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brands);

            //if(brand is not null && brand.Count() > 0)
            //if(!(brand?.Count() > 0) )
            //{
            //brand = brand.Select<>()
            if (!(_dbContext.Set<ProductBrand>().Any()))
            {


                foreach (var br in brand)
                {
                    _dbContext.Set<ProductBrand>().Add(br);
                }
                _dbContext.SaveChanges();

            }
            //}



        }
    }
}
