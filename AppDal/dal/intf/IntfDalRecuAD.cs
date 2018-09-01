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
    /// Interface du service recu AD
    /// </summary>
    public interface IntfDalRecuAD
    {
        string insertRecuAD(crlRecuAD RecuAD);
        string insertRecuADAssociation(crlRecuAD RecuAD, string numAutorisationDepart);
        bool insertAssociationRecuADAD(string numRecuAD, string numerosnumAutorisationDepart);
        bool deleteRecuAD(crlRecuAD RecuAD);
        bool deleteRecuAD(string numRecuAD);
        bool updateRecuAD(crlRecuAD RecuAD);


        crlRecuAD selectRecuAD(string numRecuAD);
        List<crlRecuAD> selectRecuADProprietaireFactureIsNull(string numProprietaire);
        List<crlRecuAD> selectRecuADProprietaireResteNonNull(string numProprietaire);
        List<crlRecuAD> selectRecuADFacture(string numFacture);
        crlRecuAD isValideRecuAD(string numRecuAD);

        bool isValideMontant(double montant, string numAutorisationDepart);
        string getNumRecuAD(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        void loadDdlLibelle(DropDownList ddlTri);

        double getTotalRecuADDecaisser(string matriculeAgent, DateTime dateDebut, DateTime dateFin);

        void insertToGridAvanceAutorisationDepart(GridView gridView, string numAutorisationDepart);
        DataTable getDataTableAvanceAutorisationDepart(string strRqst);

        void insertToGridRecuADDecaisser(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent, DateTime dateDebut, DateTime dateFin);
        DataTable getDataTableRecuADDecaisser(string strRqst);
    }
}