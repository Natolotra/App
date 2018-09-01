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
    /// Interface du service fiche de bord
    /// </summary>
    public interface IntfDalFicheBord
    {
        string insertFicheBord(crlFicheBord FicheBord);
        bool deleteFicheBord(crlFicheBord FicheBord);
        bool deleteFicheBord(string numerosFB);
        bool updateFicheBord(crlFicheBord FicheBord);

        int isFicheBord(crlFicheBord FicheBord);
        crlFicheBord selectFicheBord(string numerosFB);
        string getNumerosFB(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);
        List<crlCommission> getCommission(string numerosFB);
        List<crlVoyage> getVoyage(string numerosFB);
        List<crlEscorteVoyage> getEscorteVoyage(string numerosFB);

        List<crlFicheBord> selectFicheBord(DateTime date, string heure, string idItineraire, string numAgence);

        int getNombreTotalPassager(string numerosFB);
        double getPoidTotalBagage(string numerosFB);
        double getPoidTotalCommission(string numerosFB);

        double getPrixTotalBillet(string numerosFB);
        double getPrixTotalBagage(string numerosFB);
        double getPrixTotalCommission(string numerosFB);

        void insertToGridFicheBordSansAD(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableFicheBordSansAD(string strRqst);
    }
}
