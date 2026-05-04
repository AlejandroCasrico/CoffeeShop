using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoffeShop.Models;

namespace CoffeeShop.Models.DTOs
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Range(0,double.MaxValue)]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
         [Range(0, int.MaxValue)]

        public int Stock { get; set; }
         public string? ImgUrl { get; set; }
        public DateTime CreationDate { get; set; } =  DateTime.UtcNow;
        public DateTime? UpdateDate { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category{get;set;}
 
         public ICollection<SaleDetail>? Details { get; set; }
    }
}