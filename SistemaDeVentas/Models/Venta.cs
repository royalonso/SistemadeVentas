using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class Venta
    {
        public int id { get; set; }
        public string Comentarios { get; set; }

        //public virtual ICollection<ProductoVendido> ProductoVendido { get; set; };
        public Venta()
        {

        }
        public Venta(int id, string comentarios)
        {
            this.id = id;
            Comentarios = comentarios;
        }
       
    }
}
