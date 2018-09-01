using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalTypeAgence
    /// </summary>
    public interface IntfDalTypeAgence
    {
        crlTypeAgence selectTypeAgence(string typeAgence);
        void loadDddlTypeAgence(DropDownList ddl);
    }
}