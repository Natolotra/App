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
    /// Interface du service session agence
    /// </summary>
    public class ImplDalSessionAgence : IntfDalSessionAgence
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalSessionAgence()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalSessionAgence(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        crlSessionAgence IntfDalSessionAgence.selectSessionAgence(string numSessionAgence)
        {
            #region declaration
            crlSessionAgence sessionAgence = null;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT * FROM `sessionagence` WHERE `numSessionAgence`='" + numSessionAgence + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            sessionAgence = new crlSessionAgence();
                            try
                            {
                                sessionAgence.DateHeureOuverture = Convert.ToDateTime(this.reader["dateHeureOuverutre"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                sessionAgence.DateHeureFermeture = Convert.ToDateTime(this.reader["dateHeureFermeture"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            sessionAgence.NumAgence = this.reader["numAgence"].ToString();
                            sessionAgence.MatriculeAgentFermeture = this.reader["matriculeAgentFermeture"].ToString();
                            sessionAgence.MatriculeAgentOuverture = this.reader["matriculeAgentOuverture"].ToString();
                            sessionAgence.NumSessionAgence = this.reader["numSessionAgence"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                
            }
            #endregion

            return sessionAgence;
        }

        string IntfDalSessionAgence.insertSessionAgence(crlSessionAgence sessionAgence, string sigleAgence)
        {
            #region declaration
            string numSessionCaisse = "";
            int nbInsert = 0;
            string matriculeAgentFermeture = "NULL";
            string matriculeAgentOuverture = "NULL";

            IntfDalSessionAgence serviceSessionAgence = new ImplDalSessionAgence();
            #endregion

            #region implementation
            if (sessionAgence != null)
            {
                if (sessionAgence.MatriculeAgentFermeture != "")
                {
                    matriculeAgentFermeture = "'" + sessionAgence.MatriculeAgentFermeture + "'";
                }
                if (sessionAgence.MatriculeAgentOuverture != "")
                {
                    matriculeAgentOuverture = "'" + sessionAgence.MatriculeAgentOuverture + "'";
                }

                sessionAgence.NumSessionAgence = serviceSessionAgence.getNumSessionAgence(sigleAgence);
                this.strCommande = "INSERT INTO `sessionagence` (`numSessionAgence`,`numAgence`,`matriculeAgentOuverture`,";
                this.strCommande += " `matriculeAgentFermeture`,`dateHeureOuverutre`,`dateHeureFermeture`) VALUES";
                this.strCommande += " ('" + sessionAgence.NumSessionAgence + "','" + sessionAgence.NumAgence + "',";
                this.strCommande += " " + matriculeAgentOuverture + "," + matriculeAgentFermeture + ",";
                this.strCommande += " '" + sessionAgence.DateHeureOuverture.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + sessionAgence.DateHeureFermeture.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    numSessionCaisse = sessionAgence.NumSessionAgence;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numSessionCaisse;
        }

        bool IntfDalSessionAgence.updateSessionAgence(crlSessionAgence sessionAgence)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string matriculeAgentFermeture = "NULL";
            string matriculeAgentOuverture = "NULL";
            #endregion

            #region implementation
            if (sessionAgence != null)
            {
                if (sessionAgence.MatriculeAgentFermeture != "")
                {
                    matriculeAgentFermeture = "'" + sessionAgence.MatriculeAgentFermeture + "'";
                }
                if (sessionAgence.MatriculeAgentOuverture != "")
                {
                    matriculeAgentOuverture = "'" + sessionAgence.MatriculeAgentOuverture + "'";
                }

                this.strCommande = "UPDATE `sessionagence` SET `numAgence`='" + sessionAgence.NumAgence + "',";
                this.strCommande += " `matriculeAgentOuverture`=" + matriculeAgentOuverture + ",";
                this.strCommande += " `matriculeAgentFermeture`=" + matriculeAgentFermeture + ",";
                this.strCommande += " `dateHeureOuverutre`='" + sessionAgence.DateHeureOuverture.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `dateHeureFermeture`='" + sessionAgence.DateHeureFermeture.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                this.strCommande += " WHERE `numSessionAgence`='" + sessionAgence.NumSessionAgence + "'";

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

        crlSessionAgence IntfDalSessionAgence.getSessionAgenceEncours(string numAgence)
        {
            #region declaration
            crlSessionAgence sessionAgence = null;
            #endregion

            #region implementation
            if (numAgence != "")
            {
                this.strCommande = "SELECT * FROM `sessionagence` WHERE sessionagence.matriculeAgentFermeture IS NULL AND";
                this.strCommande += " sessionagence.numAgence = '" + numAgence + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            sessionAgence = new crlSessionAgence();
                            try
                            {
                                sessionAgence.DateHeureOuverture = Convert.ToDateTime(this.reader["dateHeureOuverutre"].ToString());
                            }
                            catch (Exception)
                            {
                                sessionAgence.DateHeureOuverture = DateTime.Now;
                            }
                            try
                            {
                                sessionAgence.DateHeureFermeture = Convert.ToDateTime(this.reader["dateHeureFermeture"].ToString());
                            }
                            catch (Exception)
                            {
                                sessionAgence.DateHeureOuverture = DateTime.Now;
                            }

                            sessionAgence.NumAgence = this.reader["numAgence"].ToString();
                            sessionAgence.MatriculeAgentFermeture = this.reader["matriculeAgentFermeture"].ToString();
                            sessionAgence.MatriculeAgentOuverture = this.reader["matriculeAgentOuverture"].ToString();
                            sessionAgence.NumSessionAgence = this.reader["numSessionAgence"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                
            }
            #endregion

            return sessionAgence;
        }

        string IntfDalSessionAgence.getNumSessionAgence(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numSessionAgence = "00001";
            string[] tempNumSessionAgence = null;
            string strDate = "SA" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT sessionagence.numSessionAgence AS maxNum FROM sessionagence";
            this.strCommande += " WHERE sessionagence.numSessionAgence LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumSessionAgence = reader["maxNum"].ToString().ToString().Split('/');
                        numSessionAgence = tempNumSessionAgence[tempNumSessionAgence.Length - 1];
                    }
                    numTemp = double.Parse(numSessionAgence) + 1;
                    if (numTemp < 10)
                        numSessionAgence = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numSessionAgence = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numSessionAgence = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numSessionAgence = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numSessionAgence = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numSessionAgence = strDate + "/" + sigleAgence + "/" + numSessionAgence;
            #endregion

            return numSessionAgence;
        }

        double IntfDalSessionAgence.getMontantTotalBillet(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(billet.prixBillet) AS montantTotal FROM billet";
                this.strCommande += " Inner Join assocsessioncaissebillet ON assocsessioncaissebillet.numBillet = billet.numBillet";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaissebillet.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "'";

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

        double IntfDalSessionAgence.getMontantTotalCommission(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(commission.fraisEnvoi) AS montantTotal FROM commission";
                this.strCommande += " Inner Join assocsessioncaissecommission ON assocsessioncaissecommission.idCommission = commission.idCommission";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaissecommission.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "'";

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

        double IntfDalSessionAgence.getMontantTotalDureeAbonnement(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(dureeabonnement.prixTotal) AS montantTotal FROM dureeabonnement";
                this.strCommande += " Inner Join assocsessioncaissedureeabonnement ON assocsessioncaissedureeabonnement.numDureeAbonnement = dureeabonnement.numDureeAbonnement";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaissedureeabonnement.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "'";

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

        double IntfDalSessionAgence.getMontantTotalVoyageAbonnement(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(voyageabonnement.prixUnitaire * voyageabonnement.nbVoyageAbonnement) AS montantTotal FROM voyageabonnement";
                this.strCommande += " Inner Join assocsessioncaissevoyageabonnement ON assocsessioncaissevoyageabonnement.numVoyageAbonnement = voyageabonnement.numVoyageAbonnement";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaissevoyageabonnement.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "'";

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

        double IntfDalSessionAgence.getMontantTotalRecuEncaisser(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS montantTotal FROM recuencaisser";
                this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaisserecuencaisser.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "'";

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

        double IntfDalSessionAgence.getMontantTotalRecuEncaisserCheque(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS montantTotal FROM recuencaisser";
                this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaisserecuencaisser.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "' AND";
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

        double IntfDalSessionAgence.getMontantTotalRecuEncaisserEspece(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(recuencaisser.montantRecuEncaisser) AS montantTotal FROM recuencaisser";
                this.strCommande += " Inner Join assocsessioncaisserecuencaisser ON assocsessioncaisserecuencaisser.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaisserecuencaisser.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "' AND";
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

        double IntfDalSessionAgence.getMontantTotalRecuAD(string numSessionAgence)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS montantTotal FROM recuad";
                this.strCommande += " Inner Join assocsessioncaisserecuad ON assocsessioncaisserecuad.numRecuAD = recuad.numRecuAD";
                this.strCommande += " Inner Join sessioncaisse ON sessioncaisse.numSessionCaisse = assocsessioncaisserecuad.numSessionCaisse";
                this.strCommande += " WHERE sessioncaisse.numSessionAgence = '" + numSessionAgence + "'";

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
    }
}