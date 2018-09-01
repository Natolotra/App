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
    public partial class Organisme : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalOrganisme serviceOrganisme = null;
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
            serviceOrganisme = new ImplDalOrganisme();
            serviceGeneral = new ImplDalGeneral();
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
                serviceEtatCivil.loadDddlEtatCivil(ddlCiviliteRespOrganisme);
                this.initialiseFormulaire();

                this.initialiseGrid();

                this.initialiseErrorMessage();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "032"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGrid()
        {
            serviceOrganisme.insertToGridOrganisme(gvOrganisme, ddlTriOrganisme.SelectedValue, ddlTriOrganisme.SelectedValue, TextRechercheOrganisme.Text);
        }

        private void afficheOrganisme(string numOrganisme)
        {
            #region declaration
            crlOrganisme organisme = null;
            #endregion

            #region implementation
            if (numOrganisme != "")
            {
                organisme = serviceOrganisme.selectOrganisme(numOrganisme);
                if (organisme != null)
                {
                    TextAdresseOrganisme.Text = organisme.AdresseOrganisme;
                    TextFixeOrganisme.Text = organisme.TelephoneFixeOrganisme;
                    TextMailOrganisme.Text = organisme.MailOrganisme;
                    TextMobileOrganisme.Text = organisme.TelephoneMobileOrganisme;
                    TextNomOrganisme.Text = organisme.NomOrganisme;

                    if (organisme.IsCheque > 0)
                    {
                        cbCheque.Checked = true;
                    }
                    else
                    {
                        cbCheque.Checked = false;
                    }
                    if (organisme.IsBonCommande > 0)
                    {
                        cbBonCommande.Checked = true;
                    }
                    else
                    {
                        cbBonCommande.Checked = false;
                    }

                    this.initialiseCB();

                    this.afficheQuartierOrganisme(organisme.NumQuartier);

                    hfNumOrganisme.Value = organisme.NumOrganisme;


                    if (organisme.individuResponsable != null)
                    {
                        TextAdresseRespOrganisme.Text = organisme.individuResponsable.Adresse;
                        TextCinRespOrganisme.Text = organisme.individuResponsable.CinIndividu;
                        TextFixeRespOrganisme.Text = organisme.individuResponsable.TelephoneFixeIndividu;
                        TextMailRespOrganisme.Text = organisme.individuResponsable.MailIndividu;
                        TextMobileRespOrganisme.Text = organisme.individuResponsable.TelephoneMobileIndividu;
                        TextNomRespOrganisme.Text = organisme.individuResponsable.NomIndividu;
                        TextPrenomRespOrganisme.Text = organisme.individuResponsable.PrenomIndividu;
                        this.afficheQuartierRespOrganisme(organisme.individuResponsable.NumQuartier);

                        try
                        {
                            ddlCiviliteRespOrganisme.SelectedValue = organisme.individuResponsable.CiviliteIndividu;
                        }
                        catch (Exception)
                        {
                        }

                        hfNumIndividuOrganisme.Value = organisme.individuResponsable.NumIndividu;
                    }

                    btnValider.Enabled = false;
                    btnModifier.Enabled = true;
                    btnNouveauRespOrganisme.Enabled = true;

                    this.divIndicationText("", "Red");
                }
            }
            #endregion
        }

        private void insertToObjet(crlOrganisme organisme)
        {
            #region implementation
            if (organisme != null)
            {
                organisme.AdresseOrganisme = TextAdresseOrganisme.Text;
                organisme.MailOrganisme = TextMailOrganisme.Text;
                organisme.NomOrganisme = TextNomOrganisme.Text;
                organisme.TelephoneFixeOrganisme = TextFixeOrganisme.Text;
                organisme.TelephoneMobileOrganisme = TextMobileOrganisme.Text;
                organisme.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierOrganisme.Text, ddlCommuneOrganisme.SelectedValue, ddlArrondissementOrganisme.SelectedValue, agent.agence.SigleAgence);

                if (cbCheque.Checked)
                {
                    organisme.IsCheque = 1;
                }
                else
                {
                    organisme.IsCheque = 0;
                }
                if (cbBonCommande.Checked)
                {
                    organisme.IsBonCommande = 1;
                }
                else
                {
                    organisme.IsBonCommande = 0;
                }

                if (organisme.individuResponsable == null)
                {
                    organisme.individuResponsable = new crlIndividu();
                }
                organisme.individuResponsable.Adresse = TextAdresseRespOrganisme.Text;
                organisme.individuResponsable.CinIndividu = TextCinRespOrganisme.Text;
                organisme.individuResponsable.CiviliteIndividu = ddlCiviliteRespOrganisme.SelectedValue;
                organisme.individuResponsable.MailIndividu = TextMailRespOrganisme.Text;
                organisme.individuResponsable.NomIndividu = TextNomRespOrganisme.Text;
                organisme.individuResponsable.NumQuartier = serviceQuartier.getNumQuartier(TextQuartierRespOrganisme.Text, ddlCommuneRespOrganisme.SelectedValue, ddlArrondissementRespOrganisme.SelectedValue, agent.agence.SigleAgence);
                organisme.individuResponsable.PrenomIndividu = TextPrenomRespOrganisme.Text;
                organisme.individuResponsable.TelephoneFixeIndividu = TextFixeRespOrganisme.Text;
                organisme.individuResponsable.TelephoneMobileIndividu = TextMobileRespOrganisme.Text;

            }
            #endregion
        }

        private void initialiseErrorMessage()
        {
            TextAdresseOrganisme_RequiredFieldValidator.ErrorMessage = ReOrganisme.adresseOrganismeNonVide;
            TextAdresseRespOrganisme_RequiredFieldValidator.ErrorMessage = ReOrganisme.adresseResponsableNonVide;
            TextCinRespOrganisme_RequiredFieldValidator.ErrorMessage = ReOrganisme.cinResponsableNonVide;
            TextMailOrganisme_RegularExpressionValidator.ErrorMessage = ReOrganisme.mailNonValide;
            TextMailRespOrganisme_RegularExpressionValidator.ErrorMessage = ReOrganisme.mailResponsableNonValide;
            TextNomOrganisme_RequiredFieldValidator.ErrorMessage = ReOrganisme.nomOrganismeNonVide;
            TextNomRespOrganisme_RequiredFieldValidator.ErrorMessage = ReOrganisme.nomResponsableNonVide;
        }

        private void initialiseFormulaire()
        {
            hfNumOrganisme.Value = "";
            TextAdresseOrganisme.Text = "";
            TextFixeOrganisme.Text = "";
            TextMailOrganisme.Text = "";
            TextMobileOrganisme.Text = "";
            TextNomOrganisme.Text = "";

            btnValider.Enabled = true;
            btnModifier.Enabled = false;
            btnNouveauRespOrganisme.Enabled = false;

            this.initialiseFormulaireResponsable();
            this.loadDdlProvinceOrganisme();

            cbCheque.Checked = false;
            cbBonCommande.Checked = false;
            this.initialiseCB();

            this.divIndicationText("", "Red");
        }

        private void initialiseFormulaireResponsable()
        {
            TextAdresseRespOrganisme.Text = "";
            TextFixeRespOrganisme.Text = "";
            TextMailRespOrganisme.Text = "";
            TextMobileRespOrganisme.Text = "";
            TextNomRespOrganisme.Text = "";
            TextPrenomRespOrganisme.Text = "";
            TextQuartierRespOrganisme.Text = "";
            TextCinRespOrganisme.Text = "";
            hfNumIndividuOrganisme.Value = "";

            this.loadDdlProvinceRespOrganisme();
            try
            {
                ddlCiviliteRespOrganisme.SelectedValue = "";
            }
            catch (Exception)
            {
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

        private void insertOrganisme()
        {
            #region declaration
            crlOrganisme organisme = null;
            string strIndication = "";
            #endregion

            #region implementation
            organisme = new crlOrganisme();
            this.insertToObjet(organisme);

            if (organisme.individuResponsable != null)
            {
                organisme.individuResponsable.NumIndividu = serviceIndividu.isIndividu(organisme.individuResponsable);

                if (organisme.individuResponsable.NumIndividu != "")
                {
                    serviceIndividu.updateIndividu(organisme.individuResponsable);
                }
                else
                {
                    organisme.individuResponsable.NumIndividu = serviceIndividu.insertIndividu(organisme.individuResponsable, agent.agence.SigleAgence);
                }

                if (organisme.individuResponsable.NumIndividu != "")
                {
                    organisme.NumIndividuResponsable = organisme.individuResponsable.NumIndividu;
                    organisme.NumOrganisme = serviceOrganisme.isOrganisme(organisme);

                    if (organisme.NumOrganisme != "")
                    {
                        strIndication = "Information déjà enregistrer dans la base de données!";
                        this.divIndicationText(strIndication, "Red");
                    }
                    else
                    {
                        organisme.NumOrganisme = serviceOrganisme.insertOrganisme(organisme, agent.agence.SigleAgence);
                        if (organisme.NomOrganisme != "")
                        {
                            this.initialiseFormulaire();
                            this.initialiseGrid();

                            strIndication = organisme.NomOrganisme + " bien enregistrer!";
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

        private void updateOrganisme()
        {
            #region declaration
            crlOrganisme organisme = null;
            string strIndication = "";
            string numOrganisme = "";
            string numIndividu = "";
            #endregion

            #region implementation
            if (hfNumOrganisme.Value != "")
            {
                organisme = serviceOrganisme.selectOrganisme(hfNumOrganisme.Value);
                if (organisme != null)
                {
                    this.insertToObjet(organisme);
                    if (organisme.individuResponsable != null)
                    {
                        if (hfNumIndividuOrganisme.Value != "")
                        {
                            numIndividu = serviceIndividu.isIndividu(organisme.individuResponsable);
                            if (numIndividu.Equals(""))
                            {
                                if (serviceIndividu.updateIndividu(organisme.individuResponsable))
                                {
                                    numOrganisme = serviceOrganisme.isOrganisme(organisme);
                                    if (numOrganisme.Equals(""))
                                    {
                                        if (serviceOrganisme.updateOrganisme(organisme))
                                        {
                                            this.initialiseFormulaire();
                                            this.initialiseGrid();

                                            strIndication = organisme.NomOrganisme + " bien modifier!";
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
                            organisme.NumIndividuResponsable = "";
                            organisme.individuResponsable.NumIndividu = "";

                            organisme.individuResponsable.NumIndividu = serviceIndividu.isIndividu(organisme.individuResponsable);
                            if (organisme.individuResponsable.NumIndividu != "")
                            {
                                serviceIndividu.updateIndividu(organisme.individuResponsable);
                            }
                            else
                            {
                                organisme.individuResponsable.NumIndividu = serviceIndividu.insertIndividu(organisme.individuResponsable, agent.agence.SigleAgence);
                            }

                            if (organisme.individuResponsable.NumIndividu != "")
                            {
                                organisme.NumIndividuResponsable = organisme.individuResponsable.NumIndividu;
                                numOrganisme = serviceOrganisme.isOrganisme(organisme);

                                if (numOrganisme != "")
                                {
                                    strIndication = "Information déjà enregistrer dans la base de données!";
                                    this.divIndicationText(strIndication, "Red");
                                }
                                else
                                {
                                    if (serviceOrganisme.updateOrganisme(organisme))
                                    {
                                        this.initialiseFormulaire();
                                        this.initialiseGrid();

                                        strIndication = organisme.NomOrganisme + " bien modifier!";
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

        #region organisme
        private void initialiseCB()
        {
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
        #endregion

        #region respOrganisme
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
        protected void ddlTriOrganisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGrid();
        }
        protected void btnOrganisme_Click(object sender, EventArgs e)
        {
            this.initialiseGrid();
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region implementation
            this.insertOrganisme();
            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region implementation
            this.updateOrganisme();
            #endregion
        }
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }

        protected void gvOrganisme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrganisme.PageIndex = e.NewPageIndex;
            this.initialiseGrid();
        }
        protected void gvOrganisme_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheOrganisme(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                int isDelete = serviceGeneral.delete("organisme", "numOrganisme", e.CommandArgument.ToString());
                if (isDelete > 0)
                {

                    this.initialiseGrid();
                }
                else
                {
                    this.divIndicationText(ReOrganisme.suppressionImpossible, "Red");
                }
            }
            #endregion
        }

        #region organisme
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
        #endregion

        #region respOrganisme
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

        protected void btnNouveauRespOrganisme_Click(object sender, EventArgs e)
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