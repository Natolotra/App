using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service IntfDalCalculPrixBillet
    /// </summary>
    public interface IntfDalCalculPrixBillet
    {
        crlCalculPrixBillet selectCalculPrixBillet(string numCalculPrixBillet);

        crlCalculPrixBillet selectCalculPrixBilletIndicateur(string indicateurCalculPrixBillet);

        void loadDdlCulculePrixBillet(DropDownList ddl);

        void loadDdlCalculePrixBillet(DropDownList ddl,string strWhere);
    }
}