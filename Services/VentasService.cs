using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models;
using CoffeeShop.Services.IRepository;
using CoffeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services
{
    public class VentasService : IVentaRepository
    {
        private readonly ApplicationDBContext _db;
        public VentasService(ApplicationDBContext db)
        {
            _db = db;            
        }
        public async Task<Venta> AddAsyncVenta(Venta venta)
        {
            venta.Date = DateTime.Now;
            _db.Ventas.Add(venta);
            await _db.SaveChangesAsync();
            return venta;
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