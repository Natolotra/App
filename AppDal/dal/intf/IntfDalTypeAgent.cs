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
    /// Interface pour le service type agent
    /// </summary>
    public interface IntfDalTypeAgent
    {
        void loadDddlTypeAgent(DropDownList ddl);
        crlTypeAgent selectTypeAgent(string typeAgent);
        bool insertTypeAgent(crlTypeAgent typeAgent);
        bool updateTypeAgent(crlTypeAgent typeAgent, string typeAgentStr);
        string isTypeAgent(crlTypeAgent typeAgent, string typeAgentStr);

        void insertToGridTypeAgent(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableTypeAgent(string strRqst);
    }
}
