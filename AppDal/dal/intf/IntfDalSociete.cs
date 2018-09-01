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
    /// Interface du service societe
    /// </summary>
    public interface IntfDalSociete
    {
        string insertSociete(crlSociete societe, string sigleAgence);
        bool updateSociete(crlSociete societe);
        crlSociete selectSociete(string numSociete);
        string isSociete(crlSociete societe);
        string getNumSociete(string sigleAgence);

        int isBonCommande(string numSociete);

        string insertSociete(crlSociete societe, string sigleAgence, HtmlGenericControl divIndication);
        bool updateSociete(crlSociete societe, HtmlGenericControl divIndication, string numIndividu, string sigleAgence);

        void loadDddlSocieteReduction(DropDownList ddl);

        void insertToGridSociete(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableSociete(string strRqst);
        void insertToGridSocieteReduction(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableSocieteReduction(string strRqst);
    }
}