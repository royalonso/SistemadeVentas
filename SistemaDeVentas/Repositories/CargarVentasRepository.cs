using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SistemaDeVentas.Repositories
{
    public class CargarVentasRepository  // En desarrollo
    {
        public CargarVentas ActualizarVentas(CargarVentas ventasproductosvendidos)
        {
            List<CargarVentas> venta1 = new List<CargarVentas>();
            venta1 = ventaProductos();
            ventasproductosvendidos.IdProducto = venta1.Count;
            return ventasproductosvendidos;
            foreach(CargarVentas venta in  venta1)
            {
                
            }
           
        }
        public  static List<CargarVentas> ventaProductos()
        {
            var listaDeVentas = new List<CargarVentas>();
            var cargaventas = new CargarVentas();
            cargaventas.IdProducto = 1;
            cargaventas.idUsuario = 2;
            cargaventas.Stock = 3;
            listaDeVentas.Add(cargaventas);
            return listaDeVentas;
        }


    }
}
