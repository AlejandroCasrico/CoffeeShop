using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;
using CoffeeShop.Models.DTOs.CategoryDTOs;
using CoffeShop.Models;

namespace CoffeeShop.Services.IRepository
{
    public interface ICategoryRepository
    {
       Task<IEnumerable<Category>> GetCategories();
       Task<Category?> GetCategory(int id);
       Task<Category> CreateCategoryAsync(Category category);
       Task<Category?> UpdateCategoryAsync(Category category);
       Task<bool> DeleteCategoryAsync(Category category);
       Task<bool> Save();

    }
}