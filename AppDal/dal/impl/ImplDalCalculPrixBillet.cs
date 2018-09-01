using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using arch.dal.intf;
using arch.crl;
using System.Web.UI.WebControls;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation
    /// du serviceImplDalCalculPrixBillet
    /// </summary>
    public class ImplDalCalculPrixBillet : IntfDalCalculPrixBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCalculPrixBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCalculPrixBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlCalculPrixBillet IntfDalCalculPrixBillet.selectCalculPrixBillet(string numCalculPrixBillet)
        {
            #region declaration
            crlCalculPrixBillet CalculPrixBillet = null;
            #endregion

            #region implementation
            if (numCalculPrixBillet != "")
            {
                this.strCommande = "SELECT * FROM `calculprixbillet` WHERE (`numCalculPrixBillet`='" + numCalculPrixBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            CalculPrixBillet = new crlCalculPrixBillet();
                            CalculPrixBillet.NumCalculPrixBillet = this.reader["numCalculPrixBillet"].ToString();
                            CalculPrixBillet.IndicateurCalculPrixBillet = this.reader["indicateurCalculPrixBillet"].ToString();
                            try
                            {
                                CalculPrixBillet.PourcentagePrixBillet = double.Parse(this.reader["pourcentagePrixBillet"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return CalculPrixBillet;
        }

        crlCalculPrixBillet IntfDalCalculPrixBillet.selectCalculPrixBilletIndicateur(string indicateurCalculPrixBillet)
        {
            #region declaration
            crlCalculPrixBillet CalculPrixBillet = null;
            #endregion

            #region implementation
            if (indicateurCalculPrixBillet != "")
            {
                this.strCommande = "SELECT * FROM `calculprixbillet` WHERE (`indicateurCalculPrixBillet`='" + indicateurCalculPrixBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            CalculPrixBillet = new crlCalculPrixBillet();
                            CalculPrixBillet.NumCalculPrixBillet = this.reader["numCalculPrixBillet"].ToString();
                            CalculPrixBillet.IndicateurCalculPrixBillet = this.reader["indicateurCalculPrixBillet"].ToString();
                            try
                            {
                                CalculPrixBillet.PourcentagePrixBillet = double.Parse(this.reader["pourcentagePrixBillet"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return CalculPrixBillet;
        }

        void IntfDalCalculPrixBillet.loadDdlCulculePrixBillet(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                this.strCommande = "SELECT * FROM `calculprixbillet` ORDER BY calculprixbillet.numCalculPrixBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        while(this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["indicateurCalculPrixBillet"].ToString(), this.reader["numCalculPrixBillet"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalCalculPrixBillet.loadDdlCalculePrixBillet(DropDownList ddl, string strWhere)
        {
            #region declaration
            string[] strTab = null;
            string strWhereVar = "";
            #endregion

            #region implementation
            if (ddl != null)
            {
                strTab = strWhere.Split(';');

                if (strTab.Length > 0)
                {
                    for (int i = 0; i < strTab.Length; i++)
                    {
                        if (i == 0)
                        {
                            strWhereVar = " WHERE calculprixbillet.indicateurCalculPrixBillet <> '" + strTab[i] + "'";
                        }
                        else
                        {
                            strWhereVar += " OR calculprixbillet.indicateurCalculPrixBillet <> '" + strTab[i] + "'";
                        }
                    }
                }

                this.strCommande = "SELECT * FROM `calculprixbillet` " + strWhereVar;
                this.strCommande += " ORDER BY calculprixbillet.numCalculPrixBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["indicateurCalculPrixBillet"].ToString(), this.reader["numCalculPrixBillet"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }
        #endregion
      
    }
}