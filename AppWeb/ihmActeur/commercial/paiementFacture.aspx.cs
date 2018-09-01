using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.commercial
{
    public partial class paiementFacture : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalFacture serviceFacture = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalRecuAD serviceRecuAD = null;
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
            serviceFacture = new ImplDalFacture();
            serviceGeneral = new ImplDalGeneral();
            serviceRecuAD = new ImplDalRecuAD();
            #endregion

            #region
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                this.initialiseGridFacture();
                this.initialiseFormulaire();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "043"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridFacture()
        {
            serviceFacture.insertToGridFactureNotRecu(gvFacture, ddlTriFacture.SelectedValue, ddlTriFacture.SelectedValue, TextRechercheFacture.Text);
        }

        private void afficheFacture(string numFacture)
        {
            #region declaration
            crlFacture facture = null;
            crlProprietaire proprietaire = null;
            #endregion

            #region implementation
            if (numFacture != "")
            {
                facture = serviceFacture.selectFacture(numFacture);
                if (facture != null)
                {
                    hfNumFacture.Value = facture.NumFacture;
                    LabNumFacture.Text = "N°" + facture.NumFacture;

                    if (facture.autorisationDeparts.Count > 0)
                    {
                        proprietaire = facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire;

                        TextMontantFacture.Text = serviceGeneral.separateurDesMilles(facture.Montant);
                        TextDate.Text = facture.DateFacturation.ToString("dd MMMM yyyy");

                        if (proprietaire.Individu != null)
                        {
                            LabelNomIndividu.Text = proprietaire.Individu.NomIndividu;
                            LabelPrenomIndividu.Text = proprietaire.Individu.PrenomIndividu;
                            LabelCINIndividu.Text = proprietaire.Individu.CinIndividu;
                            LabelAdresseIndividu.Text = proprietaire.Individu.Adresse;
                            LabelFixeIndividu.Text = proprietaire.Individu.TelephoneFixeIndividu;
                            LabelMobileIndividu.Text = proprietaire.Individu.TelephoneMobileIndividu;

                            Panel_Individu.Visible = true;
                        }
                        else
                        {
                            Panel_Individu.Visible = false;
                        }

                        if (proprietaire.organisme != null)
                        {

                            LabelAdresseOrganisme.Text = proprietaire.organisme.AdresseOrganisme;
                            LabelFixeOrganisme.Text = proprietaire.organisme.TelephoneFixeOrganisme;
                            LabelMailOrganisme.Text = proprietaire.organisme.MailOrganisme;
                            LabelMobileOrganisme.Text = proprietaire.organisme.TelephoneMobileOrganisme;
                            LabelNomOrganisme.Text = proprietaire.organisme.NomOrganisme;


                            if (proprietaire.organisme.individuResponsable != null)
                            {
                                LabelAdresseRespOrganisme.Text = proprietaire.organisme.individuResponsable.Adresse;
                                LabelCINRespOrganisme.Text = proprietaire.organisme.individuResponsable.CinIndividu;
                                LabelFixeRespOrganisme.Text = proprietaire.organisme.individuResponsable.TelephoneFixeIndividu;
                                LabelMobileRespOrganisme.Text = proprietaire.organisme.individuResponsable.TelephoneMobileIndividu;
                                LabelNomRespOrganisme.Text = proprietaire.organisme.individuResponsable.NomIndividu;
                                LabelPrenomRespOrganisme.Text = proprietaire.organisme.individuResponsable.PrenomIndividu;
                            }

                            Panel_Organisme.Visible = true;
                        }
                        else
                        {
                            Panel_Organisme.Visible = false;
                        }

                        if (proprietaire.societe != null)
                        {


                            LabelAdresseSociete.Text = proprietaire.societe.AdresseSociete;
                            LabelFixeSociete.Text = proprietaire.societe.TelephoneFixeSociete;
                            LabelMailSociete.Text = proprietaire.societe.MailSociete;
                            LabelMobileSociete.Text = proprietaire.societe.TelephoneMobileSociete;
                            LabelNomSociete.Text = proprietaire.societe.NomSociete;
                            LabelSecteurActiviteSociete.Text = proprietaire.societe.SecteurActiviteSociete;

                            if (proprietaire.societe.individuResponsable != null)
                            {
                                LabelAdresseRespSociete.Text = proprietaire.societe.individuResponsable.Adresse;
                                LabelCINRespSociete.Text = proprietaire.societe.individuResponsable.CinIndividu;
                                LabelFixeRespSociete.Text = proprietaire.societe.individuResponsable.TelephoneFixeIndividu;
                                LabelMobileRespSociete.Text = proprietaire.societe.individuResponsable.TelephoneMobileIndividu;
                                LabelNomRespSociete.Text = proprietaire.societe.individuResponsable.NomIndividu;
                                LabelPrenomRespSociete.Text = proprietaire.societe.individuResponsable.PrenomIndividu;
                            }

                            Panel_Societe.Visible = true;
                        }
                        else
                        {
                            Panel_Societe.Visible = false;
                        }
                    }
                }
            }
            #endregion
        }

        private void initialiseFormulaire()
        {

            TextDate.Text = "";
            TextMontantFacture.Text = "";

            hfNumFacture.Value = "";

            LabelNomIndividu.Text = "";
            LabelPrenomIndividu.Text = "";
            LabelCINIndividu.Text = "";
            LabelAdresseIndividu.Text = "";
            LabelFixeIndividu.Text = "";
            LabelMobileIndividu.Text = "";

            LabelAdresseOrganisme.Text = "";
            LabelAdresseRespOrganisme.Text = "";
            LabelCINRespOrganisme.Text = "";
            LabelFixeOrganisme.Text = "";
            LabelFixeRespOrganisme.Text = "";
            LabelMailOrganisme.Text = "";
            LabelMobileOrganisme.Text = "";
            LabelMobileRespOrganisme.Text = "";
            LabelNomOrganisme.Text = "";
            LabelNomRespOrganisme.Text = "";
            LabelPrenomRespOrganisme.Text = "";

            LabelAdresseRespSociete.Text = "";
            LabelAdresseSociete.Text = "";
            LabelCINRespSociete.Text = "";
            LabelFixeRespSociete.Text = "";
            LabelFixeSociete.Text = "";
            LabelMailSociete.Text = "";
            LabelMobileRespSociete.Text = "";
            LabelMobileSociete.Text = "";
            LabelNomRespSociete.Text = "";
            LabelNomSociete.Text = "";
            LabelPrenomRespSociete.Text = "";
            LabelSecteurActiviteSociete.Text = "";


            Panel_Individu.Visible = false;
            Panel_Organisme.Visible = false;
            Panel_Societe.Visible = false;
        }

        private void initialiseErrorMessage()
        {

        }
        #endregion


        #region event
        protected void gvFacture_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacture.PageIndex = e.NewPageIndex;
            this.initialiseGridFacture();
        }
        protected void gvFacture_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheFacture(e.CommandArgument.ToString());
            }
        }
        protected void btnRechercheFacture_Click(object sender, EventArgs e)
        {
            this.initialiseGridFacture();
        }
        protected void ddlTriFacture_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridFacture();
        }
        #endregion


        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlRecuAD recuAD = null;
            #endregion

            #region implementation
            if (hfNumFacture.Value != "")
            {
                recuAD = new crlRecuAD();
                recuAD.Libele = "Paiement transporteur.";
                recuAD.MatriculeAgent = agent.matriculeAgent;
                recuAD.Montant = TextMontantFacture.Text.Replace(" ", "");
                recuAD.NumFacture = hfNumFacture.Value;
                recuAD.agent = agent;

                recuAD.NumRecuAD = serviceRecuAD.insertRecuAD(recuAD);
                if (recuAD.NumRecuAD != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                        string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=recuAD&numRecuAD=" + recuAD.NumRecuAD, 700,
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
            #endregion
        }
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
    }
}