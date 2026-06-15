using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_VentasPlus
{
    [DataContract]
    public class DetalleVenta
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public decimal PrecioUnitario { get; set; }

        [DataMember]
        public decimal Subtotal { get; set; }

        [DataMember]
        public int IdVenta { get; set; }

        [DataMember]
        public int IdProducto { get; set; }
    }
}
