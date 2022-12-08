namespace SistemaDeVentas.Repositories
{
    public class CargarVentasRepository  // En desarrollo
    {
        public CargarVentas ActualizarVentas(CargarVentas ventasproductosvendidos)
        {
           // List<Producto> products = new List<Producto>();
           //products = ProductoRepository.DevolverProducto();
           //ventasproductosvendidos = ventaProductos()
            return ventasproductosvendidos;
        }
        public  List<CargarVentas> ventaProductos()
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
