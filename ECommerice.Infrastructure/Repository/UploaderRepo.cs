using ECommerice.Core.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.Repository
{
    public class UploaderRepo : IUploaderRepo
    {
        private IHostingEnvironment _hostingEnvironment;

        public UploaderRepo() { }

        public async Task<string> UploadFile([FromForm] IFormFile file, IHostingEnvironment hostingEnvironment, string imagesPath)
        {
            _hostingEnvironment = hostingEnvironment;
            try
            {
                string fileName = "";
                if (file.Length > 0)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\" + imagesPath + "\\Products\\";
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string fullPath = Path.Combine(webRootPath, fileName);

                    using (FileStream fileStream = System.IO.File.Create(fullPath))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                    }
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
