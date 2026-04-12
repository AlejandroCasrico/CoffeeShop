using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}