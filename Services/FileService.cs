using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Services.IRepository;

namespace CoffeeShop.Services
{
    public class FileService : IImageFileRepository
    {
        private readonly string _imagePath = Path.Combine("wwwroot","images");
       
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var allowedExtensions = new[]{".jpg",".png",".jpeg"};
            var extension  = Path.GetExtension(file.Name).ToLower();
            if( file == null || file.Length == 0)
            {
                throw new ArgumentException("Archivo Invalido");
            }
            if (!Directory.Exists(_imagePath))
            {
                Directory.CreateDirectory(_imagePath);
            }
        
            if(!allowedExtensions.Contains(extension))
            {
                throw new Exception("No es un archivo del tipo correcto");
            }
             var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(_imagePath,filename);
                using var stream = new FileStream(fullPath,FileMode.Create);
                await file.CopyToAsync(stream);
               return   "/images/" + filename;
        }
    }
}