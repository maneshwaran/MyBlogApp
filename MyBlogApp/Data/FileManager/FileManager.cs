using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _ImagePath;

        public FileManager(IConfiguration config)
        {
            _ImagePath = config["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_ImagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var save_path = Path.Combine(_ImagePath);
                if (!Directory.Exists(save_path))
                {
                    Directory.CreateDirectory(save_path);
                }
                var mime = image.FileName.Substring(image.FileName.LastIndexOf("."));
                var fileName = $"img.{DateTime.Now.ToString("dd-MM-yyyy-HH-MM-ss")}{mime}";
                using(var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return "Error";
            }
        }
    }
}
