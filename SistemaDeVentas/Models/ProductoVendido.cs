
using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class ProductoVendido
    {

        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int idVenta { get; set; }

        public string Producto { get; set; }  
        public int CantidadVendida { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal TotalVendido { get; set; }

        public int idUsuario { get; set; }
       // public Producto? producto { get; set; } //nuevo para venta
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
        public ProductoVendido(int id, int idProducto, int stock, int idVenta,string Producto,int CantidadVendida,decimal PrecioVenta, decimal TotalVendido, int idUsuario)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            this.idVenta = idVenta;
            this.Producto = Producto;
            this.CantidadVendida= CantidadVendida;  
            this.PrecioVenta= PrecioVenta;
            this.TotalVendido = TotalVendido;
            this.idUsuario = idUsuario;
        }
       
    }
}
