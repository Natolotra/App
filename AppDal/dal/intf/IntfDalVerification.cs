using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service verification
    /// </summary>
    public interface IntfDalVerification
    {
        string insertVerification(crlVerification Verification, string sigleAgence);
        bool deleteVerification(crlVerification Verification);
        bool deleteVerification(string idVerification);
        bool updateVerification(crlVerification Verification);

        int isVerification(crlVerification Verification);
        crlVerification selectVerification(string idVerification);
        string getIdVerification(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        void insertToGridVerificationNonValider(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableVerificationNonValider(string strRqst);
    }
}
