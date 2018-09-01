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
    /// Implementation du service client
    /// </summary>
    public class ImplDalClient : IntfDalClient
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalClient()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalClient(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalPassager Members

        string IntfDalClient.insertClient(crlClient Client, string sigleAgence)
        {
            #region declaration
            IntfDalClient serviceClient = new ImplDalClient();
            int nombreInsertion = 0;
            string numClient = "";
            #endregion

            #region implementation
            if (Client != null)
            {
                Client.NumClient = serviceClient.getNumClient(sigleAgence);
                this.strCommande = "INSERT INTO `client` (`numClient`,`nomClient`";
                this.strCommande += ",`prenomClient`,`adresseClient`,`cinClient`,`telephoneClient`,`mobileClient`";
                this.strCommande += ",`isCheque`,`isBonCommande`)";
                this.strCommande += " VALUES ('" + Client.NumClient + "','" + Client.NomClient + "'";
                this.strCommande += ",'" + Client.PrenomClient + "','" + Client.AdresseClient + "'";
                this.strCommande += ",'" + Client.CinClient + "','" + Client.TelephoneClient + "','" + Client.MobileClient + "'";
                this.strCommande += ",'" + Client.IsCheque + "','" + Client.IsBonCommande + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numClient = Client.NumClient;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numClient;
        }

        bool IntfDalClient.deleteClient(crlClient Client)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Client != null)
            {
                if (Client.NumClient != "")
                {
                    this.strCommande = "DELETE FROM `client` WHERE (`numClient` = '" + Client.NumClient + "')";
                    this.serviceConnectBase.openConnection();
                    nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreDelete == 1)
                        isDelete = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isDelete;
        }

        bool IntfDalClient.deleteClient(string numClient)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numClient != "")
            {
                this.strCommande = "DELETE FROM `client` WHERE (`numClient` = '" + numClient + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalClient.updateClient(crlClient Client)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Client != null)
            {
                if (Client.NumClient != "")
                {
                    this.strCommande = "UPDATE `client` SET `nomClient`='" + Client.NomClient + "', `prenomClient`='" + Client.PrenomClient + "', ";
                    this.strCommande += "`adresseClient`='" + Client.AdresseClient + "', `cinClient`='" + Client.CinClient + "', `telephoneClient`='" + Client.TelephoneClient + "', ";
                    this.strCommande += " `mobileClient`='" + Client.MobileClient + "', `isCheque`='" + Client.IsCheque + "', `isBonCommande`='" + Client.IsBonCommande + "'";
                    this.strCommande += " WHERE (`numClient`='" + Client.NumClient + "')";

                    this.serviceConnectBase.openConnection();
                    nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreUpdate == 1)
                        isUpdate = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        string IntfDalClient.isClient(crlClient Client)
        {
            #region declaration
            string numClient = "";
            #endregion

            #region implementation
            if (Client != null)
            {
                if (Client.CinClient != "")
                {
                    this.strCommande = "SELECT * FROM `client` WHERE (`cinClient`='" + Client.CinClient + "' AND";
                    this.strCommande += " `nomClient`='" + Client.NomClient + "' AND `prenomClient`='" + Client.PrenomClient + "' AND";
                    this.strCommande += " `numClient` <> '" + Client.NumClient + "')";
                }
                else
                {
                    this.strCommande = "SELECT * FROM `client` WHERE (";
                    this.strCommande += " `nomClient`='" + Client.NomClient + "' AND `prenomClient`='" + Client.PrenomClient + "' AND";
                    this.strCommande += " `numClient` <> '" + Client.NumClient + "')";
                }

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            numClient = this.reader["numClient"].ToString();
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numClient;
        }

        int IntfDalClient.isClientInt(crlClient Client)
        {
            #region declaration
            int isClient = 0;
            #endregion

            #region implementation
            if (Client != null)
            {
                if (Client.CinClient != "")
                {
                    this.strCommande = "SELECT * FROM `client` WHERE (`cinClient`='" + Client.CinClient + "' AND";
                    this.strCommande += " `nomClient`='" + Client.NomClient + "' AND `prenomClient`='" + Client.PrenomClient + "' AND";
                    this.strCommande += " `numClient` <> '" + Client.NumClient + "')";
                }
                else
                {
                    this.strCommande = "SELECT * FROM `client` WHERE (";
                    this.strCommande += " `nomClient`='" + Client.NomClient + "' AND `prenomClient`='" + Client.PrenomClient + "' AND";
                    this.strCommande += " `numClient` <> '" + Client.NumClient + "')";
                }

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            isClient = 1;
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isClient;
        }

        crlClient IntfDalClient.selectClient(string numClient)
        {
            #region declaration
            crlClient Client = null;
            #endregion

            #region implementation
            if (numClient != "")
            {
                this.strCommande = "SELECT * FROM `client` WHERE (`numClient` = '" + numClient + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Client = new crlClient();
                        reader.Read();
                        Client.NumClient = reader["numClient"].ToString();
                        Client.NomClient = reader["nomClient"].ToString();
                        Client.PrenomClient = reader["prenomClient"].ToString();
                        Client.CinClient = reader["cinClient"].ToString();
                        Client.AdresseClient = reader["adresseClient"].ToString();
                        Client.TelephoneClient = reader["telephoneClient"].ToString();
                        Client.MobileClient = reader["mobileClient"].ToString();
                        try
                        {
                            Client.IsCheque = int.Parse(reader["isCheque"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            Client.IsBonCommande = int.Parse(reader["isBonCommande"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return Client;
        }

        string IntfDalClient.getNumClient(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numClient = "00001";
            string[] tempNumClient = null;
            string strDate = "CL" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT client.numClient AS maxNum FROM client";
            this.strCommande += " WHERE client.numClient LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumClient = reader["maxNum"].ToString().ToString().Split('/');
                        numClient = tempNumClient[tempNumClient.Length - 1];
                    }
                    numTemp = double.Parse(numClient) + 1;
                    if (numTemp < 10)
                        numClient = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numClient = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numClient = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numClient = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numClient = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numClient = strDate + "/" + sigleAgence + "/" + numClient;
            #endregion

            return numClient;
        }

        int IntfDalClient.isBonCommande(string numClient)
        {
            #region declaration
            int isBonCommande = 0;
            #endregion

            #region implementation
            if (numClient != "")
            {
                this.strCommande = "SELECT Count(bondecommande.numBonDeCommande) AS nbCommande FROM bondecommande";
                this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numBonDeCommande = bondecommande.numBonDeCommande";
                this.strCommande += " Inner Join proforma ON proforma.numProforma = bondecommande.numProforma";
                this.strCommande += " Inner Join client ON client.numClient = proforma.numClient";
                this.strCommande += " WHERE assocrecuencaisserproformabondecommande.numBonDeCommande IS NULL AND";
                this.strCommande += " client.numClient = '" + numClient + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                isBonCommande = int.Parse(this.reader["nbCommande"].ToString());
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

            return isBonCommande;
        }

        #endregion

        #region insert to grid
        void IntfDalClient.insertToGridClient(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalClient serviceClient = new ImplDalClient();
            #endregion

            #region implementation
            this.strCommande  = "SELECT client.numClient, client.nomClient, client.prenomClient, client.adresseClient,";
            this.strCommande += " client.cinClient, client.telephoneClient, client.mobileClient FROM client";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceClient.getDataTableClient(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalClient.getDataTableClient(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implemntation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numClient", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("cin", typeof(string));
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

                        dr["numClient"] = this.reader["numClient"].ToString();
                        dr["client"] = this.reader["prenomClient"].ToString() + " " + this.reader["nomClient"].ToString();
                        dr["adresse"] = this.reader["adresseClient"].ToString();
                        dr["contact"] = this.reader["telephoneClient"].ToString() + " / " + this.reader["mobileClient"].ToString();
                        dr["cin"] = this.reader["cinClient"].ToString();

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