using AppRessources.Ressources;
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
    public partial class Abonnement : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAbonnement serviceAbonnement = null;
        IntfDalBillet serviceBillet = null;
        IntfDalVille serviceVille = null;
        IntfDalTrajet serviceTrajet = null;
        IntfDalCalculPrixBillet serviceCalculPrixBillet = null;
        IntfDalSociete serviceSociete = null;
        IntfDalOrganisme serviceOrganisme = null;
        IntfDalClient serviceClient = null;
        IntfDalLien serviceLien = null;
        IntfDalProvince serviceProvince = null;
        IntfDalRegion serviceRegion = null;
        IntfDalDistrict serviceDistrict = null;
        IntfDalCommune serviceCommune = null;
        IntfDalQuartier serviceQuartier = null;
        IntfDalArrondissement serviceArrondissement = null;
        IntfDalIndividu serviceIndividu = null;
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
            serviceAbonnement = new ImplDalAbonnement();
            serviceBillet = new ImplDalBillet();
            serviceVille = new ImplDalVille();
            serviceTrajet = new ImplDalTrajet();
            serviceCalculPrixBillet = new ImplDalCalculPrixBillet();
            serviceSociete = new ImplDalSociete();
            serviceOrganisme = new ImplDalOrganisme();
            serviceClient = new ImplDalClient();
            serviceProvince = new ImplDalProvince();
            serviceRegion = new ImplDalRegion();
            serviceDistrict = new ImplDalDistrict();
            serviceCommune = new ImplDalCommune();
            serviceArrondissement = new ImplDalArrondissement();
            serviceQuartier = new ImplDalQuartier();
            serviceIndividu = new ImplDalIndividu();
            serviceEtatCivil = new ImplDalEtatCivil();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                hfNumAbonnement.Value = "";


                this.initialiseOrganisme();
                this.initialiseSociete();
                this.initialiseIndividu();

                this.initialiseFormulaire();
                this.initialiseGridAbonnement();

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "040"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void afficheAbonnement(string numAbonnement)
        {
            #region declaration
            crlAbonnement abonnement = null;
            string strConfirm = "";
            #endregion

            #region implemenation
            abonnement = serviceAbonnement.selectAbonnement(numAbonnement);

            if (abonnement != null)
            {


                hfNumAbonnement.Value = abonnement.NumAbonnement;

                if (abonnement.organisme != null)
                {
                    RadioListeAbonnement.SelectedValue = "organisme";
                    LabAbonnement.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.organisme.NomOrganisme;

                    TextAdresseOrganisme.Text = abonnement.organisme.AdresseOrganisme;
                    TextFixeOrganisme.Text = abonnement.organisme.TelephoneFixeOrganisme;
                    TextMailOrganisme.Text = abonnement.organisme.MailOrganisme;
                    TextMobileOrganisme.Text = abonnement.organisme.TelephoneMobileOrganisme;
                    TextNomOrganisme.Text = abonnement.organisme.NomOrganisme;
                    this.afficheQuartierOrganisme(abonnement.organisme.NumQuartier);

                    if (abonnement.organisme.individuResponsable != null)
                    {
                        TextAdresseRespOrganisme.Text = abonnement.organisme.individuResponsable.Adresse;
                        TextCinRespOrganisme.Text = abonnement.organisme.individuResponsable.CinIndividu;
                        TextFixeRespOrganisme.Text = abonnement.organisme.individuResponsable.TelephoneFixeIndividu;
                        TextMailRespOrganisme.Text = abonnement.organisme.individuResponsable.MailIndividu;
                        TextMobileRespOrganisme.Text = abonnement.organisme.individuResponsable.TelephoneMobileIndividu;
                        TextNomRespOrganisme.Text = abonnement.organisme.individuResponsable.NomIndividu;
                        TextPrenomRespOrganisme.Text = abonnement.organisme.individuResponsable.PrenomIndividu;
                        try
                        {
                            ddlCiviliteRespOrganisme.SelectedValue = abonnement.organisme.individuResponsable.CiviliteIndividu;
                        }
                        catch (Exception)
                        {
                        }
                        this.afficheQuartierRespOrganisme(abonnement.organisme.individuResponsable.NumQuartier);
                        hfNumResponsableOrganisme.Value = abonnement.organisme.individuResponsable.NumIndividu;
                    }

                    strConfirm = "Voulez vous vraiment modifier l'abonnement \nde l'organisme " + abonnement.organisme.NomOrganisme + "?";

                }
                else if (abonnement.societe != null)
                {
                    RadioListeAbonnement.SelectedValue = "societe";
                    LabAbonnement.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.societe.NomSociete;

                    TextAdresseSociete.Text = abonnement.societe.AdresseSociete;
                    TextMailSociete.Text = abonnement.societe.MailSociete;
                    TextMobileSociete.Text = abonnement.societe.TelephoneMobileSociete;
                    TextNomSociete.Text = abonnement.societe.NomSociete;
                    TextSecteurSociete.Text = abonnement.societe.SecteurActiviteSociete;
                    TextTelephoneSociete.Text = abonnement.societe.TelephoneFixeSociete;
                    this.afficheQuartierSociete(abonnement.societe.NumQuartier);
                    if (abonnement.societe.IsReductionUS == 1)
                    {
                        cbReductionUS.Checked = true;
                    }
                    else
                    {
                        cbReductionUS.Checked = false;
                    }

                    if (abonnement.societe.individuResponsable != null)
                    {
                        TextAdresseRespSociete.Text = abonnement.societe.individuResponsable.Adresse;
                        TextCinRespSociete.Text = abonnement.societe.individuResponsable.CinIndividu;
                        TextFixeRespSociete.Text = abonnement.societe.individuResponsable.TelephoneFixeIndividu;
                        TextMailRespSociete.Text = abonnement.societe.individuResponsable.MailIndividu;
                        TextMobileRespSociete.Text = abonnement.societe.individuResponsable.TelephoneMobileIndividu;
                        TextNomResponsableSociete.Text = abonnement.societe.individuResponsable.NomIndividu;
                        TextPrenomRespSociete.Text = abonnement.societe.individuResponsable.PrenomIndividu;
                        try
                        {
                            ddlCiviliteRespSociete.SelectedValue = abonnement.societe.individuResponsable.CiviliteIndividu;
                        }
                        catch (Exception)
                        {
                        }
                        this.afficheQuartierRespSociete(abonnement.societe.individuResponsable.NumQuartier);
                        hfNumResponsableSociete.Value = abonnement.societe.individuResponsable.NumIndividu;
                    }


                    strConfirm = "Voulez vous vraiment modifier l'abonnement \nde la société " + abonnement.societe.NomSociete + "?";
                }
                else if (abonnement.individu != null)
                {
                    RadioListeAbonnement.SelectedValue = "individu";
                    LabAbonnement.Text = "N°" + abonnement.NumAbonnement + " de " + abonnement.individu.NomIndividu + " " + abonnement.individu.PrenomIndividu;

                    TextAdresseClient.Text = abonnement.individu.Adresse;
                    TextCinClient.Text = abonnement.individu.CinIndividu;
                    TextNomClient.Text = abonnement.individu.NomIndividu;
                    TextPrenom.Text = abonnement.individu.PrenomIndividu;
                    TextTelephoneFixeClient.Text = abonnement.individu.TelephoneFixeIndividu;
                    TextTelephoneMobile.Text = abonnement.individu.TelephoneMobileIndividu;
                    if (abonnement.individu.DateNaissanceIndividu.Year > 1900)
                    {
                        TextDateNaissanceIndividu.Text = abonnement.individu.DateNaissanceIndividu.ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        TextDateNaissanceIndividu.Text = "";
                    }
                    TextLieuNaissanceIndividu.Text = abonnement.individu.LieuNaissanceIndividu;
                    TextProfessionIndividu.Text = abonnement.individu.Profession;
                    TextMailIndividu.Text = abonnement.individu.MailIndividu;
                    try
                    {
                        ddlCiviliteIndividu.SelectedValue = abonnement.individu.CiviliteIndividu;
                    }
                    catch (Exception)
                    {
                    }
                    this.afficheQuartierIndividu(abonnement.individu.NumQuartier);

                    strConfirm = "Voulez vous vraiment modifier l'abonnement \nde " + abonnement.individu.PrenomIndividu + " " + abonnement.individu.NomIndividu + "?";
                }


                this.initialiseSociete();
                this.initialiseOrganisme();
                this.initialiseIndividu();

                btnValider.Enabled = false;
                RadioListeAbonnement.Enabled = false;
                btnModifier.Enabled = true;

                btnNouveauRespOrganisme.Enabled = true;
                btnNouveauRespSociete.Enabled = true;

                ConfirmButtonExtender_btnModifier.ConfirmText = strConfirm;

            }
            #endregion
        }

        private void initialiseFormulaire()
        {
            this.initialiseFormulaireIndividu();
            this.initialiseFormulaireSociete();
            this.initialiseFormulaireOrganisme();

            RadioListeAbonnement.SelectedValue = "individu";

            LabAbonnement.Text = "";
            this.initialiseIndividu();
            this.initialiseOrganisme();
            this.initialiseSociete();

            hfNumAbonnement.Value = "";

            btnValider.Enabled = true;
            RadioListeAbonnement.Enabled = true;
            btnModifier.Enabled = false;

            this.divIndicationText("", "Red");

            ConfirmButtonExtender_btnModifier.ConfirmText = "";
        }

        private void initialiseGridAbonnement()
        {
            serviceAbonnement.insertToGridAbonnement(gvAbonnement, ddlTriAbonnement.SelectedValue, ddlTriAbonnement.SelectedValue, TextRechercheAbonnement.Text);
        }

        private void insertToObjet(crlAbonnement abonnement)
        {
            #region implementation

            if (abonnement != null)
            {
                abonnement.MatriculeAgent = agent.matriculeAgent;
                abonnement.agent = agent;

                if (RadioListeAbonnement.SelectedValue.Equals("individu"))
                {
                    if (abonnement.individu == null)
                    {
                        abonnement.individu = new crlIndividu();
                    }

                    abonnement.individu.CiviliteIndividu = ddlCiviliteIndividu.SelectedValue;
                    abonnement.individu.NomIndividu = TextNomClient.Text;
                    abonnement.individu.PrenomIndividu = TextPrenom.Text;
                    try
                    {
                        abonnement.individu.DateNaissanceIndividu = Convert.ToDateTime(TextDateNaissanceIndividu.Text);
                    }
                    catch (Exception)
                    {
                    }
                    abonnement.individu.LieuNaissanceIndividu = TextLieuNaissanceIndividu.Text;
                    abonnement.individu.CinIndividu = TextCinClient.Text;
                    abonnement.individu.MailIndividu = TextMailIndividu.Text;
                    abonnement.individu.Adresse = TextAdresseClient.Text;
                    abonnement.individu.TelephoneFixeIndividu = TextTelephoneFixeClient.Text;
                    abonnement.individu.TelephoneMobileIndividu = TextTelephoneMobile.Text;
                    abonnement.individu.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierIndividu.Text, ddlCommuneIndividu.SelectedValue, ddlArrondissementIndividu.SelectedValue, agent.agence.SigleAgence);
                    abonnement.individu.Profession = TextProfessionIndividu.Text;
                }
                else
                {
                    abonnement.individu = null;
                }

                if (RadioListeAbonnement.SelectedValue.Equals("societe"))
                {
                    if (abonnement.societe == null)
                    {
                        abonnement.societe = new crlSociete();
                    }

                    abonnement.societe.AdresseSociete = TextAdresseSociete.Text;
                    abonnement.societe.MailSociete = TextMailSociete.Text;
                    abonnement.societe.NomSociete = TextNomSociete.Text;
                    abonnement.societe.SecteurActiviteSociete = TextSecteurSociete.Text;
                    abonnement.societe.TelephoneFixeSociete = TextTelephoneSociete.Text;
                    abonnement.societe.TelephoneMobileSociete = TextMobileSociete.Text;
                    abonnement.societe.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierSociete.Text, ddlCommuneSociete.SelectedValue, ddlArrondissementSociete.SelectedValue, agent.agence.SigleAgence);
                    if (cbReductionUS.Checked)
                    {
                        abonnement.societe.IsReductionUS = 1;
                    }
                    else
                    {
                        abonnement.societe.IsReductionUS = 0;
                    }

                    if (abonnement.societe.individuResponsable == null)
                    {
                        abonnement.societe.individuResponsable = new crlIndividu();
                    }

                    abonnement.societe.individuResponsable.Adresse = TextAdresseRespSociete.Text;
                    abonnement.societe.individuResponsable.CinIndividu = TextCinRespSociete.Text;
                    abonnement.societe.individuResponsable.MailIndividu = TextMailRespSociete.Text;
                    abonnement.societe.individuResponsable.NomIndividu = TextNomResponsableSociete.Text;
                    abonnement.societe.individuResponsable.PrenomIndividu = TextPrenomRespSociete.Text;
                    abonnement.societe.individuResponsable.TelephoneFixeIndividu = TextFixeRespSociete.Text;
                    abonnement.societe.individuResponsable.TelephoneMobileIndividu = TextMobileRespSociete.Text;
                    abonnement.societe.individuResponsable.CiviliteIndividu = ddlCiviliteRespSociete.SelectedValue;
                    abonnement.societe.individuResponsable.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierRespSociete.Text, ddlCommuneRespSociete.SelectedValue, ddlArrondissementRespSociete.SelectedValue, agent.agence.SigleAgence);
                }
                else
                {
                    abonnement.societe = null;
                }

                if (RadioListeAbonnement.SelectedValue.Equals("organisme"))
                {
                    if (abonnement.organisme == null)
                    {
                        abonnement.organisme = new crlOrganisme();
                    }

                    abonnement.organisme.AdresseOrganisme = TextAdresseOrganisme.Text;
                    abonnement.organisme.MailOrganisme = TextMailOrganisme.Text;
                    abonnement.organisme.NomOrganisme = TextNomOrganisme.Text;
                    abonnement.organisme.TelephoneFixeOrganisme = TextFixeOrganisme.Text;
                    abonnement.organisme.TelephoneMobileOrganisme = TextMobileOrganisme.Text;
                    abonnement.organisme.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierOrganisme.Text, ddlCommuneOrganisme.SelectedValue, ddlArrondissementOrganisme.SelectedValue, agent.agence.SigleAgence);

                    if (abonnement.organisme.individuResponsable == null)
                    {
                        abonnement.organisme.individuResponsable = new crlIndividu();
                    }

                    abonnement.organisme.individuResponsable.Adresse = TextAdresseRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.CinIndividu = TextCinRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.MailIndividu = TextMailRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.NomIndividu = TextNomRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.PrenomIndividu = TextPrenomRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.TelephoneFixeIndividu = TextFixeRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.TelephoneMobileIndividu = TextMobileRespOrganisme.Text;
                    abonnement.organisme.individuResponsable.CiviliteIndividu = ddlCiviliteRespOrganisme.SelectedValue;
                    abonnement.organisme.individuResponsable.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierRespOrganisme.Text, ddlCommuneRespOrganisme.SelectedValue, ddlArrondissementRespOrganisme.SelectedValue, agent.agence.SigleAgence);
                }
                else
                {
                    abonnement.organisme = null;
                }
            }
            #endregion
        }

        private void initialiseErrorMessage()
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
        }



        private void initialiseSociete()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("societe"))
            {
                PanelSociete.Visible = true;
                btnValider.ValidationGroup = "groupeSociete";
                btnModifier.ValidationGroup = "groupeSociete";
            }
            else
            {
                PanelSociete.Visible = false;
            }
        }

        private void initialiseOrganisme()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("organisme"))
            {
                PanelOrganisme.Visible = true;
                btnModifier.ValidationGroup = "groupeOrganisme";
                btnValider.ValidationGroup = "groupeOrganisme";
            }
            else
            {
                PanelOrganisme.Visible = false;
            }
        }

        private void initialiseIndividu()
        {
            if (RadioListeAbonnement.SelectedValue.Equals("individu"))
            {
                PanelClient.Visible = true;
                btnValider.ValidationGroup = "groupeIndividu";
                btnModifier.ValidationGroup = "goupeIndividu";
            }
            else
            {
                PanelClient.Visible = false;
            }
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

        private void insertAbonnement()
        {
            #region declaration
            crlAbonnement abonnement = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumAbonnement.Value.Equals(""))
            {
                abonnement = new crlAbonnement();
                this.insertToObjet(abonnement);
                if (abonnement.individu != null)
                {
                    abonnement.NumIndividu = this.insertIndividu(abonnement.individu);
                }
                else if (abonnement.societe != null)
                {
                    abonnement.societe.NumIndividuResponsable = this.insertIndividu(abonnement.societe.individuResponsable);
                    if (abonnement.societe.NumIndividuResponsable != "")
                    {
                        abonnement.societe.NumSociete = serviceSociete.isSociete(abonnement.societe);
                        if (abonnement.societe.NumSociete.Equals(""))
                        {
                            abonnement.NumSociete = serviceSociete.insertSociete(abonnement.societe, agent.agence.SigleAgence);
                        }
                        else
                        {
                            if (serviceSociete.updateSociete(abonnement.societe))
                            {
                                abonnement.NumSociete = abonnement.societe.NumSociete;
                            }
                            else
                            {
                                abonnement.NumSociete = "";
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                    }
                }
                else if (abonnement.organisme != null)
                {
                    abonnement.organisme.NumIndividuResponsable = this.insertIndividu(abonnement.organisme.individuResponsable);
                    if (abonnement.organisme.NumIndividuResponsable != "")
                    {
                        abonnement.organisme.NumOrganisme = serviceOrganisme.isOrganisme(abonnement.organisme);
                        if (abonnement.organisme.NumOrganisme.Equals(""))
                        {
                            abonnement.NumOrganisme = serviceOrganisme.insertOrganisme(abonnement.organisme, agent.agence.SigleAgence);
                        }
                        else
                        {
                            if (serviceOrganisme.updateOrganisme(abonnement.organisme))
                            {
                                abonnement.NumOrganisme = abonnement.organisme.NumOrganisme;
                            }
                            else
                            {
                                abonnement.NumOrganisme = "";
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                    }
                }

                if (abonnement.NumOrganisme != "" || abonnement.NumSociete != "" || abonnement.NumIndividu != "")
                {

                    abonnement.NumAbonnement = serviceAbonnement.isAbonnement(abonnement);
                    if (abonnement.NumAbonnement.Equals(""))
                    {
                        abonnement.NumAbonnement = serviceAbonnement.insertAbonnement(abonnement);
                        if (abonnement.NumAbonnement.Equals(""))
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            this.divIndicationText(strIndication, "Red");
                        }
                        else
                        {
                            this.initialiseFormulaire();
                            this.initialiseGridAbonnement();

                            strIndication = "Abonnement bien enregistrer!<br/>";
                            if (abonnement.individu != null)
                            {
                                strIndication += "Client:" + abonnement.individu.PrenomIndividu + " " + abonnement.individu.NomIndividu;
                            }
                            else if (abonnement.organisme != null)
                            {
                                strIndication += "Organisme:" + abonnement.organisme.NomOrganisme;
                            }
                            else if (abonnement.societe != null)
                            {
                                strIndication += "Société:" + abonnement.societe.NomSociete;
                            }
                            this.divIndicationText(strIndication, "Black");
                        }
                    }
                    else
                    {
                        strIndication = "Information déjà enregistrer dans la base de données!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }

            }
            else
            {
                strIndication = "";
            }
            #endregion
        }

        private void updateAbonnement()
        {
            #region declaration
            crlAbonnement abonnement = null;
            string strIndication = "";
            bool isUpdateIndividu = false;
            string numSociete = "";
            bool isUpdateSociete = false;
            string numOrganisme = "";
            bool isUpdateOrganisme = false;
            string numAbonnement = "";
            #endregion

            #region implementation
            if (hfNumAbonnement.Value != "")
            {
                abonnement = serviceAbonnement.selectAbonnement(hfNumAbonnement.Value);
                if (abonnement != null)
                {
                    this.insertToObjet(abonnement);
                    if (abonnement.individu != null)
                    {
                        isUpdateIndividu = this.updateIndividu(abonnement.individu);
                    }
                    else if (abonnement.societe != null)
                    {
                        if (hfNumResponsableSociete.Value.Equals(""))
                        {
                            abonnement.societe.NumIndividuResponsable = this.insertIndividu(abonnement.societe.individuResponsable);
                            if (abonnement.societe.NumIndividuResponsable != "")
                            {
                                numSociete = serviceSociete.isSociete(abonnement.societe);
                                if (numSociete.Equals(""))
                                {
                                    isUpdateSociete = serviceSociete.updateSociete(abonnement.societe);
                                }
                                else
                                {
                                    strIndication = "Information déjà enregistrer dans la base de données!";
                                    this.divIndicationText(strIndication, "Red");
                                }
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant la modification!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                        else
                        {
                            if (this.updateIndividu(abonnement.societe.individuResponsable))
                            {
                                numSociete = serviceSociete.isSociete(abonnement.societe);
                                if (numSociete.Equals(""))
                                {
                                    isUpdateSociete = serviceSociete.updateSociete(abonnement.societe);
                                }
                                else
                                {
                                    strIndication = "Information déjà enregistrer dans la base de données!";
                                    this.divIndicationText(strIndication, "Red");
                                }
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant la modification!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }

                    }
                    else if (abonnement.organisme != null)
                    {
                        if (hfNumResponsableOrganisme.Value.Equals(""))
                        {
                            abonnement.organisme.NumIndividuResponsable = this.insertIndividu(abonnement.organisme.individuResponsable);
                            if (abonnement.organisme.NumIndividuResponsable != "")
                            {
                                numOrganisme = serviceOrganisme.isOrganisme(abonnement.organisme);
                                if (numOrganisme.Equals(""))
                                {
                                    isUpdateOrganisme = serviceOrganisme.updateOrganisme(abonnement.organisme);
                                }
                                else
                                {
                                    strIndication = "Information déjà enregistrer dans la base de données!";
                                    this.divIndicationText(strIndication, "Red");
                                }
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant la modification!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                        else
                        {
                            if (this.updateIndividu(abonnement.organisme.individuResponsable))
                            {
                                numOrganisme = serviceOrganisme.isOrganisme(abonnement.organisme);
                                if (numOrganisme.Equals(""))
                                {
                                    isUpdateOrganisme = serviceOrganisme.updateOrganisme(abonnement.organisme);
                                }
                                else
                                {
                                    strIndication = "Information déjà enregistrer dans la base de données!";
                                    this.divIndicationText(strIndication, "Red");
                                }
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant la modification!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                    }

                    if (isUpdateIndividu || isUpdateOrganisme || isUpdateSociete)
                    {
                        numAbonnement = serviceAbonnement.isAbonnement(abonnement);
                        if (numAbonnement.Equals(""))
                        {
                            if (serviceAbonnement.updateAbonnement(abonnement))
                            {
                                this.initialiseFormulaire();
                                this.initialiseGridAbonnement();

                                strIndication = "Abonnement bien modifier!<br/>";
                                if (abonnement.individu != null)
                                {
                                    strIndication += "Client:" + abonnement.individu.PrenomIndividu + " " + abonnement.individu.NomIndividu;
                                }
                                else if (abonnement.organisme != null)
                                {
                                    strIndication += "Organisme:" + abonnement.organisme.NomOrganisme;
                                }
                                else if (abonnement.societe != null)
                                {
                                    strIndication += "Société:" + abonnement.societe.NomSociete;
                                }
                                this.divIndicationText(strIndication, "Black");
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant la modification!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant la modification!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else
                {
                    strIndication = "Une erreur ce produit durant le chargement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                strIndication = "";
            }
            #endregion
        }

        #region insertUPdateIndividu
        private string insertIndividu(crlIndividu individu)
        {
            #region declaration
            string strNumIndividuTemp = "";
            string numIndividu = "";
            string strIndication = "";
            crlIndividu individuIndication = null;
            #endregion

            #region implementation
            if (individu != null)
            {
                if (individu.CinIndividu != "")
                {
                    individu.NumIndividu = serviceIndividu.isCINIndividu(individu);
                    if (individu.NumIndividu.Equals(""))
                    {
                        individu.NumIndividu = serviceIndividu.insertIndividu(individu, agent.agence.SigleAgence);
                        if (individu.NumIndividu != "")
                        {
                            numIndividu = individu.NumIndividu;
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                    else
                    {
                        strNumIndividuTemp = individu.NumIndividu;
                        individu.NumIndividu = "";
                        individu.NumIndividu = serviceIndividu.isAllIndividu(individu);
                        if (individu.NumIndividu.Equals(""))
                        {
                            strIndication = "CIN déjà enregistrer dans la base de données!<br/>";
                            individuIndication = serviceIndividu.selectIndividu(strNumIndividuTemp);
                            if (individuIndication != null)
                            {
                                strIndication += "Nom:" + individuIndication.NomIndividu + "<br/>";
                                strIndication += "Prénom:" + individuIndication.PrenomIndividu + "<br/>";
                            }
                            this.divIndicationText(strIndication, "Red");
                        }
                        else
                        {
                            if (serviceIndividu.updateIndividu(individu))
                            {
                                numIndividu = individu.NumIndividu;
                            }
                            else
                            {
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                    }
                }
                else
                {
                    individu.NumIndividu = serviceIndividu.isNotCINIndividu(individu);
                    if (individuIndication.NumIndividu.Equals(""))
                    {
                        individu.NumIndividu = serviceIndividu.insertIndividu(individu, agent.agence.SigleAgence);
                        if (individu.NumIndividu != "")
                        {
                            numIndividu = individu.NumIndividu;
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                    else
                    {
                        if (serviceIndividu.updateIndividu(individu))
                        {
                            numIndividu = individu.NumIndividu;
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                }
            }
            #endregion

            return numIndividu;
        }

        private bool updateIndividu(crlIndividu individu)
        {
            #region declaration
            bool isUpdate = false;
            string numIndividu = "";
            string strIndication = "";
            crlIndividu individuIndication = null;
            #endregion

            #region implementation
            if (individu != null)
            {
                if (individu.CinIndividu != "")
                {
                    numIndividu = serviceIndividu.isAllIndividu(individu);
                    if (numIndividu.Equals(""))
                    {
                        isUpdate = serviceIndividu.updateIndividu(individu);
                        if (!isUpdate)
                        {
                            strIndication = "Une erreur ce produit durant la modification!";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                    else
                    {
                        strIndication = "CIN déjà enregistrer dans la base de données!<br/>";
                        individuIndication = serviceIndividu.selectIndividu(numIndividu);
                        if (individuIndication != null)
                        {
                            strIndication += "Nom:" + individuIndication.NomIndividu + "<br/>";
                            strIndication += "Prénom:" + individuIndication.PrenomIndividu + "<br/>";
                        }
                        this.divIndicationText(strIndication, "Red");
                    }

                }
                else
                {
                    isUpdate = serviceIndividu.updateIndividu(individu);
                    if (!isUpdate)
                    {
                        strIndication = "Une erreur ce produit durant la modification!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
            }
            #endregion

            return isUpdate;
        }
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

            this.divIndicationText("", "Red");
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

            hfNumResponsableSociete.Value = "";
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


            this.divIndicationText("", "Red");
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

            hfNumResponsableOrganisme.Value = "";
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
        protected void gvAbonnement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAbonnement.PageIndex = e.NewPageIndex;
            this.initialiseGridAbonnement();
        }
        protected void gvAbonnement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                this.afficheAbonnement(e.CommandArgument.ToString());
                this.divIndicationText("", "Red");
            }
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region implementation
            this.insertAbonnement();
            #endregion
        }
        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }

        protected void ddlTriAbonnement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAbonnement();
        }
        protected void btnRechercherAbonnement_Click(object sender, EventArgs e)
        {
            this.initialiseGridAbonnement();
        }

        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region implementation
            this.updateAbonnement();
            #endregion
        }
        protected void RadioListeAbonnement_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseOrganisme();
            this.initialiseSociete();
            this.initialiseIndividu();
        }



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
    }
}