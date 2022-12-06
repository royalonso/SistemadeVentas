using System.Data.SqlClient;
using System.Data;
namespace SistemaDeVentas.Repositories
{
    public class LoginRepository
    {

        public LoginRepository() 
        {

        }
        public bool VerificarUsuario(LoginUsuario lusuario)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contrasenia", conecta))
                {
                    conecta.Open();
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = lusuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = lusuario.Contraseña});
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.HasRows;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conecta.Close();
            }
            
        }
        

    }
}
