using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;

namespace CoffeeShop.Models
{
    public class SaleDetail
    {
        [Key]
        public int Id{get;set;}
        public int VentaId { get; set; }
        public Venta? Venta { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        [Range(0,double.MaxValue)]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
    }
}