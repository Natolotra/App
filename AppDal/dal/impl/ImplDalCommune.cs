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
using MySql.Data.MySqlClient;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalCommune
    /// </summary>
    public class ImplDalCommune :IntfDalCommune
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCommune()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCommune(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion



        #region IntfDalCommune Members

        string IntfDalCommune.insertCommune(crlCommune commune, string sigleAgence)
        {
            #region declaration
            IntfDalCommune serviceCommune = new ImplDalCommune();
            string numCommune = "";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (commune != null) 
            {
                commune.NumCommune = serviceCommune.getNumCommune(sigleAgence);
                this.strCommande = "INSERT INTO `commune` (`numCommune`,`commune`,`numDistrict`)";
                this.strCommande += " VALUES ('" + commune.NumCommune + "','" + commune.Commune + "',";
                this.strCommande += " '" + commune.NumDistrict + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numCommune = commune.NumCommune;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCommune;
        }

        bool IntfDalCommune.updateCommune(crlCommune commune)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (commune != null)
            {
                this.strCommande = "UPDATE `commune` SET `commune`='" + commune.Commune + "',";
                this.strCommande += " `numDistrict`='" + commune.NumDistrict + "' WHERE";
                this.strCommande += " `numCommune`='" + commune.NumCommune + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalCommune.isCommune(crlCommune commune)
        {
            #region declaration
            string numCommune = "";
            #endregion

            #region implementation
            if (commune != null)
            {
                this.strCommande = "SELECT * FROM `commune` WHERE";
                this.strCommande += " commune.commune = '" + commune.Commune + "' AND";
                this.strCommande += " commune.numDistrict = '" + commune.NumDistrict + "' AND";
                this.strCommande += " commune.numCommune <> '" + commune.NumCommune + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numCommune = this.reader["numCommune"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCommune;
        }

        crlCommune IntfDalCommune.selectCommune(string numCommune)
        {
            #region declaration
            crlCommune commune = null;
            #endregion

            #region implementation
            if (numCommune != "") 
            {
                this.strCommande = "SELECT * FROM `commune` WHERE `numCommune`='" + numCommune + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            commune = new crlCommune();
                            commune.Commune = this.reader["commune"].ToString();
                            commune.NumCommune = this.reader["numCommune"].ToString();
                            commune.NumDistrict = this.reader["numDistrict"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return commune;
        }

        string IntfDalCommune.getNumCommune(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCommune = "00001";
            string[] tempNumCommune = null;
            string strDate = "CO" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT commune.numCommune AS maxNum FROM commune";
            this.strCommande += " WHERE commune.numCommune LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCommune = reader["maxNum"].ToString().ToString().Split('/');
                        numCommune = tempNumCommune[tempNumCommune.Length - 1];
                    }
                    numTemp = double.Parse(numCommune) + 1;
                    if (numTemp < 10)
                        numCommune = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCommune = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCommune = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCommune = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCommune = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numCommune = strDate + "/" + sigleAgence + "/" + numCommune;
            #endregion

            return numCommune;
        }

        void IntfDalCommune.loadDddlCommune(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `commune`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["commune"].ToString(), this.reader["numCommune"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalCommune.loadDddlCommuneDistrict(DropDownList ddl, string numDistrict)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `commune` WHERE `numDistrict`='" + numDistrict + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["commune"].ToString(), this.reader["numCommune"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        #endregion

        #region IntfDalCommune Members


        void IntfDalCommune.loadDddlCommuneUSZone(DropDownList ddl)
        {
            #region implementation
            if (ddl != null) 
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT commune.numCommune, commune.commune FROM commune";
                this.strCommande += " Inner Join uszone ON uszone.numCommune = commune.numCommune";
                this.strCommande += " WHERE uszone.numZone IS NOT NULL";
                this.strCommande += " GROUP BY commune.numCommune";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        while (this.reader.Read()) 
                        {
                            ddl.Items.Add(new ListItem(this.reader["commune"].ToString(), this.reader["numCommune"].ToString()));
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
