using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalDistrict
    /// </summary>
    public interface IntfDalDistrict
    {
        string insertDistrict(crlDistrict district, string sigleAgence);
        bool updateDistrict(crlDistrict district);
        string isDistrict(crlDistrict district);
        crlDistrict selectDistrict(string numDistrict);
        string getNumDistrict(string sigleAgence);

        void loadDddlDistrict(DropDownList ddl);
        void loadDddlDistrictRegion(DropDownList ddl, string nomRegion);
    }
}