using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service Organisme
    /// </summary>
    public interface IntfDalOrganisme
    {
        string insertOrganisme(crlOrganisme organisme, string sigleAgence);
        crlOrganisme selectOrganisme(string numOrganisme);
        bool updateOrganisme(crlOrganisme organisme);
        string isOrganisme(crlOrganisme organisme);
        string getNumOrganisme(string sigleAgence);

        string insertOrganisme(crlOrganisme organisme, string sigleAgence, HtmlGenericControl divIndication);
        bool updateOrganisme(crlOrganisme organisme, HtmlGenericControl divIndication, string numIndividu, string sigleAgence);

        int isBonCommande(string numOrganisme);

        void insertToGridOrganisme(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableOrganisme(string strRqst);
    }
}