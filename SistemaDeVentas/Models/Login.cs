namespace SistemaDeVentas
{
    public class Login
    {
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }

        public Login() 
        { 
        
        }
        public Login(string nombreUsuario, string contrasenia)
        {
            this.NombreUsuario = nombreUsuario;
            Contrasenia= contrasenia;    
        }
    }
}
