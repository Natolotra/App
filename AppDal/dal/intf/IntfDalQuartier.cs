using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;


namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalQuartier
    /// </summary>
    public interface IntfDalQuartier
    {
        string insertQuartier(crlQuartier quartier, string sigleAgence);
        bool updateQuartier(crlQuartier quartier);
        string isQuartier(crlQuartier quartier);
        crlQuartier selectQuartier(string numQuartier);
        string getNumQuartier(string sigleAgence);

        string getNumQuartier(string quartier, string numCommune, string numArrondissement, string sigleAgence);

        void loadDdlQuartier(DropDownList ddl);
        void loadDdlQuartierCommune(DropDownList ddl, string numCommune);
    }
}