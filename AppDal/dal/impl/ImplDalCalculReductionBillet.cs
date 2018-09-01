using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.dal.intf;
using arch.crl;
using MySql.Data.MySqlClient;

namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalCalculReductionBillet
    /// </summary>
    public class ImplDalCalculReductionBillet : IntfDalCalculReductionBillet
    {
         #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCalculReductionBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCalculReductionBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region IntfDalCalculReductionBillet Members

        crlCalculReductionBillet IntfDalCalculReductionBillet.selectCalculReductionBillet(string numCalculReductionBillet)
        {
            #region declaration
            crlCalculReductionBillet CalculReductionBillet = null;
            #endregion

            #region implementation
            if (numCalculReductionBillet != "")
            {
                this.strCommande = "SELECT * FROM `calculreductionbillet` WHERE (`numCalculReductionBillet`='" + numCalculReductionBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            CalculReductionBillet = new crlCalculReductionBillet();
                            CalculReductionBillet.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            CalculReductionBillet.IndicateurCalculReductionBillet = this.reader["indicateurCalculReductionBillet"].ToString();
                            try
                            {
                                CalculReductionBillet.PourcentagePrixBillet = double.Parse(this.reader["pourcentagePrixBillet"].ToString());
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

            return CalculReductionBillet;
        }

        crlCalculReductionBillet IntfDalCalculReductionBillet.selectCalculReductionBilletIndicateur(string indicateurCalculReductionBillet)
        {
            #region declaration
            crlCalculReductionBillet CalculReductionBillet = null;
            #endregion

            #region implementation
            if (indicateurCalculReductionBillet != "")
            {
                this.strCommande = "SELECT * FROM `calculreductionbillet` WHERE (`indicateurCalculReductionBillet`='" + indicateurCalculReductionBillet + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            CalculReductionBillet = new crlCalculReductionBillet();
                            CalculReductionBillet.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            CalculReductionBillet.IndicateurCalculReductionBillet = this.reader["indicateurCalculReductionBillet"].ToString();
                            try
                            {
                                CalculReductionBillet.PourcentagePrixBillet = double.Parse(this.reader["pourcentagePrixBillet"].ToString());
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

            return CalculReductionBillet;
        }

        void IntfDalCalculReductionBillet.loadDdlCulculeReductionBillet(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                this.strCommande = "SELECT * FROM `calculreductionbillet` ORDER BY calculreductionbillet.numCalculReductionBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        ddl.Items.Add(new ListItem("", ""));
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

        void IntfDalCalculReductionBillet.loadDdlCalculeReductionBillet(DropDownList ddl, string strWhere)
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
                            strWhereVar = " WHERE calculreductionbillet.indicateurCalculReductionBillet <> '" + strTab[i] + "'";
                        }
                        else
                        {
                            strWhereVar += " OR calculreductionbillet.indicateurCalculReductionBillet <> '" + strTab[i] + "'";
                        }
                    }
                }

                this.strCommande = "SELECT * FROM `calculreductionbillet` " + strWhereVar;
                this.strCommande += " ORDER BY calculreductionbillet.numCalculPrixBillet ASC";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Clear();
                        ddl.Items.Add(new ListItem("", ""));
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["indicateurCalculReductionBillet"].ToString(), this.reader["numCalculReductionBillet"].ToString()));
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
