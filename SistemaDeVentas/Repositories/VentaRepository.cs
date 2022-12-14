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
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"select id, Comentarios, idUsuario from Venta";
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
        public bool CrearVenta(Venta venta)  //Creo una venta para una lista de productos vendidos.
        {

            try
            {
                //Verifico si hay Sock de los productos para la venta
                ProductoRepository producto = new ProductoRepository();
                foreach (var item in venta.productosvendidos)
                {
                    if (producto.hayStock(item.IdProducto, item.CantidadVendida) == false)
                    {
                        throw new Exception("El Producto no Existe o no hay Stock Suficiente");
                    }
                }
                //Genero la venta
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"INSERT INTO Venta(Comentarios,idUsuario) VALUES(@Comentarios,@idUsuario)";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                comando.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = venta.idUsuario});
                comando.ExecuteNonQuery();
                conecta.Close();

                var query1 = @"INSERT INTO ProductoVendido(Stock,idProducto,Idventa) VALUES(@Stock,@IdProducto,@IdVenta)";
                SqlCommand comando1 = new SqlCommand(query1, conecta);
                conecta.Open();              
                foreach(var item in venta.productosvendidos) // Cargo los productos vendidos
                {
                    comando1.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = item.CantidadVendida });
                    comando1.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Int) { Value = item.IdProducto});
                    comando1.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = DameUltimoID() });
                    comando1.ExecuteNonQuery();
                    producto.descargarStock(item.IdProducto, item.CantidadVendida); // descargo del stock los productos vendidos
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
        public bool CrearVentaPV(Venta venta)  // Creo una venta para una producto vendido unitario
        {

            try
            {
                //Verifico si hay Sock de los productos para la venta
                //ProductoRepository producto = new ProductoRepository();
                //Genero la venta
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"INSERT INTO Venta(Comentarios,idUsuario) VALUES(@Comentarios,@idUsuario)";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                comando.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = venta.idUsuario });
                comando.ExecuteNonQuery();
                conecta.Close();
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
        public  List<Venta> DevolverVenta2(int? id)
        {

            var listaVenta = new List<Venta>();
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                string query = @"SELECT A.Id, A.Comentarios, A.IdUsuario, B.Id AS IdProductoVendido, B.IdProducto,C.Descripciones, B.IdVenta, B.Stock, C.Descripciones, C.PrecioVenta 
                                FROM Venta AS A 
                                INNER JOIN ProductoVendido AS B 
                                ON A.Id = B.IdVenta 
                                INNER JOIN Producto AS C 
                                ON B.IdProducto = C.Id";
                if (id != null)
                {
                    query += " WHERE A.Id = @id";
                }
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    if (id != null)
                    {
                        comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    }                     
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var venta= new Venta();
                                venta.id = Convert.ToInt32(dr["IdVenta"]);
                                venta.Comentarios = Convert.ToString(dr["Comentarios"]);
                                venta.idUsuario = Convert.ToInt32(dr["Id"]);
                                venta.idproductovendido = Convert.ToInt32(dr["IdProductoVendido"]);
                                venta.idproducto = Convert.ToInt32(dr["IdProducto"]);
                                venta.descripciones = Convert.ToString(dr["Descripciones"]);
                                venta.cantidadvendida = Convert.ToInt32(dr["Stock"]);
                                venta.precioventa = Convert.ToDouble(dr["PrecioVenta"]);
                                listaVenta.Add(venta);
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

            return listaVenta;
        }
        /*
        public List<Venta> obtenerVenta2(int? id)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            List<Venta> lista = new List<Venta>();
            try
            {
                string query = "SELECT A.Id, A.Comentarios, A.IdUsuario, B.Id AS IdProductoVendido, B.IdProducto, B.IdVenta, B.Stock, C.Descripciones, C.PrecioVenta " +
                    "FROM Venta AS A " +
                    "INNER JOIN ProductoVendido AS B " +
                    "ON A.Id = B.IdVenta " +
                    "INNER JOIN Producto AS C " +
                    "ON B.IdProducto = C.Id";
                if (id != null)
                {
                    query += " WHERE A.Id = @id";
                }
                using (SqlCommand cmd = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    if (id != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int ultimoIdVenta = 0;
                            Venta venta = new Venta();
                            while (reader.Read())
                            {
                                int IdVenta = int.Parse(reader["Id"].ToString());
                                if (IdVenta == ultimoIdVenta)
                                {
                                    ProductoVendido productoVendido = new ProductoVendido()
                                    {
                                        Id = int.Parse(reader["IdProductoVendido"].ToString()),
                                        IdProducto = int.Parse(reader["IdProducto"].ToString()),
                                        Stock = int.Parse(reader["Stock"].ToString()),
                                        producto = new Producto()
                                        {
                                            Descripcion = reader["Descripciones"].ToString(),
                                            PrecioVenta = decimal.Parse(reader["PrecioVenta"].ToString())
                                        }
                                    };
                                    if (venta.productosvendidos != null)
                                    {
                                        venta.productosvendidos.Add(productoVendido);
                                    }
                                }
                                else
                                {
                                    if (ultimoIdVenta != 0)
                                    {
                                        lista.Add(venta);
                                    }
                                    venta = new Venta()
                                    {
                                        id = IdVenta,
                                        Comentarios = reader["Comentarios"].ToString(),
                                        idUsuario = int.Parse(reader["idUsuario"].ToString()),
                                        productosvendidos = new List<ProductoVendido>(),
                                    };
                                    ultimoIdVenta = IdVenta;
                                }
                            }
                            lista.Add(venta);
                        }
                    }
                }
                return lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                conecta.Close();
            }
        }
        */
    }
}
