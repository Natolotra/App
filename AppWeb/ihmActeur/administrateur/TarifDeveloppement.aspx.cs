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
    public partial class TarifDeveloppement : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalTarifDeveloppement serviceTarifDeveloppement = null;
        IntfDalZone serviceZone = null;
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
            serviceTarifDeveloppement = new ImplDalTarifDeveloppement();
            serviceZone = new ImplDalZone();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseFormulaire();
                this.initialiseGridTD();
                serviceZone.loadDDLAllZone(ddlZone);
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "013"))
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
            TextCommentaire.Text = "";
            TextMontant.Text = "";
            try
            {
                ddlZone.SelectedValue = "";
            }
            catch (Exception)
            {
            }

            hfNumTarifDeveloppement.Value = "";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;

            this.divIndicationText("", "Red");
            ConfirmButtonExtender_btnModifier.ConfirmText = "";
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

        private void afficheTarifDeveloppement(string numTarifDeveloppement)
        {
            #region declaration
            crlTarifDeveloppement tarifDeveloppement = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numTarifDeveloppement != "")
            {
                tarifDeveloppement = serviceTarifDeveloppement.selectTarifDeveloppement(numTarifDeveloppement);

                if (tarifDeveloppement != null)
                {
                    TextCommentaire.Text = tarifDeveloppement.CommentaireTarifDeveloppement;
                    TextMontant.Text = tarifDeveloppement.MontantTarifDeveloppement.ToString("0");
                    ddlZone.SelectedValue = tarifDeveloppement.Zone;

                    hfNumTarifDeveloppement.Value = tarifDeveloppement.NumTarifDeveloppement;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;

                    strConfirm = "Voulez vous vraiment modifier le tarif de développement\n de le zone " + tarifDeveloppement.Zone + "?";
                    ConfirmButtonExtender_btnModifier.ConfirmText = strConfirm;

                    this.divIndicationText("", "Red");
                }
            }
            #endregion
        }

        private void initialiseGridTD()
        {
            serviceTarifDeveloppement.insertToGridTarifDeveloppement(gvTD, ddlTriTarifD.SelectedValue, ddlTriTarifD.SelectedValue, TextRechercheTD.Text);
        }

        private void insertToObj(crlTarifDeveloppement tarifDeveloppement)
        {
            if (tarifDeveloppement != null)
            {
                try
                {
                    tarifDeveloppement.MontantTarifDeveloppement = int.Parse(TextMontant.Text);
                }
                catch (Exception)
                {
                }
                tarifDeveloppement.Zone = ddlZone.SelectedValue;
                tarifDeveloppement.CommentaireTarifDeveloppement = TextCommentaire.Text;
            }
        }

        private void initialiseErrorMessage()
        {
            TextMontant_RequiredFieldValidator.ErrorMessage = ReTarifDeveloppement.montantNonVide;
            ddlZone_RequiredFieldValidator.ErrorMessage = ReTarifDeveloppement.zoneNonVide;
        }
        #endregion

        #region event
        protected void ddlTriTarifD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridTD();
            this.divIndicationText("", "Red");
        }
        protected void btnRechercheTD_Click(object sender, EventArgs e)
        {
            this.initialiseGridTD();
            this.divIndicationText("", "Red");
        }
        protected void gvTD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTD.PageIndex = e.NewPageIndex;
            this.initialiseGridTD();
            this.divIndicationText("", "Red");
        }
        protected void gvTD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheTarifDeveloppement(e.CommandArgument.ToString());
            }
        }

        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTarifDeveloppement tarifDeveloppement = null;
            string numTarifDeveloppement = "";
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumTarifDeveloppement.Value != "")
            {
                tarifDeveloppement = serviceTarifDeveloppement.selectTarifDeveloppement(hfNumTarifDeveloppement.Value);

                if (tarifDeveloppement != null)
                {
                    this.insertToObj(tarifDeveloppement);

                    numTarifDeveloppement = serviceTarifDeveloppement.isTarifDeveloppement(tarifDeveloppement);
                    if (numTarifDeveloppement.Equals(""))
                    {
                        if (serviceTarifDeveloppement.updateTarifDeveloppement(tarifDeveloppement))
                        {
                            //
                            this.initialiseFormulaire();
                            this.initialiseGridTD();

                            strIndication = "Tarif développement pour la zone " + tarifDeveloppement.Zone + " bien enregister!";
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
                        strIndication = "Le zone " + tarifDeveloppement.Zone + " à déjà un tarif de développement!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
            }
            #endregion
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTarifDeveloppement tarifDeveloppement = null;
            string strIndication = "";
            #endregion

            #region implementation
            tarifDeveloppement = new crlTarifDeveloppement();
            this.insertToObj(tarifDeveloppement);

            tarifDeveloppement.NumTarifDeveloppement = serviceTarifDeveloppement.isTarifDeveloppement(tarifDeveloppement);
            if (tarifDeveloppement.NumTarifDeveloppement.Equals(""))
            {
                tarifDeveloppement.NumTarifDeveloppement = serviceTarifDeveloppement.insertTarifDeveloppement(tarifDeveloppement, agent.agence.SigleAgence);

                if (tarifDeveloppement.NumTarifDeveloppement != "")
                {
                    //
                    this.initialiseFormulaire();
                    this.initialiseGridTD();

                    strIndication = "Tarif développement pour la zone " + tarifDeveloppement.Zone + " modifier!";
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
                strIndication = "Le zone " + tarifDeveloppement.Zone + " à déjà un tarif de développement!";
                this.divIndicationText(strIndication, "Red");
            }
            #endregion
        }
        #endregion
    }
}