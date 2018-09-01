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

namespace AppWeb.ihmActeur.administrateur
{
    public partial class Societe : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalSociete serviceSociete = null;
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
            serviceSociete = new ImplDalSociete();
            serviceGeneral = new ImplDalGeneral();
            serviceIndividu = new ImplDalIndividu();
            serviceProvince = new ImplDalProvince();
            serviceRegion = new ImplDalRegion();
            serviceDistrict = new ImplDalDistrict();
            serviceCommune = new ImplDalCommune();
            serviceArrondissement = new ImplDalArrondissement();
            serviceQuartier = new ImplDalQuartier();
            serviceIndividu = new ImplDalIndividu();
            serviceEtatCivil = new ImplDalEtatCivil();
            #endregion

            #region
            if (!IsPostBack)
            {

                this.initialiseCB();
                this.initialiseGridSociete();


                this.initialiseFormulaire();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "031"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridSociete()
        {
            serviceSociete.insertToGridSociete(gvSociete, ddlTriSociete.SelectedValue, ddlTriSociete.SelectedValue, TextRechercheSociete.Text);
        }

        private void afficheSociete(string numSociete)
        {
            #region declaration
            crlSociete societe = null;
            #endregion

            #region implementation
            if (numSociete != "")
            {
                societe = serviceSociete.selectSociete(numSociete);
                if (societe != null)
                {
                    TextAdresseSociete.Text = societe.AdresseSociete;
                    TextMailSociete.Text = societe.MailSociete;
                    TextMobileSociete.Text = societe.TelephoneMobileSociete;
                    TextNomSociete.Text = societe.NomSociete;
                    TextSecteurSociete.Text = societe.SecteurActiviteSociete;
                    TextTelephoneSociete.Text = societe.TelephoneFixeSociete;
                    this.afficheQuartierSociete(societe.NumQuartier);


                    if (societe.IsReductionUS > 0)
                    {
                        cbReductionUS.Checked = true;
                    }
                    else
                    {
                        cbReductionUS.Checked = false;
                    }
                    if (societe.IsCheque > 0)
                    {
                        cbCheque.Checked = true;
                    }
                    else
                    {
                        cbCheque.Checked = false;
                    }
                    if (societe.IsBonCommande > 0)
                    {
                        cbBonCommande.Checked = true;
                    }
                    else
                    {
                        cbBonCommande.Checked = false;
                    }

                    this.initialiseCB();

                    hfNumSociete.Value = societe.NumSociete;
                    hfNomSociete.Value = societe.NomSociete;

                    if (societe.individuResponsable != null)
                    {
                        try
                        {
                            ddlCiviliteRespSociete.SelectedValue = societe.individuResponsable.CiviliteIndividu;
                        }
                        catch (Exception)
                        {
                        }
                        TextAdresseRespSociete.Text = societe.individuResponsable.Adresse;
                        TextNomResponsableSociete.Text = societe.individuResponsable.NomIndividu;
                        TextPrenomRespSociete.Text = societe.individuResponsable.PrenomIndividu;
                        TextCinRespSociete.Text = societe.individuResponsable.CinIndividu;
                        TextMailRespSociete.Text = societe.individuResponsable.MailIndividu;
                        TextFixeRespSociete.Text = societe.individuResponsable.TelephoneFixeIndividu;
                        TextMobileRespSociete.Text = societe.individuResponsable.TelephoneMobileIndividu;

                        this.afficheQuartierRespSociete(societe.individuResponsable.NumQuartier);
                        hfNumIndividuResponsable.Value = societe.NumIndividuResponsable;
                    }

                    btnValider.Enabled = false;
                    btnModifier.Enabled = true;
                    btnNouveauRespSociete.Enabled = true;

                    this.divIndicationText("", "Red");
                }
            }
            #endregion
        }

        private void insertToObjet(crlSociete societe)
        {
            #region Implementation
            if (societe != null)
            {
                societe.AdresseSociete = TextAdresseSociete.Text;
                societe.MailSociete = TextMailSociete.Text;
                societe.NomSociete = TextNomSociete.Text;
                societe.SecteurActiviteSociete = TextSecteurSociete.Text;
                societe.TelephoneFixeSociete = TextTelephoneSociete.Text;
                societe.TelephoneMobileSociete = TextMobileSociete.Text;

                if (cbReductionUS.Checked)
                {
                    societe.IsReductionUS = 1;
                }
                else
                {
                    societe.IsReductionUS = 0;
                }
                if (cbCheque.Checked)
                {
                    societe.IsCheque = 1;
                }
                else
                {
                    societe.IsCheque = 0;
                }
                if (cbBonCommande.Checked)
                {
                    societe.IsBonCommande = 1;
                }
                else
                {
                    societe.IsBonCommande = 0;
                }
                societe.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierSociete.Text, ddlCommuneSociete.SelectedValue, ddlArrondissementSociete.SelectedValue, agent.agence.SigleAgence);


                if (societe.individuResponsable == null)
                {
                    societe.individuResponsable = new crlIndividu();
                }

                societe.individuResponsable.Adresse = TextAdresseRespSociete.Text;
                societe.individuResponsable.CinIndividu = TextCinRespSociete.Text;
                societe.individuResponsable.CiviliteIndividu = ddlCiviliteRespSociete.SelectedValue;
                societe.individuResponsable.MailIndividu = TextMailRespSociete.Text;
                societe.individuResponsable.NomIndividu = TextNomResponsableSociete.Text;
                societe.individuResponsable.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierRespSociete.Text, ddlCommuneRespSociete.SelectedValue, ddlArrondissementRespSociete.SelectedValue, agent.agence.SigleAgence);
                societe.individuResponsable.PrenomIndividu = TextPrenomRespSociete.Text;
                societe.individuResponsable.TelephoneFixeIndividu = TextFixeRespSociete.Text;
                societe.individuResponsable.TelephoneMobileIndividu = TextMobileRespSociete.Text;
            }
            #endregion
        }

        private void initialiseErrorMessage()
        {
            TextAdresseRespSociete_RequiredFieldValidator.ErrorMessage = ReSociete.adresseResponsableSocieteNonVide;
            TextAdresseSociete_RequiredFieldValidator.ErrorMessage = ReSociete.adresseSocieteNonVide;
            TextCinRespSociete_RequiredFieldValidator.ErrorMessage = ReSociete.cinResponsableSocieteNonVide;
            TextMailRespSociete_RegularExpressionValidator.ErrorMessage = ReSociete.mailResponsableSocieteNonVide;
            TextMailSociete_RegularExpressionValidator.ErrorMessage = ReSociete.mailSocieteNonValide;
            TextNomResponsableSociete_RequiredFieldValidator.ErrorMessage = ReSociete.nomResponsableNonVide;
            TextNomSociete_RequiredFieldValidator.ErrorMessage = ReSociete.nomSocieteNonVide;
            TextSecteurSociete_RequiredFieldValidator.ErrorMessage = ReSociete.secteurNonVide;

        }

        private void initialiseFormulaire()
        {
            serviceEtatCivil.loadDddlEtatCivil(ddlCiviliteRespSociete);
            hfNumSociete.Value = "";
            hfNomSociete.Value = "";

            TextAdresseSociete.Text = "";
            TextTelephoneSociete.Text = "";
            TextMailSociete.Text = "";
            TextMobileSociete.Text = "";
            TextSecteurSociete.Text = "";
            TextNomSociete.Text = "";
            TextQuartierSociete.Text = "";

            this.initialiseFormulaireResponsable();

            this.loadDdlProvinceSociete();


            cbReductionUS.Checked = false;
            cbCheque.Checked = false;
            cbBonCommande.Checked = false;
            this.initialiseCB();

            btnValider.Enabled = true;
            btnModifier.Enabled = false;
            btnNouveauRespSociete.Enabled = false;

            this.divIndicationText("", "Red");
        }

        private void initialiseFormulaireResponsable()
        {
            #region implementation
            hfNumIndividuResponsable.Value = "";
            TextAdresseRespSociete.Text = "";
            TextCinRespSociete.Text = "";
            TextFixeRespSociete.Text = "";
            TextMailRespSociete.Text = "";
            TextMobileRespSociete.Text = "";
            TextNomResponsableSociete.Text = "";
            TextPrenomRespSociete.Text = "";
            TextQuartierRespSociete.Text = "";

            this.loadDdlProvinceRespSociete();

            try
            {
                ddlCiviliteRespSociete.SelectedValue = "";
            }
            catch (Exception)
            {
            }
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

        private void insertSociete()
        {
            #region declaration
            crlSociete societe = null;
            string strIndication = "";
            #endregion

            #region implementation
            societe = new crlSociete();
            this.insertToObjet(societe);

            if (societe.individuResponsable != null)
            {
                societe.individuResponsable.NumIndividu = serviceIndividu.isIndividu(societe.individuResponsable);

                if (societe.individuResponsable.NumIndividu != "")
                {
                    serviceIndividu.updateIndividu(societe.individuResponsable);
                }
                else
                {
                    societe.individuResponsable.NumIndividu = serviceIndividu.insertIndividu(societe.individuResponsable, agent.agence.SigleAgence);
                }

                if (societe.individuResponsable.NumIndividu != "")
                {
                    societe.NumIndividuResponsable = societe.individuResponsable.NumIndividu;
                    societe.NumSociete = serviceSociete.isSociete(societe);

                    if (societe.NumSociete != "")
                    {
                        strIndication = "Information déjà enregistrer dans la base de données!";
                        this.divIndicationText(strIndication, "Red");
                    }
                    else
                    {
                        societe.NumSociete = serviceSociete.insertSociete(societe, agent.agence.SigleAgence);
                        if (societe.NumSociete != "")
                        {
                            this.initialiseFormulaire();
                            this.initialiseGridSociete();

                            strIndication = societe.NomSociete + " bien enregistrer!";
                            this.divIndicationText(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                strIndication = "Une erreur chargement!";
                this.divIndicationText(strIndication, "Red");
            }
            #endregion
        }

        private void updateSociete()
        {
            #region declaration
            crlSociete societe = null;
            string strIndication = "";
            string numSociete = "";
            string numIndividu = "";
            #endregion

            #region implementation
            if (hfNumSociete.Value != "")
            {
                societe = serviceSociete.selectSociete(hfNumSociete.Value);
                if (societe != null)
                {
                    this.insertToObjet(societe);
                    if (societe.individuResponsable != null)
                    {
                        if (hfNumIndividuResponsable.Value != "")
                        {
                            numIndividu = serviceIndividu.isIndividu(societe.individuResponsable);
                            if (numIndividu.Equals(""))
                            {
                                if (serviceIndividu.updateIndividu(societe.individuResponsable))
                                {
                                    numSociete = serviceSociete.isSociete(societe);
                                    if (numSociete.Equals(""))
                                    {
                                        if (serviceSociete.updateSociete(societe))
                                        {
                                            this.initialiseFormulaire();
                                            this.initialiseGridSociete();

                                            strIndication = societe.NomSociete + " bien modifier!";
                                            this.divIndicationText(strIndication, "Black");
                                        }
                                        else
                                        {
                                            strIndication = "Une erreur ce produit durant la modification!";
                                            this.divIndicationText(strIndication, "Red");
                                        }
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
                                strIndication = "Information déjà enregistrer dans la base de données!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                        else
                        {
                            societe.NumIndividuResponsable = "";
                            societe.individuResponsable.NumIndividu = "";

                            societe.individuResponsable.NumIndividu = serviceIndividu.isIndividu(societe.individuResponsable);
                            if (societe.individuResponsable.NumIndividu != "")
                            {
                                serviceIndividu.updateIndividu(societe.individuResponsable);
                            }
                            else
                            {
                                societe.individuResponsable.NumIndividu = serviceIndividu.insertIndividu(societe.individuResponsable, agent.agence.SigleAgence);
                            }

                            if (societe.individuResponsable.NumIndividu != "")
                            {
                                societe.NumIndividuResponsable = societe.individuResponsable.NumIndividu;
                                numSociete = serviceSociete.isSociete(societe);

                                if (numSociete != "")
                                {
                                    strIndication = "Information déjà enregistrer dans la base de données!";
                                    this.divIndicationText(strIndication, "Red");
                                }
                                else
                                {
                                    if (serviceSociete.updateSociete(societe))
                                    {
                                        this.initialiseFormulaire();
                                        this.initialiseGridSociete();

                                        strIndication = societe.NomSociete + " bien modifier!";
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
                                strIndication = "Une erreur ce produit durant l'enregistrement!";
                                this.divIndicationText(strIndication, "Red");
                            }
                        }
                    }
                }
                else
                {
                    strIndication = "Une erreur chargement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            #endregion
        }

        #region societe
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
            if (cbCheque.Checked)
            {
                cbCheque.Text = "Accepter";
            }
            else
            {
                cbCheque.Text = "Refuser";
            }
            if (cbBonCommande.Checked)
            {
                cbBonCommande.Text = "Accepter";
            }
            else
            {
                cbBonCommande.Text = "Refuser";
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
        #endregion

        #region respSociete
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

        #endregion

        #region event
        protected void cbReductionUS_CheckedChanged(object sender, EventArgs e)
        {
            this.initialiseCB();
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
                afficheSociete(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                int isDelete = serviceGeneral.delete("societe", "numSociete", e.CommandArgument.ToString());
                if (isDelete == 0)
                {
                    this.divIndicationText(ReSociete.suppressionImpossible, "Red");
                }
                else
                {
                    this.initialiseGridSociete();
                }
            }
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region implementation
            this.updateSociete();
            #endregion
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region implementation
            this.insertSociete();
            #endregion
        }

        protected void ddlTriSociete_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridSociete();
        }
        protected void btnRechercheSociete_Click(object sender, EventArgs e)
        {
            this.initialiseGridSociete();
        }

        #region societe
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
        #endregion

        #region respSociete
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

        protected void btnNouveauRespSociete_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireResponsable();
        }
        #endregion
        #endregion

        protected void cbCheque_CheckedChanged(object sender, EventArgs e)
        {
            this.initialiseCB();
        }
        protected void cbBonCommande_CheckedChanged(object sender, EventArgs e)
        {
            this.initialiseCB();
        }
    }
}