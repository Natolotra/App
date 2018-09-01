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
    /// Interface du service us voyage
    /// </summary>
    public interface IntfDalUSVoyage
    {
        string insertUSVoyage(crlUSVoyage voyage, string sigleAgence);
        bool updateUSVoyage(crlUSVoyage voyage);
        crlUSVoyage selectUSVoyage(string numVoyage);
        string getNumUSVoyage(string sigleAgence);

        double montantTotalVoyage(string numVoyage);

        bool isChauffeurVoyage(string matriculeAgent, string matriculeAgentN);
        bool isReceveurVoyage(string matriculeAgent, string matriculeAgentN);
        bool isControleVoyage(string matriculeAgent, string matriculeAgentN);
        bool isVehiculeVoyage(string numLicence, string numLicenceN);
        bool isMaterielVoyage(string numAppareil, string numAppareilN);

        void insertToGridUSVoyageNonArrive(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableUSVoyageNonArrive(string strRqst);
    }
}
