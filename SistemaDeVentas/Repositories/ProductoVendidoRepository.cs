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
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;              
                var query = @"select idProducto, Descripciones as Articulo,P.Stock as StockActual, Pv.Stock as CantidadVendida , P.PrecioVenta, (P.PrecioVenta*Pv.Stock) as TotalVendido, Pv.idVenta as NrodeVenta
                from producto P inner join ProductoVendido Pv on P.Id = Pv.IdProducto ";
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
                                //productov.Id = Convert.ToInt32(dr["id"]);
                                productov.IdProducto = Convert.ToInt32(dr["idProducto"]);
                                productov.Producto = Convert.ToString(dr["Articulo"]); //nuevo
                                productov.Stock = Convert.ToInt32(dr["StockActual"]);
                                productov.CantidadVendida = Convert.ToInt32(dr["CantidadVendida"]);//nuevo
                                productov.PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]);//nuevo
                                productov.TotalVendido = Convert.ToDecimal(dr["TotalVendido"]);//nuevo
                                productov.idVenta = Convert.ToInt32(dr["NrodeVenta"]);
                                //productov.idVenta = Convert.ToInt32(dr["idVenta"]); ;
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

        public static List<ProductoVendido> DevolverProductoVendido2(int id)
        {
            
            var listaProductoV = new List<ProductoVendido>();
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"select idProducto as Codigo,Descripciones as Articulo,P.Stock as StockActual, Pv.Stock as CantidadVendida , P.PrecioVenta, 
                            (P.PrecioVenta*Pv.Stock) as TotalVendido, Pv.idVenta as NrodeVenta, P.idUsuario as Usuario
                            from producto P inner join ProductoVendido Pv on P.Id = Pv.IdProducto WHERE p.idUsuario = @id";
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var productov = new ProductoVendido();
                                productov.IdProducto = Convert.ToInt32(dr["Codigo"]);
                                productov.Producto = Convert.ToString(dr["Articulo"]);
                                productov.Stock = Convert.ToInt32(dr["StockActual"]);
                                productov.CantidadVendida = Convert.ToInt32(dr["CantidadVendida"]);
                                productov.PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]);
                                productov.TotalVendido = Convert.ToDecimal(dr["TotalVendido"]);
                                productov.idVenta = Convert.ToInt32(dr["NrodeVenta"]);
                                productov.idUsuario = Convert.ToInt32(dr["Usuario"]);
                                listaProductoV.Add(productov);

                            }
                            conecta.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return listaProductoV;
        }
        public bool CrearProductoVendido(ProductoVendido productovendido)
        {
            try
            {
                ProductoRepository haystock = new ProductoRepository();
                if (haystock.verificarydescargarStock(productovendido.IdProducto, productovendido.Stock)) // Verifico si hay Stock 
                {                                                                               // y descargo el mismo
                    VentaRepository venta1 = new VentaRepository(); //Creo la venta
                    Venta venta2 = new Venta();
                    venta2.Comentarios = $"Vendida";
                    venta2.idUsuario = 1;
                    bool p = venta1.CrearVentaPV(venta2);
                    int idVenta = venta1.DameUltimoID(); //Obtengo el id de ventas
                    ConexionDB conexion = new ConexionDB();   //Cargo el producto vendido
                    SqlConnection conecta = conexion.conexionR;
                    var query = @"INSERT INTO ProductoVendido(Stock,idProducto,Idventa) VALUES(@Stock,@IdProducto,@IdVenta)";
                    SqlCommand comando = new SqlCommand(query, conecta);
                    conecta.Open();
                    comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productovendido.Stock });
                    comando.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Float) { Value = productovendido.IdProducto });
                    comando.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = idVenta });
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
        public bool EliminarProductoVendidoporVenta(int id)
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from ProductoVendido where idVenta = @id";
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
        public int DameIDProducto(int id2)                           // Obtiene el IdproductoVendido para un idventa 
        {
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"SELECT TOP 1 idProducto from ProductoVendido WHERE idVenta = @id";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id2 });
                int id = Convert.ToInt32(comando.ExecuteScalar());
                return id;
                     

            }
                //nuevo
            catch (Exception ex)
            {
                throw;
            }

        }
        public int DameStockVendido(int id2)                           //  Actualmente no utilizo este metodo Obtiene el IdproductoVendido para un idventa 
        {
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"SELECT TOP 1 stock from ProductoVendido WHERE idVenta = @id";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id2 });
                int stock = Convert.ToInt32(comando.ExecuteScalar());
                return stock;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool ReponerStockProducto(int id, int stock) // Actualmente no utilizo este metodo
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                conecta.Open();
                var query = "UPDATE Producto SET stock = stock+@stock WHERE id = @id" ;
                SqlCommand comando = new SqlCommand(query, conecta);
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                comando.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = stock });
                filaseliminadas = comando.ExecuteNonQuery();
                conecta.Close();
                return filaseliminadas > 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool ReponerStockProducto2(int id)   // Creo un dr con los Prod. Vendidos para una venta y repongo el stock antes de borrarlos
        {
            // Nuevo
            ConexionDB conexion = new ConexionDB();
            ConexionDB conexion2 = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            SqlConnection conecta2 = conexion2.conexionR2;
            int id2 = 0;
            int idp = 0;
            int idst = 0;
            try
            {              
                conecta.Open();
                conecta2.Open(); //Genere otra conexion,ya para recorrer un dr con un sqlcommand en su interior no pueden usar la misma.Sino genera un error de conexion 
                var query2 = @"SELECT idProducto, stock from ProductoVendido WHERE idVenta = @id";
                using (SqlCommand comnd = new SqlCommand(query2, conecta))
                {
                    comnd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    using (SqlDataReader dr = comnd.ExecuteReader())
                        
                    {
                        if (dr.HasRows)
                        {
                            var query3 = "UPDATE Producto SET stock = stock+@stock WHERE id = @id";
                            while (dr.Read())                          
                            {
                                
                                idp = Convert.ToInt32(dr["idProducto"]);
                                idst = Convert.ToInt32(dr["stock"]);
                                query3 = "UPDATE Producto SET stock = stock+@stock WHERE id = @id";
                                SqlCommand comnd2 = new SqlCommand(query3, conecta2); // Recordar que para recorrer un dr debemos utilizar otra
                                                                                      // conexion.Si utilizamos un sqlcommnad en el mismo while
                                comnd2.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = idp});
                                comnd2.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = idst});
                                comnd2.ExecuteNonQuery();
                                                               
                            }
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conecta.Close();
                conecta2.Close();
            }
        }


    }
}
