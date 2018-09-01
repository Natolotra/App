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
    public partial class CategorieBille : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalCalculCategorieBillet serviceCalculCategorieBillet = null;
        IntfDalGeneral serviceGeneral = null;
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
            serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialisationGridCategorieBillet();
                this.initialiseFormulaire();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "011"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialisationGridCategorieBillet()
        {
            serviceCalculCategorieBillet.insertToGridCalculCategorieBillet(gvCategorieBillet, ddlTriCategorieBillet.SelectedValue, ddlTriCategorieBillet.SelectedValue, TextRechercheCategorieBillet.Text);
        }

        private void initialiseFormulaire()
        {
            TextCategorie.Text = "";
            TextPourcentage.Text = "";

            hfNumCalculCategorieBillet.Value = "";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;
            this.divIndicationText("", "Red");
        }

        private void afficheCategorieBillet(string numCalculCategorieBillet)
        {
            #region declaration
            crlCalculCategorieBillet calculCategorieBillet = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numCalculCategorieBillet != "")
            {
                calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(numCalculCategorieBillet);
                if (calculCategorieBillet != null)
                {
                    TextCategorie.Text = calculCategorieBillet.IndicateurPrixBillet;
                    TextPourcentage.Text = calculCategorieBillet.PourcentagePrixBillet.ToString();

                    hfNumCalculCategorieBillet.Value = calculCategorieBillet.NumCalculCategorieBillet;

                    strConfirm = "Voulez vous vraiment modifier le catégorie " + calculCategorieBillet.IndicateurPrixBillet + "?";
                    btnModifier_ConfirmButtonExtender.ConfirmText = strConfirm;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;

                    this.divIndicationText("", "Red");
                }
            }
            #endregion

        }

        private void insertToObjet(crlCalculCategorieBillet calculCategorieBillet)
        {
            #region implementation
            if (calculCategorieBillet != null)
            {
                calculCategorieBillet.IndicateurPrixBillet = TextCategorie.Text;
                try
                {
                    calculCategorieBillet.PourcentagePrixBillet = double.Parse(TextPourcentage.Text);
                }
                catch (Exception)
                {
                }
            }
            #endregion
        }

        private void initialiseErrorMessage()
        {
            TextCategorie_RequiredFieldValidator.ErrorMessage = ReCalculCategorieBillet.categorieNonVide;
            TextPourcentage_RequiredFieldValidator.ErrorMessage = ReCalculCategorieBillet.pourcentageNonVide;
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
        protected void ddlTriCategorieBillet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialisationGridCategorieBillet();
        }
        protected void btnRechercheCategorieBillet_Click(object sender, EventArgs e)
        {
            this.initialisationGridCategorieBillet();
        }
        protected void gvCategorieBillet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategorieBillet.PageIndex = e.NewPageIndex;
            this.initialisationGridCategorieBillet();
        }
        protected void gvCategorieBillet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheCategorieBillet(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("calculcategoriebillet", "numCalculCategorieBillet", e.CommandArgument.ToString()) == 1)
                {
                    this.initialisationGridCategorieBillet();
                }
                else
                {
                    //
                }
            }
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlCalculCategorieBillet calculCategorieBillet = null;
            string strIndication = "";
            #endregion

            #region implementation
            calculCategorieBillet = new crlCalculCategorieBillet();
            this.insertToObjet(calculCategorieBillet);

            calculCategorieBillet.NumCalculCategorieBillet = serviceCalculCategorieBillet.isCalculeCategorieBillet(calculCategorieBillet);

            if (calculCategorieBillet.NumCalculCategorieBillet.Equals(""))
            {
                calculCategorieBillet.NumCalculCategorieBillet = serviceCalculCategorieBillet.insertCalculeCategorieBillet(calculCategorieBillet, agent.agence.SigleAgence);

                if (calculCategorieBillet.NumCalculCategorieBillet != "")
                {
                    this.initialisationGridCategorieBillet();
                    this.initialiseFormulaire();

                    strIndication = "La catégorie " + calculCategorieBillet.IndicateurPrixBillet + " est bien enregistrer!";
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
                strIndication = "La catégorie " + calculCategorieBillet.IndicateurPrixBillet + " est déjà dans la base de donnée!";
                this.divIndicationText(strIndication, "Red");
            }
            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlCalculCategorieBillet calculCategorieBillet = null;
            string numCalculCategorieBillet = "";
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumCalculCategorieBillet.Value != "")
            {
                calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(hfNumCalculCategorieBillet.Value);

                if (calculCategorieBillet != null)
                {
                    this.insertToObjet(calculCategorieBillet);
                    numCalculCategorieBillet = serviceCalculCategorieBillet.isCalculeCategorieBillet(calculCategorieBillet);

                    if (numCalculCategorieBillet.Equals(""))
                    {
                        if (serviceCalculCategorieBillet.updateCalculCategorieBillet(calculCategorieBillet))
                        {
                            this.initialisationGridCategorieBillet();
                            this.initialiseFormulaire();

                            strIndication = "La catégorie " + calculCategorieBillet.IndicateurPrixBillet + " est bien modifier!";
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
                        strIndication = "La catégorie " + calculCategorieBillet.IndicateurPrixBillet + " est déjà dans la base de donnée!";
                        this.divIndicationText(strIndication, "Red");
                    }

                }
            }
            #endregion
        }
        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        #endregion
    }
}