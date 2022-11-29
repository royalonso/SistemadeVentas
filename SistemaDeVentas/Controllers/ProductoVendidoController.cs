using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;
namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : Controller
    {
        
        [HttpGet]
        public IActionResult GET()                                           //public IEnumerable<ProductoVendido> Get()
        {
            List<ProductoVendido> productv = new List<ProductoVendido>();
            productv = ProductoVendidoRepository.DevolverProductoVendido();
            return Ok(productv);                                              //return Ok;
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
