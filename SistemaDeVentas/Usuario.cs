using Microsoft.AspNetCore.Connections;
using System.Data.Common;
using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class Usuario :ConexionDB
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
        //public static List<Usuario> DevolverUsuarios()
        public static List<Usuario> DevolverUsuarios()
        {
            var listaUsuario = new List<Usuario>();
            try
            {

                //string cadena = conexion.conexion;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                var query = "SELECT ID,Nombre,Apellido,NombreUsuario,Contraseña,Mail from Usuario";
               // using (SqlConnection conect = new SqlConnection(cadena))
                //{
                    using (SqlCommand comando = new SqlCommand(query, conecta))
                    {
                        conecta.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())

                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var usuario = new Usuario();
                                    usuario.id = Convert.ToInt32(dr["id"]);
                                    usuario.Nombre = dr["Nombre"].ToString();
                                    usuario.Apellido = dr["Apellido"].ToString();
                                    usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                    usuario.Contraseña = dr["Contraseña"].ToString();
                                    usuario.mail = dr["Mail"].ToString();
                                    listaUsuario.Add(usuario);
                                }
                                conecta.Close();
                            }
                        }
                    //}

                }
                

            }
            catch (Exception ex) 
            {
                
            }
            return listaUsuario;


        }


    }
}
