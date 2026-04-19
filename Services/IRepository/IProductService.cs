using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;

namespace CoffeeShop.Services.IRepository
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> Save();
        bool ProductExists(string name);
        bool CategoryExists(int CategoryId);
    }
}