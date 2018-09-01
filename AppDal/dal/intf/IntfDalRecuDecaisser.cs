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
    /// Description résumée de IntfDalRecuDecaisser
    /// </summary>
    public interface IntfDalRecuDecaisser
    {
        crlRecuDecaisser selectRecuDecaisser(string numRecuDecaisser);
        string insertRecuDecaisser(crlRecuDecaisser recuDecaisser, string sigleAgence);
        bool updateRecuDecaisser(crlRecuDecaisser recuDecaisser);

        string getNumRecuDecaisser(string sigleAgence);

        bool insertAssocRecuDecaisserCarte(string numRecuDecaisser, string numCarte);


        void insertToGridRecuDecaisser(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableRecuDecaisser(string strRqst);
    }
}