using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        [Required]
        [MaxLength(40, ErrorMessage ="Ha superado el numero de caracteres permimtido")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
         [Range(0, int.MaxValue)]
        public int Stock { get; set; }
         public IFormFile? ImageFile { get; set; }
         [Required]
         [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}