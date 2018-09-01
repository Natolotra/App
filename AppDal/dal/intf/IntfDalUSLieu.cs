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
    /// Interface du service lieu
    /// </summary>
    public interface IntfDalUSLieu
    {
        string insertUSLieu(crlUSLieu lieu, string sigleAgence);
        bool updateUSLieu(crlUSLieu lieu);
        string isUSLieu(crlUSLieu lieu);
        crlUSLieu selectUSLieu(string numLieu);
        string getNumUSLieu(string sigleAgence);

        void loadDdlUSLieu(DropDownList ddl);
        void loadDdlUSLieu(DropDownList ddl, string numLieu);
        string getNumLignes(string numLieu);

        void insertToGridLieu(GridView gridView, string param, string paramLike, string valueLike, string numZone);
        DataTable getDataTableLieu(string strRqst);

        void insertToGridLieuAxe(GridView gridView, string param, string paramLike, string valueLike, string numAxe);
        DataTable getDataTableLieuAxe(string strRqst);
        void insertToGridLieuNonAxe(GridView gridView, string param, string paramLike, string valueLike, string numAxe);
        DataTable getDataTableLieuNonAxe(string strRqst);
    }
}