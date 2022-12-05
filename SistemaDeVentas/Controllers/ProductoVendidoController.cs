using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;
namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/vRA1/[controller]")]
    public class ProductoVendidoController : Controller
    {
        private ProductoVendidoRepository repository = new ProductoVendidoRepository();
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
        public IActionResult Delete([FromBody] int id)
        {
            //bool borradoPV = new ProductoVendidoRepository().EliminarProductoVendido(id);
            bool borradoPV = repository.EliminarProductoVendido(id);
            if (borradoPV)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }
    }
}
