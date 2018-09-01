using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Data;
using System.Web.UI.WebControls;


namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service cheque
    /// </summary>
    public class ImplDalCheque : IntfDalCheque
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCheque()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCheque(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlCheque IntfDalCheque.selectCheque(string numCheque)
        {
            #region declaration
            crlCheque cheque = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numCheque != "")
            {
                this.strCommande = "SELECT * FROM `cheque` WHERE (`numCheque`='" + numCheque + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            cheque = new crlCheque();
                            cheque.NumCheque = this.reader["numCheque"].ToString();
                            cheque.NumerosCheque = this.reader["numerosCheque"].ToString();
                            try
                            {
                                cheque.DateCheque = Convert.ToDateTime(this.reader["dateCheque"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                cheque.MontantCheque = double.Parse(this.reader["montantCheque"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            cheque.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            cheque.AdresseTitulaireCheque = this.reader["adresseTitulaireCheque"].ToString();
                            cheque.Banque = this.reader["banque"].ToString();
                            cheque.NumCompte = this.reader["numCompte"].ToString();
                            cheque.TitulaireCheque = this.reader["titulaireCheque"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (cheque != null)
                {
                    if (cheque.MatriculeAgent != "")
                    {
                        cheque.agent = serviceAgent.selectAgent(cheque.MatriculeAgent);
                    }
                }
                
            }
            #endregion

            return cheque;
        }

        string IntfDalCheque.insertCheque(crlCheque cheque, string sigleAgence)
        {
            #region declaration
            string numCheque = "";
            int nombreInsert = 0;
            IntfDalCheque serviceCheque = new ImplDalCheque();
            #endregion

            #region implementation
            if (cheque != null)
            {

                if (sigleAgence != "")
                {

                    cheque.NumCheque = serviceCheque.getNumCheque(sigleAgence);
                    this.strCommande = "INSERT INTO `cheque` (`numCheque`,`dateCheque`,`banque`,`numerosCheque`,";
                    this.strCommande += " `montantCheque`,`matriculeAgent`,`numCompte`,`titulaireCheque`,`adresseTitulaireCheque`)";
                    this.strCommande += " VALUES ('" + cheque.NumCheque + "','" + cheque.DateCheque.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " '" + cheque.Banque + "','" + cheque.NumerosCheque + "','" + cheque.MontantCheque + "',";
                    this.strCommande += " '" + cheque.MatriculeAgent + "','" + cheque.NumCompte + "','" + cheque.TitulaireCheque + "',";
                    this.strCommande += " '" + cheque.AdresseTitulaireCheque + "')";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numCheque = cheque.NumCheque;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numCheque;
        }

        bool IntfDalCheque.updateCheque(crlCheque cheque)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (cheque != null)
            {


                this.strCommande = "UPDATE `cheque` SET `dateCheque`='" + cheque.DateCheque.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `banque`='" + cheque.Banque + "', `numerosCheque`='" + cheque.NumerosCheque + "',";
                this.strCommande += " `montantCheque`='" + cheque.MontantCheque + "',`matriculeAgent`='" + cheque.MatriculeAgent + "',";
                this.strCommande += " `numCompte`='" + cheque.NumCompte + "',`titulaireCheque`='" + cheque.TitulaireCheque + "',";
                this.strCommande += " `adresseTitulaireCheque`='" + cheque.AdresseTitulaireCheque + "'";
                this.strCommande += " WHERE `numCheque`='" + cheque.NumCheque + "'";

                this.serviceConnectBase.openConnection();
                nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nombreUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        crlCheque IntfDalCheque.isChequeCredit(string numCheque)
        {
            #region declaration
            crlCheque cheque = null;
            #endregion

            #region implementation
            if (numCheque != "")
            {
                this.strCommande = "SELECT cheque.numCheque, cheque.banque, cheque.numerosCheque,";
                this.strCommande += " cheque.dateCheque, cheque.montantCheque, cheque.matriculeAgent, cheque.numCompte,";
                this.strCommande += " cheque.titulaireCheque, cheque.adresseTitulaireCheque FROM cheque";
                this.strCommande += " Left Join recuabonnement ON recuabonnement.numCheque = cheque.numCheque";
                this.strCommande += " WHERE recuabonnement.numCheque IS NULL  AND";
                this.strCommande += " cheque.numCheque = '" + numCheque + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            cheque = new crlCheque();
                            try
                            {
                                cheque.DateCheque = Convert.ToDateTime(this.reader["dateCheque"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            cheque.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            cheque.MontantCheque = double.Parse(this.reader["montantCheque"].ToString());
                            cheque.NumCheque = this.reader["numCheque"].ToString();
                            cheque.NumerosCheque = this.reader["numerosCheque"].ToString();
                            cheque.Banque = this.reader["banque"].ToString();
                            cheque.NumCompte = this.reader["numCompte"].ToString();
                            cheque.TitulaireCheque = this.reader["titulaireCheque"].ToString();
                            cheque.AdresseTitulaireCheque = this.reader["adresseTitulaireCheque"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return cheque;
        }

        string IntfDalCheque.getNumCheque(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCheque = "00001";
            string[] tempNumCheque = null;
            string strDate = "CH" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT cheque.numCheque AS maxNum FROM cheque";
            this.strCommande += " WHERE cheque.numCheque LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCheque = reader["maxNum"].ToString().ToString().Split('/');
                        numCheque = tempNumCheque[tempNumCheque.Length - 1];
                    }
                    numTemp = double.Parse(numCheque) + 1;
                    if (numTemp < 10)
                        numCheque = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCheque = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCheque = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCheque = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCheque = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numCheque = strDate + "/" + sigleAgence + "/" + numCheque;
            #endregion

            return numCheque;
        }
        #endregion

        #region insert to grid
        void IntfDalCheque.insertToGridCheque(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalCheque serviceCheque = new ImplDalCheque();
            #endregion

            #region implementation

            this.strCommande = "SELECT cheque.numCheque, cheque.banque, cheque.numerosCheque,";
            this.strCommande += " cheque.dateCheque, cheque.montantCheque, cheque.matriculeAgent, cheque.numCompte,";
            this.strCommande += " cheque.titulaireCheque, cheque.adresseTitulaireCheque FROM cheque";
            this.strCommande += "  Left Join recuabonnement ON recuabonnement.numCheque = cheque.numCheque";
            this.strCommande += " WHERE recuabonnement.numCheque IS NULL AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCheque.getDataTableCheque(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCheque.getDataTableCheque(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numCheque", typeof(string));
            dataTable.Columns.Add("numerosCheque", typeof(string));
            dataTable.Columns.Add("banque", typeof(string));
            dataTable.Columns.Add("dateCheque", typeof(DateTime));
            dataTable.Columns.Add("montantCheque", typeof(string));
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

                        dr["numCheque"] = reader["numCheque"].ToString();
                        dr["numerosCheque"] = reader["numerosCheque"].ToString();
                        dr["banque"] = reader["banque"].ToString();
                        try
                        {
                            dr["dateCheque"] = Convert.ToDateTime(this.reader["dateCheque"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dr["montantCheque"] = serviceGeneral.separateurDesMilles(this.reader["montantCheque"].ToString()) + "Ar";

                        
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