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

namespace AppWeb.ihmActeur.administrateur
{
    public partial class SessionAgenceStart : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAgence serviceAgence = null;
        IntfDalSessionAgence serviceSessionAgence = null;
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
            serviceAgence = new ImplDalAgence();
            serviceSessionAgence = new ImplDalSessionAgence();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseGridAgence();

                Panel_BilletMontantTotal.Visible = false;
                Panel_CommissionMontantTotal.Visible = false;
                Panel_DureeAbonnementMontantTotal.Visible = false;
                Panel_MontantTotalSessionCaisse.Visible = false;
                Panel_RecuEncaisserMontantTotal.Visible = false;
                Panel_VoyageAbonnementMontantTotal.Visible = false;
                Panel_RecuADTotal.Visible = false;
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "422"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridAgence()
        {
            serviceAgence.insertToGridAgenceListe(gvAgence, ddlTriAgence.SelectedValue, ddlTriAgence.SelectedValue, TextRechercheAgence.Text);
        }

        private void afficheAgence(string numAgence)
        {
            #region declaration
            crlAgence agence = null;
            #endregion

            #region implementation
            if (numAgence != "")
            {
                agence = serviceAgence.selectAgence(numAgence);

                if (agence != null)
                {
                    LabelLocalisation.Text = agence.LocalisationAgence;
                    LabelNomGare.Text = agence.NomAgence;
                    LabelSigle.Text = agence.SigleAgence;
                    LabelType.Text = agence.TypeAgence;
                    if (agence.ville != null)
                    {
                        LabelVille.Text = agence.ville.NomVille;
                    }
                    hfNumAgence.Value = agence.NumAgence;

                    if (agence.sessionAgence != null)
                    {
                        LabelSessionStatu.Text = "Session ouverte";
                        imageStatut.ImageUrl = "~/CssStyle/images/vert.png";

                        btnOuvrirSession.Enabled = false;
                        btnFermerSession.Enabled = true;

                        this.afficheMontantTotal(agence.sessionAgence.NumSessionAgence);
                    }
                    else
                    {
                        LabelSessionStatu.Text = "Session fermée";
                        imageStatut.ImageUrl = "~/CssStyle/images/rouge.png";

                        btnOuvrirSession.Enabled = true;
                        btnFermerSession.Enabled = false;

                        Panel_BilletMontantTotal.Visible = false;
                        Panel_CommissionMontantTotal.Visible = false;
                        Panel_DureeAbonnementMontantTotal.Visible = false;
                        Panel_MontantTotalSessionCaisse.Visible = false;
                        Panel_RecuEncaisserMontantTotal.Visible = false;
                        Panel_VoyageAbonnementMontantTotal.Visible = false;
                        Panel_RecuADTotal.Visible = false;
                    }
                }
            }
            #endregion
        }

