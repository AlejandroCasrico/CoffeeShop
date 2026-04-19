using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoffeShop.Models;

namespace CoffeeShop.Models.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
         [Range(0, int.MaxValue)]
        public int Stock { get; set; }
         public IFormFile? ImageFile { get; set; }
         [Required]
        public int CategoryId { get; set; }
        public Category? CategoryName { get; set; }
    }
}