using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_VentasPlus
{
    [DataContract]
    public class Venta
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public int IdCliente { get; set; }
    }
}
