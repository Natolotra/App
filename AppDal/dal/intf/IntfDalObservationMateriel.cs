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
    /// Summary description for IntfDalObservationMateriel
    /// </summary>
    public interface IntfDalObservationMateriel
    {
        string insertObservationMateriel(crlObservationMateriel observation, string sigleAgence);
        bool updateObservationMateriel(crlObservationMateriel observation);
        crlObservationMateriel selectObservationMateriel(string numObservation);
        string getNumObservation(string sigleAgence);
        int getObservationMateriel(string numAppareil);
        string getObservationMateriel(string numAppareil, int isListeNoire);

        void insertToGridObservationMateriel(GridView gridView, string param, string paramLike, string valueLike, string numAppareil);
        DataTable getDataTableObservationMateriel(string strRqst);
    }
}
