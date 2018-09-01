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
    /// Interface du service recu
    /// </summary>
    public interface IntfDalRecu
    {
        string insertRecu(crlRecu Recu);
        bool deleteRecu(crlRecu Recu);
        bool deleteRecu(string numRecu);
        bool updateRecu(crlRecu Recu);


        crlRecu selectRecu(string numRecu);
        crlRecu isValideRecu(string numRecu);
        
        string getNumRecu();
        void loadDdlTri(DropDownList ddlTri);

        double getTotalRecuEncaisser(string matriculeAgent, DateTime dateDebut, DateTime dateFin);

        void insertToGridRecu(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableRecu(string strRqst);

        void insertToGridRecuEncaisser(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent, DateTime dateDebut, DateTime dateFin);
        DataTable getDataTableRecuEncaisser(string strRqst);
    }
}