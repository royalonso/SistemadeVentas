using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class Venta
    {
        public int id { get; set; }
        public string Comentarios { get; set; }
        public int idUsuario { get; set; }
        public List<ProductoVendido> productosvendidos { get; set; }  

        public int idproductovendido { get; set; }
        public int idproducto { get; set; }
        public int cantidadvendida { get; set; }
        public double precioventa { get; set; }
        public string descripciones { get; set; }
        public Venta()
        {
    //        productosvendidos = new List<ProductoVendido>();
        }
        public Venta(int id, string comentarios, int idUsuario, int idproductovendido,int idproducto,int cantidadvendida, double precioventa,string descripciones, List<ProductoVendido> productosvendidos)
        {
            this.id = id;
            Comentarios = comentarios;
            this.idUsuario= idUsuario;
            this.idproductovendido = idproductovendido;
            this.idproducto = idproducto;
            this.productosvendidos= productosvendidos;
            this.cantidadvendida=cantidadvendida;
            this.precioventa= precioventa;
            this.descripciones= descripciones;
            
        }
       
    }
}
 