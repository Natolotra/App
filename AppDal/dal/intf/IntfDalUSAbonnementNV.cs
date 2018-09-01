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
using System.Collections.Generic;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service USAbonnementNV
    /// </summary>
    public interface IntfDalUSAbonnementNV
    {
        string insertUSAbonnementNV(crlUSAbonnementNV abonnementNV, string sigleAgence);
        bool updateUSAbonnementNV(crlUSAbonnementNV abonnementNV);
        crlUSAbonnementNV selectUSAbonnementNV(string numAbonnementNV);
        crlUSAbonnementNV selectUSAbonnementNVCarte(string numCarte);
        string getNumUSAbonnementNV(string sigleAgence);
        List<crlUSAbonnementNVDevis> getAbonnementNVDevisValide(string numAbonnementNV);
        int getNombreVoyageAbonnementNV(string numAbonnementNV);

        void loadDdlAbonnementNVNonCarte(DropDownList ddl, string numAbonnement);

        void insertToGridAbonnementNV(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement);
        DataTable getDataTableAbonnementNV(string strRqst);
    }
}
