using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Api.DTOS;
using NamaaTalabat.Api.Errors;
using NamaaTalabat.Api.Helpers;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Repositories.Contract;
using NamaaTalabat.Core.Specification;

namespace NamaaTalabat.Api.Controllers
{
   
    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrand;
        private readonly IGenericRepository<ProductCategory> _productCategory;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> ProductRepo ,
            IGenericRepository<ProductBrand> productBrand,
            IGenericRepository<ProductCategory> productCategory,
            IMapper mapper )
        {
            _productRepo = ProductRepo;
            _productBrand = productBrand;
            _productCategory = productCategory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            #region GetAllWithoutSpec
            //var products = await  _productRepo.GetAll();

            //var productDtos = new List<ProductDto>();
            //var tempdto = new ProductDto();
            //foreach (var item in products)
            //{   
            //    tempdto.Name = item.Name;
            //    tempdto.Description = item.Description;
            //    tempdto.Price = item.Price;
            //    tempdto.CategoryName = item.Category.Name;
            //    tempdto.BrandName = item.Brand.Name;
            //    productDtos.Add(tempdto);

            //} 
            #endregion
            //var spec = new BaseSpecification<Product>();
            //spec.Includes.Add(p => p.Brand);
            //spec.Includes.Add(p => p.Category);
            var spec = new ProductWithBrandAndCategorySpecififcation();

            var producs = await _productRepo.GetAllWithSpec(spec);
           

            return Ok( _mapper.Map<IEnumerable<Product> , IEnumerable<ProductDto>>(producs) );
        }
        [HttpGet("{id}")]
        public async  Task<ActionResult<ProductDto>> Get(int id ) {

            var spec = new ProductWithBrandAndCategorySpecififcation(id);
            // return Null
            var product = await _productRepo.GetByIdWithSpec(spec);
            //var product = await _productRepo.Get(id);
            if (product is null)
                return NotFound(new { Message = "Product Not Found " , StatusCode = StatusCodes.Status404NotFound});

            return Ok(_mapper.Map<Product , ProductDto>(product));
            //return Ok(product);
        
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> brands()
        {
            var brands = await  _productBrand.GetAll();

            return Ok(brands);
        }
        [HttpGet("brands/{id}")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> Getbrand(int id )
        {
            var brands = await _productBrand.Get(id);
            if (brands is null)
                return NotFound(new ApiResponse(400));
            return Ok(brands);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllCategory()
        {
            var Cats = await _productCategory.GetAll();

            return Ok(Cats);
        }
        [HttpGet("Categories/{id}")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetCategory(int id)
        {
            var cat = await _productCategory.Get(id);
            if (cat is null)
                return NotFound(new ApiResponse(400));
            return Ok(cat);
        }

    }
}
