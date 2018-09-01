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
    /// Interface du service IntfDalIndividu
    /// </summary>
    public interface IntfDalIndividu
    {
        crlIndividu selectIndividu(string numIndividu);

        string insertIndividu(crlIndividu Individu, string sigleAgence);

        string isIndividu(crlIndividu Individu);

        string isCINIndividu(crlIndividu Individu);
        string isNotCINIndividu(crlIndividu Individu);
        string isAllIndividu(crlIndividu Individu);

        string insertIndividu(crlIndividu Individu, string sigleAgence, HtmlGenericControl divIndication);
        bool updateIndividu(crlIndividu Individu, HtmlGenericControl divIndication);

        bool updateIndividu(crlIndividu Individu);

        string getNumIndividu(string sigleAgence);



        void insertToGridIndividu(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableIndividu(string strRqst);
    }
}