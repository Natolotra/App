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
    /// Summary description for IntfDalDureeAbonnementDevis
    /// </summary>
    public interface IntfDalDureeAbonnementDevis
    {
        crlDureeAbonnementDevis selectDureeAbonnementDevis(string numDureeAbonnementDevis);
        string insertDureeAbonnementDevis(crlDureeAbonnementDevis dureeAbonnementDevis, string sigleAgence);
        bool updateDureeAbonnementDevis(crlDureeAbonnementDevis dureeAbonnementDevis);

        List<crlDureeAbonnement> getDureeAbonnement(crlDureeAbonnementDevis dureeAbonnementDevis, crlAgent agent);

        string getNumDureeAbonnementDevis(string sigleAgence);

        void insertToGridDureeAbonnementDevis(GridView gridView, string param, string paramLike, string valueLike, string numProforma);
        DataTable getDataTableDureeAbonnementDevis(string strRqst);
    }
}
