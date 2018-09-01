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

namespace arch.dal.intf
{
    public interface IntfDalServicePage
    {
        DataView getDateTable(string strCommand);
        void insertToGridView(GridView gridView, string nomTable, string param);
        void insertToGridView(GridView gridView, string nomTable, string param, string paramLike, string valueLike);

        #region insert to grid agent
        void insertToGridViewAgentVerificateur(GridView gridView, string param, string paramLike, string valueLike);
        #endregion

        #region insert to grid voiture
        void insertToGridViewVoiture(GridView gridView, string param, string paramLike, string valueLike);
        #endregion

        #region insert to grid Autorisation
        void insertToGridViewAutorisation(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        #endregion

        #region insert to grid Autorisation sans fiche de bord
        void insertToGridViewAutorisationSanFicheBord(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        #endregion

        bool testConnection();

    }
}
