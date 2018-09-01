using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service suivi commission
    /// </summary>
    public interface IntfDalSuiviCommission
    {
        crlSuiviCommission selectSuiviCommission(string numSuiviCommission);
        string insertSuiviCommission(crlSuiviCommission suiviCommission, string sigleAgence);
        bool updateSuiviCommission(crlSuiviCommission suiviCommission);
        string getNumSuiviCommission(string sigleAgence);

    }
}