using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs;
using CoffeeShop.Models.DTOs.ProductDTOs;
using CoffeeShop.Services;
using CoffeeShop.Services.IRepository;
using CoffeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _ps;

        public ProductsController(IProductService ps)
        {
            _ps = ps;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _ps.GetProductsAsync();
            var productsDto = products.Select( products=> new ProductDTO
            {
                ProductId = products.ProductId,
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                Stock = products.Stock,
                ImageUrl = products.ImgUrl,
                CategoryId = products.CategoryId,
                CategoryName = products.Category?.CategoryName
            });
          return  Ok(productsDto);
        }
        [HttpGet("{id:int}", Name ="GetProductByID")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _ps.GetProductByIdAsync(id);
            if(product is null)
            {
               return NotFound();
            }
            if (!_ps.CategoryExists(product.CategoryId))
            {
                return NotFound();
            }
            var productDTO = new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImgUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName  
            };
          return  Ok(productDTO);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromForm]  CreateProductDTO createProductDTO)
        {
            if(!ModelState.IsValid)
            {
                 return BadRequest(ModelState);   
            }
            if (string.IsNullOrWhiteSpace(createProductDTO.Name))
            {
               ModelState.AddModelError("Error",$"El producto no puede estar vacio y la categoria no puede ser 0 ");
                 return BadRequest();   
            }
            if (!_ps.CategoryExists(createProductDTO.CategoryId))
            {
               return  NotFound(); 
            }
            if (_ps.ProductExists(createProductDTO.Name))
            {
                 ModelState.AddModelError("Error",$"El producto {createProductDTO.Name} ya existe");
                 return BadRequest();
            }   
            var productoDTO = new Product
            { 
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Price = createProductDTO.Price,
                Stock = createProductDTO.Stock,
                CategoryId = createProductDTO.CategoryId
            };
            if(createProductDTO.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString()+ Path.GetExtension(createProductDTO.ImageFile.FileName);
                var path = Path.Combine("wwwroot/images",fileName);
                using var stream = new FileStream(path,FileMode.Create);
                await createProductDTO.ImageFile.CopyToAsync(stream);
                productoDTO.ImgUrl = "/images/" + fileName;
            }
          
            var createdProduct = await _ps.CreateProductAsync(productoDTO);
           return Ok(createdProduct);
        }
    }
}