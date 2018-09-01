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
    /// Summary description for ImplDalQuartier
    /// </summary>
    public class ImplDalQuartier : IntfDalQuartier
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalQuartier()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalQuartier(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion



        #region IntfDalQuartier Members

        string IntfDalQuartier.insertQuartier(crlQuartier quartier, string sigleAgence)
        {
            #region declaration
            IntfDalQuartier serviceQuartier = new ImplDalQuartier();
            string numQuartier = "";
            int nbInsert = 0;
            string numArrondissement = "NULL";
            #endregion

            #region implementation
            if (quartier != null)
            {
                if (quartier.NumArrondissement != "") 
                {
                    numArrondissement = "'" + quartier.NumArrondissement + "'";
                }
                quartier.NumQuartier = serviceQuartier.getNumQuartier(sigleAgence);
                this.strCommande = "INSERT INTO `quartier` (`numQuartier`,`quartier`,`numCommune`,`numArrondissement`)";
                this.strCommande += " VALUES ('" + quartier.NumQuartier + "','" + quartier.Quartier + "',";
                this.strCommande += " '" + quartier.NumCommune + "'," + numArrondissement + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numQuartier = quartier.NumQuartier;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numQuartier;
        }

        bool IntfDalQuartier.updateQuartier(crlQuartier quartier)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string numArrondissement = "NULL";
            #endregion

            #region implementation
            if (quartier != null)
            {
                if (quartier.NumArrondissement != "")
                {
                    numArrondissement = "'" + quartier.NumArrondissement + "'";
                }
                this.strCommande = "UPDATE `quartier` SET `quartier`='" + quartier.Quartier + "',";
                this.strCommande += " `numCommune`='" + quartier.NumCommune + "',";
                this.strCommande += " `numArrondissement`=" + numArrondissement + " WHERE";
                this.strCommande += " `numQuartier`='" + quartier.NumQuartier + "'";

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

        string IntfDalQuartier.isQuartier(crlQuartier quartier)
        {
            #region declaration
            string numQuartier = "";
            #endregion

            #region implementation
            if (quartier != null)
            {
                this.strCommande = "SELECT * FROM `quartier` WHERE";
                this.strCommande += " quartier.quartier = '" + quartier.Quartier + "' AND";
                this.strCommande += " quartier.numArrondissement = '" + quartier.NumArrondissement + "' AND";
                this.strCommande += " quartier.numCommune = '" + quartier.NumCommune + "' AND";
                this.strCommande += " quartier.numQuartier <> '" + quartier.NumQuartier + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numQuartier = this.reader["numQuartier"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numQuartier;
        }

        string IntfDalQuartier.getNumQuartier(string quartier, string numCommune, string numArrondissement, string sigleAgence)
        {
            #region declaration
            string numQuartier = "";
            crlQuartier quartierObj = null;
            IntfDalQuartier serviceQuartier = new ImplDalQuartier();
            #endregion

            #region implementation
            if (quartier != null)
            {
                quartierObj = new crlQuartier();
                quartierObj.Quartier = quartier;
                quartierObj.NumArrondissement = numArrondissement;
                quartierObj.NumCommune = numCommune;

                numQuartier = serviceQuartier.isQuartier(quartierObj);
                if (numQuartier.Equals(""))
                {
                    numQuartier = serviceQuartier.insertQuartier(quartierObj, sigleAgence);
                }
            }
            #endregion

            return numQuartier;
        }

        crlQuartier IntfDalQuartier.selectQuartier(string numQuartier)
        {
            #region declaration
            crlQuartier quartier = null;
            #endregion

            #region implementation
            if (numQuartier != "")
            {
                this.strCommande = "SELECT * FROM `quartier` WHERE `numQuartier`='" + numQuartier + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            quartier = new crlQuartier();
                            quartier.NumArrondissement = this.reader["numArrondissement"].ToString();
                            quartier.NumCommune = this.reader["numCommune"].ToString();
                            quartier.NumQuartier = this.reader["numQuartier"].ToString();
                            quartier.Quartier = this.reader["quartier"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return quartier;
        }

        string IntfDalQuartier.getNumQuartier(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numQuartier = "00001";
            string[] tempNumQuartier = null;
            string strDate = "QU" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT quartier.numQuartier AS maxNum FROM quartier";
            this.strCommande += " WHERE quartier.numQuartier LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumQuartier = reader["maxNum"].ToString().ToString().Split('/');
                        numQuartier = tempNumQuartier[tempNumQuartier.Length - 1];
                    }
                    numTemp = double.Parse(numQuartier) + 1;
                    if (numTemp < 10)
                        numQuartier = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numQuartier = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numQuartier = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numQuartier = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numQuartier = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numQuartier = strDate + "/" + sigleAgence + "/" + numQuartier;
            #endregion

            return numQuartier;
        }

        void IntfDalQuartier.loadDdlQuartier(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `quartier`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["quartier"].ToString(), this.reader["numQuartier"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalQuartier.loadDdlQuartierCommune(DropDownList ddl, string numCommune)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `quartier` WHERE `numCommune`='" + numCommune + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["quartier"].ToString(), this.reader["numQuartier"].ToString()));
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
