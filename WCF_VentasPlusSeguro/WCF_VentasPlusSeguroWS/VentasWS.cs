using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL_VentasPlus;
using Model_VentasPlus;

namespace WCF_VentasPlusSeguroWS
{
    public class VentasWS : IVentasService
    {
        private ProductoBL productoBL = new ProductoBL();
        private VentaBL ventaBL = new VentaBL();

        public List<Producto> ObtenerProductosActivos()
        {
            return productoBL.ObtenerProductosActivos();
        }

        public void RegistrarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            try
            {
                ventaBL.RegistrarVenta(venta, detalles);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
