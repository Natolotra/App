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
    /// Interface du service observation vehicule
    /// </summary>
    public interface IntfDalObservationVehicule
    {
        string insertObservationVehicule(crlObservationVehicule observation, string sigleAgence);
        bool updateObservationVehicule(crlObservationVehicule observation);
        crlObservationVehicule selectObservationVehicule(string numObservationVehicule);
        string getNumObservationVehicule(string sigleAgence);
        int getObservationVehicule(string numVehicule);
        string getObservationVehicule(string numVehicule, int isListeNoire);

        void insertToGridObservationVehicule(GridView gridView, string param, string paramLike, string valueLike, string numVehicule);
        DataTable getDataTableObservationVehicule(string strRqst);
    }
}
