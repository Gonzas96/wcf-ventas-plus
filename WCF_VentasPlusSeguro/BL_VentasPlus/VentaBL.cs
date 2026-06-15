using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BL_VentasPlus;
using DAO_VentasPlus;
using DB_VentasPlus;
using Model_VentasPlus;

namespace BL_VentasPlus
{
    public class VentaBL
    {
        private VentaDAO ventaDAO = new VentaDAO();
        private ProductoDAO productoDAO = new ProductoDAO();

        public void RegistrarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            using (SqlConnection cn = DBManager.Instancia.ObtenerConexion())
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction();

                try
                {
                    // Validar stock de cada producto antes de registrar nada
                    foreach (var detalle in detalles)
                    {
                        int stockActual = ventaDAO.ObtenerStockProducto(detalle.IdProducto, cn, tr);

                        if (stockActual < detalle.Cantidad)
                        {
                            tr.Rollback();
                            throw new Exception($"Stock insuficiente para el producto con id {detalle.IdProducto}. " +
                                                $"Stock disponible: {stockActual}, cantidad solicitada: {detalle.Cantidad}.");
                        }
                    }

                    // Si todos tienen stock suficiente, registrar
                    venta.Estado = "A";
                    venta.Fecha = DateTime.Now;
                    venta.IdCliente = 1; // Cliente genérico

                    ventaDAO.RegistrarVenta(venta, detalles, cn, tr);

                    tr.Commit();
                }
                catch (Exception)
                {
                    tr.Rollback();
                    throw;
                }
            }
        }
    }
}
