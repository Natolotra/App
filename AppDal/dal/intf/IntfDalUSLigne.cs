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
    /// Interface du service ligne
    /// </summary>
    public interface IntfDalUSLigne
    {
        string insertUSLigne(crlUSLigne ligne, string sigleAgence);
        bool updateUSLigne(crlUSLigne ligne);
        crlUSLigne selectUSLigne(string numLigne);
        string getNumUSLigne(string sigleAgence);

        void loadDdlLigne(DropDownList ddl);

        /*bool insertUSAssocLicenceLigne(string numLigne, string numLicence);
        bool deleteUSAssocLicenceLigne(string numLigne, string numLicence);*/

        bool insertUSAssocArretLigne(string numLigne, string numArret);
        bool deleteUSAssocArretLigne(string numLigne, string numArret);

        void insertToGridLigne(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableLigne(string strRqst);

    }
}
