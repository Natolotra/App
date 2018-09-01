using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalEtatCivil
    /// </summary>
    public class ImplDalEtatCivil : IntfDalEtatCivil
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalEtatCivil()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalEtatCivil(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalEtatCivil Members

        void IntfDalEtatCivil.loadDddlEtatCivil(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            ddl.Items.Add("");
            this.strCommande = "SELECT * FROM `etatcivil`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["etatCivil"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }

        #endregion
    }
}