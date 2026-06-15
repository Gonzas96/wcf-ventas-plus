using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_VentasPlus
{
    [DataContract]
    public class Producto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CodigoSKU { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public decimal Precio { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}
