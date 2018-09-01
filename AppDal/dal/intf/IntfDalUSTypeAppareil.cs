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
    /// Interface du service type appareil
    /// </summary>
    public interface IntfDalUSTypeAppareil
    {
        void loadDddlTypeAppareil(DropDownList ddl);
        string insertTypeAppareil(crlUSTypeAppareil typeAppareil);
        bool updateTypeAppareil(crlUSTypeAppareil typeAppareilObj, string typeAppareil);
        string isTypeAppareil(crlUSTypeAppareil typeAppareilObj, string typeAppareil);

        void insertToGridTypeAppareil(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableTypeAppareil(string strRqst);
    }
}
