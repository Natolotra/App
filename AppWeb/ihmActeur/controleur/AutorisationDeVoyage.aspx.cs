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

namespace AppWeb.ihmActeur.controleur
{
    public partial class AutorisationDeVoyage : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;

        IntfAutorisationVoyage serviceAutorisationVoyage = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalPlaceFB servicePlaceFB = null;
        IntfDalLien serviceLien = null;

        crlAgent agent = null;
        crlAutorisationVoyage autorisationVoyage = null;
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
            serviceAutorisationVoyage = new ImplAutorisationVoyage(this.serviceRessource.getDefaultStrConnection());
            serviceFicheBord = new ImplDalFicheBord(this.serviceRessource.getDefaultStrConnection());
            servicePlaceFB = new ImplDalPlaceFB(this.serviceRessource.getDefaultStrConnection());
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                this.loadDDLTriAutorisation();
                this.initialiseGridAutorisation();

                hfNumerosAV.Value = "";
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "301"))
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

        private void loadDDLTriAutorisation()
        {
            ddlTriAutorisation.Items.Clear();
            ddlTriAutorisation.Items.Add(new ListItem("N°", "autorisationvoyage.numerosAV"));
            ddlTriAutorisation.Items.Add(new ListItem("Matricule véhicule", "matriculeVehicule"));
            ddlTriAutorisation.Items.Add(new ListItem("Marque véhicule", "marqueVehicule"));
            ddlTriAutorisation.Items.Add(new ListItem("Couleur véhicule", "couleurVehicule"));
            ddlTriAutorisation.Items.Add(new ListItem("Nom chauffeur", "nomChauffeur"));
            ddlTriAutorisation.Items.Add(new ListItem("Prénom chauffeur", "prenomChauffeur"));
            //ddlTriAutorisation.Items.Add(new ListItem("Prénom chauffeur", "prenomChauffeur"));
            ddlTriAutorisation.Items.Add(new ListItem("Date prevue de départ", "datePrevueDepart"));
            //ddlTriAutorisation.Items.Add(new ListItem("Itineraire", "villeItineraireDebut;villeItineraireFin"));
            //ddlTriAutorisation.Items.Add(new ListItem("Itineraire", "villeItineraireFin"));
        }
        private void initialiseGridAutorisation()
        {
            serviceAutorisationVoyage.insertToGridAutorisationSansFicheBordASC(gvAutorisationVoyageNonFiche, ddlTriAutorisation.SelectedValue, ddlTriAutorisation.SelectedValue, TextRechercheAutorisation.Text, agent.numAgence);
        }

        private void initialiseFormulaire()
        {
            TextDatePrevueDepart.Text = "";
            TextItineraire.Text = "";
            TextMatriculVoiture.Text = "";
            TextNomAgentTechnique.Text = "";
            TextNomAgentVerificateur.Text = "";
            TextNomChauffeur.Text = "";
            TextPrenomAT.Text = "";
            TextPrenomAV.Text = "";
            TextPrenomChauffeur.Text = "";
            hfNumerosAV.Value = "";
            divIndication.InnerText = "";

            Image_Vehicule.ImageUrl = "";
        }

        private void afficheAutorisationVoyage(string numerosAV)
        {
            #region implementation
            autorisationVoyage = serviceAutorisationVoyage.selectAutorisationVoyage(numerosAV);

            if (autorisationVoyage != null)
            {
                hfNumerosAV.Value = autorisationVoyage.NumerosAV;

                TextNomAgentTechnique.Text = autorisationVoyage.Agent.nomAgent;
                TextPrenomAT.Text = autorisationVoyage.Agent.prenomAgent;

                TextNomAgentVerificateur.Text = autorisationVoyage.Verification.Agent.nomAgent;
                TextPrenomAV.Text = autorisationVoyage.Verification.Agent.prenomAgent;

                TextNomChauffeur.Text = autorisationVoyage.Verification.Chauffeur.nomChauffeur;
                TextPrenomChauffeur.Text = autorisationVoyage.Verification.Chauffeur.prenomChauffeur;

                TextMatriculVoiture.Text = autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;
                TextItineraire.Text = autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + autorisationVoyage.Verification.Itineraire.villeF.NomVille;
                TextDatePrevueDepart.Text = autorisationVoyage.DatePrevueDepart.ToString("dd MMMM yyyy");

                Image_Vehicule.ImageUrl = ConfigurationManager.AppSettings["urlImageVehicule"] + autorisationVoyage.Verification.Licence.vehicule.ImageVehicule;
            }
            else
            {
                this.divIndicationText(ReRealiseFB.affichageAutorisationNonOk, "red");
            }
            #endregion
        }

        private bool testFormulaire()
        {
            #region declaration
            bool isTest = false;
            #endregion

            #region implementation
            if (TextDateDepart.Text != "")
            {
                if (ddlHeure.SelectedValue != "")
                {
                    if (ddlMinute.SelectedValue != "")
                    {
                        isTest = true;
                    }
                    else
                    {
                        this.divIndicationText(ReRealiseFB.heureNonVide, "red");
                    }
                }
                else
                {
                    this.divIndicationText(ReRealiseFB.heureNonVide, "red");
                }
            }
            else
            {
                this.divIndicationText(ReRealiseFB.dateNonVide, "red");
            }
            #endregion

            return isTest;
        }

        private void initialiseErrorMessage()
        {
            TextDateDepart_RequiredFieldValidator.ErrorMessage = ReRealiseFB.dateNonVide;
            TextNomAgentTechnique_RequiredFieldValidator.ErrorMessage = ReRealiseFB.choisireAV;
        }
        #endregion

        #region event
        protected void btnRechercheAutorisation_Click(object sender, EventArgs e)
        {
            this.initialiseGridAutorisation();
        }
        protected void ddlTriAutorisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAutorisation();
        }
        protected void gvAutorisationVoyageNonFiche_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAutorisationVoyageNonFiche.PageIndex = e.NewPageIndex;
            this.initialiseGridAutorisation();
        }
        protected void gvAutorisationVoyageNonFiche_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheAutorisationVoyage(e.CommandArgument.ToString().Trim());
            }
        }
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnValide_Click(object sender, EventArgs e)
        {
            #region initialisation
            crlFicheBord FicheBord = new crlFicheBord();
            int annee = 0;
            int mois = 0;
            int jour = 0;
            int heure = 0;
            int minute = 0;

            string numerosFB = "";
            #endregion

            #region implementataion
            if (hfNumerosAV.Value != "" && this.testFormulaire())
            {
                FicheBord.MatriculeAgent = agent.matriculeAgent;
                FicheBord.agent = agent;
                FicheBord.NumerosAV = hfNumerosAV.Value;
                FicheBord.autorisationVoyage = serviceAutorisationVoyage.selectAutorisationVoyage(FicheBord.NumerosAV);
                try
                {
                    DateTime datePrevu = Convert.ToDateTime(TextDateDepart.Text);
                    annee = datePrevu.Year;
                    mois = datePrevu.Month;
                    jour = datePrevu.Day;
                    heure = int.Parse(ddlHeure.SelectedValue);
                    minute = int.Parse(ddlMinute.SelectedValue);
                    FicheBord.DateHeurPrevue = new DateTime(annee, mois, jour, heure, minute, 0);

                }
                catch (Exception)
                {

                }

                numerosFB = serviceFicheBord.insertFicheBord(FicheBord);

                if (numerosFB != "")
                {
                    servicePlaceFB.insertPlaceForFB(FicheBord);
                    Response.Redirect("FicheDeBord.aspx");
                }
                else
                {
                    this.divIndicationText(ReRealiseFB.ficheBordNonRealise, "red");
                }
            }
            else
            {
                this.divIndicationText(ReRealiseFB.choisireAV, "red");
            }

            #endregion
        }
        #endregion
    }
}