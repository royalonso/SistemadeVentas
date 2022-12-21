using System.Data;
using System.Data.SqlClient;
using SistemaDeVentas.Repositories;
namespace SistemaDeVentas
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Costo { get; set; } 
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; } 
        public int idUsuario { get; set; } 
        
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
