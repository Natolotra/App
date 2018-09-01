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

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service chauffeur
    /// </summary>
    public interface IntfDalChauffeur
    {
        string insertChauffeur(crlChauffeur Chauffeur, string sigleAgence);
        bool insertAssoVehiculeChauffeur(string numVehicule, string idChauffeur);
        bool deleteChauffeur(crlChauffeur Chauffeur);
        bool deleteChauffeur(string idChauffeur);
        bool deleteAssoVehiculeChauffeur(string numVehicule, string idChauffeur);
        bool updateChauffeur(crlChauffeur Chauffeur);

        int isChauffeur(crlChauffeur Chauffeur);
        string isChauffeurStr(crlChauffeur Chauffeur);
        crlChauffeur selectChauffeur(string idChauffeur);
        void loadDdlTri(DropDownList ddlTri);
        string getIdChauffeur(string sigleAgence);

        void insertToGridChauffeur(GridView gridView, string param, string paramLike, string valueLike, string numVehicule);
        DataTable getDataTableChauffeur(string strRqst);
        void insertToGridChauffeurAll(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableChauffeurAll(string strRqst);
        void insertToGridChauffeurListeNoire(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableChauffeurListeNoire(string strRqst);

    }
}
