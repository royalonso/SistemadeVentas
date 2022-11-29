using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SistemaDeVentas
{
    
    public  class ConexionDB
    {
        public string conexion { get; set; } = "";
        public SqlConnection conexionR;
        private string cadenadeConexionR = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
        public IConfiguration _Configuration;
        public ConexionDB()
        {
            try
            {
                conexion = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                //conexion = _Configuration["ConnectionString:CadenaConexionSQL"];
                conexionR = new SqlConnection(conexion);
               
            }
            catch(Exception ex)  
            {
                
            }
        }
       
        
        // En prueba seleccionar la cadena de conexion desde appconfig.json
        public ConexionDB(IConfiguration configuration)
        {
            _Configuration = configuration;
             conexion = _Configuration["ConnectionString:CadenaConexionSQL"];
             conexionR = new SqlConnection(conexion);
        }
        
        
                    

    }

}
