using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service IntfDalSourceEnergie
    /// </summary>
    public interface IntfDalSourceEnergie
    {
        crlSourceEnergie selectSourceEnergie(string sourceEnergie);
        void loadDddlSourceEnergie(DropDownList ddl);
    }
}