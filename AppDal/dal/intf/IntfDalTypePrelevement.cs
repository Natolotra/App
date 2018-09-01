using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalTypePrelevement
    /// </summary>
    public interface IntfDalTypePrelevement
    {
        crlTypePrelevement selectTypePrelevement(string typePrelevement);
        void loadDddlTypePrelevement(DropDownList ddl);
    }
}