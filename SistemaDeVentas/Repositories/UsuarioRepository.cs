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
        // HttpDelete
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
        // HttpPUT
        public Usuario ActualizarUsuario(int id, Usuario usuarioAActualizar)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            usuarioAActualizar.id = id;
            Usuario usuario = obtenerUsuario(usuarioAActualizar.id);
            List<string> CamposAActualizar = new List<string>();
            if (usuario.Apellido != usuarioAActualizar.Apellido && !string.IsNullOrEmpty(usuarioAActualizar.Apellido))
            {
                CamposAActualizar.Add("Apellido = @apellido");
                usuario.Apellido = usuarioAActualizar.Apellido;
            }
            if (usuario.Nombre != usuario.Nombre && !string.IsNullOrEmpty(usuarioAActualizar.Nombre))
            {
                CamposAActualizar.Add("Nombre = @nombre");
                usuario.Nombre = usuarioAActualizar.Nombre;
            }
            if (usuario.NombreUsuario != usuarioAActualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioAActualizar.NombreUsuario))
            {
                CamposAActualizar.Add("NombreUsuario = @nombreusuario");
                usuario.NombreUsuario    = usuarioAActualizar.NombreUsuario;
            }
            if (usuario.Contraseña != usuarioAActualizar.Contraseña && !string.IsNullOrEmpty(usuarioAActualizar.Contraseña))
            {
                CamposAActualizar.Add("Contraseña= @contraseña");
                usuario.Contraseña = usuarioAActualizar.Contraseña;
            }
            if (usuario.mail != usuarioAActualizar.mail && !string.IsNullOrEmpty(usuarioAActualizar.mail))
            {
                CamposAActualizar.Add("Mail= @mail");
                usuario.mail = usuarioAActualizar.mail;
            }
            if (CamposAActualizar.Count == 0)
            {
                throw new Exception("No hay Productos para Actualizar");
            }
            conecta.Open();
            var query = $"UPDATE Usuario SET {String.Join(" ,", CamposAActualizar)} WHERE id = @id";
            //using SqlCommand comando = new SqlCommand($"UPDATE Producto SET{String.Join(" ,", CamposAActualizar)} WHERE ID = @id",conecta);
            using SqlCommand comando = new SqlCommand(query, conecta);

            comando.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuarioAActualizar.Apellido });
            comando.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuarioAActualizar.Nombre });
            comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuarioAActualizar.NombreUsuario });
            comando.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuarioAActualizar.Contraseña });
            comando.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuarioAActualizar.mail });
            comando.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = usuarioAActualizar.id });
            comando.ExecuteNonQuery();
            conecta.Close();


            return usuario;

        }
        public bool CrearUsuario(Usuario usuario)
        {
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"INSERT INTO Usuario(Nombre,Apellido,NombreUsuario,Contraseña,Mail) VALUES(@Nombre,@Apellido,@NombreUsuario,@Contraseña,@Mail)";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                comando.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario});
                comando.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                comando.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.mail });
                comando.ExecuteNonQuery();
                conecta.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo crear el Usuario");
            }
        }

        public Usuario? obtenerUsuario(int id)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE id = @id", conecta))
                {
                    conecta.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Usuario usuario = obtenerUsuariodr(dr);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                return null;
                throw;
            }
            finally
            {
                conecta.Close();
            }
        }
        public Usuario? obtenerUsuariopornombre(string nombreusuario)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE NombreUsuario = @nombreusuario", conecta))
                {
                    conecta.Open();
                    cmd.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = nombreusuario });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Usuario usuario = obtenerUsuariodr(dr);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                return null;
                throw;
            }
            finally
            {
                conecta.Close();
            }
        }
        private Usuario obtenerUsuariodr(SqlDataReader dr)
        {

            Usuario usuario = new Usuario();
            usuario.id = int.Parse(dr["Id"].ToString());
            usuario.Apellido= dr["Apellido"].ToString();
            usuario.Nombre = dr["Nombre"].ToString();
            usuario.NombreUsuario = dr["NombreUsuario"].ToString();
            usuario.mail = dr["mail"].ToString();
            usuario.Contraseña = "********"; //dr["contraseña"].ToString();
            return usuario;
        }

    }
}
