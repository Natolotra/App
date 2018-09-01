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
    /// Description résumée de ImplDalModePaiement
    /// </summary>
    public class ImplDalModePaiement : IntfDalModePaiement
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalModePaiement()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalModePaiement(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion


        void IntfDalModePaiement.loadDddlModePaiement(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `modepaiement`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["modePaiement"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }


        void IntfDalModePaiement.loadDddlModePaiement(DropDownList ddl, string strWhere)
        {
            #region declaration
            string[] strTab = null;
            string strWhereVar = "";
            #endregion

            #region implementation
            ddl.Items.Clear();
            strTab = strWhere.Split(';');

            if (strTab.Length > 0)
            {
                for (int i = 0; i < strTab.Length; i++)
                {
                    if (i == 0)
                    {
                        strWhereVar = " WHERE modepaiement.modePaiement <> '" + strTab[i] + "'";
                    }
                    else
                    {
                        strWhereVar += " AND modepaiement.modePaiement <> '" + strTab[i] + "'";
                    }
                }
            }

            this.strCommande = "SELECT * FROM `modepaiement` " + strWhereVar;
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["modePaiement"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }
    }
}