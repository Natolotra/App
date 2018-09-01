using AppRessources.Ressources;
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

namespace AppWeb.ihmActeur.administrateur
{
    public partial class ObservationAgent : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalLien serviceLien = null;
        IntfDalAgent serviceAgent = null;
        IntfDalObservationAgent serviceObservationAgent = null;
        IntfDalGeneral serviceGeneral = null;

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
            serviceGeneral = new ImplDalGeneral();
            serviceObservationAgent = new ImplDalObservationAgent();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseFormulaire();
                this.initialiseGridObservation();
                this.initialiseErrorMessage();
                this.initialiseGridAgent();
                this.initialiseGridAgentListeNoire();

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "063"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseErrorMessage()
        {
            TextObservation_RequiredFieldValidator.ErrorMessage = ReObservationAgent.ObservationNonVide;
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

        private void initialiseFormulaire()
        {
            LabelNomAgent.Text = "";
            LabelPrenomAgent.Text = "";
            LabelAdresseAgent.Text = "";
            LabelMatriculeAgent.Text = "";
            LabelContactAgent.Text = "";


            hfObservationAgent.Value = "";

            LabelNomAgentObservation.Text = "";

            TextObservation.Text = "";
            rbAvertissement.SelectedValue = "0";

            btnModifier.Enabled = false;
            btnValider.Enabled = false;
            this.initialiseGridObservation();

            this.divIndicationText("", "Red");

            ImageAgent.ImageUrl = "";
        }

        private void initialiseFormulaireObs()
        {
            hfObservationAgent.Value = "";

            TextObservation.Text = "";
            rbAvertissement.SelectedValue = "0";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;

            this.divIndicationText("", "Red");
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
                    LabelNomAgent.Text = agent.nomAgent;
                    LabelPrenomAgent.Text = agent.prenomAgent;
                    LabelAdresseAgent.Text = agent.adresseAgent;
                    LabelMatriculeAgent.Text = agent.matriculeAgent;
                    LabelContactAgent.Text = agent.telephoneAgent + " " + agent.telephoneMobileAgent;

                    ImageAgent.ImageUrl = ConfigurationManager.AppSettings["urlImageAgent"] + agent.ImageAgent;

                    LabelNomAgentObservation.Text = " / " + agent.prenomAgent + " " + agent.nomAgent;


                    this.initialiseGridObservation();
                    this.initialiseFormulaireObs();
                }
            }
            #endregion
        }

        private void afficheObservation(string numObservation)
        {
            #region declaration
            crlObservationAgent observationAgent = null;
            crlAgent agent = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numObservation != "")
            {
                observationAgent = serviceObservationAgent.selectObservationAgent(numObservation);

                if (observationAgent != null)
                {
                    this.afficheAgent(observationAgent.MatriculeAgent);
                    TextObservation.Text = observationAgent.TextObesvation;

                    rbAvertissement.SelectedValue = observationAgent.IsListeNoire.ToString();

                    hfObservationAgent.Value = observationAgent.NumObservation;
                    agent = serviceAgent.selectAgent(observationAgent.MatriculeAgent);

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;


                    strConfirm = "Voulez vous vraiment modifier l'observation?\n";

                    if (agent != null)
                    {
                        strConfirm += "Chauffeur: " + agent.prenomAgent + " " + agent.nomAgent + "\n";
                        strConfirm += "Date: " + observationAgent.DateObservation.ToString("dd MMMM yyyy");
                    }

                    ConfirmButtonExtender_btnModifier.ConfirmText = strConfirm;
                }
            }
            #endregion
        }

        private void insertToObjObservationAgent(crlObservationAgent observation)
        {
            if (observation != null)
            {
                observation.MatriculeAgent = LabelMatriculeAgent.Text;
                observation.TextObesvation = TextObservation.Text;
                observation.IsListeNoire = int.Parse(rbAvertissement.SelectedValue);

            }
        }

        private void initialiseGridAgent()
        {
            serviceAgent.insertToGridAgentListe(gvAgent, ddlTriAgent.SelectedValue, ddlTriAgent.SelectedValue, TextRechercheAgent.Text, "");
        }

        private void initialiseGridAgentListeNoire()
        {
            serviceAgent.insertToGridAgentListeNoire(gvListeNoireAgent, ddlTriAgent.SelectedValue, ddlTriAgent.SelectedValue, TextRechercheAgent.Text, "");
        }

        private void initialiseGridObservation()
        {
            serviceObservationAgent.insertToGridObservationAgent(gvObservationAgent, "observationagent.numObservation", "observationagent.numObservation", "", LabelMatriculeAgent.Text);
        }
        #endregion

        #region event
        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlObservationAgent observationAgent = null;
            crlAgent agentObj = null;
            string strIndicationText = "";
            #endregion

            #region implementation
            if (hfObservationAgent.Value != "")
            {
                observationAgent = serviceObservationAgent.selectObservationAgent(hfObservationAgent.Value);
                if (observationAgent != null)
                {
                    if (LabelMatriculeAgent.Text != "")
                    {
                        this.insertToObjObservationAgent(observationAgent);
                        if (serviceObservationAgent.updateObservationAgent(observationAgent))
                        {
                            this.initialiseGridObservation();
                            this.initialiseGridAgentListeNoire();
                            this.initialiseFormulaireObs();

                            agentObj = serviceAgent.selectAgent(observationAgent.MatriculeAgent);


                            strIndicationText = "Observation bien modifier!";
                            if (agentObj != null)
                            {
                                strIndicationText += " Agent: " + agentObj.prenomAgent + " " + agentObj.nomAgent;
                            }
                            this.divIndicationText(strIndicationText, "Black");
                        }
                        else
                        {
                            strIndicationText = "Une erreur ce produit durant la modification!";
                            this.divIndicationText(strIndicationText, "Red");
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
            #endregion
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlObservationAgent observation = null;
            crlAgent agentObj = null;
            string strIndicationText = "";
            #endregion

            #region implementation
            observation = new crlObservationAgent();
            this.insertToObjObservationAgent(observation);

            if (LabelMatriculeAgent.Text != "")
            {
                observation.NumObservation = serviceObservationAgent.insertObservationAgent(observation, this.agent.agence.SigleAgence);
                if (observation.NumObservation != "")
                {
                    this.initialiseGridObservation();
                    this.initialiseGridAgentListeNoire();
                    this.initialiseFormulaireObs();

                    agentObj = serviceAgent.selectAgent(observation.MatriculeAgent);


                    strIndicationText = "Observation bien enregistrer!";
                    if (agentObj != null)
                    {
                        strIndicationText += " Agent: " + agentObj.prenomAgent + " " + agentObj.nomAgent;
                    }
                    this.divIndicationText(strIndicationText, "Black");
                }
                else
                {
                    strIndicationText = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strIndicationText, "Red");
                }
            }
            else
            {
                //
            }
            #endregion
        }


        protected void ddlTriAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divIndicationText("", "Red");
            this.initialiseGridAgent();
        }
        protected void btnRechercheAgent_Click(object sender, EventArgs e)
        {
            this.divIndicationText("", "Red");
            this.initialiseGridAgent();
        }
        protected void gvAgent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.divIndicationText("", "Red");
            gvAgent.PageIndex = e.NewPageIndex;
            this.initialiseGridAgent();
        }
        protected void gvAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.divIndicationText("", "Red");
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheAgent(e.CommandArgument.ToString());
            }
            #endregion
        }


        protected void gvObservationAgent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.divIndicationText("", "Red");
            gvObservationAgent.PageIndex = e.NewPageIndex;
            this.initialiseGridObservation();
        }
        protected void gvObservationAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.divIndicationText("", "Red");
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheObservation(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("observationagent", "numObservation", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridObservation();
                    this.initialiseGridAgentListeNoire();
                }
            }
            #endregion
        }


        protected void ddlTriListeNoireAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divIndicationText("", "Red");
            this.initialiseGridAgentListeNoire();
        }
        protected void btnRechercheListeNoireAgent_Click(object sender, EventArgs e)
        {
            this.divIndicationText("", "Red");
            this.initialiseGridAgentListeNoire();
        }
        protected void gvListeNoireAgent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.divIndicationText("", "Red");
            gvListeNoireAgent.PageIndex = e.NewPageIndex;
            this.initialiseGridAgentListeNoire();
        }
        protected void gvListeNoireAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.divIndicationText("", "Red");
            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheAgent(e.CommandArgument.ToString());
            }
            #endregion
        }
        #endregion
    }
}