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
    /// Interface du service duree abonnement
    /// </summary>
    public interface IntfDalDureeAbonnement
    {
        crlDureeAbonnement selectDureeAbonnement(string numDureeAbonnement);
        string insertDureeAbonnement(crlDureeAbonnement dureeAbonnement);
        bool updateDureeAbonnement(crlDureeAbonnement dureeAbonnement);

        string getNumDureeAbonnement(string sigleAgence);

        void insertToGridDureeAbonnementNonRecu(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement);
        DataTable getDataTableDureeAbonnementNonRecu(string strRqst);

        void insertToGridDureeAbonnementValide(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement);
        DataTable getDataTableDureeAbonnementValide(string strRqst);
    }
}
