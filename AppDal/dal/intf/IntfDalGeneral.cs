using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace arch.dal.intf
{
    public interface IntfDalGeneral
    {
        int delete(string paramTable, string paramPropriete, string paramValue);

        int upDate(string strCommande);

        DataTable getDateTable(string strCommand);

        TimeSpan getTimeSpan(string timeSpanStr);

        string getStringTimeSpan(TimeSpan timeSpan);

        string getTextTimeSpan(string timeSpanStr);

        string separateurDesMilles(string strMontant);
    }
}
