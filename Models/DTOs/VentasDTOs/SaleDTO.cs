using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.VentasDTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<SaleDetailDTO>? Details { get; set; }
    }
}