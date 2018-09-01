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
    /// Interaface du service observation agent
    /// </summary>
    public interface IntfDalObservationAgent
    {
        string insertObservationAgent(crlObservationAgent observation, string sigleAgence);
        bool updateObservationAgent(crlObservationAgent observation);
        crlObservationAgent selectObservationAgent(string numObservation);
        string getNumObservation(string sigleAgence);
        int getObservationAgent(string matriculeAgent);
        string getObservationAgent(string matriculeAgent, int isListeNoire);

        void insertToGridObservationAgent(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent);
        DataTable getDataTableObservationAgent(string strRqst);
    }
}