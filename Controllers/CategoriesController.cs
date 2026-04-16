using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;
using CoffeeShop.Models.DTOs.CategoryDTOs;
using CoffeeShop.Services;
using CoffeeShop.Services.IRepository;
using CoffeShop.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _cs;
        public CategoriesController(ICategoryRepository cs)
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
            ).ToList();
            return Ok(categoriesDTO);
        }
        [HttpGet("{id:int}",Name ="GetCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _cs.GetCategory(id);  
            if(category == null)
            {
                return NotFound();
            } 
            var categoryDTO =  new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            return Ok(categoryDTO);
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            if(createCategoryDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(createCategoryDTO.CategoryName))
            {
                ModelState.AddModelError("Error", "El nombre es obligatorio");
                return BadRequest(ModelState);
            }
            if (_cs.CategoryExists(createCategoryDTO.CategoryName!))
            {
                ModelState.AddModelError("Error",$"Ya existe una categoria con {createCategoryDTO.CategoryName}");
                return BadRequest(ModelState);
            }
            var category = new Category
            {
              CategoryName = createCategoryDTO.CategoryName,
              Description = createCategoryDTO.Description
            };
             var createdCategory = await _cs.CreateCategoryAsync(category);
            if (createdCategory == null)
            {
                ModelState.AddModelError("Error",
                    $"Algo pasó al crear la categoría {createCategoryDTO.CategoryName}");

                return BadRequest(ModelState);
            }
            return Ok(createdCategory);
        }
        [HttpPut("{id:int}",Name ="UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>UpdateCategory(int id, [FromBody]UpdateCategoryDTO updateCategoryDTO)
        {
             if(updateCategoryDTO == null)
            {
                return BadRequest(ModelState);
            }
            var category = await _cs.GetCategory(id);
            if(category == null) return NotFound();
            category!.CategoryName = updateCategoryDTO.CategoryName;
            category!.Description = updateCategoryDTO.Description;
            var updateCategory = await _cs.UpdateCategoryAsync(category);
            if(updateCategory == null)
            {
                 ModelState.AddModelError("Error",$"Algo pasó al actualizar la categoría");
                return BadRequest(ModelState);
            }
         return NoContent();
        }
        [HttpDelete("{id:int}", Name ="DeleteCategory")] 
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleteCategory = await _cs.DeleteCategoryAsync(id);
              if(!deleteCategory)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}