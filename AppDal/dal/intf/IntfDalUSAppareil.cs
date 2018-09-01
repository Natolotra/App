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
    /// Interface du service appareil
    /// </summary>
    public interface IntfDalUSAppareil
    {
        string insertUSAppareil(crlUSAppareil appareil, string sigleAgence);
        bool updateUSArret(crlUSAppareil appareil);
        string isUSAppareil(crlUSAppareil appareil);
        crlUSAppareil selectUSAppareil(string numAppareil);
        string getNumUSAppareil(string sigleAgence);

        void insertToGridAppareil(GridView gridView, string param, string paramLike, string valueLike, string typeAppareil);
        DataTable getDataTableAppareil(string strRqst);

        void insertToGridAppareilListeNoire(GridView gridView, string param, string paramLike, string valueLike, string typeAppareil);
        DataTable getDataTableAppareilListeNoire(string strRqst);
    }
}