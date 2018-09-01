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
    /// Interface du service trajet
    /// </summary>
    public interface IntfDalTrajet
    {
        string insertTrajet(crlTrajet Trajet, string sigleAgence);
        bool insertAssociationVilleTrajet(crlTrajet Trajet,crlVille Ville);
        string insertTrajetAll(crlTrajet Trajet, string sigleAgence);
        bool deleteTrajet(crlTrajet Trajet);
        bool deleteTrajet(string numTrajet);
        bool deleteAssociationVilleTrajet(string numTrajet);
        bool updateTrajet(crlTrajet Trajet);

        int isTrajetInt(crlTrajet Trajet);
        string isTrajet(crlTrajet Trajet);
        crlTrajet selectTrajet(string numTrajet);
        crlTrajet selectTrajet(string numVilleD, string numVilleF);

        List<crlTrajet> selectTrajets(string idItineraire);
        List<crlTrajet> selectTrajetsVille(string numVille);

        string getNumTrajet(string sigleAgence);

        void loadDdlListeTrajet(DropDownList ddl, string idItineraire, string ville);

        void insertToGridTrajetItineraire(GridView gridView, string param, string paramLike, string valueLike, string idItineraire);
        DataTable getDataTableTrajetItineraire(string strRqst);

        void insertToGridTrajetNotItineraire(GridView gridView, string param, string paramLike, string valueLike, List<crlTrajet> trajets);
        DataTable getDataTableTrajetNotItineraire(string strRqst);
    }
}