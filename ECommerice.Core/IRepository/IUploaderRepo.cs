using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.IRepository
{
    public interface IUploaderRepo
    {
        Task<string> UploadFile([FromForm] IFormFile file, IHostingEnvironment hostingEnvironment, string imagesPath);
    }
}
