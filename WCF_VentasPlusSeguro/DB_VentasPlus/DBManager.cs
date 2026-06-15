using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DB_VentasPlus
{
    public class DBManager
    {
        private static DBManager instancia;

        private DBManager() { }

        public static DBManager Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new DBManager();

                return instancia;
            }
        }

        public SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["VentaPlusDB"].ConnectionString;
            return new SqlConnection(cadena);
        }
    }
}
