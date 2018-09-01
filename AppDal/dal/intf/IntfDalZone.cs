using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service IntfDalZone
    /// </summary>
    public interface IntfDalZone
    {
        crlZone selectZone(string zone);
        void loadDDLAllZone(DropDownList ddlZone);
        void loadDDLAllZone(DropDownList ddlZone, string strWhere);
    }
}