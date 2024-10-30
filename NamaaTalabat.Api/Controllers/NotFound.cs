using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamaaTalabat.Api.Errors;

namespace NamaaTalabat.Api.Controllers
{
    [Route("notfound/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class NotFound : ControllerBase
    {
        public ActionResult notfound(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
