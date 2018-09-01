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
    /// Interface du service agent
    /// </summary>
    public interface IntfDalAgent
    {
        string insertAgent(crlAgent Agent);
        bool deleteAgent(crlAgent Agent);
        bool deleteAgent(string matriculeAgent);
        bool updateAgent(crlAgent Agent);

        /// <summary>
        /// is agent
        /// </summary>
        /// <param name="Agent"></param>
        /// <returns>s'il retourne 0 fales s'il retourne 1 cin,s'il retourne 2 login</returns>
        int isAgent(crlAgent Agent);
        crlAgent selectAgent(string matriculeAgent);
        crlAgent selectAgent(string param, string paramValue);
        string getMatriculeAgent(string sigle);
        void loadDdlTri(DropDownList ddlTri);
        crlAgent login(string login,string motDePasse);

        void insertToGridAgent(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableAgent(string strRqst);

        void insertToGridAgentListe(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableAgentListe(string strRqst);

        void insertToGridAgentParType(GridView gridView, string param, string paramLike, string valueLike, string numAgence, string typeAgent);
        DataTable getDataTableAgentParType(string strRqst);

        void insertToGridAgentListeNoire(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableAgentListeNoire(string strRqst);
    }
}
