namespace SistemaDeVentas
{
    public class LoginUsuario
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        public LoginUsuario() 
        { 
        
        }
        public LoginUsuario(string nombreUsuario, string contrasenia)
        {
            this.NombreUsuario = nombreUsuario;
            Contraseña= contrasenia;    
        }
    }
}
