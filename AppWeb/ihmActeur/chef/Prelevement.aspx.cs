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

namespace AppWeb.ihmActeur.chef
{
    public partial class Prelevement : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAutorisationDepart serviceAutorisationDepart = null;
        IntfDalRecuAD serviceRecuAD = null;
        IntfDalTypeRecuAD serviceTypeRecuAD = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalPrelevement servicePrelevement = null;
        IntfDalTypePrelevement serviceTypePrelevement = null;
        IntfDalLien serviceLien = null;

        crlAgent agent = null;
        crlAutorisationDepart autorisationDepart = null;

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
            serviceAutorisationDepart = new ImplDalAutorisationDepart();
            serviceRecuAD = new ImplDalRecuAD();
            serviceTypeRecuAD = new ImplDalTypeRecuAD();
            serviceFicheBord = new ImplDalFicheBord();
            serviceGeneral = new ImplDalGeneral();
            servicePrelevement = new ImplDalPrelevement();
            serviceTypePrelevement = new ImplDalTypePrelevement();
            #endregion

            #region !IsPostBack 
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                hfAutorisationDepart.Value = "";
                hfPrelevement.Value = "";

                serviceTypePrelevement.loadDddlTypePrelevement(ddlTypeRecuAD);

                this.initialiseGridAD();

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "211"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridAD()
        {
            serviceAutorisationDepart.insertToGridAutorisationDepart(gvAutorisationDeVoyage, ddlTriRechercheAD.SelectedValue, ddlTriRechercheAD.SelectedValue, TextRechercheAD.Text, agent.agence.NumAgence);
        }

        private void initialiseGridRecuAD()
        {
            if (hfAutorisationDepart.Value != "")
            {
                serviceRecuAD.insertToGridAvanceAutorisationDepart(gvRecuAD, hfAutorisationDepart.Value);
            }
        }

        private void initialiseGridPrelevement()
        {
            if (hfAutorisationDepart.Value != "")
            {
                servicePrelevement.insertToGridPrelevementNonPayer(gvPrelevement, hfAutorisationDepart.Value);
            }
        }

        private void initialiseFormulaireAvance()
        {
            TextMontant.Text = "";
            this.divIndicationText("", "");
            hfPrelevement.Value = "";
            ddlTypeRecuAD.SelectedValue = "";
            btnModifier.Enabled = false;
            btnValideAvance.Enabled = true;
        }

        private void divIndicationText(string str, string color)
        {
            if (str != "" && color != "")
            {
                divIndication.Style.Add("font-size", "14px");
                divIndication.Style.Add("color", color);
                divIndication.InnerText = str;
            }
            else
            {
                divIndication.InnerText = "";
            }
        }

