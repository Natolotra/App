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
    public partial class DevisAbonnement : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAbonnement serviceAbonnement = null;
        IntfDalZone serviceZone = null;
        IntfDalCalculCategorieBillet serviceCalculCategorieBillet = null;
        IntfDalCalculReductionBillet serviceCalculReductionBillet = null;
        IntfDalVille serviceVille = null;
        IntfDalTrajet serviceTrajet = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalDureeAbonnementDevis serviceDureeAbonnementDevis = null;
        IntfDalVoyageAbonnementDevis serviceVoyageAbonnementDevis = null;
        IntfDalProforma serviceProforma = null;
        IntfDalVoyageAbonnement serviceVoyageAbonnement = null;
        IntfDalDureeAbonnement serviceDureeAbonnement = null;
        IntfDalSessionCaisse serviceSessionCaisse = null;
        IntfDalCommission serviceCommission = null;
        IntfDalCommissionDevis serviceCommissionDevis = null;
        IntfDalBillet serviceBillet = null;
        IntfDalBilletCommande serviceBilletCommande = null;
        IntfDalModePaiement serviceModePaiement = null;
        IntfDalRecuEncaisser serviceRecuEncaisser = null;
        IntfDalBonDeCommande serviceBonDeCommande = null;
        IntfDalLien serviceLien = null;

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
            serviceAbonnement = new ImplDalAbonnement();
            serviceZone = new ImplDalZone();
            serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            serviceVille = new ImplDalVille();
            serviceTrajet = new ImplDalTrajet();
            serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            serviceGeneral = new ImplDalGeneral();
            serviceVoyageAbonnementDevis = new ImplDalVoyageAbonnementDevis();
            serviceDureeAbonnementDevis = new ImplDalDureeAbonnementDevis();
            serviceProforma = new ImplDalProforma();
            serviceVoyageAbonnement = new ImplDalVoyageAbonnement();
            serviceDureeAbonnement = new ImplDalDureeAbonnement();
            serviceSessionCaisse = new ImplDalSessionCaisse();
            serviceCommission = new ImplDalCommission();
            serviceCommissionDevis = new ImplDalCommissionDevis();
            serviceBillet = new ImplDalBillet();
            serviceBilletCommande = new ImplDalBilletCommande();
            serviceModePaiement = new ImplDalModePaiement();
            serviceRecuEncaisser = new ImplDalRecuEncaisser();
            serviceBonDeCommande = new ImplDalBonDeCommande();
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

                Panel_Societe.Visible = false;
                Panel_Organisme.Visible = false;
                Panel_Individu.Visible = false;

                serviceVille.loadDdlVille(ddlVilleDepart);
                serviceVille.loadDdlVille(ddlDepartDureeTemps);

                this.initialiseFormulaireTrajetDureeTemps();
                this.initialiseFormulaireTrajetNombreVoyage();

                serviceZone.loadDDLAllZone(ddlZoneDureeTemps);
                serviceZone.loadDDLAllZone(ddlZoneNombreVoyage);

                serviceCalculCategorieBillet.loadDdlCulculeCategorieBillet(ddlCalculCategorieBillet);
                serviceCalculCategorieBillet.loadDdlCulculeCategorieBillet(ddlCategorieDureeTemps);

                this.initialisePanelSUNombreVoyage();
                this.initialisePanelTrajetNombreVoyage();

                this.initialisePanelTrajetDureeTemps();
                this.initialisePanelSUDureeTemps();

                this.initialiseGridAbonnement();

                btnModifierDureeTemps.Enabled = false;
                btnModifierNombreVoyage.Enabled = false;
                btnAjouterDureeTemps.Enabled = false;
                btnAjouterNombreVoyage.Enabled = false;
                btnNouveauNombreVoyage.Enabled = false;
                btnNouveauDevis.Enabled = false;
                btnNouveauDureeTemps.Enabled = false;
                btnValideANV.Enabled = false;
                btnValiderADT.Enabled = false;
                btnAnnulerDevis.Enabled = false;


                this.initialiseFormulaireAbonner();
                this.initialiseErrorMessageAbonner();

                this.initialiseGridProforma();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "202"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void divIndicationTextAV(string str, string color)
        {
            if (str != "" && color != "")
            {
                divIndicationAV.Style.Add("font-size", "14px");
                divIndicationAV.Style.Add("color", color);
                divIndicationAV.InnerHtml = "<p>" + str + "</p>";
            }
            else
            {
                divIndicationAV.InnerHtml = "";
            }
        }

        private void divIndicationTextAD(string str, string color)
        {
            if (str != "" && color != "")
            {
                divIndicationAD.Style.Add("font-size", "14px");
                divIndicationAD.Style.Add("color", color);
                divIndicationAD.InnerHtml = "<p>" + str + "</p>";
            }
            else
            {
                divIndicationAD.InnerHtml = "";
            }
        }

        private void initialiseGridAbonnement()
        {
            serviceAbonnement.insertToGridAbonnement(gvAbonnement, ddlTriAbonnement.SelectedValue, ddlTriAbonnement.SelectedValue, TextRechercheAbonnement.Text);
        }

        /*
        private void afficheAbonnement(string numAbonnement)
        {
            #region declaration
            crlAbonnement abonnement = null;
            #endregion

            #region implemenation
            abonnement = serviceAbonnement.selectAbonnement(numAbonnement);

            if (abonnement != null)
            {
                if (abonnement.organisme != null)
                {
                    LabAbonnement.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.organisme.NomOrganisme;
                }
                if (abonnement.societe != null)
                {
                    LabAbonnement.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.societe.NomSociete;
                }
                if (abonnement.client != null)
                {
                    LabAbonnement.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.client.NomClient + " " + abonnement.client.PrenomClient;
                }
                hfNumAbonnement.Value = abonnement.NumAbonnement;
            }
            #endregion
        }
         * */

        private void initialisePanelSUNombreVoyage()
        {
            //panel pour suburbaine et urbaine//

            #region implemùentation
            if (ddlZoneNombreVoyage.SelectedValue.Equals("Suburbaine") || ddlZoneNombreVoyage.SelectedValue.Equals("Urbaine"))
            {
                Panel_SUVoyageAbonnement.Visible = true;

                TextPrixUnitaireSU_RequiredFieldValidator.ValidationGroup = "gNombreVoyage";
            }
            else
            {
                Panel_SUVoyageAbonnement.Visible = false;

                TextPrixUnitaireSU_RequiredFieldValidator.ValidationGroup = "";
            }
            #endregion
        }

        private void initialisePanelTrajetNombreVoyage()
        {
            //panel pour les nationale et regionale//

            #region implementation
            if (ddlZoneNombreVoyage.SelectedValue.Equals("Nationale") || ddlZoneNombreVoyage.SelectedValue.Equals("Regionale"))
            {
                Panel_FormulaireTrajetRNVoyageAbonnement.Visible = true;

                TextPrixBilletUnitaire_RequiredFieldValidator.ValidationGroup = "gNombreVoyage";
                ddlCalculCategorieBillet_RequiredFieldValidator.ValidationGroup = "gNombreVoyage";
            }
            else
            {
                Panel_FormulaireTrajetRNVoyageAbonnement.Visible = false;

                TextPrixBilletUnitaire_RequiredFieldValidator.ValidationGroup = "";
                ddlCalculCategorieBillet_RequiredFieldValidator.ValidationGroup = "";
            }
            #endregion
        }

        private void initialisePanelSUDureeTemps()
        {
            //panel pour suburbaine et urbaine//

            #region implemùentation
            if (ddlZoneDureeTemps.SelectedValue.Equals("Suburbaine") || ddlZoneDureeTemps.SelectedValue.Equals("Urbaine"))
            {
                Panel_SUDureeTemps.Visible = true;

                TextPrixUnitaireSUDureeTemps_RequiredFieldValidator.ValidationGroup = "gDureeTemps";
            }
            else
            {
                Panel_SUDureeTemps.Visible = false;

                TextPrixUnitaireSUDureeTemps_RequiredFieldValidator.ValidationGroup = "";
            }
            #endregion
        }

        private void initialisePanelTrajetDureeTemps()
        {
            //panel pour les nationale et regionale//

            #region implementation
            if (ddlZoneDureeTemps.SelectedValue.Equals("Nationale") || ddlZoneDureeTemps.SelectedValue.Equals("Regionale"))
            {
                Panel_FormulaireTrajetRNDureeTemps.Visible = true;

                Text_PrixUnitaireDureeTemps_RequiredFieldValidator.ValidationGroup = "gDureeTemps";
                ddlCategorieDureeTemps_RequiredFieldValidator.ValidationGroup = "gDureeTemps";
            }
            else
            {
                Panel_FormulaireTrajetRNDureeTemps.Visible = false;

                Text_PrixUnitaireDureeTemps_RequiredFieldValidator.ValidationGroup = "";
                ddlCategorieDureeTemps_RequiredFieldValidator.ValidationGroup = "";
            }
            #endregion
        }

        private void initialiseFormulaireTrajetNombreVoyage()
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
                ddlCalculCategorieBillet.SelectedValue = "";
            }
            catch (Exception)
            {
            }

            //this.affichePrix();
        }

        private void initialiseFormulaireTrajetDureeTemps()
        {
            try
            {
                ddlDepartDureeTemps.SelectedValue = agent.agence.ville.NumVille;
            }
            catch (Exception)
            {
            }
            serviceVille.loadDdlVilleDestination(ddlDestinationDureeTemps, ddlDepartDureeTemps.SelectedValue);

            try
            {
                ddlCategorieDureeTemps.SelectedValue = "";
            }
            catch (Exception)
            {
            }

            //this.affichePrix();
        }

        private void affichePrixNombreVoyage()
        {
            #region declaration
            crlTrajet Trajet = null;
            crlCalculCategorieBillet CalculCategorieBillet = null;
            crlCalculReductionBillet CalculReductionBillet = null;
            double montantBillet = 0.00;
            #endregion

            #region implementation
            if (ddlZoneNombreVoyage.SelectedValue != "")
            {
                if (ddlVilleDepart.SelectedValue != "" && ddlDestination.SelectedValue != "" && ddlCalculCategorieBillet.SelectedValue != "")
                {
                    Trajet = serviceTrajet.selectTrajet(ddlVilleDepart.SelectedValue, ddlDestination.SelectedValue);
                    CalculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(ddlCalculCategorieBillet.SelectedValue);
                    CalculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBilletIndicateur("Abonnement");

                    if (Trajet != null && CalculCategorieBillet != null)
                    {
                        montantBillet = Trajet.tarifBaseBillet.MontantTarifBaseBillet * CalculCategorieBillet.PourcentagePrixBillet / 100;
                        montantBillet = montantBillet * CalculReductionBillet.PourcentagePrixBillet / 100;

                        TextPrixBilletUnitaire.Text = serviceGeneral.separateurDesMilles(montantBillet.ToString("0"));
                        hfNumTrajet.Value = Trajet.NumTrajet;

                        this.affichePrixTotalNombreVoyage();
                    }
                    else
                    {
                        //
                    }
                }
                else
                {
                    TextPrixBilletUnitaire.Text = "";
                    TextPrixTotalVA.Text = "";
                }
            }
            else
            {
                TextPrixBilletUnitaire.Text = "";
                TextPrixTotalVA.Text = "";
                this.initialiseFormulaireTrajetNombreVoyage();
            }
            #endregion
        }

        private void affichePrixTotalNombreVoyage()
        {
            #region declaration
            int nbVoyage = 0;
            double montantUnitaire = 0;
            #endregion

            #region implementation

            if (ddlZoneNombreVoyage.SelectedValue.Equals("Nationale") || ddlZoneNombreVoyage.SelectedValue.Equals("Regionale"))
            {
                if (TextPrixBilletUnitaire.Text != "" && TextNbVoyageVA.Text != "")
                {
                    try
                    {
                        nbVoyage = int.Parse(TextNbVoyageVA.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        montantUnitaire = double.Parse(TextPrixBilletUnitaire.Text.Replace(" ", ""));
                    }
                    catch (Exception)
                    {
                    }

                    TextPrixTotalVA.Text = serviceGeneral.separateurDesMilles((montantUnitaire * nbVoyage).ToString("0"));
                }
            }
            else if (ddlZoneNombreVoyage.SelectedValue.Equals("Suburbaine") || ddlZoneNombreVoyage.SelectedValue.Equals("Urbaine"))
            {
                if (TextPrixUnitaireSU.Text != "" && TextNbVoyageVA.Text != "")
                {
                    try
                    {
                        nbVoyage = int.Parse(TextNbVoyageVA.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        montantUnitaire = double.Parse(TextPrixUnitaireSU.Text.Replace(" ", ""));
                    }
                    catch (Exception)
                    {
                    }

                    TextPrixTotalVA.Text = serviceGeneral.separateurDesMilles((montantUnitaire * nbVoyage).ToString("0"));
                }
            }


            #endregion
        }

        private void affichePrixDureeTemps()
        {
            #region declaration
            crlTrajet Trajet = null;
            crlCalculCategorieBillet CalculCategorieBillet = null;
            crlCalculReductionBillet CalculReductionBillet = null;
            double montantBillet = 0.00;
            #endregion

            #region implementation
            if (ddlZoneDureeTemps.SelectedValue != "")
            {
                if (ddlDepartDureeTemps.SelectedValue != "" && ddlDestinationDureeTemps.SelectedValue != "" && ddlCategorieDureeTemps.SelectedValue != "")
                {
                    Trajet = serviceTrajet.selectTrajet(ddlDepartDureeTemps.SelectedValue, ddlDestinationDureeTemps.SelectedValue);
                    CalculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(ddlCategorieDureeTemps.SelectedValue);
                    CalculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBilletIndicateur("Abonnement");

                    if (Trajet != null && CalculCategorieBillet != null && CalculReductionBillet != null)
                    {
                        montantBillet = Trajet.tarifBaseBillet.MontantTarifBaseBillet * CalculCategorieBillet.PourcentagePrixBillet / 100;
                        montantBillet = montantBillet * CalculReductionBillet.PourcentagePrixBillet / 100;

                        Text_PrixUnitaireDureeTemps.Text = serviceGeneral.separateurDesMilles(montantBillet.ToString("0"));
                        hfNumTrajetDureeTemps.Value = Trajet.NumTrajet;

                    }
                    else
                    {
                        //
                    }
                }
                else
                {
                    Text_PrixUnitaireDureeTemps.Text = "";
                    TextPrixTotalDA.Text = "";
                }
            }
            else
            {
                Text_PrixUnitaireDureeTemps.Text = "";
                TextPrixTotalDA.Text = "";
                this.initialiseFormulaireTrajetDureeTemps();
            }
            #endregion
        }

        private void affichePrixTotalDureeTemps()
        {
            #region declaration
            double montantADT = 0;
            int nbPersonne = 0;
            #endregion

            #region implementation
            if (TextMontantADT.Text != "" && TextNbPersonne.Text != "")
            {
                try
                {
                    montantADT = double.Parse(TextMontantADT.Text.Replace(" ", ""));
                }
                catch (Exception)
                {
                }
                try
                {
                    nbPersonne = int.Parse(TextNbPersonne.Text);
                }
                catch (Exception)
                {
                }
                TextPrixTotalDA.Text = serviceGeneral.separateurDesMilles((nbPersonne * montantADT).ToString("0"));
            }
            #endregion
        }

        private void initialiseGridANV()
        {
            #region declaration
            Convertisseuse convertisseuse = new Convertisseuse();
            #endregion

            #region implementation
            serviceVoyageAbonnementDevis.insertToGridVoyageAbonnementDevis(gvANV, "voyageabonnementdevis.numVoyageAbonnementDevis", "voyageabonnementdevis.numVoyageAbonnementDevis", "", hfNumProforma.Value);
            LabelPrixTotalANV.Text = serviceGeneral.separateurDesMilles(serviceProforma.getPrixTotalVoyageAbonnementProforma(hfNumProforma.Value).ToString("0")) + "Ar";
            //LabelPrixTotalLettreANV.Text = convertisseuse.convertion(serviceProforma.getPrixTotalVoyageAbonnementProforma(hfNumProforma.Value).ToString("0")) + " Ariary";
            this.initialisePrixTotalDevis();
            #endregion
        }

        private void initialiseGridADT()
        {
            #region declaration
            Convertisseuse convertisseuse = new Convertisseuse();
            #endregion

            #region implementation
            serviceDureeAbonnementDevis.insertToGridDureeAbonnementDevis(gvADT, "dureeabonnementdevis.numDureeAbonnementDevis", "dureeabonnementdevis.numDureeAbonnementDevis", "", hfNumProforma.Value);
            LabelPrixTotaADT.Text = serviceGeneral.separateurDesMilles(serviceProforma.getPrixTotalDureeAbonnementProforma(hfNumProforma.Value).ToString("0")) + "Ar";
            //LabelPrixTotalLettreADT.Text = convertisseuse.convertion(serviceProforma.getPrixTotalDureeAbonnementProforma(hfNumProforma.Value).ToString("0")) + " Ariary";
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

            if (montantTotal > 0)
            {
                TextMontantProforma.Text = serviceGeneral.separateurDesMilles(montantTotal.ToString("0"));
            }
            else
            {
                TextMontantProforma.Text = "";
            }
            #endregion
        }

        private void initialiseFormulaireANV()
        {
            try
            {
                ddlZoneNombreVoyage.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            this.initialiseFormulaireTrajetNombreVoyage();
            this.affichePrixNombreVoyage();
            this.initialisePanelSUNombreVoyage();
            this.initialisePanelTrajetNombreVoyage();
            TextNbVoyageVA.Text = "";
            hfANV.Value = "";
            btnModifierNombreVoyage.Enabled = false;
            btnAjouterNombreVoyage.Enabled = true;
            btnNouveauNombreVoyage.Enabled = true;
            btnValideANV.Enabled = true;


        }

        private void initialiseFormulaireADT()
        {
            try
            {
                ddlZoneDureeTemps.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            this.initialiseFormulaireTrajetDureeTemps();
            this.affichePrixDureeTemps();
            this.initialisePanelSUDureeTemps();
            this.initialisePanelTrajetDureeTemps();
            TextDateAuDA.Text = "";
            TextDateDuDA.Text = "";
            TextNbPersonne.Text = "";
            TextMontantADT.Text = "";
            hfADT.Value = "";
            btnModifierDureeTemps.Enabled = false;
            btnAjouterDureeTemps.Enabled = true;
            btnNouveauDureeTemps.Enabled = true;
            btnValiderADT.Enabled = true;

        }

        private void insertToObjetANV(crlVoyageAbonnementDevis voyageAbonnementDevis)
        {
            #region implementation
            if (voyageAbonnementDevis != null)
            {
                if (ddlZoneNombreVoyage.SelectedValue.Equals("Nationale") || ddlZoneNombreVoyage.SelectedValue.Equals("Regionale"))
                {
                    voyageAbonnementDevis.NumTrajet = hfNumTrajet.Value;
                    try
                    {
                        voyageAbonnementDevis.PrixUnitaire = double.Parse(TextPrixBilletUnitaire.Text);
                    }
                    catch (Exception)
                    {
                    }
                    voyageAbonnementDevis.NumCalculCategorieBillet = ddlCalculCategorieBillet.SelectedValue;
                    voyageAbonnementDevis.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBilletIndicateur("Abonnement");
                    if (voyageAbonnementDevis.calculReductionBillet.NumCalculReductionBillet != "")
                    {
                        voyageAbonnementDevis.NumCalculReductionBillet = voyageAbonnementDevis.calculReductionBillet.NumCalculReductionBillet;
                    }
                }
                else if (ddlZoneNombreVoyage.SelectedValue.Equals("Suburbaine") || ddlZoneNombreVoyage.SelectedValue.Equals("Urbaine"))
                {
                    voyageAbonnementDevis.NumTrajet = "";
                    voyageAbonnementDevis.NumCalculCategorieBillet = "";
                    voyageAbonnementDevis.NumCalculReductionBillet = "";
                    try
                    {
                        voyageAbonnementDevis.PrixUnitaire = double.Parse(TextPrixUnitaireSU.Text);
                    }
                    catch (Exception)
                    {
                    }
                }

                try
                {
                    voyageAbonnementDevis.NbVoyageAbonnement = int.Parse(TextNbVoyageVA.Text);
                }
                catch (Exception)
                {
                }
                voyageAbonnementDevis.NumProforma = hfNumProforma.Value;

                voyageAbonnementDevis.Zone = ddlZoneNombreVoyage.SelectedValue;
                voyageAbonnementDevis.NumAbonnement = hfNumAbonnement.Value;
            }
            #endregion
        }

        private void insertToObjetADT(crlDureeAbonnementDevis dureeAbonnementDevis)
        {
            #region implementation
            if (dureeAbonnementDevis != null)
            {
                if (ddlZoneDureeTemps.SelectedValue.Equals("Nationale") || ddlZoneDureeTemps.SelectedValue.Equals("Regionale"))
                {
                    dureeAbonnementDevis.NumTrajet = hfNumTrajetDureeTemps.Value;
                    try
                    {
                        dureeAbonnementDevis.PrixUnitaire = double.Parse(Text_PrixUnitaireDureeTemps.Text);
                    }
                    catch (Exception)
                    {
                    }
                    dureeAbonnementDevis.NumCalculCategorieBillet = ddlCategorieDureeTemps.SelectedValue;
                    dureeAbonnementDevis.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBilletIndicateur("Abonnement");
                    if (dureeAbonnementDevis.calculReductionBillet.NumCalculReductionBillet != "")
                    {
                        dureeAbonnementDevis.NumCalculReductionBillet = dureeAbonnementDevis.calculReductionBillet.NumCalculReductionBillet;
                    }
                }
                else if (ddlZoneDureeTemps.SelectedValue.Equals("Suburbaine") || ddlZoneDureeTemps.SelectedValue.Equals("Urbaine"))
                {
                    dureeAbonnementDevis.NumTrajet = "";
                    dureeAbonnementDevis.NumCalculCategorieBillet = "";
                    dureeAbonnementDevis.NumCalculReductionBillet = "";
                    try
                    {
                        dureeAbonnementDevis.PrixUnitaire = double.Parse(TextPrixUnitaireSUDureeTemps.Text);
                    }
                    catch (Exception)
                    {
                    }
                }

                try
                {
                    dureeAbonnementDevis.NombreDureeAbonnement = int.Parse(TextNbPersonne.Text);
                }
                catch (Exception)
                {
                }
                dureeAbonnementDevis.NumProforma = hfNumProforma.Value;

                try
                {
                    dureeAbonnementDevis.PrixTotal = double.Parse(TextMontantADT.Text);
                }
                catch (Exception)
                {
                }

                try
                {
                    dureeAbonnementDevis.ValideAu = Convert.ToDateTime(TextDateAuDA.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    dureeAbonnementDevis.ValideDu = Convert.ToDateTime(TextDateDuDA.Text);
                }
                catch (Exception)
                {
                }
                dureeAbonnementDevis.Zone = ddlZoneDureeTemps.SelectedValue;
                dureeAbonnementDevis.NumAbonnement = hfNumAbonnement.Value;
            }
            #endregion
        }

        private void insertToObjetVoyageAbonnement(crlVoyageAbonnement voyageAbonnement)
        {
            #region implementation
            if (voyageAbonnement != null)
            {
                if (hfNumAbonnement.Value != "")
                {
                    if (ddlZoneNombreVoyage.SelectedValue.Equals("Nationale") || ddlZoneNombreVoyage.SelectedValue.Equals("Regionale"))
                    {
                        voyageAbonnement.NumTrajet = hfNumTrajet.Value;
                        try
                        {
                            voyageAbonnement.PrixUnitaire = double.Parse(TextPrixBilletUnitaire.Text);
                        }
                        catch (Exception)
                        {
                        }
                        voyageAbonnement.NumCalculCategorieBillet = ddlCalculCategorieBillet.SelectedValue;
                        voyageAbonnement.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBilletIndicateur("Abonnement");
                        if (voyageAbonnement.calculReductionBillet.NumCalculReductionBillet != "")
                        {
                            voyageAbonnement.NumCalculReductionBillet = voyageAbonnement.calculReductionBillet.NumCalculReductionBillet;
                        }
                    }
                    else if (ddlZoneNombreVoyage.SelectedValue.Equals("Suburbaine") || ddlZoneNombreVoyage.SelectedValue.Equals("Urbaine"))
                    {
                        voyageAbonnement.NumTrajet = "";
                        voyageAbonnement.NumCalculCategorieBillet = "";
                        voyageAbonnement.NumCalculReductionBillet = "";
                        try
                        {
                            voyageAbonnement.PrixUnitaire = double.Parse(TextPrixUnitaireSU.Text);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    try
                    {
                        voyageAbonnement.NbVoyageAbonnement = int.Parse(TextNbVoyageVA.Text);
                    }
                    catch (Exception)
                    {
                    }


                    voyageAbonnement.Zone = ddlZoneNombreVoyage.SelectedValue;
                    voyageAbonnement.MatriculeAgent = agent.matriculeAgent;
                    voyageAbonnement.agent = agent;
                    voyageAbonnement.NumAbonnement = hfNumAbonnement.Value;
                    voyageAbonnement.ModePaiement = "Espèce";
                }
                else
                {
                    //
                }
            }
            #endregion
        }

        private void insertToObjetDureeAbonnement(crlDureeAbonnement dureeAbonnement)
        {
            #region implementation
            if (dureeAbonnement != null)
            {
                if (hfNumAbonnement.Value != "")
                {
                    if (ddlZoneDureeTemps.SelectedValue.Equals("Nationale") || ddlZoneDureeTemps.SelectedValue.Equals("Regionale"))
                    {
                        dureeAbonnement.NumTrajet = hfNumTrajetDureeTemps.Value;
                        try
                        {
                            dureeAbonnement.PrixUnitaire = double.Parse(Text_PrixUnitaireDureeTemps.Text);
                        }
                        catch (Exception)
                        {
                        }
                        dureeAbonnement.NumCalculCategorieBillet = ddlCategorieDureeTemps.SelectedValue;
                        dureeAbonnement.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBilletIndicateur("Abonnement");
                        if (dureeAbonnement.calculReductionBillet.NumCalculReductionBillet != "")
                        {
                            dureeAbonnement.NumCalculReductionBillet = dureeAbonnement.calculReductionBillet.NumCalculReductionBillet;
                        }
                    }
                    else if (ddlZoneDureeTemps.SelectedValue.Equals("Suburbaine") || ddlZoneDureeTemps.SelectedValue.Equals("Urbaine"))
                    {
                        dureeAbonnement.NumTrajet = "";
                        dureeAbonnement.NumCalculCategorieBillet = "";
                        dureeAbonnement.NumCalculReductionBillet = "";
                        try
                        {
                            dureeAbonnement.PrixUnitaire = double.Parse(TextPrixUnitaireSUDureeTemps.Text);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    try
                    {
                        dureeAbonnement.PrixTotal = double.Parse(TextMontantADT.Text);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        dureeAbonnement.ValideAu = Convert.ToDateTime(TextDateAuDA.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        dureeAbonnement.ValideDu = Convert.ToDateTime(TextDateDuDA.Text);
                    }
                    catch (Exception)
                    {
                    }
                    dureeAbonnement.Zone = ddlZoneDureeTemps.SelectedValue;
                    dureeAbonnement.agent = agent;
                    dureeAbonnement.MatriculeAgent = agent.matriculeAgent;
                    dureeAbonnement.NumAbonnement = hfNumAbonnement.Value;
                    dureeAbonnement.ModePaiement = "Espèce";
                }
                else
                {
                    //
                }
            }
            #endregion
        }

        private void insertToObjetProforma(crlProforma proforma)
        {
            #region declaration
            crlAbonnement abonnement = null;
            #endregion

            #region implementation
            if (proforma != null)
            {
                proforma.MatriculeAgent = agent.matriculeAgent;
                proforma.agent = agent;

                abonnement = serviceAbonnement.selectAbonnement(hfNumAbonnement.Value);
                if (abonnement != null)
                {
                    if (abonnement.NumIndividu != "")
                    {
                        /*****mila jerena ny proforma**********/
                        proforma.NumIndividu = abonnement.NumIndividu;
                    }
                    if (abonnement.NumOrganisme != "")
                    {
                        proforma.NumOrganisme = abonnement.NumOrganisme;
                    }
                    if (abonnement.NumSociete != "")
                    {
                        proforma.NumSociete = abonnement.NumSociete;
                    }
                }
            }
            #endregion
        }

        private void afficheANV(string numVoyageAbonnementDevis)
        {
            #region declaration
            crlVoyageAbonnementDevis voyageAbonnementDevis = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numVoyageAbonnementDevis != "")
            {
                voyageAbonnementDevis = serviceVoyageAbonnementDevis.selectVoyageAbonnementDevis(numVoyageAbonnementDevis);

                if (voyageAbonnementDevis != null)
                {
                    hfANV.Value = voyageAbonnementDevis.NumVoyageAbonnementDevis;
                    try
                    {
                        ddlZoneNombreVoyage.SelectedValue = voyageAbonnementDevis.Zone;
                    }
                    catch (Exception)
                    {
                    }
                    TextNbVoyageVA.Text = voyageAbonnementDevis.NbVoyageAbonnement.ToString("0");

                    this.initialisePanelSUNombreVoyage();
                    this.initialisePanelTrajetNombreVoyage();

                    if (voyageAbonnementDevis.Zone.Equals("Nationale") || voyageAbonnementDevis.Zone.Equals("Regionale"))
                    {
                        ddlVilleDepart.SelectedValue = voyageAbonnementDevis.trajet.villeD.NumVille;
                        serviceVille.loadDdlVilleDestination(ddlDestination, ddlVilleDepart.SelectedValue);
                        ddlDestination.SelectedValue = voyageAbonnementDevis.trajet.villeF.NumVille;
                        ddlCalculCategorieBillet.SelectedValue = voyageAbonnementDevis.NumCalculCategorieBillet;
                        this.affichePrixNombreVoyage();
                        this.affichePrixTotalNombreVoyage();
                    }
                    else if (voyageAbonnementDevis.Zone.Equals("Suburbaine") || voyageAbonnementDevis.Zone.Equals("Urbaine"))
                    {
                        TextPrixUnitaireSU.Text = voyageAbonnementDevis.PrixUnitaire.ToString("0");
                        this.affichePrixNombreVoyage();
                        this.affichePrixTotalNombreVoyage();
                    }

                    strConfirm = "Voulez vous vraiment modifier l'abonnement N°" + voyageAbonnementDevis.NumVoyageAbonnementDevis + "?\n";
                    strConfirm += "Montant total:" + serviceGeneral.separateurDesMilles((voyageAbonnementDevis.NbVoyageAbonnement * voyageAbonnementDevis.PrixUnitaire).ToString()) + "Ar";

                    ConfirmButtonExtender_btnModifierNombreVoyage.ConfirmText = strConfirm;

                    btnModifierNombreVoyage.Enabled = true;
                    btnAjouterNombreVoyage.Enabled = false;
                    btnValideANV.Enabled = false;
                }
            }
            #endregion
        }

        private void afficheADT(string numDureeAbonnementDevis)
        {
            #region declaration
            crlDureeAbonnementDevis dureeAbonnementDevis = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numDureeAbonnementDevis != "")
            {
                dureeAbonnementDevis = serviceDureeAbonnementDevis.selectDureeAbonnementDevis(numDureeAbonnementDevis);

                if (dureeAbonnementDevis != null)
                {
                    hfADT.Value = dureeAbonnementDevis.NumDureeAbonnementDevis;
                    try
                    {
                        ddlZoneDureeTemps.SelectedValue = dureeAbonnementDevis.Zone;
                    }
                    catch (Exception)
                    {
                    }
                    TextDateDuDA.Text = dureeAbonnementDevis.ValideDu.ToString("dd MMMM yyyy");
                    TextDateAuDA.Text = dureeAbonnementDevis.ValideAu.ToString("dd MMMM yyyy");
                    TextNbPersonne.Text = dureeAbonnementDevis.NombreDureeAbonnement.ToString("0");
                    TextMontantADT.Text = dureeAbonnementDevis.PrixTotal.ToString("0");

                    this.affichePrixTotalDureeTemps();
                    this.initialisePanelSUDureeTemps();
                    this.initialisePanelTrajetDureeTemps();

                    if (dureeAbonnementDevis.Zone.Equals("Nationale") || dureeAbonnementDevis.Zone.Equals("Regionale"))
                    {
                        ddlDepartDureeTemps.SelectedValue = dureeAbonnementDevis.trajet.villeD.NumVille;
                        serviceVille.loadDdlVilleDestination(ddlDestinationDureeTemps, ddlDepartDureeTemps.SelectedValue);
                        ddlDestinationDureeTemps.SelectedValue = dureeAbonnementDevis.trajet.villeF.NumVille;
                        ddlCategorieDureeTemps.SelectedValue = dureeAbonnementDevis.NumCalculCategorieBillet;
                        this.affichePrixDureeTemps();
                    }
                    else if (dureeAbonnementDevis.Zone.Equals("Suburbaine") || dureeAbonnementDevis.Zone.Equals("Urbaine"))
                    {
                        TextPrixUnitaireSUDureeTemps.Text = dureeAbonnementDevis.PrixUnitaire.ToString("0");
                        this.affichePrixDureeTemps();
                    }

                    strConfirm = "Voulez vous vraiment modifier l'abonnement N°" + dureeAbonnementDevis.NumDureeAbonnementDevis + "?\n";
                    strConfirm += "Montant total:" + serviceGeneral.separateurDesMilles((dureeAbonnementDevis.NombreDureeAbonnement * dureeAbonnementDevis.PrixTotal).ToString()) + "Ar";

                    ConfirmButtonExtender_btnModifierDureeTemps.ConfirmText = strConfirm;

                    btnModifierDureeTemps.Enabled = true;
                    btnAjouterDureeTemps.Enabled = false;
                    btnValiderADT.Enabled = false;
                }
            }
            #endregion
        }

        private void initialiseErrorMessageAbonner()
        {
            TextAdresseClient_RequiredFieldValidator.ErrorMessage = ReAbonnement.adresseClientNonVide;
            TextCinClient_RequiredFieldValidator.ErrorMessage = ReAbonnement.cinClientNonVide;
            TextNomClient_RequiredFieldValidator.ErrorMessage = ReAbonnement.nomClientNonVide;

            TextAdresseRespSociete_RequiredFieldValidator.ErrorMessage = ReAbonnement.adresseRespSocieteNonVide;
            TextAdresseSociete_RequiredFieldValidator.ErrorMessage = ReAbonnement.adresseSocieteNonVide;
            TextCinRespSociete_RequiredFieldValidator.ErrorMessage = ReAbonnement.cinRespSocieteNonVide;
            TextNomResponsableSociete_RequiredFieldValidator.ErrorMessage = ReAbonnement.nomRespSocieteNonVide;
            TextNomSociete_RequiredFieldValidator.ErrorMessage = ReAbonnement.nomSocieteNonVide;
            TextSecteurSociete_RequiredFieldValidator.ErrorMessage = ReAbonnement.secteurSocieteNonVide;
            TextMailRespSociete_RegularExpressionValidator.ErrorMessage = ReAbonnement.mailResSocieteNonValide;
            TextMailSociete_RegularExpressionValidator.ErrorMessage = ReAbonnement.mailSocieteNonValide;

            TextAdresseOrganisme_RequiredFieldValidator.ErrorMessage = ReAbonnement.adresseOrganismeNonVide;
            TextAdresseRespOrganisme_RequiredFieldValidator.ErrorMessage = ReAbonnement.adresseRespOrganisme;
            TextCinRespOrganisme_RequiredFieldValidator.ErrorMessage = ReAbonnement.cinRespOrganisme;
            TextNomOrganisme_RequiredFieldValidator.ErrorMessage = ReAbonnement.nomOrganismeNonVide;
            TextNomRespOrganisme_RequiredFieldValidator.ErrorMessage = ReAbonnement.nomRespOrganisme;
            TextMailOrganisme_RegularExpressionValidator.ErrorMessage = ReAbonnement.mailOrganismeNonValide;
            TextMailRespOrganisme_RegularExpressionValidator.ErrorMessage = ReAbonnement.mailRespOrganismeNonValide;

            ddlZoneDureeTemps_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.zoneNonVide;
            ddlCategorieDureeTemps_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.categorieNonVide;
            Text_PrixUnitaireDureeTemps_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.prixUnitaireNR;
            TextPrixUnitaireSUDureeTemps_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.prixUnitaireSU;
            TextDateDuDA_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.dateDuNonVide;
            TextDateAuDA_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.dateAuNonVide;
            TextNbPersonne_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.nombrePersonneNonVide;
            TextPrixTotalDA_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.prixTotalNonVide;

            ddlZoneNombreVoyage_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.zoneNonVide;
            ddlCalculCategorieBillet_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.categorieNonVide;
            TextPrixBilletUnitaire_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.prixUnitaireNR;
            TextPrixUnitaireSU_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.prixUnitaireSU;
            TextNbVoyageVA_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.nombreVoyageNonVide;
            TextPrixTotalVA_RequiredFieldValidator.ErrorMessage = ReDevisAbonnement.prixTotalNonVide;
        }

        private void initialiseSociete()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("societe"))
            {
                PanelSociete.Visible = true;
                TextAdresseRespSociete_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextAdresseSociete_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextCinRespSociete_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextNomResponsableSociete_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextNomSociete_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextSecteurSociete_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextMailRespSociete_RegularExpressionValidator.ValidationGroup = "groupAbonnement";
                TextMailSociete_RegularExpressionValidator.ValidationGroup = "groupAbonnement";
            }
            else
            {
                PanelSociete.Visible = false;
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
                TextAdresseOrganisme_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextAdresseRespOrganisme_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextCinRespOrganisme_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextNomOrganisme_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextNomRespOrganisme_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextMailRespOrganisme_RegularExpressionValidator.ValidationGroup = "groupAbonnement";
                TextMailOrganisme_RegularExpressionValidator.ValidationGroup = "groupAbonnement";
            }
            else
            {
                PanelOrganisme.Visible = false;
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

                TextNomClient_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextCinClient_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
                TextAdresseClient_RequiredFieldValidator.ValidationGroup = "groupAbonnement";
            }
            else
            {
                PanelClient.Visible = false;

                TextNomClient_RequiredFieldValidator.ValidationGroup = "";
                TextCinClient_RequiredFieldValidator.ValidationGroup = "";
                TextAdresseClient_RequiredFieldValidator.ValidationGroup = "";
            }
        }

        private void initialiseFormulaireAbonner()
        {
            TextAdresseClient.Text = "";
            TextAdresseOrganisme.Text = "";
            TextAdresseRespOrganisme.Text = "";
            TextAdresseRespSociete.Text = "";
            TextAdresseSociete.Text = "";
            TextCinClient.Text = "";
            TextCinRespOrganisme.Text = "";
            TextCinRespSociete.Text = "";
            TextFixeOrganisme.Text = "";
            TextFixeRespOrganisme.Text = "";
            TextFixeRespSociete.Text = "";
            TextMailOrganisme.Text = "";
            TextMailRespOrganisme.Text = "";
            TextMailRespSociete.Text = "";
            TextMailSociete.Text = "";
            TextMobileOrganisme.Text = "";
            TextMobileRespOrganisme.Text = "";
            TextMobileRespSociete.Text = "";
            TextMobileSociete.Text = "";
            TextNomClient.Text = "";
            TextNomOrganisme.Text = "";
            TextNomResponsableSociete.Text = "";
            TextNomRespOrganisme.Text = "";
            TextNomSociete.Text = "";
            TextPrenom.Text = "";
            TextPrenomRespOrganisme.Text = "";
            TextPrenomRespSociete.Text = "";
            TextSecteurSociete.Text = "";
            TextTelephoneFixeClient.Text = "";
            TextTelephoneMobile.Text = "";
            TextTelephoneSociete.Text = "";
            try
            {
                RadioListeAbonnement.SelectedValue = "individu";
            }
            catch (Exception)
            {
            }

            this.initialiseIndividu();
            this.initialiseOrganisme();
            this.initialiseSociete();

        }

        private void afficheAbonnement(string numAbonnement)
        {
            #region declaration
            crlAbonnement abonnement = null;
            #endregion

            #region implemenation
            abonnement = serviceAbonnement.selectAbonnement(numAbonnement);

            if (abonnement != null)
            {


                hfNumAbonnementTemp.Value = abonnement.NumAbonnement;
                if (abonnement.organisme != null)
                {
                    RadioListeAbonnement.SelectedValue = "organisme";

                    TextAdresseOrganisme.Text = abonnement.organisme.AdresseOrganisme;
                    //TextAdresseRespOrganisme.Text = abonnement.organisme.AdresseResponsable;
                    //TextCinRespOrganisme.Text = abonnement.organisme.CinResponsable;
                    TextFixeOrganisme.Text = abonnement.organisme.TelephoneFixeOrganisme;
                    //TextFixeRespOrganisme.Text = abonnement.organisme.TelephoneFixeResponsable;
                    TextMailOrganisme.Text = abonnement.organisme.MailOrganisme;
                    //TextMailRespOrganisme.Text = abonnement.organisme.MailResponsable;
                    TextMobileOrganisme.Text = abonnement.organisme.TelephoneMobileOrganisme;
                    //TextMobileRespOrganisme.Text = abonnement.organisme.TelephoneMobileResponsable;
                    TextNomOrganisme.Text = abonnement.organisme.NomOrganisme;
                    //TextNomRespOrganisme.Text = abonnement.organisme.NomResponsable;
                    //TextPrenomRespOrganisme.Text = abonnement.organisme.PrenomResponsable;


                }
                else if (abonnement.societe != null)
                {
                    RadioListeAbonnement.SelectedValue = "societe";

                    //TextAdresseRespSociete.Text = abonnement.societe.AdresseResponsable;
                    TextAdresseSociete.Text = abonnement.societe.AdresseSociete;
                    //TextCinRespSociete.Text = abonnement.societe.CinResponsable;
                    //TextFixeRespSociete.Text = abonnement.societe.TelephoneFixeResponsable;
                    //TextMailRespSociete.Text = abonnement.societe.MailResponsable;
                    TextMailSociete.Text = abonnement.societe.MailSociete;
                    //TextMobileRespSociete.Text = abonnement.societe.TelephoneMobileResponsable;
                    TextMobileSociete.Text = abonnement.societe.TelephoneMobileSociete;
                    //TextNomResponsableSociete.Text = abonnement.societe.NomResponsable;
                    TextNomSociete.Text = abonnement.societe.NomSociete;
                    //TextPrenomRespSociete.Text = abonnement.societe.PrenomResponsable;
                    TextSecteurSociete.Text = abonnement.societe.SecteurActiviteSociete;
                    TextTelephoneSociete.Text = abonnement.societe.TelephoneFixeSociete;

                }
                else if (abonnement.individu != null)
                {
                    RadioListeAbonnement.SelectedValue = "individu";

                    TextAdresseClient.Text = abonnement.individu.Adresse;
                    TextCinClient.Text = abonnement.individu.CinIndividu;
                    TextNomClient.Text = abonnement.individu.NomIndividu;
                    TextPrenom.Text = abonnement.individu.PrenomIndividu;
                    TextTelephoneFixeClient.Text = abonnement.individu.TelephoneFixeIndividu;
                    TextTelephoneMobile.Text = abonnement.individu.TelephoneMobileIndividu;

                }


                this.initialiseSociete();
                this.initialiseOrganisme();
                this.initialiseIndividu();

                RadioListeAbonnement.Enabled = false;


            }
            #endregion
        }

        private void initialiseGridProforma()
        {
            serviceProforma.insertToGridProformaADTANV(gvProforma, ddlTriProforma.SelectedValue, ddlTriProforma.SelectedValue, TextRechercheProforma.Text);
        }

        private void afficheProforma(string numProforma)
        {
            #region declaration
            crlProforma proforma = null;
            crlAbonnement abonnement = null;
            #endregion

            #region implementation
            if (numProforma != "")
            {
                proforma = serviceProforma.selectProforma(numProforma);
                if (proforma != null)
                {
                    hfNumProforma.Value = numProforma;
                    this.initialiseGridProforma();
                    this.initialiseGridADT();
                    this.initialiseGridANV();
                    this.initialiseFormulaireADT();
                    this.initialiseFormulaireANV();
                    btnNouveauDevis.Enabled = true;
                    btnAnnulerDevis.Enabled = true;
                    if (proforma.NumIndividu != "")
                    {
                        abonnement = serviceAbonnement.selectAbonnement(proforma.NumIndividu);
                    }
                    else if (proforma.NumOrganisme != "")
                    {
                        abonnement = serviceAbonnement.selectAbonnement(proforma.NumOrganisme);
                    }
                    else if (proforma.NumSociete != "")
                    {
                        abonnement = serviceAbonnement.selectAbonnement(proforma.NumSociete);
                    }
                    if (abonnement != null)
                    {
                        hfNumAbonnement.Value = abonnement.NumAbonnement;
                        if (abonnement.organisme != null)
                        {
                            btnAbonner.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.organisme.NomOrganisme;
                        }
                        if (abonnement.societe != null)
                        {
                            btnAbonner.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.societe.NomSociete;
                        }
                        if (abonnement.individu != null)
                        {
                            btnAbonner.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.individu.NomIndividu + " " + abonnement.individu.PrenomIndividu;
                        }
                    }

                    if (proforma.societe == null)
                    {
                        Panel_Societe.Visible = false;

                        if (proforma.organisme == null)
                        {
                            Panel_Organisme.Visible = false;

                            if (proforma.individu == null)
                            {

                                TextMontantProforma.Text = "";

                                Panel_Individu.Visible = false;
                            }
                            else
                            {


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

                        //LabelAdresseRespSociete.Text = proforma.societe.AdresseResponsable;
                        LabelAdresseSociete.Text = proforma.societe.AdresseSociete;
                        //LabelCINRespSociete.Text = proforma.societe.CinResponsable;
                        //LabelFixeRespSociete.Text = proforma.societe.TelephoneFixeResponsable;
                        LabelFixeSociete.Text = proforma.societe.TelephoneFixeSociete;
                        LabelMailSociete.Text = proforma.societe.MailSociete;
                        //LabelMobileRespSociete.Text = proforma.societe.TelephoneMobileResponsable;
                        LabelMobileSociete.Text = proforma.societe.TelephoneMobileSociete;
                        //LabelNomRespSociete.Text = proforma.societe.NomResponsable;
                        LabelNomSociete.Text = proforma.societe.NomSociete;
                        //LabelPrenomRespSociete.Text = proforma.societe.PrenomResponsable;
                        LabelSecteurActiviteSociete.Text = proforma.societe.SecteurActiviteSociete;


                        Panel_Societe.Visible = true;
                    }
                }
            }
            #endregion
        }

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

        #endregion

        #region event
        protected void ddlTriAbonnement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAbonnement();
        }
        protected void btnRechercherAbonnement_Click(object sender, EventArgs e)
        {
            this.initialiseGridAbonnement();
        }
        protected void gvAbonnement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAbonnement.PageIndex = e.NewPageIndex;
            this.initialiseGridAbonnement();
        }
        protected void gvAbonnement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheAbonnement(e.CommandArgument.ToString());
            }
        }

        protected void ddlZoneNombreVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialisePanelSUNombreVoyage();
            this.initialisePanelTrajetNombreVoyage();

            this.affichePrixNombreVoyage();
        }

        protected void ddlZoneDureeTemps_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialisePanelTrajetDureeTemps();
            this.initialisePanelSUDureeTemps();

            this.affichePrixDureeTemps();
        }
        #endregion

        protected void ddlVilleDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceVille.loadDdlVilleDestination(ddlDestination, ddlVilleDepart.SelectedValue);
            this.affichePrixNombreVoyage();
        }
        protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrixNombreVoyage();
        }
        protected void ddlCalculCategorieBillet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrixNombreVoyage();
        }
        protected void TextNbVoyageVA_TextChanged(object sender, EventArgs e)
        {
            this.affichePrixNombreVoyage();
            this.affichePrixTotalNombreVoyage();
        }
        protected void TextPrixUnitaireSU_TextChanged(object sender, EventArgs e)
        {
            this.affichePrixNombreVoyage();
        }

        protected void ddlDepartDureeTemps_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceVille.loadDdlVilleDestination(ddlDestinationDureeTemps, ddlDepartDureeTemps.SelectedValue);
            this.affichePrixDureeTemps();
        }
        protected void ddlDestinationDureeTemps_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrixDureeTemps();
        }
        protected void ddlCategorieDureeTemps_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.affichePrixDureeTemps();
        }
        protected void TextNbPersonne_TextChanged(object sender, EventArgs e)
        {
            this.affichePrixTotalDureeTemps();
        }
        protected void TextMontantADT_TextChanged(object sender, EventArgs e)
        {
            this.affichePrixTotalDureeTemps();
        }

        protected void btnAjouterNombreVoyage_Click(object sender, EventArgs e)
        {
            #region declaration
            crlVoyageAbonnementDevis voyageAbonnementDevis = null;
            crlProforma proforma = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumAbonnement.Value != "")
            {
                if (hfNumProforma.Value != "")
                {
                    voyageAbonnementDevis = new crlVoyageAbonnementDevis();
                    this.insertToObjetANV(voyageAbonnementDevis);
                    voyageAbonnementDevis.NumVoyageAbonnementDevis = serviceVoyageAbonnementDevis.insertVoyageAbonnementDevis(voyageAbonnementDevis, agent.agence.SigleAgence);

                    if (voyageAbonnementDevis.NumVoyageAbonnementDevis != "")
                    {
                        this.initialiseGridANV();

                        strIndication = "Abonnement par nombre de voyage bien enregistrer!<br/>";
                        strIndication += "Montant:" + serviceGeneral.separateurDesMilles((voyageAbonnementDevis.PrixUnitaire * voyageAbonnementDevis.NbVoyageAbonnement).ToString("0")) + "Ar";
                        this.divIndicationTextAV(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationTextAV(strIndication, "Red");
                    }
                    this.afficheProforma(hfNumProforma.Value);
                }
                else
                {
                    proforma = new crlProforma();
                    this.insertToObjetProforma(proforma);

                    proforma.NumProforma = serviceProforma.insertProforma(proforma);
                    if (proforma.NumProforma != "")
                    {
                        hfNumProforma.Value = proforma.NumProforma;

                        voyageAbonnementDevis = new crlVoyageAbonnementDevis();
                        this.insertToObjetANV(voyageAbonnementDevis);
                        voyageAbonnementDevis.NumVoyageAbonnementDevis = serviceVoyageAbonnementDevis.insertVoyageAbonnementDevis(voyageAbonnementDevis, agent.agence.SigleAgence);

                        if (voyageAbonnementDevis.NumVoyageAbonnementDevis != "")
                        {
                            this.initialiseGridANV();

                            strIndication = "Abonnement par nombre de voyage bien enregistrer!<br/>";
                            strIndication += "Montant:" + serviceGeneral.separateurDesMilles((voyageAbonnementDevis.PrixUnitaire * voyageAbonnementDevis.NbVoyageAbonnement).ToString("0")) + "Ar";
                            this.divIndicationTextAV(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                            this.divIndicationTextAV(strIndication, "Red");
                        }
                        this.afficheProforma(proforma.NumProforma);
                    }
                    btnNouveauDevis.Enabled = true;
                    btnAnnulerDevis.Enabled = true;
                }
            }
            else
            {
                //
            }
            this.initialiseGridProforma();
            #endregion
        }
        protected void btnNouveauNombreVoyage_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireANV();
            this.divIndicationTextAV("", "Red");
        }
        protected void btnModifierNombreVoyage_Click(object sender, EventArgs e)
        {
            #region declaration
            crlVoyageAbonnementDevis voyageAbonnementDevis = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumAbonnement.Value != "")
            {
                if (hfANV.Value != "")
                {
                    voyageAbonnementDevis = serviceVoyageAbonnementDevis.selectVoyageAbonnementDevis(hfANV.Value);
                    if (voyageAbonnementDevis != null)
                    {
                        this.insertToObjetANV(voyageAbonnementDevis);

                        if (serviceVoyageAbonnementDevis.updateVoyageAbonnementDevis(voyageAbonnementDevis))
                        {
                            strIndication = "Modification abonnement par nombre de voyage bien enregistrer!<br/>";
                            strIndication += "Montant:" + serviceGeneral.separateurDesMilles((voyageAbonnementDevis.PrixUnitaire * voyageAbonnementDevis.NbVoyageAbonnement).ToString()) + "Ar";
                            this.divIndicationTextAV(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ca produit durant l'enregistrement!";
                            this.divIndicationTextAV(strIndication, "Red");
                        }
                        this.initialiseGridANV();
                        this.initialiseFormulaireANV();
                    }
                }
            }
            else
            {
                //
            }
            #endregion
        }
        protected void btnValideANV_Click(object sender, EventArgs e)
        {
            #region declaration
            crlVoyageAbonnement voyageAbonnement = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (agent.sessionCaisse != null)
            {
                if (hfNumAbonnement.Value != "")
                {
                    voyageAbonnement = new crlVoyageAbonnement();
                    this.insertToObjetVoyageAbonnement(voyageAbonnement);

                    voyageAbonnement.NumVoyageAbonnement = serviceVoyageAbonnement.insertVoyageAbonnement(voyageAbonnement);

                    if (voyageAbonnement.NumVoyageAbonnement != "")
                    {
                        serviceSessionCaisse.insertAssocSessionCaisseVoyageAbonnement(voyageAbonnement.NumVoyageAbonnement, agent.sessionCaisse.NumSessionCaisse);
                        strIndication = "Abonnement par nombre de voyage bien enregistrer!<br/>";
                        strIndication += "Montant:" + (voyageAbonnement.PrixUnitaire * voyageAbonnement.NbVoyageAbonnement) + "Ar";
                        this.divIndicationTextAV(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationTextAV(strIndication, "Red");
                    }
                }
                else
                {
                    //
                }
            }
            else
            {
                strIndication = "Votre session est encore inactive!<br/>";
                strIndication += "Veuillez contacter votre responsable";
                this.divIndicationTextAV(strIndication, "Red");
            }
            #endregion
        }

        protected void btnAjouterDureeTemps_Click(object sender, EventArgs e)
        {
            #region declaration
            crlDureeAbonnementDevis dureeAbonnementDevis = null;
            crlProforma proforma = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumAbonnement.Value != "")
            {
                if (hfNumProforma.Value != "")
                {
                    dureeAbonnementDevis = new crlDureeAbonnementDevis();
                    this.insertToObjetADT(dureeAbonnementDevis);
                    dureeAbonnementDevis.NumDureeAbonnementDevis = serviceDureeAbonnementDevis.insertDureeAbonnementDevis(dureeAbonnementDevis, agent.agence.SigleAgence);

                    if (dureeAbonnementDevis.NumDureeAbonnementDevis != "")
                    {
                        this.initialiseGridADT();

                        strIndication = "Abonnement par durée de temps bien enregistrer!<br/>";
                        strIndication += "Montant:" + serviceGeneral.separateurDesMilles((dureeAbonnementDevis.PrixTotal).ToString("0")) + "Ar";
                        this.divIndicationTextAD(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                        this.divIndicationTextAD(strIndication, "Red");
                    }
                }
                else
                {
                    proforma = new crlProforma();
                    this.insertToObjetProforma(proforma);

                    proforma.NumProforma = serviceProforma.insertProforma(proforma);
                    if (proforma.NumProforma != "")
                    {
                        hfNumProforma.Value = proforma.NumProforma;
                        dureeAbonnementDevis = new crlDureeAbonnementDevis();
                        this.insertToObjetADT(dureeAbonnementDevis);
                        dureeAbonnementDevis.NumDureeAbonnementDevis = serviceDureeAbonnementDevis.insertDureeAbonnementDevis(dureeAbonnementDevis, agent.agence.SigleAgence);

                        if (dureeAbonnementDevis.NumDureeAbonnementDevis != "")
                        {
                            this.initialiseGridADT();

                            strIndication = "Abonnement par durée de temps bien enregistrer!<br/>";
                            strIndication += "Montant:" + serviceGeneral.separateurDesMilles((dureeAbonnementDevis.PrixTotal).ToString("0")) + "Ar";
                            this.divIndicationTextAD(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                            this.divIndicationTextAD(strIndication, "Red");
                        }
                    }
                    btnNouveauDevis.Enabled = true;
                    btnAnnulerDevis.Enabled = true;
                }
            }
            else
            {
                //
            }
            this.initialiseGridProforma();
            #endregion
        }
        protected void btnNouveauDureeTemps_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireADT();
            this.divIndicationTextAD("", "Red");
        }
        protected void btnModifierDureeTemps_Click(object sender, EventArgs e)
        {
            #region declaration
            crlDureeAbonnementDevis dureeAbonnementDevis = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumAbonnement.Value != "")
            {
                if (hfADT.Value != "")
                {
                    dureeAbonnementDevis = serviceDureeAbonnementDevis.selectDureeAbonnementDevis(hfADT.Value);
                    if (dureeAbonnementDevis != null)
                    {
                        this.insertToObjetADT(dureeAbonnementDevis);

                        if (serviceDureeAbonnementDevis.updateDureeAbonnementDevis(dureeAbonnementDevis))
                        {
                            strIndication = "Modification abonnement par durée de temps bien enregistrer!<br/>";
                            strIndication += "Montant:" + serviceGeneral.separateurDesMilles((dureeAbonnementDevis.PrixTotal).ToString("0")) + "Ar";
                            this.divIndicationTextAD(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ca produit durant l'enregistrement!";
                            this.divIndicationTextAD(strIndication, "Red");
                        }
                        this.initialiseFormulaireADT();
                        this.initialiseGridADT();


                    }
                }
            }
            else
            {
                //
            }
            #endregion
        }
        protected void btnValiderADT_Click(object sender, EventArgs e)
        {
            #region declaration
            crlDureeAbonnement dureeAbonnement = null;
            int nombrePersonne = 0;
            int nombreInsert = 0;
            string strIndication = "";
            #endregion

            #region implementation
            if (agent.sessionCaisse != null)
            {
                if (hfNumAbonnement.Value != "")
                {
                    try
                    {
                        nombrePersonne = int.Parse(TextNbPersonne.Text);
                    }
                    catch (Exception)
                    {
                    }

                    if (nombrePersonne > 0)
                    {
                        for (int i = 0; i < nombrePersonne; i++)
                        {
                            dureeAbonnement = new crlDureeAbonnement();
                            this.insertToObjetDureeAbonnement(dureeAbonnement);

                            dureeAbonnement.NumDureeAbonnement = serviceDureeAbonnement.insertDureeAbonnement(dureeAbonnement);

                            if (dureeAbonnement.NumDureeAbonnement != "")
                            {
                                nombreInsert++;
                                serviceSessionCaisse.insertAssocSessionCaisseDureeAbonnement(dureeAbonnement.NumDureeAbonnement, agent.sessionCaisse.NumSessionCaisse);

                                strIndication = "Abonnement par durée de temps bien enregistrer!<br/>";
                                strIndication += "Montant:" + serviceGeneral.separateurDesMilles((dureeAbonnement.PrixTotal).ToString("0")) + "Ar";
                                this.divIndicationTextAD(strIndication, "Black");
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!<br/>";
                                this.divIndicationTextAD(strIndication, "Red");
                            }
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
                strIndication = "Votre session est encore inactive!<br/>";
                strIndication += "Veuillez contacter votre responsable";
                this.divIndicationTextAD(strIndication, "Red");
            }
            #endregion
        }

        protected void gvADT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvADT.PageIndex = e.NewPageIndex;
            this.initialiseGridADT();
        }
        protected void gvADT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheADT(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                serviceGeneral.delete("dureeabonnementdevis", "numDureeAbonnementDevis", e.CommandArgument.ToString());
                this.initialiseGridADT();
            }
        }

        protected void gvANV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvANV.PageIndex = e.NewPageIndex;
            this.initialiseGridANV();
        }
        protected void gvANV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheANV(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                serviceGeneral.delete("voyageabonnementdevis", "numVoyageAbonnementDevis", e.CommandArgument.ToString());
                this.initialiseGridANV();
            }
        }

        protected void btnAnnulerDevis_Click(object sender, EventArgs e)
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
                    serviceProforma.deleteProforma(proforma);
                    this.initialiseFormulaireADT();
                    this.initialiseFormulaireANV();
                    this.initialiseGridADT();
                    this.initialiseGridANV();
                    this.initialiseGridProforma();
                    hfNumProforma.Value = "";

                    this.divIndicationTextAV("", "Red");
                    this.divIndicationTextAD("", "Red");

                    Panel_Societe.Visible = false;
                    Panel_Organisme.Visible = false;
                    Panel_Individu.Visible = false;
                }
            }
            #endregion
        }
        protected void btnNouveauDevis_Click(object sender, EventArgs e)
        {
            #region implementation
            this.initialiseFormulaireADT();
            this.initialiseFormulaireANV();
            hfNumProforma.Value = "";
            this.initialiseGridADT();
            this.initialiseGridANV();

            this.divIndicationTextAV("", "Red");
            this.divIndicationTextAD("", "Red");

            btnNouveauDevis.Enabled = false;
            btnAnnulerDevis.Enabled = false;

            Panel_Societe.Visible = false;
            Panel_Organisme.Visible = false;
            Panel_Individu.Visible = false;
            #endregion
        }


        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Panel_FormulaireAbonner.Visible = false;
            this.initialiseFormulaireAbonner();
        }
        protected void btnValiderAbonner_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAbonnement abonner = null;
            #endregion

            #region implementation
            abonner = serviceAbonnement.selectAbonnement(hfNumAbonnementTemp.Value);
            if (abonner != null)
            {
                hfNumAbonnement.Value = abonner.NumAbonnement;
                if (abonner.organisme != null)
                {
                    btnAbonner.Text = "N°" + abonner.NumAbonnement + " de " + abonner.organisme.NomOrganisme;
                }
                if (abonner.societe != null)
                {
                    btnAbonner.Text = "N°" + abonner.NumAbonnement + " de " + abonner.societe.NomSociete;
                }
                if (abonner.individu != null)
                {
                    btnAbonner.Text = "N°" + abonner.NumAbonnement + " de " + abonner.individu.NomIndividu + " " + abonner.individu.PrenomIndividu;
                }
            }

            #region initialiseFormulaire
            Panel_FormulaireAbonner.Visible = false;

            this.initialiseFormulaireADT();
            this.initialiseFormulaireANV();
            hfNumProforma.Value = "";
            this.initialiseGridADT();
            this.initialiseGridANV();

            this.divIndicationTextAV("", "Red");
            this.divIndicationTextAD("", "Red");

            btnNouveauDevis.Enabled = false;
            btnAnnulerDevis.Enabled = false;

            Panel_Societe.Visible = false;
            Panel_Organisme.Visible = false;
            Panel_Individu.Visible = false;
            #endregion
            #endregion
        }
        protected void btnAbonnerListe_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireAbonner();
            Panel_FormulaireAbonner.CssClass = "PanneauAction";
            Panel_FormulaireAbonner.Visible = true;
        }
        protected void btnAbonner_Click(object sender, EventArgs e)
        {
            #region implementation
            this.afficheAbonnement(hfNumAbonnement.Value);
            Panel_FormulaireAbonner.CssClass = "PanneauAction";
            Panel_FormulaireAbonner.Visible = true;
            #endregion
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

            #region implementation
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
                        this.initialiseGridADT();
                        this.initialiseGridANV();
                    }
                }
            }
            #endregion
        }


        #region paiement
        protected void ddlModePaiement_SelectedIndexChanged(object sender, EventArgs e)
        {
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

                                        this.initialiseFormulaireADT();
                                        this.initialiseFormulaireANV();
                                        hfNumProforma.Value = "";
                                        this.initialiseGridADT();
                                        this.initialiseGridANV();

                                        btnNouveauDevis.Enabled = false;
                                        btnAnnulerDevis.Enabled = false;

                                        Panel_Societe.Visible = false;
                                        Panel_Organisme.Visible = false;
                                        Panel_Individu.Visible = false;

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
                        else if (ddlModePaiement.SelectedValue.Equals("Chèque"))
                        {
                            if (agent.sessionCaisse != null)
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
                        }
                        else if (ddlModePaiement.SelectedValue.Equals("Bon de commande"))
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