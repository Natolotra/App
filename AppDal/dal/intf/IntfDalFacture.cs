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
    /// interface du service facture
    /// </summary>
    public interface IntfDalFacture
    {
        string insertFacture(crlFacture Facture);
        string insertFactureAssoc(crlFacture Facture);
        bool insertAssocFactureAD(string numFacture, string numAutorisationDepart);
        bool deleteFacture(crlFacture Facture);
        bool deleteFacture(string numFacture);
        bool updateFacture(crlFacture Facture);
        crlFacture selectFacture(string numFacture);
        string getNumFacture(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        List<crlAutorisationDepart> selectADForFacture(string numFacture);

        double getTotalPrixBillet(string numFacture);
        double getTotalPrixBagage(string numFacture);
        double getTotalPrixCommission(string numFacture);
        double getTotalMontantRecu(string numFacture);

        double getMontantRecette(string numFacture);
        double getMontantRecu(string numFacture);

        void insertToGridFactureNotRecu(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableFactureNotRecu(string strRqst);
       
    }
}