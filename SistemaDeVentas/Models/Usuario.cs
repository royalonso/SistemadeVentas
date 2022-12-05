using Microsoft.AspNetCore.Connections;
using System.Data.Common;
using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class Usuario 
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string mail { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string nombre, string apellido, string nombreUsuario, string contrasenia, string mail)
        {
            this.id = id;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Contraseña = contrasenia;
            this.mail = mail;
        }
       

    }
}
