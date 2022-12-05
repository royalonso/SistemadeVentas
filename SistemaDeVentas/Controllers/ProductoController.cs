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
        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                bool p = new ProductoRepository().CrearProducto(producto);
                
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
            bool borrado = repository.EliminarProducto(id);
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
