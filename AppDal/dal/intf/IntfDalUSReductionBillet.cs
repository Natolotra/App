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
    /// Interface du service IntfDalUSReductionBillet
    /// </summary>
    public interface IntfDalUSReductionBillet
    {
        string insertUSReductionBillet(crlUSReductionBillet reductionBillet, string sigleAgence);
        bool updateUSReductionBillet(crlUSReductionBillet reductionBillet);
        crlUSReductionBillet selectUSReductionBillet(string numReductionBillet);
        string getNumUSReductionBillet(string sigleAgence);

        void loadDddlReductionBillet(DropDownList ddl);

        void insertToGridUSReductionBillet(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableUSReductionBillet(string strRqst);
    }
}
