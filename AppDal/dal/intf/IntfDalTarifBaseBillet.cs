using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalTarifBaseBillet
    /// </summary>
    public interface IntfDalTarifBaseBillet
    {
        crlTarifBaseBillet selectTarifBaseBillet(string numTarifBaseBillet);
        string insertTarifBaseBillet(crlTarifBaseBillet tarifBaseBillet, string sigleAgence);

        bool updateTarifBaseBillet(crlTarifBaseBillet tarifBaseBillet);

        string getNumerosTarifBaseBillet(string sigleAgence);
    }
}