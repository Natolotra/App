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
    /// Interface du service arret
    public interface IntfDalUSArret
    {
        string insertUSArret(crlUSArret arret, string sigleAgence);
        bool updateUSArret(crlUSArret arret);
        string isUSArret(crlUSArret arret);
        crlUSArret selectUSArret(string numArret);
        string getNumUSArret(string sigleAgence);

        void loadDdlArret(DropDownList ddl, string numArret);
        void loadDdlArretVoyage(DropDownList ddl, string numVoyage);

        void insertToGridArret(GridView gridView, string param, string paramLike, string valueLike, string numLieu);
        DataTable getDataTableArret(string strRqst);

        void insertToGridArretLigne(GridView gridView, string param, string paramLike, string valueLike, string numLigne);
        DataTable getDataTableArretLigne(string strRqst);
        void insertToGridArretNonLigne(GridView gridView, string param, string paramLike, string valueLike, string numLigne);
        DataTable getDataTableArretNonLigne(string strRqst);
    }
}
