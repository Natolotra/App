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
    /// Interface du service itineraire
    /// </summary>
    public interface IntfDalItineraire
    {
        string insertItineraire(crlItineraire Itineraire, string sigleAgence);
        string insertItineraireAll(crlItineraire Itineraire, string sigleAgence);

        bool insertAssociationVilleItineraire(crlItineraire Itineraire, crlVille Ville);
        bool insertAssociationItineraireInfoPrixCommission(crlItineraire Itineraire, crlInfoPrixCommission InfoPrixCommission);
        bool insertAssociationTrajetItineraire(string numTrajet, string idItineraire);
        bool insertAssoItineraireRouteNationale(string idItineraire, string routeNationale);
        bool deleteAssoItineraireRouteNationale(string idItineraire, string routeNationale);
        bool deleteAssociationVilleItineraire(string idItineraire);
        bool deleteAssociationItineraireInfoPrixCommission(string idItineraire);
        bool deleteAssociationTrajetItineraire(string numTrajet, string idItineraire);
        bool deleteItineraire(crlItineraire Itineraire);
        bool deleteItineraire(string idItineraire);
        bool updateItineraire(crlItineraire Itineraire);

        List<string> getNonVille(string numVilleDepart, string idItineraire);

        List<crlItineraire> selectAllItineraire();

        int isItineraire(crlItineraire Itineraire);
        string isItineraireStr(crlItineraire Itineraire);
        crlItineraire selectItineraire(string idItineraire);
        string getIdItineraire(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        void loadDdlItineraireForCalendar(DropDownList ddlItineraire, string numVille);
        void loadDdlItineraireVilleD(DropDownList ddlItineraire);

        void loadDdlItineraireVille1(DropDownList ddlItineraire);
        void loadDdlItineraireVille2(DropDownList ddlItineraire, string numVille);

        void insertToGridItineraire(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableItineraire(string strRqst);

        void insertToGridItineraireLicence(GridView gridView, string param, string paramLike, string valueLike, string numLicence);
        DataTable getDataTableItineraireLicence(string strRqst);

        void insertToGridItineraireAll(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableItineraireAll(string strRqst);
      
    }
}
