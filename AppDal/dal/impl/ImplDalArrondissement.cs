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
    /// Description résumée de ImplDalArrondissement
    /// </summary>
    public class ImplDalArrondissement : IntfDalArrondissement
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalArrondissement()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalArrondissement(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalCommune Members

        string IntfDalArrondissement.insertArrondissement(crlArrondissement arrondissement, string sigleAgence)
        {
            #region declaration
            IntfDalArrondissement serviceArrondissement = new ImplDalArrondissement();
            string numArrondissement = "";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (arrondissement != null)
            {
                arrondissement.NumArrondissement = serviceArrondissement.getNumArrondissement(sigleAgence);
                this.strCommande = "INSERT INTO `arrondissement` (`numArrondissement`,`arrondissement`,`numCommune`)";
                this.strCommande += " VALUES ('" + arrondissement.NumArrondissement + "','" + arrondissement.Arrondissement + "',";
                this.strCommande += " '" + arrondissement.NumCommune + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numArrondissement = arrondissement.NumArrondissement;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numArrondissement;
        }

        bool IntfDalArrondissement.updateArrondissement(crlArrondissement arrondissement)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (arrondissement != null)
            {
                this.strCommande = "UPDATE `arrondissement` SET `arrondissement`='" + arrondissement.Arrondissement + "',";
                this.strCommande += " `numCommune`='" + arrondissement.NumCommune + "' WHERE";
                this.strCommande += " `numArrondissement`='" + arrondissement.NumArrondissement + "'";

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

        string IntfDalArrondissement.isArrondissement(crlArrondissement arrondissement)
        {
            #region declaration
            string numCommune = "";
            #endregion

            #region implementation
            if (arrondissement != null)
            {
                this.strCommande = "SELECT * FROM `arrondissement` WHERE";
                this.strCommande += " arrondissement.arrondissement = '" + arrondissement.Arrondissement + "' AND";
                this.strCommande += " arrondissement.numCommune = '" + arrondissement.NumCommune + "' AND";
                this.strCommande += " arrondissement.numArrondissement <> '" + arrondissement.NumArrondissement + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numCommune = this.reader["numArrondissement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCommune;
        }

        crlArrondissement IntfDalArrondissement.selectArrondissement(string numArrondissement)
        {
            #region declaration
            crlArrondissement arrondissement = null;
            #endregion

            #region implementation
            if (numArrondissement != "")
            {
                this.strCommande = "SELECT * FROM `arrondissement` WHERE `numArrondissement`='" + numArrondissement + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            arrondissement = new crlArrondissement();
                            arrondissement.Arrondissement = this.reader["arrondissement"].ToString();
                            arrondissement.NumCommune = this.reader["numCommune"].ToString();
                            arrondissement.NumArrondissement = this.reader["numArrondissement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return arrondissement;
        }

        string IntfDalArrondissement.getNumArrondissement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numArrondissement = "00001";
            string[] tempNumArrondissement = null;
            string strDate = "AR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT arrondissement.numArrondissement AS maxNum FROM arrondissement";
            this.strCommande += " WHERE arrondissement.numArrondissement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumArrondissement = reader["maxNum"].ToString().ToString().Split('/');
                        numArrondissement = tempNumArrondissement[tempNumArrondissement.Length - 1];
                    }
                    numTemp = double.Parse(numArrondissement) + 1;
                    if (numTemp < 10)
                        numArrondissement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numArrondissement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numArrondissement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numArrondissement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numArrondissement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numArrondissement = strDate + "/" + sigleAgence + "/" + numArrondissement;
            #endregion

            return numArrondissement;
        }

        void IntfDalArrondissement.loadDddlArrondissement(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `arrondissement`";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["arrondissement"].ToString(), this.reader["numArrondissement"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion
        }

        void IntfDalArrondissement.loadDddlArrondissementCommune(DropDownList ddl, string numCommune)
        {
            #region implementation
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");
                this.strCommande = "SELECT * FROM `arrondissement` WHERE `numCommune`='" + numCommune + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["arrondissement"].ToString(), this.reader["numArrondissement"].ToString()));
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