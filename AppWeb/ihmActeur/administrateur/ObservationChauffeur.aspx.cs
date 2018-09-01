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
    public partial class ObservationChauffeur : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalChauffeur serviceChauffeur = null;
        IntfDalObservationChauffeur serviceObservationChauffeur = null;
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
            serviceChauffeur = new ImplDalChauffeur();
            serviceObservationChauffeur = new ImplDalObservationChauffeur();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseGridChauffeur();
                this.initialiseGridListeNoireChauffeur();
                this.initialiseFormulaire();
                this.initialiseGridObservation();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "062"))
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
            TextObservation_RequiredFieldValidator.ErrorMessage = ReObservationChauffeur.ObservationNonVide;
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

        private void initialiseGridChauffeur()
        {
            serviceChauffeur.insertToGridChauffeurAll(gvChauffeur, ddlTriChauffeur.SelectedValue, ddlTriChauffeur.SelectedValue, TextRechercheChauffeur.Text);
        }

        private void initialiseGridListeNoireChauffeur()
        {
            serviceChauffeur.insertToGridChauffeurListeNoire(gvListeNoire, ddlTriListeNoire.SelectedValue, ddlTriListeNoire.SelectedValue, TextRechercheListeNoire.Text);
        }

        private void initialiseFormulaire()
        {
            LabelNomChauffeur.Text = "";
            LabelPrenomChauffeur.Text = "";
            LabelAdresseChauffeur.Text = "";
            LabelCINAdresseChauffeur.Text = "";
            LabelTelephone.Text = "";

            hfIdChauffeur.Value = "";
            hfObservationChauffeur.Value = "";

            LabelNomChauffeurObservation.Text = "";

            TextObservation.Text = "";
            rbAvertissement.SelectedValue = "0";

            btnModifier.Enabled = false;
            btnValider.Enabled = false;
            this.initialiseGridObservation();

            this.divIndicationText("", "Red");

            Image_Chauffeur.ImageUrl = "";
        }

        private void initialiseFormulaireObs()
        {
            hfObservationChauffeur.Value = "";

            TextObservation.Text = "";
            rbAvertissement.SelectedValue = "0";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;

            this.divIndicationText("", "Red");
        }

        private void afficheChauffeur(string idChauffeur)
        {
            #region declaration
            crlChauffeur chauffeur = null;
            #endregion

            #region implementation
            if (idChauffeur != "")
            {
                chauffeur = serviceChauffeur.selectChauffeur(idChauffeur);
                if (chauffeur != null)
                {
                    LabelNomChauffeur.Text = chauffeur.nomChauffeur;
                    LabelPrenomChauffeur.Text = chauffeur.prenomChauffeur;
                    LabelAdresseChauffeur.Text = chauffeur.adresseChauffeur;
                    LabelCINAdresseChauffeur.Text = chauffeur.cinChauffeur;
                    LabelTelephone.Text = chauffeur.telephoneChauffeur + " " + chauffeur.telephoneMobileChauffeur;

                    Image_Chauffeur.ImageUrl = ConfigurationManager.AppSettings["urlImageChauffeur"] + chauffeur.ImageChauffeur;

                    LabelNomChauffeurObservation.Text = " / " + chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur;

                    hfIdChauffeur.Value = chauffeur.idChauffeur;
                    this.initialiseGridObservation();
                    this.initialiseFormulaireObs();
                }
            }
            #endregion
        }

        private void afficheObservation(string numObservation)
        {
            #region declaration
            crlObservationChauffeur observationChauffeur = null;
            crlChauffeur chauffeur = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numObservation != "")
            {
                observationChauffeur = serviceObservationChauffeur.selectObservationChauffeur(numObservation);

                if (observationChauffeur != null)
                {
                    this.afficheChauffeur(observationChauffeur.IdChauffeur);
                    TextObservation.Text = observationChauffeur.TextObesvation;

                    rbAvertissement.SelectedValue = observationChauffeur.IsListeNoire.ToString();

                    hfObservationChauffeur.Value = observationChauffeur.NumObservation;
                    chauffeur = serviceChauffeur.selectChauffeur(observationChauffeur.IdChauffeur);

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;


                    strConfirm = "Voulez vous vraiment modifier l'observation?\n";

                    if (chauffeur != null)
                    {
                        strConfirm += "Chauffeur: " + chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur + "\n";
                        strConfirm += "Date: " + observationChauffeur.DateObservation.ToString("dd MMMM yyyy");
                    }

                    ConfirmButtonExtender_btnModifier.ConfirmText = strConfirm;
                }
            }
            #endregion
        }

        private void insertToObjObservationChauffeur(crlObservationChauffeur observationChauffeur)
        {
            if (observationChauffeur != null)
            {
                observationChauffeur.IdChauffeur = hfIdChauffeur.Value;
                observationChauffeur.TextObesvation = TextObservation.Text;
                observationChauffeur.IsListeNoire = int.Parse(rbAvertissement.SelectedValue);

            }
        }

        private void initialiseGridObservation()
        {
            serviceObservationChauffeur.insertToGridObservationChauffeur(gvObservationChauffeur, "observationchauffeur.numObservation", "observationchauffeur.numObservation", "", hfIdChauffeur.Value);
        }
        #endregion

        #region event
        protected void ddlTriChauffeur_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridChauffeur();
        }
        protected void btnRechercheChauffeur_Click(object sender, EventArgs e)
        {
            this.initialiseGridChauffeur();
        }
        protected void gvChauffeur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChauffeur.PageIndex = e.NewPageIndex;
            this.initialiseGridChauffeur();
        }
        protected void gvChauffeur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheChauffeur(e.CommandArgument.ToString());
            }
        }

        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlObservationChauffeur observationChauffeur = null;
            crlChauffeur chauffeur = null;
            string strIndicationText = "";
            #endregion

            #region implementation
            if (hfObservationChauffeur.Value != "")
            {
                observationChauffeur = serviceObservationChauffeur.selectObservationChauffeur(hfObservationChauffeur.Value);
                if (observationChauffeur != null)
                {
                    if (hfIdChauffeur.Value != "")
                    {
                        this.insertToObjObservationChauffeur(observationChauffeur);
                        if (serviceObservationChauffeur.updateObservationChauffeur(observationChauffeur))
                        {
                            this.initialiseGridObservation();
                            this.initialiseGridListeNoireChauffeur();
                            this.initialiseFormulaireObs();

                            chauffeur = serviceChauffeur.selectChauffeur(observationChauffeur.IdChauffeur);


                            strIndicationText = "Observation bien modifier!";
                            if (chauffeur != null)
                            {
                                strIndicationText += " Chauffeur: " + chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur;
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
            crlObservationChauffeur observationChauffeur = null;
            crlChauffeur chauffeur = null;
            string strIndicationText = "";
            #endregion

            #region implementation
            if (hfIdChauffeur.Value != "")
            {
                observationChauffeur = new crlObservationChauffeur();
                this.insertToObjObservationChauffeur(observationChauffeur);

                observationChauffeur.NumObservation = serviceObservationChauffeur.insertObservationChauffeur(observationChauffeur, agent.agence.SigleAgence);

                if (observationChauffeur.NumObservation != "")
                {
                    this.initialiseGridObservation();
                    this.initialiseGridListeNoireChauffeur();
                    this.initialiseFormulaireObs();

                    chauffeur = serviceChauffeur.selectChauffeur(observationChauffeur.IdChauffeur);


                    strIndicationText = "Observation bien enregistrer!";
                    if (chauffeur != null)
                    {
                        strIndicationText += " Chauffeur: " + chauffeur.prenomChauffeur + " " + chauffeur.nomChauffeur;
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


        protected void gvListeNoire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListeNoire.PageIndex = e.NewPageIndex;
            this.initialiseGridListeNoireChauffeur();
        }
        protected void gvListeNoire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheChauffeur(e.CommandArgument.ToString());
            }
        }
        protected void ddlTriListeNoire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridListeNoireChauffeur();
        }
        protected void btnRechercheListeNoire_Click(object sender, EventArgs e)
        {
            this.initialiseGridListeNoireChauffeur();
        }

        protected void gvObservationChauffeur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvObservationChauffeur.PageIndex = e.NewPageIndex;
            this.initialiseGridObservation();
        }
        protected void gvObservationChauffeur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheObservation(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("observationchauffeur", "numObservation", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridObservation();
                    this.initialiseGridListeNoireChauffeur();
                }
            }
        }
        #endregion
    }
}