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
    /// Summary description for IntfDalCommune
    /// </summary>
    public interface IntfDalCommune
    {
        string insertCommune(crlCommune commune, string sigleAgence);
        bool updateCommune(crlCommune commune);
        string isCommune(crlCommune commune);
        crlCommune selectCommune(string numCommune);
        string getNumCommune(string sigleAgence);

        void loadDddlCommune(DropDownList ddl);
        void loadDddlCommuneDistrict(DropDownList ddl, string numDistrict);
        void loadDddlCommuneUSZone(DropDownList ddl);
    }
}