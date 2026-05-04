using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models;
using CoffeeShop.Models.DTOs;
using CoffeShop.Migrations;

namespace CoffeeShop.Services.IRepository
{
    public interface ISalesRepository
    {
        Task AddSaleAsync(Venta venta);
        void InformacionVentas(Product product);
    }
}