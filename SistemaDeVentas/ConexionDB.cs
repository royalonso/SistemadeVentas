using System.Data.SqlClient;
namespace SistemaDeVentas
{
    public  class ConexionDB
    {
        private SqlConnection conexion;
        private string cadenaConexion = @"Server=NEXTHP11\\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";

        public ConexionDB()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch 
            {

            }
        }
    }
}
