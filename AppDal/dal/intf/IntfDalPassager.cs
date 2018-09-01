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
    /// Interface du service passager
    /// </summary>
    public interface IntfDalPassager
    {
        string insertPassager(crlPassager Passager);
        bool deletePassager(crlPassager Passager);
        bool deletePassager(string idPassager);
        bool updatePassager(crlPassager Passager);

        
        string isPassager(crlPassager Passager);
        crlPassager selectPassager(string idPassager);
        string getIdPassager();
        void loadDdlTri(DropDownList ddlTri);
    }
}
