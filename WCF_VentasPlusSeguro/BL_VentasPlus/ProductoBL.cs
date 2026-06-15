using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_VentasPlus;
using Model_VentasPlus;

namespace BL_VentasPlus
{
    public class ProductoBL
    {
        private ProductoDAO productoDAO = new ProductoDAO();

        public List<Producto> ObtenerProductosActivos()
        {
            return productoDAO.ObtenerProductosActivos();
        }
    }
}
