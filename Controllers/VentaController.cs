using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
               return NotFound();
            }
            return Ok(venta);
        }
        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)] 
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status201Created)]
   
    }
}