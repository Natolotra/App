using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.crl;
using System.Collections.Generic;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service EscorteVoyage voyage
    /// </summary>
    public interface IntfDalEscorteVoyage
    {
        string insertEscorteVoyage(crlEscorteVoyage EscorteVoyage);
        string insertEscorteVoyageAll(crlEscorteVoyage EscorteVoyage);
        bool deleteEscorteVoyage(crlEscorteVoyage EscorteVoyage);
        bool deleteEscorteVoyage(string idEscorteVoyage);
        bool updateEscorteVoyage(crlEscorteVoyage EscorteVoyage);
        bool updateEscorteVoyageAll(crlEscorteVoyage EscorteVoyage);

        string isEscorteVoyage(crlEscorteVoyage EscorteVoyage);
        int isEscorteVoyageInt(crlEscorteVoyage EscorteVoyage);

        crlEscorteVoyage selectEscorteVoyage(string idEscorteVoyage);
        string getIdEscorteVoyage();
        void loadDdlTri(DropDownList ddlTri);

        List<crlEscorteVoyage> selectEscorteVoyageFB(string numerosFB);

        void insertToGridEscorteVoyageFB(GridView gridView, string numerosFB);
        DataTable getDataTableEscorteVoyageFB(List<crlEscorteVoyage> escorteVoyages);
    }
}