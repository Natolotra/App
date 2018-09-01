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
    /// Interface du service IntfDalUSCategorieBillet
    /// </summary>
    public interface IntfDalUSCategorieBillet
    {
        string insertUSCategorieBillet(crlUSCategorieBillet categorieBillet, string sigleAgence);
        bool updateUSCategorieBillet(crlUSCategorieBillet categorieBillet);
        crlUSCategorieBillet selectUSCategorieBillet(string numCategorieBillet);
        string getNumUSCategorieBillet(string sigleAgence);

        string isCategorieBillet(crlUSCategorieBillet categorieBillet);

        void loadDdlCategorieBillet(DropDownList ddl);

        void insertToGridCategorieBillet(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableCategorieBillet(string strRqst);
    }
}
