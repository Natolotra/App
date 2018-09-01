using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;


namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalRecuAbonnement
    /// </summary>
    public class ImplDalRecuAbonnement : IntfDalRecuAbonnement
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRecuAbonnement()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalRecuAbonnement(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlRecuAbonnement IntfDalRecuAbonnement.selectRecuAbonnement(string numRecuAbonnement)
        {
            #region declaration
            crlRecuAbonnement recuAbonnement = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalCheque serviceCheque = new ImplDalCheque();
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();
            IntfDalVoyageAbonnement serviceVoyageAbonnement = new ImplDalVoyageAbonnement();
            IntfDalDureeAbonnement serviceDureeAbonnement = new ImplDalDureeAbonnement();
            #endregion

            #region implementation
            if (numRecuAbonnement != "")
            {
                this.strCommande = "SELECT * FROM `recuabonnement` WHERE (`numRecuAbonnement`='" + numRecuAbonnement + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            recuAbonnement = new crlRecuAbonnement();
                            try
                            {
                                recuAbonnement.DateRecuAbonnement = Convert.ToDateTime(this.reader["dateRecuAbonnement"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            recuAbonnement.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            recuAbonnement.ModePaiement = this.reader["modePaiement"].ToString();
                            try
                            {
                                recuAbonnement.MontantRecuAbonnement = double.Parse(this.reader["montantRecuAbonnement"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            recuAbonnement.NumBonDeCommande = this.reader["numBonDeCommande"].ToString();
                            recuAbonnement.NumCheque = this.reader["numCheque"].ToString();
                            recuAbonnement.NumDureeAbonnement = this.reader["numDureeAbonnement"].ToString();
                            recuAbonnement.NumRecuAbonnement = this.reader["numRecuAbonnement"].ToString();
                            recuAbonnement.NumVoyageAbonnement = this.reader["numVoyageAbonnement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (recuAbonnement != null)
                {
                    if (recuAbonnement.MatriculeAgent != "")
                    {
                        recuAbonnement.agent = serviceAgent.selectAgent(recuAbonnement.MatriculeAgent);
                    }
                    if (recuAbonnement.NumBonDeCommande != "")
                    {
                        recuAbonnement.bonDeCommande = serviceBonDeCommande.selectBonDeCommande(recuAbonnement.NumBonDeCommande);
                    }
                    if (recuAbonnement.NumCheque != "")
                    {
                        recuAbonnement.cheque = serviceCheque.selectCheque(recuAbonnement.NumCheque);
                    }
                    if (recuAbonnement.NumDureeAbonnement !="")
                    {
                        recuAbonnement.dureeAbonnement = serviceDureeAbonnement.selectDureeAbonnement(recuAbonnement.NumDureeAbonnement);
                    }
                    if (recuAbonnement.NumVoyageAbonnement != "")
                    {
                        recuAbonnement.voyageAbonnement = serviceVoyageAbonnement.selectVoyageAbonnement(recuAbonnement.NumVoyageAbonnement);
                    }
                }
            }
            #endregion

            return recuAbonnement;
        }

        string IntfDalRecuAbonnement.insertRecuAbonnement(crlRecuAbonnement recuAbonnement)
        {
            #region declaration
            string numRecuAbonnement = "";
            int nombreInsert = 0;
            IntfDalRecuAbonnement serviceRecuAbonnement = new ImplDalRecuAbonnement();

            string numVoyageAbonnement = "";
            string numDureeAbonnement = "";
            string numCheque = "";
            string numBonDeCommande = "";
            #endregion

            #region implementation
            if (recuAbonnement != null)
            {

                if (recuAbonnement.agent != null)
                {
                    if (recuAbonnement.NumVoyageAbonnement != "")
                    {
                        numVoyageAbonnement = "'" + recuAbonnement.NumVoyageAbonnement + "'";
                    }
                    else
                    {
                        numVoyageAbonnement = "NULL";
                    }
                    if (recuAbonnement.NumDureeAbonnement != "")
                    {
                        numDureeAbonnement = "'" + recuAbonnement.NumDureeAbonnement + "'";
                    }
                    else
                    {
                        numDureeAbonnement = "NULL";
                    }
                    if (recuAbonnement.NumCheque != "")
                    {
                        numCheque = "'" + recuAbonnement.NumCheque + "'";
                    }
                    else
                    {
                        numCheque = "NULL";
                    }
                    if (recuAbonnement.NumBonDeCommande != "")
                    {
                        numBonDeCommande = "'" + recuAbonnement.NumBonDeCommande + "'";
                    }
                    else
                    {
                        numBonDeCommande = "NULL";
                    }

                    recuAbonnement.NumRecuAbonnement = serviceRecuAbonnement.getNumRecuAbonnement(recuAbonnement.agent.agence.SigleAgence);
                    this.strCommande = "INSERT INTO `recuabonnement` (`numRecuAbonnement`,`numVoyageAbonnement`,";
                    this.strCommande += " `numDureeAbonnement`,`modePaiement`,`numCheque`,`numBonDeCommande`,";
                    this.strCommande += " `matriculeAgent`,`dateRecuAbonnement`,`montantRecuAbonnement`)";
                    this.strCommande += " VALUES ('" + recuAbonnement.NumRecuAbonnement + "', " + numVoyageAbonnement + ",";
                    this.strCommande += " " + numDureeAbonnement + ", '" + recuAbonnement.ModePaiement + "',";
                    this.strCommande += " " + numCheque + ", " + numBonDeCommande + ",";
                    this.strCommande += " '" + recuAbonnement.MatriculeAgent + "', '" + recuAbonnement.DateRecuAbonnement.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " '" + recuAbonnement.MontantRecuAbonnement + "')";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numRecuAbonnement = recuAbonnement.NumRecuAbonnement;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numRecuAbonnement;
        }

        bool IntfDalRecuAbonnement.updateRecuAbonnement(crlRecuAbonnement recuAbonnement)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numVoyageAbonnement = "";
            string numDureeAbonnement = "";
            string numCheque = "";
            string numBonDeCommande = "";
            #endregion

            #region implementation
            if (recuAbonnement != null)
            {
                
                if (recuAbonnement.NumVoyageAbonnement != "")
                {
                    numVoyageAbonnement = "'" + recuAbonnement.NumVoyageAbonnement + "'";
                }
                else
                {
                    numVoyageAbonnement = "NULL";
                }
                if (recuAbonnement.NumDureeAbonnement != "")
                {
                    numDureeAbonnement = "'" + recuAbonnement.NumDureeAbonnement + "'";
                }
                else
                {
                    numDureeAbonnement = "NULL";
                }
                if (recuAbonnement.NumCheque != "")
                {
                    numCheque = "'" + recuAbonnement.NumCheque + "'";
                }
                else
                {
                    numCheque = "NULL";
                }
                if (recuAbonnement.NumBonDeCommande != "")
                {
                    numBonDeCommande = "'" + recuAbonnement.NumBonDeCommande + "'";
                }
                else
                {
                    numBonDeCommande = "NULL";
                }
                
                this.strCommande = "UPDATE `recuabonnement` SET `dateRecuAbonnement`='" + recuAbonnement.DateRecuAbonnement.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `matriculeAgent`='" + recuAbonnement.MatriculeAgent + "', `modePaiement`='" + recuAbonnement.ModePaiement + "',";
                this.strCommande += " `montantRecuAbonnement`='" + recuAbonnement.MontantRecuAbonnement + "',";
                this.strCommande += " `numBonDeCommande`=" + numBonDeCommande + ",";
                this.strCommande += " `numCheque`=" + numCheque + ", `numDureeAbonnement`=" + numDureeAbonnement + ",";
                this.strCommande += " `numVoyageAbonnement`=" + numVoyageAbonnement;
                this.strCommande += " WHERE `numRecuAbonnement`='" + recuAbonnement.NumRecuAbonnement + "'";

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

        string IntfDalRecuAbonnement.getNumRecuAbonnement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numRecuAbonnement = "00001";
            string[] tempNumRecuAbonnement = null;
            string strDate = "RA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT recuabonnement.numRecuAbonnement AS maxNum FROM recuabonnement";
            this.strCommande += " WHERE recuabonnement.numRecuAbonnement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumRecuAbonnement = reader["maxNum"].ToString().ToString().Split('/');
                        numRecuAbonnement = tempNumRecuAbonnement[tempNumRecuAbonnement.Length - 1];
                    }
                    numTemp = double.Parse(numRecuAbonnement) + 1;
                    if (numTemp < 10)
                        numRecuAbonnement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numRecuAbonnement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numRecuAbonnement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numRecuAbonnement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numRecuAbonnement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numRecuAbonnement = strDate + "/" + sigleAgence + "/" + numRecuAbonnement;
            #endregion

            return numRecuAbonnement;
        }
        #endregion
    }
}