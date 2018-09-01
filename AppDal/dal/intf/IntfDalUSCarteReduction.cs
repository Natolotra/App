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
    /// Interface du service carte de reduction
    /// </summary>
    public interface IntfDalUSCarteReduction
    {
        string insertUSCarteReduction(crlUSCarteReduction carteReduction, string sigleAgence);
        bool updateUSCarteReduction(crlUSCarteReduction carteReduction);
        string isUSCarteReduction(crlUSCarteReduction carteReduction);
        crlUSCarteReduction selectUSCarteReduction(string numCarteReduction);
        string getNumUSCarteReduction(string sigleAgence);

        bool isCarteReductionValide(string numCarteReduction, DateTime date);

        void insertToGridCarteReduction(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableCarteReduction(string strRqst);
    }
}