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
    /// Interface du service IntfDalAutorisationDepart
    /// </summary>
    public interface IntfDalAutorisationDepart
    {
        string insertAutorisationDepart(crlAutorisationDepart AutorisationDepart);
        //string insertADFacture(crlAutorisationDepart AutorisationDepart, double prixDeveloppement);
        bool deleteAutorisationDepart(crlAutorisationDepart AutorisationDepart);
        bool deleteAutorisationDepart(string numAutorisationDepart);
        bool updateAutorisationDepart(crlAutorisationDepart AutorisationDepart);
        crlAutorisationDepart selectAutorisationDepart(string numAutorisationDepart);
        string getNumAutorisationDepart(string sigleAgence);

        List<crlAutorisationDepart> selectADProprietaireFactureIsNull(string numProprietaire);
        List<crlAutorisationDepart> selectADFacture(string numFacture);
        List<crlAutorisationDepart> selectADProprietaireResteNonNull(string numProprietaire);

        double getMontanRecu(string numAutorisationDepart);
        double getMontanDevelopement(string numAutorisationDepart);
        double getMontanPrelevement(string numAutorisationDepart);

        void insertToGridAutorisationDepart(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableAutorisationDepart(string strRqst);
    }
}