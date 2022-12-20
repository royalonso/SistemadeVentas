using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;
namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/vRA1/[controller]")] /// [Route("[controller]/[action]")] si quiero agregar otro get por ejemplo
    public class VentaController : Controller
    {
        private VentaRepository repository = new VentaRepository();
        [HttpGet]
        public IActionResult GET()                            //public IEnumerable<Venta> Get()
        {
            List<Venta> venta1   = new List<Venta>();
            venta1 = VentaRepository.DevolverVenta(); 
            return Ok(venta1);                                // return venta1;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                bool p = repository.CrearVenta(venta);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        /*
        [HttpPost("Listado")] //Personalizo el GET
                              // public IEnumerable<ProductoVendido> GETlISTADO()
        public IActionResult PostListado([FromBody] Venta venta)
        {
            try
            {
                bool p = repository.CrearVenta(venta);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        */
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            bool borrado = new VentaRepository().EliminarVenta(id);
            if (borrado)
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
        { return Ok();
        }      
    }
}
