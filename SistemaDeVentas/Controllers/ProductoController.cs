using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
     
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
          List<Producto> products = new List<Producto>();
            products = ProductoRepository.DevolverProducto();
            return products;
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
        {
            return Ok();
        }
    }
}
