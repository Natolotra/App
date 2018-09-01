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
    public partial class ObservationVehicule : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalVehicule serviceVehicule = null;
        IntfDalObservationVehicule serviceObservationVehicule = null;
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
            serviceVehicule = new ImplDalVehicule();
            serviceObservationVehicule = new ImplDalObservationVehicule();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseGridVehicule();
                this.initialiseGridListeNoireVehicule();
                this.initialiseFormulaire();
                this.initialiseGridObservationVehicule();
                this.initialiseErrorMessage();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "061"))
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
            TextObservation_RequiredFieldValidator.ErrorMessage = ReObservationVehicule.observationNonVide;
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

        private void initialiseGridVehicule()
        {
            serviceVehicule.insertToGridVehiculeForFacture(gvVehicule, ddlTriVehicule.SelectedValue, ddlTriVehicule.SelectedValue, TextRechercheVehicule.Text);
        }

        private void initialiseGridListeNoireVehicule()
        {
            serviceVehicule.insertToGridVehiculeListeNoire(gvListeNoireVehicule, ddlTriListeNoireVehicule.SelectedValue, ddlTriListeNoireVehicule.SelectedValue, TextRechercheListeNoire.Text);
        }

        private void initialiseGridObservationVehicule()
        {
            serviceObservationVehicule.insertToGridObservationVehicule(gvObservationVehicule, " observationvehicule.numObservationVehicule", " observationvehicule.numObservationVehicule", "", hfNumVehicule.Value);
        }

        private void initialiseFormulaire()
        {
            LabelCouleur.Text = "";
            LabelMarque.Text = "";
            LabelMatricule.Text = "";
            LabelSerie.Text = "";
            LabelSouceEnergie.Text = "";
            LabelType.Text = "";

            hfNumVehicule.Value = "";
            hfObservationVehicule.Value = "";

            LabelVehiculeObservation.Text = "";

            TextObservation.Text = "";
            rbAvertissement.SelectedValue = "0";

            btnModifier.Enabled = false;
            btnValider.Enabled = false;
            this.initialiseGridObservationVehicule();

            this.divIndicationText("", "Red");

            Image_Vehicule.ImageUrl = "";
        }

        private void initialiseFormulaireObs()
        {
            hfObservationVehicule.Value = "";

            TextObservation.Text = "";
            rbAvertissement.SelectedValue = "0";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;

            this.divIndicationText("", "Red");
        }

        private void afficheVehicule(string numVehicule)
        {
            #region declaration
            crlVehicule vehicule = null;
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                vehicule = serviceVehicule.selectVehicule(numVehicule);

                if (vehicule != null)
                {
                    LabelCouleur.Text = vehicule.CouleurVehicule;
                    LabelMarque.Text = vehicule.MarqueVehicule;
                    LabelMatricule.Text = vehicule.MatriculeVehicule;
                    LabelSerie.Text = vehicule.NumSerieVehicule;
                    LabelSouceEnergie.Text = vehicule.SourceEnergie;
                    LabelType.Text = vehicule.TypeVehicule;

                    hfNumVehicule.Value = vehicule.NumVehicule;

                    Image_Vehicule.ImageUrl = ConfigurationManager.AppSettings["urlImageVehicule"] + vehicule.ImageVehicule;

                    LabelVehiculeObservation.Text = vehicule.MatriculeVehicule + " " + vehicule.MarqueVehicule + " " + vehicule.CouleurVehicule;

                    this.initialiseGridObservationVehicule();
                    this.initialiseFormulaireObs();
                }
            }
            #endregion
        }

        private void afficheObservation(string numObservation)
        {
            #region declaration
            crlObservationVehicule observation = null;
            crlVehicule vehicule = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numObservation != "")
            {
                observation = serviceObservationVehicule.selectObservationVehicule(numObservation);
                if (observation != null)
                {
                    this.afficheVehicule(observation.NumVehicule);

                    TextObservation.Text = observation.TextObesvationVehicule;

                    rbAvertissement.SelectedValue = observation.IsListeNoire.ToString();

                    hfObservationVehicule.Value = observation.NumObservationVehicule;
                    vehicule = serviceVehicule.selectVehicule(observation.NumVehicule);

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;

                    strConfirm = "Voulez vous vraiment modifier l'observation?\n";

                    if (vehicule != null)
                    {
                        strConfirm += "Véhicule: " + vehicule.MatriculeVehicule + " " + vehicule.MarqueVehicule + " " + vehicule.CouleurVehicule + "\n";
                        strConfirm += "Date: " + observation.DateObservation.ToString("dd MMMM yyyy");
                    }

                    ConfirmButtonExtender_btnModifier.ConfirmText = strConfirm;
                }
            }
            #endregion
        }

        private void insertToObjObservationVehicule(crlObservationVehicule observationVehicule)
        {
            if (observationVehicule != null)
            {
                observationVehicule.NumVehicule = hfNumVehicule.Value;
                observationVehicule.TextObesvationVehicule = TextObservation.Text;
                observationVehicule.IsListeNoire = int.Parse(rbAvertissement.SelectedValue);

            }
        }
        #endregion

        #region event
        protected void btnRechercheVehicule_Click(object sender, EventArgs e)
        {
            this.initialiseGridVehicule();
        }
        protected void ddlTriVehicule_SelectedIndexChanged(object sender, EventArgs e)
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


        protected void btnRechercheListeNoire_Click(object sender, EventArgs e)
        {
            this.initialiseGridListeNoireVehicule();
        }
        protected void ddlTriListeNoireVehicule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridListeNoireVehicule();
        }
        protected void gvListeNoireVehicule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListeNoireVehicule.PageIndex = e.NewPageIndex;
            this.initialiseGridListeNoireVehicule();
        }
        protected void gvListeNoireVehicule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheVehicule(e.CommandArgument.ToString());
            }
        }


        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlObservationVehicule observationVehicule = null;
            crlVehicule vehicule = null;
            string strIndicationText = "";
            #endregion

            #region implementation
            if (hfObservationVehicule.Value != "")
            {
                observationVehicule = serviceObservationVehicule.selectObservationVehicule(hfObservationVehicule.Value);
                if (observationVehicule != null)
                {
                    if (hfNumVehicule.Value != "")
                    {
                        this.insertToObjObservationVehicule(observationVehicule);
                        if (serviceObservationVehicule.updateObservationVehicule(observationVehicule))
                        {
                            this.initialiseGridObservationVehicule();
                            this.initialiseGridListeNoireVehicule();
                            this.initialiseFormulaireObs();

                            vehicule = serviceVehicule.selectVehicule(observationVehicule.NumVehicule);


                            strIndicationText = "Observation bien modifier!";
                            if (vehicule != null)
                            {
                                strIndicationText += " Véhicule: " + vehicule.MatriculeVehicule + " " + vehicule.MarqueVehicule + " " + vehicule.CouleurVehicule;
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
            crlObservationVehicule observationVehicule = null;
            crlVehicule vehicule = null;
            string strIndicationText = "";
            #endregion

            #region implementation
            if (hfNumVehicule.Value != "")
            {
                observationVehicule = new crlObservationVehicule();
                this.insertToObjObservationVehicule(observationVehicule);

                observationVehicule.NumObservationVehicule = serviceObservationVehicule.insertObservationVehicule(observationVehicule, agent.agence.SigleAgence);

                if (observationVehicule.NumObservationVehicule != "")
                {
                    this.initialiseGridObservationVehicule();
                    this.initialiseGridListeNoireVehicule();
                    this.initialiseFormulaireObs();

                    vehicule = serviceVehicule.selectVehicule(observationVehicule.NumVehicule);


                    strIndicationText = "Observation bien enregistrer!";
                    if (vehicule != null)
                    {
                        strIndicationText += " Véhicule: " + vehicule.MatriculeVehicule + " " + vehicule.MarqueVehicule + " " + vehicule.CouleurVehicule;
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


        protected void gvObservationVehicule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvObservationVehicule.PageIndex = e.NewPageIndex;
            this.initialiseGridObservationVehicule();
        }
        protected void gvObservationVehicule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheObservation(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("observationvehicule", "numObservationVehicule", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridObservationVehicule();
                    this.initialiseGridListeNoireVehicule();
                }
            }
        }
        #endregion
    }
}