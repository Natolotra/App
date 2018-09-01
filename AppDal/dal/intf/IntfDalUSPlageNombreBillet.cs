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
    /// Interface du service IntfDalUSPlageNombreBillet
    /// </summary>
    public interface IntfDalUSPlageNombreBillet
    {
        string insertUSPlageNombreBillet(crlUSPlageNombreBillet plageNombreBillet, string sigleAgence);
        bool updateUSPlageNombreBillet(crlUSPlageNombreBillet plageNombreBillet);
        string isUSPlageNombreBillet(crlUSPlageNombreBillet plageNombreBillet);
        crlUSPlageNombreBillet selectUSPlageNombreBillet(string numPlageNombreBillet);
        string getNumUSPlageNombreBillet(string sigleAgence);

        crlUSReductionBillet getReductionBillet(int nombreBillet);
        crlUSPlageNombreBillet getPlageNombreBillet(int nombreBillet);

        void insertToGridPlageNombreBillet(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTablePlageNombreBillet(string strRqst);
    }
}