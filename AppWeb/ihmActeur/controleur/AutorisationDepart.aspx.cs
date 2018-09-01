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

namespace AppWeb.ihmActeur.controleur
{
    public partial class AutorisationDepart : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalVoyage serviceVoyage = null;
        IntfDalCommission serviceCommission = null;
        IntfDalAutorisationDepart serviceAutorisationDepart = null;
        IntfDalTarifDeveloppement serviceTarifDeveloppement = null;
        IntfDalPrelevement servicePrelevement = null;
        IntfDalRecuAD serviceRecuAD = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalLien serviceLien = null;

        crlAgent agent = null;
        crlFicheBord ficheDebord = null;
        string numerosFB = null;
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
            serviceFicheBord = new ImplDalFicheBord();
            serviceVoyage = new ImplDalVoyage();
            serviceCommission = new ImplDalCommission();
            serviceAutorisationDepart = new ImplDalAutorisationDepart();
            serviceTarifDeveloppement = new ImplDalTarifDeveloppement();
            servicePrelevement = new ImplDalPrelevement();
            serviceRecuAD = new ImplDalRecuAD();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseGVListeFB();

                numerosFB = Request.QueryString["numerosFB"];

                if (numerosFB != null)
                {
                    this.afficheAutorisationVoyage(numerosFB);
                    hfNumerosFB.Value = numerosFB;

                    this.initialiseGridPassager();
                    this.initialiseGridCommission();
                    this.initialiseLabel();
                }
                else
                {
                    hfNumerosFB.Value = "";
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "311"))
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

        private void afficheAutorisationVoyage(string numerosFB)
        {
            if (numerosFB != "")
            {
                ficheDebord = serviceFicheBord.selectFicheBord(numerosFB);

                if (ficheDebord != null)
                {
                    labNumFB.Text = ficheDebord.NumerosFB;
                    labDateHeureFB.Text = ficheDebord.DateHeurPrevue.ToString("dd MMMM yyyy à HH:mm");

                    labNumAV.Text = ficheDebord.autorisationVoyage.NumerosAV;
                    labDateAV.Text = ficheDebord.autorisationVoyage.DatePrevueDepart.ToString("dd MMMM yyyy");

                    labNomChauffeur.Text = ficheDebord.autorisationVoyage.Verification.Chauffeur.nomChauffeur;
                    labPrenomChauffeur.Text = ficheDebord.autorisationVoyage.Verification.Chauffeur.prenomChauffeur;

                    labMatriculeVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;
                    labMarqueVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule;
                    labCouleurVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule;
                    labPoidsAutoriseVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.PoidsTotalVehicule + "Kg";

                    labItineraire.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + ficheDebord.autorisationVoyage.Verification.Itineraire.villeF.NomVille;
                    labDistance.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.DistanceParcour + "Km";
                    labDureeTrajet.Text = serviceGeneral.getTextTimeSpan(ficheDebord.autorisationVoyage.Verification.Itineraire.DureeTrajet);
                    labNombreRepos.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.NombreRepos.ToString();

                }
                else
                {
                    Response.Redirect("PlanningCalendarJour.aspx");
                }
            }
        }

        private void initialiseGridPassager()
        {
            serviceVoyage.insertToGrigVoyageAutorisationDepart(gvPassager, ddlTriListePassager.SelectedValue, ddlTriListePassager.SelectedValue, TextRechercheListePassager.Text, hfNumerosFB.Value);
        }

        private void initialiseGridCommission()
        {
            serviceCommission.insertToGridCommissionAutorisationDepart(gvCommission, ddlTriCommission.SelectedValue, ddlTriCommission.SelectedValue, TextRechercheCommission.Text, hfNumerosFB.Value);
        }

        private void initialiseLabel()
        {
            labNbPassager.Text = serviceFicheBord.getNombreTotalPassager(hfNumerosFB.Value).ToString();
            labPoidTotalBagage.Text = serviceFicheBord.getPoidTotalBagage(hfNumerosFB.Value).ToString("0") + "Kg";
            labSommeRecu.Text = serviceGeneral.separateurDesMilles((serviceFicheBord.getPrixTotalBillet(hfNumerosFB.Value) + serviceFicheBord.getPrixTotalBagage(hfNumerosFB.Value)).ToString("0")) + "Ar";

            labPoidTotalCommission.Text = serviceFicheBord.getPoidTotalCommission(hfNumerosFB.Value).ToString("0") + "Kg";
            labTotalFraisCommission.Text = serviceGeneral.separateurDesMilles(serviceFicheBord.getPrixTotalCommission(hfNumerosFB.Value).ToString("0")) + "Ar";
        }

        private void insertObjetAD(crlAutorisationDepart autorisationDepart)
        {
            #region declaration
            double montantRecette = 0.00;
            #endregion

            #region implementation
            if (autorisationDepart != null && hfNumerosFB.Value != "")
            {
                montantRecette = serviceFicheBord.getPrixTotalBagage(hfNumerosFB.Value) + serviceFicheBord.getPrixTotalBillet(hfNumerosFB.Value) + serviceFicheBord.getPrixTotalCommission(hfNumerosFB.Value);

                autorisationDepart.agent = agent;

                autorisationDepart.NumerosFB = hfNumerosFB.Value;
                autorisationDepart.MatriculeAgent = agent.matriculeAgent;
                autorisationDepart.RecetteTotale = montantRecette;
                autorisationDepart.ResteRegle = montantRecette;

            }
            #endregion
        }

        private void initialiseGVListeFB()
        {
            serviceFicheBord.insertToGridFicheBordSansAD(gvFicheBord, ddlTriListeFB.SelectedValue, ddlTriListeFB.SelectedValue, TextRechercheFB.Text, agent.numAgence);
        }

        private void prelevementFond(string numAutorisationDepart)
        {
            #region declaration
            crlPrelevement prelevement = null;
            crlAutorisationDepart autorisationDepart = null;
            crlRecuAD recuAD = null;
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(numAutorisationDepart);
                if (autorisationDepart != null)
                {
                    if (autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.Fond > 0)
                    {
                        prelevement = new crlPrelevement();
                        prelevement.agent = agent;
                        prelevement.MatriculeAgent = agent.matriculeAgent;
                        prelevement.MontantPrelevement = autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.Fond;
                        prelevement.NumAutorisationDepart = autorisationDepart.NumAutorisationDepart;
                        prelevement.TypePrelevement = "Fond";

                        prelevement.NumPrelevement = servicePrelevement.insertPrelevement(prelevement);

                        if (prelevement.NumPrelevement != "")
                        {
                            recuAD = new crlRecuAD();
                            recuAD.Libele = "Fond";
                            recuAD.agent = agent;
                            recuAD.MatriculeAgent = agent.matriculeAgent;
                            recuAD.Montant = prelevement.MontantPrelevement.ToString("0");
                            recuAD.NumPrelevement = prelevement.NumPrelevement;
                            recuAD.NumRecuAD = serviceRecuAD.insertRecuAD(recuAD);

                            autorisationDepart.ResteRegle = autorisationDepart.ResteRegle - prelevement.MontantPrelevement;

                            serviceAutorisationDepart.updateAutorisationDepart(autorisationDepart);
                        }
                        else
                        {
                        }
                    }
                }
            }

            #endregion
        }
        #endregion

        #region event
        protected void ddlTriListePassager_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridPassager();
        }
        protected void btnRecherchePassager_Click(object sender, EventArgs e)
        {
            this.initialiseGridPassager();
        }

        protected void ddlTriCommission_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }
        protected void btnRechercheCommission_Click(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }

        protected void btnAutorisationDepart_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAutorisationDepart autorisationDepart = null;
            crlTarifDeveloppement tarifDeveloppement = null;
            crlPrelevement prelevement = null;
            crlRecuAD recuAD = null;
            #endregion

            #region implementation
            if (hfNumerosFB.Value != "")
            {
                autorisationDepart = new crlAutorisationDepart();
                this.insertObjetAD(autorisationDepart);

                autorisationDepart.NumAutorisationDepart = serviceAutorisationDepart.insertAutorisationDepart(autorisationDepart);


                if (autorisationDepart.NumAutorisationDepart != "")
                {
                    autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(autorisationDepart.NumAutorisationDepart);
                    if (autorisationDepart != null)
                    {
                        tarifDeveloppement = serviceTarifDeveloppement.selectTarifDeveloppementZone(autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.Zone);


                        if (tarifDeveloppement != null)
                        {
                            prelevement = new crlPrelevement();
                            prelevement.agent = agent;
                            prelevement.MatriculeAgent = agent.matriculeAgent;
                            prelevement.MontantPrelevement = tarifDeveloppement.MontantTarifDeveloppement;
                            prelevement.NumAutorisationDepart = autorisationDepart.NumAutorisationDepart;
                            prelevement.TypePrelevement = "Développement";

                            prelevement.NumPrelevement = servicePrelevement.insertPrelevement(prelevement);

                            if (prelevement.NumPrelevement != "")
                            {
                                recuAD = new crlRecuAD();
                                recuAD.Libele = "Développement";
                                recuAD.agent = agent;
                                recuAD.MatriculeAgent = agent.matriculeAgent;
                                recuAD.Montant = prelevement.MontantPrelevement.ToString("0");
                                recuAD.NumPrelevement = prelevement.NumPrelevement;
                                recuAD.NumRecuAD = serviceRecuAD.insertRecuAD(recuAD);

                                autorisationDepart.ResteRegle = autorisationDepart.ResteRegle - prelevement.MontantPrelevement;

                                serviceAutorisationDepart.updateAutorisationDepart(autorisationDepart);

                                this.prelevementFond(autorisationDepart.NumAutorisationDepart);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                    ficheDebord = serviceFicheBord.selectFicheBord(hfNumerosFB.Value);
                    ficheDebord.DateHeurDepart = DateTime.Now;
                    serviceFicheBord.updateFicheBord(ficheDebord);

                    Response.Redirect("~/ihmActeur/controleur/PlanningCalendarJourAD.aspx");

                    //Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReAutorisationDepart.ADBienEnregistre + "');", true);
                }
                else
                {
                    this.divIndicationText(ReAutorisationDepart.ADNonEnregistre, "Red");
                }
            }
            else
            {
                Response.Redirect("PlanningCalendarJour.aspx");
            }
            #endregion
        }

        protected void gvFicheBord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFicheBord.PageIndex = e.NewPageIndex;
            this.initialiseGVListeFB();
        }
        protected void gvFicheBord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                this.afficheAutorisationVoyage(e.CommandArgument.ToString());
                hfNumerosFB.Value = e.CommandArgument.ToString();

                this.initialiseGridPassager();
                this.initialiseGridCommission();
                this.initialiseLabel();
            }
        }
        protected void bvtnRechercheFB_Click(object sender, EventArgs e)
        {
            this.initialiseGVListeFB();
        }
        protected void ddlTriListeFB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGVListeFB();
        }
        #endregion
    }
}