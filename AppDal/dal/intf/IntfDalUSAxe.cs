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
    /// Interface du service axe
    /// </summary>
    public interface IntfDalUSAxe
    {
        string insertUSAxe(crlUSAxe axe, string sigleAgence);
        bool updateUSAxe(crlUSAxe axe);
        crlUSAxe selectUSAxe(string numAxe);
        crlUSAxe selectUSAxeLieu(string numLieu);
        string isUSAxe(crlUSAxe axe);
        string getNumUSAxe(string sigleAgence);

        bool insertUSAssocAxeLieu(string numAxe, string numLieu);
        bool deleteUSAssocAxeLieu(string numAxe, string numLieu);

        void loadDdlUSAxe(DropDownList ddl);

        void insertToGridAxe(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableAxe(string strRqst);
    }
}
