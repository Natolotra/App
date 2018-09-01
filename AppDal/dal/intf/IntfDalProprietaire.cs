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
    /// Interface du service IntfDalProprietaire
    /// </summary>
    public interface IntfDalProprietaire
    {
        crlProprietaire selectProprietaire(string numProprietaire);

        string insertProprietaire(crlProprietaire proprietaire, string sigleAgence);

        string isProprietaire(crlProprietaire proprietaire);

        string getNumProprietaire(string sigleAgence);

        double getTotalPrixBillet(string numProprietaire);
        double getTotalPrixBagage(string numProprietaire);
        double getTotalPrixCommission(string numProprietaire);
        double getTotalMontantRecu(string numProprietaire);
        double getTotalPrelevemen(string numProprietaire);
        double getTotalRecette(string numProprietaire);
        double getTotalReste(string numProprietaire);

        void insertToGridProprietaire(GridView gridView, string param, string paramLike, string valueLike, string typeProprietaire);
        DataTable getDataTableProprietaire(string strRqst);

        void insertToGridListeVoyageVoitureProprietaire(GridView gridView, string param, string paramLike, string valueLike, string numProprietaire);
        DataTable getDataTableVoyageVoitureProprietaire(string strRqst);

        void insertToGridRecu(GridView gridView, string param, string paramLike, string valueLike, string numProprietaire);
        DataTable getDataTableRecu(string strRqst);

        void insertToGridProprietaireAll(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableProprietaireAll(string strRqst);
    }
}