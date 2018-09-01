using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    ///     Interface du service region
    /// </summary>
    public interface IntfDalRegion
    {
        void loadDddlRegion(DropDownList ddl);
        void loadDddlRegionProvince(DropDownList ddl, string nomProvince);

        crlRegion selectRegion(string nomRegion);
    }
}