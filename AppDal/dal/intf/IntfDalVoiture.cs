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
    /// Interface du service voiture
    /// </summary>
    public interface IntfDalVoiture
    {
        string insertVoiture(crlVoiture Voiture);
        bool deleteVoiture(crlVoiture Voiture);
        bool deleteVoiture(string numVehicule);
        bool updateVoiture(crlVoiture Voiture);

        int isVoiture(crlVoiture Voiture);
        crlVoiture selectVoiture(string numVehicule);
        string getNumVehicule();
        void loadDdlTri(DropDownList ddlTri);
        void loadDdlItineraire(DropDownList ddlItineraire, List<crlItineraire> Itineraires);
    }
}
