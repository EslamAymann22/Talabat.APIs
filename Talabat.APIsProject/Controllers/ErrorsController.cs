using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIsProject.Errors;

namespace Talabat.APIsProject.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {

        public ActionResult Error(int code)
        => NotFound(new ApiResponses(code));

    }
}
