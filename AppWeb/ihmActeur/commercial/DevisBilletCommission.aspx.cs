using AppRessources.Ressources;
using arc.utile;
using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.commercial
{
    public partial class DevisBilletCommission : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalModePaiement serviceModePaiement = null;
        IntfDalVille serviceVille = null;
        IntfDalTrajet serviceTrajet = null;
        IntfDalIndividu serviceIndividu = null;
        IntfDalBillet serviceBillet = null;
        IntfDalCalculPrixBillet serviceCalculPrixBillet = null;
        IntfDalProforma serviceProforma = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalCalculCategorieBillet serviceCalculCategorieBillet = null;
        IntfDalBilletCommande serviceBilletCommande = null;
        IntfDalSociete serviceSociete = null;
        IntfDalOrganisme serviceOrganisme = null;
        IntfDalReceptionnaire serviceReceptionnaire = null;
        IntfDalTypeCommission serviceTypeCommission = null;
        IntfDalDesignationCommission serviceDesignationCommission = null;
        IntfDalCommissionDevis serviceCommissionDevis = null;
        IntfDalCommission serviceCommission = null;
        IntfDalSessionCaisse serviceSessionCaisse = null;
        IntfDalDureeAbonnement serviceDureeAbonnement = null;
        IntfDalVoyageAbonnement serviceVoyageAbonnement = null;
        IntfDalAbonnement serviceAbonnement = null;
        IntfDalDureeAbonnementDevis serviceDureeAbonnementDevis = null;
        IntfDalVoyageAbonnementDevis serviceVoyageAbonnementDevis = null;
        IntfDalRecuEncaisser serviceRecuEncaisser = null;
        IntfDalBonDeCommande serviceBonDeCommande = null;
        IntfDalLien serviceLien = null;
        IntfDalClient serviceClient = null;


        crlAgent agent = null;
        #endregion

        #region event page
        protected void Page_Load(object sender, EventArgs e)
        {
            #region initialisation ressource
            serviceRessource = new ImplDalServiceRessource();
            serviceLien = new ImplDalLien();
            #endregion

            #region verification
            this.verification();
            #endregion

            #region initialisation
            serviceModePaiement = new ImplDalModePaiement();
            serviceVille = new ImplDalVille();
            serviceTrajet = new ImplDalTrajet();
            serviceIndividu = new ImplDalIndividu();
            serviceBillet = new ImplDalBillet();
            serviceCalculPrixBillet = new ImplDalCalculPrixBillet();
            serviceProforma = new ImplDalProforma();
            serviceGeneral = new ImplDalGeneral();
            serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            serviceBilletCommande = new ImplDalBilletCommande();
            serviceSociete = new ImplDalSociete();
            serviceOrganisme = new ImplDalOrganisme();
            serviceReceptionnaire = new ImplDalReceptionnaire();
            serviceTypeCommission = new ImplDalTypeCommission();
            serviceDesignationCommission = new ImplDalDesignationCommission();
            serviceCommissionDevis = new ImplDalCommissionDevis();
            serviceCommission = new ImplDalCommission();
            serviceSessionCaisse = new ImplDalSessionCaisse();
            serviceDureeAbonnement = new ImplDalDureeAbonnement();
            serviceVoyageAbonnement = new ImplDalVoyageAbonnement();
            serviceAbonnement = new ImplDalAbonnement();
            serviceDureeAbonnementDevis = new ImplDalDureeAbonnementDevis();
            serviceVoyageAbonnementDevis = new ImplDalVoyageAbonnementDevis();
            serviceRecuEncaisser = new ImplDalRecuEncaisser();
            serviceBonDeCommande = new ImplDalBonDeCommande();
            serviceClient = new ImplDalClient();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                serviceModePaiement.loadDddlModePaiement(ddlModePaiement, "Abonnement;Commande");
                try
                {
                    ddlModePaiement.SelectedValue = "Espèce";
                }
                catch (Exception)
                {
                }
                this.initialisePanelBonCommande();
                this.initialisePanelCheque();

                Panel_AbonnementDureeTemps.Visible = false;
                Panel_AbonnementNbVoyage.Visible = false;
                Panel_FormulaireBillet.Visible = true;

                Panel_Societe.Visible = false;
                Panel_Organisme.Visible = false;
                Panel_Individu.Visible = false;

                this.initialiseErrorMessage();
                //serviceModePaiement.loadDddlModePaiement(ddlModePaiement);
                serviceVille.loadDdlVille(ddlVilleDepart);
                serviceCalculCategorieBillet.loadDdlCulculeCategorieBillet(ddlCalculePrixBillet);

                serviceTypeCommission.loadDddlTypeCommission(ddlTypeCommission);
                serviceVille.loadDdlVille(ddlVilleDepartCommission);

                this.loadDdlDesignation();
                this.initialiseFormulaireTrajetCommission();

                this.initialiseFormulaireCommission();
                //serviceCalculPrixBillet.loadDdlCulculePrixBillet(ddlCalculePrixBillet);
                this.initialiseGridBilletProforma();
                this.initialiseGridCommissionDevis();
                this.initialiseFormulaireTrajet();
                this.initialiseGridProforma();
                this.initialiseGridIndividu();
                this.initialiseGridExpediteur();
                this.initialiseGridRecepteur();
                this.initialiseGridIndividuListe();
                this.initialiseGridSociete();
                this.initialiseGridOrganisme();
                hfNumProforma.Value = "";
                hfNumBilletCommande.Value = "";
                btnModifier.Enabled = false;
                btnModifierCommissionDevis.Enabled = false;

                this.initialiseOrganisme();
                this.initialiseSociete();
                this.initialiseIndividu();
            }
            #endregion
        }
        #endregion

        #region methode page
        private void verification()
        {
            if (!serviceRessource.testBase(serviceRessource.getDefaultStrConnection()))
            {
                Session.Clear();
                Response.Redirect("~/ihmActeur/ConfigurationBD.aspx");
            }
            else
            {
                if (Session["agent"] != null)
                {
                    agent = (crlAgent)Session["agent"];
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "035"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseErrorMessage()
        {
            #region billet
            ddlCalculePrixBillet_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.billetCategorieNonVide;
            ddlDestination_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.billetVilleDestinationNonVide;
            ddlVilleDepart_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.billetVilleDepartNonVide;
            TextPrixBillet_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.billetPrixNonVide;
            TextNombreBillet_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.billetNombreNonVide;
            TextPrixTotal_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.billetPrixTotalNonVide;
            #endregion

            #region commission
            TextNombreCommission_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.commissionNombreNonVide;
            TextPoidsCommission_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.commissionPoidsNonVide;
            TextFraisCommission_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.commissionFraisNonVide;
            TextPieceJustificatifCommission_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.commissionPieceJustificatifNonVide;
            #endregion

            #region client
            TextClientNom_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.clientNomNonVide;
            TextClientAdesse_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.clientAdresseNonVide;
            TextClientCin_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.clientCINNonVide;
            #endregion

            #region individu
            TextNomClient_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.individuNomNonVide;
            TextAdresseClient_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.individuAdresseNonVide;
            TextCinClient_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.individuCINNonVide;
            #endregion

            #region societe
            TextNomSociete_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.societeNomNonVide;
            TextAdresseRespSociete_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.societeAdresseResponsableNonVide;
            TextAdresseSociete_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.societeAdresseNonVide;
            TextMailSociete_RegularExpressionValidator.ErrorMessage = ReBilletCommissionDevis.societeMailNonValide;
            TextSecteurSociete_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.societeSecteurNonVide;
            TextNomResponsableSociete_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.societeNomResponsableNonVide;
            TextCinRespSociete_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.societeCINRespNonVide;
            TextMailRespSociete_RegularExpressionValidator.ErrorMessage = ReBilletCommissionDevis.societeMailResponsableNonValide;
            #endregion

            #region organisme
            TextNomOrganisme_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.organismeNonNonVide;
            TextAdresseOrganisme_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.organismeAdresseNonVide;
            TextMailOrganisme_RegularExpressionValidator.ErrorMessage = ReBilletCommissionDevis.organismeMailNonValide;
            TextNomRespOrganisme_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.organismeNonRespNonVide;
            TextAdresseRespOrganisme_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.organismeAdresseRespNonVide;
            TextCinRespOrganisme_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.organismeCINRespNonVide;
            TextMailRespOrganisme_RegularExpressionValidator.ErrorMessage = ReBilletCommissionDevis.organismeMailRespNonVide;
            #endregion

            #region expediteur
            TextNomExpediteur_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.expediteurNomNonVide;
            TextCINExpediteur_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.expediteurCINNonVide;
            #endregion

            #region recepteur
            TextNomRecepteur_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.receptionnaireNomNonVide;
            #endregion

            #region voyageAbonnement
            TextNbBillet_RequiredFieldValidator.ErrorMessage = ReBilletCommissionDevis.nbBilletNonVide;
            #endregion

            #region cheque
            TextDateCheque_RequiredFieldValidator.ErrorMessage = ReCheque.dateChequeNonVide;
            TextMontant_RequiredFieldValidator.ErrorMessage = ReCheque.montantChequeNonVide;
            TextNumeroCheque_RequiredFieldValidator.ErrorMessage = ReCheque.numerosChequeNonVide;
            TextBanque_RequiredFieldValidator.ErrorMessage = ReCheque.banqueNonVide;
            TextNumCompte_RequiredFieldValidator.ErrorMessage = ReCheque.numComptNonVide;
            TextTitulaire_RequiredFieldValidator.ErrorMessage = ReCheque.titulaireNonVide;
            TextAdresseTutulaire_RequiredFieldValidator.ErrorMessage = ReCheque.adresseTitulaireNonVide;
            #endregion

            #region bonCommande
            TextDatePaiementBC_RequiredFieldValidator.ErrorMessage = ReBonDeCommande.dateDePaiementNonVide;
            #endregion

        }

        private void divIndicationText(string str, string color)
        {
            if (str != "" && color != "")
            {
                divIndication.Style.Add("font-size", "14px");
                divIndication.Style.Add("color", color);
                divIndication.InnerHtml = "<p>" + str + "</p>";
            }
            else
            {
                divIndication.InnerHtml = "";
            }
        }

        private void divIndicationCommissionText(string str, string color)
        {
            if (str != "" && color != "")
            {
                divIndicationCommission.Style.Add("font-size", "14px");
                divIndicationCommission.Style.Add("color", color);
                divIndicationCommission.InnerHtml = "<p>" + str + "</p>";
            }
            else
            {
                divIndicationCommission.InnerHtml = "";
            }
        }

        private void divIndicationPaiementText(string str, string color)
        {
            if (str != "" && color != "")
            {
                divIndicationPaiement.Style.Add("font-size", "14px");
                divIndicationPaiement.Style.Add("color", color);
                divIndicationPaiement.InnerHtml = "<p>" + str + "</p>";
            }
            else
            {
                divIndicationPaiement.InnerHtml = "";
            }
        }

        private void initialiseFormulaire()
        {
            Panel_AbonnementDureeTemps.Visible = false;
            Panel_AbonnementNbVoyage.Visible = false;
            Panel_FormulaireBillet.Visible = true;

            LabelClient.Text = "";
            hfNumClient.Value = "";
            TextNombreBillet.Text = "";
            hfNumBilletCommande.Value = "";
            try
            {
                ddlCalculePrixBillet.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            this.initialiseFormulaireTrajet();
            btnModifier.Enabled = false;
            btnAjouter.Enabled = true;
            btnValideBillet.Enabled = true;

            divIndication.InnerHtml = "";
        }

        private void initialiseFormulaireBillet()
        {
            Panel_AbonnementDureeTemps.Visible = false;
            Panel_AbonnementNbVoyage.Visible = false;

            Panel_FormulaireBillet.Visible = true;

            LabelClient.Text = "";
            hfNumClient.Value = "";
            TextNombreBillet.Text = "";
            TextNumAbonnement.Text = "";
            hfNumBilletCommande.Value = "";
            try
            {
                ddlCalculePrixBillet.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            this.affichePrix();
            btnModifier.Enabled = false;
            btnAjouter.Enabled = true;
            btnValideBillet.Enabled = true;

            ConfirmButtonExtender_btnModifier.ConfirmText = "";

            ddlVilleDepart_RequiredFieldValidator.ValidationGroup = "gBillet";
            ddlDestination_RequiredFieldValidator.ValidationGroup = "gBillet";
            ddlCalculePrixBillet_RequiredFieldValidator.ValidationGroup = "gBillet";
            TextPrixBillet_RequiredFieldValidator.ValidationGroup = "gBillet";
            TextNombreBillet_RequiredFieldValidator.ValidationGroup = "gBillet";
            TextPrixTotal_RequiredFieldValidator.ValidationGroup = "gBillet";

            TextNbBillet_RequiredFieldValidator.ValidationGroup = "";

            ddlDestination.SelectedValue = "";
        }

        private void initialiseFormulaireCommission()
        {
            LabelRecepteur.Text = "";
            hfNumReceptionnaire.Value = "";
            LabelExpediteur.Text = "";
            hfNumExpediteur.Value = "";

            TextNombreCommission.Text = "";
            TextPoidsCommission.Text = "";
            TextFraisCommission.Text = "";
            TextPieceJustificatifCommission.Text = "";

            hfIdCommissionDevis.Value = "";


            btnModifierCommissionDevis.Enabled = false;
            btnAjouterCommissionDevis.Enabled = true;
            btnValiderCommission.Enabled = true;

            divIndicationCommission.InnerHtml = "";

            ddlDestinationCommission.SelectedValue = "";
        }

        private void initialiseFormulaireTrajet()
        {
            try
            {
                ddlVilleDepart.SelectedValue = agent.agence.ville.NumVille;
            }
            catch (Exception)
            {
            }
            serviceVille.loadDdlVilleDestination(ddlDestination, ddlVilleDepart.SelectedValue);
            try
            {
                ddlDestination.SelectedValue = "";
            }
            catch (Exception)
            {
            }

            this.affichePrix();
        }

        private void initialiseFormulaireTrajetCommission()
        {
            ddlVilleDepartCommission.SelectedValue = agent.agence.ville.NumVille;
            serviceVille.loadDdlVilleDestination(ddlDestinationCommission, ddlVilleDepartCommission.SelectedValue);
            try
            {
                ddlDestinationCommission.SelectedValue = "";
            }
            catch (Exception)
            {
            }
        }

        private void loadDdlDesignation()
        {
            serviceDesignationCommission.loadDdlDesignationCommission(ddlDesignation, ddlTypeCommission.SelectedValue);
        }

        private void affichePrix()
        {
            #region declaration
            crlTrajet Trajet = null;
            crlCalculCategorieBillet CalculCategorieBillet = null;
            double montantBillet = 0.00;
            int nombreBillet = 0;
            #endregion

            #region implementation
            if (ddlVilleDepart.SelectedValue != "" && ddlDestination.SelectedValue != "" && ddlCalculePrixBillet.SelectedValue != "")
            {
                Trajet = serviceTrajet.selectTrajet(ddlVilleDepart.SelectedValue, ddlDestination.SelectedValue);
                CalculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(ddlCalculePrixBillet.SelectedValue);
                if (Trajet != null && CalculCategorieBillet != null)
                {
                    montantBillet = Trajet.tarifBaseBillet.MontantTarifBaseBillet * CalculCategorieBillet.PourcentagePrixBillet / 100;
                    TextPrixBillet.Text = serviceGeneral.separateurDesMilles(montantBillet.ToString("0"));
                    hfNumTrajet.Value = Trajet.NumTrajet;
                    if (TextNombreBillet.Text != "")
                    {
                        try
                        {
                            nombreBillet = int.Parse(TextNombreBillet.Text);
                        }
                        catch (Exception)
                        {

                        }
                        TextPrixTotal.Text = serviceGeneral.separateurDesMilles((nombreBillet * montantBillet).ToString("0"));
                    }
                    else
                    {
                        TextPrixTotal.Text = "";
                    }
                }
                else
                {
                    this.divIndicationText(ReVenteBillet.trajetNonVide, "red");
                }
            }
            else
            {
                TextPrixBillet.Text = "";
                TextPrixTotal.Text = "";
            }
            #endregion
        }

        private void affichePrixCommission()
        {
            #region declaration
            crlDesignationCommission DesignationCommission = null;
            crlTarifBaseCommission tarifBaseCommission = null;
            crlTrajet Trajet = null;
            double montant = 0;
            #endregion

            #region implementation

            if (TextPoidsCommission.Text.Trim() != "" && TextNombreCommission.Text.Trim() != "" && ddlDestinationCommission.SelectedValue != "")
            {
                DesignationCommission = serviceDesignationCommission.selectDesignationCommission(ddlDesignation.SelectedValue);
                if (DesignationCommission != null)
                {
                    Trajet = serviceTrajet.selectTrajet(ddlVilleDepartCommission.SelectedValue, ddlDestinationCommission.SelectedValue);
                    if (Trajet != null)
                    {
                        if (Trajet.tarifBaseCommissions != null)
                        {
                            for (int i = 0; i < Trajet.tarifBaseCommissions.Count; i++)
                            {
                                if (DesignationCommission.Paiement == Trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule)
                                {
                                    tarifBaseCommission = Trajet.tarifBaseCommissions[i];
                                    break;
                                }
                            }
                        }
                        else
                        {
                            TextFraisCommission.Text = "0";
                        }
                    }
                    else
                    {
                        TextFraisCommission.Text = "0";
                    }

                    if (tarifBaseCommission != null)
                    {
                        TextFraisCommission.Text = serviceGeneral.separateurDesMilles(tarifBaseCommission.MontantTarifBaseCommission.ToString("0"));

                        if (tarifBaseCommission.tarifCommissionPar.TypeCalcule == 0)
                        {
                            TextFraisCommission.Text = "0,00";
                        }
                        else if (tarifBaseCommission.tarifCommissionPar.TypeCalcule == 1)
                        {
                            try
                            {
                                montant = tarifBaseCommission.MontantTarifBaseCommission * int.Parse(TextPoidsCommission.Text);
                                TextFraisCommission.Text = serviceGeneral.separateurDesMilles(montant.ToString("0"));
                            }
                            catch (Exception)
                            {
                                TextNombreCommission.Text = "";
                                TextPoidsCommission.Text = "";
                            }
                        }
                        else if (tarifBaseCommission.tarifCommissionPar.TypeCalcule == 2)
                        {
                            try
                            {
                                montant = tarifBaseCommission.MontantTarifBaseCommission * int.Parse(TextNombreCommission.Text);
                                TextFraisCommission.Text = serviceGeneral.separateurDesMilles(montant.ToString("0"));
                            }
                            catch (Exception)
                            {
                                TextNombreCommission.Text = "";
                                TextPoidsCommission.Text = "";
                            }
                        }
                    }

                }
            }
            #endregion
        }

        private void insertObjetBillet(crlBillet billet)
        {
            #region declaration
            #endregion

            #region implementation
            if (billet != null)
            {

                try
                {
                    billet.DateDeValidite = DateTime.Now.AddMonths(int.Parse(ReGlobalParam.nbValiditeBillet));
                }
                catch (Exception)
                {
                    billet.DateDeValidite = DateTime.Now.AddMonths(1);
                }
                billet.NumTrajet = hfNumTrajet.Value;
                billet.MatriculeAgent = agent.matriculeAgent;
                billet.PrixBillet = TextPrixBillet.Text.Replace(" ", "");
                billet.NumCalculCategorieBillet = ddlCalculePrixBillet.SelectedValue;
                billet.DateBillet = DateTime.Now;
                billet.ModePaiement = "Espèce";

                billet.agent = agent;
                billet.trajet = serviceTrajet.selectTrajet(hfNumTrajet.Value);
                billet.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(ddlCalculePrixBillet.SelectedValue);

                if (hfNumClient.Value != "")
                {
                    billet.individu = serviceIndividu.selectIndividu(hfNumClient.Value);
                    if (billet.individu != null)
                    {
                        billet.NumIndividu = billet.individu.NumIndividu;
                    }
                }
            }
            #endregion
        }

        private void insertObjetBilletCommande(crlBilletCommande billetCommande)
        {
            if (billetCommande != null)
            {
                billetCommande.MontantBilletCommande = double.Parse(TextPrixBillet.Text.Replace(" ", ""));
                billetCommande.NombreBilletCommande = int.Parse(TextNombreBillet.Text);
                billetCommande.NumCalculCategorieBillet = ddlCalculePrixBillet.SelectedValue;
                billetCommande.NumTrajet = hfNumTrajet.Value;
                billetCommande.NumIndividu = hfNumClient.Value;

                billetCommande.trajet = serviceTrajet.selectTrajet(hfNumTrajet.Value);
                billetCommande.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(ddlCalculePrixBillet.SelectedValue);
            }
        }

        private void insertToObjetCommissionDevis(crlCommissionDevis commissionDevis)
        {
            #region declaration
            crlTrajet trajet = null;
            #endregion

            #region implementation
            if (commissionDevis != null)
            {
                trajet = serviceTrajet.selectTrajet(ddlVilleDepartCommission.SelectedValue, ddlDestinationCommission.SelectedValue);
                if (trajet != null)
                {
                    if (trajet.villeD.NumVille != agent.agence.NumVille)
                    {
                        commissionDevis.Destination = trajet.villeD.NomVille;
                    }
                    else
                    {
                        commissionDevis.Destination = trajet.villeF.NomVille;
                    }

                    try
                    {
                        commissionDevis.FraisEnvoi = double.Parse(TextFraisCommission.Text.Replace(" ", ""));
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        commissionDevis.Nombre = int.Parse(TextNombreCommission.Text);
                    }
                    catch (Exception)
                    {
                    }
                    commissionDevis.NumDesignation = ddlDesignation.SelectedValue;
                    commissionDevis.NumTrajet = trajet.NumTrajet;
                    commissionDevis.PieceJustificatif = TextPieceJustificatifCommission.Text;
                    try
                    {
                        commissionDevis.Poids = double.Parse(TextPoidsCommission.Text);
                    }
                    catch (Exception)
                    {
                    }
                    commissionDevis.TypeCommission = ddlTypeCommission.SelectedValue;
                    commissionDevis.NumExpediteur = hfNumExpediteur.Value;
                    commissionDevis.NumRecepteur = hfNumReceptionnaire.Value;
                }
            }
            #endregion
        }

        private void insertToObjetIndividu(crlIndividu individu)
        {
            if (individu != null)
            {
                individu.Adresse = TextAdresseClient.Text;
                individu.CinIndividu = TextCinClient.Text;
                individu.TelephoneMobileIndividu = TextTelephoneMobile.Text;
                individu.NomIndividu = TextNomClient.Text;
                individu.PrenomIndividu = TextPrenom.Text;
                individu.TelephoneFixeIndividu = TextTelephoneFixeClient.Text;
            }
        }

        private void insertToObjetExpediteur(crlClient client)
        {
            if (client != null)
            {
                client.AdresseClient = TextAdresseExpediteur.Text;
                client.CinClient = TextCINExpediteur.Text;
                client.MobileClient = TextPortableExpediteur.Text;
                client.NomClient = TextNomExpediteur.Text;
                client.PrenomClient = TextPrenomExpediteur.Text;
                client.TelephoneClient = TextFixeExpediteur.Text;
            }
        }

        private void insertToObjetReceptionnaire(crlReceptionnaire receptionnaire)
        {
            if (receptionnaire != null)
            {
                receptionnaire.AdressePersonne = TextAdresseRecepteur.Text;
                receptionnaire.NomPersonne = TextNomRecepteur.Text;
                receptionnaire.PrenomPersonne = TextPrenomRecepteur.Text;
                receptionnaire.Telephone = TextTelephoneRecepteur.Text;
            }
        }

        private void insertToObjetSociete(crlSociete societe)
        {
            if (societe != null)
            {
                //societe.AdresseResponsable = TextAdresseRespSociete.Text;
                societe.AdresseSociete = TextAdresseSociete.Text;
                //societe.CinResponsable = TextCinRespSociete.Text;
                //societe.MailResponsable = TextMailRespSociete.Text;
                societe.MailSociete = TextMailSociete.Text;
                //societe.NomResponsable = TextNomResponsableSociete.Text;
                societe.NomSociete = TextNomSociete.Text;
                ///societe.PrenomResponsable = TextPrenomRespSociete.Text;
                societe.SecteurActiviteSociete = TextSecteurSociete.Text;
                //societe.TelephoneFixeResponsable = TextFixeRespSociete.Text;
                societe.TelephoneFixeSociete = TextTelephoneSociete.Text;
                //societe.TelephoneMobileResponsable = TextMobileRespSociete.Text;
                societe.TelephoneMobileSociete = TextMobileSociete.Text;
            }
        }

        private void insertToObjetOrganisme(crlOrganisme organisme)
        {
            if (organisme != null)
            {
                organisme.AdresseOrganisme = TextAdresseOrganisme.Text;
                //organisme.AdresseResponsable = TextAdresseRespOrganisme.Text;
                //organisme.CinResponsable = TextCinRespOrganisme.Text;
                organisme.MailOrganisme = TextMailOrganisme.Text;
                //organisme.MailResponsable = TextMailRespOrganisme.Text;
                organisme.NomOrganisme = TextNomOrganisme.Text;
                //organisme.NomResponsable = TextNomRespOrganisme.Text;
                //organisme.PrenomResponsable = TextPrenomRespOrganisme.Text;
                organisme.TelephoneFixeOrganisme = TextFixeOrganisme.Text;
                //organisme.TelephoneFixeResponsable = TextFixeRespOrganisme.Text;
                organisme.TelephoneMobileOrganisme = TextMobileOrganisme.Text;
                //organisme.TelephoneMobileResponsable = TextMobileRespOrganisme.Text;
            }
        }

        private void insertToObjetCommission(crlCommission commission)
        {
            #region declaration
            crlTrajet trajet = null;
            #endregion

            #region Implementation
            if (commission != null)
            {

                trajet = serviceTrajet.selectTrajet(ddlVilleDepartCommission.SelectedValue, ddlDestinationCommission.SelectedValue);
                if (trajet != null)
                {
                    if (trajet.villeD.NumVille != agent.agence.NumVille)
                    {
                        commission.Destination = trajet.villeD.NomVille;
                    }
                    else
                    {
                        commission.Destination = trajet.villeF.NomVille;
                    }
                    commission.agent = agent;
                    commission.FraisEnvoi = TextFraisCommission.Text.Replace(" ", "");
                    commission.MatriculeAgent = agent.matriculeAgent;
                    commission.ModePaiement = "Espèce";
                    try
                    {
                        commission.Nombre = int.Parse(TextNombreCommission.Text);
                    }
                    catch (Exception)
                    {
                    }
                    commission.NumDesignation = ddlDesignation.SelectedValue;
                    commission.NumExpediteur = hfNumExpediteur.Value;
                    commission.NumRecepteur = hfNumReceptionnaire.Value;
                    commission.NumTrajet = trajet.NumTrajet;
                    commission.PieceJustificatif = TextPieceJustificatifCommission.Text;

                    commission.Poids = TextPoidsCommission.Text;

                    commission.TypeCommission = ddlTypeCommission.SelectedValue;
                }


            }
            #endregion
        }

        private void afficheIndividu(string numIndividu)
        {
            #region declaration
            crlIndividu individu = null;
            #endregion

            #region implementation
            if (numIndividu != "")
            {
                individu = serviceIndividu.selectIndividu(numIndividu);
                if (individu != null)
                {
                    TextAdresseClient.Text = individu.Adresse;
                    TextCinClient.Text = individu.CinIndividu;
                    TextNomClient.Text = individu.NomIndividu;
                    TextPrenom.Text = individu.PrenomIndividu;
                    TextTelephoneFixeClient.Text = individu.TelephoneFixeIndividu;
                    TextTelephoneMobile.Text = individu.TelephoneMobileIndividu;
                }
            }
            #endregion
        }

        private void afficheExpediteur(string numClient)
        {
            #region declaration
            crlClient client = null;
            #endregion

            #region implementation
            if (numClient != "")
            {
                client = serviceClient.selectClient(numClient);
                if (client != null)
                {
                    TextAdresseExpediteur.Text = client.AdresseClient;
                    TextCINExpediteur.Text = client.CinClient;
                    TextNomExpediteur.Text = client.NomClient;
                    TextPrenomExpediteur.Text = client.PrenomClient;
                    TextFixeExpediteur.Text = client.TelephoneClient;
                    TextPortableExpediteur.Text = client.MobileClient;

                }
            }
            #endregion
        }

        private void afficheRecepteur(string idPersonne)
        {
            #region declaration
            crlReceptionnaire receptionnaire = null;
            #endregion

            #region imlementation
            if (idPersonne != "")
            {
                receptionnaire = serviceReceptionnaire.selectPersonne(idPersonne);
                if (receptionnaire != null)
                {
                    TextAdresseRecepteur.Text = receptionnaire.AdressePersonne;
                    TextNomRecepteur.Text = receptionnaire.NomPersonne;
                    TextPrenomRecepteur.Text = receptionnaire.PrenomPersonne;
                    TextTelephoneRecepteur.Text = receptionnaire.Telephone;
                }
            }
            #endregion
        }

        private void afficheSociete(string numSociete)
        {
            #region declaration
            crlSociete societe = null;
            #endregion

            #region
            if (numSociete != "")
            {
                societe = serviceSociete.selectSociete(numSociete);
                if (societe != null)
                {
                    //TextAdresseRespSociete.Text = societe.AdresseResponsable;
                    TextAdresseSociete.Text = societe.AdresseSociete;
                    //TextCinRespSociete.Text = societe.CinResponsable;
                    //TextFixeRespSociete.Text = societe.TelephoneFixeResponsable;
                    //TextMailRespSociete.Text = societe.MailResponsable;
                    TextMailSociete.Text = societe.MailSociete;
                    //TextMobileRespSociete.Text = societe.TelephoneMobileResponsable;
                    TextMobileSociete.Text = societe.TelephoneMobileSociete;
                    //TextNomResponsableSociete.Text = societe.NomResponsable;
                    TextNomSociete.Text = societe.NomSociete;
                    //TextPrenomRespSociete.Text = societe.PrenomResponsable;
                    TextSecteurSociete.Text = societe.SecteurActiviteSociete;
                }
            }
            #endregion
        }

        private void afficheOrganisme(string numOganisme)
        {
            #region declaration
            crlOrganisme organisme = null;
            #endregion

            #region implementation
            if (numOganisme != "")
            {
                organisme = serviceOrganisme.selectOrganisme(numOganisme);
                if (organisme != null)
                {
                    TextAdresseOrganisme.Text = organisme.AdresseOrganisme;
                    //TextAdresseRespOrganisme.Text = organisme.AdresseResponsable;
                    //TextCinRespOrganisme.Text = organisme.CinResponsable;
                    TextFixeOrganisme.Text = organisme.TelephoneFixeOrganisme;
                    //TextFixeRespOrganisme.Text = organisme.TelephoneFixeResponsable;
                    TextMailOrganisme.Text = organisme.MailOrganisme;
                    //TextMailRespOrganisme.Text = organisme.MailResponsable;
                    TextMobileOrganisme.Text = organisme.TelephoneMobileOrganisme;
                    //TextMobileRespOrganisme.Text = organisme.TelephoneMobileResponsable;
                    TextNomOrganisme.Text = organisme.NomOrganisme;
                    //TextNomRespOrganisme.Text = organisme.NomResponsable;
                    //TextPrenomRespOrganisme.Text = organisme.PrenomResponsable;
                }
            }
            #endregion
        }

        private void initialiseFormulaireClient()
        {
            TextAdresseClient.Text = "";
            TextCinClient.Text = "";
            TextNomClient.Text = "";
            TextPrenom.Text = "";
            TextTelephoneFixeClient.Text = "";
            TextTelephoneMobile.Text = "";
        }

        private void initialiseFormulaireSociete()
        {
            TextAdresseRespSociete.Text = "";
            TextAdresseSociete.Text = "";
            TextCinRespSociete.Text = "";
            TextFixeRespSociete.Text = "";
            TextMailRespSociete.Text = "";
            TextMailSociete.Text = "";
            TextMobileRespSociete.Text = "";
            TextMobileSociete.Text = "";
            TextNomResponsableSociete.Text = "";
            TextNomSociete.Text = "";
            TextPrenomRespSociete.Text = "";
            TextSecteurSociete.Text = "";
        }

        private void initialiseFormulaireOrganisme()
        {
            TextAdresseOrganisme.Text = "";
            TextAdresseRespOrganisme.Text = "";
            TextCinRespOrganisme.Text = "";
            TextFixeOrganisme.Text = "";
            TextFixeRespOrganisme.Text = "";
            TextMailOrganisme.Text = "";
            TextMailRespOrganisme.Text = "";
            TextMobileOrganisme.Text = "";
            TextMobileRespOrganisme.Text = "";
            TextNomOrganisme.Text = "";
            TextNomRespOrganisme.Text = "";
            TextPrenomRespOrganisme.Text = "";
        }

        private void initialiseGridBilletProforma()
        {
            #region declaration
            Convertisseuse convertiseur = new Convertisseuse();
            #endregion

            #region implementation
            if (hfNumProforma.Value != "")
            {
                serviceProforma.insertToGridBillet(gvBilletProforma, "billetcommande.numBilletCommande", "billetcommande.numBilletCommande", "", hfNumProforma.Value);

                LabelPrixTotalProforma.Text = serviceGeneral.separateurDesMilles(serviceProforma.getPrixTotalBilletCommandeProforma(hfNumProforma.Value).ToString("0")) + "Ar";
                //LabelPrixTotalProformaLettre.Text = convertiseur.convertion(serviceProforma.getPrixTotalBilletCommandeProforma(hfNumProforma.Value).ToString("0")) + " Ariary";

            }
            else
            {
                serviceProforma.insertToGridBillet(gvBilletProforma, "", "", "", "");
                LabelPrixTotalProforma.Text = serviceGeneral.separateurDesMilles(serviceProforma.getPrixTotalBilletCommandeProforma("").ToString("0")) + "Ar";
                //LabelPrixTotalProformaLettre.Text = convertiseur.convertion(serviceProforma.getPrixTotalBilletCommandeProforma("").ToString("0")) + " Ariary";
                btnEnregistrerProforma.Enabled = false;
                btnNouvelleCommande.Enabled = false;
                btnAnnulerProforma.Enabled = false;
            }
            this.initialisePrixTotalDevis();
            #endregion
        }

        private void initialiseGridCommissionDevis()
        {
            #region declaration
            Convertisseuse convertiseur = new Convertisseuse();
            #endregion

            #region implementation
            if (hfNumProforma.Value != "")
            {
                serviceCommissionDevis.insertToGridCommissionDevis(gvCommissionProforma, " commissiondevis.idCommissionDevis", " commissiondevis.idCommissionDevis", "", hfNumProforma.Value);

                LabelPrixDevisCommission.Text = serviceGeneral.separateurDesMilles(serviceProforma.getPrixTotalCommissionDevisProforma(hfNumProforma.Value).ToString("0")) + "Ar";
                //LabelPrixDevisCommissionLettre.Text = convertiseur.convertion(serviceProforma.getPrixTotalCommissionDevisProforma(hfNumProforma.Value).ToString("0")) + " Ariary";

            }
            else
            {
                serviceCommissionDevis.insertToGridCommissionDevis(gvCommissionProforma, "", "", "", "");
                LabelPrixDevisCommission.Text = serviceGeneral.separateurDesMilles(serviceProforma.getPrixTotalCommissionDevisProforma("").ToString("0")) + "Ar";
                //LabelPrixDevisCommissionLettre.Text = convertiseur.convertion(serviceProforma.getPrixTotalCommissionDevisProforma("").ToString("0")) + " Ariary";

            }
            this.initialisePrixTotalDevis();
            #endregion
        }

        private void initialisePrixTotalDevis()
        {
            #region declaration
            Convertisseuse convertisseuse = new Convertisseuse();
            double montantTotal = 0;
            #endregion

            #region implementation
            montantTotal += serviceProforma.getPrixTotalBilletCommandeProforma(hfNumProforma.Value);
            montantTotal += serviceProforma.getPrixTotalCommissionDevisProforma(hfNumProforma.Value);
            montantTotal += serviceProforma.getPrixTotalDureeAbonnementProforma(hfNumProforma.Value);
            montantTotal += serviceProforma.getPrixTotalVoyageAbonnementProforma(hfNumProforma.Value);

            LabelTotalDevis.Text = serviceGeneral.separateurDesMilles(montantTotal.ToString("0")) + "Ar";
            LabelTotalDevisLettre.Text = convertisseuse.convertion(montantTotal.ToString("0")) + " Ariary";
            if (montantTotal == 0)
            {
                TextMontantProforma.Text = "";
            }
            else
            {
                TextMontantProforma.Text = serviceGeneral.separateurDesMilles(montantTotal.ToString("0"));
            }
            #endregion
        }

        private void initialiseGridProforma()
        {
            serviceProforma.insertToGridProformaBilletCommission(gvProforma, ddlTriProforma.SelectedValue, ddlTriProforma.SelectedValue, TextRechercheProforma.Text);
        }

        private void afficheProforma(string numProforma)
        {
            #region declaration
            crlProforma proforma = null;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                hfNumProforma.Value = numProforma;
                this.initialiseGridProforma();
                this.initialiseGridBilletProforma();
                this.initialiseGridCommissionDevis();
                btnNouvelleCommande.Enabled = true;


                proforma = serviceProforma.selectProforma(numProforma);
                if (proforma != null)
                {
                    if (proforma.societe == null)
                    {
                        Panel_Societe.Visible = false;

                        if (proforma.organisme == null)
                        {
                            Panel_Organisme.Visible = false;

                            if (proforma.individu == null)
                            {
                                btnEnregistrerProforma.Enabled = true;
                                btnAnnulerProforma.Enabled = true;
                                TextMontantProforma.Text = "";

                                Panel_Individu.Visible = false;
                            }
                            else
                            {
                                btnEnregistrerProforma.Enabled = false;
                                btnAnnulerProforma.Enabled = false;

                                LabelAdresseClient.Text = proforma.individu.Adresse;
                                LabelCINClient.Text = proforma.individu.CinIndividu;
                                LabelFixeClient.Text = proforma.individu.TelephoneFixeIndividu;
                                LabelMobileClient.Text = proforma.individu.TelephoneMobileIndividu;
                                LabelNomClient.Text = proforma.individu.NomIndividu;
                                LabelPrenomClient.Text = proforma.individu.PrenomIndividu;

                                Panel_Individu.Visible = true;
                            }
                        }
                        else
                        {
                            btnEnregistrerProforma.Enabled = false;
                            btnAnnulerProforma.Enabled = false;

                            LabelAdresseOrganisme.Text = proforma.organisme.AdresseOrganisme;
                            LabelFixeOrganisme.Text = proforma.organisme.TelephoneFixeOrganisme;
                            LabelMailOrganisme.Text = proforma.organisme.MailOrganisme;
                            LabelMobileOrganisme.Text = proforma.organisme.TelephoneMobileOrganisme;
                            LabelNomOrganisme.Text = proforma.organisme.NomOrganisme;


                            if (proforma.organisme.individuResponsable != null)
                            {
                                LabelAdresseRespOrganisme.Text = proforma.organisme.individuResponsable.Adresse;
                                LabelCINRespOrganisme.Text = proforma.organisme.individuResponsable.CinIndividu;
                                LabelFixeRespOrganisme.Text = proforma.organisme.individuResponsable.TelephoneFixeIndividu;
                                LabelMobileRespOrganisme.Text = proforma.organisme.individuResponsable.TelephoneMobileIndividu;
                                LabelNomRespOrganisme.Text = proforma.organisme.individuResponsable.NomIndividu;
                                LabelPrenomRespOrganisme.Text = proforma.organisme.individuResponsable.PrenomIndividu;
                            }

                            Panel_Organisme.Visible = true;
                        }
                    }
                    else
                    {
                        btnEnregistrerProforma.Enabled = false;
                        btnAnnulerProforma.Enabled = false;

                        LabelAdresseSociete.Text = proforma.societe.AdresseSociete;
                        LabelFixeSociete.Text = proforma.societe.TelephoneFixeSociete;
                        LabelMailSociete.Text = proforma.societe.MailSociete;
                        LabelMobileSociete.Text = proforma.societe.TelephoneMobileSociete;
                        LabelNomSociete.Text = proforma.societe.NomSociete;
                        LabelSecteurActiviteSociete.Text = proforma.societe.SecteurActiviteSociete;

                        if (proforma.societe.individuResponsable != null)
                        {
                            LabelAdresseRespSociete.Text = proforma.societe.individuResponsable.Adresse;
                            LabelCINRespSociete.Text = proforma.societe.individuResponsable.CinIndividu;
                            LabelFixeRespSociete.Text = proforma.societe.individuResponsable.TelephoneFixeIndividu;
                            LabelMobileRespSociete.Text = proforma.societe.individuResponsable.TelephoneMobileIndividu;
                            LabelNomRespSociete.Text = proforma.societe.individuResponsable.NomIndividu;
                            LabelPrenomRespSociete.Text = proforma.societe.individuResponsable.PrenomIndividu;
                        }


                        Panel_Societe.Visible = true;
                    }
                }
            }

            #endregion
        }

        private void afficheBilletCommande(string numBilletCommande)
        {
            #region declaration
            crlBilletCommande billetCommande = null;
            #endregion

            #region implementation
            if (numBilletCommande != "")
            {
                billetCommande = serviceBilletCommande.selectBilletCommande(numBilletCommande);
                if (billetCommande != null)
                {
                    try
                    {
                        ddlVilleDepart.SelectedValue = billetCommande.trajet.villeD.NumVille;
                    }
                    catch (Exception)
                    {
                    }
                    serviceVille.loadDdlVilleDestination(ddlDestination, ddlVilleDepart.SelectedValue);
                    try
                    {
                        ddlDestination.SelectedValue = billetCommande.trajet.villeF.NumVille;
                    }
                    catch (Exception)
                    {
                    }
                    hfNumTrajet.Value = billetCommande.trajet.NumTrajet;
                    try
                    {
                        ddlCalculePrixBillet.SelectedValue = billetCommande.calculCategorieBillet.NumCalculCategorieBillet;
                    }
                    catch (Exception)
                    {
                    }
                    TextNombreBillet.Text = billetCommande.NombreBilletCommande.ToString("0");
                    this.affichePrix();

                    if (billetCommande.individu != null)
                    {
                        hfNumClient.Value = billetCommande.individu.NumIndividu;
                        LabelClient.Text = billetCommande.individu.PrenomIndividu + " " + billetCommande.individu.NomIndividu;
                    }

                    hfNumBilletCommande.Value = billetCommande.NumBilletCommande;
                    btnModifier.Enabled = true;
                    btnAjouter.Enabled = false;
                    btnValideBillet.Enabled = false;

                    ConfirmButtonExtender_btnModifier.ConfirmText = "Voulez vous vraiment modifier le commande billet du trajet " + billetCommande.trajet.villeD.NomVille + "-" + billetCommande.trajet.villeF.NomVille + "?\nMontant total:" + serviceGeneral.separateurDesMilles((billetCommande.MontantBilletCommande * billetCommande.NombreBilletCommande).ToString("0")) + "Ar";
                }
            }
            #endregion
        }

        private void afficheCommissionDevis(string idCommissionDevis)
        {
            #region declaration
            crlCommissionDevis commissionDevis = null;
            #endregion

            #region implementation
            if (idCommissionDevis != "")
            {
                commissionDevis = serviceCommissionDevis.selectCommissionDevis(idCommissionDevis);
                if (commissionDevis != null)
                {
                    try
                    {
                        ddlVilleDepartCommission.SelectedValue = commissionDevis.trajet.NumVilleD;
                    }
                    catch (Exception)
                    {
                    }
                    serviceVille.loadDdlVilleDestination(ddlDestinationCommission, ddlVilleDepartCommission.SelectedValue);
                    try
                    {
                        ddlDestinationCommission.SelectedValue = commissionDevis.trajet.NumVilleF;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        ddlDesignation.SelectedValue = commissionDevis.NumDesignation;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        ddlTypeCommission.SelectedValue = commissionDevis.TypeCommission;
                    }
                    catch (Exception)
                    {
                    }
                    TextNombreCommission.Text = commissionDevis.Nombre.ToString("0");
                    TextFraisCommission.Text = serviceGeneral.separateurDesMilles(commissionDevis.FraisEnvoi.ToString("0"));
                    TextPoidsCommission.Text = commissionDevis.Poids.ToString();
                    TextPieceJustificatifCommission.Text = commissionDevis.PieceJustificatif;
                    if (commissionDevis.expediteur != null)
                    {
                        hfNumExpediteur.Value = commissionDevis.expediteur.NumClient;
                        LabelExpediteur.Text = commissionDevis.expediteur.PrenomClient + " " + commissionDevis.expediteur.NomClient;
                    }
                    if (commissionDevis.recepteur != null)
                    {
                        hfNumReceptionnaire.Value = commissionDevis.recepteur.IdPersonne;
                        LabelRecepteur.Text = commissionDevis.recepteur.PrenomPersonne + " " + commissionDevis.recepteur.NomPersonne;
                    }

                    hfIdCommissionDevis.Value = commissionDevis.IdCommissionDevis;

                    btnModifierCommissionDevis_ConfirmButtonExtender.ConfirmText = "Voulez vous vraiment modifier le devis commission pour le trajet " + commissionDevis.trajet.villeD.NomVille + "-" + commissionDevis.trajet.villeF.NomVille + "?\nMontant:" + serviceGeneral.separateurDesMilles(commissionDevis.FraisEnvoi.ToString("0")) + "Ar";

                    btnModifierCommissionDevis.Enabled = true;
                    btnAjouterCommissionDevis.Enabled = false;
                    btnValiderCommission.Enabled = false;
                }
            }
            #endregion
        }

        private void initialiseSociete()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("societe"))
            {
                PanelSociete.Visible = true;
                PanelListeSociete.Visible = true;
                TextAdresseRespSociete_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextAdresseSociete_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextCinRespSociete_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextNomResponsableSociete_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextNomSociete_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextSecteurSociete_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextMailRespSociete_RegularExpressionValidator.ValidationGroup = "gProforma";
                TextMailSociete_RegularExpressionValidator.ValidationGroup = "gProforma";
            }
            else
            {
                PanelSociete.Visible = false;
                PanelListeSociete.Visible = false;
                TextAdresseRespSociete_RequiredFieldValidator.ValidationGroup = "";
                TextAdresseSociete_RequiredFieldValidator.ValidationGroup = "";
                TextCinRespSociete_RequiredFieldValidator.ValidationGroup = "";
                TextNomResponsableSociete_RequiredFieldValidator.ValidationGroup = "";
                TextNomSociete_RequiredFieldValidator.ValidationGroup = "";
                TextSecteurSociete_RequiredFieldValidator.ValidationGroup = "";
                TextMailRespSociete_RegularExpressionValidator.ValidationGroup = "";
                TextMailSociete_RegularExpressionValidator.ValidationGroup = "";
            }
        }

        private void initialiseOrganisme()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("organisme"))
            {
                PanelOrganisme.Visible = true;
                PanelListeOrganisme.Visible = true;
                TextAdresseOrganisme_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextAdresseRespOrganisme_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextCinRespOrganisme_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextNomOrganisme_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextNomRespOrganisme_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextMailRespOrganisme_RegularExpressionValidator.ValidationGroup = "gProforma";
                TextMailOrganisme_RegularExpressionValidator.ValidationGroup = "gProforma";
            }
            else
            {
                PanelOrganisme.Visible = false;
                PanelListeOrganisme.Visible = false;
                TextAdresseOrganisme_RequiredFieldValidator.ValidationGroup = "";
                TextAdresseRespOrganisme_RequiredFieldValidator.ValidationGroup = "";
                TextCinRespOrganisme_RequiredFieldValidator.ValidationGroup = "";
                TextNomOrganisme_RequiredFieldValidator.ValidationGroup = "";
                TextNomRespOrganisme_RequiredFieldValidator.ValidationGroup = "";
                TextMailRespOrganisme_RegularExpressionValidator.ValidationGroup = "";
                TextMailOrganisme_RegularExpressionValidator.ValidationGroup = "";
            }
        }

        private void initialiseIndividu()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("individu"))
            {
                PanelClient.Visible = true;
                PanelListeClient.Visible = true;
                TextNomClient_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextCinClient_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextAdresseClient_RequiredFieldValidator.ValidationGroup = "gProforma";
            }
            else
            {
                PanelClient.Visible = false;
                PanelListeClient.Visible = false;
                TextNomClient_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextCinClient_RequiredFieldValidator.ValidationGroup = "gProforma";
                TextAdresseClient_RequiredFieldValidator.ValidationGroup = "gProforma";
            }
        }

        private void initialiseGridIndividu()
        {
            serviceIndividu.insertToGridIndividu(gvClient, ddlTriClient.SelectedValue, ddlTriClient.SelectedValue, TextRechercheClient.Text);
        }

        private void initialiseGridSociete()
        {
            serviceSociete.insertToGridSociete(gvSociete, ddlTriSociete.SelectedValue, ddlTriSociete.SelectedValue, TextRechercheSociete.Text);
        }

        private void initialiseGridOrganisme()
        {
            serviceOrganisme.insertToGridOrganisme(gvOrganisme, ddlTriOrganisme.SelectedValue, ddlTriOrganisme.SelectedValue, TextRechercheOrganisme.Text);
        }

        private void initialiseFormulaireExpediteur()
        {
            TextAdresseExpediteur.Text = "";
            TextCINExpediteur.Text = "";
            TextNomExpediteur.Text = "";
            TextPrenomExpediteur.Text = "";
            TextFixeExpediteur.Text = "";
            TextPortableExpediteur.Text = "";
        }

        private void initialiseFormulaireRecepteur()
        {
            TextAdresseRecepteur.Text = "";
            TextNomRecepteur.Text = "";
            TextPrenomRecepteur.Text = "";
            TextTelephoneRecepteur.Text = "";
        }

        private void initialiseGridExpediteur()
        {
            serviceClient.insertToGridClient(gvListeExpediteur, ddlTriListeExpediteur.SelectedValue, ddlTriListeExpediteur.SelectedValue, TextRechercheListeExpediteur.Text);
        }

        private void initialiseGridRecepteur()
        {
            serviceReceptionnaire.insertToGridReceptionnaire(gvRecepteur, ddlTriRecepteur.SelectedValue, ddlTriRecepteur.SelectedValue, TextRechercheRecepteur.Text);
        }

        #region abonnement
        private void initialisePanelDureeAbonnement()
        {
            if (RadioButtonList_TypeAbonnement.SelectedValue.Equals("0"))
            {
                Panel_AbonnementDureeTempsListe.Visible = false;
            }
            else if (RadioButtonList_TypeAbonnement.SelectedValue.Equals("1"))
            {
                Panel_AbonnementDureeTempsListe.Visible = true;
            }
        }

        private void initialisePanelVoyageAbonnement()
        {
            if (RadioButtonList_TypeAbonnement.SelectedValue.Equals("0"))
            {
                Panel_AbonnementNbVoyageListe.Visible = true;
            }
            else if (RadioButtonList_TypeAbonnement.SelectedValue.Equals("1"))
            {
                Panel_AbonnementNbVoyageListe.Visible = false;
            }
        }

        private void initialiseGridVoyageAbonnement()
        {
            serviceVoyageAbonnement.insertToGridVoyageAbonnementValide(gvVoyageAbonnement, ddlTriVoyageAbonnement.SelectedValue, ddlTriVoyageAbonnement.SelectedValue, TextRechercheVoyageAbonnement.Text, hfNumAbonnement.Value);
        }

        private void initialiseGridDureeAbonnement()
        {
            serviceDureeAbonnement.insertToGridDureeAbonnementValide(gvAbonnementDureeTemps, ddlTriAbonnementDureeTemps.SelectedValue, ddlTriAbonnementDureeTemps.SelectedValue, TextRechercheAbonnementDureeTemps.Text, hfNumAbonnement.Value);
        }

        private void afficheVoyageAbonnement(string numVoyageAbonnement)
        {
            #region declaration
            crlVoyageAbonnement voyageAbonnement = null;
            #endregion

            #region implemenattion
            if (numVoyageAbonnement != "")
            {
                voyageAbonnement = serviceVoyageAbonnement.selectVoyageAbonnement(numVoyageAbonnement);

                if (voyageAbonnement != null)
                {
                    hfNumVoyageAbonnement.Value = voyageAbonnement.NumVoyageAbonnement;

                    LabelZoneVoyageAbonnement.Text = voyageAbonnement.Zone;

                    if (voyageAbonnement.trajet != null)
                    {
                        LabelTrajetVoyageAbonnement.Text = voyageAbonnement.trajet.villeD.NomVille + "-" + voyageAbonnement.trajet.villeF.NomVille;
                    }
                    else
                    {
                        LabelTrajetVoyageAbonnement.Text = "";
                    }

                    if (voyageAbonnement.calculCategorieBillet != null)
                    {
                        LabelCategorieVoyageAbonnement.Text = voyageAbonnement.calculCategorieBillet.IndicateurPrixBillet;
                    }
                    else
                    {
                        LabelCategorieVoyageAbonnement.Text = "";
                    }

                    Panel_AbonnementDureeTemps.Visible = false;
                    Panel_AbonnementNbVoyage.Visible = true;
                    Panel_FormulaireBillet.Visible = false;

                    Panel_FormulaireAbonnement.Visible = false;

                    this.initialiseValidationGroupBillet();

                    TextNbBillet_RequiredFieldValidator.ValidationGroup = "gBillet";
                }
            }
            #endregion
        }

        private void afficheDureeAbonnement(string numDureeAbonnement)
        {
            #region declaration
            crlDureeAbonnement dureeAbonnement = null;
            #endregion

            #region implementation
            if (numDureeAbonnement != "")
            {
                dureeAbonnement = serviceDureeAbonnement.selectDureeAbonnement(numDureeAbonnement);

                if (dureeAbonnement != null)
                {
                    hfNumDureeAbonnement.Value = dureeAbonnement.NumDureeAbonnement;
                    LabelZoneDureeAbonnement.Text = dureeAbonnement.Zone;
                    if (dureeAbonnement.trajet != null)
                    {
                        LabelTrajetDureeAbonnement.Text = dureeAbonnement.trajet.villeD.NomVille + "-" + dureeAbonnement.trajet.villeF.NomVille;

                    }
                    else
                    {
                        LabelTrajetDureeAbonnement.Text = "";
                    }

                    if (dureeAbonnement.calculCategorieBillet != null)
                    {
                        LabelCategorieDureeAbonnement.Text = dureeAbonnement.calculCategorieBillet.IndicateurPrixBillet;
                    }
                    else
                    {
                        LabelCategorieDureeAbonnement.Text = "";
                    }

                    Panel_AbonnementDureeTemps.Visible = true;
                    Panel_AbonnementNbVoyage.Visible = false;
                    Panel_FormulaireBillet.Visible = false;

                    Panel_FormulaireAbonnement.Visible = false;

                    this.initialiseValidationGroupBillet();
                }
            }
            #endregion
        }

        private void initialiseFormulaireVoyageAbonnement()
        {
            LabelTrajetVoyageAbonnement.Text = "";
            LabelZoneVoyageAbonnement.Text = "";
            TextNbBillet.Text = "1";
            hfNumVoyageAbonnement.Value = "";
        }

        private void initialiseFormaulaireDureAbonnement()
        {
            LabelTrajetDureeAbonnement.Text = "";
            LabelZoneDureeAbonnement.Text = "";
            hfNumDureeAbonnement.Value = "";
        }

        private void initialiseValidationGroupBillet()
        {
            ddlVilleDepart_RequiredFieldValidator.ValidationGroup = "";
            ddlDestination_RequiredFieldValidator.ValidationGroup = "";
            ddlCalculePrixBillet_RequiredFieldValidator.ValidationGroup = "";
            TextPrixBillet_RequiredFieldValidator.ValidationGroup = "";
            TextNombreBillet_RequiredFieldValidator.ValidationGroup = "";
            TextPrixTotal_RequiredFieldValidator.ValidationGroup = "";
        }
        #endregion

        #region paiement
        private void initialiseFormulaireBonCommande()
        {
            TextDatePaiementBC.Text = "";
            TextDescriptionBC.Text = "";
        }

        private void initialiseFormulaireCheque()
        {
            TextBanque.Text = "";
            TextMontant.Text = "";
            TextNumeroCheque.Text = "";
            TextDateCheque.Text = "";
            TextNumCompte.Text = "";
            TextTitulaire.Text = "";
            TextAdresseTutulaire.Text = "";
        }

        private void initialiseFormulairePaiement()
        {
            Panel_Societe.Visible = false;
            Panel_Organisme.Visible = false;
            Panel_Individu.Visible = false;

            try
            {
                ddlModePaiement.SelectedValue = "Espèce";
            }
            catch (Exception)
            {
            }

            this.initialiseFormulaireBonCommande();
            this.initialiseFormulaireCheque();

            this.initialisePanelBonCommande();
            this.initialisePanelCheque();
        }

        private void initialisePanelBonCommande()
        {
            #region implementation
            if (ddlModePaiement.SelectedValue.Equals("Bon de commande"))
            {
                Panel_BonDeCommande.Visible = true;

                TextDatePaiementBC_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
            }
            else
            {
                Panel_BonDeCommande.Visible = false;

                TextDatePaiementBC_RequiredFieldValidator.ValidationGroup = "gTemp";
            }

            this.initialiseFormulaireBonCommande();
            #endregion
        }

        private void initialisePanelCheque()
        {
            #region implementation
            if (ddlModePaiement.SelectedValue.Equals("Chèque"))
            {
                Panel_Cheque.Visible = true;

                TextAdresseTutulaire_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
                TextBanque_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
                TextDateCheque_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
                TextMontant_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
                TextNumCompte_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
                TextNumeroCheque_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
                TextTitulaire_RequiredFieldValidator.ValidationGroup = "gProformaPaiement";
            }
            else
            {
                Panel_Cheque.Visible = false;

                TextAdresseTutulaire_RequiredFieldValidator.ValidationGroup = "gTemp";
                TextBanque_RequiredFieldValidator.ValidationGroup = "gTemp";
                TextDateCheque_RequiredFieldValidator.ValidationGroup = "gTemp";
                TextMontant_RequiredFieldValidator.ValidationGroup = "gTemp";
                TextNumCompte_RequiredFieldValidator.ValidationGroup = "gTemp";
                TextNumeroCheque_RequiredFieldValidator.ValidationGroup = "gTemp";
                TextTitulaire_RequiredFieldValidator.ValidationGroup = "gTemp";
            }

            this.initialiseFormulaireCheque();
            #endregion
        }

        private void insertToObjetRecu(crlRecuEncaisser recu)
        {
            #region implementation
            if (recu != null)
            {
                recu.agent = agent;
                recu.LibelleRecuEncaisser = "";
                recu.MatriculeAgent = agent.matriculeAgent;
                recu.ModePaiement = ddlModePaiement.SelectedValue;
                try
                {
                    recu.MontantRecuEncaisser = double.Parse(TextMontantProforma.Text.Replace(" ", ""));
                }
                catch (Exception)
                {
                }

                if (ddlModePaiement.SelectedValue.Equals("Chèque"))
                {
                    recu.cheque = new crlCheque();
                    recu.cheque.AdresseTitulaireCheque = TextAdresseTutulaire.Text;
                    recu.cheque.Banque = TextBanque.Text;
                    recu.cheque.MatriculeAgent = agent.matriculeAgent;
                    recu.cheque.NumerosCheque = TextNumeroCheque.Text;
                    recu.cheque.agent = agent;
                    try
                    {
                        recu.cheque.MontantCheque = double.Parse(TextMontant.Text.Replace(" ", ""));
                    }
                    catch (Exception)
                    {
                    }
                    recu.cheque.NumCompte = TextNumCompte.Text;
                    recu.cheque.TitulaireCheque = TextTitulaire.Text;
                }

            }
            #endregion
        }

        private void insertToObjetBonDeCommande(crlBonDeCommande bonDeCommande)
        {
            #region implementation
            if (bonDeCommande != null)
            {
                bonDeCommande.agent = agent;
                try
                {
                    bonDeCommande.DatePaiementBC = Convert.ToDateTime(TextDatePaiementBC.Text);
                }
                catch (Exception)
                {
                }
                bonDeCommande.DescriptionBC = TextDescriptionBC.Text;
                bonDeCommande.MatriculeAgent = agent.matriculeAgent;
                try
                {
                    bonDeCommande.MontantBC = double.Parse(TextMontantProforma.Text.Replace(" ", ""));
                }
                catch (Exception)
                {
                }
                bonDeCommande.NumProforma = hfNumProforma.Value;
            }
            #endregion
        }

        private List<List<crlBillet>> insertBillet(List<crlBilletCommande> billetCommandes)
        {
            #region declaration
            List<crlBillet> billets = null;
            List<List<crlBillet>> billetsReturn = null;
            #endregion

            #region implementation
            if (billetCommandes != null)
            {
                billetsReturn = new List<List<crlBillet>>();
                for (int i = 0; i < billetCommandes.Count; i++)
                {
                    billets = serviceBilletCommande.getBillet(billetCommandes[i], agent);
                    if (billets != null)
                    {
                        for (int j = 0; j < billets.Count; j++)
                        {
                            billets[j].NumBillet = serviceBillet.insertBillet(billets[j]);
                        }
                    }
                    billetsReturn.Add(billets);
                    billets = null;
                }
            }
            #endregion

            return billetsReturn;
        }

        private List<crlCommission> insertCommission(List<crlCommissionDevis> commissionDevis)
        {
            #region declaration
            crlCommission commission = null;
            List<crlCommission> commissionReturn = null;
            #endregion

            #region implementation
            if (commissionDevis != null)
            {
                commissionReturn = new List<crlCommission>();
                for (int i = 0; i < commissionDevis.Count; i++)
                {
                    commission = serviceCommissionDevis.getCommission(commissionDevis[i], agent);

                    if (commission != null)
                    {
                        commission.IdCommission = serviceCommission.insertCommission(commission);
                    }

                    commissionReturn.Add(commission);
                    commission = null;

                }
            }
            #endregion

            return commissionReturn;
        }

        private List<List<crlDureeAbonnement>> insertDureeAbonnement(List<crlDureeAbonnementDevis> dureeAbonnementDevis)
        {
            #region declaration
            List<crlDureeAbonnement> dureeAbonnements = null;
            List<List<crlDureeAbonnement>> dureeAbonnementReturn = null;
            #endregion


            #region implementation
            if (dureeAbonnementDevis != null)
            {
                dureeAbonnementReturn = new List<List<crlDureeAbonnement>>();
                for (int i = 0; i < dureeAbonnementDevis.Count; i++)
                {
                    dureeAbonnements = serviceDureeAbonnementDevis.getDureeAbonnement(dureeAbonnementDevis[i], agent);
                    if (dureeAbonnements != null)
                    {
                        for (int j = 0; j < dureeAbonnements.Count; j++)
                        {
                            dureeAbonnements[j].NumDureeAbonnement = serviceDureeAbonnement.insertDureeAbonnement(dureeAbonnements[j]);
                        }

                        dureeAbonnementReturn.Add(dureeAbonnements);
                        dureeAbonnements = null;
                    }
                }
            }
            #endregion

            return dureeAbonnementReturn;
        }

        private List<crlVoyageAbonnement> insertVoyageAbonnement(List<crlVoyageAbonnementDevis> voyageAbonnementDevis)
        {
            #region declaration
            crlVoyageAbonnement voyageAbonnement = null;
            List<crlVoyageAbonnement> voyageAbonnementReturn = null;
            #endregion

            #region implementation
            if (voyageAbonnementDevis != null)
            {
                voyageAbonnementReturn = new List<crlVoyageAbonnement>();
                for (int i = 0; i < voyageAbonnementDevis.Count; i++)
                {
                    voyageAbonnement = serviceVoyageAbonnementDevis.getVoyageAbonnement(voyageAbonnementDevis[i], agent);

                    if (voyageAbonnement != null)
                    {
                        voyageAbonnement.NumVoyageAbonnement = serviceVoyageAbonnement.insertVoyageAbonnement(voyageAbonnement);
                    }
                    voyageAbonnementReturn.Add(voyageAbonnement);
                    voyageAbonnement = null;
                }

            }
            #endregion

            return voyageAbonnementReturn;
        }

        private void insertBCDAVA(crlProforma proforma, List<List<crlBillet>> billets, List<List<crlDureeAbonnement>> dureeAbonnements, List<crlCommission> commissions, List<crlVoyageAbonnement> voyageAbonnements)
        {
            #region declaration
            string numBillets = "";
            string idCommissions = "";
            #endregion

            #region implementation
            if (proforma != null)
            {
                if (proforma.billetCommande != null)
                {
                    billets = this.insertBillet(proforma.billetCommande);
                }
                if (proforma.commissionDevis != null)
                {
                    commissions = this.insertCommission(proforma.commissionDevis);
                }
                if (proforma.dureeAbonnementDevis != null)
                {
                    dureeAbonnements = this.insertDureeAbonnement(proforma.dureeAbonnementDevis);
                }
                if (proforma.voyageAbonnementDevis != null)
                {
                    voyageAbonnements = this.insertVoyageAbonnement(proforma.voyageAbonnementDevis);
                }
            }

            if (billets != null)
            {
                for (int i = 0; i < billets.Count; i++)
                {
                    for (int j = 0; j < billets[i].Count; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            numBillets = billets[i][j].NumBillet;
                        }
                        else
                        {
                            numBillets += ";" + billets[i][j].NumBillet;
                        }
                    }
                }
            }

            if (numBillets != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdfBillets",
                    string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=billet&numBillet=" + numBillets, 700,
                    500, 10, 10), true);
            }

            if (commissions != null)
            {
                for (int i = 0; i < commissions.Count; i++)
                {
                    if (i == 0)
                    {
                        idCommissions = commissions[i].IdCommission;
                    }
                    else
                    {
                        idCommissions += ";" + commissions[i].IdCommission;
                    }
                }
            }

            if (idCommissions != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdfCommissions",
                    string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=commission&idCommission=" + idCommissions, 700,
                    500, 10, 10), true);
            }
            #endregion
        }
        #endregion

        #region verification paiement proforma
        private string verificationPaiementCheque(crlProforma proforma)
        {
            #region declaration
            string indicationText = "";
            #endregion

            #region implementation
            if (proforma != null)
            {

                if (proforma.individu != null)
                {
                    if (proforma.individu.IsCheque > 0)
                    {
                        indicationText = "Client " + proforma.individu.NomIndividu + " " + proforma.individu.PrenomIndividu;
                        indicationText += " non autorisé à payer par chèque.";
                    }
                }
                else if (proforma.societe != null)
                {
                    if (proforma.societe.IsCheque > 0)
                    {
                        indicationText = "Société " + proforma.societe.NomSociete;
                        indicationText += " non autorisé à payer par chèque.";
                    }
                }
                else if (proforma.organisme != null)
                {
                    if (proforma.organisme.IsCheque > 0)
                    {
                        indicationText = "Organisme " + proforma.organisme.NomOrganisme;
                        indicationText += " non autorisé à payer par chèque.";
                    }
                }
            }
            #endregion

            return indicationText;
        }

        private string verificationPaiementBonCommande(crlProforma proforma)
        {
            #region declaration
            string indicationText = "";
            #endregion

            #region implementation
            if (proforma != null)
            {

                if (proforma.individu != null)
                {
                    if (proforma.individu.IsBonCommande > 0)
                    {
                        indicationText = "Client " + proforma.individu.NomIndividu + " " + proforma.individu.PrenomIndividu;
                        indicationText += " non autorisé à payer par bon de commande.";
                    }
                }
                else if (proforma.societe != null)
                {
                    if (proforma.societe.IsBonCommande > 0)
                    {
                        indicationText = "Société " + proforma.societe.NomSociete;
                        indicationText += " non autorisé à payer par bon de commande.";
                    }
                }
                else if (proforma.organisme != null)
                {
                    if (proforma.organisme.IsBonCommande > 0)
                    {
                        indicationText = "Organisme " + proforma.organisme.NomOrganisme;
                        indicationText += " non autorisé à payer par bon de commande.";
                    }
                }

            }
            #endregion

            return indicationText;
        }
        #endregion

        #endregion

        #region event
        protected void ddlVilleDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceVille.loadDdlVilleDestination(ddlDestination, ddlVilleDepart.SelectedValue);
            this.affichePrix();
        }

        protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrix();
        }

        protected void ddlCalculePrixBillet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrix();
        }

        protected void RadioListeAbonnement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseOrganisme();
            this.initialiseSociete();
            this.initialiseIndividu();
        }
        #endregion


        protected void TextNombreBillet_TextChanged(object sender, EventArgs e)
        {
            this.affichePrix();
        }

        protected void btnEnregistrerProforma_Click(object sender, EventArgs e)
        {
            Panel_Formulaire.CssClass = "PanneauAction";
            Panel_Formulaire.Visible = true;
            this.initialiseFormulaireClient();
            this.initialiseFormulaireOrganisme();
            this.initialiseFormulaireSociete();
        }
        protected void btnAnnulerProforma_Click(object sender, EventArgs e)
        {
            #region declaration
            bool isDelete = false;
            crlProforma proforma = null;
            #endregion

            #region implementation
            if (hfNumProforma.Value != "")
            {
                proforma = serviceProforma.selectProforma(hfNumProforma.Value);
                if (proforma != null)
                {
                    isDelete = serviceProforma.deleteProforma(proforma);
                    if (isDelete)
                    {
                        this.initialiseGridProforma();
                        hfNumProforma.Value = "";
                        this.initialiseGridBilletProforma();
                        this.initialiseGridCommissionDevis();

                    }
                }
            }
            this.initialiseFormulaire();
            this.initialiseFormulaireCommission();
            this.initialiseFormulairePaiement();
            #endregion
        }
        protected void gvBilletProforma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBilletProforma.PageIndex = e.NewPageIndex;
            this.initialiseGridBilletProforma();

        }
        protected void gvBilletProforma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheBilletCommande(e.CommandArgument.ToString());
                this.divIndication.InnerHtml = "";
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                this.serviceGeneral.delete("billetcommande", "numBilletCommande", e.CommandArgument.ToString());
                this.initialiseGridBilletProforma();
            }
        }

        protected void ddlTriProforma_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridProforma();
        }
        protected void btnRechercheProforma_Click(object sender, EventArgs e)
        {
            this.initialiseGridProforma();
        }
        protected void gvProforma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProforma.PageIndex = e.NewPageIndex;
            this.initialiseGridProforma();
        }
        protected void gvProforma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region declaration
            bool isDelete = false;
            crlProforma proforma = null;
            #endregion

            if (e.CommandName.Equals("select"))
            {
                this.afficheProforma(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                proforma = serviceProforma.selectProforma(e.CommandArgument.ToString());
                if (proforma != null)
                {
                    isDelete = serviceProforma.deleteProforma(proforma);
                    if (isDelete)
                    {
                        this.initialiseGridProforma();
                        hfNumProforma.Value = "";
                        this.initialiseGridBilletProforma();
                        this.initialiseGridCommissionDevis();
                    }
                }
            }
        }
        protected void btnNouvelleCommande_Click(object sender, EventArgs e)
        {
            #region declaration
            bool isDelete = false;
            crlProforma proforma = null;
            #endregion

            #region implementation
            if (hfNumProforma.Value != "")
            {
                proforma = serviceProforma.selectProforma(hfNumProforma.Value);
                if (proforma != null)
                {
                    if (proforma.individu == null && proforma.organisme == null && proforma.societe == null)
                    {
                        isDelete = serviceProforma.deleteProforma(proforma);
                    }

                    if (isDelete)
                    {
                        this.initialiseGridProforma();

                    }
                }
            }

            hfNumProforma.Value = "";
            this.initialiseGridBilletProforma();
            this.initialiseGridCommissionDevis();
            this.initialiseFormulaire();
            this.initialiseFormulaireCommission();
            this.initialiseFormulairePaiement();
            #endregion
        }

        protected void btnAjouter_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProforma proforma = null;
            crlBilletCommande billetCommande = null;
            string strIndication = "";
            #endregion

            #region implementation

            if (hfNumProforma.Value != "")
            {
                proforma = serviceProforma.selectProforma(hfNumProforma.Value);
                if (proforma != null)
                {
                    billetCommande = new crlBilletCommande();
                    this.insertObjetBilletCommande(billetCommande);
                    billetCommande.NumProforma = proforma.NumProforma;
                    billetCommande.NumBilletCommande = serviceBilletCommande.insertBilletCommande(billetCommande, agent.agence.SigleAgence);

                    if (billetCommande.NumBilletCommande != "")
                    {
                        strIndication = "Commande billet bien enregistrer!<br/>";
                        strIndication += "Montant total:" + serviceGeneral.separateurDesMilles((billetCommande.MontantBilletCommande * billetCommande.NombreBilletCommande).ToString("0")) + "Ar";
                        this.divIndicationText(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationText(strIndication, "Red");
                    }
                    this.afficheProforma(proforma.NumProforma);
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                proforma = new crlProforma();
                proforma.MatriculeAgent = agent.matriculeAgent;
                proforma.agent = agent;

                proforma.NumProforma = serviceProforma.insertProforma(proforma);
                if (proforma.NumProforma != "")
                {
                    billetCommande = new crlBilletCommande();
                    this.insertObjetBilletCommande(billetCommande);
                    billetCommande.NumProforma = proforma.NumProforma;
                    billetCommande.NumBilletCommande = serviceBilletCommande.insertBilletCommande(billetCommande, agent.agence.SigleAgence);

                    if (billetCommande.NumBilletCommande != "")
                    {
                        strIndication = "Commande billet bien enregistrer!<br/>";
                        strIndication += "Montant total:" + serviceGeneral.separateurDesMilles((billetCommande.MontantBilletCommande * billetCommande.NombreBilletCommande).ToString("0")) + "Ar";
                        this.divIndicationText(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationText(strIndication, "Red");
                    }
                    this.afficheProforma(proforma.NumProforma);
                }
            }

            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlBilletCommande billetCommande = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumBilletCommande.Value != "")
            {
                billetCommande = serviceBilletCommande.selectBilletCommande(hfNumBilletCommande.Value);
                if (billetCommande != null)
                {
                    this.insertObjetBilletCommande(billetCommande);
                    serviceBilletCommande.updateBilletCommande(billetCommande);

                    this.afficheBilletCommande(billetCommande.NumBilletCommande);
                    this.initialiseGridBilletProforma();
                    strIndication = "Modification commande billet bien enregistrer!<br/>";
                    strIndication += "Montant total:" + serviceGeneral.separateurDesMilles((billetCommande.MontantBilletCommande * billetCommande.NombreBilletCommande).ToString("0")) + "Ar";
                    this.divIndicationText(strIndication, "Black");
                }
                else
                {
                    strIndication = "Une erreur ca produit durant l'enregistrement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                strIndication = "Veuillez sélectionner un billet avant de faire une modification!";
                this.divIndicationText(strIndication, "Red");
            }
            #endregion
        }
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            divIndication.InnerHtml = "";
            this.initialiseFormulaireBillet();
        }


        protected void btnValideEnregistrementProforma_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProforma proforma = null;
            #endregion

            #region implementation
            if (hfNumProforma.Value != "")
            {
                proforma = serviceProforma.selectProforma(hfNumProforma.Value);
                if (proforma != null)
                {
                    if (RadioListeAbonnement.SelectedValue.Equals("individu"))
                    {
                        proforma.individu = new crlIndividu();
                        this.insertToObjetIndividu(proforma.individu);
                        proforma.individu.NumIndividu = serviceIndividu.isIndividu(proforma.individu);

                        if (proforma.individu.NumIndividu != "")
                        {
                            serviceIndividu.updateIndividu(proforma.individu);
                        }
                        else
                        {
                            proforma.individu.NumIndividu = serviceIndividu.insertIndividu(proforma.individu, agent.agence.SigleAgence);
                        }

                        if (proforma.individu.NumIndividu != "")
                        {
                            proforma.NumIndividu = proforma.individu.NumIndividu;
                            serviceProforma.updateProforma(proforma);
                        }
                    }
                    else
                    {
                        proforma.individu = null;
                        proforma.NumIndividu = "";
                    }
                    if (RadioListeAbonnement.SelectedValue.Equals("societe"))
                    {
                        proforma.societe = new crlSociete();
                        this.insertToObjetSociete(proforma.societe);
                        proforma.societe.NumSociete = serviceSociete.isSociete(proforma.societe);

                        if (proforma.societe.NumSociete != "")
                        {
                            serviceSociete.updateSociete(proforma.societe);
                        }
                        else
                        {
                            proforma.societe.NumSociete = serviceSociete.insertSociete(proforma.societe, agent.agence.SigleAgence);
                        }

                        if (proforma.societe.NumSociete != "")
                        {
                            proforma.NumSociete = proforma.societe.NumSociete;
                            serviceProforma.updateProforma(proforma);
                        }
                    }
                    else
                    {
                        proforma.societe = null;
                        proforma.NumSociete = "";
                    }
                    if (RadioListeAbonnement.SelectedValue.Equals("organisme"))
                    {
                        proforma.organisme = new crlOrganisme();
                        this.insertToObjetOrganisme(proforma.organisme);
                        proforma.organisme.NumOrganisme = serviceOrganisme.isOrganisme(proforma.organisme);

                        if (proforma.organisme.NumOrganisme != "")
                        {
                            serviceOrganisme.updateOrganisme(proforma.organisme);
                        }
                        else
                        {
                            proforma.organisme.NumOrganisme = serviceOrganisme.insertOrganisme(proforma.organisme, agent.agence.SigleAgence);
                        }

                        if (proforma.organisme.NumOrganisme != "")
                        {
                            proforma.NumOrganisme = proforma.organisme.NumOrganisme;
                            serviceProforma.updateProforma(proforma);
                        }
                    }
                    else
                    {
                        proforma.organisme = null;
                        proforma.NumOrganisme = "";
                    }

                    this.initialiseGridProforma();
                    this.afficheProforma(proforma.NumProforma);
                    Panel_Formulaire.Visible = false;
                    btnEnregistrerProforma.Enabled = false;
                    btnAnnulerProforma.Enabled = false;
                }
            }
            #endregion
        }
        protected void btnAnnulerEnregistrementProforma_Click(object sender, EventArgs e)
        {
            Panel_Formulaire.Visible = false;
        }


        protected void gvClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClient.PageIndex = e.NewPageIndex;
            this.initialiseGridIndividu();
        }
        protected void gvClient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheIndividu(e.CommandArgument.ToString());
            }
        }
        protected void btnRechercheClient_Click(object sender, EventArgs e)
        {
            this.initialiseGridIndividu();
        }
        protected void ddlTriClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridIndividu();
        }


        protected void gvSociete_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSociete.PageIndex = e.NewPageIndex;
            this.initialiseGridSociete();
        }
        protected void gvSociete_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheSociete(e.CommandArgument.ToString());
            }
        }
        protected void btnRechercheSociete_Click(object sender, EventArgs e)
        {
            this.initialiseGridSociete();
        }
        protected void ddlTriSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridSociete();
        }


        protected void ddlTriOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridOrganisme();
        }
        protected void btnRechercheOrganisme_Click(object sender, EventArgs e)
        {
            this.initialiseGridOrganisme();
        }
        protected void gvOrganisme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrganisme.PageIndex = e.NewPageIndex;
            this.initialiseGridOrganisme();
        }
        protected void gvOrganisme_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheOrganisme(e.CommandArgument.ToString());
            }
        }

        protected void btnValideBillet_Click(object sender, EventArgs e)
        {
            #region declaration
            crlBillet billet = null;
            crlVoyageAbonnement voyageAbonnement = null;
            crlDureeAbonnement dureeAbonnement = null;
            int nombreBilletVA = 0;
            int nombreBillet = 0;
            int nombreBilletEnregistre = 0;
            string numBillets = "";

            string strIndication = "";
            #endregion

            #region implementation
            if (agent.sessionCaisse != null)
            {
                if (hfNumVoyageAbonnement.Value != "" && Panel_AbonnementNbVoyage.Visible.Equals(true))
                {
                    voyageAbonnement = serviceVoyageAbonnement.selectVoyageAbonnement(hfNumVoyageAbonnement.Value);

                    if (voyageAbonnement != null)
                    {
                        try
                        {
                            nombreBilletVA = int.Parse(TextNbBillet.Text);
                        }
                        catch (Exception)
                        {

                        }

                        if (nombreBilletVA <= voyageAbonnement.NbVoyageAbonnement && nombreBilletVA > 0)
                        {
                            for (int i = 0; i < nombreBilletVA; i++)
                            {
                                billet = new crlBillet();
                                billet.agent = agent;
                                try
                                {
                                    billet.DateDeValidite = DateTime.Now.AddMonths(int.Parse(ReGlobalParam.nbValiditeBillet));
                                }
                                catch (Exception)
                                {
                                    billet.DateDeValidite = DateTime.Now.AddMonths(1);
                                }
                                billet.MatriculeAgent = agent.matriculeAgent;
                                billet.ModePaiement = "Abonnement";
                                billet.NumCalculCategorieBillet = voyageAbonnement.NumCalculCategorieBillet;
                                billet.NumCalculReductionBillet = voyageAbonnement.NumCalculReductionBillet;
                                billet.NumTrajet = voyageAbonnement.NumTrajet;
                                billet.NumVoyageAbonnement = voyageAbonnement.NumVoyageAbonnement;
                                billet.PrixBillet = voyageAbonnement.PrixUnitaire.ToString("0");

                                if (voyageAbonnement.abonnement != null)
                                {
                                    /*jereo tsara le billet*/
                                    billet.NumIndividu = voyageAbonnement.abonnement.NumIndividu;
                                }

                                billet.NumBillet = serviceBillet.insertBillet(billet);

                                if (billet.NumBillet != "")
                                {
                                    //serviceSessionCaisse.insertAssocSessionCaisseBillet(billet.NumBillet, agent.sessionCaisse.NumSessionCaisse);
                                    nombreBilletEnregistre++;

                                    if (i == 0)
                                    {
                                        numBillets = billet.NumBillet;
                                    }
                                    else
                                    {
                                        numBillets += ";" + billet.NumBillet;
                                    }



                                }

                                billet = null;
                            }

                            voyageAbonnement.NbVoyageAbonnement = voyageAbonnement.NbVoyageAbonnement - nombreBilletEnregistre;
                            serviceVoyageAbonnement.updateVoyageAbonnement(voyageAbonnement);

                            strIndication = nombreBilletEnregistre + "/" + nombreBilletVA + " des billets sont bien enregistré!";
                            this.divIndicationText(strIndication, "Black");

                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=billet&numBillet=" + numBillets, 700,
                                500, 10, 10), true);

                            this.initialiseFormulaireBillet();

                        }
                        else
                        {
                            strIndication = "Le nombre de billet doit être inférieur ou égal à " + voyageAbonnement.NbVoyageAbonnement + ".";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'exécution, <br/>veuillez reessayer s'il vous plaît.";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else if (hfNumDureeAbonnement.Value != "" && Panel_AbonnementDureeTemps.Visible.Equals(true))
                {
                    dureeAbonnement = serviceDureeAbonnement.selectDureeAbonnement(hfNumDureeAbonnement.Value);

                    if (dureeAbonnement != null)
                    {
                        billet = new crlBillet();
                        billet = new crlBillet();
                        billet.agent = agent;
                        try
                        {
                            billet.DateDeValidite = DateTime.Now.AddMonths(int.Parse(ReGlobalParam.nbValiditeBillet));
                        }
                        catch (Exception)
                        {
                            billet.DateDeValidite = DateTime.Now.AddMonths(1);
                        }
                        billet.MatriculeAgent = agent.matriculeAgent;
                        billet.ModePaiement = "Abonnement";
                        billet.NumCalculCategorieBillet = dureeAbonnement.NumCalculCategorieBillet;
                        billet.NumCalculReductionBillet = dureeAbonnement.NumCalculReductionBillet;
                        billet.NumDureeAbonnement = dureeAbonnement.NumDureeAbonnement;
                        billet.NumTrajet = dureeAbonnement.NumTrajet;
                        billet.PrixBillet = dureeAbonnement.PrixUnitaire.ToString();

                        if (dureeAbonnement.abonnement != null)
                        {
                            /**************************/
                            billet.NumIndividu = dureeAbonnement.abonnement.NumIndividu;
                        }

                        billet.NumBillet = serviceBillet.insertBillet(billet);

                        if (billet.NumBillet != "")
                        {
                            strIndication = "Billet bien enregistré!";
                            this.divIndicationText(strIndication, "Black");

                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=billet&numBillet=" + billet.NumBillet, 700,
                                500, 10, 10), true);

                            this.initialiseFormulaireBillet();
                        }
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'exécution, <br/>veuillez reessayer s'il vous plaît.";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else
                {

                    try
                    {
                        nombreBillet = int.Parse(TextNombreBillet.Text);
                    }
                    catch (Exception)
                    {
                    }

                    if (nombreBillet > 0)
                    {
                        for (int i = 0; i < nombreBillet; i++)
                        {
                            billet = new crlBillet();
                            this.insertObjetBillet(billet);
                            billet.NumBillet = serviceBillet.insertBillet(billet);

                            if (billet.NumBillet != "")
                            {
                                serviceSessionCaisse.insertAssocSessionCaisseBillet(billet.NumBillet, agent.sessionCaisse.NumSessionCaisse);
                                nombreBilletEnregistre++;

                                if (i == 0)
                                {
                                    numBillets = billet.NumBillet;
                                }
                                else
                                {
                                    numBillets += ";" + billet.NumBillet;
                                }



                            }

                            billet = null;
                        }

                        strIndication = nombreBilletEnregistre + "/" + nombreBillet + " des billets sont bien enregistré!";
                        this.divIndicationText(strIndication, "Black");

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                            string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=billet&numBillet=" + numBillets, 700,
                            500, 10, 10), true);

                        this.initialiseFormulaireBillet();

                    }
                }

            }
            else
            {
                strIndication = "Votre session est encore inactive!<br/>";
                strIndication += "Veuillez contacter votre responsable";
                this.divIndicationText(strIndication, "Red");
                //pas de session
            }
            #endregion
        }
        protected void btnBilletAuNomDe_Click(object sender, EventArgs e)
        {
            Panel_FormulaireClient.CssClass = "PanneauAction";
            this.initialiseFormulaireClientListe();
            Panel_FormulaireClient.Visible = true;
        }



        #region formulaire Client

        #region methode
        private void initialiseFormulaireClientListe()
        {
            TextClientAdesse.Text = "";
            TextClientCin.Text = "";
            TextClientFixe.Text = "";
            TextClientMobile.Text = "";
            TextClientNom.Text = "";
            TextClientPrenom.Text = "";
        }

        private void initialiseGridIndividuListe()
        {
            serviceIndividu.insertToGridIndividu(gvClientListe, ddlTriClientListe.SelectedValue, ddlTriClientListe.SelectedValue, TextRechercheClientListe.Text);
        }

        private void afficheIndividutListe(string numIndividu)
        {
            #region declaration
            crlIndividu individu = null;
            #endregion

            #region implementation
            if (numIndividu != "")
            {
                individu = serviceIndividu.selectIndividu(numIndividu);
                if (individu != null)
                {
                    TextClientAdesse.Text = individu.Adresse;
                    TextClientCin.Text = individu.CinIndividu;
                    TextClientFixe.Text = individu.TelephoneFixeIndividu;
                    TextClientMobile.Text = individu.TelephoneFixeIndividu;
                    TextClientNom.Text = individu.NomIndividu;
                    TextClientPrenom.Text = individu.PrenomIndividu;
                }
            }
            #endregion
        }

        private void insertToObjetIndividuListe(crlIndividu individu)
        {
            #region implementation
            if (individu != null)
            {
                individu.Adresse = TextClientAdesse.Text;
                individu.CinIndividu = TextClientCin.Text;
                individu.TelephoneMobileIndividu = TextClientMobile.Text;
                individu.NomIndividu = TextClientNom.Text;
                individu.PrenomIndividu = TextClientPrenom.Text;
                individu.TelephoneFixeIndividu = TextClientFixe.Text;
            }
            #endregion
        }
        #endregion

        protected void btnValiderFormulaireClient_Click(object sender, EventArgs e)
        {
            #region declaration
            crlIndividu individu = null;
            #endregion

            #region implementation
            individu = new crlIndividu();
            this.insertToObjetIndividuListe(individu);
            individu.NumIndividu = serviceIndividu.isIndividu(individu);
            if (individu.NumIndividu != "")
            {
                individu = serviceIndividu.selectIndividu(individu.NumIndividu);
            }
            else
            {
                individu.NumIndividu = serviceIndividu.insertIndividu(individu, agent.agence.SigleAgence);
            }
            if (individu.NumIndividu != "")
            {
                LabelClient.Text = individu.PrenomIndividu + " " + individu.NomIndividu;
                hfNumClient.Value = individu.NumIndividu;
            }
            Panel_FormulaireClient.Visible = false;
            //btnAjouter.Enabled = false;
            #endregion
        }
        protected void btnAnnulerFormulaireClient_Click(object sender, EventArgs e)
        {
            hfNumClient.Value = "";
            LabelClient.Text = "";
            Panel_FormulaireClient.Visible = false;
        }
        protected void gvClientListe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClientListe.PageIndex = e.NewPageIndex;
            this.initialiseGridIndividuListe();
        }
        protected void gvClientListe_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheIndividutListe(e.CommandArgument.ToString());
            }
        }
        protected void btnRechercheClientListe_Click(object sender, EventArgs e)
        {
            this.initialiseGridIndividuListe();
        }
        protected void ddlTriClientListe_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridIndividuListe();
        }
        #endregion

        #region expediteur
        protected void btnValideExpediteur_Click(object sender, EventArgs e)
        {
            #region declaration
            crlClient client = null;
            #endregion

            #region implementation
            client = new crlClient();
            this.insertToObjetExpediteur(client);
            client.NumClient = serviceClient.isClient(client);
            if (client.NumClient != "")
            {
                client = serviceClient.selectClient(client.NumClient);
            }
            else
            {
                client.NumClient = serviceClient.insertClient(client, agent.agence.SigleAgence);
            }
            if (client.NumClient != "")
            {
                LabelExpediteur.Text = client.PrenomClient + " " + client.NomClient;
                hfNumExpediteur.Value = client.NumClient;
            }
            Panel_FormulaireExpediteurCommission.Visible = false;
            //btnAjouterCommissionDevis.Enabled = false;
            #endregion

        }
        protected void btnAnnulerExpediteur_Click(object sender, EventArgs e)
        {
            hfNumExpediteur.Value = "";
            LabelExpediteur.Text = "";
            Panel_FormulaireExpediteurCommission.Visible = false;
        }

        protected void btnRechercheListeExpediteur_Click(object sender, EventArgs e)
        {
            this.initialiseGridExpediteur();
        }
        protected void ddlTriListeExpediteur_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridExpediteur();
        }
        protected void gvListeExpediteur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListeExpediteur.PageIndex = e.NewPageIndex;
            this.initialiseGridExpediteur();
        }
        protected void gvListeExpediteur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheExpediteur(e.CommandArgument.ToString());
            }
        }
        #endregion
        protected void btnExpediteur_Click(object sender, EventArgs e)
        {
            Panel_FormulaireExpediteurCommission.CssClass = "PanneauActionRight";
            this.initialiseFormulaireExpediteur();
            Panel_FormulaireExpediteurCommission.Visible = true;
        }

        #region recepteur
        protected void btnValideRecepteurCommission_Click(object sender, EventArgs e)
        {
            #region declaration
            crlReceptionnaire receptionnaire = null;
            #endregion

            #region implementation
            receptionnaire = new crlReceptionnaire();
            this.insertToObjetReceptionnaire(receptionnaire);

            receptionnaire.IdPersonne = serviceReceptionnaire.isPersonne(receptionnaire);
            if (receptionnaire.IdPersonne != "")
            {
                receptionnaire = serviceReceptionnaire.selectPersonne(receptionnaire.IdPersonne);
            }
            else
            {
                receptionnaire.IdPersonne = serviceReceptionnaire.insertPersonne(receptionnaire, agent.agence.SigleAgence);
            }
            if (receptionnaire.IdPersonne != "")
            {
                LabelRecepteur.Text = receptionnaire.PrenomPersonne + " " + receptionnaire.NomPersonne;
                hfNumReceptionnaire.Value = receptionnaire.IdPersonne;
            }
            Panel_FormulaireRecepteurCommission.Visible = false;
            //btnAjouterCommissionDevis.Enabled = false;
            #endregion
        }
        protected void btnAnnulerRecepteurCommission_Click(object sender, EventArgs e)
        {
            hfNumReceptionnaire.Value = "";
            LabelRecepteur.Text = "";
            Panel_FormulaireRecepteurCommission.Visible = false;
        }

        protected void btnRechercheRecepteur_Click(object sender, EventArgs e)
        {
            this.initialiseGridRecepteur();
        }
        protected void ddlTriRecepteur_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridRecepteur();
        }
        protected void gvRecepteur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecepteur.PageIndex = e.NewPageIndex;
            this.initialiseGridRecepteur();
        }
        protected void gvRecepteur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheRecepteur(e.CommandArgument.ToString());
            }
        }
        #endregion
        protected void btnRecepteur_Click(object sender, EventArgs e)
        {
            Panel_FormulaireRecepteurCommission.CssClass = "PanneauActionRight";
            this.initialiseFormulaireRecepteur();
            Panel_FormulaireRecepteurCommission.Visible = true;
        }

        #region formulaire commission
        protected void ddlVilleDepartCommission_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceVille.loadDdlVilleDestination(ddlDestinationCommission, ddlVilleDepartCommission.SelectedValue);
            this.affichePrixCommission();
        }
        protected void ddlDestinationCommission_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrixCommission();
        }
        protected void ddlTypeCommission_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlDesignation();
            this.affichePrixCommission();
        }
        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrixCommission();
        }
        protected void TextNombreCommission_TextChanged(object sender, EventArgs e)
        {
            this.affichePrixCommission();
        }
        protected void TextPoidsCommission_TextChanged(object sender, EventArgs e)
        {
            this.affichePrixCommission();
        }
        protected void btnAjouterCommissionDevis_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProforma proforma = null;
            crlCommissionDevis commissionDevis = null;
            string strIndication = "";
            #endregion

            #region implementation

            if (hfNumExpediteur.Value != "")
            {
                if (hfNumProforma.Value != "")
                {
                    proforma = serviceProforma.selectProforma(hfNumProforma.Value);
                    if (proforma != null)
                    {
                        commissionDevis = new crlCommissionDevis();
                        this.insertToObjetCommissionDevis(commissionDevis);
                        commissionDevis.NumProforma = proforma.NumProforma;
                        commissionDevis.IdCommissionDevis = serviceCommissionDevis.insertCommissionDevis(commissionDevis, agent.agence.SigleAgence);

                        if (commissionDevis.NumDesignation != "")
                        {
                            strIndication = "Commission bien enregistrer!<br/>";
                            strIndication += "Montant:" + serviceGeneral.separateurDesMilles(commissionDevis.FraisEnvoi.ToString("0")) + "Ar";
                            this.divIndicationCommissionText(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                            this.divIndicationCommissionText(strIndication, "Red");
                        }
                        this.afficheProforma(proforma.NumProforma);
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationCommissionText(strIndication, "Red");
                    }
                }
                else
                {
                    proforma = new crlProforma();
                    proforma.MatriculeAgent = agent.matriculeAgent;
                    proforma.agent = agent;

                    proforma.NumProforma = serviceProforma.insertProforma(proforma);
                    if (proforma.NumProforma != "")
                    {
                        commissionDevis = new crlCommissionDevis();
                        this.insertToObjetCommissionDevis(commissionDevis);
                        commissionDevis.NumProforma = proforma.NumProforma;
                        commissionDevis.IdCommissionDevis = serviceCommissionDevis.insertCommissionDevis(commissionDevis, agent.agence.SigleAgence);

                        if (commissionDevis.IdCommissionDevis != "")
                        {
                            strIndication = "Commission bien enregistrer!<br/>";
                            strIndication += "Montant:" + serviceGeneral.separateurDesMilles(commissionDevis.FraisEnvoi.ToString("0")) + "Ar";
                            this.divIndicationCommissionText(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                            this.divIndicationCommissionText(strIndication, "Red");
                        }
                        this.afficheProforma(proforma.NumProforma);
                    }
                }
            }
            else
            {
                strIndication = "Expédieur obligatoire!";
                this.divIndicationCommissionText(strIndication, "Red");
            }

            #endregion
        }
        protected void btnModifierCommissionDevis_Click(object sender, EventArgs e)
        {
            #region declaration
            crlCommissionDevis commissionDevis = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfIdCommissionDevis.Value != "")
            {
                commissionDevis = serviceCommissionDevis.selectCommissionDevis(hfIdCommissionDevis.Value);
                if (commissionDevis != null)
                {
                    this.insertToObjetCommissionDevis(commissionDevis);
                    serviceCommissionDevis.updateCommissionDevis(commissionDevis);

                    this.afficheCommissionDevis(commissionDevis.IdCommissionDevis);

                    this.initialiseGridCommissionDevis();

                    strIndication = "Modification commande commission bien enregistrer!<br/>";
                    strIndication += "Montant total:" + serviceGeneral.separateurDesMilles((commissionDevis.FraisEnvoi).ToString("0")) + "Ar";
                    this.divIndicationCommissionText(strIndication, "Black");
                }
                else
                {
                    strIndication = "Une erreur ca produit durant l'enregistrement!";
                    this.divIndicationCommissionText(strIndication, "Red");
                }
            }
            else
            {
                strIndication = "Veuillez sélectionner une commission avant de faire une modification!";
                this.divIndicationCommissionText(strIndication, "Red");
            }
            #endregion
        }
        protected void btnNewCommission_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireCommission();
            this.divIndicationCommissionText("", "Black");
        }
        protected void btnValiderCommission_Click(object sender, EventArgs e)
        {
            #region declaration
            crlCommission commission = null;

            string strIndication = "";
            #endregion

            #region implementation
            if (agent.sessionCaisse != null)
            {
                if (hfNumExpediteur.Value != "")
                {
                    commission = new crlCommission();
                    this.insertToObjetCommission(commission);
                    commission.IdCommission = serviceCommission.insertCommission(commission);

                    if (commission.IdCommission != "")
                    {
                        this.initialiseFormulaireCommission();
                        serviceSessionCaisse.insertAssocSessionCaisseCommission(commission.IdCommission, agent.sessionCaisse.NumSessionCaisse);
                        strIndication = "Commission bien enregistrer!<br/>";
                        strIndication += "Montant:" + serviceGeneral.separateurDesMilles(commission.FraisEnvoi) + "Ar";
                        this.divIndicationCommissionText(strIndication, "Black");

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                        string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=commission&idCommission=" + commission.IdCommission, 700,
                            500, 10, 10), true);
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationCommissionText(strIndication, "Red");
                    }
                }
                else
                {
                    strIndication = "Expédieur obligatoire!";
                    this.divIndicationCommissionText(strIndication, "Red");
                }
            }
            else
            {
                strIndication = "Votre session est encore inactive!<br/>";
                strIndication += "Veuillez contacter votre responsable";
                this.divIndicationCommissionText(strIndication, "Red");
                //pas de session
            }
            #endregion
        }
        #endregion


        protected void gvCommissionProforma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommissionProforma.PageIndex = e.NewPageIndex;
            this.initialiseGridCommissionDevis();
        }
        protected void gvCommissionProforma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheCommissionDevis(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                this.serviceGeneral.delete("commissiondevis", "idCommissionDevis", e.CommandArgument.ToString());
                this.initialiseGridCommissionDevis();
            }
        }


        #region abonnement
        protected void RadioButtonList_TypeAbonnement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialisePanelDureeAbonnement();
            this.initialisePanelVoyageAbonnement();
        }

        protected void btnAbonnement_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAbonnement abonnement = null;
            #endregion

            #region implementation
            if (TextNumAbonnement.Text != "")
            {
                abonnement = serviceAbonnement.selectAbonnement(TextNumAbonnement.Text);
                if (abonnement != null)
                {
                    hfNumAbonnement.Value = abonnement.NumAbonnement;
                    Panel_FormulaireAbonnement.CssClass = "PanneauAction";
                    RadioButtonList_TypeAbonnement.SelectedValue = "0";
                    this.initialisePanelDureeAbonnement();
                    this.initialisePanelVoyageAbonnement();
                    this.initialiseGridVoyageAbonnement();
                    this.initialiseGridDureeAbonnement();
                    Panel_FormulaireAbonnement.Visible = true;
                }
                else
                {
                    //
                }
            }
            else
            {
                //
            }
            #endregion
        }

        protected void btnAnnulerAbonnement_Click(object sender, EventArgs e)
        {
            Panel_FormulaireAbonnement.Visible = false;
        }

        protected void ddlTriVoyageAbonnement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridVoyageAbonnement();
        }
        protected void btnRechercheVoyageAbonnement_Click(object sender, EventArgs e)
        {
            this.initialiseGridVoyageAbonnement();
        }
        protected void gvVoyageAbonnement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVoyageAbonnement.PageIndex = e.NewPageIndex;
            this.initialiseGridVoyageAbonnement();
        }
        protected void gvVoyageAbonnement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheVoyageAbonnement(e.CommandArgument.ToString());
                btnAjouter.Enabled = false;
            }
            #endregion
        }

        protected void ddlTriAbonnementDureeTemps_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridDureeAbonnement();
        }
        protected void btnRechercheAbonnementDureeTemps_Click(object sender, EventArgs e)
        {
            this.initialiseGridDureeAbonnement();
        }
        protected void gvAbonnementDureeTemps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAbonnementDureeTemps.PageIndex = e.NewPageIndex;
            this.initialiseGridDureeAbonnement();
        }
        protected void gvAbonnementDureeTemps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheDureeAbonnement(e.CommandArgument.ToString());
                btnAjouter.Enabled = false;
            }
            #endregion
        }
        #endregion

        #region paiement
        protected void ddlModePaiement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divIndicationPaiementText("", "Red");
            this.initialisePanelBonCommande();
            this.initialisePanelCheque();
        }

        protected void btnValiderPaiement_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProforma proforma = null;
            crlRecuEncaisser recuEncaisser = null;
            crlBonDeCommande bonDeCommande = null;

            List<List<crlBillet>> billets = null;
            List<List<crlDureeAbonnement>> dureeAbonnements = null;
            List<crlCommission> commissions = null;
            List<crlVoyageAbonnement> voyageAbonnements = null;

            string strVerificationCheque = "";
            string strVerificationBonCommande = "";
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumProforma.Value != "")
            {
                proforma = serviceProforma.selectProforma(hfNumProforma.Value);
                if (proforma != null)
                {
                    if (proforma.individu != null || proforma.organisme != null || proforma.societe != null)
                    {
                        if (ddlModePaiement.SelectedValue.Equals("Espèce"))
                        {
                            if (agent.sessionCaisse != null)
                            {
                                proforma.ModePaiement = ddlModePaiement.SelectedValue;
                                recuEncaisser = new crlRecuEncaisser();
                                this.insertToObjetRecu(recuEncaisser);

                                recuEncaisser.NumRecuEncaisser = serviceRecuEncaisser.insertRecuEncaisser(recuEncaisser);

                                if (recuEncaisser.NumRecuEncaisser != "" && serviceProforma.updateProforma(proforma))
                                {
                                    if (serviceRecuEncaisser.insertAssocRecuEncaisserProformaBonDeCommande(proforma.NumProforma, recuEncaisser.NumRecuEncaisser, ""))
                                    {
                                        this.insertBCDAVA(proforma, billets, dureeAbonnements, commissions, voyageAbonnements);

                                        serviceSessionCaisse.insertAssocSessionCaisseRecuEncaisser(recuEncaisser.NumRecuEncaisser, agent.sessionCaisse.NumSessionCaisse);

                                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                            string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=recuEncaisser&numRecuEncaisser=" + recuEncaisser.NumRecuEncaisser, 700,
                                            500, 10, 10), true);

                                        this.initialiseGridProforma();

                                        hfNumProforma.Value = "";
                                        this.initialiseGridBilletProforma();
                                        this.initialiseGridCommissionDevis();
                                        this.initialiseFormulaire();
                                        this.initialiseFormulaireCommission();
                                        this.initialiseFormulairePaiement();


                                    }
                                    else
                                    {
                                        //
                                    }
                                }
                            }
                            else
                            {
                                strIndication = "Votre session est encore inactive!<br/>";
                                strIndication += "Veuillez contacter votre responsable.";
                                this.divIndicationPaiementText(strIndication, "Red");
                            }
                        }
                        else if (ddlModePaiement.SelectedValue.Equals("Chèque"))
                        {
                            if (agent.sessionCaisse != null)
                            {
                                strVerificationCheque = this.verificationPaiementCheque(proforma);

                                if (strVerificationCheque.Equals(""))
                                {
                                    proforma.ModePaiement = ddlModePaiement.SelectedValue;
                                    recuEncaisser = new crlRecuEncaisser();
                                    this.insertToObjetRecu(recuEncaisser);

                                    if (recuEncaisser.MontantRecuEncaisser <= recuEncaisser.cheque.MontantCheque)
                                    {

                                        recuEncaisser.NumRecuEncaisser = serviceRecuEncaisser.insertRecuEncaisserCheque(recuEncaisser);

                                        if (recuEncaisser.NumRecuEncaisser != "" && serviceProforma.updateProforma(proforma))
                                        {
                                            if (serviceRecuEncaisser.insertAssocRecuEncaisserProformaBonDeCommande(proforma.NumProforma, recuEncaisser.NumRecuEncaisser, ""))
                                            {
                                                this.insertBCDAVA(proforma, billets, dureeAbonnements, commissions, voyageAbonnements);

                                                //enregistrement du recu dans le session caisse
                                                serviceSessionCaisse.insertAssocSessionCaisseRecuEncaisser(recuEncaisser.NumRecuEncaisser, agent.sessionCaisse.NumSessionCaisse);

                                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                                    string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=recuEncaisser&numRecuEncaisser=" + recuEncaisser.NumRecuEncaisser, 700,
                                                    500, 10, 10), true);
                                            }
                                            else
                                            {
                                                //
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //
                                    }
                                }
                                else
                                {
                                    this.divIndicationPaiementText(strVerificationCheque, "Red");
                                }
                            }
                            else
                            {
                                strIndication = "Votre session est encore inactive!<br/>";
                                strIndication += "Veuillez contacter votre responsable.";
                                this.divIndicationPaiementText(strIndication, "Red");
                            }
                        }
                        else if (ddlModePaiement.SelectedValue.Equals("Bon de commande"))
                        {
                            strVerificationBonCommande = this.verificationPaiementBonCommande(proforma);

                            if (strVerificationBonCommande.Equals(""))
                            {
                                proforma.ModePaiement = ddlModePaiement.SelectedValue;
                                bonDeCommande = new crlBonDeCommande();
                                this.insertToObjetBonDeCommande(bonDeCommande);

                                bonDeCommande.NumBonDeCommande = serviceBonDeCommande.insertBonDeCommande(bonDeCommande, agent.agence.SigleAgence);

                                if (bonDeCommande.NumBonDeCommande != "")
                                {
                                    this.insertBCDAVA(proforma, billets, dureeAbonnements, commissions, voyageAbonnements);

                                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                        string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=bonCommande&numBonDeCommande=" + bonDeCommande.NumBonDeCommande, 700,
                                        500, 10, 10), true);
                                }
                                else
                                {
                                    //
                                }
                            }
                            else
                            {
                                this.divIndicationPaiementText(strVerificationBonCommande, "Red");
                            }
                        }

                        this.initialiseGridProforma();
                    }
                    else
                    {
                        //
                    }

                }
                else
                {
                    //
                }
            }
            else
            {
                //
            }

            #endregion
        }
        #endregion


        protected void btnEditer_Click(object sender, EventArgs e)
        {
            if (hfNumProforma.Value != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                    string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=proforma&numProforma=" + hfNumProforma.Value, 700,
                    500, 10, 10), true);
            }
        }
    }
}