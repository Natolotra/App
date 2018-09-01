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
    /// Interface du service Autorisation de voyage
    /// </summary>
    public interface IntfAutorisationVoyage
    {
        string insertAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage);
        bool deleteAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage);
        bool deleteAutorisationVoyage(string numerosAV);
        bool updateAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage);

        int isAutorisationVoyage(crlAutorisationVoyage AutorisationVoyage);
        crlAutorisationVoyage selectAutorisationVoyage(string numerosAV);
        string getNumerosAV(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        void insertToGridAutorisationSansFicheBord(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        void insertToGridAutorisationSansFicheBordASC(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableAutorisationSansFicheBord(string strRqst);
    }
}
