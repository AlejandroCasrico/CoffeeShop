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
        public async Task<SaleDTO> AddAsyncVenta(List<SalesItemDTO> items )
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
            var ventaDTO = new SaleDTO
            {
                Id = venta.Id,
                Date = venta.Date,
                Details = venta.Details.Select(d => new SaleDetailDTO
                {
                    ProductName = _productoRepository.GetProductByIdAsync(d.ProductId).Result?.Name,
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            };
            return ventaDTO;
        }

       
        public async  Task<SaleDTO?> GetVentaIdAsync(int id)
        {
            var venta  = await _db.Ventas
            .Include(v => v.Details)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(v => v.Id == id);
            if(venta is null)
            {
                return null;
            }
            return new SaleDTO
            {
                 Id = venta.Id,
                 Date = venta.Date,
                 Details = venta.Details?.Select(d => new SaleDetailDTO
                {
                    ProductName = d.Product!.Name,
                    Quantity = d.Quantity,
                    Price = d.Price

                }).ToList()
            };
        }

        public async Task<ICollection<SaleDTO>> Ventas()
        {
            var ventas  = await _db.Ventas.AsNoTracking()
            .Include(d => d.Details)
            .ThenInclude(d=> d.Product)
            .OrderByDescending(v=> v.Date)
            .ToListAsync();
            return ventas.Select(v => new SaleDTO
            {
                Id = v.Id,
                Date = v.Date,
                Details = v.Details?.Select(d => new SaleDetailDTO
                {
                
                    ProductName = d.Product?.Name,
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            }).ToList();
        }
    }
}