using arc.utile;
using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.commercial
{
    public partial class sessionCaisseVue : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAgent serviceAgent = null;
        IntfDalSessionCaisse serviceSessionCaisse = null;
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
            serviceAgent = new ImplDalAgent();
            serviceSessionCaisse = new ImplDalSessionCaisse();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.afficheAgent(agent.matriculeAgent);
                if (agent.sessionCaisse != null)
                {
                    this.afficheMontantTotal(agent.sessionCaisse.NumSessionCaisse);
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "231"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void afficheAgent(string matriculeAgent)
        {
            #region declaration
            crlAgent agent = null;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                agent = serviceAgent.selectAgent(matriculeAgent);
                if (agent != null)
                {
                    ImageAgent.ImageUrl = ConfigurationManager.AppSettings["urlImageAgent"] + agent.ImageAgent;
                    LabelMatriculeAgent.Text = agent.matriculeAgent;
                    LabelNomPrenomAgent.Text = agent.prenomAgent + " " + agent.nomAgent;
                    LabelAdresseAgent.Text = agent.adresseAgent;
                    LabelContactAgent.Text = agent.telephoneAgent + " / " + agent.telephoneMobileAgent;
                    LabelCINAgent.Text = agent.cinAgent;
                    LabelTypeAgent.Text = "Agent " + agent.typeAgent;

                    if (agent.sessionCaisse != null)
                    {
                        LabelSessionStatu.Text = "Session ouverte";
                        imageStatut.ImageUrl = "~/CssStyle/images/vert.png";
                        TextFondCaisse.Text = serviceGeneral.separateurDesMilles(agent.sessionCaisse.FondCaisse.ToString("0"));

                        this.afficheMontantTotal(agent.sessionCaisse.NumSessionCaisse);
                    }
                    else
                    {
                        LabelSessionStatu.Text = "Session fermée";
                        imageStatut.ImageUrl = "~/CssStyle/images/rouge.png";
                        TextFondCaisse.Text = "0";

                        Panel_BilletMontantTotal.Visible = false;
                        Panel_CommissionMontantTotal.Visible = false;
                        Panel_DureeAbonnementMontantTotal.Visible = false;
                        Panel_MontantTotalSessionCaisse.Visible = false;
                        Panel_RecuEncaisserMontantTotal.Visible = false;
                        Panel_VoyageAbonnementMontantTotal.Visible = false;
                        Panel_AbonnementNbVoyageUS.Visible = false;
                        Panel_BilletUS.Visible = false;
                        Panel_RecuADTotal.Visible = false;
                    }
                }
            }
            #endregion
        }

        private void afficheMontantTotal(string numSessionCaisse)
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
            double montantAbonnementNbVoyageUS = 0;
            double montantBilletUS = 0;

            crlSessionCaisse sessionCaisse = null;
            Convertisseuse convertisseuse = new Convertisseuse();
            #endregion

            #region implementation
            if (numSessionCaisse != "")
            {
                sessionCaisse = serviceSessionCaisse.selectSessionCaisse(numSessionCaisse);
                montantBillet = serviceSessionCaisse.getMontantTotalBillet(numSessionCaisse);
                montantCommission = serviceSessionCaisse.getMontantTotalCommission(numSessionCaisse);
                montantDureeAbonnement = serviceSessionCaisse.getMontantTotalDureeAbonnement(numSessionCaisse);
                montantRecuEncaisser = serviceSessionCaisse.getMontantTotalRecuEncaisserEspece(numSessionCaisse);
                montantRecuEncaisserCheque = serviceSessionCaisse.getMontantTotalRecuEncaisserCheque(numSessionCaisse);
                montantVoyageAbonnement = serviceSessionCaisse.getMontantTotalVoyageAbonnement(numSessionCaisse);
                montantRecuAD = serviceSessionCaisse.getMontantTotalRecuAD(numSessionCaisse);
                montantAbonnementNbVoyageUS = serviceSessionCaisse.getMontantTotalAbonnementNVUS(numSessionCaisse);
                montantBilletUS = serviceSessionCaisse.getMontantTotalBilletUS(numSessionCaisse);

                montantTotalSession = montantBillet + montantCommission + montantDureeAbonnement + montantRecuEncaisser + montantVoyageAbonnement + sessionCaisse.FondCaisse + montantAbonnementNbVoyageUS + montantBilletUS - montantRecuAD;
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

                if (montantAbonnementNbVoyageUS > 0)
                {
                    Panel_AbonnementNbVoyageUS.Visible = true;
                    LabelMontantAbonnementNBVoyageUS.Text = serviceGeneral.separateurDesMilles(montantAbonnementNbVoyageUS.ToString("0")) + "Ar";
                    LabelMontantAbonnementNBVoyageUSLettre.Text = convertisseuse.convertion(montantAbonnementNbVoyageUS.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_AbonnementNbVoyageUS.Visible = false;
                    LabelMontantAbonnementNBVoyageUS.Text = "";
                    LabelMontantAbonnementNBVoyageUSLettre.Text = "";

                }

                if (montantBilletUS > 0)
                {
                    Panel_BilletUS.Visible = true;
                    LabelMontantBilletUS.Text = serviceGeneral.separateurDesMilles(montantBilletUS.ToString("0")) + "Ar";
                    LabelMontantBilletUSLettre.Text = convertisseuse.convertion(montantBilletUS.ToString("0")) + " Ariary";
                }
                else
                {
                    Panel_BilletUS.Visible = false;
                    LabelMontantBilletUS.Text = "";
                    LabelMontantBilletUSLettre.Text = "";
                }

                if (sessionCaisse != null)
                {
                    LabelDateDebutSession.Text = sessionCaisse.DateHeureDebutSession.ToString("dd MMMM yyyy");
                }


                /*else
                {

                    Panel_MontantTotalSessionCaisse.Visible = false;
                    LabelMontantTotalSession.Text = "0Ar";
                    LabelMontantTotalSessionLettre.Text = "Zéro Ariary";
                    LabelDateDebutSession.Text = "";

                    LabelMontantTotalSessionCheque.Text = "0Ar";
                    LabelMontantTotalSessionChequelettre.Text = "Zéro Ariary";
                }*/



                this.initialiseGridBillet();
                this.initialiseGridCommission();
                this.initialiseGridADT();
                this.initialiseGridANV();
                this.initialiseGridRecuEspece();
                this.initialiseGridRecuCheque();
                this.initialiseGridRecuAD();
            }
            #endregion
        }


        #region billet
        private void initialiseGridBillet()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridBilletSession(gvBillet, "billet.numBillet", "billet.numBillet", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #region commission
        private void initialiseGridCommission()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridCommissionSession(gvCommission, "commission.idCommission", "commission.idCommission", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #region adt
        private void initialiseGridADT()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridAbonnementDureeTempsSession(gvADT, "dureeabonnement.numDureeAbonnement", "dureeabonnement.numDureeAbonnement", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #region avn
        private void initialiseGridANV()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridAbonnementNbVoyageSession(gvANV, "voyageabonnement.numVoyageAbonnement", "voyageabonnement.numVoyageAbonnement", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #region recu espece
        private void initialiseGridRecuEspece()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridRecuEspeceSession(gvRecuEspece, "recuencaisser.numRecuEncaisser", "recuencaisser.numRecuEncaisser", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #region recu cheque
        private void initialiseGridRecuCheque()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridRecuChequeSession(gvRecuCheque, "recuencaisser.numRecuEncaisser", "recuencaisser.numRecuEncaisser", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #region recuAD
        private void initialiseGridRecuAD()
        {
            if (agent.sessionCaisse != null)
            {
                serviceSessionCaisse.insertToGridRecuADSession(gvRecuAD, "recuad.numRecuAD", "recuad.numRecuAD", "", agent.sessionCaisse.NumSessionCaisse);
            }
        }
        #endregion

        #endregion

        #region event billet
        protected void btnFermerBillet_Click(object sender, EventArgs e)
        {
            Panel_BilletListe.Visible = false;
        }
        protected void gvBillet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBillet.PageIndex = e.NewPageIndex;
            this.initialiseGridBillet();
        }
        protected void btnDetailBillet_Click(object sender, EventArgs e)
        {
            Panel_BilletListe.CssClass = "PanneauActionRight";
            Panel_BilletListe.Visible = true;
        }
        #endregion

        #region event commission
        protected void btnFermerCommssion_Click(object sender, EventArgs e)
        {
            Panel_CommissionListe.Visible = false;
        }
        protected void gvCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommission.PageIndex = e.NewPageIndex;
            this.initialiseGridCommission();
        }
        protected void btnDetailCommission_Click(object sender, EventArgs e)
        {
            Panel_CommissionListe.CssClass = "PanneauActionRight";
            Panel_CommissionListe.Visible = true;
        }
        #endregion

        #region event ADT
        protected void btnDetailtADT_Click(object sender, EventArgs e)
        {
            Panel_AbonnementDureeTemps.CssClass = "PanneauActionRight";
            Panel_AbonnementDureeTemps.Visible = true;
        }
        protected void btnFermerADT_Click(object sender, EventArgs e)
        {
            Panel_AbonnementDureeTemps.Visible = false;
        }
        protected void gvADT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvADT.PageIndex = e.NewPageIndex;
            this.initialiseGridADT();
        }
        #endregion

        #region event ANV
        protected void gvANV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvANV.PageIndex = e.NewPageIndex;
            this.initialiseGridANV();
        }
        protected void btnFermerANV_Click(object sender, EventArgs e)
        {
            Panel_ANVListe.Visible = false;
        }
        protected void btnDetailANV_Click(object sender, EventArgs e)
        {
            Panel_ANVListe.CssClass = "PanneauActionRight";
            Panel_ANVListe.Visible = true;
        }
        #endregion

        #region recu espece
        protected void btnFermerRecuEspece_Click(object sender, EventArgs e)
        {
            Panel_RecuEspece.Visible = false;
        }
        protected void gvRecuEspece_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecuEspece.PageIndex = e.NewPageIndex;
            this.initialiseGridRecuEspece();
        }
        protected void btnDetailRecuEspece_Click(object sender, EventArgs e)
        {
            Panel_RecuEspece.CssClass = "PanneauActionRight";
            Panel_RecuEspece.Visible = true;
        }
        #endregion

        #region recu cheque
        protected void gvRecuCheque_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecuCheque.PageIndex = e.NewPageIndex;
            this.initialiseGridRecuCheque();
        }
        protected void btnFermeRecuCheque_Click(object sender, EventArgs e)
        {
            Panel_RecuCheque.Visible = false;
        }
        protected void btnDetailRecuCheque_Click(object sender, EventArgs e)
        {
            Panel_RecuCheque.CssClass = "PanneauActionRight";
            Panel_RecuCheque.Visible = true;
        }
        #endregion

        #region recuAD
        protected void gvRecuAD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecuAD.PageIndex = e.NewPageIndex;
            this.initialiseGridRecuAD();
        }
        protected void btnFermerRecuAD_Click(object sender, EventArgs e)
        {
            Panel_RecuDecaisser.Visible = false;
        }
        protected void btnDetailRecuAD_Click(object sender, EventArgs e)
        {
            Panel_RecuDecaisser.CssClass = "PanneauActionRight";
            Panel_RecuDecaisser.Visible = true;
        }
        #endregion
    }
}