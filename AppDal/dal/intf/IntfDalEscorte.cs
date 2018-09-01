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
    /// interface du service escorte
    /// </summary>
    public interface IntfDalEscorte
    {
        string insertEscorte(crlEscorte Escorte);
        bool deleteEscorte(crlEscorte Escorte);
        bool deleteEscorte(string matriculeEscorte);
        bool updateEscorte(crlEscorte Escorte);

        /// <summary>
        /// is Escorte
        /// </summary>
        /// <param name="Escorte"></param>
        /// <returns>s'il retourne 0 fales s'il retourne 1 cin,s'il retourne 2 login</returns>
        string isEscorte(crlEscorte Escorte);
        int isEscorteInt(crlEscorte Escorte);

        crlEscorte selectEscorte(string matriculeEscorte);
        string getMatriculeEscorte();
        void loadDdlTri(DropDownList ddlTri);
    }
}
