namespace SistemaDeVentas
{
    public class CargarVentas // En desarrollo
    {
        public int IdProductoVendido { get; set; }  // id producto vendido
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int idVenta { get; set; }
        public int idUsuario { get; set; }

        public CargarVentas()
        {
        
        }
        public CargarVentas(int idProdVend,int idProd,int Stock,int idUsu, int idVenta )
        {
            IdProductoVendido= idProdVend;
            IdProducto = idProd;
            this.Stock= Stock;
            idUsuario= idUsu;
            this.idVenta= idVenta;  
        }


    }
    
}
