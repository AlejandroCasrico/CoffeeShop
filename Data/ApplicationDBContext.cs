using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models;
using CoffeeShop.Models.DTOs;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeShop.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): 
        base(options)
        {
            
        }
        public DbSet<Product> Product{get;set;}
        public DbSet<Category> Categories{get;set;}
        public DbSet<User> Users{get;set;}
        public DbSet<Venta> Ventas {get;set;}
        public DbSet<SaleDetail> SaleDetails{get;set;}
    }
}