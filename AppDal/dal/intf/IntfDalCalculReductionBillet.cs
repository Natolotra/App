using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalCalculReductionBillet
    /// </summary>
    public interface IntfDalCalculReductionBillet
    {
        crlCalculReductionBillet selectCalculReductionBillet(string numCalculReductionBillet);

        crlCalculReductionBillet selectCalculReductionBilletIndicateur(string indicateurCalculReductionBillet);

        void loadDdlCulculeReductionBillet(DropDownList ddl);

        void loadDdlCalculeReductionBillet(DropDownList ddl, string strWhere);
    }
}