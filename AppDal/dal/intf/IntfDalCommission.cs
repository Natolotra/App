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
using System.Collections.Generic;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service commission
    /// </summary>
    public interface IntfDalCommission
    {
        string insertCommission(crlCommission Commission);
        string insertCommissionAll(crlCommission Commission);
        bool insertCommissionToFB(string idCommission,string numerosFB);

        bool deleteCommission(crlCommission Commission);
        bool deleteCommission(string idCommission);
        bool deleteCommissionToFB(string idCommission, string numerosFB);

        bool updateCommission(crlCommission Commission);
        bool updateCommissionAll(crlCommission Commission);

        string isCommission(crlCommission Commission);
        int isCommissionInt(crlCommission Commission);

        crlCommission selectCommission(string idCommission);
        string getidCommission(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        List<crlCommission> selectCommissionFB(string numerosFB);


        void insertToGridCommissionFB(GridView gridView, string param, string paramLike, string valueLike,string numerosFB);
        DataTable getDataTableCommissionFB(string strCommande);

        void insertToGridCommissionAutorisationDepart(GridView gridView, string param, string paramLike, string valueLike, string numerosFB);
        DataTable getDataTableCommissionAutorisationDepar(string strCommande);

        void insertToGridCommission(GridView gridView,string param,string paramLike,string valueLike);
        DataTable getDataTableCommission(string strCommande);

        void insertToGridCommissionDepart(GridView gridView, string param, string paramLike, string valueLike, string numVille);
        DataTable getDataTableCommissionDepart(string strCommande);

        void insertToGridCommissionNonFB(GridView gridView, string param, string paramLike, string valueLike, string idItineraire, string numerosGareRoutiere);
        DataTable getDataTableCommissionNonFB(string strCommande);

        void insertToGridCommissionArrive(GridView gridView, string param, string paramLike, string valueLike, string numVille);
        DataTable getDataTableCommissionArrive(string strCommande);
    }
}
