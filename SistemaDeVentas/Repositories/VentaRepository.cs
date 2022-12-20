using System.Data;
using System.Data.SqlClient;
using SistemaDeVentas;
namespace SistemaDeVentas.Repositories
{
    public class VentaRepository
    {     
        public static List<Venta> DevolverVenta()
        {
            var listaVenta = new List<Venta>();
            try
            {
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"select id, Comentarios, idUsuarios from Venta";
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var ventas = new Venta();
                                ventas.id = Convert.ToInt32(dr["id"]);
                                ventas.Comentarios = dr["Comentarios"].ToString();
                                ventas.idUsuario = Convert.ToInt32(dr["idUsuario"]); ;
                                listaVenta.Add(ventas);
                                
                            }
                            conecta.Close();
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {

            }

            return listaVenta;
        }
        public bool CrearVenta(Venta venta)
        {

            try
            {
                //Nuevo Verifico si hay Sock de los productos
                ProductoRepository producto = new ProductoRepository();
                foreach (var item in venta.productosvendidos)
                {
                    if (producto.hayStock(item.IdProducto, item.CantidadVendida) == false)
                    {
                        throw new Exception("El Producto no Existe o no hay Stock Suficiente");
                    }
                }
                //Nuevo
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"INSERT INTO Venta(Comentarios,idUsuario) VALUES(@Comentarios,@idUsuario)";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                comando.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = venta.idUsuario});
                comando.ExecuteNonQuery();
                conecta.Close();
                //Nuevo
                //ProductoVendidoRepository prodven = new ProductoVendidoRepository();
                var query1 = @"INSERT INTO ProductoVendido(Stock,idProducto,Idventa) VALUES(@Stock,@IdProducto,@IdVenta)";
                SqlCommand comando1 = new SqlCommand(query1, conecta);
                conecta.Open();              
                foreach(var item in venta.productosvendidos)
                {
                    comando1.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = item.CantidadVendida });
                    comando1.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Int) { Value = item.IdProducto});
                    comando1.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = DameUltimoID() });
                    comando1.ExecuteNonQuery();
                    producto.descargarStock(item.IdProducto, item.CantidadVendida);
                }
                conecta.Close();
                //Nuevo
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo Insertar la Venta");
            }
        }
        public bool EliminarVenta(int id)
        {
            try
            {
                //Nuevo
                ProductoVendidoRepository prodven = new ProductoVendidoRepository();
                /*
                int idpv = prodven.DameIDProducto(id);        //Obtengo el id de producto de una venta
                int stockv = prodven.DameStockVendido(id);    //Obtengo el stock de producto vendido para esa venta
                prodven.ReponerStockProducto(idpv, stockv);   //Repongo el Stock Vendido a la tabla producto

                //Resumo los 3 metodos anteriores en uno solo (ReponerStockProducto2) ya que eran para ventas y productos vendidos en forma unitaria
                */
                prodven.ReponerStockProducto2(id);              //Repongo el stock del producto vendido para una venta con un datareader.
                                                                //Ya que puede haber mas de un producto vendido asociado a una venta. 
                prodven.EliminarProductoVendidoporVenta(id);    //Elimino el producto vendido
                
                // Elimino la venta
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from Venta where id = @id";
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
        public int  DameUltimoID()  
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            var query = @"SELECT TOP 1 id from Venta ORDER BY id DESC";
            SqlCommand comando = new SqlCommand(query, conecta);
            conecta.Open();
            int id = Convert.ToInt32(comando.ExecuteScalar());
            return id;
        }
    }
}
