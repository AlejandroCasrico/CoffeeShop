using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;

namespace CoffeeShop.Models
{
    public class Venta
    {
    public int Id { get; set; }
    public ICollection<SaleDetail>? Details{get;set;}
    public DateTime Date { get; set; }
    }
}