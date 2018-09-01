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
    /// Interface du service DesignationCommission
    /// </summary>
    public interface IntfDalDesignationCommission
    {
        string insertDesignationCommission(crlDesignationCommission DesignationCommission, string sigleAgence);
        bool deleteDesignationCommission(crlDesignationCommission DesignationCommission);
        bool deleteDesignationCommission(string numDesignation);
        bool updateDesignationCommission(crlDesignationCommission DesignationCommission);
        string isDesignationCommission(crlDesignationCommission DesignationCommission);

        crlDesignationCommission selectDesignationCommission(string numDesignation);
        List<crlDesignationCommission> selectDesignationCommissions(string typeCommission);
        string getNumDesignation(string sigleAgence);

        void loadDdlDesignationCommission(DropDownList ddl,string typeCommission);
        //void loadDdlTri(DropDownList ddlTri);

        void insertToGridDesignationCommission(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableDesignationCommission(string strRqst);
    }
}