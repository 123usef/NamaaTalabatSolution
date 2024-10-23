using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Repositories.Contract;

namespace NamaaTalabat.Api.Controllers
{
    public class ProductBrandController : BaseController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public ProductBrandController(IGenericRepository<ProductBrand> productBrandRepo)
        {
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAll()
        {
            var prodBrands = await _productBrandRepo.GetAll();
            
            return Ok(prodBrands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> Get(int id)
        {
            var prodBrand = await _productBrandRepo.Get(id);
            return Ok(prodBrand);
        }
    }
}
