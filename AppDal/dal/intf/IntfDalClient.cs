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
    /// Interface du service client
    /// </summary>
    public interface IntfDalClient
    {
        string insertClient(crlClient Client, string sigleAgence);
        bool deleteClient(crlClient Client);
        bool deleteClient(string numClient);
        bool updateClient(crlClient Client);


        string isClient(crlClient Client);
        int isClientInt(crlClient Client);
        crlClient selectClient(string numClient);
        string getNumClient(string sigleAgence);

        int isBonCommande(string numClient);

        void insertToGridClient(GridView gridView, string param, string paramLike, string valueLike);
        DataTable getDataTableClient(string strRqst);
    }
}