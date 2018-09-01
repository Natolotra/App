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
        IntfDalSessionCaisse serviceSessionCaisse = null;
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
            serviceSessionCaisse = new ImplDalSessionCaisse();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                hfAutorisationDepart.Value = "";
                hfPrelevement.Value = "";

                serviceRecuAD.loadDdlLibelle(ddlLibelle);

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "042"))
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
                    try
                    {
                        labReste.Text = serviceGeneral.separateurDesMilles(autorisationDepart.ResteRegle.ToString("0")) + "Ar";
                    }
                    catch (Exception)
                    {
                    }

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "alert('" + ReAvanceAutorisationDepart.erreurSurAffiche + "');", true);
                }
            }
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
                    hfPrelevement.Value = prelevement.NumPrelevement;

                    TextMontant.Text = prelevement.MontantPrelevement.ToString("0");
                    TextType.Text = prelevement.objTypePrelevement.Commentaire;
                }
            }
            #endregion
        }
        #endregion

        #region event
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

        protected void gvPrelevement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.affichePrelevement(e.CommandArgument.ToString().Trim());
            }
        }
        #endregion

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            TextMontant.Text = "";
            TextType.Text = "";
            hfPrelevement.Value = "";
            ddlLibelle.SelectedValue = "";
        }
        protected void btnValideAvance_Click(object sender, EventArgs e)
        {
            #region declaration
            crlRecuAD recu = null;
            crlPrelevement prelevement = null;
            crlAutorisationDepart autorisationDepart = null;
            #endregion

            #region implementation
            if (hfPrelevement.Value != "" && hfAutorisationDepart.Value != "")
            {
                if (agent.sessionCaisse != null)
                {
                    autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(hfAutorisationDepart.Value);
                    prelevement = servicePrelevement.selectPrelevement(hfPrelevement.Value);
                    if (prelevement != null && autorisationDepart != null)
                    {
                        if (serviceRecuAD.isValideMontant(prelevement.MontantPrelevement, autorisationDepart.NumAutorisationDepart))
                        {
                            recu = new crlRecuAD();
                            recu.agent = agent;
                            recu.Date = DateTime.Now;
                            recu.Libele = ddlLibelle.Text;
                            recu.MatriculeAgent = agent.matriculeAgent;
                            recu.Montant = prelevement.MontantPrelevement.ToString("0");
                            recu.NumPrelevement = prelevement.NumPrelevement;

                            recu.NumRecuAD = serviceRecuAD.insertRecuAD(recu);

                            if (recu.NumRecuAD != "")
                            {
                                serviceSessionCaisse.insertAssocSessionCaisseRecuAD(recu.NumRecuAD, agent.sessionCaisse.NumSessionCaisse);

                                this.initialiseGridPrelevement();
                                this.initialiseGridRecuAD();

                                autorisationDepart.ResteRegle = autorisationDepart.ResteRegle - prelevement.MontantPrelevement;
                                serviceAutorisationDepart.updateAutorisationDepart(autorisationDepart);

                                TextMontant.Text = "";
                                TextType.Text = "";
                                hfPrelevement.Value = "";
                                ddlLibelle.SelectedValue = "";
                                this.afficheAutorisationDepart(hfAutorisationDepart.Value);

                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                        string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=recuAD&numRecuAD=" + recu.NumRecuAD, 700,
                                        500, 10, 10), true);
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
            }
            else
            {
                //
            }
            #endregion
        }
    }
}