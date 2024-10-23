using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Repositories.Contract;

namespace NamaaTalabat.Api.Controllers
{
    
    public class ProductCategoryController : BaseController
    {
        private readonly IGenericRepository<ProductCategory> _productCategoryRepo;

        public ProductCategoryController(IGenericRepository<ProductCategory> productCategoryRepo)
        {
            _productCategoryRepo = productCategoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAll()
        {
            var prodCategs = await _productCategoryRepo.GetAll();
            return Ok(prodCategs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> Get(int id)
        {
            var prodCateg = await _productCategoryRepo.Get(id);
            return Ok(prodCateg);
        }
    }
}
