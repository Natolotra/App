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
    /// Description résumée de IntfDalRecuEncaisser
    /// </summary>
    public interface IntfDalRecuEncaisser
    {
        crlRecuEncaisser selectRecuEncaisser(string numRecuEncaisser);
        string insertRecuEncaisser(crlRecuEncaisser recuEncaisser);
        string insertRecuEncaisserCheque(crlRecuEncaisser recuEncaisser);
        bool updateRecuEncaisser(crlRecuEncaisser recuEncaisser);

        crlRecuEncaisser isValideRecu(string numRecu);

        string getNumRecuEncaisser(string sigleAgence);

        bool insertAssocRecuEncaisserProformaBonDeCommande(string numProforma, string numRecuEncaisser, string numBonDeCommande);

        bool insertAssocRecuEncaisserCarte(string numRecuEncaisser, string numCarte);

        void insertToGridRecuEncaisser(GridView gridView, string param, string paramLike, string valueLike, string numAgence);
        DataTable getDataTableRecuEncaisser(string strRqst);
    }
}