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
    /// Description résumée de IntfDalTarifDeveloppement
    /// </summary>
    public interface IntfDalTarifDeveloppement
    {
        crlTarifDeveloppement selectTarifDeveloppement(string numTarifDeveloppement);
        crlTarifDeveloppement selectTarifDeveloppementZone(string zone);
        string insertTarifDeveloppement(crlTarifDeveloppement tarifDeveloppement, string sigleAgence);
        bool updateTarifDeveloppement(crlTarifDeveloppement tarifDeveloppement);
        string isTarifDeveloppement(crlTarifDeveloppement tarifDeveloppement);
        string getNumTarifDeveloppement(string sigleAgence);

        void insertToGridTarifDeveloppement(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableTarifDeveloppement(string strRqst);
    }
}