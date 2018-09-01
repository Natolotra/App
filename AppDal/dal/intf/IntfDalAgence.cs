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
    /// Description résumée de IntfDalAgence
    /// </summary>
    public interface IntfDalAgence
    {
        string insertAgence(crlAgence agence);
        bool updateAgence(crlAgence agence);
        int isAgence(crlAgence agence);
        string getNumAgence(string sigleAgence);

        crlAgence selectAgence(string numAgence);
        void loadDdlAgence(DropDownList ddl);

        void insertToGridAgenceListe(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        void insertToGridAgenceListe(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableAgenceListe(string strRqst);

        
    }
}