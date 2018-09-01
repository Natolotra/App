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
    /// Summary description for IntfDalBilletCommande
    /// </summary>
    public interface IntfDalBilletCommande
    {
        crlBilletCommande selectBilletCommande(string numBilletCommande);
        string insertBilletCommande(crlBilletCommande billetCommande, string sigleAgence);
        bool updateBilletCommande(crlBilletCommande billetCommande);
        string getNumBilletCommande(string sigleAgence);

        List<crlBillet> getBillet(crlBilletCommande billetCommande, crlAgent agent);

    }
}
