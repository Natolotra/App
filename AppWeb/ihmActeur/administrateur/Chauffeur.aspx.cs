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
    public partial class Chauffeur : System.Web.UI.Page
    {
        #region declaration
        IntfDalServiceRessource serviceRessource = null;
        IntfDalServicePage servicePage = null;
        IntfDalChauffeur serviceChauffeur = null;
        IntfDalCooperative serviceCooperative = null;
        IntfDalLien serviceLien = null;
        IntfDalSituationFamiliale serviceSituationFamiliale = null;

        crlAgent agent = null;
        #endregion

        #region event page
        protected void Page_Load(object sender, EventArgs e)
        {
            #region initialiseRessource
            serviceRessource = new ImplDalServiceRessource();
            serviceLien = new ImplDalLien();
            #endregion

            #region verification
            this.verification();
            #endregion

            #region initialisation
            servicePage = new ImplDalServicePage();
            serviceChauffeur = new ImplDalChauffeur();
            serviceCooperative = new ImplDalCooperative();
            serviceSituationFamiliale = new ImplDalSituationFamiliale();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                serviceSituationFamiliale.loadDddlSituationFamiliale(ddlSituationFamiliale);
                serviceCooperative.loadDdlCooperative(ddlCooperative);
                this.serviceChauffeur.loadDdlTri(ddlTriChauffeur);
                this.initialiseFormulaire();
                this.initialiseGridView();
                this.initialiseErreurMessage();
            }
            #endregion
        }
        #endregion

        #region methode page
        private void initialiseErreurMessage()
        {
            TextAdresse_RequiredFieldValidator.ErrorMessage = ReChauffeur.adresseNonVide;
            TextNom_RequiredFieldValidator.ErrorMessage = ReChauffeur.nomNonVide;
            TextCIN_RequiredFieldValidator.ErrorMessage = ReChauffeur.cinNonVide;
            ddlSituationFamiliale_RequiredFieldValidator.ErrorMessage = ReChauffeur.situationFamilialeNonVide;
        }
        private void initialiseGridView()
        {
            if (TextRechercher.Text.Trim().Equals(""))
            {
                servicePage.insertToGridView(gvChauffeur, "chauffeur", ddlTriChauffeur.SelectedValue);
            }
            else
            {
                servicePage.insertToGridView(gvChauffeur, "chauffeur", ddlTriChauffeur.SelectedValue, ddlTriChauffeur.SelectedValue, TextRechercher.Text);
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "041"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseFormulaire()
        {
            TextNom.Text = "";
            TextPrenom.Text = "";
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

            hfId.Value = "";
            hfValue.Value = "";
            ddlCooperative.SelectedValue = "";
            this.divIndicationText("", "black");
            this.initialiseGridView();

            btnInserer.Enabled = true;
            btnModiffier.Enabled = false;
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
                    TextPrenom.Text = chauffeur.prenomChauffeur;
                    TextCIN.Text = chauffeur.cinChauffeur;
                    TextAdresse.Text = chauffeur.adresseChauffeur;
                    TextTelephone.Text = chauffeur.telephoneChauffeur;
                    TextMobile.Text = chauffeur.telephoneMobileChauffeur;
                    hfValue.Value = chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur;
                    ddlCooperative.SelectedValue = chauffeur.NumCooperative;

                    try
                    {
                        ddlSituationFamiliale.SelectedValue = chauffeur.SituationFamilialeChauffeur;
                    }
                    catch (Exception)
                    {
                    }
                    TextDateNaissance.Text = chauffeur.DateNaissanceChauffeur.ToString("dd MMMM yyyy");
                    TextLieuNaissance.Text = chauffeur.LieuNaissanceChauffeur;

                    strConfirm = "Voulez vous vraiment modifier le chauffeur \n" + chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur + "?";
                    ConfirmButtonExtender_btnModiffier.ConfirmText = strConfirm;

                    btnInserer.Enabled = false;
                    btnModiffier.Enabled = true;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.jsErreureAffichage + "');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.jsErreureAffichage + "');", true);
            }
            #endregion
        }
        #endregion

        #region event
        protected void btnInserer_Click(object sender, EventArgs e)
        {
            #region declaration
            crlChauffeur chauffeur = null;
            string idChauffeur = "";
            #endregion

            #region implementation

            chauffeur = new crlChauffeur();
            chauffeur.nomChauffeur = TextNom.Text;
            chauffeur.prenomChauffeur = TextPrenom.Text;
            chauffeur.cinChauffeur = TextCIN.Text;
            chauffeur.adresseChauffeur = TextAdresse.Text;
            chauffeur.telephoneChauffeur = TextTelephone.Text;
            chauffeur.telephoneMobileChauffeur = TextMobile.Text;
            chauffeur.NumCooperative = ddlCooperative.SelectedValue;
            try
            {
                chauffeur.DateNaissanceChauffeur = Convert.ToDateTime(TextDateNaissance.Text);
            }
            catch (Exception)
            {
            }
            chauffeur.LieuNaissanceChauffeur = TextLieuNaissance.Text;
            chauffeur.SituationFamilialeChauffeur = ddlSituationFamiliale.SelectedValue;

            if (serviceChauffeur.isChauffeur(chauffeur) == 0)
            {
                idChauffeur = serviceChauffeur.insertChauffeur(chauffeur, agent.agence.SigleAgence);
                if (idChauffeur != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.jsInsertionOk + "');", true);
                    this.initialiseFormulaire();
                    this.initialiseGridView();
                }
                else
                {
                    this.divIndicationText(ReChauffeur.InsertionNonValide, "red");
                }
            }
            else
            {
                this.divIndicationText(ReChauffeur.FormulaireExistant, "red");
            }

            #endregion
        }
        protected void btnModiffier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlChauffeur chauffeur = null;
            bool isUpdate = false;
            #endregion

            #region implementation
            if (hfId.Value != "")
            {

                chauffeur = serviceChauffeur.selectChauffeur(hfId.Value);
                if (chauffeur != null && chauffeur.idChauffeur != "")
                {
                    chauffeur.nomChauffeur = TextNom.Text;
                    chauffeur.prenomChauffeur = TextPrenom.Text;
                    chauffeur.adresseChauffeur = TextAdresse.Text;
                    chauffeur.cinChauffeur = TextCIN.Text;
                    chauffeur.telephoneChauffeur = TextTelephone.Text;
                    chauffeur.telephoneMobileChauffeur = TextMobile.Text;
                    chauffeur.NumCooperative = ddlCooperative.SelectedValue;
                    try
                    {
                        chauffeur.DateNaissanceChauffeur = Convert.ToDateTime(TextDateNaissance.Text);
                    }
                    catch (Exception)
                    {
                    }
                    chauffeur.LieuNaissanceChauffeur = TextLieuNaissance.Text;
                    chauffeur.SituationFamilialeChauffeur = ddlSituationFamiliale.SelectedValue;

                    if (serviceChauffeur.isChauffeur(chauffeur) == 0)
                    {
                        isUpdate = serviceChauffeur.updateChauffeur(chauffeur);
                        if (isUpdate)
                        {
                            this.initialiseFormulaire();
                            this.initialiseGridView();
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.jsModificationEnregistrer + "');", true);
                        }
                        else
                        {
                            this.divIndicationText(ReChauffeur.ModiffierNonEnregistre, "red");
                        }
                    }
                    else
                    {
                        this.divIndicationText(ReChauffeur.FormulaireExistant, "red");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.JsErreurObjet + "');", true);
                }

            }
            else
            {
                this.divIndicationText(ReChauffeur.ModificationNonAutorise, "red");
            }
            #endregion
        }
        protected void btnSupprimer_Click(object sender, EventArgs e)
        {
            #region declaration
            bool isDelete = false;
            #endregion

            #region implementation
            if (hfId.Value != "")
            {
                isDelete = serviceChauffeur.deleteChauffeur(hfId.Value);
                if (isDelete)
                {
                    this.initialiseFormulaire();
                    this.initialiseGridView();
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.jsSuppresionEnregistre + "');", true);
                }
                else
                {
                    this.divIndicationText(ReChauffeur.SuppressionNonEnregistre, "red");
                }
            }
            else
            {
                this.divIndicationText(ReChauffeur.SuppressionNonAutorise, "red");
            }
            #endregion
        }
        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
            this.initialiseGridView();
        }
        protected void ddlTriChauffeur_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridView();
        }
        protected void btnRechercher_Click(object sender, EventArgs e)
        {
            this.initialiseGridView();
        }
        protected void gvChauffeur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChauffeur.PageIndex = e.NewPageIndex;
            this.initialiseFormulaire();
        }
        protected void gvChauffeur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region initialisation
            string idChauffeur = e.CommandArgument.ToString().Trim();
            #endregion

            #region implementation
            if (e.CommandName == "select")
            {
                if (idChauffeur != "")
                {
                    this.afficheChauffeur(idChauffeur);
                    hfId.Value = idChauffeur;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReChauffeur.jsErreureAffichage + "');", true);
                }
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceChauffeur.deleteChauffeur(e.CommandArgument.ToString()))
                {
                    this.initialiseGridView();
                }
                else
                {
                }
            }
            #endregion
        }
        #endregion
    }
}