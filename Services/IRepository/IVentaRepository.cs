using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models;
using CoffeeShop.Models.DTOs.VentasDTOs;

namespace CoffeeShop.Services.IRepository
{
    public interface IVentaRepository
    {
        Task<SaleDTO> AddAsyncVenta(List<SalesItemDTO> items);
        Task<SaleDTO?> GetVentaIdAsync(int id);
        Task<ICollection<SaleDTO>> Ventas();
        
    }
}