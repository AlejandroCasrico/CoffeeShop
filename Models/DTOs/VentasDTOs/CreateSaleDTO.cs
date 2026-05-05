using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.VentasDTOs
{
    public class CreateSaleDTO
    {
        public List<SalesItemDTO>? Items { get; set; }
    }
}