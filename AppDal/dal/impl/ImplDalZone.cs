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
    /// Description résumée de ImplDalZone
    /// </summary>
    public class ImplDalZone : IntfDalZone
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalZone(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalZone()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlZone IntfDalZone.selectZone(string zone)
        {
            #region declaration
            crlZone zoneObj = null;
            #endregion

            #region implementation
            if (zone != "")
            {
                this.strCommande = "SELECT * FROM `zone` WHERE (`zone`='" + zone + "')";

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
                                zoneObj = new crlZone();
                                zoneObj.ZonePro = this.reader["zone"].ToString();
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

            }
            #endregion

            return zoneObj;
        }
       
        void IntfDalZone.loadDDLAllZone(DropDownList ddlZone)
        {
            #region implementation
            if (ddlZone != null)
            {
                this.strCommande = "SELECT * FROM `zone`";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddlZone.Items.Clear();
                        ddlZone.Items.Add(new ListItem("", ""));
                        while(this.reader.Read())
                        {
                            ddlZone.Items.Add(new ListItem(this.reader["zone"].ToString(), this.reader["zone"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion
        }

        void IntfDalZone.loadDDLAllZone(DropDownList ddlZone, string strWhere)
        {
            #region declaration
            string[] strTab = null;
            string strWhereVar = "";
            #endregion

            #region implementation
            if (ddlZone != null)
            {
                ddlZone.Items.Clear();
                strTab = strWhere.Split(';');

                if (strTab.Length > 0)
                {
                    for (int i = 0; i < strTab.Length; i++)
                    {
                        if (i == 0)
                        {
                            strWhereVar = " WHERE zone.zone <> '" + strTab[i] + "'";
                        }
                        else
                        {
                            strWhereVar += " AND zone.zone <> '" + strTab[i] + "'";
                        }
                    }
                }

                this.strCommande = "SELECT * FROM `zone`" + strWhereVar;

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddlZone.Items.Clear();
                        ddlZone.Items.Add(new ListItem("", ""));
                        while (this.reader.Read())
                        {
                            ddlZone.Items.Add(new ListItem(this.reader["zone"].ToString(), this.reader["zone"].ToString()));
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