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
    /// Interface du service abonnementNVDevis
    /// </summary>
    public interface IntfDalUSAbonnementNVDevis
    {
        string insertUSAbonnementNVDevis(crlUSAbonnementNVDevis abonnementNVDevis, string sigleAgence);
        bool updateUSAbonnementNVDevis(crlUSAbonnementNVDevis abonnementNVDevis);
        crlUSAbonnementNVDevis selectUSAbonnementNVDevis(string numAbonnementNVDevis);
        string getNumUSAbonnementNVDevis(string sigleAgence);
        crlUSAbonnementNV getUSAbonnementNV(crlUSAbonnementNVDevis abonnementNVDevis);
        crlUSAbonnementNV getUSAbonnementNV(crlUSAbonnementNVDevis abonnementNVDevis, crlUSCarte carte);

        bool deleteUSAbonnementNVDevis(string numAbonnementNVDevis);
        bool deleteUSAbonnementNVDevisProforma(string numProforma);

        void insertToGridAbonnementNVDevisProforma(GridView gridView, string param, string paramLike, string valueLike, string numProforma);
        DataTable getDataTableAbonnementNVDevisProforma(string strRqst);
    }
}
