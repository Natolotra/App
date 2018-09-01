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
    /// Interface du service cheque
    /// </summary>
    public interface IntfDalCheque
    {
        crlCheque selectCheque(string numCheque);
        string insertCheque(crlCheque cheque, string sigleAgence);
        bool updateCheque(crlCheque cheque);

        string getNumCheque(string sigleAgence);
        crlCheque isChequeCredit(string numCheque);

        void insertToGridCheque(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableCheque(string strRqst);
    }
}