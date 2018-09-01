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
    /// Interface du service billet
    /// </summary>
    public interface IntfDalBillet
    {
        string insertBillet(crlBillet Billet);
        bool deleteBillet(crlBillet Billet);
        bool deleteBillet(string numBillet);
        bool updateBillet(crlBillet Billet);

        crlBillet selectBillet(string numBillet);
        crlBillet isValide(string numBillet, string idItineraire);
        List<crlBillet> selectBilletsForTrajet(string numTrajet);
        string getNumBillet(string sigleAgence);

        double getTotalBilletEncaisser(string matriculeAgent, DateTime dateDebut, DateTime dateFin);

        void insertToGridBillet(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableBillet(string strRqst);

        void insertToGridBilletInsertToFB(GridView gridView, string param, string paramLike, string valueLike, string numVille);
        DataTable getDataTableBilletInsertToFB(string strRqst);

        void insertToGridBilletEncaisse(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent, DateTime dateDebut, DateTime dateFin);
        DataTable getDataTableBilletEncaisse(string strRqst);

        void insertToGridBilletAbonnement(GridView gridView, string param, string paramLike, string valueLike, string numAbonnement);
        DataTable getDataTableBilletAbonnement(string strRqst);
    }
}