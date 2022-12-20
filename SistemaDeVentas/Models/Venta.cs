using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class Venta
    {
        public int id { get; set; }
        public string Comentarios { get; set; }
        public int idUsuario { get; set; }
        public List<ProductoVendido> productosvendidos { get; set; }  


        public Venta()
        {

        }
        public Venta(int id, string comentarios, int idUsuario, List<ProductoVendido> productosvendidos)
        {
            this.id = id;
            Comentarios = comentarios;
            this.idUsuario= idUsuario;
            this.productosvendidos= productosvendidos;
        }
       
    }
}
 