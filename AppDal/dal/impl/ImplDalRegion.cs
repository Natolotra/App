using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service region
    /// </summary>
    public class ImplDalRegion : IntfDalRegion
    {
        #region declaration
        IntfDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRegion()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnection = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalRegion(string strConnection)
        {
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalProvince Members
        public void loadDddlRegion(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `region`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["nomRegion"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }
        #endregion

        #region IntfDalRegion Members

        void IntfDalRegion.loadDddlRegion(DropDownList ddl)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `region`";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["nomRegion"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }

        void IntfDalRegion.loadDddlRegionProvince(DropDownList ddl, string nomProvince)
        {
            #region implementation
            ddl.Items.Clear();
            this.strCommande = "SELECT * FROM `region` WHERE `nomProvince`='" + nomProvince + "'";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    ddl.Items.Add("");
                    while (reader.Read())
                    {
                        ddl.Items.Add(reader["nomRegion"].ToString());
                    }
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            #endregion
        }

        crlRegion IntfDalRegion.selectRegion(string nomRegion)
        {
            #region declaration
            crlRegion region = null;
            #endregion

            #region implementation
            if (nomRegion != "")
            {
                this.strCommande = "SELECT * FROM `region` WHERE `nomRegion`='" + nomRegion + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            region = new crlRegion();
                            region.NomRegion = this.reader["nomRegion"].ToString();
                            region.NomProvince = this.reader["nomProvince"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return region;
        }
        #endregion


        
    }
}