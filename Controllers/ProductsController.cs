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
        private readonly IImageFileRepository _img;

        public ProductsController(IProductService ps, IImageFileRepository img)
        {
            _ps = ps;
            _img = img;
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
                productoDTO.ImgUrl = await _img.SaveImageAsync(createProductDTO.ImageFile);
            }
          
            var createdProduct = await _ps.CreateProductAsync(productoDTO);
           return Ok(createdProduct);
        }
        [HttpPut("{productId:int}",Name ="UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int productId,[FromForm] UpdateProductDTO updateProductDTO)
        {
            if(updateProductDTO is null)
            {
                  ModelState.AddModelError("Error",$"El producto no puede ser null ");
                 return BadRequest();   
            }
            var product= await _ps.GetProductByIdAsync(productId);
            if(product == null)
            {
                return NotFound();
            }
           product.Name = updateProductDTO.Name;
           product.Description = updateProductDTO.Description;
           product.Price = updateProductDTO.Price;
           product.Stock = updateProductDTO.Stock;
           product.CategoryId = updateProductDTO.CategoryId;
            if (updateProductDTO.ImageFile != null)
            {
                product.ImgUrl =  await _img.SaveImageAsync(updateProductDTO.ImageFile);
            }
            var updatedProduct  =await _ps.UpdateProduct(product);
            if(updatedProduct is null)
            {
                ModelState.AddModelError("Error",$"Algo pasó al actualizar el producto");
                return BadRequest(ModelState);
            }
           return NoContent();
        }
        [HttpDelete("{id:int}",Name ="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if( id == 0)
            {
                  return BadRequest(ModelState);
            }
            var product = await _ps.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
           if(!await _ps.DeleteProductAsync(product.ProductId))
            {
                  ModelState.AddModelError("Error",$"Algo pasó al eliminar el producto");
                return BadRequest(ModelState);
            }
            return NoContent();

        }
    }
}