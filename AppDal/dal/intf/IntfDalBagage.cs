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

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service bagage
    /// </summary>
    public interface IntfDalBagage
    {
        string insertBagage(crlBagage Bagage, string sigleAgence);
        bool deleteBagage(crlBagage Bagage);
        bool deleteBagage(string idBagage);
        bool updateBagage(crlBagage Bagage);

        
        int isBagage(crlBagage Bagage);
        crlBagage selectBagage(string idBagage);
        crlBagage selectBagageForVoyage(string idVoyage);
        string getIdBagage(string sigleAgence);
        void loadDdlTri(DropDownList ddlTri);

        bool deleteAssociationBagageVoyage(string idVoyage);
        bool insertAssociationBagageVoyage(string idVoyage,string idBagage);
    }
}
