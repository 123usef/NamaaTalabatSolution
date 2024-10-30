using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Api.Errors;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Repository.Data;

namespace NamaaTalabat.Api.Controllers
{
    [Route("error")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("notfound")] // Not Found
        public IActionResult GetNotFound()
        {
            var product = _dbContext.products.Find(100);
            if (product is null)
                return NotFound(new ApiResponse(400));

            return Ok(product);
        }
        [HttpGet("servererror/{id}")]
        public ActionResult<Product> GetServerError(int id)
        {
            var product = _dbContext.products.Find(id);
            var ProductToReturn = product.ToString();

            return Ok(ProductToReturn);
        }

        [HttpGet("badRequest")] //GET " api/buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest();

        }

        [HttpGet("badRequest/id")] //Get : api/buggy/badrequest/five
        public ActionResult GetBadRequest(int id) //Validation Error
        {
            return Ok();

        }

    }
}
