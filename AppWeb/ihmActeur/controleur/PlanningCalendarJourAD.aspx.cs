using arch.crl;
using arch.dal.impl;
using arch.dal.intf;
using DayPilot.Web.Ui.Events.Bubble;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.controleur
{
    public partial class PlanningCalendarJourAD : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalCalendar serviceCalendar = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalAutorisationDepart serviceAutorisationDepart = null;
        IntfDalPlaceFB servicePlaceFB = null;
        IntfDalItineraire serviceItineraire = null;
        IntfDalLien serviceLien = null;

        DataTable table = null;
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
            serviceCalendar = new ImplDalCalendar();
            serviceFicheBord = new ImplDalFicheBord();
            servicePlaceFB = new ImplDalPlaceFB();
            serviceAutorisationDepart = new ImplDalAutorisationDepart();
            serviceItineraire = new ImplDalItineraire();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                TextDateCalendar.Text = DateTime.Now.ToString("dd MMMM yyyy");
                ddlDebutItineraire.Items.Add(new ListItem(agent.agence.ville.NomVille, agent.agence.ville.NumVille));
                serviceItineraire.loadDdlItineraireForCalendar(ddlFinItineraire, ddlDebutItineraire.SelectedValue);

                this.initialiseCalendar();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "332"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseCalendar()
        {
            #region declaration
            crlItineraire itineraire = null;
            string axeStr = "";
            #endregion

            #region implementation
            table = serviceCalendar.getDTCalendarFBADIsNotNull(ddlFinItineraire.SelectedValue, agent.numAgence);
            Calendar.DataSource = table;
            Calendar.DataBind();

            itineraire = serviceItineraire.selectItineraire(ddlFinItineraire.SelectedValue);
            if (itineraire != null)
            {
                if (itineraire.routeNationale != null)
                {
                    for (int i = 0; i < itineraire.routeNationale.Count; i++)
                    {
                        if (i == 0)
                        {
                            axeStr = itineraire.routeNationale[i].RouteNationale;
                        }
                        else
                        {
                            axeStr = axeStr + "-" + itineraire.routeNationale[i].RouteNationale;
                        }
                    }
                }
            }

            LabAxe.Text = axeStr;
            #endregion
        }
        #endregion

        #region event
        protected void TextDateCalendar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calendar.StartDate = Convert.ToDateTime(TextDateCalendar.Text);
            }
            catch (Exception)
            {
                TextDateCalendar.Text = Calendar.StartDate.ToString("dd MMMM yyyy");
            }
            this.initialiseCalendar();
        }

        protected void Calendar_EventDoubleClick(object sender, DayPilot.Web.Ui.Events.EventClickEventArgs e)
        {
            this.initialiseCalendar();
            //Response.Redirect("Edition.aspx?numerosAD=" + e.Value);
        }

        protected void ddlNbJour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNbJour.SelectedValue != "")
            {
                Calendar.Days = int.Parse(ddlNbJour.SelectedValue);
                this.initialiseCalendar();
            }
        }

        protected void BubbleFicheBord_RenderContent(object sender, RenderEventArgs e)
        {
            #region initialisation
            crlAutorisationDepart autorisationDepart = null;
            //crlFicheBord ficheBord = null;
            string nombrePlaceLibre = "0";
            #endregion

            #region implementation
            if (e is RenderEventBubbleEventArgs)
            {
                RenderEventBubbleEventArgs re = e as RenderEventBubbleEventArgs;

                StringBuilder sb = new StringBuilder();

                autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(re.Value);
                //ficheBord = serviceFicheBord.selectFicheBord(re.Value);


                if (autorisationDepart != null)
                {
                    nombrePlaceLibre = servicePlaceFB.getNombrePlaceLibre(autorisationDepart.ficheBord.NumerosFB);

                    sb.AppendFormat("<div class='divBubble'>");

                    sb.AppendFormat("<b>Heure de départ:</b> {0}<br />", re.Start.ToString("HH:mm"));
                    sb.AppendFormat("<b>Autorisation de voyage:</b> {0}<br />", autorisationDepart.ficheBord.autorisationVoyage.NumerosAV + " du " + autorisationDepart.ficheBord.autorisationVoyage.DatePrevueDepart.ToString("dd MMMM yyyy"));
                    sb.AppendFormat("<b>Vehicule:</b> {0}<br />", autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule + " " + autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule + " " + autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule);
                    sb.AppendFormat("<b>Chauffeur:</b> {0}<br />", autorisationDepart.ficheBord.autorisationVoyage.Verification.Chauffeur.prenomChauffeur + " " + autorisationDepart.ficheBord.autorisationVoyage.Verification.Chauffeur.nomChauffeur);
                    sb.AppendFormat("<b>Itineraire:</b> {0}<br />", autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + autorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.villeF.NomVille);
                    sb.AppendFormat("<b>Poids autorisé:</b> {0}<br />", autorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.PoidsTotalVehicule + "Kg");
                    sb.AppendFormat("<b>Nombre de place libre:</b> {0}<br />", nombrePlaceLibre);

                    sb.AppendFormat("</div>");
                }

                re.InnerHTML = sb.ToString();
            }
            else if (e is RenderTimeBubbleEventArgs)
            {
                RenderTimeBubbleEventArgs re = e as RenderTimeBubbleEventArgs;
                e.InnerHTML = "<div class='divBubble'><b>Date:</b>" + re.Start.ToString("dd MMMM yyyy") + "</div>";
            }
            #endregion
        }


        #endregion


        protected void Calendar_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
        {
            #region declaration
            crlFicheBord ficheBord = null;
            #endregion

            #region implementation
            ficheBord = serviceFicheBord.selectFicheBord(e.Value);
            if (ficheBord != null)
            {
                ficheBord.DateHeurPrevue = e.NewStart;

                if (serviceFicheBord.updateFicheBord(ficheBord))
                {
                    this.initialiseCalendar();
                }
            }
            #endregion
        }
        protected void Calendar_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.BeforeEventRenderEventArgs e)
        {
            if ((string)e.DataItem["color"] == "0")
            {
                e.BorderColor = "#ff0000";
                e.BackgroundColor = "#ff9797";
                e.FontColor = "#000";
            }
            else if ((string)e.DataItem["color"] == "1")
            {
                e.BorderColor = "#0000ff";
                e.BackgroundColor = "#08b8ff";
                e.FontColor = "#000";
            }
            else if ((string)e.DataItem["color"] == "2")
            {
                e.BorderColor = "#068c14";
                e.BackgroundColor = "#08b81b";
                e.FontColor = "#000";
            }
            else
            {
                e.BorderColor = "#fab308";
                e.BackgroundColor = "#f8d98f";
                e.FontColor = "#000";
            }
        }
        protected void ddlFinItineraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseCalendar();
        }
    }
}