using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service recu ad
    /// </summary>
    public interface IntfDalTypeRecuAD
    {
        void loadDddlTypeRecuAD(DropDownList ddl);
    }
}