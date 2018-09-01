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

namespace AppWeb.ihmActeur.commercial
{
    public partial class RecuExcedentBagage : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalModePaiement serviceModePaiement = null;
        //IntfDalRecu serviceRecu = null;
        IntfDalRecuEncaisser serviceRecuEncaisser = null;
        IntfDalSessionCaisse serviceSessionCaisse = null;
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
            serviceModePaiement = new ImplDalModePaiement();
            //serviceRecu = new ImplDalRecu();
            serviceRecuEncaisser = new ImplDalRecuEncaisser();
            serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                TextLibelleRecu.Text = ReLibeleRecu.libeleRecuExedendBagage;

                //serviceModePaiement.loadDddlModePaiement(ddlModePaiement);
                serviceModePaiement.loadDddlModePaiement(ddlModePaiement, "Abonnement;Bon de commande;Chèque;Commande");

                try
                {
                    ddlModePaiement.SelectedValue = "Espèce";
                }
                catch (Exception)
                {
                }

                this.initialiseGridRecu();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "037"))
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
                divIndication.Style.Add("font-size", "12px");
                divIndication.Style.Add("color", color);
                divIndication.InnerText = str;
            }
            else
            {
                divIndication.InnerText = "";
            }
        }

        private void initialiseFormulaire()
        {
            TextMontantExcedent.Text = "";
        }

        private void initialiseGridRecu()
        {
            //serviceRecu.insertToGridRecu(gvRecu, ddlRecuTri.SelectedValue, ddlRecuTri.SelectedValue, TextRechercheRecu.Text, agent.numAgence);
        }

        private bool testFormulaire()
        {
            #region declaration
            bool isTest = false;
            #endregion

            #region implementation
            if (TextMontantExcedent.Text.Trim() != "")
            {
                isTest = true;
            }
            else
            {
                this.divIndicationText(ReExcedentBagage.montantRecuNonVide, "Red");
            }
            #endregion

            return isTest;
        }

        private void initialiseErrorMessage()
        {
            TextMontantExcedent_RequiredFieldValidator.ErrorMessage = ReExcedentBagage.montantRecuNonVide;
        }
        #endregion

        #region event
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            //crlRecu Recu = null;
            crlRecuEncaisser Recu = null;
            #endregion

            #region implementation
            if (this.testFormulaire())
            {
                if (agent.sessionCaisse != null)
                {
                    Recu = new crlRecuEncaisser();
                    Recu.agent = agent;
                    Recu.DateRecuEncaisser = DateTime.Now;
                    Recu.LibelleRecuEncaisser = TextLibelleRecu.Text;
                    Recu.MatriculeAgent = agent.matriculeAgent;
                    Recu.ModePaiement = ddlModePaiement.SelectedValue;
                    try
                    {
                        Recu.MontantRecuEncaisser = double.Parse(TextMontantExcedent.Text);
                    }
                    catch (Exception)
                    {
                    }

                    Recu.NumRecuEncaisser = serviceRecuEncaisser.insertRecuEncaisser(Recu);

                    if (Recu.NumRecuEncaisser != "")
                    {
                        this.initialiseGridRecu();
                        this.initialiseFormulaire();
                        serviceSessionCaisse.insertAssocSessionCaisseRecuEncaisser(Recu.NumRecuEncaisser, agent.sessionCaisse.NumSessionCaisse);

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                        string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=recuEncaisser&numRecuEncaisser=" + Recu.NumRecuEncaisser, 700,
                                        500, 10, 10), true);

                        //Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReExcedentBagage.recuEnregistre + "');", true);

                    }
                    else
                    {
                        this.divIndicationText(ReExcedentBagage.recuNonEnregistre, "Red");
                    }
                }
            }
            #endregion
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }

        #endregion
    }
}