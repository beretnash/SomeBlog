using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SomeBlog.Application.Common;
using SomeBlog.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SomeBlog.Application.Providers
{
    public class ImageProvider : IImageProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            var imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolderPath.Base);

            if (new DirectoryInfo(imagesFolder).Exists == false)
            {
                Directory.CreateDirectory(imagesFolder);
            }

        }

        public async Task<string> SaveAsync(IFormFile formFile)
        {
            if (formFile.Length == 0)
            {
                return null;
            }

            var blogImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, Constants.ImagesFolderPath.Blogs);
            var fileExtension = new FileInfo(formFile.FileName).Extension;

            if (new DirectoryInfo(blogImagesFolder).Exists == false)
            {
                Directory.CreateDirectory(blogImagesFolder);
            }

            var filePath = Path.Combine(blogImagesFolder, $"{Guid.NewGuid()}{fileExtension}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