        private void afficheAutorisationDepart(string numAutorisationDepart)
        {
            if (numAutorisationDepart != "")
            {
                autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(numAutorisationDepart);

                if (autorisationDepart != null)
                {
                    hfAutorisationDepart.Value = autorisationDepart.NumAutorisationDepart;


                    labNumFB.Text = autorisationDepart.ficheBord.NumerosFB;
                    labDateHeureFB.Text = autorisationDepart.ficheBord.DateHeurPrevue.ToString("dd MMMM yyyy à HH:mm");

                    labNumAV.Text = autorisationDepart.ficheBord.autorisationVoyage.NumerosAV;
                    labDateAV.Text = autorisationDepart.ficheBord.autorisationVoyage.DatePrevueDepart.ToString("dd MMMM yyyy");

                    labNomChauffeur.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Chauffeur.nomChauffeur;
                    labPrenomChauffeur.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Chauffeur.prenomChauffeur;

                    labMatriculeVoiture.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;
                    labMarqueVoiture.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule;
                    labCouleurVoiture.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule;
                    labPoidsAutoriseVoiture.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.PoidsTotalVehicule + "Kg";

                    labItineraire.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.villeF.NomVille;
                    labDistance.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.DistanceParcour + "Km";
                    labDureeTrajet.Text = serviceGeneral.getTextTimeSpan(autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.DureeTrajet);
                    labNombreRepos.Text = autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.NombreRepos.ToString();

                    labMotant.Text = serviceGeneral.separateurDesMilles(autorisationDepart.RecetteTotale.ToString("0")) + "Ar";
                    labReste.Text = serviceGeneral.separateurDesMilles(autorisationDepart.ResteRegle.ToString("0")) + "Ar";

                    LabCarburantMax.Text = serviceGeneral.separateurDesMilles(autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.AvanceCarburantMax.ToString("0")) + "Ar";
                    LabChauffeurMax.Text = serviceGeneral.separateurDesMilles(autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.AvanceChauffeurMax.ToString("0")) + "Ar";
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReAvanceAutorisationDepart.erreurSurAffiche + "');", true);
                }
            }
        }

        private void initialiseErrorMessage()
        {
            TextMontant_RequiredFieldValidator.ErrorMessage = ReAvanceAutorisationDepart.montantNonVide;
            ddlTypeRecuAD_RequiredFieldValidator.ErrorMessage = ReAvanceAutorisationDepart.typeNonVide;
        }

        private void affichePrelevement(string numPrelevement)
        {
            #region declaration
            crlPrelevement prelevement = null;
            #endregion

            #region implementation
            if (numPrelevement != "")
            {
                prelevement = servicePrelevement.selectPrelevement(numPrelevement);
                if (prelevement != null)
                {
                    TextMontant.Text = prelevement.MontantPrelevement.ToString("0");
                    ddlTypeRecuAD.SelectedValue = prelevement.TypePrelevement;
                    hfPrelevement.Value = prelevement.NumPrelevement;
                }
            }
            #endregion
        }

        /*
        private bool testFormulaire()
        {
            #region declaration
            bool isTest = false;
            #endregion

            #region implementation
            if (ddlTypeRecuAD.SelectedValue != "")
            {
                if (TextMontant.Text != "")
                {
                    isTest = true;
                }
                else
                {
                    this.divIndicationText(ReAvanceAutorisationDepart.montantNonVide, "red");
                }
            }
            else
            {
                this.divIndicationText(ReAvanceAutorisationDepart.typeNonVide, "red");
            }
            #endregion

            return isTest;
        }*/
        #endregion

        #region event
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireAvance();
        }

        protected void btnValideAvance_Click(object sender, EventArgs e)
        {
            #region declaration
            crlPrelevement prelevement = null;
            #endregion

            #region implementation
            if (hfAutorisationDepart.Value != "")
            {

                try
                {
                    if (serviceRecuAD.isValideMontant(double.Parse(TextMontant.Text), hfAutorisationDepart.Value))
                    {
                        prelevement = new crlPrelevement();

                        prelevement.agent = agent;
                        prelevement.MatriculeAgent = agent.matriculeAgent;
                        try
                        {
                            prelevement.MontantPrelevement = double.Parse(TextMontant.Text);
                        }
                        catch (Exception)
                        {
                        }
                        prelevement.NumAutorisationDepart = hfAutorisationDepart.Value;
                        prelevement.TypePrelevement = ddlTypeRecuAD.SelectedValue;

                        prelevement.NumPrelevement = servicePrelevement.insertPrelevement(prelevement);

                        if (prelevement.NumPrelevement != "")
                        {
                            this.initialiseFormulaireAvance();
                            this.initialiseGridRecuAD();
                            this.initialiseGridPrelevement();
                            /*hfPrelevement.Value = prelevement.NumPrelevement;
                            btnModifier.Enabled = true;
                            btnValideAvance.Enabled = false;*/
                            /*
                            autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(hfAutorisationDepart.Value);
                            autorisationDepart.ResteRegle = autorisationDepart.ResteRegle - prelevement.MontantPrelevement;
                            serviceAutorisationDepart.updateAutorisationDepart(autorisationDepart);

                            
                            this.afficheAutorisationDepart(hfAutorisationDepart.Value);
                             */


                            this.divIndicationText("Prélèvement N°" + prelevement.NumPrelevement + " bien enregistrer! Montant: " + serviceGeneral.separateurDesMilles(prelevement.MontantPrelevement.ToString("0")) + "Ar", "Black");
                        }
                        else
                        {
                            this.divIndicationText(ReAvanceAutorisationDepart.avanceNonEnregistre, "red");
                        }
                    }
                    else
                    {
                        this.divIndicationText(ReAvanceAutorisationDepart.prixNonValide, "red");
                    }
                }
                catch (Exception)
                {
                    this.divIndicationText(ReAvanceAutorisationDepart.erreurSurMontant, "red");
                }

            }
            else
            {
                this.divIndicationText(ReAvanceAutorisationDepart.autorisatrionDepartNonSelectionner, "red");
            }
            #endregion
        }

        protected void gvAutorisationDeVoyage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAutorisationDeVoyage.PageIndex = e.NewPageIndex;
            this.initialiseGridAD();
        }
        protected void gvAutorisationDeVoyage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheAutorisationDepart(e.CommandArgument.ToString().Trim());
                this.initialiseGridRecuAD();
                this.initialiseGridPrelevement();
                this.initialiseFormulaireAvance();
            }
        }

        protected void ddlTriRechercheAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAD();
        }
        protected void btnRechercheAD_Click(object sender, EventArgs e)
        {
            this.initialiseGridAD();
        }

        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlPrelevement prelevement = null;
            #endregion

            #region implementation
            if (hfPrelevement.Value != "")
            {
                prelevement = servicePrelevement.selectPrelevement(hfPrelevement.Value);
                if (prelevement != null)
                {
                    prelevement.MontantPrelevement = double.Parse(TextMontant.Text);
                    prelevement.MatriculeAgent = agent.matriculeAgent;
                    prelevement.TypePrelevement = ddlTypeRecuAD.SelectedValue;

                    bool isUpdate = servicePrelevement.updatePrelevement(prelevement);

                    if (isUpdate)
                    {
                        this.initialiseGridPrelevement();
                        this.divIndicationText("Prélèvement N°" + prelevement.NumPrelevement + " bien modifier! Montant: " + serviceGeneral.separateurDesMilles(prelevement.MontantPrelevement.ToString("0")) + "Ar", "Black");
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
        protected void gvPrelevement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteV"))
            {
                serviceGeneral.delete("prelevement", "numPrelevement", e.CommandArgument.ToString());
                this.initialiseGridPrelevement();
            }
            this.initialiseFormulaireAvance();
            if (e.CommandName.Equals("select"))
            {
                this.affichePrelevement(e.CommandArgument.ToString());
                btnModifier.Enabled = true;
                btnValideAvance.Enabled = false;
            }

        }
        #endregion
    }
}