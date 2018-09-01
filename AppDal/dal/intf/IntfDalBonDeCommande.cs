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
    /// Interface du service bon de commande
    /// </summary>
    public interface IntfDalBonDeCommande
    {
        crlBonDeCommande selectBonDeCommande(string numBonDeCommande);
        string insertBonDeCommande(crlBonDeCommande bonDeCommande, string sigleAgence);
        bool updateBonDeCommande(crlBonDeCommande bonDeCommande);

        string getNumBonDeCommande(string sigleAgence);
        crlBonDeCommande isBonDeCommandeCredit(string numBonDeCommande);

        void insertToGridBonDeCommande(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableBonDeCommande(string strRqst);

        void insertToGridBonDeCommandeNonPaie(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableBonDeCommandeNonPaie(string strRqst);

        void insertToGridBonDeCommandeNonPaieDate(GridView gridView, string param, string paramLike, string valueLike, DateTime datePaiement);
        DataTable getDataTableBonDeCommandeNonPaieDate(string strRqst);

        void insertToGridBonDeCommandeNonPaieDateDF(GridView gridView, string param, string paramLike, string valueLike, DateTime datePaiementDebut, DateTime datePaiementFin);
        DataTable getDataTableBonDeCommandeNonPaieDateDF(string strRqst);

    }
}