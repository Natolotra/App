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
    /// Summary description for IntfDalCalculCategorieBillet
    /// </summary>
    public interface IntfDalCalculCategorieBillet
    {
        crlCalculCategorieBillet selectCalculCategorieBillet(string numCalculCategorieBillet);

        crlCalculCategorieBillet selectCalculCategorieBilletIndicateur(string indicateurCalculCategorieBillet);

        void loadDdlCulculeCategorieBillet(DropDownList ddl);

        void loadDdlCalculeCategorieBillet(DropDownList ddl, string strWhere);

        string insertCalculeCategorieBillet(crlCalculCategorieBillet calculCategorieBillet, string sigleAgence);
        string isCalculeCategorieBillet(crlCalculCategorieBillet calculCategorieBillet);
        bool updateCalculCategorieBillet(crlCalculCategorieBillet calculCategorieBillet);
        string getNumCalculCategorieBillet(string sigleAgence);

        void insertToGridCalculCategorieBillet(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableCalculCategorieBillet(string strRqst);

    }
}
