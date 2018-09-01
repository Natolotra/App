using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Data;
using System.Web.UI.WebControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalVehicule
    /// </summary>
    public interface IntfDalVehicule
    {
        crlVehicule selectVehicule(string numVehicule);
        string insertVehicule(crlVehicule vehicule, string sigleAgence);
        bool upDateVehicule(crlVehicule vehicule);
        string isVehicule(crlVehicule vehicule);
        string getNumVehicule(string sigleAgence);

        
        double getTotalRecette(string numVehicule);
        double getTotalReste(string numVehicule);
        double getTotalRecu(string numVehicule);
        List<crlAutorisationDepart> getAutorisationDepartsForFacture(string numVehicule);

        void loadDdlVehiculeProprietaire(DropDownList ddl,string numProprietaire);
        void loadDdlVehiculeProprietaireUS(DropDownList ddl, string numProprietaire);

        void insertToGridVehicule(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableVehicule(string strRqst);

        void insertToGridVehiculeForFacture(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableVehiculeForFacture(string strRqst);

        void insertToGridVehiculeListeNoire(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableVehiculeListeNoire(string strRqst);

        void insertToGridADForVehicule(GridView gridView, string param, string paramLike, string valueLike, string numVehicule);
        DataTable getDataTableADForVehicule(string strRqst);

        void insertToGridRecuForVehicule(GridView gridView, string param, string paramLike, string valueLike, string numVehicule);
        DataTable getDataTableRecuForVehicule(string strRqst);
    }
}