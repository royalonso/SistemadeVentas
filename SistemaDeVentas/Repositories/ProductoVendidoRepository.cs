using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVentas.Repositories
{
    public class ProductoVendidoRepository:ProductoVendido
    {

        public static List<ProductoVendido> DevolverProductoVendido()
        {
            var listaProductoV = new List<ProductoVendido>();
            try
            {
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"select id, idProducto , Stock, idVenta from ProductoVendido";

                //using (SqlConnection conect = new SqlConnection(conectionstring))
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
                                var productov = new ProductoVendido();
                                //producto.Id = dr.GetInt16(0);
                                productov.Id = Convert.ToInt32(dr["id"]);
                                productov.IdProducto = Convert.ToInt32(dr["idProducto"]);
                                productov.Stock = Convert.ToInt32(dr["Stock"]);
                                productov.idVenta = Convert.ToInt32(dr["idVenta"]); ;
                                listaProductoV.Add(productov);

                            }
                            conecta.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return listaProductoV;
        }
        public bool CrearProductoVendido(ProductoVendido productovendido)
        {
            try
            {
                ProductoRepository haystock = new ProductoRepository();
                if (haystock.verificarStock(productovendido.IdProducto, productovendido.Stock)) // Verifico si hay Stock y descargo el mismo
                {
                    VentaRepository venta1 = new VentaRepository(); //Creo la venta
                    Venta venta2 = new Venta();
                    venta2.Comentarios = $"Vendida";
                    bool p = venta1.CrearVenta(venta2);

                    int idVenta = venta1.DameUltimoID(); //Obtengo el id de ventas

                    ConexionDB conexion = new ConexionDB();   //Cargo el producto vendido
                    SqlConnection conecta = conexion.conexionR;
                    var query = @"INSERT INTO ProductoVendido(Stock,idProducto,Idventa) VALUES(@Stock,@IdProducto,@IdVenta)";
                    SqlCommand comando = new SqlCommand(query, conecta);
                    conecta.Open();
                    comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productovendido.Stock });
                    comando.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Float) { Value = productovendido.IdProducto });
                    comando.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = idVenta });
                    //comando.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = productovendido.idVenta });
                    comando.ExecuteNonQuery();
                    conecta.Close();
                    return true;

                }
                else
                {
                    throw new Exception("No hay stock o el producto no existe, Verifique");
                }


            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo Insertar el ProductoVendido");
            }
        }

        public bool EliminarProductoVendido(int id)
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from ProductoVendido where id = @id";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                filaseliminadas = comando.ExecuteNonQuery();
                conecta.Close();
                return filaseliminadas > 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
