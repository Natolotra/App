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
    public partial class Agence : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAgence serviceAgence = null;
        IntfDalTypeAgence serviceTypeAgence = null;
        IntfDalVille serviceVille = null;
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
            serviceAgence = new ImplDalAgence();
            serviceTypeAgence = new ImplDalTypeAgence();
            serviceVille = new ImplDalVille();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                serviceVille.loadDdlVilleA(ddlVille);
                serviceTypeAgence.loadDddlTypeAgence(ddlTypeAgence);
                this.initialiseGridAgence();
                this.initialiseFormulaireAgence();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "034"))
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
            ddlTypeAgence_RequiredFieldValidator.ErrorMessage = ReAgence.typeAgenceNonVide;
            ddlVille_RequiredFieldValidator.ErrorMessage = ReAgence.villeNonVide;
            TextNomAgence_RequiredFieldValidator.ErrorMessage = ReAgence.nomNonVide;
            TextLocalisation_RequiredFieldValidator.ErrorMessage = ReAgence.localisationNonVide;
            TextSigle_RequiredFieldValidator.ErrorMessage = ReAgence.sigleNonVide;
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

        private void initialiseGridAgence()
        {
            serviceAgence.insertToGridAgenceListe(gvAgence, ddlTriAgence.SelectedValue, ddlTriAgence.SelectedValue, TextRechercheAgence.Text);
        }

        private void initialiseFormulaireAgence()
        {
            hfNumAgence.Value = "";
            ddlTypeAgence.SelectedValue = "";
            ddlVille.SelectedValue = "";
            TextNomAgence.Text = "";
            TextLocalisation.Text = "";
            TextSigle.Text = "";

            btnModifier.Enabled = false;
            btnVlider.Enabled = true;

            ConfirmButtonExtender_btnModifier.ConfirmText = "";
            this.divIndicationText("", "Red");
        }

        private void afficheAgence(string numAgence)
        {
            #region declaration
            crlAgence agence = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numAgence != "")
            {
                agence = serviceAgence.selectAgence(numAgence);

                if (agence != null)
                {
                    ddlTypeAgence.SelectedValue = agence.TypeAgence;
                    ddlVille.SelectedValue = agence.NumVille;
                    TextNomAgence.Text = agence.NomAgence;
                    TextLocalisation.Text = agence.LocalisationAgence;
                    TextSigle.Text = agence.SigleAgence;

                    hfNumAgence.Value = agence.NumAgence;

                    btnModifier.Enabled = true;
                    btnVlider.Enabled = false;

                    strConfirm = "Voulez vous vraiment modifier l'agence " + agence.NomAgence + "?";
                    ConfirmButtonExtender_btnModifier.ConfirmText = strConfirm;

                    this.divIndicationText("", "Red");
                }
            }
            #endregion
        }

        private void insertToObjAgence(crlAgence agence)
        {
            #region implementation
            if (agence != null)
            {
                agence.LocalisationAgence = TextLocalisation.Text;
                agence.NomAgence = TextNomAgence.Text;
                agence.NumVille = ddlVille.SelectedValue;
                agence.SigleAgence = TextSigle.Text;
                agence.TypeAgence = ddlTypeAgence.SelectedValue;
            }
            #endregion
        }
        #endregion

        #region event
        protected void ddlTriAgence_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAgence();
        }
        protected void btnRechercheAgence_Click(object sender, EventArgs e)
        {
            this.initialiseGridAgence();
        }
        protected void gvAgence_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAgence.PageIndex = e.NewPageIndex;
            this.initialiseGridAgence();
        }
        protected void gvAgence_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheAgence(e.CommandArgument.ToString());
            }
        }

        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireAgence();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAgence agence = null;
            int isAgence = 0;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumAgence.Value != "")
            {
                agence = serviceAgence.selectAgence(hfNumAgence.Value);

                if (agence != null)
                {
                    this.insertToObjAgence(agence);
                    isAgence = serviceAgence.isAgence(agence);

                    if (isAgence == 0)
                    {

                        if (serviceAgence.updateAgence(agence))
                        {
                            this.initialiseFormulaireAgence();
                            this.initialiseGridAgence();

                            strIndication = "Agence " + agence.NomAgence + " bien modifier!";
                            divIndicationText(strIndication, "Black");
                        }
                        else
                        {
                            strIndication = "Une erreur ce produit durant l'enregistrement!";
                            divIndicationText(strIndication, "Red");
                        }
                    }
                    else if (isAgence == 1)
                    {
                        strIndication = "Sigle existant dans la base!";
                        divIndicationText(strIndication, "Red");
                    }
                    else if (isAgence == 2)
                    {
                        strIndication = "Nom agence déjà dans la base!";
                        divIndicationText(strIndication, "Red");
                    }
                }
            }
            #endregion
        }
        protected void btnVlider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAgence agence = null;
            int isAgence = 0;
            string strIndication = "";
            #endregion

            #region implementation
            agence = new crlAgence();
            this.insertToObjAgence(agence);

            isAgence = serviceAgence.isAgence(agence);

            if (isAgence == 0)
            {
                agence.NumAgence = serviceAgence.insertAgence(agence);
                if (agence.NumAgence != "")
                {
                    this.initialiseFormulaireAgence();
                    this.initialiseGridAgence();

                    strIndication = "Agence " + agence.NomAgence + " bien enregistrer!";
                    divIndicationText(strIndication, "Black");
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    divIndicationText(strIndication, "Red");
                }
            }
            else if (isAgence == 1)
            {
                strIndication = "Sigle existant dans la base!";
                divIndicationText(strIndication, "Red");
            }
            else if (isAgence == 2)
            {
                strIndication = "Nom agence déjà dans la base!";
                divIndicationText(strIndication, "Red");
            }
            #endregion
        }
        #endregion
    }
}