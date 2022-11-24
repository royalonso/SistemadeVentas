using Microsoft.AspNetCore.Mvc;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : Controller
    {
        /*   public IActionResult Index()
               {
                   return View();
               }
            */
        [HttpGet]
        public IEnumerable<ProductoVendido> Get()
        {
            List<ProductoVendido> productv = new List<ProductoVendido>();
            productv = ProductoVendido.DevolverProductoVendido();
            return productv;
        }
    }
}
