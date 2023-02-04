using ECommerice.Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseApiController
    {
        //[HttpGet]
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
