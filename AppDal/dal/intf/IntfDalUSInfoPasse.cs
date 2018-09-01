using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface pour service info passe
    /// </summary>
    public interface IntfDalUSInfoPasse
    {
        string insertUSInfoPasse(crlUSInfoPasse infoPasse, string sigleAgence);
        bool updateUSInfoPasse(crlUSInfoPasse infoPasse);
        string isUSInfoPasse(crlUSInfoPasse infoPasse);
        crlUSInfoPasse selectUSInfoPasse(string numInfoPasse);
        string getNumUSInfoPasse(string sigleAgence);

        void loadDdlInfoPasse(DropDownList ddl, int niveau);

        void insertToGridInfoPasse(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableInfoPasse(string strRqst);
    }
}