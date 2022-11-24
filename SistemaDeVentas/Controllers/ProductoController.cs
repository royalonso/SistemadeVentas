﻿using Microsoft.AspNetCore.Mvc;


namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
     /*   public IActionResult Index()
        {
            return View();
        }
     */
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
          List<Producto> products = new List<Producto>();
            products = Producto.DevolverProducto();
            return products;
        }
    }
}