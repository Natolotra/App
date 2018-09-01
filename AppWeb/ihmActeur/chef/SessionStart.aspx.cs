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

namespace AppWeb.ihmActeur.chef
{
    public partial class SessionStart : System.Web.UI.Page
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
                this.initialiseGridAgent();

                Panel_BilletMontantTotal.Visible = false;
                Panel_CommissionMontantTotal.Visible = false;
                Panel_DureeAbonnementMontantTotal.Visible = false;
                Panel_MontantTotalSessionCaisse.Visible = false;
                Panel_RecuEncaisserMontantTotal.Visible = false;
                Panel_VoyageAbonnementMontantTotal.Visible = false;
                Panel_RecuADTotal.Visible = false;
                Panel_AbonnementNbVoyageUS.Visible = false;
                Panel_BilletUS.Visible = false;
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "411"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridAgent()
        {
            serviceAgent.insertToGridAgent(gvAgent, ddlTriAgent.SelectedValue, ddlTriAgent.SelectedValue, TextRechercheAgent.Text, agent.numAgence);
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
                    hfMatriculeAgent.Value = agent.matriculeAgent;
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
                        TextFondCaisse.Text = agent.sessionCaisse.FondCaisse.ToString("0");
                        btnSessionValider.Enabled = false;
                        btnFermerSession.Enabled = true;

                        this.afficheMontantTotal(agent.sessionCaisse.NumSessionCaisse);
                    }
                    else
                    {
                        LabelSessionStatu.Text = "Session fermée";
                        imageStatut.ImageUrl = "~/CssStyle/images/rouge.png";
                        TextFondCaisse.Text = "0";
                        btnSessionValider.Enabled = true;
                        btnFermerSession.Enabled = false;

                        Panel_BilletMontantTotal.Visible = false;
                        Panel_CommissionMontantTotal.Visible = false;
                        Panel_DureeAbonnementMontantTotal.Visible = false;
                        Panel_MontantTotalSessionCaisse.Visible = false;
                        Panel_RecuEncaisserMontantTotal.Visible = false;
                        Panel_VoyageAbonnementMontantTotal.Visible = false;
                        Panel_RecuADTotal.Visible = false;
                        Panel_AbonnementNbVoyageUS.Visible = false;
                        Panel_BilletUS.Visible = false;
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

                /*if (montantTotalSession > 0 || montantTotalSessionCheque > 0)
                {
                    Panel_MontantTotalSessionCaisse.Visible = true;
                    LabelMontantTotalSession.Text = serviceGeneral.separateurDesMilles(montantTotalSession.ToString("0")) + "Ar";
                    LabelMontantTotalSessionLettre.Text = convertisseuse.convertion(montantTotalSession.ToString("0")) + " Ariary";

                    LabelMontantTotalSessionCheque.Text = serviceGeneral.separateurDesMilles(montantTotalSessionCheque.ToString("0")) + "Ar";
                    LabelMontantTotalSessionChequelettre.Text = convertisseuse.convertion(montantTotalSessionCheque.ToString("0")) + " Ariary";

                    if (sessionCaisse != null)
                    {
                        LabelDateDebutSession.Text = sessionCaisse.DateHeureDebutSession.ToString("dd MMMM yyyy");
                    }
                }
                else
                {
                    Panel_MontantTotalSessionCaisse.Visible = false;
                    LabelMontantTotalSession.Text = "0Ar";
                    LabelMontantTotalSessionLettre.Text = "Zéro Ariary";
                    LabelDateDebutSession.Text = "";

                    LabelMontantTotalSessionCheque.Text = "0Ar";
                    LabelMontantTotalSessionChequelettre.Text = "Zéro Ariary";
                }*/


            }
            #endregion
        }

        private void insertToObjetSessionCaisse(crlSessionCaisse sessionCaisse)
        {
            #region implementation
            if (sessionCaisse != null)
            {
                try
                {
                    sessionCaisse.FondCaisse = double.Parse(TextFondCaisse.Text);
                }
                catch (Exception) { }
                sessionCaisse.MatriculeAgent = hfMatriculeAgent.Value;
                sessionCaisse.MatriculeAgentOuverture = agent.matriculeAgent;
                //sessionCaisse.agentOuverture = agent;
            }
            #endregion
        }
        #endregion

        #region event
        protected void btnRechercheAgent_Click(object sender, EventArgs e)
        {
            this.initialiseGridAgent();
        }
        protected void ddlTriAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAgent();
        }
        protected void gvAgent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAgent.PageIndex = e.NewPageIndex;
            this.initialiseGridAgent();
        }
        protected void gvAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                afficheAgent(e.CommandArgument.ToString());
            }
        }
        #endregion

        protected void btnSessionValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlSessionCaisse sessionCaisse = null;
            crlAgent agent = null;
            #endregion

            #region implementation
            if (hfMatriculeAgent.Value != "")
            {
                agent = serviceAgent.selectAgent(hfMatriculeAgent.Value);
                if (agent != null)
                {

                    if (agent.sessionCaisse != null)
                    {
                        //session deja ouvert
                    }
                    else
                    {
                        sessionCaisse = new crlSessionCaisse();
                        this.insertToObjetSessionCaisse(sessionCaisse);

                        sessionCaisse.NumSessionCaisse = serviceSessionCaisse.insertSessionCaisse(sessionCaisse, this.agent.agence.SigleAgence);

                        if (sessionCaisse.NumSessionCaisse != "")
                        {
                            this.afficheAgent(sessionCaisse.MatriculeAgent);
                            this.initialiseGridAgent();
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
            crlAgent agent = null;
            #endregion

            #region implementation
            if (hfMatriculeAgent.Value != "")
            {
                agent = serviceAgent.selectAgent(hfMatriculeAgent.Value);
                if (agent != null)
                {
                    if (agent.sessionCaisse != null)
                    {
                        if (this.agent.agence.sessionAgence != null)
                        {
                            agent.sessionCaisse.DateHeureFinSession = DateTime.Now;
                            agent.sessionCaisse.MatriculeAgentFermeture = this.agent.matriculeAgent;
                            agent.sessionCaisse.NumSessionAgence = this.agent.agence.sessionAgence.NumSessionAgence;

                            if (serviceSessionCaisse.updateSessionCaisse(agent.sessionCaisse))
                            {
                                agent = serviceAgent.selectAgent(hfMatriculeAgent.Value);
                                if (agent != null)
                                {
                                    this.afficheAgent(agent.matriculeAgent);
                                    this.initialiseGridAgent();
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
    }
}