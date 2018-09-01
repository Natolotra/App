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
    /// Interface du service voyage abonnement
    /// </summary>
    public interface IntfDalVoyageAbonnement
    {
        crlVoyageAbonnement selectVoyageAbonnement(string numVoyageAbonnement);
        string insertVoyageAbonnement(crlVoyageAbonnement voyageAbonnement);
        bool updateVoyageAbonnement(crlVoyageAbonnement voyageAbonnement);

        string getNumVoyageAbonnement(string sigleAgence);

        void insertToGridVoyageAbonnementNonRecu(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement);
        DataTable getDataTableVoyageAbonnementNonRecu(string strRqst);

        void insertToGridVoyageAbonnementValide(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement);
        DataTable getDataTableVoyageAbonnementValide(string strRqst);
    }
}