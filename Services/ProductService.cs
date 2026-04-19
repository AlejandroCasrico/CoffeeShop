using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;
using CoffeeShop.Services.IRepository;
using CoffeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services
{
    public class ProductService : IProductService
    {
        private ApplicationDBContext _db;
        public ProductService(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreationDate = DateTime.Now;
            await _db.Product.AddAsync(product);
            await Save();
            return product;
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _db.Product.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _db.Product.AsNoTracking().Include(p => p.Category).OrderBy(c => c.Name).ToListAsync();
        }

        public bool ProductExists(string name)
        {
            return _db.Product.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
        }
        public bool CategoryExists(int CategoryId)
        {
            return _db.Categories.Any(c => c.Id == CategoryId );
        }
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
           product.UpdateDate = DateTime.Now;
        
           _db.Product.Update(product);
           await Save();
           return product;
        }
            public async Task<bool> DeleteProductAsync(int id)
        {
            var category = await _db.Product.FindAsync(id);
            if( category == null)
            {
                return false;
            }
             _db.Product.Remove(category);
           return await Save();
        }
    }
}