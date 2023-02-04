using ECommerice.Core.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly IUploaderRepo _uploaderRepo;
        private IHostingEnvironment _hostingEnvironment;

        public UploaderController(IUploaderRepo uploaderRepo,
            IHostingEnvironment HostingEnvironment)
        {
            _uploaderRepo = uploaderRepo;
            _hostingEnvironment = HostingEnvironment;
        }

        [HttpPost("upload/{path}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, string path)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest();

                var result = await _uploaderRepo.UploadFile(file, _hostingEnvironment, path);
                var imageName = result;
                return Ok(imageName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
