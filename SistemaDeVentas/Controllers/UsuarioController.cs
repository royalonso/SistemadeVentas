using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        
        [HttpGet]
        public IActionResult GET()                                     //public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuario = new List<Usuario>();
            usuario = UsuarioRepository.DevolverUsuarios();
            return Ok(usuario);                                       // return usuario;
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
