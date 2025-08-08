using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi= true)] 
    public class ErrorConroller : ControllerBase
    {

        public ActionResult Error(int Code) 
        {
			return NotFound(new ApiResponse(Code));
		}

	}
}
