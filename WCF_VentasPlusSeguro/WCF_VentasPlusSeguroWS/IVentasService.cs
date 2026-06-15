using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model_VentasPlus;

namespace WCF_VentasPlusSeguroWS
{
    [ServiceContract]
    public interface IVentasService
    {
        [OperationContract]
        List<Producto> ObtenerProductosActivos();

        [OperationContract]
        void RegistrarVenta(Venta venta, List<DetalleVenta> detalles);
    }
}
