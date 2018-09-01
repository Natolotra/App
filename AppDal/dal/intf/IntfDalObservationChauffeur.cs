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
    /// Interface du service observation chauffeur
    /// </summary>
    public interface IntfDalObservationChauffeur
    {
        string insertObservationChauffeur(crlObservationChauffeur observation, string sigleAgence);
        bool updateObservationChauffeur(crlObservationChauffeur observation);
        crlObservationChauffeur selectObservationChauffeur(string numObservation);
        string getNumObservation(string sigleAgence);
        int getObservationChauffeur(string idChauffeur);
        string getObservationChauffeur(string idChauffeur, int isListeNoire);

        void insertToGridObservationChauffeur(GridView gridView, string param, string paramLike, string valueLike, string idChauffeur);
        DataTable getDataTableObservationChauffeur(string strRqst);
    }
}