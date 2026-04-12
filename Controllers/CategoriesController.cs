using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;
using CoffeeShop.Services;
using CoffeShop.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _cs;
        public CategoriesController(CategoryService cs)
        {
            _cs = cs;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryAsync()
        {
            var categories = await _cs.GetCategories();
            var categoriesDTO = categories.Select(categories => new CategoryDTO
            {
                Id = categories.Id,
                CategoryName = categories.CategoryName,
                Description = categories.Description
            }
            );
            return Ok(categoriesDTO);
        }
    }
}