using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SistemaDeVentas
{
    
    public  class ConexionDB
    {
        public string conexion { get; set; } = "";
        public string conexion2 { get; set; } = "";
        public SqlConnection conexionR;
        public SqlConnection conexionR2;
        private string cadenadeConexionR = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
        public IConfiguration _Configuration;
        public string b { get; set; }
        // En prueba seleccionar la cadena de conexion desde appconfig.json
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
                conexion2 = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                conexionR2 = new SqlConnection(conexion);

            }
            catch(Exception ex)  
            {
                throw new Exception("No se pudo establecer la conexion al Servidor");
            }
        }
       
        
        

        
        
                    

    }

}
