using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Services.IRepository;
using CoffeShop.Data;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services
{
    public class CategoryService: ICategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.CreationDate = DateTime.Now;
            await _db.Categories.AddAsync(category);
            await Save();
            return category;
        }

        public Task<bool> DeleteCategoryAsync(Category category)
        {
            _db.Categories.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
           return  await _db.Categories.AsNoTracking().OrderBy(c=>c.CategoryName).ToListAsync();
        }

        public async Task<Category?> GetCategory(int id)
        {
            return await _db.Categories.AsNoTracking().FirstOrDefaultAsync( c => c.Id == id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
             category.CreationDate = DateTime.Now;
            _db.Categories.Update(category);
            await Save();
            return category;
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

    }
}