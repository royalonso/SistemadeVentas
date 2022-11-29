
using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class ProductoVendido
    {

        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int idVenta { get; set; }

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
        public static List<ProductoVendido> DevolverProductoVendido()
        {
            var listaProductoV = new List<ProductoVendido>();
            try
            {
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"select id, idProducto , Stock, idVenta from ProductoVendido";
                
                //using (SqlConnection conect = new SqlConnection(conectionstring))
                //{
                    using (SqlCommand comando = new SqlCommand(query, conecta))
                    {
                        conecta.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var productov = new ProductoVendido();
                                    //producto.Id = dr.GetInt16(0);
                                    productov.Id = Convert.ToInt32(dr["id"]);
                                    productov.IdProducto = Convert.ToInt32(dr["idProducto"]);
                                    productov.Stock = Convert.ToInt32(dr["Stock"]);
                                    productov.idVenta = Convert.ToInt32(dr["idVenta"]); ;
                                    listaProductoV.Add(productov);

                                }
                                conecta.Close();
                            }
                        }
                    }

                //}
            }
            catch(Exception ex)
            {

            }

            return listaProductoV;
        }
    }
}
