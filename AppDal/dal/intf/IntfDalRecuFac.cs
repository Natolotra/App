using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service recu facture
    /// </summary>
    public interface IntfDalRecuFac
    {
        string insertRecuFac(crlRecuFac recuFac);
    }
}