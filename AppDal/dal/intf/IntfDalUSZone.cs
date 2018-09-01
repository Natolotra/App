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
    /// Summary description for IntfDalUSZone
    /// </summary>
    public interface IntfDalUSZone
    {
        string insertUSZone(crlUSZone zone, string sigleAgence);
        bool updateUSZone(crlUSZone zone);
        crlUSZone selectUSZone(string numZone);
        string isUSZone(crlUSZone zone);
        string getNumUSZone(string sigleAgence);
        string getNumCommune(string numZone);

        void loadDdlZoneUS(DropDownList ddl);
        void loadDdlZoneUSCommune(DropDownList ddl, string numCommune);
        void loadDdlZoneUS(DropDownList ddl, string numZone);
    }
}
