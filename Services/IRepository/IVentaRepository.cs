using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models;

namespace CoffeeShop.Services.IRepository
{
    public interface IVentaRepository
    {
        Task<Venta> AddAsyncVenta(Venta venta);
        Task<Venta?> GetVentaIdAsync(int id);
        Task<ICollection<Venta>> Ventas();
        
    }
}