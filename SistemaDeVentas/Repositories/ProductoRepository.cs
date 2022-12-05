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
                //string conectionstring = @"Server=NEXTHP11\SQLEXPRESS;database=SistemaGestion;Trusted_Connection=True;";
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

                //}

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
                SqlConnection conecta = conexion.conexionR;
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
        public Producto ActualizarProducto(long id, Producto ProductoAActualizar) 
        {
            ConexionDB conexion = new ConexionDB();
            SqlConnection conecta = conexion.conexionR;
            Producto producto = obtenerProducto(ProductoAActualizar.Id);
            List<string> CamposAActualizar = new List<string>();
            if (producto.Descripcion != ProductoAActualizar.Descripcion)
            {
                CamposAActualizar.Add("descripcion = @descripcion");
            }
            if (producto.Costo != ProductoAActualizar.Costo)
            {
                CamposAActualizar.Add("Costo = @costo");
            }
            if (producto.PrecioVenta != ProductoAActualizar.PrecioVenta)
            {
                CamposAActualizar.Add("precioventa = @precioventa");
            }
            if (producto.Stock != ProductoAActualizar.Stock)
            {
                CamposAActualizar.Add("stock = @stock");
            }
            if(CamposAActualizar.Count == 0)
            {
                throw new Exception("No hay Productos para Actualizar");
            }
            conecta.Open();
            using SqlCommand comando = new SqlCommand($"UPDATE Producto SET{String.Join(" ,", CamposAActualizar)} WHERE ID = " + ProductoAActualizar.Id);
           if (comando.Parameters.Count != 0)
            {
                comando.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = ProductoAActualizar.Descripcion });
                comando.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float) { Value = ProductoAActualizar.Costo });
                comando.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float) { Value = ProductoAActualizar.PrecioVenta });
                comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = ProductoAActualizar.Stock });
                comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = ProductoAActualizar.idUsuario });
                comando.ExecuteNonQuery();
                conecta.Close();
            }
            else
            {
                throw new Exception("El id enviado no existe en la tabla Productos");
                conecta.Close();
            }            

            return ProductoAActualizar;

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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = obtenerProductoDesdeReader(reader);
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
                throw;
            }
            finally
            {
                conecta.Close();
            }
        }
        private Producto obtenerProductoDesdeReader(SqlDataReader dr)
        {

            Producto producto = new Producto();
            producto.Id = int.Parse(dr["Id"].ToString());
            producto.Descripcion = dr["Descripciones"].ToString();
            producto.Costo = decimal.Parse(dr["Costo"].ToString());
            producto.PrecioVenta = decimal.Parse(dr["PrecioVenta"].ToString());
            producto.Stock = int.Parse(dr["Stock"].ToString());
            return producto;
        }
    }
}
