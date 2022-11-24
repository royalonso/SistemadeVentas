using Microsoft.AspNetCore.Mvc;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : Controller
    {
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        [HttpGet]
        public IEnumerable<Venta> Get()
        {
            List<Venta> venta1   = new List<Venta>();
            venta1 = Venta.DevolverVenta();
            return venta1;
        }
    }
}
