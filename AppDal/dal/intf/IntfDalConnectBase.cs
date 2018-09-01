using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace arch.dal.intf
{
    public interface IntfDalConnectBase
    {
        void openConnection();
        void closeConnection();
        MySqlDataReader select(string strCommand);
        int requete(string strCommand);
    }
}
