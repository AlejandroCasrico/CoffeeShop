using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models;
using CoffeeShop.Models.DTOs.VentasDTOs;
using CoffeeShop.Services.IRepository;
using CoffeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services
{
    public class VentasService : IVentaRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IProductService _productoRepository;
        public VentasService(ApplicationDBContext db, IProductService productoRepository)
        {
            _db = db;     
            _productoRepository = productoRepository;       
        }
        public async Task<Venta> AddAsyncVenta(List<SalesItemDTO> items )
        {
            var venta = new Venta
            {
                Date = DateTime.Now,
                Details = new List<SaleDetail>()
            };
            foreach( var item in items)
            {
                var product =  await _productoRepository.GetProductByIdAsync(item.ProductId);
                if(product is null)
                {
                    throw new Exception($"Producto con ID {item.ProductId} no encontrado.");
                }
                if(product.Stock < item.Quantity)
                {
                    throw new Exception($"Stock insuficiente para el producto {product.Name}. Stock disponible: {product.Stock}");
                }
                product.Stock -= item.Quantity;
                venta.Details.Add(new SaleDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }
            _db.Ventas.Add(venta);
            await _db.SaveChangesAsync();
            return venta;
        }

        public Task<Venta> AddAsyncVenta(Venta venta)
        {
            throw new NotImplementedException();
        }

        public async  Task<Venta?> GetVentaIdAsync(int id)
        {
            return await _db.Ventas.Include(d => d.Details).FirstOrDefaultAsync(v=> v.Id == id);
        }

        public async Task<ICollection<Venta>> Ventas()
        {
          return  await _db.Ventas.AsNoTracking().Include(d => d.Details).OrderByDescending(v => v.Date).ToListAsync();
        }
    }
}