using Microsoft.AspNetCore.Mvc;


namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuario = new List<Usuario>();
            usuario = Usuario.DevolverUsuarios();
            return usuario;
        }

    }
}
