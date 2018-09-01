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
    /// Description résumée de IntfDalCooperative
    /// </summary>
    public interface IntfDalCooperative
    {
        string insertCooperative(crlCooperative cooperative, string sigleAgence);
        string isCooperative(crlCooperative cooperative);
        bool updateCooperative(crlCooperative cooperative);

        string getNumCooperative(string sigleAgence);

        crlCooperative selectCooperative(string numCooperative);
        void loadDdlCooperative(DropDownList ddlCooperative);
        void loadDdlCooperativeZoneUS(DropDownList ddlCooperative);

        void insertToGridCooperative(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableCooperative(string strRqst);
    }
}