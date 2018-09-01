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
    /// interface service gare routiere
    /// </summary>
    
    public interface IntfDalGareRoutiere
    {
        string insertGareRoutiere(crlGareRoutiere gareRoutiere);
        bool deleteGareRoutiere(crlGareRoutiere gareRoutiere);
        bool deleteGareRoutiere(string numerosGareRoutiere);
        bool updateGareRoutiere(crlGareRoutiere gareRoutiere);
        bool isGareRoutiere(crlGareRoutiere gareRoutiere);
        crlGareRoutiere selectGareRoutiere(string numerosGareRoutiere);
        string getNumerosGareRoutiere(string nomProvince);
        void loadDdlTri(DropDownList ddlTri);
    }
}
