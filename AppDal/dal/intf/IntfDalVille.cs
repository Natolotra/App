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
    /// Interface du service ville
    /// </summary>
    public interface IntfDalVille
    {
        string insertVille(crlVille Ville, string sigleAgence);
        bool deleteVille(crlVille Ville);
        bool deleteVille(string numVille);
        bool updateVille(crlVille Ville);

        int isVilleInt(crlVille Ville);
        string isVille(crlVille Ville);
        crlVille selectVille(string numVille);
        crlVille selectVilleNom(string nomVille);
        List<crlVille> selectVillesForItineraire(string idItineraire);
        List<crlVille> selectVillesForRN(string routeNationale);

        /*
        crlVille selectVilleForTrajet(string numTrajet, string NumVille);
        List<crlVille> selectVilleTrajet(string NumVille);
        */
        string getNumVille(string sigleAgence);

        
        void insertToGridVille(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableVille(string strRqst);

        void insertToGridVilleRN(GridView gridView, string param, string paramLike, string valueLike, string routeNationale);
        DataTable getDataTableVilleRN(string strRqst);

        void insertToGridVilleNotRN(GridView gridView, string param, string paramLike, string valueLike, List<crlVille> villes);
        DataTable getDataTableVilleNotRN(string strRqst);

        void insertToGridVilleDestination(GridView gridView, string param, string paramLike, string valueLike, string numVille);

        void loadDdlVilleA(DropDownList ddl);
        void loadDdlVille(DropDownList ddl);
        void loadDdlVille(DropDownList ddl, string numVille);
        void loadDdlVilleDestination(DropDownList ddl,string numVilleDepart);
        
    }
}