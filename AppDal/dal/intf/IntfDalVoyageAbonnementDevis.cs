using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Data;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalVoyageAbonnementDevis
    /// </summary>
    public interface IntfDalVoyageAbonnementDevis
    {
        crlVoyageAbonnementDevis selectVoyageAbonnementDevis(string numVoyageAbonnementDevis);
        string insertVoyageAbonnementDevis(crlVoyageAbonnementDevis voyageAbonnementDevis, string sigleAgence);
        bool updateVoyageAbonnementDevis(crlVoyageAbonnementDevis voyageAbonnementDevis);

        crlVoyageAbonnement getVoyageAbonnement(crlVoyageAbonnementDevis voyageAbonnementDevis, crlAgent agent);

        string getNumVoyageAbonnementDevis(string sigleAgence);

        void insertToGridVoyageAbonnementDevis(GridView gridView, string param, string paramLike, string valueLike, string numProforma);
        DataTable getDataTableVoyageAbonnementDevis(string strRqst);
    }
}