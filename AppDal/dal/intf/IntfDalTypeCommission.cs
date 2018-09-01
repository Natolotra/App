using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface pour le service type commission
    /// </summary>
    public interface IntfDalTypeCommission
    {
        void loadDddlTypeCommission(DropDownList ddl);

        crlTypeCommssion selectTypeCommission(string typeCommissionStr);
        string insertTypeCommission(crlTypeCommssion typeCommission);
        bool updateTypeCommission(crlTypeCommssion typeCommission, string typeCommissionStr);
        string isTypeCommission(crlTypeCommssion typeCommission, string typeCommissionStr);

        void insertToGridTypeCommission(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableTypeCommission(string strRqst);
    }
}