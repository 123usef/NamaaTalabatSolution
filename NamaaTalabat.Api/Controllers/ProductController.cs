using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Api.DTOS;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Repositories.Contract;

namespace NamaaTalabat.Api.Controllers
{
   
    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
           var products = await  _productRepo.GetAll();
            var productDtos = new List<ProductDto>();
            var tempdto = new ProductDto();
            foreach (var item in products)
            {   
                tempdto.Name = item.Name;
                tempdto.Description = item.Description;
                tempdto.Price = item.Price;
                tempdto.CategoryName = item.Category.Name;
                tempdto.BrandName = item.Brand.Name;
                productDtos.Add(tempdto);
                
            }
            return Ok(productDtos);
        }
        [HttpGet("{id}")]
        public async  Task<ActionResult<Product>> Get(int id ) { 
            var product = await _productRepo.Get(id);
            return Ok(product);
        
        }


    }
}
