using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service Info prix commission
    /// </summary>
    public interface IntfDalInfoPrixCommission
    {
        string insertInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission, string sigleAgence);
        bool deleteInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission);
        bool deleteInfoPrixCommission(string numInfoPrixCommission);
        bool updateInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission);

        int isInfoPrixCommissionInt(crlInfoPrixCommission InfoPrixCommission);
        string isInfoPrixCommission(crlInfoPrixCommission InfoPrixCommission);
        crlInfoPrixCommission selectInfoPrixCommission(string numInfoPrixCommission);
        crlInfoPrixCommission selectInfoPrixCommissionPaiement(string paiement);
        List<crlInfoPrixCommission> selectInfoPrixCommissions(string idItineraire);
        string getNumInfoPrixCommission(string sigleAgence);
        //void loadDdlTri(DropDownList ddlTri);
    }
}