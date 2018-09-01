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
    public partial class Ville : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalProvince serviceProvince = null;
        IntfDalRegion serviceRegion = null;
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
            serviceProvince = new ImplDalProvince();
            serviceRegion = new ImplDalRegion();
            serviceVille = new ImplDalVille();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                serviceProvince.loadDddlProvince(ddlProvince);
                serviceRegion.loadDddlRegion(ddlRegion);
                this.initialiseGridVille();
                this.initialiseFormulaire();
                hfNumVille.Value = "";
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "021"))
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
            RequiredFieldValidator_TextNomVille.ErrorMessage = ReVille.nomVilleNonVide;
            RequiredFieldValidator_ddlProvince.ErrorMessage = ReVille.provinceNonVide;
            RequiredFieldValidator_ddlRegion.ErrorMessage = ReVille.regionNonVide;
        }

        private void initialiseGridVille()
        {
            serviceVille.insertToGridVille(gvVille, ddlTriVille.SelectedValue, ddlTriVille.SelectedValue, TextRechercheVille.Text);
        }

        private void afficheVille(string numVille)
        {
            #region declaration
            crlVille ville = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numVille != "")
            {
                ville = serviceVille.selectVille(numVille);

                if (ville != null)
                {
                    try
                    {
                        ddlProvince.SelectedValue = ville.NomProvince;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        ddlRegion.SelectedValue = ville.NomRegion;
                    }
                    catch (Exception)
                    {
                    }
                    TextNomVille.Text = ville.NomVille;
                    hfNumVille.Value = ville.NumVille;
                    strConfirm = "Voulez vous vraiment modifier le ville " + ville.NomVille + "?";
                    btnModifier_ConfirmButtonExtender.ConfirmText = strConfirm;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;
                }
            }
            #endregion
        }

        private void initialiseFormulaire()
        {
            try
            {
                ddlProvince.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            try
            {
                ddlRegion.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            TextNomVille.Text = "";
            hfNumVille.Value = "";
            this.divIndicationText("", "Red");
            btnModifier.Enabled = false;
            btnValider.Enabled = true;
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

        private void insertToObjetVille(crlVille ville)
        {
            #region implementation
            if (ville != null)
            {
                ville.NomProvince = ddlProvince.SelectedValue;
                ville.NomRegion = ddlRegion.SelectedValue;
                ville.NomVille = TextNomVille.Text;
            }
            #endregion
        }
        #endregion

        #region event
        protected void ddlTriVille_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridVille();
        }
        protected void btnRechercheVille_Click(object sender, EventArgs e)
        {
            this.initialiseGridVille();
        }
        protected void gvVille_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVille.PageIndex = e.NewPageIndex;
            this.initialiseGridVille();
        }
        protected void gvVille_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheVille(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceVille.deleteVille(e.CommandArgument.ToString()))
                {
                    this.initialiseGridVille();
                    this.initialiseFormulaire();
                }
                else
                {

                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlVille ville = null;
            string strIndication = "";
            string numVille = "";
            #endregion

            #region implementation
            if (hfNumVille.Value != "")
            {
                ville = serviceVille.selectVille(hfNumVille.Value);

                if (ville != null)
                {
                    this.insertToObjetVille(ville);

                    numVille = serviceVille.isVille(ville);
                    if (numVille.Equals(""))
                    {
                        if (serviceVille.updateVille(ville))
                        {
                            this.initialiseGridVille();
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        //
                    }
                }
            }
            #endregion
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlVille ville = null;
            string strIndication = "";
            #endregion

            #region implementation
            ville = new crlVille();
            this.insertToObjetVille(ville);

            ville.NumVille = serviceVille.isVille(ville);

            if (ville.NumVille.Equals(""))
            {
                ville.NumVille = serviceVille.insertVille(ville, agent.agence.SigleAgence);

                if (ville.NumVille != "")
                {
                    this.initialiseFormulaire();
                    strIndication = "Ville " + ville.NomVille + " bien enregistrer!";
                    this.divIndicationText(strIndication, "Black");
                    this.initialiseGridVille();
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                this.afficheVille(ville.NumVille);
            }
            #endregion
        }
        #endregion
    }
}