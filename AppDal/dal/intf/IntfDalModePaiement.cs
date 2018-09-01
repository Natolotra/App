using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service mode paiement
    /// </summary>
    public interface IntfDalModePaiement
    {
        void loadDddlModePaiement(DropDownList ddl);
        void loadDddlModePaiement(DropDownList ddl, string strWhere);
    }
}