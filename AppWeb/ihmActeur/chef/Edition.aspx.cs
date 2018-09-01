using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.chef
{
    public partial class Edition : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalVoyage serviceVoyage = null;
        IntfDalCommission serviceCommission = null;
        IntfDalAutorisationDepart serviceAutorisationDepart = null;
        IntfDalRecuAD serviceRecuAD = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalPrelevement servicePrelevement = null;
        IntfDalLien serviceLien = null;

        crlAgent agent = null;
        crlAutorisationDepart autorisationDepart = null;
        crlFicheBord ficheDebord = null;
        string numAutorisationDepart = null;
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
            serviceFicheBord = new ImplDalFicheBord();
            serviceVoyage = new ImplDalVoyage();
            serviceCommission = new ImplDalCommission();
            serviceAutorisationDepart = new ImplDalAutorisationDepart();
            serviceRecuAD = new ImplDalRecuAD();
            serviceGeneral = new ImplDalGeneral();
            servicePrelevement = new ImplDalPrelevement();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                numAutorisationDepart = Request.QueryString["numerosAD"];

                if (numAutorisationDepart != null)
                {
                    this.afficheAutorisationVoyage(numAutorisationDepart);
                    this.initialiseGridCommission();
                    this.initialiseGridPassager();
                    this.initialiseGridRecuAD();
                    this.initialiseGridPrelevement();
                    this.initialiseLabel();

                    this.initialiseGridAD();
                }
                else
                {
                    hfAutorisationDepart.Value = "";

                    this.initialiseGridAD();
                }


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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "401"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void afficheAutorisationVoyage(string numAutorisationDepart)
        {
            if (numAutorisationDepart != "")
            {
                autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(numAutorisationDepart);


                if (autorisationDepart != null)
                {
                    ficheDebord = autorisationDepart.ficheBord;

                    hfAutorisationDepart.Value = autorisationDepart.NumAutorisationDepart;

                    if (ficheDebord != null)
                    {
                        hfNumerosFB.Value = ficheDebord.NumerosFB;

                        labNumFB.Text = ficheDebord.NumerosFB;
                        labDateHeureFB.Text = ficheDebord.DateHeurPrevue.ToString("dd MMMM yyyy à HH:mm");

                        labNumAV.Text = ficheDebord.autorisationVoyage.NumerosAV;
                        labDateAV.Text = ficheDebord.autorisationVoyage.DatePrevueDepart.ToString("dd MMMM yyyy");

                        labNomChauffeur.Text = ficheDebord.autorisationVoyage.Verification.Chauffeur.nomChauffeur;
                        labPrenomChauffeur.Text = ficheDebord.autorisationVoyage.Verification.Chauffeur.prenomChauffeur;

                        labMatriculeVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;
                        labMarqueVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule;
                        labCouleurVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule;
                        labPoidsAutoriseVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.PoidsTotalVehicule + "Kg";

                        labItineraire.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + ficheDebord.autorisationVoyage.Verification.Itineraire.villeF.NomVille;
                        labDistance.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.DistanceParcour + "Km";
                        labDureeTrajet.Text = serviceGeneral.getTextTimeSpan(ficheDebord.autorisationVoyage.Verification.Itineraire.DureeTrajet);
                        labNombreRepos.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.NombreRepos.ToString();

                        labMotant.Text = serviceGeneral.separateurDesMilles(autorisationDepart.RecetteTotale.ToString("0")) + "Ar";
                        labReste.Text = serviceGeneral.separateurDesMilles(autorisationDepart.ResteRegle.ToString("0")) + "Ar";

                        LabTotalPrelevement.Text = serviceGeneral.separateurDesMilles(serviceAutorisationDepart.getMontanPrelevement(autorisationDepart.NumAutorisationDepart).ToString("0")) + "Ar";
                        LabTotalRecu.Text = serviceGeneral.separateurDesMilles(serviceAutorisationDepart.getMontanRecu(autorisationDepart.NumAutorisationDepart).ToString("0")) + "Ar";
                    }

                }
                else
                {
                    Response.Redirect("PlanningCalendarJour.aspx");
                }
            }
        }

        private void initialiseGridPassager()
        {
            serviceVoyage.insertToGrigVoyageAutorisationDepart(gvPassager, ddlTriListePassager.SelectedValue, ddlTriListePassager.SelectedValue, TextRechercheListePassager.Text, hfNumerosFB.Value);
        }

        private void initialiseGridCommission()
        {
            serviceCommission.insertToGridCommissionAutorisationDepart(gvCommission, ddlTriCommission.SelectedValue, ddlTriCommission.SelectedValue, TextRechercheCommission.Text, hfNumerosFB.Value);
        }

        private void initialiseLabel()
        {
            labNbPassager.Text = serviceFicheBord.getNombreTotalPassager(hfNumerosFB.Value).ToString();
            labPoidTotalBagage.Text = serviceGeneral.separateurDesMilles(serviceFicheBord.getPoidTotalBagage(hfNumerosFB.Value).ToString("0")) + "Kg";
            labSommeRecu.Text = serviceGeneral.separateurDesMilles((serviceFicheBord.getPrixTotalBillet(hfNumerosFB.Value) + serviceFicheBord.getPrixTotalBagage(hfNumerosFB.Value)).ToString("0")) + "Ar";

            labPoidTotalCommission.Text = serviceGeneral.separateurDesMilles(serviceFicheBord.getPoidTotalCommission(hfNumerosFB.Value).ToString("0")) + "Kg";
            labTotalFraisCommission.Text = serviceGeneral.separateurDesMilles(serviceFicheBord.getPrixTotalCommission(hfNumerosFB.Value).ToString("0")) + "Ar";
        }

        private void initialiseGridAD()
        {
            serviceAutorisationDepart.insertToGridAutorisationDepart(gvAutorisationDeVoyage, ddlTriRechercheAD.SelectedValue, ddlTriRechercheAD.SelectedValue, TextRechercheAD.Text, agent.agence.NumAgence);
        }

        private void initialiseGridRecuAD()
        {
            if (hfAutorisationDepart.Value != "")
            {
                serviceRecuAD.insertToGridAvanceAutorisationDepart(gvRecuAD, hfAutorisationDepart.Value);
            }
        }

        private void initialiseGridPrelevement()
        {
            if (hfAutorisationDepart.Value != "")
            {
                servicePrelevement.insertToGridPrelevement(gvPrelevement, hfAutorisationDepart.Value);
            }
        }
        #endregion

        #region event
        protected void ddlTriListePassager_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridPassager();
        }
        protected void btnRecherchePassager_Click(object sender, EventArgs e)
        {
            this.initialiseGridPassager();
        }

        protected void ddlTriCommission_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }
        protected void btnRechercheCommission_Click(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }

        protected void gvAutorisationDeVoyage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAutorisationDeVoyage.PageIndex = e.NewPageIndex;
            this.initialiseGridAD();
        }
        protected void gvAutorisationDeVoyage_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("select"))
            {
                this.afficheAutorisationVoyage(e.CommandArgument.ToString());
                this.initialiseGridCommission();
                this.initialiseGridPassager();
                this.initialiseGridRecuAD();
                this.initialiseGridPrelevement();
                this.initialiseLabel();
            }
            else if (e.CommandName.Equals("selectPdf"))
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                    string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=AD&numerosAD=" + e.CommandArgument.ToString(), 700,
                       500, 10, 10), true);
                }
            }
        }

        protected void btnEditerFicheBord_Click(object sender, EventArgs e)
        {
            if (hfAutorisationDepart.Value != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=AD&numerosAD=" + hfAutorisationDepart.Value, 700,
                   500, 10, 10), true);
            }
        }

        protected void ddlTriRechercheAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAD();
        }
        protected void btnRechercheAD_Click(object sender, EventArgs e)
        {
            this.initialiseGridAD();
        }
        protected void gvPassager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPassager.PageIndex = e.NewPageIndex;
            this.initialiseGridPassager();
        }
        protected void gvCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommission.PageIndex = e.NewPageIndex;
            this.initialiseGridCommission();
        }
        protected void gvRecuAD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecuAD.PageIndex = e.NewPageIndex;
            this.initialiseGridRecuAD();
        }
        protected void gvPrelevement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrelevement.PageIndex = e.NewPageIndex;
            this.initialiseGridPrelevement();
        }
        #endregion
    }
}