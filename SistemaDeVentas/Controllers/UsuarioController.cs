using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/vRA1/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();
        [HttpGet]
        public ActionResult GET()                                     //public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuario = new List<Usuario>();
            usuario = UsuarioRepository.DevolverUsuarios();
            return Ok(usuario);                                       // return usuario;
        }
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            try
            {
                Usuario? usuario = repository.obtenerUsuario(id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete([FromBody] int id)
        {
            //bool borrado = new UsuarioRepository().EliminarUsuario(id);
            bool borrado = repository.EliminarUsuario(id);  
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
        public ActionResult<Usuario> Put(int id, [FromBody] Usuario usuarioAActualizar)
        {
            try
            {
                Usuario? usuarioActualizado = repository.ActualizarUsuario(id, usuarioAActualizar);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("El producto no fue encontrado");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


        }

    }
}
