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
    /// Interface du service Point
    /// </summary>
    public interface IntfDalUSPoint
    {
        string insertUSPoint(crlUSPoint point, string sigleAgence);
        bool updateUSPoint(crlUSPoint point);
        crlUSPoint selectUSPoint(string numPoint);
        string getNumUSPoint(string sigleAgence);

        void insertToGridPointVoyage(GridView gridView, string param, string paramLike, string valueLike, string numVoyage);
        DataTable getDataTablePointVoyage(string strRqst);
    }
}
