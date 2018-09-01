using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalProforma
    /// </summary>
    public class ImplDalProforma : IntfDalProforma
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalProforma()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalProforma(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalProforma Members

        crlProforma IntfDalProforma.selectProforma(string numProforma)
        {
            #region variable
            crlProforma proforma = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implemntation
            if (numProforma != "") 
            {
                this.strCommande = "SELECT * FROM `proforma` WHERE (`numProforma`='" + numProforma + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            proforma = new crlProforma();
                            try
                            {
                                proforma.DateProforma = Convert.ToDateTime(this.reader["dateProforma"].ToString());
                            }
                            catch (Exception) { }
                            proforma.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            proforma.NumIndividu = this.reader["numIndividu"].ToString();
                            proforma.NumOrganisme = this.reader["numOrganisme"].ToString();
                            proforma.NumProforma = this.reader["numProforma"].ToString();
                            proforma.NumSociete = this.reader["numSociete"].ToString();
                            proforma.ModePaiement = this.reader["modePaiement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (proforma != null) 
                {
                    if (proforma.MatriculeAgent != "") 
                    {
                        proforma.agent = serviceAgent.selectAgent(proforma.MatriculeAgent);
                    }
                    if (proforma.NumIndividu != "") 
                    {
                        proforma.individu = serviceIndividu.selectIndividu(proforma.NumIndividu);
                    }
                    if (proforma.NumOrganisme != "") 
                    {
                        proforma.organisme = serviceOrganisme.selectOrganisme(proforma.NumOrganisme);
                    }
                    if (proforma.NumSociete != "") 
                    {
                        proforma.societe = serviceSociete.selectSociete(proforma.NumSociete);
                    }
                    if (proforma.NumProforma != "") 
                    {
                        proforma.billetCommande = serviceProforma.selectBilletCommandeProforma(proforma.NumProforma);
                        proforma.commissionDevis = serviceProforma.selectCommissionProforma(proforma.NumProforma);
                        proforma.dureeAbonnementDevis = serviceProforma.selectDureeAbonnementProforma(proforma.NumProforma);
                        proforma.voyageAbonnementDevis = serviceProforma.selectVoyageAbonnementProforma(proforma.NumProforma);
                        proforma.uSAbonnementNVDevis = serviceProforma.selectUSAbonnementNVDevis(proforma.NumProforma);
                    }
                }
            }
            #endregion

            return proforma;
        }

        string IntfDalProforma.insertProforma(crlProforma proforma)
        {
            #region declaration
            int nbInsert = 0;
            string numProforma = "";
            string numSociete = "";
            string numOrganisme = "";
            string numIndividu = "";
            string modePaiement = "";
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation
            if (proforma != null) 
            {
                if (proforma.agent != null)
                {
                    if (proforma.ModePaiement != "")
                    {
                        modePaiement = "'" + proforma.ModePaiement + "'";
                    }
                    else
                    {
                        modePaiement = "NULL";
                    }
                    if (proforma.NumIndividu != "")
                    {
                        numIndividu = "'" + proforma.NumIndividu + "'";
                    }
                    else
                    {
                        numIndividu = "NULL";
                    }
                    if (proforma.NumOrganisme != "")
                    {
                        numOrganisme = "'" + proforma.NumOrganisme + "'";
                    }
                    else
                    {
                        numOrganisme = "NULL";
                    }
                    if (proforma.NumSociete != "")
                    {
                        numSociete = "'" + proforma.NumSociete + "'";
                    }
                    else
                    {
                        numSociete = "NULL";
                    }

                    proforma.NumProforma = serviceProforma.getNumProforma(proforma.agent.agence.SigleAgence);
                    this.strCommande = "INSERT INTO `proforma` (`numProforma`,`numSociete`,`numOrganisme`,";
                    this.strCommande += " `numIndividu`,`dateProforma`,`matriculeAgent`,`modePaiement`)";
                    this.strCommande += " VALUES ('" + proforma.NumProforma + "'," + numSociete + ",";
                    this.strCommande += " " + numOrganisme + "," + numIndividu + ",'" + proforma.DateProforma.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " '" + proforma.MatriculeAgent + "'," + modePaiement + ")";

                    this.serviceConnectBase.openConnection();
                    nbInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nbInsert == 1) 
                    {
                        numProforma = proforma.NumProforma;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numProforma;
        }

        bool IntfDalProforma.updateProforma(crlProforma proforma)
        {
            #region declaration
            int nbUpdate = 0;
            bool isUpdate = false;
            string numSociete = "";
            string numOrganisme = "";
            string numIndividu = "";
            string modePaiement = "";
            #endregion

            #region implementation
            if (proforma != null) 
            {
                if (proforma.ModePaiement != "")
                {
                    modePaiement = "'" + proforma.ModePaiement + "'";
                }
                else
                {
                    modePaiement = "NULL";
                }
                if (proforma.NumIndividu != "")
                {
                    numIndividu = "'" + proforma.NumIndividu + "'";
                }
                else
                {
                    numIndividu = "NULL";
                }
                if (proforma.NumOrganisme != "")
                {
                    numOrganisme = "'" + proforma.NumOrganisme + "'";
                }
                else
                {
                    numOrganisme = "NULL";
                }
                if (proforma.NumSociete != "")
                {
                    numSociete = "'" + proforma.NumSociete + "'";
                }
                else
                {
                    numSociete = "NULL";
                }

                this.strCommande = "UPDATE `proforma` SET `numIndividu`=" + numIndividu + ",";
                this.strCommande += " `numOrganisme`=" + numOrganisme + ", `numSociete`=" + numSociete + ",";
                this.strCommande += " `dateProforma`='" + proforma.DateProforma.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `matriculeAgent`='" + proforma.MatriculeAgent + "',";
                this.strCommande += " `modePaiement`=" + modePaiement;
                this.strCommande += " WHERE `numProforma`='" + proforma.NumProforma + "'";

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

        bool IntfDalProforma.deleteProforma(crlProforma proforma)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalUSAbonnementNVDevis serviceUSAbonnementNVDevis = new ImplDalUSAbonnementNVDevis();
            #endregion

            #region implementation
            if (proforma != null) 
            {
                serviceGeneral.delete("billetcommande", "numProforma", proforma.NumProforma);
                serviceGeneral.delete("commissiondevis", "numProforma", proforma.NumProforma);
                serviceGeneral.delete("voyageabonnementdevis", "numProforma", proforma.NumProforma);
                serviceGeneral.delete("dureeabonnementdevis", "numProforma", proforma.NumProforma);
                serviceUSAbonnementNVDevis.deleteUSAbonnementNVDevisProforma(proforma.NumProforma);
                nombreDelete = serviceGeneral.delete("proforma", "numProforma", proforma.NumProforma);
                if (nombreDelete == 1)
                {
                    isDelete = true;
                }
            }
            #endregion

            return isDelete;
        }

        string IntfDalProforma.getNumProforma(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numProforma = "00001";
            string[] tempNumProforma = null;
            string strDate = "PF" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT proforma.numProforma AS maxNum FROM proforma";
            this.strCommande += " WHERE proforma.numProforma LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumProforma = reader["maxNum"].ToString().ToString().Split('/');
                        numProforma = tempNumProforma[tempNumProforma.Length - 1];
                    }
                    numTemp = double.Parse(numProforma) + 1;
                    if (numTemp < 10)
                        numProforma = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numProforma = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numProforma = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numProforma = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numProforma = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numProforma = strDate + "/" + sigleAgence + "/" + numProforma;
            #endregion

            return numProforma;
        }

        List<crlBilletCommande> IntfDalProforma.selectBilletCommandeProforma(string numProforma)
        {
            #region declaration
            crlBilletCommande tempBilletCommande = null;
            List<crlBilletCommande> billetCommande = null;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalProforma serviceProforma = new ImplDalProforma();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT * FROM `billetcommande` WHERE (`numProforma`='" + numProforma + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        billetCommande = new List<crlBilletCommande>();
                        while(this.reader.Read())
                        {
                            tempBilletCommande = new crlBilletCommande();
                            tempBilletCommande.NumBilletCommande = this.reader["numBilletCommande"].ToString();
                            tempBilletCommande.NumTrajet = this.reader["numTrajet"].ToString();

                            tempBilletCommande.NumProforma = this.reader["numProforma"].ToString();
                            try
                            {
                                tempBilletCommande.MontantBilletCommande = double.Parse(this.reader["montantBilletCommande"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                tempBilletCommande.NombreBilletCommande = int.Parse(this.reader["nombreBilletCommande"].ToString());
                            }
                            catch (Exception) { }
                            tempBilletCommande.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            tempBilletCommande.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();

                            billetCommande.Add(tempBilletCommande);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (billetCommande != null)
                {
                    for (int i = 0; i < billetCommande.Count; i++) 
                    {
                        if (billetCommande[i].NumTrajet != "")
                        {
                            billetCommande[i].trajet = serviceTrajet.selectTrajet(billetCommande[i].NumTrajet);
                        }
                        if (billetCommande[i].NumCalculCategorieBillet != "")
                        {
                            billetCommande[i].calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(billetCommande[i].NumCalculCategorieBillet);
                        }
                        if (billetCommande[i].NumCalculReductionBillet != "")
                        {
                            billetCommande[i].calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(billetCommande[i].NumCalculReductionBillet);
                        }
                    }
                        
                }

            }
            #endregion

            return billetCommande;
        }

        List<crlCommissionDevis> IntfDalProforma.selectCommissionProforma(string numProforma)
        {
            #region initialisation
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();

            List<crlCommissionDevis> CommissionDevis = null;
            crlCommissionDevis tempCommissionDevis = null;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT * FROM `commissiondevis` WHERE (`numProforma`='" + numProforma + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        CommissionDevis = new List<crlCommissionDevis>();
                        while (reader.Read())
                        {
                            tempCommissionDevis = new crlCommissionDevis();
                            tempCommissionDevis.Destination = reader["destination"].ToString();
                            try
                            {
                                tempCommissionDevis.FraisEnvoi = double.Parse(reader["fraisEnvoi"].ToString());
                            }
                            catch (Exception) { }
                            tempCommissionDevis.IdCommissionDevis = reader["idCommissionDevis"].ToString();
                            tempCommissionDevis.PieceJustificatif = reader["pieceJustificatif"].ToString();
                            try
                            {
                                tempCommissionDevis.Poids = double.Parse(reader["poids"].ToString());
                            }
                            catch (Exception) { }
                            tempCommissionDevis.TypeCommission = reader["typeCommission"].ToString();
                            tempCommissionDevis.NumDesignation = reader["numDesignation"].ToString();
                            try
                            {
                                tempCommissionDevis.Nombre = int.Parse(reader["nombre"].ToString());
                            }
                            catch (Exception) { }
                            tempCommissionDevis.NumTrajet = reader["numTrajet"].ToString();
                            tempCommissionDevis.NumProforma = reader["numProforma"].ToString();
                            tempCommissionDevis.NumExpediteur = reader["numExpediteur"].ToString();
                            tempCommissionDevis.NumRecepteur = reader["numRecepteur"].ToString();

                            CommissionDevis.Add(tempCommissionDevis);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (CommissionDevis != null)
                {
                    for (int i = 0; i < CommissionDevis.Count; i++) 
                    {
                        if (CommissionDevis[i].NumDesignation != "")
                        {
                            CommissionDevis[i].designationCommission = serviceDesignationCommission.selectDesignationCommission(CommissionDevis[i].NumDesignation);
                        }
                        if (CommissionDevis[i].NumTrajet != "")
                        {
                            CommissionDevis[i].trajet = serviceTrajet.selectTrajet(CommissionDevis[i].NumTrajet);
                        }

                        if (CommissionDevis[i].NumExpediteur != "")
                        {
                            CommissionDevis[i].expediteur = serviceClient.selectClient(CommissionDevis[i].NumExpediteur);
                        }
                        if (CommissionDevis[i].NumRecepteur != "")
                        {
                            CommissionDevis[i].recepteur = serviceReceptionnaire.selectPersonne(CommissionDevis[i].NumRecepteur);
                        }
                    }
                        

                }
            }
            #endregion

            return CommissionDevis;
        }

        List<crlDureeAbonnementDevis> IntfDalProforma.selectDureeAbonnementProforma(string numProforma)
        {
            #region declaration
            List<crlDureeAbonnementDevis> dureeAbonnementDevis = null;
            crlDureeAbonnementDevis tempDureeAbonnementDevis = null;

            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT * FROM `dureeabonnementdevis` WHERE (`numProforma`='" + numProforma + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        dureeAbonnementDevis = new List<crlDureeAbonnementDevis>();
                        while(this.reader.Read())
                        {
                            tempDureeAbonnementDevis = new crlDureeAbonnementDevis();


                            tempDureeAbonnementDevis.NumProforma = this.reader["numProforma"].ToString();
                            tempDureeAbonnementDevis.NumDureeAbonnementDevis = this.reader["numDureeAbonnementDevis"].ToString();
                            tempDureeAbonnementDevis.NumTrajet = this.reader["numTrajet"].ToString();
                            try
                            {
                                tempDureeAbonnementDevis.PrixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                tempDureeAbonnementDevis.PrixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                tempDureeAbonnementDevis.ValideAu = Convert.ToDateTime(this.reader["valideAu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                tempDureeAbonnementDevis.ValideDu = Convert.ToDateTime(this.reader["valideDu"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempDureeAbonnementDevis.Zone = this.reader["zone"].ToString();
                            try
                            {
                                tempDureeAbonnementDevis.NombreDureeAbonnement = int.Parse(this.reader["nombreDureeAbonnement"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempDureeAbonnementDevis.NumAbonnement = this.reader["numAbonnement"].ToString();
                            tempDureeAbonnementDevis.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            tempDureeAbonnementDevis.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();

                            dureeAbonnementDevis.Add(tempDureeAbonnementDevis);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (dureeAbonnementDevis != null)
                {
                    for (int i = 0; i < dureeAbonnementDevis.Count; i++)
                    {
                        if (dureeAbonnementDevis[i].NumTrajet != "")
                        {
                            dureeAbonnementDevis[i].trajet = serviceTrajet.selectTrajet(dureeAbonnementDevis[i].NumTrajet);
                        }
                        if (dureeAbonnementDevis[i].Zone != "")
                        {
                            dureeAbonnementDevis[i].zoneObj = serviceZone.selectZone(dureeAbonnementDevis[i].Zone);
                        }
                        if (dureeAbonnementDevis[i].NumCalculCategorieBillet != "")
                        {
                            dureeAbonnementDevis[i].calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(dureeAbonnementDevis[i].NumCalculCategorieBillet);
                        }
                        if (dureeAbonnementDevis[i].NumCalculReductionBillet != "")
                        {
                            dureeAbonnementDevis[i].calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(dureeAbonnementDevis[i].NumCalculReductionBillet);
                        }
                    }
                        
                }
            }
            #endregion

            return dureeAbonnementDevis;
        }

        List<crlVoyageAbonnementDevis> IntfDalProforma.selectVoyageAbonnementProforma(string numProforma)
        {
            #region declaration
            List<crlVoyageAbonnementDevis> voyageAbonnementDevis = null;
            crlVoyageAbonnementDevis tempVoyageAbonnementDevis = null;

            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalZone serviceZone = new ImplDalZone();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT * FROM `voyageabonnementdevis` WHERE `numProforma`='" + numProforma + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        voyageAbonnementDevis = new List<crlVoyageAbonnementDevis>();
                        while(this.reader.Read())
                        {
                            tempVoyageAbonnementDevis = new crlVoyageAbonnementDevis();


                            try
                            {
                                tempVoyageAbonnementDevis.NbVoyageAbonnement = int.Parse(this.reader["nbVoyageAbonnement"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            
                            tempVoyageAbonnementDevis.NumProforma = this.reader["numProforma"].ToString();
                            tempVoyageAbonnementDevis.NumTrajet = this.reader["numTrajet"].ToString();
                            tempVoyageAbonnementDevis.NumVoyageAbonnementDevis = this.reader["numVoyageAbonnementDevis"].ToString();
                            try
                            {
                                tempVoyageAbonnementDevis.PrixUnitaire = double.Parse(this.reader["prixUnitaire"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempVoyageAbonnementDevis.Zone = this.reader["zone"].ToString();
                            tempVoyageAbonnementDevis.NumAbonnement = this.reader["numAbonnement"].ToString();
                            tempVoyageAbonnementDevis.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            tempVoyageAbonnementDevis.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();

                            voyageAbonnementDevis.Add(tempVoyageAbonnementDevis);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (voyageAbonnementDevis != null)
                {
                    for (int i = 0; i < voyageAbonnementDevis.Count; i++)
                    {
                        if (voyageAbonnementDevis[i].NumTrajet != "")
                        {
                            voyageAbonnementDevis[i].trajet = serviceTrajet.selectTrajet(voyageAbonnementDevis[i].NumTrajet);
                        }
                        if (voyageAbonnementDevis[i].Zone != "")
                        {
                            voyageAbonnementDevis[i].zoneObj = serviceZone.selectZone(voyageAbonnementDevis[i].Zone);
                        }
                        if (voyageAbonnementDevis[i].NumCalculCategorieBillet != "")
                        {
                            voyageAbonnementDevis[i].calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(voyageAbonnementDevis[i].NumCalculCategorieBillet);
                        }
                        if (voyageAbonnementDevis[i].NumCalculReductionBillet != "")
                        {
                            voyageAbonnementDevis[i].calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(voyageAbonnementDevis[i].NumCalculReductionBillet);
                        }
                    }
                        
                }
            }
            #endregion

            return voyageAbonnementDevis;
        }

        List<crlUSAbonnementNVDevis> IntfDalProforma.selectUSAbonnementNVDevis(string numProforma)
        {
            #region declaration
            List<crlUSAbonnementNVDevis> USAbonnementNVDevis = null;
            crlUSAbonnementNVDevis tempUSAbonnementNVDevis = null;

            IntfDalUSLieu serviceUSLieu = new ImplDalUSLieu();
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            IntfDalUSInfoPasse serviceUSInfoPasse = new ImplDalUSInfoPasse();
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT * FROM `usabonnementnvdevis` WHERE `numProforma`='" + numProforma + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        USAbonnementNVDevis = new List<crlUSAbonnementNVDevis>();
                        while (this.reader.Read())
                        {
                            tempUSAbonnementNVDevis = new crlUSAbonnementNVDevis();


                            try
                            {
                                tempUSAbonnementNVDevis.MontantNV = double.Parse(this.reader["montantNV"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            tempUSAbonnementNVDevis.NumProforma = this.reader["numProforma"].ToString();
                            
                            tempUSAbonnementNVDevis.NumAbonnement = this.reader["numAbonnement"].ToString();
                            
                            tempUSAbonnementNVDevis.NumAbonnementNVDevis = this.reader["numAbonnementNVDevis"].ToString();
                            tempUSAbonnementNVDevis.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            tempUSAbonnementNVDevis.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                            tempUSAbonnementNVDevis.NumZoneD = this.reader["numZoneD"].ToString();
                            tempUSAbonnementNVDevis.NumZoneF = this.reader["numZoneF"].ToString();
                            try
                            {
                                tempUSAbonnementNVDevis.MontantCarte = double.Parse(this.reader["montantCarte"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            tempUSAbonnementNVDevis.NumCarte = this.reader["numCarte"].ToString();
                            tempUSAbonnementNVDevis.NumInfoPasse = this.reader["numInfoPasse"].ToString();
                            try
                            {
                                tempUSAbonnementNVDevis.PrixUnitaireNV = double.Parse(this.reader["prixUnitaireNV"].ToString());
                            }catch(Exception){}

                            tempUSAbonnementNVDevis.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            try
                            {
                                tempUSAbonnementNVDevis.NombreVoyage = int.Parse(this.reader["nombreVoyage"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            
                            if (tempUSAbonnementNVDevis.NumZoneD != "")
                            {
                                tempUSAbonnementNVDevis.zoneD = serviceUSZone.selectUSZone(tempUSAbonnementNVDevis.NumZoneD);
                            }
                            if (tempUSAbonnementNVDevis.NumZoneF != "")
                            {
                                tempUSAbonnementNVDevis.zoneF = serviceUSZone.selectUSZone(tempUSAbonnementNVDevis.NumZoneF);
                            }
                            if (tempUSAbonnementNVDevis.NumInfoPasse != "")
                            {
                                tempUSAbonnementNVDevis.infoPasse = serviceUSInfoPasse.selectUSInfoPasse(tempUSAbonnementNVDevis.NumInfoPasse);
                            }
                            


                            USAbonnementNVDevis.Add(tempUSAbonnementNVDevis);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                

            }
            #endregion

            return USAbonnementNVDevis;
        }

        double IntfDalProforma.getPrixTotalDureeAbonnementProforma(string numProforma)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT Sum(dureeabonnementdevis.prixTotal *";
                this.strCommande += " dureeabonnementdevis.nombreDureeAbonnement) AS totalPrix";
                this.strCommande += " FROM dureeabonnementdevis";
                this.strCommande += " WHERE dureeabonnementdevis.numProforma = '" + numProforma + "'";

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
                                prixTotal = double.Parse(this.reader["totalPrix"].ToString());
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

            return prixTotal;
        }

        double IntfDalProforma.getPrixTotalVoyageAbonnementProforma(string numProforma)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT Sum(voyageabonnementdevis.prixUnitaire *";
                this.strCommande += " voyageabonnementdevis.nbVoyageAbonnement) AS totalPrix";
                this.strCommande += " FROM voyageabonnementdevis";
                this.strCommande += " WHERE voyageabonnementdevis.numProforma = '" + numProforma + "'";

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
                                prixTotal = double.Parse(this.reader["totalPrix"].ToString());
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

            return prixTotal;
        }

        double IntfDalProforma.getPrixTotalBilletCommandeProforma(string numProforma)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT Sum(billetcommande.montantBilletCommande *";
                this.strCommande += " billetcommande.nombreBilletCommande) AS totalPrix";
                this.strCommande += " FROM billetcommande";
                this.strCommande += " WHERE billetcommande.numProforma = '" + numProforma + "'";

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
                                prixTotal = double.Parse(this.reader["totalPrix"].ToString());
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

            return prixTotal;
        }

        double IntfDalProforma.getPrixTotalCommissionDevisProforma(string numProforma)
        {
            
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT Sum(commissiondevis.fraisEnvoi) AS totalPrix";
                this.strCommande += " FROM commissiondevis";
                this.strCommande += " WHERE commissiondevis.numProforma = '" + numProforma + "'";

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
                                prixTotal = double.Parse(this.reader["totalPrix"].ToString());
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

            return prixTotal;
        }

        double IntfDalProforma.getPrixTotalAbonnementUSNVProforma(string numProforma)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                this.strCommande = "SELECT Sum(usabonnementnvdevis.montantNV) AS totalPrix";
                this.strCommande += " FROM usabonnementnvdevis";
                this.strCommande += " WHERE usabonnementnvdevis.numProforma = '" + numProforma + "'";

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
                                prixTotal = double.Parse(this.reader["totalPrix"].ToString());
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

            return prixTotal;
        }

        double IntfDalProforma.getPrixTotalAbonnementUSCarteProforma(string numProforma)
        {
            #region declaration
            double prixTotal = 0;
            #endregion

            #region implementation
            
            if (numProforma != "")
            {
                this.strCommande = "SELECT Sum(usabonnementnvdevis.montantCarte) AS totalPrix";
                this.strCommande += " FROM usabonnementnvdevis";
                this.strCommande += " WHERE usabonnementnvdevis.numProforma = '" + numProforma + "'";

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
                                prixTotal = double.Parse(this.reader["totalPrix"].ToString());
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

            return prixTotal;
        }

        double IntfDalProforma.getPrixTotalProforma(string numProforma)
        {
            #region declaration
            double montantTotal = 0;
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation
            if (numProforma != "")
            {
                montantTotal += serviceProforma.getPrixTotalBilletCommandeProforma(numProforma);
                montantTotal += serviceProforma.getPrixTotalCommissionDevisProforma(numProforma);
                montantTotal += serviceProforma.getPrixTotalDureeAbonnementProforma(numProforma);
                montantTotal += serviceProforma.getPrixTotalVoyageAbonnementProforma(numProforma);
                montantTotal += serviceProforma.getPrixTotalAbonnementUSCarteProforma(numProforma);
                montantTotal += serviceProforma.getPrixTotalAbonnementUSNVProforma(numProforma);
            }
            #endregion

            return montantTotal;
        }
        #endregion

        #region IntfDalProforma Members

        double IntfDalProforma.getPrixTotalBilletCommandeProforma(crlProforma proforma)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (proforma != null)
            {
                if (proforma.billetCommande != null)
                {
                    for (int i = 0; i < proforma.billetCommande.Count; i++)
                    {
                        try
                        {
                            montantTotal += (proforma.billetCommande[i].MontantBilletCommande * proforma.billetCommande[i].NombreBilletCommande);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                
            }
            #endregion

            return montantTotal;
        }

        double IntfDalProforma.getPrixTotalCommissionDevisProforma(crlProforma proforma)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (proforma != null)
            {
                if (proforma.commissionDevis != null)
                {
                    for (int i = 0; i < proforma.commissionDevis.Count; i++)
                    {
                        try
                        {
                            montantTotal += (proforma.commissionDevis[i].FraisEnvoi);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }


            }
            #endregion

            return montantTotal;
        }

        double IntfDalProforma.getPrixTotalDureeAbonnementProforma(crlProforma proforma)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (proforma != null)
            {
                if (proforma.dureeAbonnementDevis != null)
                {
                    for (int i = 0; i < proforma.dureeAbonnementDevis.Count; i++)
                    {
                        try
                        {
                            montantTotal += (proforma.dureeAbonnementDevis[i].NombreDureeAbonnement * proforma.dureeAbonnementDevis[i].PrixTotal);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            #endregion

            return montantTotal;
        }

        double IntfDalProforma.getPrixTotalVoyageAbonnementProforma(crlProforma proforma)
        {
            #region declaration
            double montantTotal = 0;
            #endregion

            #region implementation
            if (proforma != null)
            {
                if (proforma.voyageAbonnementDevis != null)
                {
                    for (int i = 0; i < proforma.voyageAbonnementDevis.Count; i++)
                    {
                        try
                        {
                            montantTotal += (proforma.voyageAbonnementDevis[i].PrixUnitaire * proforma.voyageAbonnementDevis[i].NbVoyageAbonnement);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            #endregion

            return montantTotal;
        }
        #endregion

        #region insert to grid
        void IntfDalProforma.insertToGridBillet(GridView gridView, List<crlBilletCommande> billets)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            gridView.DataSource = serviceProforma.getDataTableBillet(billets);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableBillet(List<crlBilletCommande> billets)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("trajet", typeof(string));
            dataTable.Columns.Add("prix", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("prixTotal", typeof(string));
            dataTable.Columns.Add("categorie", typeof(string));
            DataRow dr;
            #endregion

            if (billets != null)
            {
                if (billets.Count > 0)
                {
                    for (int i = 0; i < billets.Count; i++ )
                    {
                         dr = dataTable.NewRow();

                        if (billets[i].trajet != null)
                        {
                            dr["trajet"] = billets[i].trajet.villeD.NomVille + "-" + billets[i].trajet.villeF.NomVille;
                        }
                        dr["prix"] = serviceGeneral.separateurDesMilles(billets[i].MontantBilletCommande.ToString("0")) + "Ar";
                       
                        dr["nombre"] = billets[i].NombreBilletCommande.ToString("0");

                        dr["prixTotal"] = serviceGeneral.separateurDesMilles((billets[i].MontantBilletCommande * billets[i].NombreBilletCommande).ToString("0")) + "Ar";

                        if (billets[i].calculCategorieBillet != null) 
                        {
                            dr["categorie"] = billets[i].calculCategorieBillet.IndicateurPrixBillet;
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }

            #endregion

            return dataTable;
        }

        void IntfDalProforma.insertToGridBillet(GridView gridView, string param, string paramLike, string valueLike, string numProforma)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT billetcommande.numTrajet, billetcommande.montantBilletCommande, billetcommande.nombreBilletCommande,";
            this.strCommande += " billetcommande.numCalculCategorieBillet, billetcommande.numBilletCommande, billetcommande.numProforma,";
            this.strCommande += " billetcommande.numCalculReductionBillet, calculcategoriebillet.indicateurPrixBillet FROM proforma";
            this.strCommande += " Inner Join billetcommande ON billetcommande.numProforma = proforma.numProforma";
            this.strCommande += " Inner Join calculcategoriebillet ON calculcategoriebillet.numCalculCategorieBillet = billetcommande.numCalculCategorieBillet";
            this.strCommande += " WHERE proforma.numProforma = '" + numProforma + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableBillet(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableBillet(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            crlTrajet trajet = null;
            double montantUnitaire = 0;
            int nombreBillet = 0;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numBilletCommande", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));
            dataTable.Columns.Add("prix", typeof(string));
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("prixTotal", typeof(string));
            dataTable.Columns.Add("categorie", typeof(string));
            DataRow dr;
            #endregion

            if (strRqst != "")
            {
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(strRqst);

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while(this.reader.Read())
                        {
                            dr = dataTable.NewRow();

                            dr["numBilletCommande"] = this.reader["numBilletCommande"].ToString();

                            trajet = serviceTrajet.selectTrajet(this.reader["numTrajet"].ToString());
                            if (trajet != null)
                            {
                                dr["trajet"] = trajet.villeD.NomVille + "-" + trajet.villeF.NomVille;
                            }
                            else 
                            {
                                dr["trajet"] = this.reader["numTrajet"].ToString();
                            }
                            try
                            {
                                montantUnitaire = double.Parse(this.reader["montantBilletCommande"].ToString());
                            }
                            catch (Exception) { }

                            dr["prix"] = serviceGeneral.separateurDesMilles(montantUnitaire.ToString("0")) + "Ar";

                            try 
                            {
                                nombreBillet = int.Parse(this.reader["nombreBilletCommande"].ToString());
                            }
                            catch (Exception) { }

                            dr["nombre"] = nombreBillet.ToString("0");

                            dr["prixTotal"] = serviceGeneral.separateurDesMilles((nombreBillet * montantUnitaire).ToString("0")) + "Ar";


                            dr["categorie"] = this.reader["indicateurPrixBillet"].ToString();

                            trajet = null;
                            montantUnitaire = 0;
                            nombreBillet = 0;
                            dataTable.Rows.Add(dr);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }

            #endregion

            return dataTable;
        }

        void IntfDalProforma.insertToGridProforma(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT proforma.numProforma, proforma.numSociete, proforma.numOrganisme,";
            this.strCommande += " proforma.numIndividu, proforma.dateProforma, proforma.matriculeAgent,";
            this.strCommande += " proforma.modePaiement FROM proforma";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join individu ON individu.numIndividu = proforma.numIndividu";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableProforma(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableProforma(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string telephone = "";
            string mobile = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProforma", typeof(string));
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

                        dr["numProforma"] = reader["numProforma"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (individu != null)
                        {
                            dr["client"] = individu.PrenomIndividu + " " + individu.NomIndividu;

                            dr["adresse"] = individu.Adresse;

                            dr["contact"] = individu.TelephoneFixeIndividu + " / " + individu.TelephoneMobileIndividu;

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

                        individu = null;
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


        void IntfDalProforma.insertToGridProformaBilletCommission(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT proforma.numProforma, proforma.numSociete, proforma.numOrganisme,";
            this.strCommande += " proforma.numIndividu, proforma.dateProforma, proforma.matriculeAgent,";
            this.strCommande += " proforma.modePaiement FROM proforma";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join billetcommande ON billetcommande.numProforma = proforma.numProforma";
            this.strCommande += " Left Join commissiondevis ON commissiondevis.numProforma = proforma.numProforma";
            this.strCommande += " Left Join individu ON individu.numIndividu = proforma.numIndividu";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numProforma = proforma.numProforma";
            this.strCommande += " Left Join bondecommande ON bondecommande.numProforma = proforma.numProforma";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " (billetcommande.numProforma IS NOT NULL  OR";
            this.strCommande += " commissiondevis.numProforma IS NOT NULL) AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numProforma IS NULL  AND";
            this.strCommande += " bondecommande.numProforma IS NULL";
            this.strCommande += " GROUP BY proforma.numProforma";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableProformaBilletCommission(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableProformaBilletCommission(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string telephone = "";
            string mobile = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProforma", typeof(string));
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

                        dr["numProforma"] = reader["numProforma"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (individu != null)
                        {
                            dr["client"] = individu.PrenomIndividu + " " + individu.NomIndividu;

                            dr["adresse"] = individu.Adresse;

                            dr["contact"] = individu.TelephoneFixeIndividu + " / " + individu.TelephoneMobileIndividu;

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

                        individu = null;
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

        void IntfDalProforma.insertToGridProformaADTANV(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT proforma.numProforma, proforma.numSociete, proforma.numOrganisme,";
            this.strCommande += " proforma.numIndividu, proforma.dateProforma, proforma.matriculeAgent,";
            this.strCommande += " proforma.modePaiement FROM proforma";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join individu ON individu.numIndividu = proforma.numIndividu";
            this.strCommande += " Left Join voyageabonnementdevis ON voyageabonnementdevis.numProforma = proforma.numProforma";
            this.strCommande += " Left Join dureeabonnementdevis ON dureeabonnementdevis.numProforma = proforma.numProforma";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numProforma = proforma.numProforma";
            this.strCommande += " Left Join bondecommande ON bondecommande.numProforma = proforma.numProforma";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " (dureeabonnementdevis.numProforma IS NOT NULL  OR";
            this.strCommande += " voyageabonnementdevis.numProforma IS NOT NULL) AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numProforma IS NULL  AND";
            this.strCommande += " bondecommande.numProforma IS NULL";
            this.strCommande += " GROUP BY proforma.numProforma";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableProformaDTANV(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableProformaDTANV(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string telephone = "";
            string mobile = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProforma", typeof(string));
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

                        dr["numProforma"] = reader["numProforma"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (individu != null)
                        {
                            dr["client"] = individu.PrenomIndividu + " " + individu.NomIndividu;

                            dr["adresse"] = individu.Adresse;

                            dr["contact"] = individu.TelephoneFixeIndividu + " / " + individu.TelephoneMobileIndividu;

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

                        individu = null;
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


        void IntfDalProforma.insertToGridProformaNonPaie(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT proforma.numProforma, proforma.numSociete, proforma.numOrganisme,";
            this.strCommande += " proforma.numIndividu, proforma.dateProforma, proforma.matriculeAgent,";
            this.strCommande += " proforma.modePaiement FROM proforma";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join individu ON individu.numIndividu = proforma.numIndividu";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numProforma = proforma.numProforma";
            this.strCommande += " Left Join bondecommande ON bondecommande.numProforma = proforma.numProforma";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numProforma IS NULL  AND";
            this.strCommande += " bondecommande.numProforma IS NULL";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableProformaNonPaie(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableProformaNonPaie(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string telephone = "";
            string mobile = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProforma", typeof(string));
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

                        dr["numProforma"] = reader["numProforma"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (individu != null)
                        {
                            dr["client"] = individu.PrenomIndividu + " " + individu.NomIndividu;

                            dr["adresse"] = individu.Adresse;

                            dr["contact"] = individu.TelephoneFixeIndividu + " / " + individu.TelephoneMobileIndividu;

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

                        individu = null;
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

        #region IntfDalProforma Members


        void IntfDalProforma.insertToGridProformaUSNVDT(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT proforma.numProforma, proforma.numSociete, proforma.numOrganisme,";
            this.strCommande += " proforma.numIndividu, proforma.dateProforma, proforma.matriculeAgent,";
            this.strCommande += " proforma.modePaiement FROM proforma";
            this.strCommande += " Left Join societe ON societe.numSociete = proforma.numSociete";
            this.strCommande += " Left Join individu ON individu.numIndividu = proforma.numIndividu";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proforma.numOrganisme";
            this.strCommande += " Left Join usabonnementnvdevis ON usabonnementnvdevis.numProforma = proforma.numProforma";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numProforma = proforma.numProforma";
            this.strCommande += " Left Join bondecommande ON bondecommande.numProforma = proforma.numProforma";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " (usabonnementnvdevis.numProforma IS NOT NULL) AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numProforma IS NULL  AND";
            this.strCommande += " bondecommande.numProforma IS NULL";
            this.strCommande += " GROUP BY proforma.numProforma";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableProformaDTANV(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableProformaUSNVDT(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string telephone = "";
            string mobile = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProforma", typeof(string));
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

                        dr["numProforma"] = reader["numProforma"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (individu != null)
                        {
                            dr["client"] = individu.PrenomIndividu + " " + individu.NomIndividu;

                            dr["adresse"] = individu.Adresse;

                            dr["contact"] = individu.TelephoneFixeIndividu + " / " + individu.TelephoneMobileIndividu;

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

                        individu = null;
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



        void IntfDalProforma.insertToGridProformaCarteSansAbonnement(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent)
        {
            #region declaration
            IntfDalProforma serviceProforma = new ImplDalProforma();
            #endregion

            #region implementation

            this.strCommande = "SELECT proforma.dateProforma, proforma.matriculeAgent,";
            this.strCommande += " proforma.numProforma, agent.nomAgent, agent.prenomAgent";
            this.strCommande += " FROM proforma";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = proforma.matriculeAgent";
            this.strCommande += " Inner Join usabonnementnvdevis ON usabonnementnvdevis.numProforma = proforma.numProforma";
            this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numProforma = proforma.numProforma";
            this.strCommande += " WHERE proforma.numSociete IS NULL  AND";
            this.strCommande += " proforma.numOrganisme IS NULL  AND";
            this.strCommande += " proforma.numIndividu IS NULL  AND";
            this.strCommande += " usabonnementnvdevis.numAbonnementNVDevis IS NOT NULL  AND";
            this.strCommande += " assocrecuencaisserproformabondecommande.numProforma IS NULL  AND";
            this.strCommande += " agent.matriculeAgent = '" + matriculeAgent + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceProforma.getDataTableProformaCarteSansAbonnement(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalProforma.getDataTableProformaCarteSansAbonnement(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProforma", typeof(string));
            dataTable.Columns.Add("dateProforma", typeof(DateTime));
            dataTable.Columns.Add("agent", typeof(string));
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

                        dr["numProforma"] = reader["numProforma"].ToString();

                        dr["dateProforma"] = Convert.ToDateTime(this.reader["dateProforma"].ToString());

                        dr["agent"] = reader["prenomAgent"].ToString() + " " + reader["nomAgent"].ToString();

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