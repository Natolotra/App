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
    /// Description résumée de IntfDalUSValidationReduction
    /// </summary>
    public interface IntfDalUSValidationReduction
    {
        string insertUSValidationReduction(crlUSValidationReduction validationReduction, string sigleAgence);
        bool updateUSValidationReduction(crlUSValidationReduction validationReduction);
        string isUSValidationReduction(crlUSValidationReduction validationReduction);
        crlUSValidationReduction selectUSValidationReduction(string numUSValidationReduction);
        crlUSValidationReduction selectUSValidationReductionCarte(string numCarte);
        string getNumUSValidationReduction(string sigleAgence);

        string isEncourValidation(crlUSValidationReduction validationReduction);
        string isEncourUtilisation(crlUSValidationReduction validationReduction);

        void loadDdlValidationReductionValide(DropDownList ddl, string numUSReductionParticulier, DateTime dateNow);
        void loadDdlValidationReductionValideNonCarte(DropDownList ddl, string numUSReductionParticulier, DateTime dateNow);

        void insertToGridUSValidationReduction(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier);
        DataTable getDataTableUSValidationReduction(string strRqst);

        void insertToGridUSValidationReductionEncourValidation(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier);
        DataTable getDataTableUSValidationReductionEncourValidation(string strRqst);

        void insertToGridUSValidationReductionEncourUtilisation(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier);
        DataTable getDataTableUSValidationReductionEncourUtilisation(string strRqst);

        void insertToGridUSValidationReductionPerime(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier);
        DataTable getDataTableUSValidationReductionPerime(string strRqst);

        void insertToGridUSValidationReductionNonValider(GridView gridView, string param, string paramLike, string valueLike, string numUSReductionParticulier);
        DataTable getDataTableUSValidationReductionNonValider(string strRqst);

        void insertToGridUSValidationReductionAll(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableUSValidationReductionAll(string strRqst);
    }
}