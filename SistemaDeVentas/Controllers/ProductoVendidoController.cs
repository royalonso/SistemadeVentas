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
        public ActionResult GET()                                            //public IEnumerable<ProductoVendido> Get()
        {
            List<ProductoVendido> productv = new List<ProductoVendido>();
            productv = ProductoVendidoRepository.DevolverProductoVendido();
            return Ok(productv);                                              //return Ok;
        }
        [HttpGet("Listado")] //Personalizo el GET
        public IEnumerable<ProductoVendido> GETlISTADO()                                            
        {
            List<ProductoVendido> productv = new List<ProductoVendido>();
            productv = ProductoVendidoRepository.DevolverProductoVendido2();
            return (productv);                                              
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductoVendido productovendido)
        {
            try
            {
                bool p = repository.CrearProductoVendido(productovendido); 

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


        }
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
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
