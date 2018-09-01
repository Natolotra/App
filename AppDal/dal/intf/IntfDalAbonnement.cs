using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service abonnement
    /// </summary>
    public interface IntfDalAbonnement
    {
        string insertAbonnement(crlAbonnement Abonnement);
        string insertAbonnementAll(crlAbonnement Abonnement);

        bool insertAssocAbonnementBillet(string numAbonnement, string numBillet);
        bool deleteAssocAbonnementBillet(string numAbonnement, string numBillet);

        bool updateAbonnement(crlAbonnement Abonnement);
        bool updateAbonnementAll(crlAbonnement Abonnement);

        crlAbonnement selectAbonnement(string numAbonnement);

        string isAbonnement(crlAbonnement abonnement);

        string getNumAbonnement(string sigleAgence);

        void insertToGridAbonnement(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableAbonnement(string strRqst);
    }
}