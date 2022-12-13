
using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class ProductoVendido
    {
        //"select Descripciones as Articulo, Pv.Stock as CantidadVendida , P.PrecioVenta, (P.PrecioVenta*Pv.Stock) as TotalVendido
        //from producto P inner join ProductoVendido Pv on P.Id = Pv.IdProducto";
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int idVenta { get; set; }

        public string Producto { get; set; }  
        public decimal CantidadVendida { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal TotalVendido { get; set; }
        



        public ProductoVendido()
        {

        }
        public ProductoVendido(int id, int idProducto, int stock, int idVenta)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            this.idVenta = idVenta;
        }
        public ProductoVendido(int id, int idProducto, int stock, int idVenta,string Producto,decimal CantidadVendida,decimal PrecioVenta, decimal TotalVendido)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            this.idVenta = idVenta;
            this.Producto = Producto;
            this.CantidadVendida= CantidadVendida;  
            this.PrecioVenta= PrecioVenta;
            this.TotalVendido = TotalVendido;
        }
       
    }
}
