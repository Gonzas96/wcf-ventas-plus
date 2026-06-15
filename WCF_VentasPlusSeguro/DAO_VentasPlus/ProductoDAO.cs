using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DB_VentasPlus;
using Model_VentasPlus;

namespace DAO_VentasPlus
{
    public class ProductoDAO
    {
        public List<Producto> ObtenerProductosActivos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = DBManager.Instancia.ObtenerConexion())
            {
                string sql = "SELECT id, codigoSKU, nombre, descripcion, stock, precio, estado " +
                             "FROM Producto WHERE estado = 'A'";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Producto
                    {
                        Id = (int)dr["id"],
                        CodigoSKU = dr["codigoSKU"].ToString(),
                        Nombre = dr["nombre"].ToString(),
                        Descripcion = dr["descripcion"].ToString(),
                        Stock = (int)dr["stock"],
                        Precio = (decimal)dr["precio"],
                        Estado = dr["estado"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
