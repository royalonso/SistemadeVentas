using Microsoft.AspNetCore.Mvc;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        /* public IActionResult Index()
        { 
            return View();
        }
        */
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuario = new List<Usuario>();
            usuario = Usuario.DevolverUsuarios();
            return usuario;
        }

    }
}
