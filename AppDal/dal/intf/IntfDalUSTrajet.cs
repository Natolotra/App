using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface Trajet
    /// </summary>
    public interface IntfDalUSTrajet
    {
        string insertUSTrajet(crlUSTrajet trajet, string sigleAgence);
        bool updateUSTrajet(crlUSTrajet trajet);
        crlUSTrajet selectUSTrajet(string numTrajet);
        string getNumUSTrajet(string sigleAgence);
        string isUSTrajet(crlUSTrajet trajet);

        crlUSTrajet getTrajet(string numArretD, string numArretF);

        void insertToGridTrajet(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableTrajet(string strRqst);
    }
}
