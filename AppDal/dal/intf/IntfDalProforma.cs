using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalProforma
    /// </summary>
    public interface IntfDalProforma
    {
        crlProforma selectProforma(string numProforma);
        string insertProforma(crlProforma proforma);
        bool updateProforma(crlProforma proforma);
        string getNumProforma(string sigleAgence);
        bool deleteProforma(crlProforma proforma);

        List<crlBilletCommande> selectBilletCommandeProforma(string numProforma);
        List<crlCommissionDevis> selectCommissionProforma(string numProforma);
        List<crlDureeAbonnementDevis> selectDureeAbonnementProforma(string numProforma);
        List<crlVoyageAbonnementDevis> selectVoyageAbonnementProforma(string numProforma);
        List<crlUSAbonnementNVDevis> selectUSAbonnementNVDevis(string numProforma);

        double getPrixTotalDureeAbonnementProforma(crlProforma proforma);
        double getPrixTotalVoyageAbonnementProforma(crlProforma proforma);
        double getPrixTotalBilletCommandeProforma(crlProforma proforma);
        double getPrixTotalCommissionDevisProforma(crlProforma proforma);

        double getPrixTotalDureeAbonnementProforma(string numProforma);
        double getPrixTotalVoyageAbonnementProforma(string numProforma);
        double getPrixTotalBilletCommandeProforma(string numProforma);
        double getPrixTotalCommissionDevisProforma(string numProforma);
        double getPrixTotalAbonnementUSNVProforma(string numProforma);
        double getPrixTotalAbonnementUSCarteProforma(string numProforma);

        double getPrixTotalProforma(string numProforma);

        void insertToGridBillet(GridView gridView, List<crlBilletCommande> billets);
        DataTable getDataTableBillet(List<crlBilletCommande> billets);

        void insertToGridBillet(GridView gridView, string param, string paramLike, string valueLike, string numProforma);
        DataTable getDataTableBillet(string strRqst);

        void insertToGridProforma(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableProforma(string strRqst);

        void insertToGridProformaNonPaie(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableProformaNonPaie(string strRqst);

        void insertToGridProformaBilletCommission(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableProformaBilletCommission(string strRqst);

        void insertToGridProformaADTANV(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableProformaDTANV(string strRqst);

        void insertToGridProformaUSNVDT(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableProformaUSNVDT(string strRqst);

        void insertToGridProformaCarteSansAbonnement(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent);
        DataTable getDataTableProformaCarteSansAbonnement(string strRqst);
    }
}