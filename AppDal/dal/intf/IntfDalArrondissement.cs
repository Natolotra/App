using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalArrondissement
    /// </summary>
    public interface IntfDalArrondissement
    {
        string insertArrondissement(crlArrondissement arrondissement, string sigleAgence);
        bool updateArrondissement(crlArrondissement arrondissement);
        string isArrondissement(crlArrondissement arrondissement);
        crlArrondissement selectArrondissement(string numArrondissement);
        string getNumArrondissement(string sigleAgence);

        void loadDddlArrondissement(DropDownList ddl);
        void loadDddlArrondissementCommune(DropDownList ddl, string numCommune);
    }
}