using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.ServiceModel;


namespace WCF_VentasPlusSeguroWS
{
    public class Autenticar : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "adminventas" || password != "Ventas2026")
            {
                throw new FaultException("Usuario o contraseña incorrectos.");
            }
        }
    }
}
