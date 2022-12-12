using System.Data;
using System.Data.SqlClient;
using SistemaDeVentas;
namespace SistemaDeVentas.Repositories
{
    public class VentaRepository
    {

       
        public static List<Venta> DevolverVenta()
        {
            var listaVenta = new List<Venta>();
            try
            {
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"select id, Comentarios from Venta";
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var ventas = new Venta();
                                ventas.id = Convert.ToInt32(dr["id"]);
                                ventas.Comentarios = dr["Comentarios"].ToString();
                                listaVenta.Add(ventas);

                            }
                            conecta.Close();
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {

            }

            return listaVenta;
        }
        public bool CrearVenta(Venta venta)
        {
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"INSERT INTO Venta(Comentarios) VALUES(@Comentarios)";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                //comando.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Float) { Value = productovendido.IdProducto });
                comando.ExecuteNonQuery();
                conecta.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo Insertar la Venta");
            }
        }
        public bool EliminarVenta(int id)
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from Venta where id = @id";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                filaseliminadas = comando.ExecuteNonQuery();
                conecta.Close();
                return filaseliminadas > 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public int  DameUltimoID()
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            var query = @"SELECT TOP 1 id from Venta ORDER BY id DESC";
            SqlCommand comando = new SqlCommand(query, conecta);
            conecta.Open();
            int id = Convert.ToInt32(comando.ExecuteScalar());
            return id;
        }
    }
}
