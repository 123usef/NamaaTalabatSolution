using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public decimal  Price { get; set; }
        public string PictureUrl { get; set; }

        [ForeignKey(nameof(Product.Category))]
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        [ForeignKey(nameof(Product.Brand))]
        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }

    }
}
