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
    /// interface du service voyage
    /// </summary>
    public interface IntfDalVoyage
    {
        string insertVoyage(crlVoyage Voyage, string sigleAgence);
        bool deleteVoyage(crlVoyage Voyage);
        bool deleteVoyage(string idVoyage);
        bool updateVoyage(crlVoyage Voyage);

        bool deleteAllVoyage(string idVoyage);

        

        int isVoyage(crlVoyage Voyage);
        crlVoyage selectVoyage(string idVoyage);
        string getIdVoyage(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        void insertToGridVoyageFB(GridView gridView, string param, string paramLike, string valueLike, string numerosFB);
        DataTable getDataTableVoyageFB(string strRqst);

        void insertToGrigVoyageAutorisationDepart(GridView gridView, string param, string paramLike, string valueLike, string numerosFB);
        DataTable getDataTableVoyageAutorisationDepart(string strRqst);
    }
}
