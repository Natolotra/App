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
    /// Description résumée de ImplDalCooperative
    /// </summary>
    public class ImplDalCooperative : IntfDalCooperative
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalCooperative(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalCooperative()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        string IntfDalCooperative.insertCooperative(crlCooperative cooperative, string sigleAgence)
        {
            #region decalration
            string numCooperative = "";
            int nombreInsert = 0;
            IntfDalCooperative serviceCooperative = new ImplDalCooperative(); 
            #endregion

            #region implementation
            if (cooperative != null && sigleAgence != "")
            {
                cooperative.NumCooperative = serviceCooperative.getNumCooperative(sigleAgence);
                this.strCommande = "INSERT INTO `cooperative` (`numCooperative`, `nomCooperative`,";
                this.strCommande += " `sigleCooperative`,`adresseCooperative`, `zone`,`telephoneFixeCooperative`,";
                this.strCommande += " `telephoneMobileCooperative`,`numVille`,`nomResponsable`,`prenomResponsable`,";
                this.strCommande += " `cinResponsable`,`adressseResponsable`,`telephoneFixeResponsable`,";
                this.strCommande += " `telephoneMobileResponsable`) VALUES";
                this.strCommande += " ('" + cooperative.NumCooperative + "','" + cooperative.NomCooperative + "',";
                this.strCommande += " '" + cooperative.SigleCooperative + "','" + cooperative.AdresseCooperative + "',";
                this.strCommande += " '" + cooperative.Zone + "','" + cooperative.TelephoneFixeCooperative + "',";
                this.strCommande += " '" + cooperative.TelephoneMobileCooperative + "','" + cooperative.NumVille + "',";
                this.strCommande += " '" + cooperative.NomResponsable + "','" + cooperative.PrenomResponsable + "',";
                this.strCommande += " '" + cooperative.CinResponsable + "','" + cooperative.AdressseResponsable + "',";
                this.strCommande += " '" + cooperative.TelephoneFixeResponsable + "','" + cooperative.TelephoneMobileResponsable + "')";

                this.serviceConnection.openConnection();
                nombreInsert = this.serviceConnection.requete(this.strCommande);
                if (nombreInsert == 1)
                {
                    numCooperative = cooperative.NumCooperative;
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return numCooperative;
        }

        string IntfDalCooperative.isCooperative(crlCooperative cooperative)
        {
            #region declaration
            string numCooperative = "";
            #endregion

            #region implementation
            if (cooperative != null)
            {
                this.strCommande = "SELECT * FROM `cooperative` WHERE `numCooperative`<>'" + cooperative.NumCooperative + "' AND";
                this.strCommande += " `nomCooperative`='" + cooperative.NomCooperative + "' AND `nomResponsable`='" + cooperative.NomResponsable + "' AND";
                this.strCommande += " `prenomResponsable`='" + cooperative.PrenomResponsable + "' AND `cinResponsable`='" + cooperative.CinResponsable + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numCooperative = this.reader["numCooperative"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();

            }
            #endregion

            return numCooperative;
        }

        bool IntfDalCooperative.updateCooperative(crlCooperative cooperative)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (cooperative != null)
            {


                this.strCommande = "UPDATE `cooperative` SET `nomCooperative`='" + cooperative.NomCooperative + "',";
                this.strCommande += " `sigleCooperative`='" + cooperative.SigleCooperative + "',";
                this.strCommande += " `adresseCooperative`='" + cooperative.AdresseCooperative + "',";
                this.strCommande += " `zone`='" + cooperative.Zone + "',`telephoneFixeCooperative`='" + cooperative.TelephoneFixeCooperative + "',";
                this.strCommande += " `telephoneMobileCooperative`='" + cooperative.TelephoneMobileCooperative + "',";
                this.strCommande += " `numVille`='" + cooperative.NumVille + "',`nomResponsable`='" + cooperative.NomResponsable + "',";
                this.strCommande += " `prenomResponsable`='" + cooperative.PrenomResponsable + "',`cinResponsable`='" + cooperative.CinResponsable + "',";
                this.strCommande += " `adressseResponsable`='" + cooperative.AdressseResponsable + "',`telephoneFixeResponsable`='" + cooperative.TelephoneFixeResponsable + "',";
                this.strCommande += " `telephoneMobileResponsable`='" + cooperative.TelephoneMobileResponsable + "'";
                this.strCommande += " WHERE `numCooperative`='" + cooperative.NumCooperative + "'";

                this.serviceConnection.openConnection();
                nombreUpdate = this.serviceConnection.requete(this.strCommande);
                if (nombreUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalCooperative.getNumCooperative(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCooperative = "00001";
            string[] tempNumCooperative = null;
            string strDate = "CO" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT cooperative.numCooperative AS maxNum FROM cooperative";
            this.strCommande += " WHERE cooperative.numCooperative LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCooperative = reader["maxNum"].ToString().ToString().Split('/');
                        numCooperative = tempNumCooperative[tempNumCooperative.Length - 1];
                    }
                    numTemp = double.Parse(numCooperative) + 1;
                    if (numTemp < 10)
                        numCooperative = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCooperative = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCooperative = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCooperative = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCooperative = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            numCooperative = strDate + "/" + sigleAgence + "/" + numCooperative;
            #endregion

            return numCooperative;
        }

        crlCooperative IntfDalCooperative.selectCooperative(string numCooperative)
        {
            #region declaration
            crlCooperative cooperative = null;
            #endregion

            #region implementation
            if (numCooperative != "")
            {
                this.strCommande = "SELECT * FROM `cooperative` WHERE (`numCooperative`='" + numCooperative + "')";

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
                                cooperative = new crlCooperative();
                                cooperative.NumCooperative = this.reader["numCooperative"].ToString();
                                cooperative.AdresseCooperative = this.reader["adresseCooperative"].ToString();
                                cooperative.NomCooperative = this.reader["nomCooperative"].ToString();
                                cooperative.SigleCooperative = this.reader["sigleCooperative"].ToString();
                                cooperative.Zone = this.reader["zone"].ToString();
                                cooperative.TelephoneFixeCooperative = this.reader["telephoneFixeCooperative"].ToString();
                                cooperative.TelephoneMobileCooperative = this.reader["telephoneMobileCooperative"].ToString();
                                cooperative.NumVille = this.reader["numVille"].ToString();
                                cooperative.NomResponsable = this.reader["nomResponsable"].ToString();
                                cooperative.PrenomResponsable = this.reader["prenomResponsable"].ToString();
                                cooperative.CinResponsable = this.reader["cinResponsable"].ToString();
                                cooperative.AdresseCooperative = this.reader["adresseCooperative"].ToString();
                                cooperative.TelephoneFixeResponsable = this.reader["telephoneFixeResponsable"].ToString();
                                cooperative.TelephoneMobileResponsable = this.reader["telephoneMobileResponsable"].ToString();
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

            }
            #endregion

            return cooperative;
        }

        void IntfDalCooperative.loadDdlCooperative(DropDownList ddlCooperative)
        {
            #region implementation
            if (ddlCooperative != null)
            {
                this.strCommande = "SELECT * FROM `cooperative`";
                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddlCooperative.Items.Clear();
                        ddlCooperative.Items.Add("");
                        while (this.reader.Read())
                        {
                            ddlCooperative.Items.Add(new ListItem(this.reader["nomCooperative"].ToString(), this.reader["numCooperative"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion
        }

        void IntfDalCooperative.loadDdlCooperativeZoneUS(DropDownList ddlCooperative)
        {
            #region implementation
            if (ddlCooperative != null)
            {
                this.strCommande = "SELECT * FROM `cooperative` WHERE";
                this.strCommande += " cooperative.zone='Urbaine' OR cooperative.zone='Suburbaine'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        ddlCooperative.Items.Clear();
                        ddlCooperative.Items.Add("");
                        while (this.reader.Read())
                        {
                            ddlCooperative.Items.Add(new ListItem(this.reader["nomCooperative"].ToString(), this.reader["numCooperative"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion
        }
        #endregion

        #region insertToGrid
        void IntfDalCooperative.insertToGridCooperative(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalCooperative serviceCooperative = new ImplDalCooperative();
            #endregion

            #region implementation
            this.strCommande = "SELECT cooperative.numCooperative, cooperative.nomCooperative,";
            this.strCommande += " cooperative.sigleCooperative,cooperative.adresseCooperative,";
            this.strCommande += " cooperative.zone, cooperative.prenomResponsable,";
            this.strCommande += " cooperative.nomResponsable FROM cooperative";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCooperative.getDataTableCooperative(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCooperative.getDataTableCooperative(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numCooperative", typeof(string));
            dataTable.Columns.Add("nomCooperative", typeof(string));
            dataTable.Columns.Add("sigleCooperative", typeof(string));
            dataTable.Columns.Add("adresseCooperative", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("responsable", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numCooperative"] = reader["numCooperative"].ToString();
                        dr["nomCooperative"] = reader["nomCooperative"].ToString();
                        dr["sigleCooperative"] = reader["sigleCooperative"].ToString();

                        dr["adresseCooperative"] = this.reader["adresseCooperative"].ToString();
                        dr["zone"] = this.reader["zone"].ToString();
                        dr["responsable"] = this.reader["prenomResponsable"].ToString() + " " + this.reader["nomResponsable"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion


        
    }
}