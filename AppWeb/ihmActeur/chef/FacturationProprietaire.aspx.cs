using AppRessources.Ressources;
using arc.utile;
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
    public partial class FacturationProprietaire : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAutorisationDepart serviceAutorisationDepart = null;
        IntfDalFacture serviceFacture = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalVehicule serviceVehicule = null;
        IntfDalLien serviceLien = null;

        Convertisseuse convertisseuse = null;

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
            serviceAutorisationDepart = new ImplDalAutorisationDepart();
            serviceFacture = new ImplDalFacture();
            serviceGeneral = new ImplDalGeneral();
            serviceVehicule = new ImplDalVehicule();

            convertisseuse = new Convertisseuse();
            #endregion

            #region !isPostBack
            if (!IsPostBack)
            {
                this.initialiseGridVehicule();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "432"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridListeVoyage()
        {

            serviceVehicule.insertToGridADForVehicule(gvListeVoyage, ddlTriVoyage.SelectedValue, ddlTriVoyage.SelectedValue, TextRechercheVoyage.Text, hfNumVehicule.Value);

        }

        private void initialiseGridListeRecu()
        {

            serviceVehicule.insertToGridRecuForVehicule(gvRecu, ddlTriRecu.SelectedValue, ddlTriRecu.SelectedValue, TextRechercheRecu.Text, hfNumVehicule.Value);
        }

        private void initialiseGridVehicule()
        {
            serviceVehicule.insertToGridVehiculeForFacture(gvVehicule, ddlTriVehicule.SelectedValue, ddlTriVehicule.SelectedValue, TextRechercheVehicule.Text);
        }

        private void afficheVehicule(string numVehicule)
        {
            #region declaration
            crlVehicule vehicule = null;
            convertisseuse = new Convertisseuse();
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                vehicule = serviceVehicule.selectVehicule(numVehicule);

                if (vehicule != null)
                {
                    LabelMatricule.Text = vehicule.MatriculeVehicule;
                    LabelMarque.Text = vehicule.MarqueVehicule;
                    LabelCouleur.Text = vehicule.CouleurVehicule;
                    hfNumVehicule.Value = vehicule.NumVehicule;

                    if (vehicule.proprietaire != null)
                    {
                        if (vehicule.proprietaire.Individu != null)
                        {
                            LabelNomIndividu.Text = vehicule.proprietaire.Individu.NomIndividu;
                            LabelPrenomIndividu.Text = vehicule.proprietaire.Individu.PrenomIndividu;
                            LabelCINIndividu.Text = vehicule.proprietaire.Individu.CinIndividu;
                            LabelFixeIndividu.Text = vehicule.proprietaire.Individu.TelephoneFixeIndividu;
                            LabelMobileIndividu.Text = vehicule.proprietaire.Individu.TelephoneMobileIndividu;

                            Panel_Individu.Visible = true;
                        }
                        else
                        {
                            Panel_Individu.Visible = false;
                        }

                        if (vehicule.proprietaire.societe != null)
                        {
                            LabelSociete.Text = vehicule.proprietaire.societe.NomSociete;
                            LabelFixeSociete.Text = vehicule.proprietaire.societe.TelephoneFixeSociete;
                            LabelMobileSociete.Text = vehicule.proprietaire.societe.TelephoneMobileSociete;

                            if (vehicule.proprietaire.societe.individuResponsable != null)
                            {
                                LabelNomRespSociete.Text = vehicule.proprietaire.societe.individuResponsable.NomIndividu;
                                LabelPrenomRespSociete.Text = vehicule.proprietaire.societe.individuResponsable.PrenomIndividu;
                                LabelFixeRespSociete.Text = vehicule.proprietaire.societe.individuResponsable.TelephoneFixeIndividu;
                                LabelMobileRespSociete.Text = vehicule.proprietaire.societe.individuResponsable.TelephoneMobileIndividu;
                            }

                            Panel_Societe.Visible = true;
                        }
                        else
                        {
                            Panel_Societe.Visible = false;
                        }

                        if (vehicule.proprietaire.organisme != null)
                        {
                            LabelOrganisme.Text = vehicule.proprietaire.organisme.NomOrganisme;
                            LabelFixeOrganisme.Text = vehicule.proprietaire.organisme.TelephoneFixeOrganisme;
                            LabelMobileOrganisme.Text = vehicule.proprietaire.organisme.TelephoneMobileOrganisme;

                            if (vehicule.proprietaire.organisme.individuResponsable != null)
                            {
                                LabelNomRespOrganisme.Text = vehicule.proprietaire.organisme.individuResponsable.NomIndividu;
                                LabelPrenomRespOrganisme.Text = vehicule.proprietaire.organisme.individuResponsable.PrenomIndividu;
                                LabelFixeRespOrganisme.Text = vehicule.proprietaire.organisme.individuResponsable.TelephoneFixeIndividu;
                                LabelMobileRespOrganisme.Text = vehicule.proprietaire.organisme.individuResponsable.TelephoneMobileIndividu;
                            }

                            Panel_Organisme.Visible = true;
                        }
                        else
                        {
                            Panel_Organisme.Visible = false;
                        }

                        this.initialiseGridListeVoyage();
                        this.initialiseGridListeRecu();

                        LabTotalRecettes.Text = serviceGeneral.separateurDesMilles(serviceVehicule.getTotalRecette(vehicule.NumVehicule).ToString("0")) + "Ar";
                        LabTotalReste.Text = serviceGeneral.separateurDesMilles(serviceVehicule.getTotalReste(vehicule.NumVehicule).ToString("0")) + "Ar";
                        LabTotalMontantRecu.Text = serviceGeneral.separateurDesMilles(serviceVehicule.getTotalRecu(vehicule.NumVehicule).ToString("0")) + "Ar";

                        LabMontantTotalFacture.Text = serviceGeneral.separateurDesMilles(serviceVehicule.getTotalReste(vehicule.NumVehicule).ToString("0")) + "Ar";
                        LabMontantTotalFactutreLettre.Text = convertisseuse.convertion(serviceVehicule.getTotalReste(vehicule.NumVehicule).ToString("0")) + " Ariary";
                    }
                }
            }
            #endregion
        }
        #endregion

        #region event


        protected void gvListeVoyage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListeVoyage.PageIndex = e.NewPageIndex;
            this.initialiseGridListeVoyage();
        }
        protected void ddlTriVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridListeVoyage();
        }
        protected void btnRechercheVoyage_Click(object sender, EventArgs e)
        {
            this.initialiseGridListeVoyage();
        }

        protected void ddlTriRecu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridListeRecu();
        }
        protected void btnRechercheRecu_Click(object sender, EventArgs e)
        {
            this.initialiseGridListeRecu();
        }
        protected void gvRecu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecu.PageIndex = e.NewPageIndex;
            this.initialiseGridListeRecu();
        }

        protected void btnFacturer_Click(object sender, EventArgs e)
        {
            #region declaration
            crlFacture facture = null;
            double montantTotalRecettes = 0.00;
            #endregion

            #region implementation
            if (hfNumVehicule.Value != "")
            {
                montantTotalRecettes = serviceVehicule.getTotalReste(hfNumVehicule.Value);

                if (montantTotalRecettes > 0)
                {
                    facture = new crlFacture();
                    facture.agent = agent;
                    facture.MatriculeAgent = agent.matriculeAgent;
                    facture.DateFacturation = DateTime.Now;
                    facture.Libele = ReLibeleFacture.libeleFactureForProprietaire;
                    facture.Montant = montantTotalRecettes.ToString("0");
                    facture.autorisationDeparts = serviceVehicule.getAutorisationDepartsForFacture(hfNumVehicule.Value);

                    facture.NumFacture = serviceFacture.insertFactureAssoc(facture);

                    if (facture.NumFacture != "")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                        string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=facture&numFacture=" + facture.NumFacture, 700,
                           500, 10, 10), true);
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
            else
            {
                //
            }
            #endregion

            /*
            #region implementation
            if (hfNumProprietaire.Value != "")
            {
                montantTotalRecettes = serviceProprietaire.getTotalReste(hfNumProprietaire.Value);

                facture = new crlFacture();
                facture.agent = agent;
                facture.MatriculeAgent = agent.matriculeAgent;
                facture.autorisationDeparts = serviceAutorisationDepart.selectADProprietaireResteNonNull(hfNumProprietaire.Value);
                facture.DateFacturation = DateTime.Now;
                facture.Libele = ReLibeleFacture.libeleFactureForProprietaire;
                facture.Montant = montantTotalRecettes.ToString("0");

                facture.NumFacture = serviceFacture.insertFactureAssoc(facture);

                if (facture.NumFacture != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                    string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=facture&numFacture=" + facture.NumFacture, 700,
                       500, 10, 10), true);
                }
            }
            #endregion
             * */
        }
        #endregion

        protected void ddlTriVehicule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridVehicule();
        }
        protected void btnRechercheVehicule_Click(object sender, EventArgs e)
        {
            this.initialiseGridVehicule();
        }
        protected void gvVehicule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehicule.PageIndex = e.NewPageIndex;
            this.initialiseGridVehicule();
        }
        protected void gvVehicule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheVehicule(e.CommandArgument.ToString());
            }
        }
    }
}