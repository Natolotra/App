using AppRessources.Ressources;
using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.administrateur
{
    public partial class Vehicule : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalProprietaire serviceProprietaire = null;
        IntfDalIndividu serviceIndividu = null;
        IntfDalSociete serviceSociete = null;
        IntfDalOrganisme serviceOrganisme = null;
        IntfDalSourceEnergie serviceSourceEnergie = null;
        IntfDalZone serviceZone = null;
        IntfDalCooperative serviceCooperative = null;
        IntfDalParamVehicule serviceParamVehicule = null;
        IntfDalLicence serviceLicence = null;
        IntfDalVehicule serviceVehicule = null;
        IntfDalChauffeur serviceChauffeur = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalItineraire serviceItineraire = null;
        IntfDalLien serviceLien = null;
        IntfDalSituationFamiliale serviceSituationFamiliale = null;
        IntfDalProvince serviceProvince = null;
        IntfDalRegion serviceRegion = null;
        IntfDalDistrict serviceDistrict = null;
        IntfDalCommune serviceCommune = null;
        IntfDalQuartier serviceQuartier = null;
        IntfDalArrondissement serviceArrondissement = null;
        IntfDalEtatCivil serviceEtatCivil = null;

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
            serviceProprietaire = new ImplDalProprietaire();
            serviceIndividu = new ImplDalIndividu();
            serviceOrganisme = new ImplDalOrganisme();
            serviceSociete = new ImplDalSociete();
            serviceSourceEnergie = new ImplDalSourceEnergie();
            serviceZone = new ImplDalZone();
            serviceCooperative = new ImplDalCooperative();
            serviceLicence = new ImplDalLicence();
            serviceParamVehicule = new ImplDalParamVehicule();
            serviceVehicule = new ImplDalVehicule();
            serviceChauffeur = new ImplDalChauffeur();
            serviceGeneral = new ImplDalGeneral();
            serviceItineraire = new ImplDalItineraire();
            serviceSituationFamiliale = new ImplDalSituationFamiliale();
            serviceEtatCivil = new ImplDalEtatCivil();
            serviceProvince = new ImplDalProvince();
            serviceRegion = new ImplDalRegion();
            serviceDistrict = new ImplDalDistrict();
            serviceCommune = new ImplDalCommune();
            serviceArrondissement = new ImplDalArrondissement();
            serviceQuartier = new ImplDalQuartier();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();

                //this.initialiseGridItineraire();
                this.serviceChauffeur.loadDdlTri(ddlTriChauffeur);

                this.serviceSourceEnergie.loadDddlSourceEnergie(ddlSourceEnergie);
                try
                {
                    ddlSourceEnergie.SelectedValue = "Gasoil";
                }
                catch (Exception)
                {
                }
                this.serviceZone.loadDDLAllZone(ddlZone, "Suburbaine;Urbaine");
                this.serviceCooperative.loadDdlCooperative(ddlCooperative);

                this.initialiseFormulaireProprietaireAll();
                this.initialiseGridProprietaire();

                this.initialiseDdlVehicule();

                this.serviceSituationFamiliale.loadDddlSituationFamiliale(ddlSituationFamiliale);
                /*********itineraire******************/
                serviceItineraire.loadDdlItineraireVille1(ddlDebutItineraire1);
                serviceItineraire.loadDdlItineraireVille1(ddlDebutItineraire2);

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "051"))
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
            #region vehiculeLicenceParam
            TextMatricule_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.matriculeVehiculeNonVide;
            TextMatricule_RegularExpressionValidator.ErrorMessage = ReVehiculeParametre.matriculeForme;
            TextMarque_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.marqueVehiculeNonVide;
            TextType_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.typeVehiculeNonVide;
            TextNumSerie_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.numSerieNonVide;
            TextNumMoteur_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.numMoteurNonVide;
            TextPlaceAssise_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.nbPlaceAssiseNonVide;
            TextNbColonne_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.nbColonneNonVide;
            TextNumerosLicence_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.numerosLicenceNonVide;
            ddlZone_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.zoneNonVide;
            ddlCooperative_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.cooperativeNonVide;
            TextDateMiseCirculation_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.dateMiseEnCirculationNonVide;
            TextPremiereExploitation_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.datePremiereExploitationNoNVide;
            TextDateValideDu_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.dateValideDuNonVide;
            TextDateValideAu_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.dateValideAuNonVide;
            TextNbPlacePayante_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.nbPlacePayanteNonVide;
            TextNbPlaceMin_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.nombrePlaceMinNonVide;
            TextPoidsBagageMax_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.poidBagageMaximum;
            TextAvanceCarburant_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.avanceCarburantNonVide;
            TextAvanceChauffeur_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.avanceChauffeurNonVide;
            TextFond_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.fondNonVide;
            ddlFinItineraire1_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.itineraireNonVide;
            #endregion

            #region chauffeur
            TextNom_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.NonChauffeurNonVide;
            TextCIN_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.cinChauffeurNonVide;
            TextAdresse_RequiredFieldValidator.ErrorMessage = ReVehiculeParametre.adresseChauffeurNonVide;
            #endregion
        }

        #region proprietaire
        private void initialiseGridProprietaire()
        {
            serviceProprietaire.insertToGridProprietaireAll(gvProprietaire, ddlTriProprietaire.SelectedValue, ddlTriProprietaire.SelectedValue, TextRechercheProprietaire.Text, agent.agence.NumAgence);
        }

        private void initialiseSociete()
        {
            if (RadioListeProprietaire.SelectedValue.Equals("societe"))
            {
                PanelSociete.Visible = true;
                btnValiderProprietaire.ValidationGroup = "groupeSociete";
                btnModifierProprietaire.ValidationGroup = "groupeSociete";
                btnAjouterProprietaire.ValidationGroup = "groupeSociete";
            }
            else
            {
                PanelSociete.Visible = false;
            }
        }

        private void initialiseOrganisme()
        {
            if (RadioListeProprietaire.SelectedValue.Equals("organisme"))
            {
                PanelOrganisme.Visible = true;
                btnValiderProprietaire.ValidationGroup = "groupeOrganisme";
                btnModifierProprietaire.ValidationGroup = "groupeOrganisme";
                btnAjouterProprietaire.ValidationGroup = "groupeOrganisme";
            }
            else
            {
                PanelOrganisme.Visible = false;
            }
        }

        private void initialiseIndividu()
        {
            if (RadioListeProprietaire.SelectedValue.Equals("individu"))
            {
                PanelIndividu.Visible = true;
                btnValiderProprietaire.ValidationGroup = "groupeIndividu";
                btnModifierProprietaire.ValidationGroup = "groupeIndividu";
                btnAjouterProprietaire.ValidationGroup = "groupeIndividu";
            }
            else
            {
                PanelIndividu.Visible = false;
            }
        }

        private void initialiseFormulaireProprietaire()
        {
            this.initialiseFormulaireIndividu();
            this.initialiseFormulaireOrganisme();
            this.initialiseFormulaireSociete();
            hfNumProprietaireTemp.Value = "";

            ConfirmButtonExtender_btnModifierProprietaire.ConfirmText = "";
        }

        private void initialiseFormulaireProprietaireAll()
        {
            try
            {
                RadioListeProprietaire.SelectedValue = "individu";
            }
            catch (Exception)
            {
            }

            this.initialiseIndividu();
            this.initialiseOrganisme();
            this.initialiseSociete();

            this.initialiseFormulaireProprietaire();
            btnModifierProprietaire.Enabled = false;
            btnValiderProprietaire.Enabled = false;
            btnAjouterProprietaire.Enabled = true;
            RadioListeProprietaire.Enabled = true;
        }

        private void afficheProprietaire(string numProprietaire)
        {
            #region declaration
            crlProprietaire proprietaire = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                proprietaire = serviceProprietaire.selectProprietaire(numProprietaire);
                if (proprietaire != null)
                {
                    hfNumProprietaireTemp.Value = proprietaire.NumProprietaire;

                    if (proprietaire.Individu != null)
                    {
                        TextAdresseClient.Text = proprietaire.Individu.Adresse;
                        TextCinClient.Text = proprietaire.Individu.CinIndividu;
                        TextNomClient.Text = proprietaire.Individu.NomIndividu;
                        TextPrenom.Text = proprietaire.Individu.PrenomIndividu;
                        TextProfessionIndividu.Text = proprietaire.Individu.Profession;
                        TextTelephoneFixeClient.Text = proprietaire.Individu.TelephoneFixeIndividu;
                        TextTelephoneMobile.Text = proprietaire.Individu.TelephoneMobileIndividu;
                        this.afficheQuartierIndividu(proprietaire.Individu.NumQuartier);
                        try
                        {
                            ddlCiviliteIndividu.SelectedValue = proprietaire.Individu.CiviliteIndividu;
                        }
                        catch (Exception)
                        {
                        }
                        if (proprietaire.Individu.DateNaissanceIndividu.Year > 1900)
                        {
                            TextDateNaissanceIndividu.Text = proprietaire.Individu.DateNaissanceIndividu.ToString("dd MMMM yyyy");
                        }
                        TextLieuNaissanceIndividu.Text = proprietaire.Individu.LieuNaissanceIndividu;
                        TextMailIndividu.Text = proprietaire.Individu.MailIndividu;

                        strConfirm = "Voulez vous vraiment modifier les informations sur \n";
                        strConfirm += proprietaire.Individu.PrenomIndividu + " " + proprietaire.Individu.NomIndividu + "?\n";
                        strConfirm += "N°" + proprietaire.NumProprietaire;
                        ConfirmButtonExtender_btnModifierProprietaire.ConfirmText = strConfirm;

                        try
                        {
                            ddlCiviliteIndividu.SelectedValue = proprietaire.Individu.CiviliteIndividu;
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            RadioListeProprietaire.SelectedValue = "individu";
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (proprietaire.societe != null)
                    {
                        TextAdresseSociete.Text = proprietaire.societe.AdresseSociete;
                        TextMailSociete.Text = proprietaire.societe.MailSociete;
                        TextMobileSociete.Text = proprietaire.societe.TelephoneMobileSociete;
                        TextNomSociete.Text = proprietaire.societe.NomSociete;
                        TextSecteurSociete.Text = proprietaire.societe.SecteurActiviteSociete;
                        TextTelephoneSociete.Text = proprietaire.societe.TelephoneFixeSociete;
                        this.afficheQuartierSociete(proprietaire.societe.NumQuartier);
                        if (proprietaire.societe.IsReductionUS > 0)
                        {
                            cbReductionUS.Checked = true;
                        }
                        else
                        {
                            cbReductionUS.Checked = false;
                        }
                        this.initialiseCB();

                        if (proprietaire.societe.individuResponsable != null)
                        {
                            try
                            {
                                ddlCiviliteRespSociete.SelectedValue = proprietaire.societe.individuResponsable.CiviliteIndividu;
                            }
                            catch (Exception)
                            {
                            }
                            TextAdresseRespSociete.Text = proprietaire.societe.individuResponsable.Adresse;
                            TextCinRespSociete.Text = proprietaire.societe.individuResponsable.CinIndividu;
                            TextFixeRespSociete.Text = proprietaire.societe.individuResponsable.TelephoneFixeIndividu;
                            TextMailRespSociete.Text = proprietaire.societe.individuResponsable.MailIndividu;
                            TextMobileRespSociete.Text = proprietaire.societe.individuResponsable.TelephoneMobileIndividu;
                            TextNomResponsableSociete.Text = proprietaire.societe.individuResponsable.NomIndividu;
                            TextPrenomRespSociete.Text = proprietaire.societe.individuResponsable.PrenomIndividu;
                            this.afficheQuartierRespSociete(proprietaire.societe.individuResponsable.NumQuartier);
                        }


                        strConfirm = "Voulez vous vraiment modifier les informations sur \n";
                        strConfirm += proprietaire.societe.NomSociete + "?\n";
                        strConfirm += "N°" + proprietaire.NumProprietaire;
                        ConfirmButtonExtender_btnModifierProprietaire.ConfirmText = strConfirm;
                        try
                        {
                            RadioListeProprietaire.SelectedValue = "societe";
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (proprietaire.organisme != null)
                    {
                        TextAdresseOrganisme.Text = proprietaire.organisme.AdresseOrganisme;
                        TextFixeOrganisme.Text = proprietaire.organisme.TelephoneFixeOrganisme;
                        TextMailOrganisme.Text = proprietaire.organisme.MailOrganisme;
                        TextMobileOrganisme.Text = proprietaire.organisme.TelephoneMobileOrganisme;
                        TextNomOrganisme.Text = proprietaire.organisme.NomOrganisme;
                        this.afficheQuartierOrganisme(proprietaire.organisme.NumQuartier);

                        if (proprietaire.organisme.individuResponsable != null)
                        {
                            try
                            {
                                ddlCiviliteRespOrganisme.SelectedValue = proprietaire.organisme.individuResponsable.CiviliteIndividu;
                            }
                            catch (Exception)
                            {
                            }
                            TextAdresseRespOrganisme.Text = proprietaire.organisme.individuResponsable.Adresse;
                            TextCinRespOrganisme.Text = proprietaire.organisme.individuResponsable.CinIndividu;
                            TextFixeRespOrganisme.Text = proprietaire.organisme.individuResponsable.TelephoneFixeIndividu;
                            TextMailRespOrganisme.Text = proprietaire.organisme.individuResponsable.MailIndividu;
                            TextMobileRespOrganisme.Text = proprietaire.organisme.individuResponsable.TelephoneMobileIndividu;
                            TextNomRespOrganisme.Text = proprietaire.organisme.individuResponsable.NomIndividu;
                            TextPrenomRespOrganisme.Text = proprietaire.organisme.individuResponsable.PrenomIndividu;
                            this.afficheQuartierRespOrganisme(proprietaire.organisme.individuResponsable.NumQuartier);
                        }

                        strConfirm = "Voulez vous vraiment modifier les informations sur \n";
                        strConfirm += proprietaire.organisme.NomOrganisme + "?\n";
                        strConfirm += "N°" + proprietaire.NumProprietaire;
                        ConfirmButtonExtender_btnModifierProprietaire.ConfirmText = strConfirm;
                        try
                        {
                            RadioListeProprietaire.SelectedValue = "organisme";
                        }
                        catch (Exception)
                        {
                        }

                    }

                    this.initialiseIndividu();
                    this.initialiseOrganisme();
                    this.initialiseSociete();
                    btnModifierProprietaire.Enabled = true;
                    btnValiderProprietaire.Enabled = true;
                    btnAjouterProprietaire.Enabled = false;
                    RadioListeProprietaire.Enabled = false;
                }
            }
            #endregion
        }

        private void insertToObjetProprietaire(crlProprietaire proprietaire)
        {
            #region implementation
            if (proprietaire != null)
            {
                proprietaire.NumAgence = agent.numAgence;
                proprietaire.agence = agent.agence;

                if (RadioListeProprietaire.SelectedValue.Equals("societe"))
                {
                    proprietaire.TypeProprietaire = "Société";
                    if (proprietaire.societe == null)
                    {
                        proprietaire.societe = new crlSociete();
                    }
                    proprietaire.societe.AdresseSociete = TextAdresseSociete.Text;
                    proprietaire.societe.MailSociete = TextMailSociete.Text;
                    proprietaire.societe.NomSociete = TextNomSociete.Text;
                    proprietaire.societe.SecteurActiviteSociete = TextSecteurSociete.Text;
                    proprietaire.societe.TelephoneFixeSociete = TextTelephoneSociete.Text;
                    proprietaire.societe.TelephoneMobileSociete = TextMobileSociete.Text;
                    if (cbReductionUS.Checked)
                    {
                        proprietaire.societe.IsReductionUS = 1;
                    }
                    else
                    {
                        proprietaire.societe.IsReductionUS = 0;
                    }
                    proprietaire.societe.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierSociete.Text, ddlCommuneSociete.SelectedValue, ddlArrondissementSociete.SelectedValue, agent.agence.SigleAgence);
                    if (proprietaire.societe.individuResponsable == null)
                    {
                        proprietaire.societe.individuResponsable = new crlIndividu();
                    }

                    proprietaire.societe.individuResponsable.CiviliteIndividu = ddlCiviliteRespSociete.SelectedValue;
                    proprietaire.societe.individuResponsable.Adresse = TextAdresseRespSociete.Text;
                    proprietaire.societe.individuResponsable.CinIndividu = TextCinRespSociete.Text;
                    proprietaire.societe.individuResponsable.MailIndividu = TextMailRespSociete.Text;
                    proprietaire.societe.individuResponsable.NomIndividu = TextNomResponsableSociete.Text;
                    proprietaire.societe.individuResponsable.PrenomIndividu = TextPrenomRespSociete.Text;
                    proprietaire.societe.individuResponsable.TelephoneFixeIndividu = TextFixeRespSociete.Text;
                    proprietaire.societe.individuResponsable.TelephoneMobileIndividu = TextMobileRespSociete.Text;
                    proprietaire.societe.individuResponsable.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierRespSociete.Text, ddlCommuneRespSociete.SelectedValue, ddlArrondissementRespSociete.SelectedValue, agent.agence.SigleAgence);
                }
                if (RadioListeProprietaire.SelectedValue.Equals("organisme"))
                {
                    proprietaire.TypeProprietaire = "Organisme";
                    if (proprietaire.organisme == null)
                    {
                        proprietaire.organisme = new crlOrganisme();
                    }
                    proprietaire.organisme.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierOrganisme.Text, ddlCommuneOrganisme.SelectedValue, ddlArrondissementOrganisme.SelectedValue, agent.agence.SigleAgence);
                    proprietaire.organisme.AdresseOrganisme = TextAdresseOrganisme.Text;
                    proprietaire.organisme.MailOrganisme = TextMailOrganisme.Text;
                    proprietaire.organisme.NomOrganisme = TextNomOrganisme.Text;
                    proprietaire.organisme.TelephoneFixeOrganisme = TextFixeOrganisme.Text;
                    proprietaire.organisme.TelephoneMobileOrganisme = TextMobileOrganisme.Text;

                    if (proprietaire.organisme.individuResponsable == null)
                    {
                        proprietaire.organisme.individuResponsable = new crlIndividu();
                    }
                    proprietaire.organisme.individuResponsable.CiviliteIndividu = ddlCiviliteRespOrganisme.SelectedValue;
                    proprietaire.organisme.individuResponsable.Adresse = TextAdresseRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.CinIndividu = TextCinRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.MailIndividu = TextMailRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.NomIndividu = TextNomRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.PrenomIndividu = TextPrenomRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.TelephoneFixeIndividu = TextFixeRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.TelephoneMobileIndividu = TextMobileRespOrganisme.Text;
                    proprietaire.organisme.individuResponsable.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierRespOrganisme.Text, ddlCommuneRespOrganisme.SelectedValue, ddlArrondissementRespOrganisme.SelectedValue, agent.agence.SigleAgence);

                }
                if (RadioListeProprietaire.SelectedValue.Equals("individu"))
                {
                    proprietaire.TypeProprietaire = "Individu";
                    if (proprietaire.Individu == null)
                    {
                        proprietaire.Individu = new crlIndividu();
                    }
                    proprietaire.Individu.CiviliteIndividu = ddlCiviliteIndividu.SelectedValue;
                    proprietaire.Individu.Adresse = TextAdresseClient.Text;
                    proprietaire.Individu.CinIndividu = TextCinClient.Text;
                    proprietaire.Individu.CiviliteIndividu = ddlCiviliteIndividu.SelectedValue;
                    proprietaire.Individu.NomIndividu = TextNomClient.Text;
                    proprietaire.Individu.PrenomIndividu = TextPrenom.Text;
                    proprietaire.Individu.Profession = TextProfessionIndividu.Text;
                    proprietaire.Individu.TelephoneFixeIndividu = TextTelephoneFixeClient.Text;
                    proprietaire.Individu.TelephoneMobileIndividu = TextTelephoneMobile.Text;
                    proprietaire.Individu.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierIndividu.Text, ddlCommuneIndividu.SelectedValue, ddlArrondissementIndividu.SelectedValue, agent.agence.SigleAgence);
                    try
                    {
                        proprietaire.Individu.DateNaissanceIndividu = Convert.ToDateTime(TextDateNaissanceIndividu.Text);
                    }
                    catch (Exception)
                    {
                    }
                    proprietaire.Individu.LieuNaissanceIndividu = TextLieuNaissanceIndividu.Text;
                }
            }
            #endregion
        }

        private void insertProprietaire(crlProprietaire proprietaire)
        {
            #region implementation
            if (proprietaire != null)
            {
                if (proprietaire.Individu != null)
                {
                    proprietaire.Individu.NumIndividu = serviceIndividu.isIndividu(proprietaire.Individu);

                    if (proprietaire.Individu.NumIndividu != "")
                    {
                        proprietaire.Individu = serviceIndividu.selectIndividu(proprietaire.Individu.NumIndividu);
                    }
                    else
                    {
                        proprietaire.Individu.NumIndividu = serviceIndividu.insertIndividu(proprietaire.Individu, agent.agence.SigleAgence);
                    }

                    if (proprietaire.Individu.NumIndividu != "")
                    {
                        proprietaire.NumIndividu = proprietaire.Individu.NumIndividu;
                    }
                }
                if (proprietaire.societe != null)
                {
                    proprietaire.societe.NumSociete = serviceSociete.isSociete(proprietaire.societe);

                    if (proprietaire.societe.NumSociete != "")
                    {
                        proprietaire.societe = serviceSociete.selectSociete(proprietaire.societe.NumSociete);
                    }
                    else
                    {
                        proprietaire.societe.NumSociete = serviceSociete.insertSociete(proprietaire.societe, agent.agence.SigleAgence);
                    }

                    if (proprietaire.societe.NumSociete != "")
                    {
                        proprietaire.NumSociete = proprietaire.societe.NumSociete;
                    }
                }
                if (proprietaire.organisme != null)
                {
                    proprietaire.organisme.NumOrganisme = serviceOrganisme.isOrganisme(proprietaire.organisme);

                    if (proprietaire.organisme.NumOrganisme != "")
                    {
                        proprietaire.organisme = serviceOrganisme.selectOrganisme(proprietaire.organisme.NumOrganisme);
                    }
                    else
                    {
                        proprietaire.organisme.NumOrganisme = serviceOrganisme.insertOrganisme(proprietaire.organisme, agent.agence.SigleAgence);
                    }

                    if (proprietaire.organisme.NumOrganisme != "")
                    {
                        proprietaire.NumOrganisme = proprietaire.organisme.NumOrganisme;
                    }
                }

                proprietaire.NumProprietaire = serviceProprietaire.isProprietaire(proprietaire);

                if (proprietaire.NumProprietaire != "")
                {
                    proprietaire = serviceProprietaire.selectProprietaire(proprietaire.NumProprietaire);
                }
                else
                {
                    proprietaire.NumProprietaire = serviceProprietaire.insertProprietaire(proprietaire, agent.agence.SigleAgence);
                }

                if (proprietaire.NumProprietaire != "")
                {
                    this.afficheProprietaire(proprietaire.NumProprietaire);
                    this.initialiseGridProprietaire();
                }
            }
            #endregion
        }

        private void upDateProprietaire(crlProprietaire proprietaire)
        {
            #region implementation
            if (proprietaire != null)
            {
                if (proprietaire.Individu != null)
                {
                    serviceIndividu.updateIndividu(proprietaire.Individu);
                }
                if (proprietaire.societe != null)
                {
                    serviceSociete.updateSociete(proprietaire.societe);
                }
                if (proprietaire.organisme != null)
                {
                    serviceOrganisme.updateOrganisme(proprietaire.organisme);
                }
                this.initialiseGridProprietaire();
                this.afficheProprietaire(proprietaire.NumProprietaire);
            }
            #endregion
        }
        #endregion

        #region vehicule licence parametre
        private void divIndicationVehicule(string str, string color)
        {
            if (str != "" && color != "")
            {
                indicationVehicule.Style.Add("font-size", "14px");
                indicationVehicule.Style.Add("color", color);
                indicationVehicule.InnerHtml = "<p>" + str + "</p>";
            }
            else
            {
                indicationVehicule.InnerHtml = "";
            }
        }

        private void initialiseFormulaireVehiculeLicenceParametre()
        {
            #region implementation
            TextMatricule.Text = "";
            TextMarque.Text = "";
            TextType.Text = "";
            TextNumSerie.Text = "";
            try
            {
                ddlSourceEnergie.SelectedValue = "Gasoil";
            }
            catch (Exception)
            {
            }

            TextNumMoteur.Text = "";
            TextPuissance.Text = "";
            TextCouleur.Text = "";
            TextPlaceAssise.Text = "";
            TextNbColonne.Text = "";
            TextPoidTotal.Text = "";
            TextPoidVide.Text = "";

            TextNumerosLicence.Text = "";
            try
            {
                ddlZone.SelectedValue = "";
                ddlCooperative.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            TextDateMiseCirculation.Text = "";
            TextPremiereExploitation.Text = "";
            TextDateValideDu.Text = "";
            TextDateValideAu.Text = "";
            TextNbPlacePayante.Text = "";

            TextNbPlaceMin.Text = "";
            TextPoidsBagageMax.Text = "";
            TextAvanceCarburant.Text = "";
            TextAvanceChauffeur.Text = "";
            TextFond.Text = "";

            hfNumLicence.Value = "";
            hfNumVehicule.Value = "";
            this.initialiseGridChauffeur();
            //this.initialiseGridItineraireLicence();
            this.initialiseFormulaireChauffeur();


            try
            {
                ddlDebutItineraire1.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            try
            {
                ddlDebutItineraire2.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            serviceItineraire.loadDdlItineraireVille2(ddlFinItineraire1, ddlDebutItineraire1.SelectedValue);
            serviceItineraire.loadDdlItineraireVille2(ddlFinItineraire2, ddlDebutItineraire2.SelectedValue);

            this.divIndicationVehicule("", "Red");
            #endregion
        }

        private void insertToObjetVehicule(crlVehicule vehicule)
        {
            #region implementation
            if (vehicule != null)
            {
                if (hfNumProprietaire.Value != "")
                {
                    vehicule.CouleurVehicule = TextCouleur.Text;
                    vehicule.MarqueVehicule = TextMarque.Text;
                    vehicule.MatriculeVehicule = TextMatricule.Text;
                    try
                    {
                        vehicule.NombreColoneVehicule = int.Parse(TextNbColonne.Text);
                    }
                    catch (Exception)
                    {
                    }
                    vehicule.NumMoteurVehicule = TextNumMoteur.Text;
                    vehicule.NumProprietaire = hfNumProprietaire.Value;
                    vehicule.NumSerieVehicule = TextNumSerie.Text;
                    try
                    {
                        vehicule.PlacesAssiseVehicule = int.Parse(TextPlaceAssise.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        vehicule.PoidsTotalVehicule = double.Parse(TextPoidTotal.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        vehicule.PoidsVideVehicule = double.Parse(TextPoidVide.Text);
                    }
                    catch (Exception)
                    {
                    }
                    vehicule.PuissanceVehicule = TextPuissance.Text;
                    vehicule.SourceEnergie = ddlSourceEnergie.SelectedValue;
                    vehicule.TypeVehicule = TextType.Text;

                    if (vehicule.paramVehicule == null)
                    {
                        vehicule.paramVehicule = new crlParamVehicule();
                    }

                    try
                    {
                        vehicule.paramVehicule.AvanceCarburantMax = double.Parse(TextAvanceCarburant.Text);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        vehicule.paramVehicule.AvanceChauffeurMax = double.Parse(TextAvanceChauffeur.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        vehicule.paramVehicule.Fond = double.Parse(TextFond.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        vehicule.paramVehicule.NbPassagerMin = int.Parse(TextNbPlaceMin.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        vehicule.paramVehicule.PoidBagageMax = double.Parse(TextPoidsBagageMax.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                }
            }
            #endregion
        }

        private void insertToObjetLicence(crlLicence licence)
        {
            #region implementation
            if (licence != null)
            {
                try
                {
                    licence.DatePremiereExploitation = Convert.ToDateTime(TextPremiereExploitation.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    licence.DatePremiereMiseCiculation = Convert.ToDateTime(TextDateMiseCirculation.Text);
                }
                catch (Exception)
                {
                }
                if (checkProvisoire.Checked)
                {
                    licence.IsProvisoire = 1;
                }
                else
                {
                    licence.IsProvisoire = 0;
                }
                try
                {
                    licence.NombrePlacePayante = int.Parse(TextNbPlacePayante.Text);
                }
                catch (Exception)
                {
                }
                licence.NumCooperative = ddlCooperative.SelectedValue;
                licence.NumerosLicence = TextNumerosLicence.Text;
                try
                {
                    licence.ValideAu = Convert.ToDateTime(TextDateValideAu.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    licence.ValideDu = Convert.ToDateTime(TextDateValideDu.Text);
                }
                catch (Exception)
                {
                }
                licence.Zone = ddlZone.SelectedValue;

            }
            #endregion
        }

        private void insertVehiculeLicenceParametre(crlVehicule vehicule, crlLicence licence)
        {
            #region declaration
            string fileName = "";
            string[] tableFileName;
            string urlSaving = "";
            string strIndication = "";
            #endregion

            #region implementation
            if (vehicule != null && licence != null)
            {
                if (vehicule.paramVehicule != null)
                {
                    vehicule.paramVehicule.NumParamVehicule = serviceParamVehicule.insertParamVehicule(vehicule.paramVehicule, agent.agence.SigleAgence);

                    if (vehicule.paramVehicule.NumParamVehicule != "")
                    {
                        vehicule.NumParamVehicule = vehicule.paramVehicule.NumParamVehicule;

                        vehicule.NumVehicule = serviceVehicule.isVehicule(vehicule);
                        if (vehicule.NumVehicule != "")
                        {
                            strIndication = "Ce véhicule est déjà enregistrer dans la base de donnée!";
                            this.divIndicationVehicule(strIndication, "Red");
                        }
                        else
                        {
                            vehicule.NumVehicule = serviceVehicule.insertVehicule(vehicule, agent.agence.SigleAgence);
                            if (vehicule.NumVehicule != "")
                            {
                                licence.NumVehicule = vehicule.NumVehicule;
                                licence.NumLicence = serviceLicence.insertLicence(licence, agent.agence.SigleAgence);

                                if (licence.NumLicence != "")
                                {
                                    this.afficheVehiculeLicenceParametre(licence.NumLicence);
                                    this.initialiseDdlVehicule();
                                    this.serviceLicence.insertAssoItineraireLicence(licence.NumLicence, ddlFinItineraire1.SelectedValue);
                                    this.serviceLicence.insertAssoItineraireLicence(licence.NumLicence, ddlFinItineraire2.SelectedValue);

                                    strIndication = "Véhicule " + vehicule.MarqueVehicule + " bien enregistrer!";
                                    this.divIndicationVehicule(strIndication, "Black");
                                }
                                else
                                {
                                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                                    this.divIndicationVehicule(strIndication, "Red");
                                }


                                if (FileUpload_Image.HasFile)
                                {
                                    fileName = FileUpload_Image.FileName;

                                    tableFileName = fileName.Split('.');
                                    if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg"))
                                    {
                                        urlSaving = this.urlSavingImageVehicule(vehicule.NumVehicule, "jpg");

                                        FileUpload_Image.SaveAs(urlSaving);

                                        vehicule.ImageVehicule = vehicule.NumVehicule.Replace('/', '_') + ".jpg";
                                    }
                                    else if (tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                                    {
                                        urlSaving = this.urlSavingImageVehicule(vehicule.NumVehicule, "png");

                                        FileUpload_Image.SaveAs(urlSaving);

                                        vehicule.ImageVehicule = vehicule.NumVehicule.Replace('/', '_') + ".png";
                                    }

                                    serviceVehicule.upDateVehicule(vehicule);

                                }
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                this.divIndicationVehicule(strIndication, "Red");
                            }
                        }
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!";
                        this.divIndicationVehicule(strIndication, "Red");
                    }
                }
                else
                {
                    //pas de param
                }
            }
            #endregion
        }

        private void upDateVehiculeLicenceParametre(crlVehicule vehicule, crlLicence licence)
        {
            #region declaration
            string fileName = "";
            string[] tableFileName;
            string urlSaving = "";
            string strIndication = "";
            string numVehicule = "";
            string numLicence = "";
            #endregion

            #region implementation
            if (vehicule != null && licence != null)
            {


                numVehicule = serviceVehicule.isVehicule(vehicule);
                if (numVehicule != "")
                {
                    strIndication = "Ce véhicule est déjà enregistrer dans la base de donnée!";
                    this.divIndicationVehicule(strIndication, "Red");
                }
                else
                {
                    if (vehicule.paramVehicule != null)
                    {
                        serviceParamVehicule.upDateParamVehicule(vehicule.paramVehicule);
                    }

                    if (serviceVehicule.upDateVehicule(vehicule) && serviceLicence.updateLicence(licence))
                    {
                        strIndication = "Modification de " + vehicule.MatriculeVehicule + " bien enregistrer!";
                        this.divIndicationVehicule(strIndication, "Red");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant la modification!";
                        this.divIndicationVehicule(strIndication, "Red");
                    }
                }



                if (vehicule.NumVehicule != "")
                {
                    if (FileUpload_Image.HasFile)
                    {
                        fileName = FileUpload_Image.FileName;

                        tableFileName = fileName.Split('.');
                        if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg"))
                        {
                            urlSaving = this.urlSavingImageVehicule(vehicule.NumVehicule, "jpg");

                            FileUpload_Image.SaveAs(urlSaving);

                            vehicule.ImageVehicule = vehicule.NumVehicule.Replace('/', '_') + ".jpg";
                        }
                        else if (tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                        {
                            urlSaving = this.urlSavingImageVehicule(vehicule.NumVehicule, "png");

                            FileUpload_Image.SaveAs(urlSaving);

                            vehicule.ImageVehicule = vehicule.NumVehicule.Replace('/', '_') + ".png";
                        }

                        serviceVehicule.upDateVehicule(vehicule);

                    }
                }
            }
            #endregion
        }

        private void afficheVehiculeLicenceParametre(string numLicence)
        {
            #region declaration
            crlLicence licence = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numLicence != "")
            {
                licence = serviceLicence.selectLicence(numLicence);

                if (licence != null)
                {
                    TextNumerosLicence.Text = licence.NumerosLicence;
                    try
                    {
                        ddlZone.SelectedValue = licence.Zone;
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        ddlCooperative.SelectedValue = licence.NumCooperative;
                    }
                    catch (Exception)
                    {
                    }
                    TextDateMiseCirculation.Text = licence.DatePremiereMiseCiculation.ToString("dd MMMM yyyy");
                    TextPremiereExploitation.Text = licence.DatePremiereExploitation.ToString("dd MMMM yyyy");
                    TextDateValideDu.Text = licence.ValideDu.ToString("dd MMMM yyyy");
                    TextDateValideAu.Text = licence.ValideAu.ToString("dd MMMM yyyy");
                    TextNbPlacePayante.Text = licence.NombrePlacePayante.ToString("0");
                    hfNumLicence.Value = licence.NumLicence;

                    if (licence.vehicule != null)
                    {
                        TextMatricule.Text = licence.vehicule.MatriculeVehicule;
                        TextMarque.Text = licence.vehicule.MarqueVehicule;
                        TextType.Text = licence.vehicule.TypeVehicule;
                        TextNumSerie.Text = licence.vehicule.NumSerieVehicule;
                        try
                        {
                            ddlSourceEnergie.SelectedValue = licence.vehicule.SourceEnergie;
                        }
                        catch (Exception)
                        {
                        }

                        TextNumMoteur.Text = licence.vehicule.NumMoteurVehicule;
                        TextPuissance.Text = licence.vehicule.PuissanceVehicule;
                        TextCouleur.Text = licence.vehicule.CouleurVehicule;
                        TextPlaceAssise.Text = licence.vehicule.PlacesAssiseVehicule.ToString("0");
                        TextNbColonne.Text = licence.vehicule.NombreColoneVehicule.ToString("0");
                        TextPoidTotal.Text = licence.vehicule.PoidsTotalVehicule.ToString("0");
                        TextPoidVide.Text = licence.vehicule.PoidsVideVehicule.ToString("0");
                        hfNumVehicule.Value = licence.vehicule.NumVehicule;

                        /*strConfirm = "Voulez vous vraiment modifier le véhicule " + licence.vehicule.MatriculeVehicule + "?\n";
                        strConfirm += "Marque:" + licence.vehicule.MarqueVehicule + "\n";
                        strConfirm += "Couleur:" + licence.vehicule.CouleurVehicule + "\n";

                        btnModifier_ConfirmButtonExtender.ConfirmText = strConfirm;*/

                        if (licence.vehicule.paramVehicule != null)
                        {
                            TextNbPlaceMin.Text = licence.vehicule.paramVehicule.NbPassagerMin.ToString("0");
                            TextPoidsBagageMax.Text = licence.vehicule.paramVehicule.PoidBagageMax.ToString("0");
                            TextAvanceCarburant.Text = licence.vehicule.paramVehicule.AvanceCarburantMax.ToString("0");
                            TextAvanceChauffeur.Text = licence.vehicule.paramVehicule.AvanceChauffeurMax.ToString("0");
                            TextFond.Text = licence.vehicule.paramVehicule.Fond.ToString("0");
                        }
                    }
                }
                this.initialiseGridChauffeur();
                //this.initialiseGridItineraireLicence();

                /*affiche itineraire*/
                if (licence.itineraires != null)
                {
                    if (licence.itineraires.Count > 0)
                    {
                        try
                        {
                            ddlDebutItineraire1.SelectedValue = licence.itineraires[0].NumVilleItineraireDebut;
                        }
                        catch (Exception)
                        {
                        }
                        serviceItineraire.loadDdlItineraireVille2(ddlFinItineraire1, ddlDebutItineraire1.SelectedValue);
                        ddlFinItineraire1.SelectedValue = licence.itineraires[0].IdItineraire;

                        if (licence.itineraires.Count > 1)
                        {
                            try
                            {
                                ddlDebutItineraire2.SelectedValue = licence.itineraires[1].NumVilleItineraireDebut;
                            }
                            catch (Exception)
                            {
                            }
                            serviceItineraire.loadDdlItineraireVille2(ddlFinItineraire2, ddlDebutItineraire2.SelectedValue);
                            ddlFinItineraire2.SelectedValue = licence.itineraires[1].IdItineraire;
                        }
                    }
                }
                /***************/
            }
            #endregion
        }

        /*
        private void initialiseGridVehicule()
        {
            serviceLicence.insertToGridLicenceProprietaire(gvVehicule, ddlTriVehicule.SelectedValue, ddlTriVehicule.SelectedValue, TextRechercheVehicule.Text, hfNumProprietaire.Value);
        }
        */

        private bool testImageVehicule()
        {
            #region declaration
            bool isFileImage = false;
            string fileName = "";
            string[] tableFileName;
            #endregion

            #region implementation
            if (FileUpload_Image.HasFile)
            {
                fileName = FileUpload_Image.FileName;

                tableFileName = fileName.Split('.');
                if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg") || tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                {
                    isFileImage = true;
                }

            }
            else
            {
                isFileImage = true;
            }
            #endregion

            return isFileImage;
        }

        private string urlSavingImageVehicule(string numVehicule, string fichier)
        {
            #region declaration
            string urlImageSaving = "";
            string urlSaving = "";
            #endregion

            #region implementation
            urlImageSaving = ConfigurationManager.AppSettings["urlImageVehicule"] + numVehicule.Replace('/', '_') + "." + fichier;
            urlSaving = @Server.MapPath(urlImageSaving);
            #endregion

            return urlSaving;
        }

        private void initialiseDdlVehicule()
        {
            serviceVehicule.loadDdlVehiculeProprietaire(ddlListeVehicule, hfNumProprietaire.Value);
        }
        #endregion

        #region chauffeur
        private void divIndicationChauffeur(string str, string color)
        {
            if (str != "" && color != "")
            {
                indicationChauffeur.Style.Add("font-size", "14px");
                indicationChauffeur.Style.Add("color", color);
                indicationChauffeur.InnerHtml = str;
            }
            else
            {
                indicationChauffeur.InnerHtml = "";
            }
        }

        private bool testImageChauffeur()
        {
            #region declaration
            bool isFileImage = false;
            string fileName = "";
            string[] tableFileName;
            #endregion

            #region implementation
            if (FileUpload_Chauffeur.HasFile)
            {
                fileName = FileUpload_Chauffeur.FileName;

                tableFileName = fileName.Split('.');
                if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg") || tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                {
                    isFileImage = true;
                }

            }
            else
            {
                isFileImage = true;
            }
            #endregion

            return isFileImage;
        }

        private string urlSavingImageChauffeur(string numChauffeur, string fichier)
        {
            #region declaration
            string urlImageSaving = "";
            string urlSaving = "";
            #endregion

            #region implementation
            urlImageSaving = ConfigurationManager.AppSettings["urlImageChauffeur"] + numChauffeur.Replace('/', '_') + "." + fichier;
            urlSaving = @Server.MapPath(urlImageSaving);
            #endregion

            return urlSaving;
        }

        private void initialiseGridChauffeur()
        {
            if (hfNumVehicule.Value != "")
            {
                serviceChauffeur.insertToGridChauffeur(gvChauffeur, ddlTriChauffeur.SelectedValue, ddlTriChauffeur.SelectedValue, TextRechercheChauffeur.Text, hfNumVehicule.Value);
            }
            else
            {
                serviceChauffeur.insertToGridChauffeur(gvChauffeur, ddlTriChauffeur.SelectedValue, ddlTriChauffeur.SelectedValue, TextRechercheChauffeur.Text, " ");
            }
        }

        private void initialiseFormulaireChauffeur()
        {
            TextNom.Text = "";
            TextPrenomChauffeur.Text = "";
            TextCIN.Text = "";
            TextAdresse.Text = "";
            TextTelephone.Text = "";
            TextMobile.Text = "";
            try
            {
                ddlSituationFamiliale.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            TextDateNaissance.Text = "";
            TextLieuNaissance.Text = "";

            hfIdChauffeur.Value = "";

            //btnModiffierChauffeur_ConfirmButtonExtender.ConfirmText = "";
        }

        private void afficheChauffeur(string idChauffeur)
        {
            #region declaration
            crlChauffeur chauffeur = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (idChauffeur != "")
            {
                chauffeur = serviceChauffeur.selectChauffeur(idChauffeur);
                if (chauffeur != null)
                {
                    TextNom.Text = chauffeur.nomChauffeur;
                    TextPrenomChauffeur.Text = chauffeur.prenomChauffeur;
                    TextCIN.Text = chauffeur.cinChauffeur;
                    TextAdresse.Text = chauffeur.adresseChauffeur;
                    TextTelephone.Text = chauffeur.telephoneChauffeur;
                    TextMobile.Text = chauffeur.telephoneMobileChauffeur;

                    try
                    {
                        ddlSituationFamiliale.SelectedValue = chauffeur.SituationFamilialeChauffeur;
                    }
                    catch (Exception)
                    {
                    }
                    TextDateNaissance.Text = chauffeur.DateNaissanceChauffeur.ToString("dd MMMM yyyy");
                    TextLieuNaissance.Text = chauffeur.LieuNaissanceChauffeur;

                    hfIdChauffeur.Value = chauffeur.idChauffeur;

                    //strConfirm = "Voulez vous vraiment modifier le chauffeur " + chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur + "?";
                    //btnModiffierChauffeur_ConfirmButtonExtender.ConfirmText = strConfirm;
                }
            }
            #endregion
        }

        private void insertToObjetChauffeur(crlChauffeur chauffeur)
        {
            #region implementation
            if (chauffeur != null)
            {
                chauffeur.adresseChauffeur = TextAdresse.Text;
                chauffeur.cinChauffeur = TextCIN.Text;
                chauffeur.nomChauffeur = TextNom.Text;
                chauffeur.prenomChauffeur = TextPrenomChauffeur.Text;
                chauffeur.telephoneChauffeur = TextTelephone.Text;
                chauffeur.telephoneMobileChauffeur = TextMobile.Text;
                chauffeur.SituationFamilialeChauffeur = ddlSituationFamiliale.SelectedValue;
                try
                {
                    chauffeur.DateNaissanceChauffeur = Convert.ToDateTime(TextDateNaissance.Text);
                }
                catch (Exception)
                {
                }
                chauffeur.LieuNaissanceChauffeur = TextLieuNaissance.Text;
            }
            #endregion
        }

        private void insertChauffeur(crlChauffeur chauffeur)
        {
            #region declaration
            string fileName = "";
            string[] tableFileName;
            string urlSaving = "";
            #endregion

            #region implementation
            if (chauffeur != null)
            {
                chauffeur.idChauffeur = serviceChauffeur.isChauffeurStr(chauffeur);
                if (chauffeur.idChauffeur != "")
                {
                    this.afficheChauffeur(chauffeur.idChauffeur);
                }
                else
                {
                    chauffeur.idChauffeur = serviceChauffeur.insertChauffeur(chauffeur, agent.agence.SigleAgence);

                    if (chauffeur.idChauffeur != "")
                    {
                        this.afficheChauffeur(chauffeur.idChauffeur);

                        if (FileUpload_Chauffeur.HasFile)
                        {
                            fileName = FileUpload_Chauffeur.FileName;

                            tableFileName = fileName.Split('.');
                            if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg"))
                            {
                                urlSaving = this.urlSavingImageChauffeur(chauffeur.idChauffeur, "jpg");

                                FileUpload_Chauffeur.SaveAs(urlSaving);

                                chauffeur.ImageChauffeur = chauffeur.idChauffeur.Replace('/', '_') + ".jpg";
                            }
                            else if (tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                            {
                                urlSaving = this.urlSavingImageChauffeur(chauffeur.idChauffeur, "png");

                                FileUpload_Chauffeur.SaveAs(urlSaving);

                                chauffeur.ImageChauffeur = chauffeur.idChauffeur.Replace('/', '_') + ".png";
                            }

                            serviceChauffeur.updateChauffeur(chauffeur);

                        }
                    }
                    else
                    {
                        //
                    }
                }

                if (chauffeur.idChauffeur != "" && hfNumVehicule.Value != "")
                {
                    serviceChauffeur.insertAssoVehiculeChauffeur(hfNumVehicule.Value, chauffeur.idChauffeur);
                    this.initialiseGridChauffeur();
                }
                else
                {
                    //
                }
            }
            #endregion
        }

        private void updateChauffeur(crlChauffeur chauffeur)
        {
            #region declaration
            string idChauffeur = "";

            #endregion

            #region implementation
            if (chauffeur != null)
            {
                idChauffeur = serviceChauffeur.isChauffeurStr(chauffeur);
                if (idChauffeur != "")
                {
                    //
                }
                else
                {
                    if (serviceChauffeur.updateChauffeur(chauffeur))
                    {
                        this.initialiseGridChauffeur();


                    }
                    else
                    {
                        //
                    }
                }
            }
            #endregion
        }
        #endregion

        #region itineraire
        /*
        private void divIndicationItineraire(string str, string color)
        {
            if (str != "" && color != "")
            {
                indicationItineraire.Style.Add("font-size", "14px");
                indicationItineraire.Style.Add("color", color);
                indicationItineraire.InnerHtml = str;
            }
            else
            {
                indicationChauffeur.InnerHtml = "";
            }
        }

        private void initialiseGridItineraireLicence()
        {
            serviceItineraire.insertToGridItineraireLicence(gvItineraireLicence, "itineraire.idItineraire", "itineraire.idItineraire", "", hfNumLicence.Value);
        }

        private void initialiseGridItineraire()
        {
            serviceItineraire.insertToGridItineraireAll(gvItineraire, ddlTriItineraire.SelectedValue, ddlTriItineraire.SelectedValue, TextRechercheItineraire.Text);
        }

        */
        #endregion

        #region individu
        private void initialiseFormulaireIndividu()
        {
            serviceEtatCivil.loadDddlEtatCivil(ddlCiviliteIndividu);
            try
            {
                ddlCiviliteIndividu.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            TextNomClient.Text = "";
            TextPrenom.Text = "";
            TextCinClient.Text = "";
            TextMailIndividu.Text = "";
            TextDateNaissanceIndividu.Text = "";
            TextLieuNaissanceIndividu.Text = "";
            TextProfessionIndividu.Text = "";
            TextTelephoneFixeClient.Text = "";
            TextTelephoneMobile.Text = "";
            this.loadDdlProvinceIndividu();
            TextAdresseClient.Text = "";
            TextQuartierIndividu.Text = "";

            this.divIndicationVehicule("", "");
        }

        private void initialiseDdlArrondissementIndividu()
        {
            if (ddlArrondissementIndividu.Items.Count > 1)
            {
                ddlArrondissementIndividu_RequiredFieldValidator.ValidationGroup = "groupeIndividu";
            }
            else
            {
                ddlArrondissementIndividu_RequiredFieldValidator.ValidationGroup = "gNull";
            }
        }

        private void loadDdlProvinceIndividu()
        {
            serviceProvince.loadDddlProvince(ddlProvinceIndividu);
            serviceRegion.loadDddlRegionProvince(ddlRegionIndividu, ddlProvinceIndividu.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictIndividu, ddlRegionIndividu.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneIndividu, ddlDistrictIndividu.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementIndividu, ddlCommuneIndividu.SelectedValue);
            this.initialiseDdlArrondissementIndividu();
        }

        private void loadDdlRegionIndividu()
        {
            serviceRegion.loadDddlRegionProvince(ddlRegionIndividu, ddlProvinceIndividu.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictIndividu, ddlRegionIndividu.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneIndividu, ddlDistrictIndividu.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementIndividu, ddlCommuneIndividu.SelectedValue);
            this.initialiseDdlArrondissementIndividu();
        }

        private void loadDdlDistrictIndividu()
        {
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictIndividu, ddlRegionIndividu.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneIndividu, ddlDistrictIndividu.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementIndividu, ddlCommuneIndividu.SelectedValue);
            this.initialiseDdlArrondissementIndividu();
        }

        private void loadDdlCommuneIndividu()
        {
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneIndividu, ddlDistrictIndividu.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementIndividu, ddlCommuneIndividu.SelectedValue);
            this.initialiseDdlArrondissementIndividu();
        }

        private void loadDdlArrondissementIndividu()
        {
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementIndividu, ddlCommuneIndividu.SelectedValue);
            this.initialiseDdlArrondissementIndividu();
        }

        private void afficheQuartierIndividu(string numQuartier)
        {
            #region delcaration
            crlQuartier quartier = null;
            crlCommune commune = null;
            crlDistrict district = null;
            crlRegion region = null;
            #endregion

            #region implementation
            if (numQuartier != "")
            {
                quartier = serviceQuartier.selectQuartier(numQuartier);
                if (quartier != null)
                {
                    commune = serviceCommune.selectCommune(quartier.NumCommune);
                    if (commune != null)
                    {
                        district = serviceDistrict.selectDistrict(commune.NumDistrict);
                        if (district != null)
                        {
                            region = serviceRegion.selectRegion(district.NomRegion);
                            if (region != null)
                            {
                                try
                                {
                                    ddlProvinceIndividu.SelectedValue = region.NomProvince;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlRegionIndividu();
                                try
                                {
                                    ddlRegionIndividu.SelectedValue = region.NomRegion;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlDistrictIndividu();
                                try
                                {
                                    ddlDistrictIndividu.SelectedValue = district.NumDistrict;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlCommuneIndividu();
                                try
                                {
                                    ddlCommuneIndividu.SelectedValue = commune.NumCommune;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlArrondissementIndividu();
                                try
                                {
                                    ddlArrondissementIndividu.SelectedValue = quartier.NumArrondissement;
                                }
                                catch (Exception)
                                {
                                }
                                TextQuartierIndividu.Text = quartier.Quartier;
                            }
                            else
                            {
                                this.loadDdlProvinceIndividu();
                            }
                        }
                        else
                        {
                            this.loadDdlProvinceIndividu();
                        }
                    }
                    else
                    {
                        this.loadDdlProvinceIndividu();
                    }
                }
                else
                {
                    this.loadDdlProvinceIndividu();
                }
            }
            #endregion
        }
        #endregion

        #region societe

        private void initialiseFormulaireSociete()
        {
            serviceEtatCivil.loadDddlEtatCivil(ddlCiviliteRespSociete);

            TextAdresseSociete.Text = "";
            TextTelephoneSociete.Text = "";
            TextMailSociete.Text = "";
            TextMobileSociete.Text = "";
            TextSecteurSociete.Text = "";
            TextNomSociete.Text = "";
            TextQuartierSociete.Text = "";

            this.initialiseFormulaireResponsableSociete();

            this.loadDdlProvinceSociete();


            cbReductionUS.Checked = false;
            this.initialiseCB();

            btnNouveauRespSociete.Enabled = false;

            this.divIndicationVehicule("", "Red");
        }

        private void initialiseFormulaireResponsableSociete()
        {
            #region implementation
            TextAdresseRespSociete.Text = "";
            TextCinRespSociete.Text = "";
            TextFixeRespSociete.Text = "";
            TextMailRespSociete.Text = "";
            TextMobileRespSociete.Text = "";
            TextNomResponsableSociete.Text = "";
            TextPrenomRespSociete.Text = "";
            TextQuartierRespSociete.Text = "";

            this.loadDdlProvinceRespSociete();

            serviceEtatCivil.loadDddlEtatCivil(ddlCiviliteRespSociete);
            try
            {
                ddlCiviliteRespSociete.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            #endregion
        }



        private void initialiseCB()
        {
            if (cbReductionUS.Checked)
            {
                cbReductionUS.Text = "Accepter";
            }
            else
            {
                cbReductionUS.Text = "Refuser";
            }
        }

        private void initialiseDdlArrondissementSociete()
        {
            if (ddlArrondissementSociete.Items.Count > 1)
            {
                ddlArrondissementSociete_RequiredFieldValidator.ValidationGroup = "groupeSociete";
            }
            else
            {
                ddlArrondissementSociete_RequiredFieldValidator.ValidationGroup = "gNull";
            }
        }

        private void loadDdlProvinceSociete()
        {
            serviceProvince.loadDddlProvince(ddlProvinceSociete);
            serviceRegion.loadDddlRegionProvince(ddlRegionSociete, ddlProvinceSociete.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictSociete, ddlRegionSociete.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneSociete, ddlDistrictSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementSociete, ddlCommuneSociete.SelectedValue);
            this.initialiseDdlArrondissementSociete();
        }

        private void loadDdlRegionSociete()
        {
            serviceRegion.loadDddlRegionProvince(ddlRegionSociete, ddlProvinceSociete.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictSociete, ddlRegionSociete.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneSociete, ddlDistrictSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementSociete, ddlCommuneSociete.SelectedValue);
            this.initialiseDdlArrondissementSociete();
        }

        private void loadDdlDistrictSociete()
        {
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictSociete, ddlRegionSociete.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneSociete, ddlDistrictSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementSociete, ddlCommuneSociete.SelectedValue);
            this.initialiseDdlArrondissementSociete();
        }

        private void loadDdlCommuneSociete()
        {
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneSociete, ddlDistrictSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementSociete, ddlCommuneSociete.SelectedValue);
            this.initialiseDdlArrondissementSociete();
        }

        private void loadDdlArrondissementSociete()
        {
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementSociete, ddlCommuneSociete.SelectedValue);
            this.initialiseDdlArrondissementSociete();
        }

        private void afficheQuartierSociete(string numQuartier)
        {
            #region delcaration
            crlQuartier quartier = null;
            crlCommune commune = null;
            crlDistrict district = null;
            crlRegion region = null;
            #endregion

            #region implementation
            if (numQuartier != "")
            {
                quartier = serviceQuartier.selectQuartier(numQuartier);
                if (quartier != null)
                {
                    commune = serviceCommune.selectCommune(quartier.NumCommune);
                    if (commune != null)
                    {
                        district = serviceDistrict.selectDistrict(commune.NumDistrict);
                        if (district != null)
                        {
                            region = serviceRegion.selectRegion(district.NomRegion);
                            if (region != null)
                            {
                                try
                                {
                                    ddlProvinceSociete.SelectedValue = region.NomProvince;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlRegionSociete();
                                try
                                {
                                    ddlRegionSociete.SelectedValue = region.NomRegion;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlDistrictSociete();
                                try
                                {
                                    ddlDistrictSociete.SelectedValue = district.NumDistrict;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlCommuneSociete();
                                try
                                {
                                    ddlCommuneSociete.SelectedValue = commune.NumCommune;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlArrondissementSociete();
                                try
                                {
                                    ddlArrondissementSociete.SelectedValue = quartier.NumArrondissement;
                                }
                                catch (Exception)
                                {
                                }
                                TextQuartierSociete.Text = quartier.Quartier;
                            }
                            else
                            {
                                this.loadDdlProvinceSociete();
                            }
                        }
                        else
                        {
                            this.loadDdlProvinceSociete();
                        }
                    }
                    else
                    {
                        this.loadDdlProvinceSociete();
                    }
                }
                else
                {
                    this.loadDdlProvinceSociete();
                }
            }
            #endregion
        }




        private void initialiseDdlArrondissementRespSociete()
        {
            if (ddlArrondissementRespSociete.Items.Count > 1)
            {
                ddlArrondissementRespSociete_RequiredFieldValidator.ValidationGroup = "groupeSociete";
            }
            else
            {
                ddlArrondissementRespSociete_RequiredFieldValidator.ValidationGroup = "gNull";
            }
        }

        private void loadDdlProvinceRespSociete()
        {
            serviceProvince.loadDddlProvince(ddlprovinceRespSociete);
            serviceRegion.loadDddlRegionProvince(ddlRegionRespSociete, ddlprovinceRespSociete.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictRespSociete, ddlRegionRespSociete.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespSociete, ddlDistrictRespSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespSociete, ddlCommuneRespSociete.SelectedValue);
            this.initialiseDdlArrondissementRespSociete();
        }

        private void loadDdlRegionRespSociete()
        {
            serviceRegion.loadDddlRegionProvince(ddlRegionRespSociete, ddlprovinceRespSociete.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictRespSociete, ddlRegionRespSociete.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespSociete, ddlDistrictRespSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespSociete, ddlCommuneRespSociete.SelectedValue);
            this.initialiseDdlArrondissementRespSociete();
        }

        private void loadDdlDistrictRespSociete()
        {
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictRespSociete, ddlRegionRespSociete.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespSociete, ddlDistrictRespSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespSociete, ddlCommuneRespSociete.SelectedValue);
            this.initialiseDdlArrondissementRespSociete();
        }

        private void loadDdlCommuneRespSociete()
        {
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespSociete, ddlDistrictRespSociete.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespSociete, ddlCommuneRespSociete.SelectedValue);
            this.initialiseDdlArrondissementRespSociete();
        }

        private void loadDdlArrondissementRespSociete()
        {
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespSociete, ddlCommuneRespSociete.SelectedValue);
            this.initialiseDdlArrondissementRespSociete();
        }

        private void afficheQuartierRespSociete(string numQuartier)
        {
            #region delcaration
            crlQuartier quartier = null;
            crlCommune commune = null;
            crlDistrict district = null;
            crlRegion region = null;
            #endregion

            #region implementation
            if (numQuartier != "")
            {
                quartier = serviceQuartier.selectQuartier(numQuartier);
                if (quartier != null)
                {
                    commune = serviceCommune.selectCommune(quartier.NumCommune);
                    if (commune != null)
                    {
                        district = serviceDistrict.selectDistrict(commune.NumDistrict);
                        if (district != null)
                        {
                            region = serviceRegion.selectRegion(district.NomRegion);
                            if (region != null)
                            {
                                try
                                {
                                    ddlprovinceRespSociete.SelectedValue = region.NomProvince;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlRegionRespSociete();
                                try
                                {
                                    ddlRegionRespSociete.SelectedValue = region.NomRegion;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlDistrictRespSociete();
                                try
                                {
                                    ddlDistrictRespSociete.SelectedValue = district.NumDistrict;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlCommuneRespSociete();
                                try
                                {
                                    ddlCommuneRespSociete.SelectedValue = commune.NumCommune;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlArrondissementRespSociete();
                                try
                                {
                                    ddlArrondissementRespSociete.SelectedValue = quartier.NumArrondissement;
                                }
                                catch (Exception)
                                {
                                }
                                TextQuartierRespSociete.Text = quartier.Quartier;
                            }
                            else
                            {
                                this.loadDdlProvinceRespSociete();
                            }
                        }
                        else
                        {
                            this.loadDdlProvinceRespSociete();
                        }
                    }
                    else
                    {
                        this.loadDdlProvinceRespSociete();
                    }
                }
                else
                {
                    this.loadDdlProvinceRespSociete();
                }
            }
            #endregion
        }
        #endregion

        #region organisme
        private void initialiseFormulaireOrganisme()
        {
            TextAdresseOrganisme.Text = "";
            TextFixeOrganisme.Text = "";
            TextMailOrganisme.Text = "";
            TextMobileOrganisme.Text = "";
            TextNomOrganisme.Text = "";

            btnNouveauRespOrganisme.Enabled = false;

            this.initialiseFormulaireResponsableOrganisme();
            this.loadDdlProvinceOrganisme();


            this.divIndicationVehicule("", "Red");
        }

        private void initialiseFormulaireResponsableOrganisme()
        {
            TextAdresseRespOrganisme.Text = "";
            TextFixeRespOrganisme.Text = "";
            TextMailRespOrganisme.Text = "";
            TextMobileRespOrganisme.Text = "";
            TextNomRespOrganisme.Text = "";
            TextPrenomRespOrganisme.Text = "";
            TextQuartierRespOrganisme.Text = "";
            TextCinRespOrganisme.Text = "";

            this.loadDdlProvinceRespOrganisme();
            serviceEtatCivil.loadDddlEtatCivil(ddlCiviliteRespOrganisme);
            try
            {
                ddlCiviliteRespOrganisme.SelectedValue = "";
            }
            catch (Exception)
            {
            }
        }



        private void initialiseDdlArrondissementOrganisme()
        {
            if (ddlArrondissementOrganisme.Items.Count > 1)
            {
                ddlArrondissementOrganisme_RequiredFieldValidator.ValidationGroup = "groupeSociete";
            }
            else
            {
                ddlArrondissementOrganisme_RequiredFieldValidator.ValidationGroup = "gNull";
            }
        }

        private void loadDdlProvinceOrganisme()
        {
            serviceProvince.loadDddlProvince(ddlProvinceOrganisme);
            serviceRegion.loadDddlRegionProvince(ddlRegionOrganisme, ddlProvinceOrganisme.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictOrganisme, ddlRegionOrganisme.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneOrganisme, ddlDistrictOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementOrganisme, ddlCommuneOrganisme.SelectedValue);
            this.initialiseDdlArrondissementOrganisme();
        }

        private void loadDdlRegionOrganisme()
        {
            serviceRegion.loadDddlRegionProvince(ddlRegionOrganisme, ddlProvinceOrganisme.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictOrganisme, ddlRegionOrganisme.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneOrganisme, ddlDistrictOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementOrganisme, ddlCommuneOrganisme.SelectedValue);
            this.initialiseDdlArrondissementOrganisme();
        }

        private void loadDdlDistrictOrganisme()
        {
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictOrganisme, ddlRegionOrganisme.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneOrganisme, ddlDistrictOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementOrganisme, ddlCommuneOrganisme.SelectedValue);
            this.initialiseDdlArrondissementOrganisme();
        }

        private void loadDdlCommuneOrganisme()
        {
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneOrganisme, ddlDistrictOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementOrganisme, ddlCommuneOrganisme.SelectedValue);
            this.initialiseDdlArrondissementOrganisme();
        }

        private void loadDdlArrondissementOrganisme()
        {
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementOrganisme, ddlCommuneOrganisme.SelectedValue);
            this.initialiseDdlArrondissementOrganisme();
        }

        private void afficheQuartierOrganisme(string numQuartier)
        {
            #region delcaration
            crlQuartier quartier = null;
            crlCommune commune = null;
            crlDistrict district = null;
            crlRegion region = null;
            #endregion

            #region implementation
            if (numQuartier != "")
            {
                quartier = serviceQuartier.selectQuartier(numQuartier);
                if (quartier != null)
                {
                    commune = serviceCommune.selectCommune(quartier.NumCommune);
                    if (commune != null)
                    {
                        district = serviceDistrict.selectDistrict(commune.NumDistrict);
                        if (district != null)
                        {
                            region = serviceRegion.selectRegion(district.NomRegion);
                            if (region != null)
                            {
                                try
                                {
                                    ddlProvinceOrganisme.SelectedValue = region.NomProvince;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlRegionOrganisme();
                                try
                                {
                                    ddlRegionOrganisme.SelectedValue = region.NomRegion;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlDistrictOrganisme();
                                try
                                {
                                    ddlDistrictOrganisme.SelectedValue = district.NumDistrict;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlCommuneOrganisme();
                                try
                                {
                                    ddlCommuneOrganisme.SelectedValue = commune.NumCommune;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlArrondissementOrganisme();
                                try
                                {
                                    ddlArrondissementOrganisme.SelectedValue = quartier.NumArrondissement;
                                }
                                catch (Exception)
                                {
                                }
                                TextQuartierOrganisme.Text = quartier.Quartier;
                            }
                            else
                            {
                                this.loadDdlProvinceOrganisme();
                            }
                        }
                        else
                        {
                            this.loadDdlProvinceOrganisme();
                        }
                    }
                    else
                    {
                        this.loadDdlProvinceOrganisme();
                    }
                }
                else
                {
                    this.loadDdlProvinceOrganisme();
                }
            }
            #endregion
        }




        private void initialiseDdlArrondissementRespOrganisme()
        {
            if (ddlArrondissementRespOrganisme.Items.Count > 1)
            {
                ddlArrondissementRespOrganisme_RequiredFieldValidator.ValidationGroup = "groupeSociete";
            }
            else
            {
                ddlArrondissementRespOrganisme_RequiredFieldValidator.ValidationGroup = "gNull";
            }
        }

        private void loadDdlProvinceRespOrganisme()
        {
            serviceProvince.loadDddlProvince(ddlprovinceRespOrganisme);
            serviceRegion.loadDddlRegionProvince(ddlRegionRespOrganisme, ddlprovinceRespOrganisme.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictRespOrganisme, ddlRegionRespOrganisme.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespOrganisme, ddlDistrictRespOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespOrganisme, ddlCommuneRespOrganisme.SelectedValue);
            this.initialiseDdlArrondissementRespOrganisme();
        }

        private void loadDdlRegionRespOrganisme()
        {
            serviceRegion.loadDddlRegionProvince(ddlRegionRespOrganisme, ddlprovinceRespOrganisme.SelectedValue);
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictRespOrganisme, ddlRegionRespOrganisme.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespOrganisme, ddlDistrictRespOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespOrganisme, ddlCommuneRespOrganisme.SelectedValue);
            this.initialiseDdlArrondissementRespOrganisme();
        }

        private void loadDdlDistrictRespOrganisme()
        {
            serviceDistrict.loadDddlDistrictRegion(ddlDistrictRespOrganisme, ddlRegionRespOrganisme.SelectedValue);
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespOrganisme, ddlDistrictRespOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespOrganisme, ddlCommuneRespOrganisme.SelectedValue);
            this.initialiseDdlArrondissementRespOrganisme();
        }

        private void loadDdlCommuneRespOrganisme()
        {
            serviceCommune.loadDddlCommuneDistrict(ddlCommuneRespOrganisme, ddlDistrictRespOrganisme.SelectedValue);
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespOrganisme, ddlCommuneRespOrganisme.SelectedValue);
            this.initialiseDdlArrondissementRespOrganisme();
        }

        private void loadDdlArrondissementRespOrganisme()
        {
            serviceArrondissement.loadDddlArrondissementCommune(ddlArrondissementRespOrganisme, ddlCommuneRespOrganisme.SelectedValue);
            this.initialiseDdlArrondissementRespOrganisme();
        }

        private void afficheQuartierRespOrganisme(string numQuartier)
        {
            #region delcaration
            crlQuartier quartier = null;
            crlCommune commune = null;
            crlDistrict district = null;
            crlRegion region = null;
            #endregion

            #region implementation
            if (numQuartier != "")
            {
                quartier = serviceQuartier.selectQuartier(numQuartier);
                if (quartier != null)
                {
                    commune = serviceCommune.selectCommune(quartier.NumCommune);
                    if (commune != null)
                    {
                        district = serviceDistrict.selectDistrict(commune.NumDistrict);
                        if (district != null)
                        {
                            region = serviceRegion.selectRegion(district.NomRegion);
                            if (region != null)
                            {
                                try
                                {
                                    ddlprovinceRespOrganisme.SelectedValue = region.NomProvince;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlRegionRespOrganisme();
                                try
                                {
                                    ddlRegionRespOrganisme.SelectedValue = region.NomRegion;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlDistrictRespOrganisme();
                                try
                                {
                                    ddlDistrictRespOrganisme.SelectedValue = district.NumDistrict;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlCommuneRespOrganisme();
                                try
                                {
                                    ddlCommuneRespOrganisme.SelectedValue = commune.NumCommune;
                                }
                                catch (Exception)
                                {
                                }
                                this.loadDdlArrondissementRespOrganisme();
                                try
                                {
                                    ddlArrondissementRespOrganisme.SelectedValue = quartier.NumArrondissement;
                                }
                                catch (Exception)
                                {
                                }
                                TextQuartierRespOrganisme.Text = quartier.Quartier;
                            }
                            else
                            {
                                this.loadDdlProvinceRespOrganisme();
                            }
                        }
                        else
                        {
                            this.loadDdlProvinceRespOrganisme();
                        }
                    }
                    else
                    {
                        this.loadDdlProvinceRespOrganisme();
                    }
                }
                else
                {
                    this.loadDdlProvinceRespOrganisme();
                }
            }
            #endregion
        }
        #endregion

        #endregion

        #region event

        #region proprietaire
        protected void btnProprietaireListe_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireProprietaireAll();
            Panel_FormulaireProprietaire.CssClass = "PanneauAction";
            Panel_FormulaireProprietaire.Visible = true;
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Panel_FormulaireProprietaire.Visible = false;
            this.initialiseFormulaireProprietaireAll();
        }

        protected void RadioListeProprietaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseIndividu();
            this.initialiseOrganisme();
            this.initialiseSociete();
            this.initialiseFormulaireProprietaire();
        }

        protected void ddlTriProprietaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridProprietaire();
        }

        protected void btnRechercherProprietaire_Click(object sender, EventArgs e)
        {
            this.initialiseGridProprietaire();
        }

        protected void gvProprietaire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProprietaire.PageIndex = e.NewPageIndex;
            this.initialiseGridProprietaire();
        }

        protected void gvProprietaire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheProprietaire(e.CommandArgument.ToString());
            }
        }

        protected void btnNouveauProprietaire_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireProprietaireAll();
        }

        protected void btnValiderProprietaire_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProprietaire proprietaire = null;
            #endregion

            #region implementation
            if (hfNumProprietaireTemp.Value != null)
            {
                proprietaire = serviceProprietaire.selectProprietaire(hfNumProprietaireTemp.Value);
                if (proprietaire != null)
                {
                    hfNumProprietaire.Value = proprietaire.NumProprietaire;

                    if (proprietaire.Individu != null)
                    {
                        btnProprietaire.Text = proprietaire.Individu.PrenomIndividu + " " + proprietaire.Individu.NomIndividu;
                    }
                    if (proprietaire.societe != null)
                    {
                        btnProprietaire.Text = proprietaire.societe.NomSociete;
                    }
                    if (proprietaire.organisme != null)
                    {
                        btnProprietaire.Text = proprietaire.organisme.NomOrganisme;
                    }

                    this.initialiseDdlVehicule();
                    this.initialiseFormulaireProprietaireAll();
                    this.initialiseFormulaireVehiculeLicenceParametre();
                    Panel_FormulaireProprietaire.Visible = false;
                    this.initialiseDdlVehicule();
                    //this.initialiseGridVehicule();
                }
            }
            #endregion
        }

        protected void btnAjouterProprietaire_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProprietaire proprietaire = null;
            #endregion

            #region implementation
            proprietaire = new crlProprietaire();
            this.insertToObjetProprietaire(proprietaire);
            this.insertProprietaire(proprietaire);
            #endregion
        }

        protected void btnModifierProprietaire_Click(object sender, EventArgs e)
        {
            #region declaration
            crlProprietaire proprietaire = null;
            #endregion

            #region implementation
            if (hfNumProprietaireTemp.Value != "")
            {
                proprietaire = serviceProprietaire.selectProprietaire(hfNumProprietaireTemp.Value);
                if (proprietaire != null)
                {
                    this.insertToObjetProprietaire(proprietaire);
                    this.upDateProprietaire(proprietaire);
                }
            }
            #endregion
        }

        protected void btnProprietaire_Click(object sender, EventArgs e)
        {
            #region implementation
            if (hfNumProprietaire.Value != "")
            {
                this.afficheProprietaire(hfNumProprietaire.Value);
                Panel_FormulaireProprietaire.CssClass = "PanneauAction";
                Panel_FormulaireProprietaire.Visible = true;
            }
            #endregion
        }
        #endregion

        #region vehicule licence parametre
        protected void btnEnregistrer_Click(object sender, EventArgs e)
        {
            #region declaration
            crlVehicule vehicule = null;
            crlLicence licence = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumProprietaire.Value != "")
            {
                vehicule = new crlVehicule();
                licence = new crlLicence();

                this.insertToObjetVehicule(vehicule);
                this.insertToObjetLicence(licence);

                this.insertVehiculeLicenceParametre(vehicule, licence);
                this.initialiseDdlVehicule();
                //this.initialiseGridVehicule();
            }
            else
            {
                strIndication = "Sélectionner un propriétaire avant d'enregistrer!";
                this.divIndicationVehicule(strIndication, "Red");
            }
            #endregion
        }

        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlLicence licence = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumLicence.Value != "")
            {
                licence = serviceLicence.selectLicence(hfNumLicence.Value);
                this.insertToObjetLicence(licence);
                if (licence.vehicule != null)
                {
                    this.insertToObjetVehicule(licence.vehicule);

                    this.upDateVehiculeLicenceParametre(licence.vehicule, licence);
                    this.initialiseDdlVehicule();

                }
            }
            else
            {
                strIndication = "Sélectionner un véhicule avant de modifier!";
                this.divIndicationVehicule(strIndication, "Red");
            }
            #endregion
        }

        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireVehiculeLicenceParametre();
            try
            {
                ddlListeVehicule.SelectedValue = "";
            }
            catch (Exception)
            {
            }
        }

        /*
        protected void btnRechercheVehicule_Click(object sender, EventArgs e)
        {
            this.initialiseGridVehicule();
        }
        protected void ddlTriVehicule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridVehicule();
        }
        protected void gvVehicule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehicule.PageIndex = e.NewPageIndex;
            this.initialiseGridVehicule();
        }
        protected void gvVehicule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheVehiculeLicenceParametre(e.CommandArgument.ToString());
            }
        }
         * */
        #endregion

        #region chauffeur
        protected void btnRechercheChauffeur_Click(object sender, EventArgs e)
        {
            this.initialiseGridChauffeur();
        }
        protected void ddlTriChauffeur_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridChauffeur();
        }
        protected void gvChauffeur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChauffeur.PageIndex = e.NewPageIndex;
            this.initialiseGridChauffeur();
        }
        protected void gvChauffeur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheChauffeur(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                this.serviceChauffeur.deleteAssoVehiculeChauffeur(hfNumVehicule.Value, e.CommandArgument.ToString());
                this.initialiseGridChauffeur();
                this.initialiseFormulaireChauffeur();
            }
        }

        protected void btnInsererChauffeur_Click(object sender, EventArgs e)
        {
            #region declaration
            crlChauffeur chauffeur = null;
            #endregion

            #region implementation
            if (this.testImageChauffeur())
            {
                chauffeur = new crlChauffeur();
                this.insertToObjetChauffeur(chauffeur);
                this.insertChauffeur(chauffeur);
            }
            else
            {
                this.divIndicationChauffeur("Image en jpg ou png seulement!", "Red");
            }
            #endregion
        }

        protected void btnModiffierChauffeur_Click(object sender, EventArgs e)
        {
            #region declaration
            crlChauffeur chauffeur = null;
            string fileName = "";
            string[] tableFileName;
            string urlSaving = "";
            #endregion

            #region implementation
            chauffeur = serviceChauffeur.selectChauffeur(hfIdChauffeur.Value);

            if (chauffeur != null)
            {
                this.insertToObjetChauffeur(chauffeur);

                if (FileUpload_Chauffeur.HasFile)
                {
                    fileName = FileUpload_Chauffeur.FileName;

                    tableFileName = fileName.Split('.');
                    if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg"))
                    {
                        urlSaving = this.urlSavingImageChauffeur(chauffeur.idChauffeur, "jpg");

                        FileUpload_Chauffeur.SaveAs(urlSaving);

                        chauffeur.ImageChauffeur = chauffeur.idChauffeur.Replace('/', '_') + ".jpg";
                    }
                    else if (tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                    {
                        urlSaving = this.urlSavingImageChauffeur(chauffeur.idChauffeur, "png");

                        FileUpload_Chauffeur.SaveAs(urlSaving);

                        chauffeur.ImageChauffeur = chauffeur.idChauffeur.Replace('/', '_') + ".png";
                    }
                }

                this.updateChauffeur(chauffeur);
            }
            #endregion
        }

        protected void btnNouveauChauffeur_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireChauffeur();
        }
        #endregion

        #region itineraire
        /*
        protected void gvItineraireLicence_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region implementation
            if (e.CommandName.Equals("deleteV"))
            {
                if (hfNumLicence.Value != "")
                {
                    serviceLicence.deleteAssoItineraireLicence(hfNumLicence.Value, e.CommandArgument.ToString());
                    this.initialiseGridItineraireLicence();
                }
            }
            #endregion
        }

        protected void ddlTriItineraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridItineraire();
        }
        protected void btnRechercheItineraire_Click(object sender, EventArgs e)
        {
            this.initialiseGridItineraire();
        }
        protected void gvItineraire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItineraire.PageIndex = e.NewPageIndex;
            this.initialiseGridItineraire();
        }
        protected void gvItineraire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                if (hfNumLicence.Value != "")
                {
                    serviceLicence.insertAssoItineraireLicence(hfNumLicence.Value, e.CommandArgument.ToString());
                    this.initialiseGridItineraireLicence();
                }
                else
                {
                    //
                }
            }
            #endregion
        }
         * */
        #endregion

        #region individu
        protected void ddlProvinceIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlRegionIndividu();
        }
        protected void ddlRegionIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlDistrictIndividu();
        }
        protected void ddlDistrictIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlCommuneIndividu();
        }
        protected void ddlCommuneIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlArrondissementIndividu();
        }
        #endregion

        #region societe
        protected void btnNouveauRespSociete_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireResponsableSociete();
        }
        protected void cbReductionUS_CheckedChanged(object sender, EventArgs e)
        {
            this.initialiseCB();
        }

        protected void ddlProvinceSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlRegionSociete();
        }
        protected void ddlRegionSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlDistrictSociete();
        }
        protected void ddlDistrictSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlCommuneSociete();
        }
        protected void ddlCommuneSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlArrondissementSociete();
        }


        protected void ddlprovinceRespSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlRegionRespSociete();
        }
        protected void ddlRegionRespSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlDistrictRespSociete();
        }
        protected void ddlDistrictRespSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlCommuneRespSociete();
        }
        protected void ddlCommuneRespSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlArrondissementRespSociete();
        }
        #endregion

        #region organisme
        protected void btnNouveauRespOrganisme_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireResponsableOrganisme();
        }


        protected void ddlProvinceOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlRegionOrganisme();
        }
        protected void ddlRegionOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlDistrictOrganisme();
        }
        protected void ddlDistrictOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlCommuneOrganisme();
        }
        protected void ddlCommuneOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlArrondissementOrganisme();
        }


        protected void ddlprovinceRespOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlRegionRespOrganisme();
        }
        protected void ddlRegionRespOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlDistrictRespOrganisme();
        }
        protected void ddlDistrictRespOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlCommuneRespOrganisme();
        }
        protected void ddlCommuneRespOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadDdlArrondissementRespOrganisme();
        }
        #endregion

        #endregion

        protected void ddlListeVehicule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlListeVehicule.SelectedValue != "")
            {
                this.afficheVehiculeLicenceParametre(ddlListeVehicule.SelectedValue);
            }
            else
            {
                this.initialiseFormulaireVehiculeLicenceParametre();
            }
        }

        protected void ddlDebutItineraire1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceItineraire.loadDdlItineraireVille2(ddlFinItineraire1, ddlDebutItineraire1.SelectedValue);
            this.divIndicationVehicule("", "Black");
        }
        protected void ddlFinItineraire1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divIndicationVehicule("", "Black");
        }
        protected void ddlDebutItineraire2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serviceItineraire.loadDdlItineraireVille2(ddlFinItineraire2, ddlDebutItineraire2.SelectedValue);
            this.divIndicationVehicule("", "Black");
        }
        protected void ddlFinItineraire2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divIndicationVehicule("", "Black");
        }
    }
}