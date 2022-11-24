using System.Data.SqlClient;

namespace SistemaDeVentas
{
    public class Venta
    {
        public int id { get; set; }
        public string Comentarios { get; set; }

        public Venta()
        {

        }
        public Venta(int id, string comentarios)
        {
            this.id = id;
            Comentarios = comentarios;
        }
        public static List<Venta> DevolverVenta()
        {
            var listaVenta = new List<Venta>();
            string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
            var query = @"select idVenta, Comentarios from Venta";
             using (SqlConnection conect = new SqlConnection(conectionstring))
            //using (SqlConnection conect = new SqlConnection(conectionstring))
            {
                using (SqlCommand comando = new SqlCommand(query, conect))
                {
                    conect.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var ventas = new Venta();
                                //producto.Id = dr.GetInt16(0);
                                ventas.id = Convert.ToInt32(dr["id"]);
                                ventas.Comentarios = dr["Comentarios"].ToString();
                                listaVenta.Add(ventas);

                            }
                            conect.Close();
                        }
                    }
                }

            }
            return listaVenta;
        }
    }
}
