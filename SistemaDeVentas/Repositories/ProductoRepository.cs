using System.Data;
using System;
using System.Data.SqlClient;

namespace SistemaDeVentas.Repositories
{
    public class ProductoRepository:Producto
    {

        public static List<Producto> DevolverProducto()
        {
            var listaProducto = new List<Producto>();
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = "SELECT id,Descripciones,Costo,PrecioVenta,Stock,idUsuario from Producto";
                using (SqlCommand comando = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var producto = new Producto();
                                producto.Id = Convert.ToInt32(dr["id"]);
                                producto.Descripcion = dr["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(dr["Costo"]);
                                producto.PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dr["Stock"]);
                                producto.idUsuario = Convert.ToInt32(dr["idUsuario"]); ;
                                listaProducto.Add(producto);

                            }
                            conecta.Close();
                        }
                    }
                }

                

            }
            catch (Exception ex)
            {

            }

            return listaProducto;
        }
        public bool EliminarProducto(int id)
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from producto where id = @id";
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                filaseliminadas = comando.ExecuteNonQuery();
                conecta.Close();
                return filaseliminadas >0;
            }
            catch (Exception ex)
            {
                throw;
            }
                     
        }
        public bool EliminarProductoVendido(int id)
        {
            try
            {
                int filaseliminadas = 0;
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR;
                var query = @"DELETE from ProductoVendido where idProducto = @id";
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
        public bool CrearProducto(Producto producto)
        {
            try
            {
                ConexionDB conexion = new ConexionDB();
                SqlConnection conecta = conexion.conexionR3;
                var query = @"INSERT INTO Producto(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES(@Descripcion,@Costo,@PrecioVenta,@Stock,@idUsuario)"; 
                SqlCommand comando = new SqlCommand(query, conecta);
                conecta.Open();
                comando.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                comando.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float) { Value = producto.Costo });
                comando.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float) { Value = producto.PrecioVenta });
                comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock });
                comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.idUsuario });
                comando.ExecuteNonQuery();  
                conecta.Close();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("No se pudo Insertar el Producto");
            }
        }
        public Producto ActualizarProducto(int id, Producto ProductoAActualizar) 
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            ProductoAActualizar.Id = id;
            Producto producto = obtenerProducto(ProductoAActualizar.Id);
            try
            {
                List<string> CamposAActualizar = new List<string>();
                if (producto.Descripcion != ProductoAActualizar.Descripcion && !string.IsNullOrEmpty(ProductoAActualizar.Descripcion)) ;
                {
                    CamposAActualizar.Add("Descripciones = @descripcion");
                    producto.Descripcion = ProductoAActualizar.Descripcion;
                }
                if (producto.Costo != ProductoAActualizar.Costo && ProductoAActualizar.Costo > 0)
                {
                    CamposAActualizar.Add("Costo = @costo");
                    producto.Costo = ProductoAActualizar.Costo;
                }
                if (producto.PrecioVenta != ProductoAActualizar.PrecioVenta && ProductoAActualizar.PrecioVenta > 0)
                {
                    CamposAActualizar.Add("precioventa = @precioventa");
                    producto.PrecioVenta = ProductoAActualizar.PrecioVenta;
                }
                if (producto.Stock != ProductoAActualizar.Stock && ProductoAActualizar.Stock > 0)
                {
                    CamposAActualizar.Add("stock = @stock");
                    producto.Stock = ProductoAActualizar.Stock;
                }
                if (producto.idUsuario != ProductoAActualizar.idUsuario && ProductoAActualizar.idUsuario > 0)
                {
                    CamposAActualizar.Add("idusuario = @idusuario");
                    producto.Stock = ProductoAActualizar.Stock;
                }
                if (CamposAActualizar.Count == 0)
                {
                    throw new Exception("No hay Productos para Actualizar");
                }
                conecta.Open();
                var query = $"UPDATE Producto SET {String.Join(" ,", CamposAActualizar)} WHERE id = @id";
                using SqlCommand comando = new SqlCommand(query, conecta);
                comando.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = ProductoAActualizar.Descripcion });
                comando.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float) { Value = ProductoAActualizar.Costo });
                comando.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float) { Value = ProductoAActualizar.PrecioVenta });
                comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = ProductoAActualizar.Stock });
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = ProductoAActualizar.Id });
                comando.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = ProductoAActualizar.idUsuario });
                comando.ExecuteNonQuery();
                //conecta.Close();

                return producto;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al Actualizar");
            }


        }

        public Producto? obtenerProducto(int id)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM producto WHERE id = @id", conecta))
                {
                    conecta.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Producto producto = obtenerProductoDesdeReader(dr);
                            return producto;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                return null;
                throw new Exception("No se pudo Obtener el Producto");
            }
            finally
            {
                conecta.Close();
            }

        }
        public bool verificarydescargarStock(int id, int stock)  //Verifica si hay stock para vender un producto determinado y lo descarga
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            var  query = "SELECT * FROM producto WHERE id = @id AND Stock >= @stock";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.BigInt) { Value = stock });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Close();
                            query = "UPDATE Producto SET stock = stock-@stock WHERE id = @id ";     // Descargo del stock la cantidad vendida
                            SqlCommand comando = new SqlCommand(query, conecta);
                            comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                            comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = stock });
                            comando.ExecuteNonQuery();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
                throw;
            }
            finally
            {
                conecta.Close();
            }

        }
        public bool hayStock(int id, int stock)
        {
            int id2 = 0;
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            var query = "SELECT id FROM producto WHERE id = @id AND Stock >= @stock";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conecta))
                {
                    conecta.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.BigInt) { Value = stock });
                    id2 = Convert.ToInt32(cmd.ExecuteScalar());
                    //conecta.Close();
                    if (id2 > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
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
        public bool descargarStock(int id, int stock)
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            try
            {
                conecta.Open();
                var  query = "UPDATE Producto SET stock = stock-@stock WHERE id = @id ";     // Descargo del stock la cantidad vendida
                SqlCommand comando = new SqlCommand(query, conecta);
                comando.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = stock });
                comando.ExecuteNonQuery();
                //conecta.Close();
                return true;

            }
            catch 
            {
             return false; 
            }
            finally
            {
                conecta.Close();
            }

        }

        private Producto obtenerProductoDesdeReader(SqlDataReader dr)
        {
            Producto producto = new Producto();
            try
            {
                
                producto.Id = int.Parse(dr["Id"].ToString());
                producto.Descripcion = dr["Descripciones"].ToString();
                producto.Costo = decimal.Parse(dr["Costo"].ToString());
                producto.PrecioVenta = decimal.Parse(dr["PrecioVenta"].ToString());
                producto.Stock = int.Parse(dr["Stock"].ToString());
                return producto;
            }
            catch
            {
                return producto;
            }

        }
    }
}
