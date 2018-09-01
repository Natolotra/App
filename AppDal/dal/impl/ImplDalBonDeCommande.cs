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
    /// Description résumée de ImplDalBonDeCommande
    /// </summary>
    public class ImplDalBonDeCommande : IntfDalBonDeCommande
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalBonDeCommande()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalBonDeCommande(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode 
        crlBonDeCommande IntfDalBonDeCommande.selectBonDeCommande(string numBonDeCommande)
        {
            #region declaration
            crlBonDeCommande bonDeCommande = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation
            if (numBonDeCommande != "")
            {
                this.strCommande = "SELECT * FROM `bondecommande` WHERE (`numBonDeCommande`='" + numBonDeCommande + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            bonDeCommande = new crlBonDeCommande();
                            bonDeCommande.NumBonDeCommande = this.reader["numBonDeCommande"].ToString();
                            try
                            {
                                bonDeCommande.DateBC = Convert.ToDateTime(this.reader["dateBC"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                bonDeCommande.DatePaiementBC = Convert.ToDateTime(this.reader["datePaiementBC"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            bonDeCommande.DescriptionBC = this.reader["descriptionBC"].ToString();
                            bonDeCommande.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            try
                            {
                                bonDeCommande.MontantBC = double.Parse(this.reader["montantBC"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            bonDeCommande.NumProforma = this.reader["numProforma"].ToString();
                            
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (bonDeCommande != null)
                {
                    if (bonDeCommande.NumProforma != "")
                    {
                        bonDeCommande.proforma = serviceProforma.selectProforma(bonDeCommande.NumProforma);
                    }
                    if (bonDeCommande.MatriculeAgent != "")
                    {
                        bonDeCommande.agent = serviceAgent.selectAgent(bonDeCommande.MatriculeAgent);
                    }
                }


            }
            #endregion

            return bonDeCommande;
        }

        string IntfDalBonDeCommande.insertBonDeCommande(crlBonDeCommande bonDeCommande, string sigleAgence)
        {
            #region declaration
            string numBonDeCommande = "";
            int nombreInsert = 0;
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();

            string numProforma = "";
            #endregion

            #region implementation
            if (bonDeCommande != null)
            {

                if (sigleAgence != "")
                {
                    
                    if (bonDeCommande.NumProforma != "")
                    {
                        numProforma = "'" + bonDeCommande.NumProforma + "'";
                    }
                    else
                    {
                        numProforma = "NULL";
                    }

                    bonDeCommande.NumBonDeCommande = serviceBonDeCommande.getNumBonDeCommande(sigleAgence);
                    this.strCommande = "INSERT INTO `bondecommande` (`numBonDeCommande`,`matriculeAgent`,`descriptionBC`,";
                    this.strCommande += " `dateBC`,`datePaiementBC`,`montantBC`,`numProforma`)";
                    this.strCommande += " VALUES ('" + bonDeCommande.NumBonDeCommande + "','" + bonDeCommande.MatriculeAgent + "',";
                    this.strCommande += " '" + bonDeCommande.DescriptionBC + "',";
                    this.strCommande += " '" + bonDeCommande.DateBC.ToString("yyyy-MM-dd") + "','" + bonDeCommande.DatePaiementBC.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " '" + bonDeCommande.MontantBC + "'," + numProforma + ")";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numBonDeCommande = bonDeCommande.NumBonDeCommande;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numBonDeCommande;
        }

        bool IntfDalBonDeCommande.updateBonDeCommande(crlBonDeCommande bonDeCommande)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numProforma = "";
            #endregion

            #region implementation
            if (bonDeCommande != null)
            {
                if (bonDeCommande.NumProforma != "")
                {
                    numProforma = "'" + bonDeCommande.NumProforma + "'";
                }
                else
                {
                    numProforma = "NULL";
                }
                


                this.strCommande = "UPDATE `bondecommande` SET `dateBC`='" + bonDeCommande.DateBC.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `datePaiementBC`='" + bonDeCommande.DatePaiementBC.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `descriptionBC`='" + bonDeCommande.DescriptionBC + "', `matriculeAgent`='" + bonDeCommande.MatriculeAgent + "',";
                this.strCommande += " `montantBC`='" + bonDeCommande.MontantBC + "',";
                this.strCommande += " `numProforma`=" + numProforma + "";
                this.strCommande += " WHERE `numBonDeCommande`='" + bonDeCommande.NumBonDeCommande + "'";

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


        /// <summary>
        /// mbola ts ai oe atao inona
        /// </summary>
        /// <param name="numBonDeCommande"></param>
        /// <returns></returns>
        crlBonDeCommande IntfDalBonDeCommande.isBonDeCommandeCredit(string numBonDeCommande)
        {
            #region declaration
            crlBonDeCommande bonDeCommande = null;
            #endregion

            #region implementation
            if (numBonDeCommande != "")
            {
                this.strCommande = "SELECT bondecommande.numBonDeCommande, bondecommande.matriculeAgent, bondecommande.descriptionBC,";
                this.strCommande += " bondecommande.numerosBC, bondecommande.dateBC, bondecommande.datePaiementBC,";
                this.strCommande += " bondecommande.montantBC FROM bondecommande";
                this.strCommande += " Left Join recuabonnement ON recuabonnement.numBonDeCommande = bondecommande.numBonDeCommande";
                this.strCommande += " WHERE recuabonnement.numBonDeCommande IS NULL  AND";
                this.strCommande += " bondecommande.numBonDeCommande = '" + numBonDeCommande + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            bonDeCommande = new crlBonDeCommande();
                            try
                            {
                                bonDeCommande.DateBC = Convert.ToDateTime(this.reader["dateBC "].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                bonDeCommande.DatePaiementBC = Convert.ToDateTime(this.reader["datePaiementBC"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            bonDeCommande.DescriptionBC = this.reader["descriptionBC"].ToString();
                            bonDeCommande.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            try
                            {
                                bonDeCommande.MontantBC = double.Parse(this.reader["montantBC"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            bonDeCommande.NumBonDeCommande = this.reader["numBonDeCommande"].ToString();
                            //bonDeCommande.NumerosBC = this.reader["numerosBC"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return bonDeCommande;
        }

        string IntfDalBonDeCommande.getNumBonDeCommande(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numBonDeCommande = "00001";
            string[] tempNumBonDeCommande = null;
            string strDate = "BD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT bondecommande.numBonDeCommande AS maxNum FROM bondecommande";
            this.strCommande += " WHERE bondecommande.numBonDeCommande LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumBonDeCommande = reader["maxNum"].ToString().ToString().Split('/');
                        numBonDeCommande = tempNumBonDeCommande[tempNumBonDeCommande.Length - 1];
                    }
                    numTemp = double.Parse(numBonDeCommande) + 1;
                    if (numTemp < 10)
                        numBonDeCommande = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numBonDeCommande = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numBonDeCommande = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numBonDeCommande = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numBonDeCommande = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numBonDeCommande = strDate + "/" + sigleAgence + "/" + numBonDeCommande;
            #endregion

            return numBonDeCommande;
        }
        #endregion

        #region insert to grid
        void IntfDalBonDeCommande.insertToGridBonDeCommande(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();
            #endregion

            #region implementation

            this.strCommande = "SELECT bondecommande.numBonDeCommande, bondecommande.matriculeAgent, bondecommande.descriptionBC,";
            this.strCommande += " bondecommande.numerosBC, bondecommande.dateBC, bondecommande.datePaiementBC,";
            this.strCommande += " bondecommande.montantBC FROM bondecommande";
            this.strCommande += " Left Join recuabonnement ON recuabonnement.numBonDeCommande = bondecommande.numBonDeCommande";
            this.strCommande += " WHERE recuabonnement.numBonDeCommande IS NULL AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBonDeCommande.getDataTableBonDeCommande(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBonDeCommande.getDataTableBonDeCommande(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBonDeCommande", typeof(string));
            dataTable.Columns.Add("numerosBC", typeof(string));
            dataTable.Columns.Add("descriptionBC", typeof(string));
            dataTable.Columns.Add("dateBC", typeof(DateTime));
            dataTable.Columns.Add("datePaiementBC", typeof(DateTime));
            dataTable.Columns.Add("montantBC", typeof(string));
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

                        dr["numBonDeCommande"] = reader["numBonDeCommande"].ToString();
                        dr["numerosBC"] = reader["numerosBC"].ToString();
                        dr["descriptionBC"] = reader["descriptionBC"].ToString();
                        try
                        {
                            dr["dateBC"] = Convert.ToDateTime(this.reader["dateBC"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            dr["datePaiementBC"] = Convert.ToDateTime(this.reader["datePaiementBC"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dr["montantBC"] = serviceGeneral.separateurDesMilles(this.reader["montantBC"].ToString()) + "Ar";


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalBonDeCommande.insertToGridBonDeCommandeNonPaie(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();
            #endregion

            #region implementation

            this.strCommande = "SELECT bondecommande.numBonDeCommande, bondecommande.matriculeAgent, bondecommande.dateBC, bondecommande.datePaiementBC,";
            this.strCommande += " bondecommande.montantBC, bondecommande.descriptionBC, bondecommande.numProforma, proforma.numProforma, proforma.numSociete,";
            this.strCommande += " proforma.numOrganisme, proforma.numClient, proforma.dateProforma, proforma.matriculeAgent, proforma.modePaiement FROM bondecommande";
            this.strCommande += " Inner Join proforma ON proforma.numProforma = bondecommande.numProforma";
            this.strCommande += " Left Join client ON client.numClient = proforma.numClient";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numBonDeCommande = bondecommande.numBonDeCommande";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numBonDeCommande IS NULL";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBonDeCommande.getDataTableBonDeCommandeNonPaie(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBonDeCommande.getDataTableBonDeCommandeNonPaie(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlClient client = null;

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalClient serviceClient = new ImplDalClient();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBonDeCommande", typeof(string));
            dataTable.Columns.Add("datePaiement", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("respSociete", typeof(string));
            dataTable.Columns.Add("respContact", typeof(string));
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

                        dr["numBonDeCommande"] = reader["numBonDeCommande"].ToString();

                        dr["datePaiement"] = Convert.ToDateTime(this.reader["datePaiementBC"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(this.reader["montantBC"].ToString()) + "Ar";

                        if (reader["numClient"].ToString() != "")
                        {
                            client = serviceClient.selectClient(reader["numClient"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (client != null)
                        {
                            dr["client"] = client.PrenomClient + " " + client.NomClient;

                            dr["adresse"] = client.AdresseClient;

                            dr["contact"] = client.TelephoneClient + " / " + client.MobileClient;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["client"] = societe.NomSociete;

                            dr["adresse"] = societe.AdresseSociete;

                            dr["contact"] = societe.TelephoneFixeSociete + " / " + societe.TelephoneMobileSociete;

                            if (societe.individuResponsable != null)
                            {
                                dr["respSociete"] = societe.individuResponsable.PrenomIndividu + " " + societe.individuResponsable.NomIndividu;

                                dr["respContact"] = societe.individuResponsable.TelephoneFixeIndividu + " / " + societe.individuResponsable.TelephoneMobileIndividu;
                            }
                        }
                        else if (organisme != null)
                        {
                            dr["client"] = organisme.NomOrganisme;

                            dr["adresse"] = organisme.AdresseOrganisme;

                            dr["contact"] = organisme.TelephoneFixeOrganisme + " / " + organisme.TelephoneMobileOrganisme;

                            if (organisme.individuResponsable != null)
                            {
                                dr["respSociete"] = organisme.individuResponsable.PrenomIndividu + " " + organisme.individuResponsable.NomIndividu;

                                dr["respContact"] = organisme.individuResponsable.TelephoneFixeIndividu + " / " + organisme.individuResponsable.TelephoneMobileIndividu;
                            }
                        }

                        client = null;
                        societe = null;
                        organisme = null;
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



        void IntfDalBonDeCommande.insertToGridBonDeCommandeNonPaieDate(GridView gridView, string param, string paramLike, string valueLike, DateTime datePaiement)
        {
            #region declaration
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();
            #endregion

            #region implementation

            this.strCommande = "SELECT bondecommande.numBonDeCommande, bondecommande.matriculeAgent, bondecommande.dateBC, bondecommande.datePaiementBC,";
            this.strCommande += " bondecommande.montantBC, bondecommande.descriptionBC, bondecommande.numProforma, proforma.numProforma, proforma.numSociete,";
            this.strCommande += " proforma.numOrganisme, proforma.numClient, proforma.dateProforma, proforma.matriculeAgent, proforma.modePaiement FROM bondecommande";
            this.strCommande += " Inner Join proforma ON proforma.numProforma = bondecommande.numProforma";
            this.strCommande += " Left Join client ON client.numClient = proforma.numClient";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numBonDeCommande = bondecommande.numBonDeCommande";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numBonDeCommande IS NULL AND";
            this.strCommande += " bondecommande.datePaiementBC <= '" + datePaiement.ToString("yyyy-MM-dd") + "'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBonDeCommande.getDataTableBonDeCommandeNonPaieDate(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBonDeCommande.getDataTableBonDeCommandeNonPaieDate(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlClient client = null;

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalClient serviceClient = new ImplDalClient();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBonDeCommande", typeof(string));
            dataTable.Columns.Add("datePaiement", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("respSociete", typeof(string));
            dataTable.Columns.Add("respContact", typeof(string));
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

                        dr["numBonDeCommande"] = reader["numBonDeCommande"].ToString();

                        dr["datePaiement"] = Convert.ToDateTime(this.reader["datePaiementBC"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(this.reader["montantBC"].ToString()) + "Ar";

                        if (reader["numClient"].ToString() != "")
                        {
                            client = serviceClient.selectClient(reader["numClient"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (client != null)
                        {
                            dr["client"] = client.PrenomClient + " " + client.NomClient;

                            dr["adresse"] = client.AdresseClient;

                            dr["contact"] = client.TelephoneClient + " / " + client.MobileClient;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["client"] = societe.NomSociete;

                            dr["adresse"] = societe.AdresseSociete;

                            dr["contact"] = societe.TelephoneFixeSociete + " / " + societe.TelephoneMobileSociete;
                            if (societe.individuResponsable != null)
                            {
                                dr["respSociete"] = societe.individuResponsable.PrenomIndividu + " " + societe.individuResponsable.NomIndividu;

                                dr["respContact"] = societe.individuResponsable.TelephoneFixeIndividu + " / " + societe.individuResponsable.TelephoneMobileIndividu;
                            }
                        }
                        else if (organisme != null)
                        {
                            dr["client"] = organisme.NomOrganisme;

                            dr["adresse"] = organisme.AdresseOrganisme;

                            dr["contact"] = organisme.TelephoneFixeOrganisme + " / " + organisme.TelephoneMobileOrganisme;

                            if (organisme.individuResponsable != null)
                            {
                                dr["respSociete"] = organisme.individuResponsable.PrenomIndividu + " " + organisme.individuResponsable.NomIndividu;

                                dr["respContact"] = organisme.individuResponsable.TelephoneFixeIndividu + " / " + organisme.individuResponsable.TelephoneMobileIndividu;
                            }
                            
                        }

                        client = null;
                        societe = null;
                        organisme = null;
                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalBonDeCommande.insertToGridBonDeCommandeNonPaieDateDF(GridView gridView, string param, string paramLike, string valueLike, DateTime datePaiementDebut, DateTime datePaiementFin)
        {
            #region declaration
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();
            #endregion

            #region implementation

            this.strCommande = "SELECT bondecommande.numBonDeCommande, bondecommande.matriculeAgent, bondecommande.dateBC, bondecommande.datePaiementBC,";
            this.strCommande += " bondecommande.montantBC, bondecommande.descriptionBC, bondecommande.numProforma, proforma.numProforma, proforma.numSociete,";
            this.strCommande += " proforma.numOrganisme, proforma.numClient, proforma.dateProforma, proforma.matriculeAgent, proforma.modePaiement FROM bondecommande";
            this.strCommande += " Inner Join proforma ON proforma.numProforma = bondecommande.numProforma";
            this.strCommande += " Left Join client ON client.numClient = proforma.numClient";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numBonDeCommande = bondecommande.numBonDeCommande";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numBonDeCommande IS NULL AND";
            this.strCommande += " bondecommande.datePaiementBC >= '" + datePaiementDebut.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " bondecommande.datePaiementBC <= '" + datePaiementFin.ToString("yyyy-MM-dd") + "'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceBonDeCommande.getDataTableBonDeCommandeNonPaieDateDF(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalBonDeCommande.getDataTableBonDeCommandeNonPaieDateDF(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlClient client = null;

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalClient serviceClient = new ImplDalClient();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBonDeCommande", typeof(string));
            dataTable.Columns.Add("datePaiement", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("respSociete", typeof(string));
            dataTable.Columns.Add("respContact", typeof(string));
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

                        dr["numBonDeCommande"] = reader["numBonDeCommande"].ToString();

                        dr["datePaiement"] = Convert.ToDateTime(this.reader["datePaiementBC"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(this.reader["montantBC"].ToString()) + "Ar";

                        if (reader["numClient"].ToString() != "")
                        {
                            client = serviceClient.selectClient(reader["numClient"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (client != null)
                        {
                            dr["client"] = client.PrenomClient + " " + client.NomClient;

                            dr["adresse"] = client.AdresseClient;

                            dr["contact"] = client.TelephoneClient + " / " + client.MobileClient;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["client"] = societe.NomSociete;

                            dr["adresse"] = societe.AdresseSociete;

                            dr["contact"] = societe.TelephoneFixeSociete + " / " + societe.TelephoneMobileSociete;
                            if (societe.individuResponsable != null)
                            {
                                dr["respSociete"] = societe.individuResponsable.PrenomIndividu + " " + societe.individuResponsable.NomIndividu;

                                dr["respContact"] = societe.individuResponsable.TelephoneFixeIndividu + " / " + societe.individuResponsable.TelephoneMobileIndividu;
                            }
                        }
                        else if (organisme != null)
                        {
                            dr["client"] = organisme.NomOrganisme;

                            dr["adresse"] = organisme.AdresseOrganisme;

                            dr["contact"] = organisme.TelephoneFixeOrganisme + " / " + organisme.TelephoneMobileOrganisme;

                            if (organisme.individuResponsable != null)
                            {
                                dr["respSociete"] = organisme.individuResponsable.PrenomIndividu + " " + organisme.individuResponsable.NomIndividu;

                                dr["respContact"] = organisme.individuResponsable.TelephoneFixeIndividu + " / " + organisme.individuResponsable.TelephoneMobileIndividu;
                            }
                        }

                        client = null;
                        societe = null;
                        organisme = null;
                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
    }
}