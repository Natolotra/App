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
    /// IntfDalSocieteTransport
    /// Interface pour le service Societe transport
    /// </summary>
    public interface IntfDalSocieteTransport
    {
        string insertSocieteTransport(crlSocieteTransport SocieteTransport);
        bool deleteSocieteTransport(crlSocieteTransport SocieteTransport);
        bool deleteSocieteTransport(string numerosSociete);
        bool updateSocieteTransport(crlSocieteTransport SocieteTransport);

        int isSocieteTransport(crlSocieteTransport SocieteTransport);
        crlSocieteTransport selectSocieteTransport(string numerosSociete);
        string getNumerosSociete();
        void loadDdlTri(DropDownList ddlTri);
       
    }
}