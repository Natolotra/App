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
    /// Interface du service carte us
    /// </summary>
    public interface IntfDalUSCarte
    {
        string insertUSCarte(crlUSCarte carte, string sigleAgence);
        bool updateUSCarte(crlUSCarte carte);
        string isUSCarte(crlUSCarte carte);
        crlUSCarte selectUSCarte(string numCarte);
        string getNumUSCarte(string sigleAgence);

        void loadDdlUSCarteDisponible(DropDownList ddl, string numAgence);
        void loadDdlUSCarteAbonnement(DropDownList ddl, string numAbonnement);
        void loadDdlUSCarteAbonnementNonBloquer(DropDownList ddl, string numAbonnement);
        void loadDdlUSCarteAbonnementBloquer(DropDownList ddl, string numAbonnement);

        void loadDdlUSCarteReduction(DropDownList ddl, string numReductionParticulier);
        void loadDdlUSCarteReductionBloquer(DropDownList ddl, string numReductionParticulier);
        void loadDdlUSCarteReductionNonBloquer(DropDownList ddl, string numReductionParticulier);

        bool insertAssocAgenceCarte(string numAgence, string numCarte, string matriculeAgent, string numAbonnement, string numUSReductionParticulier, string commentaire);
        bool deleteAssocAgenceCarte(string numAgence, string numCarte, string dateTimeStr);

        void insertToGridCarte(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableCarte(string strRqst);
    }
}