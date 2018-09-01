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
    /// Description résumée de ImplDalSessionCaisse
    /// </summary>
    public class ImplDalSessionCaisse : IntfDalSessionCaisse
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalSessionCaisse()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalSessionCaisse(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlSessionCaisse IntfDalSessionCaisse.selectSessionCaisse(string numSessionCaisse)
        {
            #region declaration
            crlSessionCaisse sessionCaisse = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numSessionCaisse != "") 
            {
                this.strCommande = "SELECT * FROM `sessioncaisse` WHERE `numSessionCaisse`='" + numSessionCaisse + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            sessionCaisse = new crlSessionCaisse();
                            try
                            {
                                sessionCaisse.DateHeureDebutSession = Convert.ToDateTime(this.reader["dateHeureDebutSession"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                sessionCaisse.DateHeureFinSession = Convert.ToDateTime(this.reader["dateHeureFinSession"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                sessionCaisse.FondCaisse = double.Parse(this.reader["fondCaisse"].ToString());
                            }
                            catch (Exception) { }
                            sessionCaisse.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            sessionCaisse.MatriculeAgentFermeture = this.reader["matriculeAgentFermeture"].ToString();
                            sessionCaisse.MatriculeAgentOuverture = this.reader["matriculeAgentOuverture"].ToString();
                            sessionCaisse.NumSessionCaisse = this.reader["numSessionCaisse"].ToString();
                            sessionCaisse.NumSessionAgence = this.reader["numSessionAgence"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                /*if (sessionCaisse != null) 
                {
                    if (sessionCaisse.MatriculeAgentFermeture != "") 
                    {
                        sessionCaisse.agentFermeture = serviceAgent.selectAgent(sessionCaisse.MatriculeAgentFermeture);
                    }
                    if (sessionCaisse.MatriculeAgentOuverture != "")
                    {
                        sessionCaisse.agentOuverture = serviceAgent.selectAgent(sessionCaisse.MatriculeAgentOuverture);
                    }
                }*/
            }
            #endregion

            return sessionCaisse;
        }

        string IntfDalSessionCaisse.insertSessionCaisse(crlSessionCaisse sessionCaisse, string sigleAgence)
        {
            #region declaration
            string numSessionCaisse = "";
            int nbInsert = 0;
            string matriculeAgentFermeture = "NULL";
            string matriculeAgentOuverture = "NULL";
            string numSessionAgence = "NULL";

            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation
            if (sessionCaisse != null)
            {
                if (sessionCaisse.MatriculeAgentFermeture != "")
                {
                    matriculeAgentFermeture = "'" + sessionCaisse.MatriculeAgentFermeture + "'";
                }
                if (sessionCaisse.MatriculeAgentOuverture != "")
                {
                    matriculeAgentOuverture = "'" + sessionCaisse.MatriculeAgentOuverture + "'";
                }
                if (sessionCaisse.NumSessionAgence != "")
                {
                    numSessionAgence = "'" + sessionCaisse.NumSessionAgence + "'";
                }

                sessionCaisse.NumSessionCaisse = serviceSessionCaisse.getNumSessionCaisse(sigleAgence);
                this.strCommande = "INSERT INTO `sessioncaisse` (`numSessionCaisse`,`matriculeAgent`,`matriculeAgentFermeture`,";
                this.strCommande += " `dateHeureDebutSession`,`dateHeureFinSession`,`fondCaisse`,`matriculeAgentOuverture`,`numSessionAgence`) VALUES";
                this.strCommande += " ('" + sessionCaisse.NumSessionCaisse + "','" + sessionCaisse.MatriculeAgent + "',";
                this.strCommande += " " + matriculeAgentFermeture + ",'" + sessionCaisse.DateHeureDebutSession.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + sessionCaisse.DateHeureFinSession.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + sessionCaisse.FondCaisse.ToString("0") + "'," + matriculeAgentOuverture + "," + numSessionAgence + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numSessionCaisse = sessionCaisse.NumSessionCaisse;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numSessionCaisse;
        }

        bool IntfDalSessionCaisse.updateSessionCaisse(crlSessionCaisse sessionCaisse)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string matriculeAgentFermeture = "NULL";
            string matriculeAgentOuverture = "NULL";
            string numSessionAgence = "NULL";
            #endregion

            #region implementation
            if (sessionCaisse != null)
            {
                if (sessionCaisse.MatriculeAgentFermeture != "")
                {
                    matriculeAgentFermeture = "'" + sessionCaisse.MatriculeAgentFermeture + "'";
                }
                if (sessionCaisse.MatriculeAgentOuverture != "")
                {
                    matriculeAgentOuverture = "'" + sessionCaisse.MatriculeAgentOuverture + "'";
                }
                if (sessionCaisse.NumSessionAgence != "")
                {
                    numSessionAgence = "'" + sessionCaisse.NumSessionAgence + "'";
                }

                this.strCommande = "UPDATE `sessioncaisse` SET `matriculeAgent`='" + sessionCaisse.MatriculeAgent + "',";
                this.strCommande += " `matriculeAgentFermeture`=" + matriculeAgentFermeture + ",";
                this.strCommande += " `dateHeureDebutSession`='" + sessionCaisse.DateHeureDebutSession.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `dateHeureFinSession`='" + sessionCaisse.DateHeureFinSession.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `fondCaisse`='" + sessionCaisse.FondCaisse.ToString("0") + "',";
                this.strCommande += " `matriculeAgentOuverture`=" + matriculeAgentOuverture + ",";
                this.strCommande += " `numSessionAgence`=" + numSessionAgence;
                this.strCommande += " WHERE `numSessionCaisse`='" + sessionCaisse.NumSessionCaisse + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if(nbUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalSessionCaisse.getNumSessionCaisse(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numSessionCaisse = "00001";
            string[] tempNumSessionCaisse = null;
            string strDate = "SC" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT sessioncaisse.numSessionCaisse AS maxNum FROM sessioncaisse";
            this.strCommande += " WHERE sessioncaisse.numSessionCaisse LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumSessionCaisse = reader["maxNum"].ToString().ToString().Split('/');
                        numSessionCaisse = tempNumSessionCaisse[tempNumSessionCaisse.Length - 1];
                    }
                    numTemp = double.Parse(numSessionCaisse) + 1;
                    if (numTemp < 10)
                        numSessionCaisse = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numSessionCaisse = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numSessionCaisse = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numSessionCaisse = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numSessionCaisse = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numSessionCaisse = strDate + "/" + sigleAgence + "/" + numSessionCaisse;
            #endregion

            return numSessionCaisse;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseBillet(string numBillet, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numBillet != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaissebillet` (`numBillet`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numBillet + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseCommission(string idCommission, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (idCommission!= "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaissecommission` (`idCommission`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + idCommission + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseDureeAbonnement(string numDureeAbonnement, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numDureeAbonnement != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaissedureeabonnement` (`numDureeAbonnement`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numDureeAbonnement + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseVoyageAbonnement(string numVoyageAbonnement, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numVoyageAbonnement != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaissevoyageabonnement` (`numVoyageAbonnement`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numVoyageAbonnement + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseRecuEncaisser(string numRecuEncaisser, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numRecuEncaisser != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaisserecuencaisser` (`numRecuEncaisser`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numRecuEncaisser + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseRecuDecaisser(string numRecuDecaisser, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numRecuDecaisser != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaisserecudecaisser` (`numRecuDecaisser`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numRecuDecaisser + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertAssocSessionCaisseRecuAD(string numRecuAD, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numRecuAD != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `assocsessioncaisserecuad` (`numRecuAD`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numRecuAD + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertUSAssocSessionCaisseAbonnementNV(string numAbonnementNV, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numAbonnementNV != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `usassocsessioncaisseabonnementnv` (`numAbonnementNV`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numAbonnementNV + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertUSAssocSessionCaisseBillet(string numBillet, string numSessionCaisse)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numBillet != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `usassocsessioncaissebillet` (`numBillet`,`numSessionCaisse`)";
                this.strCommande += " VALUES ('" + numBillet + "','" + numSessionCaisse + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalSessionCaisse.insertUSAssocSessionCaisseCarte(string numCarte, string numSessionCaisse, int isEncaisser)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numCarte != "" && numSessionCaisse != "")
            {
                this.strCommande = "INSERT INTO `usassocsessioncaissecarte` (`numCarte`,`numSessionCaisse`,)";
                this.strCommande += " `date`,`isEncaisser`) VALUES ('" + numCarte + "','" + numSessionCaisse + "',";
                this.strCommande += " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + isEncaisser.ToString("0") + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        double IntfDalSessionCaisse.getMontantTotalBillet(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if(numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(billet.prixBillet) AS montantTotal FROM billet";
                this.strCommande += " Inner Join assocsessioncaissebillet ON assocsessioncaissebillet.numBillet = billet.numBillet";
                this.strCommande += " WHERE assocsessioncaissebillet.numSessionCaisse = '" + numSessionCaisse + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if(this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalCommission(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(commission.fraisEnvoi) AS montantTotal FROM commission";
                this.strCommande += " Inner Join assocsessioncaissecommission ON assocsessioncaissecommission.idCommission = commission.idCommission";
                this.strCommande += " WHERE assocsessioncaissecommission.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalDureeAbonnement(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(dureeabonnement.prixTotal) AS montantTotal FROM dureeabonnement";
                this.strCommande += " Inner Join assocsessioncaissedureeabonnement ON assocsessioncaissedureeabonnement.numDureeAbonnement = dureeabonnement.numDureeAbonnement";
                this.strCommande += " WHERE assocsessioncaissedureeabonnement.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalVoyageAbonnement(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(voyageabonnement.prixUnitaire * voyageabonnement.nbVoyageAbonnement) AS montantTotal FROM voyageabonnement";
                this.strCommande += " Inner Join assocsessioncaissevoyageabonnement ON assocsessioncaissevoyageabonnement.numVoyageAbonnement = voyageabonnement.numVoyageAbonnement";
                this.strCommande += " WHERE assocsessioncaissevoyageabonnement.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalRecuEncaisser(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS montantTotal FROM recuencaisser";
                this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " WHERE assocsessioncaisserecuencaisser.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalRecuDecaisser(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(recudecaisser.montantRecuDecaisser) AS montantTotal FROM recudecaisser";
                this.strCommande += " Inner Join assocsessioncaisserecudecaisser ON assocsessioncaisserecudecaisser.numRecuDecaisser = recudecaisser.numRecuDecaisser";
                this.strCommande += " WHERE assocsessioncaisserecudecaisser.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalRecuEncaisserCheque(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS montantTotal FROM recuencaisser";
                this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " WHERE assocsessioncaisserecuencaisser.numSessionCaisse = '" + numSessionCaisse + "' AND";
                this.strCommande += " recuencaisser.modePaiement = 'Chèque'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalRecuEncaisserEspece(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS montantTotal FROM recuencaisser";
                this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " WHERE assocsessioncaisserecuencaisser.numSessionCaisse = '" + numSessionCaisse + "' AND";
                this.strCommande += " recuencaisser.modePaiement = 'Espèce'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalRecuAD(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS montantTotal FROM recuad";
                this.strCommande += " Inner Join assocsessioncaisserecuad ON assocsessioncaisserecuad.numRecuAD = recuad.numRecuAD";
                this.strCommande += " WHERE assocsessioncaisserecuad.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalAbonnementNVUS(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(usabonnementnv.montantNV) AS montantTotal FROM usabonnementnv";
                this.strCommande += " Inner Join usassocsessioncaisseabonnementnv ON usassocsessioncaisseabonnementnv.numAbonnementNV = usabonnementnv.numAbonnementNV";
                this.strCommande += " WHERE usassocsessioncaisseabonnementnv.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalBilletUS(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(usbillet.montant) AS montantTotal FROM usbillet";
                this.strCommande += " Inner Join usassocsessioncaissebillet ON usassocsessioncaissebillet.numBillet = usbillet.numBillet";
                this.strCommande += " WHERE usassocsessioncaissebillet.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        double IntfDalSessionCaisse.getMontantTotalCarteEnCaisser(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(uscarte.prixCarte) AS montantTotal FROM uscarte";
                this.strCommande += " Inner Join usassocsessioncaissecarte ON usassocsessioncaissecarte.numCarte = uscarte.numCarte";
                this.strCommande += " WHERE usassocsessioncaissecarte.isEncaisser = '1' AND";
                this.strCommande += " usassocsessioncaissecarte.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        

        double IntfDalSessionCaisse.getMontantTotalCarteDecaisser(string numSessionCaisse)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                this.strCommande = "SELECT Sum(uscarte.prixCarte) AS montantTotal FROM uscarte";
                this.strCommande += " Inner Join usassocsessioncaissecarte ON usassocsessioncaissecarte.numCarte = uscarte.numCarte";
                this.strCommande += " WHERE usassocsessioncaissecarte.isEncaisser = '0' AND";
                this.strCommande += " usassocsessioncaissecarte.numSessionCaisse = '" + numSessionCaisse + "'";

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
                                montantTotal = double.Parse(this.reader["montantTotal"].ToString());
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

            return montantTotal;
        }

        crlSessionCaisse IntfDalSessionCaisse.getSessionCaisseEncours(string matriculeAgent)
        {
            #region declaration
            crlSessionCaisse sessionCaisse = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT * FROM `sessioncaisse` WHERE sessioncaisse.matriculeAgentFermeture IS NULL AND";
                this.strCommande += " sessioncaisse.matriculeAgent = '" + matriculeAgent + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            sessionCaisse = new crlSessionCaisse();
                            try
                            {
                                sessionCaisse.DateHeureDebutSession = Convert.ToDateTime(this.reader["dateHeureDebutSession"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                sessionCaisse.DateHeureFinSession = Convert.ToDateTime(this.reader["dateHeureFinSession"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                sessionCaisse.FondCaisse = double.Parse(this.reader["fondCaisse"].ToString());
                            }
                            catch (Exception) { }
                            sessionCaisse.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            sessionCaisse.MatriculeAgentFermeture = this.reader["matriculeAgentFermeture"].ToString();
                            sessionCaisse.MatriculeAgentOuverture = this.reader["matriculeAgentOuverture"].ToString();
                            sessionCaisse.NumSessionCaisse = this.reader["numSessionCaisse"].ToString();
                            sessionCaisse.NumSessionAgence = this.reader["numSessionAgence"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                /*if (sessionCaisse != null)
                {
                    if (sessionCaisse.MatriculeAgentFermeture != "")
                    {
                        sessionCaisse.agentFermeture = serviceAgent.selectAgent(sessionCaisse.MatriculeAgentFermeture);
                    }
                    if (sessionCaisse.MatriculeAgentOuverture != "")
                    {
                        sessionCaisse.agentOuverture = serviceAgent.selectAgent(sessionCaisse.MatriculeAgentOuverture);
                    }
                }*/
            }
            #endregion

            return sessionCaisse;
        }
        #endregion

        #region insert To grid
        void IntfDalSessionCaisse.insertToGridBilletSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT billet.numBillet, billet.dateDeValidite, billet.numTrajet, billet.numClient,trajet.numVilleD,";
            this.strCommande += " billet.matriculeAgent, billet.prixBillet, billet.dateBillet, billet.modePaiement,trajet.numVilleF,";
            this.strCommande += " billet.numCalculCategorieBillet, billet.numCalculReductionBillet, billet.numDureeAbonnement,";
            this.strCommande += " billet.numVoyageAbonnement, billet.numBilletCommande FROM billet";
            this.strCommande += " Inner Join assocsessioncaissebillet ON assocsessioncaissebillet.numBillet = billet.numBillet";
            this.strCommande += " Inner Join trajet ON trajet.numTrajet = billet.numTrajet";
            this.strCommande += " WHERE assocsessioncaissebillet.numSessionCaisse = '" +  numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableBilletSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableBilletSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBillet", typeof(string));
            dataTable.Columns.Add("dateValidite", typeof(DateTime));
            dataTable.Columns.Add("prixBillet", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));
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

                        dr["numBillet"] = reader["numBillet"].ToString();
                        dr["dateValidite"] = Convert.ToDateTime(reader["dateDeValidite"].ToString());
                        dr["prixBillet"] = serviceGeneral.separateurDesMilles(reader["prixBillet"].ToString()) + "Ar";

                        villeD = serviceVille.selectVille(reader["numVilleD"].ToString());
                        villeF = serviceVille.selectVille(reader["numVilleF"].ToString());

                        if (villeD != null && villeF != null)
                        {
                            dr["trajet"] = villeD.NomVille + "-" + villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = reader["numVilleD"].ToString() + "-" + reader["numVilleF"].ToString();
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

        void IntfDalSessionCaisse.insertToGridCommissionSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT commission.idCommission, commission.destination, commission.poids, commission.nombre,";
            this.strCommande += " commission.pieceJustificatif, commission.fraisEnvoi, commission.numExpediteur, commission.numRecepteur,";
            this.strCommande += " commission.numDesignation, commission.typeCommission, commission.numTrajet, commission.dateCommission,";
            this.strCommande += " commission.matriculeAgent, commission.matriculeAgentDelivreur, commission.dateLivraison, commission.isRecu,";
            this.strCommande += " commission.modePaiement FROM commission";
            this.strCommande += " Inner Join assocsessioncaissecommission ON assocsessioncaissecommission.idCommission = commission.idCommission";
            this.strCommande += " WHERE assocsessioncaissecommission.numSessionCaisse = '" + numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableCommissionSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableCommissionSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idCommission", typeof(string));
            dataTable.Columns.Add("dateCommission", typeof(DateTime));
            dataTable.Columns.Add("typeCommission", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            dataTable.Columns.Add("fraisEnvoi", typeof(string));
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

                        dr["idCommission"] = reader["idCommission"].ToString();
                        dr["dateCommission"] = Convert.ToDateTime(reader["dateCommission"].ToString());
                        dr["typeCommission"] = reader["typeCommission"].ToString();
                        dr["destination"] = reader["destination"].ToString();
                        dr["fraisEnvoi"] = serviceGeneral.separateurDesMilles(reader["fraisEnvoi"].ToString()) + "Ar";

                        
                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalSessionCaisse.insertToGridAbonnementDureeTempsSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT dureeabonnement.numDureeAbonnement, dureeabonnement.numTrajet, dureeabonnement.zone, dureeabonnement.prixUnitaire,";
            this.strCommande += " dureeabonnement.numAbonnement, dureeabonnement.valideDu, dureeabonnement.valideAu, dureeabonnement.prixTotal,";
            this.strCommande += " dureeabonnement.matriculeAgent, dureeabonnement.dateDureeAbonnement, dureeabonnement.numCalculCategorieBillet,";
            this.strCommande += " dureeabonnement.numCalculReductionBillet, dureeabonnement.modePaiement FROM dureeabonnement";
            this.strCommande += " Inner Join assocsessioncaissedureeabonnement ON assocsessioncaissedureeabonnement.numDureeAbonnement = dureeabonnement.numDureeAbonnement";
            this.strCommande += " WHERE assocsessioncaissedureeabonnement.numSessionCaisse = '" + numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableAbonnementDureeTempsSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableAbonnementDureeTempsSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numDureeAbonnement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("valideDu", typeof(DateTime));
            dataTable.Columns.Add("valideAu", typeof(DateTime));
            dataTable.Columns.Add("prixTotal", typeof(string));
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

                        dr["numDureeAbonnement"] = reader["numDureeAbonnement"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        dr["valideDu"] = Convert.ToDateTime(reader["valideDu"].ToString());
                        dr["valideAu"] = Convert.ToDateTime(reader["valideAu"].ToString());
                        dr["prixTotal"] = serviceGeneral.separateurDesMilles(reader["prixTotal"].ToString()) + "Ar";


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalSessionCaisse.insertToGridAbonnementNbVoyageSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT voyageabonnement.numVoyageAbonnement, voyageabonnement.numTrajet, voyageabonnement.zone, voyageabonnement.prixUnitaire,";
            this.strCommande += " voyageabonnement.nbVoyageAbonnement, voyageabonnement.numAbonnement, voyageabonnement.matriculeAgent,";
            this.strCommande += " voyageabonnement.dateVoyageAbonnement, voyageabonnement.numCalculCategorieBillet, voyageabonnement.numCalculReductionBillet,";
            this.strCommande += " voyageabonnement.modePaiement FROM voyageabonnement";
            this.strCommande += " Inner Join assocsessioncaissevoyageabonnement ON assocsessioncaissevoyageabonnement.numVoyageAbonnement = voyageabonnement.numVoyageAbonnement";
            this.strCommande += " WHERE assocsessioncaissevoyageabonnement.numSessionCaisse = '" + numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableAbonnementNbVoyageSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableAbonnementNbVoyageSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            int nbVoyage = 0;
            double prix = 0;

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVoyageAbonnement", typeof(string));
            dataTable.Columns.Add("zone", typeof(string));
            dataTable.Columns.Add("dateVoyageAbonnement", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
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

                        dr["numVoyageAbonnement"] = reader["numVoyageAbonnement"].ToString();
                        dr["zone"] = reader["zone"].ToString();
                        dr["dateVoyageAbonnement"] = Convert.ToDateTime(reader["dateVoyageAbonnement"].ToString());

                        try
                        {
                            nbVoyage = int.Parse(reader["nbVoyageAbonnement"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            prix = double.Parse(reader["prixUnitaire"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                        dr["montant"] = serviceGeneral.separateurDesMilles((nbVoyage * prix).ToString("0")) + "Ar";


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalSessionCaisse.insertToGridRecuEspeceSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT recuencaisser.numRecuEncaisser, recuencaisser.matriculeAgent, recuencaisser.numCheque, recuencaisser.modePaiement,";
            this.strCommande += " recuencaisser.dateRecuEncaisser, recuencaisser.montantRecuEncaisser, recuencaisser.libelleRecuEncaisser FROM recuencaisser";
            this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
            this.strCommande += " WHERE assocsessioncaisserecuencaisser.numSessionCaisse = '" + numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " AND recuencaisser.modePaiement = 'Espèce'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableRecuEspeceSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableRecuEspeceSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            int nbVoyage = 0;
            double prix = 0;

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuEncaisser", typeof(string));
            dataTable.Columns.Add("modePaiement", typeof(string));
            dataTable.Columns.Add("dateRecuEncaisser", typeof(DateTime));
            dataTable.Columns.Add("montantRecuEncaisser", typeof(string));
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

                        dr["numRecuEncaisser"] = reader["numRecuEncaisser"].ToString();
                        dr["modePaiement"] = reader["modePaiement"].ToString();
                        dr["dateRecuEncaisser"] = Convert.ToDateTime(reader["dateRecuEncaisser"].ToString());

                        dr["montantRecuEncaisser"] = serviceGeneral.separateurDesMilles(reader["montantRecuEncaisser"].ToString()) + "Ar";


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalSessionCaisse.insertToGridRecuChequeSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT recuencaisser.numRecuEncaisser, recuencaisser.matriculeAgent, recuencaisser.numCheque, recuencaisser.modePaiement,";
            this.strCommande += " recuencaisser.dateRecuEncaisser, recuencaisser.montantRecuEncaisser, recuencaisser.libelleRecuEncaisser FROM recuencaisser";
            this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
            this.strCommande += " WHERE assocsessioncaisserecuencaisser.numSessionCaisse = '" + numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " AND recuencaisser.modePaiement = 'Chèque'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableRecuChequeSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableRecuChequeSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            int nbVoyage = 0;
            double prix = 0;

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuEncaisser", typeof(string));
            dataTable.Columns.Add("modePaiement", typeof(string));
            dataTable.Columns.Add("dateRecuEncaisser", typeof(DateTime));
            dataTable.Columns.Add("montantRecuEncaisser", typeof(string));
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

                        dr["numRecuEncaisser"] = reader["numRecuEncaisser"].ToString();
                        dr["modePaiement"] = reader["modePaiement"].ToString();
                        dr["dateRecuEncaisser"] = Convert.ToDateTime(reader["dateRecuEncaisser"].ToString());

                        dr["montantRecuEncaisser"] = serviceGeneral.separateurDesMilles(reader["montantRecuEncaisser"].ToString()) + "Ar";


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalSessionCaisse.insertToGridRecuADSession(GridView gridView, string param, string paramLike, string valueLike, string numSessionCaisse)
        {
            #region declaration
            IntfDalSessionCaisse serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region implementation

            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.montant, recuad.dateRecu,";
            this.strCommande += " recuad.numPrelevement, recuad.numFacture FROM recuad";
            this.strCommande += " Inner Join assocsessioncaisserecuad ON assocsessioncaisserecuad.numRecuAD = recuad.numRecuAD";
            this.strCommande += " WHERE assocsessioncaisserecuad.numSessionCaisse = '" + numSessionCaisse + "'";
            this.strCommande += " AND " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " DESC";

            gridView.DataSource = serviceSessionCaisse.getDataTableRecuADSession(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalSessionCaisse.getDataTableRecuADSession(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            int nbVoyage = 0;
            double prix = 0;

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("libele", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
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

                        dr["numRecuAD"] = reader["numRecuAD"].ToString();
                        dr["libele"] = reader["libele"].ToString();
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";


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


        #region IntfDalSessionCaisse Members


        

        

        #endregion
    }
}