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
    public class VentaDAO
    {
        public void RegistrarVenta(Venta venta, List<DetalleVenta> detalles, SqlConnection cn, SqlTransaction tr)
        {
            // 1. Insertar Venta y obtener el id generado
            string sqlVenta = "INSERT INTO Venta (fecha, total, estado, idCliente) " +
                              "VALUES (@fecha, @total, @estado, @idCliente); " +
                              "SELECT SCOPE_IDENTITY();";

            SqlCommand cmdVenta = new SqlCommand(sqlVenta, cn, tr);
            cmdVenta.Parameters.AddWithValue("@fecha", venta.Fecha);
            cmdVenta.Parameters.AddWithValue("@total", venta.Total);
            cmdVenta.Parameters.AddWithValue("@estado", venta.Estado);
            cmdVenta.Parameters.AddWithValue("@idCliente", venta.IdCliente);

            int idVenta = (int)(decimal)cmdVenta.ExecuteScalar();

            // 2. Insertar cada DetalleVenta
            string sqlDetalle = "INSERT INTO DetalleVenta (cantidad, precioUnitario, subtotal, idVenta, idProducto) " +
                                "VALUES (@cantidad, @precioUnitario, @subtotal, @idVenta, @idProducto)";

            foreach (var detalle in detalles)
            {
                SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, cn, tr);
                cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                cmdDetalle.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                cmdDetalle.Parameters.AddWithValue("@subtotal", detalle.Subtotal);
                cmdDetalle.Parameters.AddWithValue("@idVenta", idVenta);
                cmdDetalle.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                cmdDetalle.ExecuteNonQuery();
            }

            // 3. Actualizar stock de cada producto
            string sqlStock = "UPDATE Producto SET stock = stock - @cantidad WHERE id = @idProducto";

            foreach (var detalle in detalles)
            {
                SqlCommand cmdStock = new SqlCommand(sqlStock, cn, tr);
                cmdStock.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                cmdStock.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                cmdStock.ExecuteNonQuery();
            }
        }

        public int ObtenerStockProducto(int idProducto, SqlConnection cn, SqlTransaction tr)
        {
            string sql = "SELECT stock FROM Producto WHERE id = @idProducto";
            SqlCommand cmd = new SqlCommand(sql, cn, tr);
            cmd.Parameters.AddWithValue("@idProducto", idProducto);
            return (int)cmd.ExecuteScalar();
        }
    }
}
