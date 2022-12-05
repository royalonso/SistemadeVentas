using System.Data;
using System.Data.SqlClient;
using SistemaDeVentas.Repositories;
namespace SistemaDeVentas
{
    public class Producto
    {
        public int Id { get; set; } = 0;
        public string Descripcion { get; set; } = "";
        public decimal Costo { get; set; } = decimal.Zero;
        public decimal PrecioVenta { get; set; } = decimal.Zero;
        public int Stock { get; set; } = int.MaxValue;
        public int idUsuario { get; set; } = 0;

        public Producto()
        {

        }
        public Producto(int id, string descripcion, decimal costo, decimal precioVenta, int stock, int idUsuario)
        {
            Id = id;
            Descripcion = descripcion;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            this.idUsuario = idUsuario;
        }

     }
}
