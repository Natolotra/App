using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalAgence
    /// </summary>
    public class ImplDalAgence : IntfDalAgence
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalAgence(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalAgence()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region methode
        string IntfDalAgence.insertAgence(crlAgence agence)
        {
            #region declaration
            string numAgence = "";
            IntfDalAgence serviceAgence = new ImplDalAgence();
            int nbInsert = 0;
            #endregion

            #region implementation
            if (agence != null)
            {
                agence.NumAgence = serviceAgence.getNumAgence(agence.SigleAgence);

                this.strCommande = "INSERT INTO `agence` (`numAgence`,`typeAgence`,`numVille`,`sigleAgence`,";
                this.strCommande += "`nomAgence`,`localisationAgence`) VALUES ('" + agence.NumAgence + "',";
                this.strCommande += "'" + agence.TypeAgence + "','" + agence.NumVille + "','" + agence.SigleAgence + "',";
                this.strCommande += "'" + agence.NomAgence + "','" + agence.LocalisationAgence + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numAgence = agence.NumAgence;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAgence;
        }

        bool IntfDalAgence.updateAgence(crlAgence agence)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implemenation
            if (agence != null)
            {
                this.strCommande = "UPDATE `agence` SET `typeAgence`='" + agence.TypeAgence + "',`numVille`='" + agence.NumVille + "',";
                this.strCommande += "`sigleAgence`='" + agence.SigleAgence + "',`nomAgence`='" + agence.NomAgence + "',";
                this.strCommande += "`localisationAgence`='" + agence.LocalisationAgence + "' WHERE `numAgence`='" + agence.NumAgence + "'";

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

        int IntfDalAgence.isAgence(crlAgence agence)
        {
            #region declaration
            int isAgence = 0;
            #endregion

            #region implementation
            if (agence != null)
            {
                this.strCommande = "SELECT agence.numAgence, agence.typeAgence, agence.numVille,";
                this.strCommande += " agence.sigleAgence, agence.nomAgence, agence.localisationAgence";
                this.strCommande += " FROM agence WHERE (agence.sigleAgence = '" + agence.SigleAgence + "' OR";
                this.strCommande += " agence.nomAgence = '" + agence.NomAgence + "') AND";
                this.strCommande += " agence.numAgence <> '" + agence.NumAgence + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            if (this.reader["sigleAgence"].ToString().Trim().ToUpper().Equals(agence.SigleAgence.Trim().ToUpper()))
                            {
                                isAgence = 1;
                            }
                            if (this.reader["nomAgence"].ToString().Trim().ToLower().Equals(agence.NomAgence.Trim().ToLower()))
                            {
                                isAgence = 2;
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            /*****************/
            /* 1 si sigle egale   */
            /* 2 nom agence     */
            /* ***************/

            return isAgence;
        }

        string IntfDalAgence.getNumAgence(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAgence = "00001";
            string[] tempNumAgence = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT agence.numAgence AS maxNum FROM agence";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAgence = reader["maxNum"].ToString().ToString().Split('/');
                        numAgence = tempNumAgence[0];
                    }
                    numTemp = double.Parse(numAgence) + 1;
                    if (numTemp < 10)
                        numAgence = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAgence = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAgence = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAgence = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAgence = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAgence = numAgence + "/" + sigleAgence;
            #endregion

            return numAgence;
        }

        crlAgence IntfDalAgence.selectAgence(string numAgence)
        {
            #region declaration
            crlAgence agence = null;
            IntfDalTypeAgence serviceTypeAgence = new ImplDalTypeAgence();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalSessionAgence serviceSessionAgence = new  ImplDalSessionAgence();
            #endregion

            #region implementation
            if (numAgence != "")
            {
                this.strCommande = "SELECT * FROM agence WHERE `numagence`='" + numAgence + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            agence = new crlAgence();

                            agence.NumAgence = this.reader["numAgence"].ToString();
                            agence.NumVille = this.reader["numVille"].ToString();
                            agence.NomAgence = this.reader["nomAgence"].ToString();
                            agence.SigleAgence = this.reader["sigleAgence"].ToString();
                            agence.TypeAgence = this.reader["typeAgence"].ToString();
                            agence.LocalisationAgence = this.reader["localisationAgence"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (agence != null)
                {

                    if (agence.TypeAgence != "")
                    {
                        agence.typeAgenceObj = serviceTypeAgence.selectTypeAgence(agence.TypeAgence);
                    }

                    if (agence.NumVille != "") 
                    {
                        agence.ville = serviceVille.selectVille(agence.NumVille);
                    }

                    if (agence.NumAgence != "")
                    {
                        agence.sessionAgence = serviceSessionAgence.getSessionAgenceEncours(agence.NumAgence);
                    }
                }
            }
            #endregion

            return agence;
        }

        void IntfDalAgence.loadDdlAgence(DropDownList ddl)
        {
            #region implementation
            if (ddl != null)
            {
                this.strCommande = "SELECT * FROM `agence`";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    ddl.Items.Clear();
                    if (this.reader.HasRows)
                    {
                        ddl.Items.Add("");
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["nomAgence"].ToString(), this.reader["numAgence"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

        }
        #endregion

        #region insert to grid
        void IntfDalAgence.insertToGridAgenceListe(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalAgence serviceAgence = new ImplDalAgence();
            #endregion

            #region implementation
            this.strCommande = "SELECT agence.numAgence, agence.typeAgence, agence.numVille,";
            this.strCommande += " agence.sigleAgence, agence.nomAgence, agence.localisationAgence";
            this.strCommande += " FROM agence WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " agence.numAgence <>  '" + numAgence + "'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAgence.getDataTableAgenceListe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        void IntfDalAgence.insertToGridAgenceListe(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalAgence serviceAgence = new ImplDalAgence();
            #endregion

            #region implementation
            this.strCommande = "SELECT agence.numAgence, agence.typeAgence, agence.numVille,";
            this.strCommande += " agence.sigleAgence, agence.nomAgence, agence.localisationAgence";
            this.strCommande += " FROM agence WHERE";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceAgence.getDataTableAgenceListe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalAgence.getDataTableAgenceListe(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlAgence agence = null;

            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalAgence serviceAgence = new ImplDalAgence();
            crlVille ville = null;
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAgence", typeof(string));
            dataTable.Columns.Add("villeAgence", typeof(string));
            dataTable.Columns.Add("nomAgence", typeof(string));
            dataTable.Columns.Add("localisation", typeof(string));
            dataTable.Columns.Add("typeAgence", typeof(string));
            dataTable.Columns.Add("sigleAgence", typeof(string));
            dataTable.Columns.Add("statut", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);

            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        agence = serviceAgence.selectAgence(this.reader["numAgence"].ToString());

                        dr["numAgence"] = this.reader["numAgence"].ToString();

                        ville = serviceVille.selectVille(this.reader["numVille"].ToString());
                        if (ville != null)
                        {
                            dr["villeAgence"] = ville.NomVille;
                        }
                        else
                        {
                            dr["villeAgence"] = this.reader["numVille"].ToString();
                        }

                        dr["nomAgence"] = this.reader["nomAgence"].ToString();
                        dr["sigleAgence"] = this.reader["sigleAgence"].ToString();
                        dr["localisation"] = this.reader["localisationAgence"].ToString();
                        dr["typeAgence"] = this.reader["typeAgence"].ToString();

                        if (agence.sessionAgence != null)
                        {
                            dr["statut"] = "vert16.png";
                        }
                        else
                        {
                            dr["statut"] = "rouge16.png";
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }

            this.serviceConnectBase.closeConnection();


            #endregion

            return dataTable;
        }
        #endregion


        
    }
}