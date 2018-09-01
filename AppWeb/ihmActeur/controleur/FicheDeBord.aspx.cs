using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.controleur
{
    public partial class FicheDeBord : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;

        IntfDalCommission serviceCommission = null;
        IntfDalReceptionnaire servicePersonne = null;
        IntfDalRecu serviceRecu = null;
        IntfDalClient serviceClient = null;
        IntfDalTypeCommission serviceTypeCommission = null;
        IntfDalBillet serviceBillet = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalPlaceFB servicePlaceFB = null;
        IntfDalVoyage serviceVoyage = null;
        IntfDalLien serviceLien = null;

        crlAgent agent = null;
        string numerosFB = null;
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
            serviceCommission = new ImplDalCommission();
            servicePersonne = new ImplDalReceptionnaire();
            serviceRecu = new ImplDalRecu();
            serviceClient = new ImplDalClient();
            serviceTypeCommission = new ImplDalTypeCommission();
            serviceBillet = new ImplDalBillet();
            serviceFicheBord = new ImplDalFicheBord();
            servicePlaceFB = new ImplDalPlaceFB();
            serviceVoyage = new ImplDalVoyage();
            #endregion

            #region !IsPostBack 
            if (!IsPostBack)
            {
                this.initialiseGVListeFB();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "302"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGVListeFB()
        {
            serviceFicheBord.insertToGridFicheBordSansAD(gvFicheBord, ddlTriListeFB.SelectedValue, ddlTriListeFB.SelectedValue, TextRechercheFB.Text, agent.numAgence);
        }

        #endregion

        #region event

        protected void gvFicheBord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFicheBord.PageIndex = e.NewPageIndex;
            this.initialiseGVListeFB();
        }
        protected void gvFicheBord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                Response.Redirect("RemplirFB.aspx?numerosFB=" + e.CommandArgument.ToString());
            }
        }
        protected void ddlTriListeFB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGVListeFB();
        }
        protected void bvtnRechercheFB_Click(object sender, EventArgs e)
        {
            this.initialiseGVListeFB();
        }
        #endregion
    }
}