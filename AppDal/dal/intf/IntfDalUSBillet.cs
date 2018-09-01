using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service billet urbaine et suburbaine
    /// </summary>
    public interface IntfDalUSBillet
    {
        string insertUSBillet(crlUSBillet billet, string sigleAgence);
        bool updateUSBillet(crlUSBillet billet);
        crlUSBillet selectUSBillet(string numBillet);
        string getNumUSBillet(string sigleAgence);

        bool insertUSAssocVoyageBillet(string numVoyage, string numBillet);
    }
}