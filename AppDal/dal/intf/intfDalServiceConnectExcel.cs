using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace arch.dal.intf
{
    /// <summary>
    /// Interface du service Connect excel
    /// </summary>
    public interface intfDalServiceConnectExcel
    {
        void openConnection();
        void closeConnection();
        OleDbDataReader select(string strCommande);
        int requete(string strCommand);
    }
}