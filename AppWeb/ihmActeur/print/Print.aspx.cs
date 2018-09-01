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

namespace AppWeb.ihmActeur.print
{
    public partial class Print : System.Web.UI.Page
    {
        #region declaration
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAgent serviceAgent = null;
        IntfDalServicePdf servicePdf = null;

        crlAgent agent = null;

        bool isPrint = false;
        string urlSavingPdf = "";
        string urlSaving = "";
        #endregion

        #region event page
        protected void Page_Load(object sender, EventArgs e)
        {
            #region initialise ressource
            serviceRessource = new ImplDalServiceRessource();
            #endregion

            #region verification
            this.verification();
            #endregion

            #region initialisation
            serviceAgent = new ImplDalAgent();
            servicePdf = new ImplDalServicePdf();
            #endregion

            #region implementation
            string param = Request.QueryString["param"];
            string numAV = Request.QueryString["numerosAV"];
            string numAD = Request.QueryString["numerosAD"];
            string numFacture = Request.QueryString["numFacture"];
            string numProforma = Request.QueryString["numProforma"];
            string numBillet = Request.QueryString["numBillet"];
            string idCommission = Request.QueryString["idCommission"];
            string numRecuEncaisser = Request.QueryString["numRecuEncaisser"];
            string numRecuDecaisser = Request.QueryString["numRecuDecaisser"];
            string numRecuAD = Request.QueryString["numRecuAD"];
            string idVoyage = Request.QueryString["idVoyage"];
            string numBonDeCommande = Request.QueryString["numBonDeCommande"];
            string idVerification = Request.QueryString["idVerification"];
            string numBilletUS = Request.QueryString["numBilletUS"];


            if (!Page.IsPostBack)
            {
                if (param != null)
                {
                    switch (param)
                    {
                        case "AV":
                            if (numAV != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + numAV.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printAutorisationVoyage(numAV, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "AD":
                            if (numAD != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + numAD.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printFicheBord(numAD, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "facture":
                            if (numFacture != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Facture" + numFacture.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printFacture(numFacture, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "proforma":
                            if (numProforma != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Proforma" + numProforma.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printProforma(numProforma, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "billet":
                            if (numBillet != null)
                            {
                                isPrint = false;
                                string nomFicheBille = (numBillet.Split(';'))[0];
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Billet" + nomFicheBille.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printBillet(numBillet, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "commission":
                            if (idCommission != null)
                            {
                                isPrint = false;
                                string numComission = (idCommission.Split(';'))[0];
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Commission" + numComission.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printCommission(idCommission, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "recuEncaisser":
                            if (numRecuEncaisser != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Recu" + numRecuEncaisser.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printRecuEncaisser(numRecuEncaisser, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "recuDecaisser":
                            if (numRecuDecaisser != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Recu" + numRecuDecaisser.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printRecuDecaisser(numRecuDecaisser, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "recuAD":
                            if (numRecuAD != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Recu" + numRecuAD.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printRecuAD(numRecuAD, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "voyage":
                            if (idVoyage != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Ticket" + idVoyage.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printVoyage(idVoyage, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "bonCommande":
                            if (numBonDeCommande != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "BonCommande" + numBonDeCommande.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printBonCommande(numBonDeCommande, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "verification":
                            if (idVerification != null)
                            {
                                isPrint = false;
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Verification" + idVerification.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printVerification(idVerification, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;

                        case "billetUS":
                            if (numBilletUS != null)
                            {
                                isPrint = false;
                                string nomFicheBilletUS = (numBilletUS.Split(';'))[0];
                                urlSavingPdf = ConfigurationManager.AppSettings["urlSavingPdf"] + "Billet" + nomFicheBilletUS.Replace('/', '_') + ".pdf";
                                urlSaving = @Server.MapPath(urlSavingPdf);

                                isPrint = servicePdf.printUSBillet(numBilletUS, urlSaving);

                                if (isPrint)
                                    Response.Redirect(urlSavingPdf);
                            }
                            break;
                    }
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
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }
        #endregion
    }
}