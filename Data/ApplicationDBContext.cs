using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public DbSet<Category> Categories{get;set;}
    }
}