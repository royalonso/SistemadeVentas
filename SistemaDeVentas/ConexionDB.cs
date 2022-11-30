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
        public string b { get; set; }
        
        public ConexionDB(IConfiguration configuration)
           
        {
            _Configuration = configuration;
            //conexion = _Configuration["ConnectionString:CadenaConexionSQL"];
            //conexionR = new SqlConnection(conexion);
        }
        public ConexionDB()
        {
            try
            {
                
                //conexion = _Configuration["ConnectionString:CadenaConexionSQL"];
                //b = _Configuration.GetValue<string>("ConnectionString");
                conexion = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                conexionR = new SqlConnection(conexion);
                
            }
            catch(Exception ex)  
            {
                
            }
        }
       
        
        // En prueba seleccionar la cadena de conexion desde appconfig.json

        
        
                    

    }

}
