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
    /// Summary description for IntfDalUSReductionParticulier
    /// </summary>
    public interface IntfDalUSReductionParticulier
    {
        string insertUSReductionParticulier(crlUSReductionParticulier reductionParticulier, string sigleAgence);
        bool updateUSReductionParticulier(crlUSReductionParticulier reductionParticulier);
        string isUSReductionParticulier(crlUSReductionParticulier reductionParticulier);
        crlUSReductionParticulier selectUSReductionParticulier(string numUSReductionParticulier);
        string getNumUSReductionParticulier(string sigleAgence);

        void insertToGridUSReductionParticulier(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableUSReductionParticulier(string strRqst);
    }
}
