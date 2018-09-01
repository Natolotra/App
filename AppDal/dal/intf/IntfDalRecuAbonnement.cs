using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service Recu abonnement
    /// </summary>
    public interface IntfDalRecuAbonnement
    {
        crlRecuAbonnement selectRecuAbonnement(string numRecuAbonnement);
        string insertRecuAbonnement(crlRecuAbonnement recuAbonnement);
        bool updateRecuAbonnement(crlRecuAbonnement recuAbonnement);

        string getNumRecuAbonnement(string sigleAgence);
    }
}