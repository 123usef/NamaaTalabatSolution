using NamaaTalabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Core.Specification
{
    public class ProductWithBrandAndCategorySpecififcation : BaseSpecification<Product>
    {
        public ProductWithBrandAndCategorySpecififcation():base()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);

            
        }
        public ProductWithBrandAndCategorySpecififcation(int id) : base(p=> p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);


        }

    }
}
