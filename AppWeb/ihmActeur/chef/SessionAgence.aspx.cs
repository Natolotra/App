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
    public partial class SessionAgence : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalSessionAgence serviceSessionAgence = null;
        IntfDalAgence serviceAgence = null;
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
            serviceSessionAgence = new ImplDalSessionAgence();
            serviceAgence = new ImplDalAgence();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.afficheAgence(agent.numAgence);

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "421"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void afficheAgence(string numAgence)
        {
            #region declaration
            crlAgence agence = null;
            #endregion

            #region implementation
            agence = serviceAgence.selectAgence(numAgence);

            if (agence != null)
            {
                LabelAgence.Text = "Agence " + agence.NomAgence;
                if (agence.sessionAgence != null)
                {
                    imageStatut.ImageUrl = "~/CssStyle/images/vert.png";
                    this.afficheMontantTotal(agent.agence.sessionAgence.NumSessionAgence);
                }
                else
                {
                    imageStatut.ImageUrl = "~/CssStyle/images/rouge.png";
                    this.afficheMontantTotal("");
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

            //crlSessionAgence sessionAgence = null;
            Convertisseuse convertisseuse = new Convertisseuse();
            #endregion

            #region implementation
            if (numSessionAgence != "")
            {
                //sessionAgence = serviceSessionAgence.selectSessionAgence(numSessionCaisse);
                montantBillet = serviceSessionAgence.getMontantTotalBillet(numSessionAgence);
                montantCommission = serviceSessionAgence.getMontantTotalCommission(numSessionAgence);
                montantDureeAbonnement = serviceSessionAgence.getMontantTotalDureeAbonnement(numSessionAgence);
                montantRecuEncaisser = serviceSessionAgence.getMontantTotalRecuEncaisserEspece(numSessionAgence);
                montantRecuEncaisserCheque = serviceSessionAgence.getMontantTotalRecuEncaisserCheque(numSessionAgence);
                montantVoyageAbonnement = serviceSessionAgence.getMontantTotalVoyageAbonnement(numSessionAgence);
                montantRecuAD = serviceSessionAgence.getMontantTotalRecuAD(numSessionAgence);

                montantTotalSession = montantBillet + montantCommission + montantDureeAbonnement + montantRecuEncaisser + montantVoyageAbonnement + -montantRecuAD;
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

                if (montantTotalSession > 0 || montantTotalSessionCheque > 0)
                {
                    Panel_MontantTotalSessionAgence.Visible = true;
                    LabelMontantTotalSession.Text = serviceGeneral.separateurDesMilles(montantTotalSession.ToString("0")) + "Ar";
                    LabelMontantTotalSessionLettre.Text = convertisseuse.convertion(montantTotalSession.ToString("0")) + " Ariary";

                    LabelMontantTotalSessionCheque.Text = serviceGeneral.separateurDesMilles(montantTotalSessionCheque.ToString("0")) + "Ar";
                    LabelMontantTotalSessionChequelettre.Text = convertisseuse.convertion(montantTotalSessionCheque.ToString("0")) + " Ariary";

                    if (agent.agence.sessionAgence != null)
                    {
                        LabelDateDebutSession.Text = agent.agence.sessionAgence.DateHeureOuverture.ToString("dd MMMM yyyy");
                    }
                }
                else
                {
                    Panel_MontantTotalSessionAgence.Visible = false;
                    LabelMontantTotalSession.Text = "0Ar";
                    LabelMontantTotalSessionLettre.Text = "Zéro Ariary";
                    LabelDateDebutSession.Text = "";

                    LabelMontantTotalSessionCheque.Text = "0Ar";
                    LabelMontantTotalSessionChequelettre.Text = "Zéro Ariary";
                }


            }
            #endregion
        }
        #endregion
    }
}