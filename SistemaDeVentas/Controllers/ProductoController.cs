using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.Repositories;


namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/vRA1/[controller]")]
    public class ProductoController : Controller
        
    {
        private ProductoRepository repository = new ProductoRepository();
        [HttpGet]      
        public ActionResult GET()                                 //public IEnumerable<Producto> Get()
        {
          List<Producto> products = new List<Producto>();
            products = ProductoRepository.DevolverProducto();
            return Ok(products);                                  // return products
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            try
            {
                Producto? producto = repository.obtenerProducto(id);
                if (producto != null)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound("Producto no encontrado");
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                bool p = repository.CrearProducto(producto); //ProductoRepository().CrearProducto(producto);
                
                return Ok();
            }
            catch(Exception ex)
            {
             return Problem(ex.Message); 
            }
 
            
        }
        [HttpDelete]
        public ActionResult Delete([FromBody]int id)
        {
            bool borradoPV = repository.EliminarProductoVendido(id);   // Borro productos vendidos antes de borrar un producto
            bool borrado = repository.EliminarProducto(id);            // Un producto no se borra en genereal ya que desaparece
                                                                       // todo el historial del mismo
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
        public ActionResult<Producto> Put(int id, [FromBody] Producto ProductoAActualizar)
        {
            try
            {
                Producto? productoActualizado = repository.ActualizarProducto(id, ProductoAActualizar);
            if(productoActualizado!= null)
                {
                    return Ok(productoActualizado);
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
