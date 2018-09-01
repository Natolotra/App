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
    /// Interface du service prix de base
    /// </summary>
    public interface IntfDalUSPrixBase
    {
        string insertUSPrixBase(crlUSPrixBase prixBase, string sigleAgence);
        bool updateUSPrixBase(crlUSPrixBase prixBase);
        string isUSPrixBase(crlUSPrixBase prixBase);
        crlUSPrixBase selectUSPrixBase(string numPrixBase);
        crlUSPrixBase selectUSPrixBase(int niveau);
        string getNumUSPrixBase(string sigleAgence);

        int getNiveauPrixBase(string numLieuD, string numLieuF);
        int getNiveauPrixBase(crlUSZone zoneD, crlUSZone zoneF, bool isMemeAxe);
        int getNiveauPrixBase(string numZoneD, string numZoneF, bool isMemeAxe);
        bool isMemeAxe(string numLieuD, string numLieuF);

        void loadDDLNiveauPrixBase(DropDownList ddlZone);

        void insertToGridPrixBase(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTablePrixBase(string strRqst);
    }
}