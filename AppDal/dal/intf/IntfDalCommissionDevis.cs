using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalCommissionDevis
    /// </summary>
    public interface IntfDalCommissionDevis
    {
        string insertCommissionDevis(crlCommissionDevis CommissionDevis, string sigleAgence);
        bool updateCommissionDevis(crlCommissionDevis CommissionDevis);

        crlCommissionDevis selectCommissionDevis(string idCommissionDevis);
        string getidCommissionDevis(string sigleAgence);

        crlCommission getCommission(crlCommissionDevis CommissionDevis, crlAgent agent);

        void insertToGridCommissionDevis(GridView gridView, string param, string paramLike, string valueLike, string numProforma);
        DataTable getDataTableCommissionDevis(string strRqst);
    }
}