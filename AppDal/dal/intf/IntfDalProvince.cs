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
    /// intfDalProvince
    /// interface pour le service province
    /// </summary>
    public interface IntfDalProvince
    {
        void loadDddlProvince(DropDownList ddl);
    }
}
