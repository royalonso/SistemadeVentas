using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace SistemaDeVentas
{
    
    public  class ConexionDB
    {
        public string conexion { get; set; } = "";
        public string conexion2 { get; set; } = "";
        public SqlConnection conexionR;
        public SqlConnection conexionR2;
        public SqlConnection conexionR3;
        private string cadenadeConexionR = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
        public IConfiguration _Configuration;
        public string b { get; set; }
        // En prueba seleccionar la cadena de conexion desde appconfig.json
        public ConexionDB(IConfiguration configuration)
           
        {
            _Configuration = configuration;
            //conexion = _Configuration["ConnectionString:CadenaConexionSQL"];
            //conexionR = new SqlConnection(conexion);
            var direccion = _Configuration.GetValue<string>("ConnectionString:Conexion");
            conexionR3 = new SqlConnection(direccion);      
        }
        public ConexionDB()
        {
            try
            {
                //nuevo
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
                conexion = configuration.GetValue<string>("ConnectionString:Conexion");
                //nuevo 

                //conexion = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
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
