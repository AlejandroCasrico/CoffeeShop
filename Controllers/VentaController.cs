using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.DTOs.VentasDTOs;
using CoffeeShop.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly Services.IRepository.IVentaRepository _ventaRepositor;
        public VentaController(IVentaRepository iv)
        {
            _ventaRepositor = iv;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVentas()
        {
            var ventas = await _ventaRepositor.Ventas();
            if(ventas is null || ventas.Count == 0)
            {
                return NotFound("No se encontraron ventas.");
            }
            return Ok(ventas);
        }
        [HttpGet("{id:int}", Name ="GetVentaByID")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVentaById(int id)
        {
            var venta = await _ventaRepositor.GetVentaIdAsync(id);
            if(venta is null)
            {
               return NotFound("Venta no encontrada.");
            }
            return Ok(venta);
        }
         [HttpPost]
         [ProducesResponseType(StatusCodes.Status403Forbidden)] 
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateVenta(List<SalesItemDTO> items)
        {
            
                var venta = await _ventaRepositor.AddAsyncVenta(items);
                if(venta is null)
                {
                    return BadRequest("No se pudo crear la venta.");
                }
            
                return CreatedAtRoute("GetVentaByID", new { id = venta.Id }, venta);
   
        }
    }
}