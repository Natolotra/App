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
    /// Interface du service session caisse
    /// </summary>
    public interface IntfDalSessionCaisse
    {
        crlSessionCaisse selectSessionCaisse(string numSessionCaisse);
        string insertSessionCaisse(crlSessionCaisse sessionCaisse, string sigleAgence);
        bool updateSessionCaisse(crlSessionCaisse sessionCaisse);

        crlSessionCaisse getSessionCaisseEncours(string matriculeAgent);

        bool insertAssocSessionCaisseBillet(string numBillet, string numSessionCaisse);
        bool insertAssocSessionCaisseCommission(string idCommission, string numSessionCaisse);
        bool insertAssocSessionCaisseDureeAbonnement(string numDureeAbonnement, string numSessionCaisse);
        bool insertAssocSessionCaisseVoyageAbonnement(string numVoyageAbonnement, string numSessionCaisse);
        bool insertAssocSessionCaisseRecuEncaisser(string numRecuEncaisser, string numSessionCaisse);
        bool insertAssocSessionCaisseRecuDecaisser(string numRecuDecaisser, string numSessionCaisse);
        bool insertAssocSessionCaisseRecuAD(string numRecuAD, string numSessionCaisse);
        bool insertUSAssocSessionCaisseAbonnementNV(string numAbonnementNV, string numSessionCaisse);
        bool insertUSAssocSessionCaisseBillet(string numBillet, string numSessionCaisse);
        bool insertUSAssocSessionCaisseCarte(string numCarte, string numSessionCaisse, int isEncaisser);

        double getMontantTotalBillet(string numSessionCaisse);
        double getMontantTotalCommission(string numSessionCaisse);
        double getMontantTotalDureeAbonnement(string numSessionCaisse);
        double getMontantTotalVoyageAbonnement(string numSessionCaisse);
        double getMontantTotalRecuEncaisser(string numSessionCaisse);
        double getMontantTotalRecuDecaisser(string numSessionCaisse);
        double getMontantTotalRecuEncaisserCheque(string numSessionCaisse);
        double getMontantTotalRecuEncaisserEspece(string numSessionCaisse);
        double getMontantTotalRecuAD(string numSessionCaisse);
        double getMontantTotalAbonnementNVUS(string numSessionCaisse);
        double getMontantTotalBilletUS(string numSessionCaisse);
        double getMontantTotalCarteEnCaisser(string numSessionCaisse);
        double getMontantTotalCarteDecaisser(string numSessionCaisse);

        string getNumSessionCaisse(string sigleAgence);

        void insertToGridBilletSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableBilletSession(string strRqst);

        void insertToGridCommissionSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableCommissionSession(string strRqst);

        void insertToGridAbonnementDureeTempsSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableAbonnementDureeTempsSession(string strRqst);

        void insertToGridAbonnementNbVoyageSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableAbonnementNbVoyageSession(string strRqst);

        void insertToGridRecuEspeceSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableRecuEspeceSession(string strRqst);

        void insertToGridRecuChequeSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableRecuChequeSession(string strRqst);

        void insertToGridRecuADSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse);
        DataTable getDataTableRecuADSession(string strRqst);

    }
}