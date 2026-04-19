using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeShop.Models;

namespace CoffeeShop.Models.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
         public string? ImageUrl { get; set; }
        public DateTime CreationDate { get; set; } 
        public DateTime? UpdateDate { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName {get;set;}
    }
}