using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVentas.Repositories
{
    public class UsuarioRepository
    {

        public static List<Usuario> DevolverUsuarios()
        {
            var listaUsuario = new List<Usuario>();
            try
            {

                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;            
                var query = "SELECT ID,Nombre,Apellido,NombreUsuario,Contraseña,Mail from Usuario";
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())

                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var usuario = new Usuario();
                                usuario.id = Convert.ToInt32(dr["id"]);
                                usuario.Nombre = dr["Nombre"].ToString();
                                usuario.Apellido = dr["Apellido"].ToString();
                                usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                usuario.Contraseña = dr["Contraseña"].ToString();
                                usuario.mail = dr["Mail"].ToString();
                                listaUsuario.Add(usuario);
                            }
                            conecta.Close();
                        }
                    }
                    

                }


            }
            catch (Exception ex)
            {

            }
            return listaUsuario;


        }
        public bool EliminarUsuario(int id)
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from Usuario where id = @id";
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

    }
}
