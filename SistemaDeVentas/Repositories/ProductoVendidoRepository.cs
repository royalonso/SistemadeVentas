using System.Data.SqlClient;

namespace SistemaDeVentas.Repositories
{
    public class ProductoVendidoRepository
    {

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

            }
            catch (Exception ex)
            {

            }

            return listaProductoV;
        }
    }
}
