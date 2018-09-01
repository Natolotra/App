using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.crl;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service InfoExedantBagage
    /// </summary>
    public interface IntfDalInfoExedantBagage
    {
        string insertInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage, string sigleAgence);
        bool deleteInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage);
        bool deleteInfoExedantBagage(string numInfoBagage);
        bool updateInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage);


        string isInfoExedantBagage(crlInfoExedantBagage InfoExedantBagage);
        int isInfoExedantBagageInt(crlInfoExedantBagage InfoExedantBagage);
        crlInfoExedantBagage selectInfoExedantBagage(string numInfoBagage);
        string getNumInfoBagage(string sigleAgence);
    }
}