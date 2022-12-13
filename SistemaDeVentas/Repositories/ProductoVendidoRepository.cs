﻿using System.Data;
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
                //var query = @"select id, idProducto , Stock, idVenta from ProductoVendido";
                var query = @"select Descripciones as Articulo, Pv.Stock as CantidadVendida , P.PrecioVenta, (P.PrecioVenta*Pv.Stock) as TotalVendido
                              from producto P inner join ProductoVendido Pv on P.Id = Pv.IdProducto";
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
        public static List<ProductoVendido> DevolverProductoVendido2()
        {
            var listaProductoV = new List<ProductoVendido>();
            try
            {
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                //var query = @"select id, idProducto , Stock, idVenta from ProductoVendido";
                var query = @"select idProducto as Codigo,Descripciones as Articulo,P.Stock as StockActual, Pv.Stock as CantidadVendida , P.PrecioVenta, (P.PrecioVenta*Pv.Stock) as TotalVendido, Pv.idVenta as NrodeVenta
                              from producto P inner join ProductoVendido Pv on P.Id = Pv.IdProducto";
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
                                productov.IdProducto = Convert.ToInt32(dr["Codigo"]);
                                productov.Producto = Convert.ToString(dr["Articulo"]);
                                productov.Stock = Convert.ToInt32(dr["StockActual"]);
                                productov.CantidadVendida = Convert.ToInt32(dr["CantidadVendida"]);
                                productov.PrecioVenta = Convert.ToInt32(dr["PrecioVenta"]);
                                productov.TotalVendido = Convert.ToInt32(dr["TotalVendido"]);
                                productov.idVenta = Convert.ToInt32(dr["NrodeVenta"]);
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
            catch (Exception ex)
            {
                throw;
            }

        }
        public int DameStockVendido(int id2)                           // Obtiene el IdproductoVendido para un idventa 
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
        public bool ReponerStockProducto(int id, int stock)
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
       
    }
}
