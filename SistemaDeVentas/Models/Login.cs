namespace SistemaDeVentas
{
    public class Login
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        public Login() 
        { 
        
        }
        public Login(string nombreUsuario, string contrasenia)
        {
            this.NombreUsuario = nombreUsuario;
            Contraseña= contrasenia;    
        }
    }
}