        private void afficheMontantTotal(string numSessionAgence)
        {
            #region declaration
            double montantBillet = 0;
            double montantCommission = 0;
            double montantDureeAbonnement = 0;
            double montantVoyageAbonnement = 0;
            double montantRecuEncaisser = 0;
            double montantRecuEncaisserCheque = 0;
            double montantTotalSession = 0;
            double montantTotalSessionCheque = 0;
            double montantRecuAD = 0;

            crlSessionAgence sessionAgence = null;
            Convertisseuse convertisseuse = new Convertisseuse();
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                sessionAgence = serviceSessionAgence.selectSessionAgence(numSessionAgence);
                montantBillet = serviceSessionAgence.getMontantTotalBillet(numSessionAgence);
                montantCommission = serviceSessionAgence.getMontantTotalCommission(numSessionAgence);
                montantDureeAbonnement = serviceSessionAgence.getMontantTotalDureeAbonnement(numSessionAgence);
                montantRecuEncaisser = serviceSessionAgence.getMontantTotalRecuEncaisserEspece(numSessionAgence);
                montantRecuEncaisserCheque = serviceSessionAgence.getMontantTotalRecuEncaisserCheque(numSessionAgence);
                montantVoyageAbonnement = serviceSessionAgence.getMontantTotalVoyageAbonnement(numSessionAgence);
                montantRecuAD = serviceSessionAgence.getMontantTotalRecuAD(numSessionAgence);

                montantTotalSession = montantBillet + montantCommission + montantDureeAbonnement + montantRecuEncaisser + montantVoyageAbonnement - montantRecuAD;
                montantTotalSessionCheque = montantRecuEncaisserCheque;

                if (montantBillet > 0)
                {
                    Panel_BilletMontantTotal.Visible = true;
                    LabelMontantTotalBillet.Text = serviceGeneral.separateurDesMilles(montantBillet.ToString("0")) + "Ar";
                    LabelMontantTotalLettreBillet.Text = convertisseuse.convertion(montantBillet.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_BilletMontantTotal.Visible = false;
                    LabelMontantTotalBillet.Text = "0Ar";
                    LabelMontantTotalLettreBillet.Text = "Zéro Ariary";
                }

                if (montantCommission > 0)
                {
                    Panel_CommissionMontantTotal.Visible = true;
                    LabelMontantTotalCommission.Text = serviceGeneral.separateurDesMilles(montantCommission.ToString("0")) + "Ar";
                    LabelMontantTotalCommissionLettre.Text = convertisseuse.convertion(montantCommission.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_CommissionMontantTotal.Visible = false;
                    LabelMontantTotalCommission.Text = "0Ar";
                    LabelMontantTotalCommissionLettre.Text = "Zéro Ariary";
                }

                if (montantDureeAbonnement > 0)
                {
                    Panel_DureeAbonnementMontantTotal.Visible = true;
                    LabelMotantTotalDureeAbonnement.Text = serviceGeneral.separateurDesMilles(montantDureeAbonnement.ToString("0")) + "Ar";
                    LabelMontantTotalDureeAbonnementLettre.Text = convertisseuse.convertion(montantDureeAbonnement.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_DureeAbonnementMontantTotal.Visible = false;
                    LabelMotantTotalDureeAbonnement.Text = "0Ar";
                    LabelMontantTotalDureeAbonnementLettre.Text = "Zéro Ariary";
                }

                if (montantRecuEncaisser > 0 || montantRecuEncaisserCheque > 0)
                {
                    Panel_RecuEncaisserMontantTotal.Visible = true;
                    LabelMontantTotalRecuEncaisser.Text = serviceGeneral.separateurDesMilles(montantRecuEncaisser.ToString("0")) + "Ar";
                    LabelMontantTotalRecuEncaisserLettre.Text = convertisseuse.convertion(montantRecuEncaisser.ToString("0")) + " Ariary";

                    LabelMontantTotalRecuEnCaisserCheque.Text = serviceGeneral.separateurDesMilles(montantRecuEncaisserCheque.ToString("0")) + "Ar";
                    LabelMontantTotalRecuEnCaisserChequeLettre.Text = convertisseuse.convertion(montantRecuEncaisserCheque.ToString("0")) + "Ariary";
                }
                else
                {
                    Panel_RecuEncaisserMontantTotal.Visible = false;
                    LabelMontantTotalRecuEncaisser.Text = "0Ar";
                    LabelMontantTotalRecuEncaisserLettre.Text = "Zéro Ariary";

                    LabelMontantTotalRecuEnCaisserCheque.Text = "0Ar";
                    LabelMontantTotalRecuEnCaisserChequeLettre.Text = "Zéro Ariary";
                }


                if (montantVoyageAbonnement > 0)
                {
                    Panel_VoyageAbonnementMontantTotal.Visible = true;
                    LabelMontantTotalVoyageAbonnement.Text = serviceGeneral.separateurDesMilles(montantVoyageAbonnement.ToString("0")) + "Ar";
                    LabelMontantTotalVoyageAbonnementLettre.Text = convertisseuse.convertion(montantVoyageAbonnement.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_VoyageAbonnementMontantTotal.Visible = false;
                    LabelMontantTotalVoyageAbonnement.Text = "0Ar";
                    LabelMontantTotalVoyageAbonnementLettre.Text = "Zéro Ariary";
                }

                if (montantRecuAD > 0)
                {
                    Panel_RecuADTotal.Visible = true;
                    LabelMontantTotalRecuDecaisser.Text = serviceGeneral.separateurDesMilles(montantRecuAD.ToString("0")) + "Ar";
                    LabelMontantTotalRecuDecaisserLettre.Text = convertisseuse.convertion(montantRecuAD.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_RecuADTotal.Visible = false;
                    LabelMontantTotalRecuDecaisser.Text = "0Ar";
                    LabelMontantTotalRecuDecaisserLettre.Text = "Zéro Ariary";
                }

                Panel_MontantTotalSessionCaisse.Visible = true;

                if (montantTotalSession >= 0)
                {
                    LabelMontantTotalSessionLettre.Text = convertisseuse.convertion(montantTotalSession.ToString("0")) + " Ariary";
                    LabelMontantTotalSession.Text = serviceGeneral.separateurDesMilles(montantTotalSession.ToString("0")) + "Ar";
                }
                else
                {
                    LabelMontantTotalSession.Text = "(" + serviceGeneral.separateurDesMilles(montantTotalSession.ToString("0")) + ") Ar";
                    LabelMontantTotalSessionLettre.Text = "(- " + convertisseuse.convertion((montantTotalSession * -1).ToString("0")) + ") Ariary";
                }


                if (montantTotalSessionCheque >= 0)
                {
                    LabelMontantTotalSessionChequelettre.Text = convertisseuse.convertion(montantTotalSessionCheque.ToString("0")) + " Ariary";
                    LabelMontantTotalSessionCheque.Text = serviceGeneral.separateurDesMilles(montantTotalSessionCheque.ToString("0")) + "Ar";
                }
                else
                {
                    LabelMontantTotalSessionCheque.Text = "(" + serviceGeneral.separateurDesMilles(montantTotalSessionCheque.ToString("0")) + ") Ar";
                    LabelMontantTotalSessionChequelettre.Text = "(- " + convertisseuse.convertion((montantTotalSessionCheque * -1).ToString("0")) + ") Ariary";
                }

                if (sessionAgence != null)
                {
                    LabelDateDebutSession.Text = sessionAgence.DateHeureOuverture.ToString("dd MMMM yyyy");
                }
            }
            #endregion
        }

        private void insertToObj(crlSessionAgence sessionAgence)
        {
            #region implementation
            if (sessionAgence != null)
            {
                sessionAgence.MatriculeAgentOuverture = agent.matriculeAgent;
                sessionAgence.NumAgence = hfNumAgence.Value;
            }
            #endregion
        }
        #endregion

        #region event
        protected void btnRechercheAgence_Click(object sender, EventArgs e)
        {
            this.initialiseGridAgence();
        }
        protected void ddlTriAgence_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void btnOuvrirSession_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAgence agence = null;
            crlSessionAgence sessionAgence = null;
            #endregion

            #region implementation
            if (hfNumAgence.Value != "")
            {
                agence = serviceAgence.selectAgence(hfNumAgence.Value);
                if (agence != null)
                {
                    if (agence.sessionAgence != null)
                    {
                        //
                    }
                    else
                    {
                        sessionAgence = new crlSessionAgence();
                        this.insertToObj(sessionAgence);

                        sessionAgence.NumSessionAgence = serviceSessionAgence.insertSessionAgence(sessionAgence, agent.agence.SigleAgence);

                        if (sessionAgence.NumSessionAgence != "")
                        {
                            this.afficheAgence(sessionAgence.NumAgence);
                            this.initialiseGridAgence();
                        }
                        else
                        {
                            //
                        }
                    }
                }
            }
            #endregion
        }
        protected void btnFermerSession_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAgence agence = null;
            #endregion

            #region implementation
            if (hfNumAgence.Value != "")
            {
                agence = serviceAgence.selectAgence(hfNumAgence.Value);
                if (agence != null)
                {
                    if (agence.sessionAgence != null)
                    {
                        agence.sessionAgence.DateHeureFermeture = DateTime.Now;
                        agence.sessionAgence.MatriculeAgentFermeture = agent.matriculeAgent;

                        if (serviceSessionAgence.updateSessionAgence(agence.sessionAgence))
                        {
                            this.afficheAgence(agence.sessionAgence.NumAgence);
                            this.initialiseGridAgence();
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
            else
            {
                //
            }
            #endregion
        }
        #endregion
    }
}