using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;
using SistemaDeVentas;
namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/vRA1/[controller]")]
    public class LoginController : Controller
    {
        private LoginRepository repository = new LoginRepository();
        
        [HttpPost]
        public ActionResult<LoginUsuario> Login(LoginUsuario lusuario)
        {
            try
            {
                bool usuarioExiste = repository.VerificarUsuario(lusuario);
                return usuarioExiste ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
