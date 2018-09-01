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
    /// Interface du service personne
    /// </summary>
    public interface IntfDalReceptionnaire
    {
        string insertPersonne(crlReceptionnaire Personne, string sigleAgence);
        bool deletePersonne(crlReceptionnaire Personne);
        bool deletePersonne(string idPersonne);
        bool updatePersonne(crlReceptionnaire Personne);

        /// <summary>
        /// is Personne
        /// </summary>
        /// <param name="Personne"></param>
        /// <returns>s'il retourne 0 fales s'il retourne 1 cin,s'il retourne 2 login</returns>
        string isPersonne(crlReceptionnaire Personne);
        int isPersonneInt(crlReceptionnaire Personne);
        crlReceptionnaire selectPersonne(string idPersonne);
        string getIdPersonne(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        void insertToGridReceptionnaire(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableReceptionnaire(string strRqst);
       
    }
}
