using System.Data.SqlClient;

namespace SistemaDeVentas.Repositories
{
    public class ProductoRepository
    {
        public static List<Producto> DevolverProducto()
        {
            var listaProducto = new List<Producto>();
            try
            {
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = "SELECT id,Descripciones,Costo,PrecioVenta,Stock,idUsuario from Producto";
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var producto = new Producto();
                                producto.Id = Convert.ToInt32(dr["id"]);
                                producto.Descripcion = dr["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(dr["Costo"]);
                                producto.PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dr["Stock"]);
                                producto.idUsuario = Convert.ToInt32(dr["idUsuario"]); ;
                                listaProducto.Add(producto);

                            }
                            conecta.Close();
                        }
                    }
                }

                //}

            }
            catch (Exception ex)
            {

            }

            return listaProducto;
        }
    }
}
