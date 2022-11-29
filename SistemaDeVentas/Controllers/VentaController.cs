﻿using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;
namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")] /// [Route("[controller]/[action]")] si quiero agregar otro get por ejemplo
    public class VentaController : Controller
    {
        
        [HttpGet]
        public IEnumerable<Venta> Get()
        {
            List<Venta> venta1   = new List<Venta>();
            venta1 = VentaRepository.DevolverVenta(); //VentaRepository.DevolverVenta();
            return venta1;
        }
        [HttpPost]
        public IActionResult Post() 
        {
         return Ok(); 
        }        
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult Put() 
        { return Ok();
        }      
    }
}
