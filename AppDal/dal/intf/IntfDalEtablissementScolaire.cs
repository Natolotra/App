using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalEtablissementScolaire
    /// </summary>
    public interface IntfDalEtablissementScolaire
    {
        string insertEtablissementScolaire(crlEtablissementScolaire etablissementScolaire, string sigleAgence);
        bool updateEtablissementScolaire(crlEtablissementScolaire etablissementScolaire);
        crlEtablissementScolaire selectEtablissementScolaire(string numEtablissementScolaire);
        string isEtablissementScolaire(crlEtablissementScolaire etablissementScolaire);
        string getNumEtablissementScolaire(string sigleAgence);

        string insertEtablissementScolaire(crlEtablissementScolaire etablissementScolaire, string sigleAgence, HtmlGenericControl divIndication);
        bool updateEtablissementScolaire(crlEtablissementScolaire etablissementScolaire, HtmlGenericControl divIndication, string numIndividu, string sigleAgence);

        void loadDddlTypeEtablissementScolaire(DropDownList ddl);
        void loadDddlEtablissementScolaire(DropDownList ddl);

        void insertToGridEtablissementScolaire(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableEtablissementScolaire(string strRqst);
    }
}