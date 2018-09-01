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
    public partial class RouteNationale : System.Web.UI.Page
    {
        #region declaration
        IntfDalServiceRessource serviceRessource = null;
        IntfDalRouteNationale serviceRouteNationale = null;
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
            serviceRouteNationale = new ImplDalRouteNationale();
            serviceVille = new ImplDalVille();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseFormulaire();
                this.initialiseErrorMassage();
                this.initialiseGridRN();

                this.initialiseGridVille();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "022"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
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

        private void initialiseErrorMassage()
        {
            TextRN_RequiredFieldValidator.ErrorMessage = ReRouteNationale.RNNonVide;
            TextDistance_RequiredFieldValidator.ErrorMessage = ReRouteNationale.DistanceNonVide;
        }

        private void initialiseGridRN()
        {
            serviceRouteNationale.insertToGridRouteNationale(gvRN, ddlTriRN.SelectedValue, ddlTriRN.SelectedValue, TextRechercheRN.Text);
        }

        private void afficheRN(string routeNationale)
        {
            #region declaration
            crlRouteNationale routeNationaleObj = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (routeNationale != "")
            {
                routeNationaleObj = serviceRouteNationale.selectRouteNationale(routeNationale);

                if (routeNationaleObj != null)
                {
                    TextRN.Text = routeNationaleObj.RouteNationale;
                    TextDistance.Text = routeNationaleObj.DistanceRN.ToString("0");
                    LabRN.Text = routeNationaleObj.RouteNationale;

                    strConfirm = "Voulez vous vraiment modifier la route nationale " + routeNationaleObj.RouteNationale + "?";
                    btnModifier_ConfirmButtonExtender.ConfirmText = strConfirm;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;
                }
            }
            #endregion
        }

        private void initialiseFormulaire()
        {
            TextDistance.Text = "";
            TextRN.Text = "";
            LabRN.Text = "";
            btnModifier_ConfirmButtonExtender.ConfirmText = "";
            btnModifier.Enabled = false;
            btnValider.Enabled = true;
            this.initialiseGridVilleRN();
            this.initialiseGridVille();
            this.divIndicationText("", "Red");
        }

        private void initialiseGridVille()
        {
            #region declaration
            List<crlVille> villes = null;
            #endregion

            #region implementation
            villes = serviceVille.selectVillesForRN(LabRN.Text);
            serviceVille.insertToGridVilleNotRN(gvListeVille, ddlTriVille.SelectedValue, ddlTriVille.SelectedValue, TextRechercheVille.Text, villes);
            #endregion

        }

        private void initialiseGridVilleRN()
        {
            serviceVille.insertToGridVilleRN(gvListeVilleRN, ddlTriVilleRN.SelectedValue, ddlTriVilleRN.SelectedValue, TextRechercheVilleRN.Text, LabRN.Text);
        }
        #endregion

        #region event
        protected void ddlTriRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridRN();
        }
        protected void btnRechercheRN_Click(object sender, EventArgs e)
        {
            this.initialiseGridRN();
        }

        protected void gvRN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRN.PageIndex = e.NewPageIndex;
            this.initialiseGridRN();
        }
        protected void gvRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheRN(e.CommandArgument.ToString());
                this.initialiseGridVilleRN();
                this.initialiseGridVille();
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceRouteNationale.deleteRouteNationale(e.CommandArgument.ToString()))
                {
                    this.initialiseFormulaire();
                    this.initialiseGridRN();
                    this.initialiseGridVille();
                    this.initialiseGridVilleRN();
                }
                else
                {
                    //
                }
            }
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlRouteNationale routeNationale = new crlRouteNationale();
            string strRouteNationale = "";
            string strIndication = "";
            #endregion

            #region implementation
            routeNationale.RouteNationale = TextRN.Text;
            routeNationale.DistanceRN = double.Parse(TextDistance.Text);

            if (!serviceRouteNationale.isRouteNationale(routeNationale))
            {
                strRouteNationale = serviceRouteNationale.insertRouteNationale(routeNationale);
                if (strRouteNationale != "")
                {
                    this.afficheRN(strRouteNationale);
                    this.initialiseGridVille();
                    this.initialiseGridVilleRN();
                    this.initialiseGridRN();

                    strIndication = "La route nationale " + strRouteNationale + " est bien enregistrer!<br/>";
                    strIndication += "Maintenant sélectionner les villes de la route nationale " + strRouteNationale + "!";
                    this.divIndicationText(strIndication, "black");
                }
                else
                {
                    //erreu pandant insertion
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strIndication, "red");
                }
            }
            else
            {
                strIndication = "La route nationale " + routeNationale.RouteNationale + " est déjà dans la base de données!";
                this.divIndicationText(strIndication, "red");
                //route nationale deja dans la base
            }
            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlRouteNationale routeNationale = new crlRouteNationale();
            string strRouteNationale = "";
            #endregion

            #region implementation
            if (LabRN.Text != "")
            {
                routeNationale = serviceRouteNationale.selectRouteNationale(LabRN.Text);

                if (routeNationale != null)
                {
                    routeNationale.RouteNationale = TextRN.Text;
                    routeNationale.DistanceRN = double.Parse(TextDistance.Text);

                    strRouteNationale = serviceRouteNationale.isRouteNationale(routeNationale, LabRN.Text);

                    if (strRouteNationale.Equals(""))
                    {
                        if (serviceRouteNationale.updateRouteNationale(routeNationale, LabRN.Text))
                        {
                            this.afficheRN(routeNationale.RouteNationale);
                            this.initialiseGridVille();
                            this.initialiseGridVilleRN();
                            this.initialiseGridRN();
                        }
                        else
                        {
                            //
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

        protected void btnRechercheVilleRN_Click(object sender, EventArgs e)
        {
            this.initialiseGridVilleRN();
        }
        protected void ddlTriVilleRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridVilleRN();
        }
        protected void ddlTriVille_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridVille();
        }
        protected void btnRechercheVille_Click(object sender, EventArgs e)
        {
            this.initialiseGridVille();
        }

        protected void gvListeVille_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListeVille.PageIndex = e.NewPageIndex;
            this.initialiseGridVille();
        }
        protected void gvListeVille_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                if (LabRN.Text != "")
                {
                    serviceRouteNationale.insertAssocVilleRouteNationale(e.CommandArgument.ToString(), LabRN.Text);
                    this.initialiseGridVilleRN();
                    this.initialiseGridVille();
                }
                else
                {
                    //selectioné ou valiD un route nationale
                }
            }
        }
        protected void gvListeVilleRN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListeVilleRN.PageIndex = e.NewPageIndex;
            this.initialiseGridVilleRN();
        }
        protected void gvListeVilleRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteV"))
            {
                if (LabRN.Text != "")
                {
                    serviceRouteNationale.deleteAssocVilleRouteNationale(e.CommandArgument.ToString(), LabRN.Text);
                    this.initialiseGridVilleRN();
                    this.initialiseGridVille();
                }
            }
        }
        #endregion
    }
}