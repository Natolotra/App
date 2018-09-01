using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service ImplDalSourceEnergie
    /// </summary>
    public class ImplDalSourceEnergie : IntfDalSourceEnergie
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalSourceEnergie(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalSourceEnergie()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlSourceEnergie IntfDalSourceEnergie.selectSourceEnergie(string sourceEnergie)
        {
            #region declaration
            crlSourceEnergie sourceEnergieObj = null;
            #endregion

            #region implementation
            if (sourceEnergie != "")
            {
                this.strCommande = "SELECT * FROM `sourceenergie` WHERE (`sourceEnergie`='" + sourceEnergie + "')";

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    this.reader = this.serviceConnection.select(this.strCommande);
                    if (this.reader != null)
                    {
                        if (this.reader.HasRows)
                        {
                            if (this.reader.Read())
                            {
                                sourceEnergieObj = new crlSourceEnergie();
                                sourceEnergieObj.SourceEnergiePro = this.reader["sourceEnergie"].ToString();
                                sourceEnergieObj.CommentaireSourceEnergie = this.reader["commentaireSourceEnergie"].ToString();
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

            }
            #endregion

            return sourceEnergieObj;
        }

        void IntfDalSourceEnergie.loadDddlSourceEnergie(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                this.strCommande = "SELECT * FROM `sourceenergie`";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["sourceEnergie"].ToString(), this.reader["sourceEnergie"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion
        }
        #endregion


        
    }
}