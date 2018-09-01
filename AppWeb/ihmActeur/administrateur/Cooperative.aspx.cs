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
    public partial class Cooperative : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalCooperative serviceCooperative = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalLien serviceLien = null;
        IntfDalZone serviceZone = null;
        IntfDalVille serviceVille = null;

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
            serviceCooperative = new ImplDalCooperative();
            serviceGeneral = new ImplDalGeneral();
            serviceVille = new ImplDalVille();
            serviceZone = new ImplDalZone();
            #endregion

            #region declaration
            if (!IsPostBack)
            {
                this.initialiseFormulaire();
                this.initialiseGridCooperative();
                this.initialiseErrorMessage();

                serviceZone.loadDDLAllZone(ddlZone);
                serviceVille.loadDdlVilleA(ddlVille);
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "033"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridCooperative()
        {
            serviceCooperative.insertToGridCooperative(gvCooperative, ddltriCooperative.SelectedValue, ddltriCooperative.SelectedValue, TextRechercheCooperative.Text);
        }

        private void afficheCooperative(string numCooperative)
        {
            #region declaration
            crlCooperative cooperative = null;
            #endregion

            #region implementation
            if (numCooperative != "")
            {
                cooperative = serviceCooperative.selectCooperative(numCooperative);

                if (cooperative != null)
                {
                    TextAdresse.Text = cooperative.AdresseCooperative;
                    TextNomCooperative.Text = cooperative.NomCooperative;
                    TextSigle.Text = cooperative.SigleCooperative;

                    try
                    {
                        ddlZone.SelectedValue = cooperative.Zone;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        ddlVille.SelectedValue = cooperative.NumVille;
                    }
                    catch (Exception)
                    {
                    }
                    TextFixeCooperative.Text = cooperative.TelephoneFixeCooperative;
                    TextMobileCooperative.Text = cooperative.TelephoneMobileCooperative;

                    TextNomResponsable.Text = cooperative.NomResponsable;
                    TextPrenomResponsable.Text = cooperative.PrenomResponsable;
                    TextCINResponsable.Text = cooperative.CinResponsable;
                    TextAdresseResponsable.Text = cooperative.AdresseCooperative;
                    TextTelephoneResponsable.Text = cooperative.TelephoneFixeResponsable;
                    TextMobileResponsable.Text = cooperative.TelephoneMobileResponsable;

                    hfNumCooperative.Value = cooperative.NumCooperative;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;

                    ConfirmButtonExtender_btnModifier.ConfirmText = "Vouler vous vraiment modifier le coopérative " + cooperative.NomCooperative + "?";

                }
            }
            #endregion
        }

        private void initialiseFormulaire()
        {
            TextAdresse.Text = "";
            TextNomCooperative.Text = "";
            TextSigle.Text = "";

            try
            {
                ddlZone.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            try
            {
                ddlVille.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            TextFixeCooperative.Text = "";
            TextMobileCooperative.Text = "";

            TextNomResponsable.Text = "";
            TextPrenomResponsable.Text = "";
            TextCINResponsable.Text = "";
            TextAdresseResponsable.Text = "";
            TextTelephoneResponsable.Text = "";
            TextMobileResponsable.Text = "";

            hfNumCooperative.Value = "";
            this.divIndicationText("", "Red");

            btnModifier.Enabled = false;
            btnValider.Enabled = true;
        }

        private void insertToObjet(crlCooperative cooperative)
        {
            #region implementation
            if (cooperative != null)
            {
                cooperative.AdresseCooperative = TextAdresse.Text;
                cooperative.NomCooperative = TextNomCooperative.Text;
                cooperative.SigleCooperative = TextSigle.Text;

                cooperative.Zone = ddlZone.SelectedValue;
                cooperative.NumVille = ddlVille.SelectedValue;
                cooperative.TelephoneFixeCooperative = TextFixeCooperative.Text;
                cooperative.TelephoneMobileCooperative = TextMobileCooperative.Text;

                cooperative.NomResponsable = TextNomResponsable.Text;
                cooperative.PrenomResponsable = TextPrenomResponsable.Text;
                cooperative.CinResponsable = TextCINResponsable.Text;
                cooperative.AdressseResponsable = TextAdresseResponsable.Text;
                cooperative.TelephoneFixeResponsable = TextTelephoneResponsable.Text;
                cooperative.TelephoneMobileResponsable = TextMobileResponsable.Text;
            }
            #endregion
        }

        private void initialiseErrorMessage()
        {
            TextAdresse_RequiredFieldValidator.ErrorMessage = ReCooperative.adresseCooperativeNonVide;
            TextNomCooperative_RequiredFieldValidator.ErrorMessage = ReCooperative.nomCooperativeNonVide;
            TextSigle_RequiredFieldValidator.ErrorMessage = ReCooperative.sigleCooperativeNonVide;

            ddlZone_RequiredFieldValidator.ErrorMessage = ReCooperative.zoneNonVide;
            ddlVille_RequiredFieldValidator.ErrorMessage = ReCooperative.villeNonVide;
            TextNomResponsable_RequiredFieldValidator.ErrorMessage = ReCooperative.nomNonVide;
            TextCINResponsable_RequiredFieldValidator.ErrorMessage = ReCooperative.cinNonVide;
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
        #endregion

        #region event
        protected void ddltriCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divIndicationText("", "Red");
            this.initialiseGridCooperative();
        }
        protected void btnRechercheCooperative_Click(object sender, EventArgs e)
        {
            this.divIndicationText("", "Red");
            this.initialiseGridCooperative();
        }
        protected void gvCooperative_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.divIndicationText("", "Red");
            gvCooperative.PageIndex = e.NewPageIndex;
            this.initialiseGridCooperative();
        }
        protected void gvCooperative_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.divIndicationText("", "Red");
            if (e.CommandName.Equals("select"))
            {
                this.afficheCooperative(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("cooperative", "numCooperative", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridCooperative();
                }
                else
                {
                }
            }
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlCooperative cooperative = null;
            string strIndication = "";
            #endregion

            #region implementation
            cooperative = new crlCooperative();

            this.insertToObjet(cooperative);
            cooperative.NumCooperative = serviceCooperative.isCooperative(cooperative);
            if (cooperative.NumCooperative.Equals(""))
            {
                cooperative.NumCooperative = serviceCooperative.insertCooperative(cooperative, agent.agence.SigleAgence);
                if (cooperative.NumCooperative != "")
                {
                    this.initialiseGridCooperative();
                    this.initialiseFormulaire();
                    strIndication = "Coopéartive " + cooperative.NomCooperative + " bien enregistrer!";
                    this.divIndicationText(strIndication, "Black");
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                strIndication = "Le cooperative " + cooperative.NomCooperative + " est déjà dans le base de donnée!";
                this.divIndicationText(strIndication, "Red");
            }
            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlCooperative cooperative = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumCooperative.Value != "")
            {
                cooperative = serviceCooperative.selectCooperative(hfNumCooperative.Value);
                if (cooperative != null)
                {
                    this.insertToObjet(cooperative);

                    if (serviceCooperative.isCooperative(cooperative).Equals(""))
                    {
                        if (serviceCooperative.updateCooperative(cooperative))
                        {
                            this.initialiseGridCooperative();
                            this.initialiseFormulaire();
                            strIndication = "Modification de la coopéartive " + cooperative.NomCooperative + " bien enregistrer!";
                            this.divIndicationText(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            this.divIndicationText(strIndication, "Red");
                        }
                    }
                    else
                    {
                        strIndication = "Le cooperative " + cooperative.NomCooperative + " est déjà dans le base de donnée!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
            }
            else
            {
                //
            }
            #endregion
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        #endregion
    }
}