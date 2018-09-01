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
    /// Description résumée de IntfDalPrelevement
    /// </summary>
    public interface IntfDalPrelevement
    {
        string insertPrelevement(crlPrelevement prelevement);
        crlPrelevement selectPrelevement(string numPrelevement);
        string getNumPrelevement(string sigleAgence);

        bool updatePrelevement(crlPrelevement prelevement);

        void insertToGridPrelevement(GridView gridView, string numAutorisationDepart);
        DataTable getDataTablePrelevement(string strRqst);

        void insertToGridPrelevementNonPayer(GridView gridView, string numAutorisationDepart);
        DataTable getDataTablePrelevementNonPayer(string strRqst);
    }
}