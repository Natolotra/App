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

namespace AppWeb.ihmActeur.administrateur
{
    public partial class Itineraire : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalTarifCommissionPar serviceTarifCommissionPar = null;
        IntfDalTarifBaseBillet serviceTarifBaseBillet = null;
        IntfDalTarifBaseCommission serviceTarifBaseCommission = null;
        IntfDalItineraire serviceItineraire = null;
        IntfDalVille serviceVille = null;
        IntfDalRouteNationale serviceRouteNationale = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalTrajet serviceTrajet = null;
        IntfDalInfoExedantBagage serviceInfoExedantBagage = null;
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
            serviceVille = new ImplDalVille();
            serviceTarifCommissionPar = new ImplDalTarifCommissionPar();
            serviceTarifBaseBillet = new ImplDalTarifBaseBillet();
            serviceItineraire = new ImplDalItineraire();
            serviceRouteNationale = new ImplDalRouteNationale();
            serviceGeneral = new ImplDalGeneral();
            serviceTrajet = new ImplDalTrajet();
            serviceTarifBaseCommission = new ImplDalTarifBaseCommission();
            serviceInfoExedantBagage = new ImplDalInfoExedantBagage();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                hfIdItineraire.Value = "";

                this.initialiseErrorMassage();

                this.initialiseLabCommissionPar();

                this.serviceVille.loadDdlVille(ddlVilleD);
                this.serviceVille.loadDdlVille(ddlVilleF, ddlVilleD.SelectedValue);

                this.serviceVille.loadDdlVille(ddlTrajetVilleD);
                this.serviceVille.loadDdlVille(ddlTrajetVilleF, ddlTrajetVilleD.SelectedValue);

                this.initialiseGVRNItineraire();
                this.initialiseGVRN();
                this.initialiseGVTrajetItineraire();
                this.initialiseGVTrajet();
                this.initialiseGVItineraire();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "023"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseErrorMassage()
        {
            TextCommissionInterne_RequiredFieldValidator.ErrorMessage = ReItineraire.interneNonVide;
            TextCommissionParKg_RequiredFieldValidator.ErrorMessage = ReItineraire.parKgNonVide;
            TextCommissionParPiece_RequiredFieldValidator.ErrorMessage = ReItineraire.parPieceNonVide;
            TextDistance_RequiredFieldValidator.ErrorMessage = ReItineraire.distanceNonVide;
            TextDureeTrajetH_RequiredFieldValidator.ErrorMessage = ReItineraire.heureNonVide;
            TextDureeTrajetJ_RequiredFieldValidator.ErrorMessage = ReItineraire.jourNonVide;
            TextDureeTrajetM_RequiredFieldValidator.ErrorMessage = ReItineraire.minuteNonVide;
            TextNbRepos_RequiredFieldValidator.ErrorMessage = ReItineraire.nombreReposNonVide;
            TextPoidsAutorise_RequiredFieldValidator.ErrorMessage = ReItineraire.poidsAutorieNonVide;
            TextPrixExcedent_RequiredFieldValidator.ErrorMessage = ReItineraire.prixExcedentNonVide;
            TextTarifBillet_RequiredFieldValidator.ErrorMessage = ReItineraire.tarifBilletNonVide;

            TextTrajetDureeH_RequiredFieldValidator.ErrorMessage = ReTrajet.heureNonVide;
            TextTrajetDureeJ_RequiredFieldValidator.ErrorMessage = ReTrajet.jourNonVide;
            TextTrajetDureeM_RequiredFieldValidator.ErrorMessage = ReTrajet.minuteNonVide;
            TextDistanceTrajet_RequiredFieldValidator.ErrorMessage = ReTrajet.distanceNonVide;
            TextTCInterne_RequiredFieldValidator.ErrorMessage = ReTrajet.interneNonVide;
            TextTCParKg_RequiredFieldValidator.ErrorMessage = ReTrajet.parKgNonVide;
            TextTCParPiece_RequiredFieldValidator.ErrorMessage = ReTrajet.parPieceNonVide;
            TextTarifBilletTrajet_RequiredFieldValidator.ErrorMessage = ReTrajet.tarifBilletNonVide;
        }

        private void initialiseFormulaire()
        {
            TextCommissionInterne.Text = "0";
            TextCommissionParKg.Text = "";
            TextCommissionParPiece.Text = "";
            TextDistance.Text = "";
            TextDureeTrajetH.Text = "00";
            TextDureeTrajetJ.Text = "00";
            TextDureeTrajetM.Text = "00";
            TextNbRepos.Text = "";
            TextPoidsAutorise.Text = "";
            TextPrixExcedent.Text = "";
            TextTarifBillet.Text = "";
            hfIdItineraire.Value = "";



            this.initialiseGVRN();
            this.initialiseGVRNItineraire();
            this.serviceVille.loadDdlVille(ddlVilleD);
            this.serviceVille.loadDdlVille(ddlVilleF, ddlVilleD.SelectedValue);

            this.initialiseFormulaireTrajet();
        }

        private void initialiseFormulaireTrajet()
        {
            TextDistanceTrajet.Text = "";
            TextTrajetDureeJ.Text = "00";
            TextTrajetDureeH.Text = "00";
            TextTrajetDureeM.Text = "00";
            TextTCInterne.Text = "0";
            TextTCParKg.Text = "";
            TextTCParPiece.Text = "";
            TextTarifBilletTrajet.Text = "";
            hfNumTrajet.Value = "";

            this.initialiseGVTrajetItineraire();
            this.initialiseGVTrajet();
            this.serviceVille.loadDdlVille(ddlTrajetVilleD);
            this.serviceVille.loadDdlVille(ddlTrajetVilleF, ddlTrajetVilleD.SelectedValue);
        }

        private void insertToObj(crlItineraire itineraire)
        {
            #region declaration
            crlTrajet trajet = null;
            crlTarifBaseCommission tempTarifBaseCommission = null;
            crlTarifCommissionPar tempTarifCommissionPar = null;
            #endregion

            #region implementation
            if (itineraire != null)
            {
                try
                {
                    itineraire.DistanceParcour = int.Parse(TextDistance.Text.Trim());
                }
                catch (Exception)
                {
                }
                itineraire.DureeTrajet = TextDureeTrajetJ.Text + ":" + TextDureeTrajetH.Text + ":" + TextDureeTrajetM.Text + ":00";
                try
                {
                    itineraire.NombreRepos = int.Parse(TextNbRepos.Text);
                }
                catch (Exception)
                {
                }
                itineraire.NumVilleItineraireDebut = ddlVilleD.SelectedValue;
                itineraire.NumVilleItineraireFin = ddlVilleF.SelectedValue;
                if (itineraire.infoExedantBagage == null)
                {
                    itineraire.infoExedantBagage = new crlInfoExedantBagage();
                }

                try
                {
                    itineraire.infoExedantBagage.PoidAutorise = double.Parse(TextPoidsAutorise.Text);
                }
                catch (Exception)
                {
                }

                try
                {
                    itineraire.infoExedantBagage.PrixExedantBagage = TextPrixExcedent.Text;
                }
                catch (Exception)
                {
                }

                itineraire.villeD = serviceVille.selectVille(itineraire.NumVilleItineraireDebut);
                itineraire.villeF = serviceVille.selectVille(itineraire.NumVilleItineraireFin);

                trajet = new crlTrajet();

                trajet.DistanceTrajet = itineraire.DistanceParcour;
                trajet.DureeTrajet = itineraire.DureeTrajet;
                trajet.NumVilleD = itineraire.NumVilleItineraireDebut;
                trajet.NumVilleF = itineraire.NumVilleItineraireFin;

                trajet.villeD = serviceVille.selectVille(trajet.NumVilleD);
                trajet.villeF = serviceVille.selectVille(trajet.NumVilleF);

                trajet.tarifBaseBillet = new crlTarifBaseBillet();

                try
                {
                    trajet.tarifBaseBillet.MontantTarifBaseBillet = double.Parse(TextTarifBillet.Text);
                }
                catch (Exception)
                {
                }

                trajet.tarifBaseCommissions = new List<crlTarifBaseCommission>();

                tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(0);

                if (tempTarifCommissionPar != null)
                {
                    if (tempTarifCommissionPar.TypeCalcule == 0)
                    {
                        tempTarifBaseCommission = new crlTarifBaseCommission();

                        try
                        {
                            tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextCommissionInterne.Text);
                        }
                        catch (Exception)
                        {
                        }

                        tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                        tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                        trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                    }
                }

                tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(1);

                if (tempTarifCommissionPar != null)
                {
                    if (tempTarifCommissionPar.TypeCalcule == 1)
                    {
                        tempTarifBaseCommission = new crlTarifBaseCommission();

                        try
                        {
                            tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextCommissionParKg.Text);
                        }
                        catch (Exception)
                        {
                        }

                        tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                        tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                        trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                    }
                }

                tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(2);

                if (tempTarifCommissionPar != null)
                {
                    if (tempTarifCommissionPar.TypeCalcule == 2)
                    {
                        tempTarifBaseCommission = new crlTarifBaseCommission();

                        try
                        {
                            tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextCommissionParPiece.Text);
                        }
                        catch (Exception)
                        {
                        }

                        tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                        tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                        trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                    }
                }

                itineraire.trajets = new List<crlTrajet>();
                itineraire.trajets.Add(trajet);

            }
            #endregion
        }

        private void insertToObjUpdate(crlItineraire itineraire)
        {
            #region declaration
            crlTrajet trajet = null;
            crlTarifBaseCommission tempTarifBaseCommission = null;
            crlTarifCommissionPar tempTarifCommissionPar = null;
            #endregion

            #region implementation
            if (itineraire != null)
            {
                try
                {
                    itineraire.DistanceParcour = int.Parse(TextDistance.Text.Trim());
                }
                catch (Exception)
                {
                }
                itineraire.DureeTrajet = TextDureeTrajetJ.Text + ":" + TextDureeTrajetH.Text + ":" + TextDureeTrajetM.Text + ":00";
                try
                {
                    itineraire.NombreRepos = int.Parse(TextNbRepos.Text);
                }
                catch (Exception)
                {
                }
                itineraire.NumVilleItineraireDebut = ddlVilleD.SelectedValue;
                itineraire.NumVilleItineraireFin = ddlVilleF.SelectedValue;
                if (itineraire.infoExedantBagage == null)
                {
                    itineraire.infoExedantBagage = new crlInfoExedantBagage();
                }

                try
                {
                    itineraire.infoExedantBagage.PoidAutorise = double.Parse(TextPoidsAutorise.Text);
                }
                catch (Exception)
                {
                }

                try
                {
                    itineraire.infoExedantBagage.PrixExedantBagage = TextPrixExcedent.Text;
                }
                catch (Exception)
                {
                }

                itineraire.villeD = serviceVille.selectVille(itineraire.NumVilleItineraireDebut);
                itineraire.villeF = serviceVille.selectVille(itineraire.NumVilleItineraireFin);

                itineraire.trajets = null;

                trajet = serviceTrajet.selectTrajet(itineraire.NumVilleItineraireDebut, itineraire.NumVilleItineraireFin);

                if (trajet != null)
                {
                    trajet.DistanceTrajet = itineraire.DistanceParcour;
                    trajet.DureeTrajet = itineraire.DureeTrajet;
                    trajet.NumVilleD = itineraire.NumVilleItineraireDebut;
                    trajet.NumVilleF = itineraire.NumVilleItineraireFin;

                    trajet.villeD = serviceVille.selectVille(trajet.NumVilleD);
                    trajet.villeF = serviceVille.selectVille(trajet.NumVilleF);

                    if (trajet.tarifBaseBillet == null)
                    {
                        trajet.tarifBaseBillet = new crlTarifBaseBillet();
                    }

                    try
                    {
                        trajet.tarifBaseBillet.MontantTarifBaseBillet = double.Parse(TextTarifBillet.Text);
                    }
                    catch (Exception)
                    {
                    }

                    for (int i = 0; i < trajet.tarifBaseCommissions.Count; i++)
                    {
                        if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 0)
                        {
                            try
                            {
                                trajet.tarifBaseCommissions[i].MontantTarifBaseCommission = double.Parse(TextCommissionInterne.Text);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 1)
                        {
                            try
                            {
                                trajet.tarifBaseCommissions[i].MontantTarifBaseCommission = double.Parse(TextCommissionParKg.Text);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 2)
                        {
                            try
                            {
                                trajet.tarifBaseCommissions[i].MontantTarifBaseCommission = double.Parse(TextCommissionParPiece.Text);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                else
                {
                    trajet = new crlTrajet();

                    trajet.DistanceTrajet = itineraire.DistanceParcour;
                    trajet.DureeTrajet = itineraire.DureeTrajet;
                    trajet.NumVilleD = itineraire.NumVilleItineraireDebut;
                    trajet.NumVilleF = itineraire.NumVilleItineraireFin;

                    trajet.villeD = serviceVille.selectVille(trajet.NumVilleD);
                    trajet.villeF = serviceVille.selectVille(trajet.NumVilleF);

                    trajet.tarifBaseBillet = new crlTarifBaseBillet();

                    try
                    {
                        trajet.tarifBaseBillet.MontantTarifBaseBillet = double.Parse(TextTarifBillet.Text);
                    }
                    catch (Exception)
                    {
                    }

                    trajet.tarifBaseCommissions = new List<crlTarifBaseCommission>();

                    tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(0);

                    if (tempTarifCommissionPar != null)
                    {
                        if (tempTarifCommissionPar.TypeCalcule == 0)
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();

                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextCommissionInterne.Text);
                            }
                            catch (Exception)
                            {
                            }

                            tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                            tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                            trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }

                    tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(1);

                    if (tempTarifCommissionPar != null)
                    {
                        if (tempTarifCommissionPar.TypeCalcule == 1)
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();

                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextCommissionParKg.Text);
                            }
                            catch (Exception)
                            {
                            }

                            tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                            tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                            trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }

                    tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(2);

                    if (tempTarifCommissionPar != null)
                    {
                        if (tempTarifCommissionPar.TypeCalcule == 2)
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();

                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextCommissionParPiece.Text);
                            }
                            catch (Exception)
                            {
                            }

                            tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                            tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                            trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }
                }

                itineraire.trajets = new List<crlTrajet>();
                itineraire.trajets.Add(trajet);
            }
            #endregion
        }

        private void insertToObj(crlTrajet trajet)
        {
            #region declaration
            crlTarifBaseCommission tempTarifBaseCommission = null;
            crlTarifCommissionPar tempTarifCommissionPar = null;
            #endregion

            #region implementation
            if (trajet != null)
            {
                try
                {
                    trajet.DistanceTrajet = int.Parse(TextDistanceTrajet.Text);
                }
                catch (Exception)
                {
                }

                trajet.DureeTrajet = TextTrajetDureeJ.Text + ":" + TextTrajetDureeH.Text + ":" + TextTrajetDureeM.Text + ":00";
                trajet.NumVilleD = ddlTrajetVilleD.SelectedValue;
                trajet.NumVilleF = ddlTrajetVilleF.SelectedValue;

                trajet.villeD = serviceVille.selectVille(trajet.NumVilleD);
                trajet.villeF = serviceVille.selectVille(trajet.NumVilleF);

                if (trajet.tarifBaseBillet == null)
                {
                    trajet.tarifBaseBillet = new crlTarifBaseBillet();
                }

                try
                {
                    trajet.tarifBaseBillet.MontantTarifBaseBillet = double.Parse(TextTarifBilletTrajet.Text);
                }
                catch (Exception)
                {
                }

                if (trajet.tarifBaseCommissions == null)
                {
                    trajet.tarifBaseCommissions = new List<crlTarifBaseCommission>();

                    tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(0);

                    if (tempTarifCommissionPar != null)
                    {
                        if (tempTarifCommissionPar.TypeCalcule == 0)
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();

                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextTCInterne.Text);
                            }
                            catch (Exception)
                            {
                            }

                            tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                            tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                            trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }

                    tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(1);

                    if (tempTarifCommissionPar != null)
                    {
                        if (tempTarifCommissionPar.TypeCalcule == 1)
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();

                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextTCParKg.Text);
                            }
                            catch (Exception)
                            {
                            }

                            tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                            tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                            trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }

                    tempTarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(2);

                    if (tempTarifCommissionPar != null)
                    {
                        if (tempTarifCommissionPar.TypeCalcule == 2)
                        {
                            tempTarifBaseCommission = new crlTarifBaseCommission();

                            try
                            {
                                tempTarifBaseCommission.MontantTarifBaseCommission = double.Parse(TextTCParPiece.Text);
                            }
                            catch (Exception)
                            {
                            }

                            tempTarifBaseCommission.NumTarifCommissionPar = tempTarifCommissionPar.NumTarifCommissionPar;
                            tempTarifBaseCommission.tarifCommissionPar = tempTarifCommissionPar;

                            trajet.tarifBaseCommissions.Add(tempTarifBaseCommission);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < trajet.tarifBaseCommissions.Count; i++)
                    {
                        if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 0)
                        {
                            try
                            {
                                trajet.tarifBaseCommissions[i].MontantTarifBaseCommission = double.Parse(TextTCInterne.Text);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 1)
                        {
                            try
                            {
                                trajet.tarifBaseCommissions[i].MontantTarifBaseCommission = double.Parse(TextTCParKg.Text);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 2)
                        {
                            try
                            {
                                trajet.tarifBaseCommissions[i].MontantTarifBaseCommission = double.Parse(TextTCParPiece.Text);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void initialiseGVRN()
        {
            #region declaration
            List<crlRouteNationale> routeNationales = null;
            #endregion

            #region implementation
            routeNationales = serviceRouteNationale.selectRNForItineraire(hfIdItineraire.Value);

            serviceRouteNationale.insertToGridRouteNationaleNotItineraire(gvRN, ddlTriRN.SelectedValue, ddlTriRN.SelectedValue, TextRechercheRN.Text, routeNationales);
            #endregion
        }

        private void initialiseGVRNItineraire()
        {
            serviceRouteNationale.insertToGridRouteNationaleItineraire(gvRNItineraire, ddlTriRNItineraire.SelectedValue, ddlTriRNItineraire.SelectedValue, TextRechercheRNItineraire.Text, hfIdItineraire.Value);
        }

        private void initialiseGVTrajet()
        {
            #region declaration
            List<crlTrajet> trajets = null;
            #endregion

            #region implementation
            trajets = serviceTrajet.selectTrajets(hfIdItineraire.Value);

            serviceTrajet.insertToGridTrajetNotItineraire(gvTrajet, ddlTriTrajet.SelectedValue, ddlTriTrajet.SelectedValue, TextRechercheTrajet.Text, trajets);
            #endregion
        }

        private void initialiseGVTrajetItineraire()
        {
            serviceTrajet.insertToGridTrajetItineraire(gvTrajetItineraire, ddlTriTrajetItineraire.SelectedValue, ddlTriTrajetItineraire.SelectedValue, TextRechercheTrajetItineraire.Text, hfIdItineraire.Value);
        }

        private void initialiseGVItineraire()
        {
            serviceItineraire.insertToGridItineraire(gvItineraire, ddlTriItineraire.SelectedValue, ddlTriItineraire.SelectedValue, TextRechercheItineraire.Text);
        }

        private void afficheItineraire(string idItineraire)
        {
            #region declaration
            crlItineraire itineraire = null;
            crlTrajet trajet = null;
            TimeSpan duree;
            #endregion

            #region implementation
            if (idItineraire != "")
            {
                itineraire = serviceItineraire.selectItineraire(idItineraire);

                if (itineraire != null)
                {
                    hfIdItineraire.Value = itineraire.IdItineraire;

                    try
                    {
                        ddlVilleD.SelectedValue = itineraire.NumVilleItineraireDebut;
                        this.serviceVille.loadDdlVille(ddlVilleF, ddlVilleD.SelectedValue);

                        try
                        {
                            ddlVilleF.SelectedValue = itineraire.NumVilleItineraireFin;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                    }

                    TextDistance.Text = itineraire.DistanceParcour.ToString();
                    TextNbRepos.Text = itineraire.NombreRepos.ToString();
                    duree = serviceGeneral.getTimeSpan(itineraire.DureeTrajet);
                    TextDureeTrajetJ.Text = duree.Days.ToString("00");
                    TextDureeTrajetH.Text = duree.Hours.ToString("00");
                    TextDureeTrajetM.Text = duree.Minutes.ToString("00");

                    TextPoidsAutorise.Text = itineraire.infoExedantBagage.PoidAutorise.ToString();
                    TextPrixExcedent.Text = itineraire.infoExedantBagage.PrixExedantBagage;

                    trajet = serviceTrajet.selectTrajet(itineraire.NumVilleItineraireDebut, itineraire.NumVilleItineraireFin);

                    if (trajet != null)
                    {
                        TextTarifBillet.Text = trajet.tarifBaseBillet.MontantTarifBaseBillet.ToString("0");

                        if (trajet.tarifBaseCommissions != null)
                        {
                            for (int i = 0; i < trajet.tarifBaseCommissions.Count; i++)
                            {
                                if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 0)
                                {
                                    TextCommissionInterne.Text = trajet.tarifBaseCommissions[i].MontantTarifBaseCommission.ToString();
                                }
                                if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 1)
                                {
                                    TextCommissionParKg.Text = trajet.tarifBaseCommissions[i].MontantTarifBaseCommission.ToString();
                                }
                                if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 2)
                                {
                                    TextCommissionParPiece.Text = trajet.tarifBaseCommissions[i].MontantTarifBaseCommission.ToString();
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void initialiseLabCommissionPar()
        {
            #region declaration
            crlTarifCommissionPar tarifCommissionPar = null;
            #endregion

            #region implementation
            tarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(0);

            if (tarifCommissionPar != null)
            {
                LabCommentaireInterne.Text = tarifCommissionPar.CommentaireTarifCommissionPar;
                LabCommentaireTCInterne.Text = tarifCommissionPar.CommentaireTarifCommissionPar;
                tarifCommissionPar = null;
            }

            tarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(1);

            if (tarifCommissionPar != null)
            {
                LabCommentaireParKg.Text = tarifCommissionPar.CommentaireTarifCommissionPar;
                LabCommentaireTCParKg.Text = tarifCommissionPar.CommentaireTarifCommissionPar;
                tarifCommissionPar = null;
            }

            tarifCommissionPar = serviceTarifCommissionPar.selectTarifCommissionPar(2);

            if (tarifCommissionPar != null)
            {
                LabCommissionParPiece.Text = tarifCommissionPar.CommentaireTarifCommissionPar;
                LabCommentaireTCParPiece.Text = tarifCommissionPar.CommentaireTarifCommissionPar;
                tarifCommissionPar = null;
            }
            #endregion
        }

        private void afficheTrajet(string numTrajet)
        {
            #region declaration
            crlTrajet trajet = null;
            TimeSpan duree;
            #endregion

            #region implementation
            if (numTrajet != "")
            {
                trajet = serviceTrajet.selectTrajet(numTrajet);

                if (trajet != null)
                {
                    hfNumTrajet.Value = trajet.NumTrajet;

                    try
                    {
                        ddlTrajetVilleD.SelectedValue = trajet.NumVilleD;
                        this.serviceVille.loadDdlVille(ddlTrajetVilleF, ddlTrajetVilleD.SelectedValue);

                        try
                        {
                            ddlTrajetVilleF.SelectedValue = trajet.NumVilleF;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                    }

                    TextDistanceTrajet.Text = trajet.DistanceTrajet.ToString();
                    duree = serviceGeneral.getTimeSpan(trajet.DureeTrajet);
                    TextTrajetDureeJ.Text = duree.Days.ToString("00");
                    TextTrajetDureeH.Text = duree.Hours.ToString("00");
                    TextTrajetDureeM.Text = duree.Minutes.ToString("00");


                    TextTarifBilletTrajet.Text = trajet.tarifBaseBillet.MontantTarifBaseBillet.ToString("0");

                    if (trajet.tarifBaseCommissions != null)
                    {
                        for (int i = 0; i < trajet.tarifBaseCommissions.Count; i++)
                        {
                            if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 0)
                            {
                                TextTCInterne.Text = trajet.tarifBaseCommissions[i].MontantTarifBaseCommission.ToString();
                            }
                            if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 1)
                            {
                                TextTCParKg.Text = trajet.tarifBaseCommissions[i].MontantTarifBaseCommission.ToString();
                            }
                            if (trajet.tarifBaseCommissions[i].tarifCommissionPar.TypeCalcule == 2)
                            {
                                TextTCParPiece.Text = trajet.tarifBaseCommissions[i].MontantTarifBaseCommission.ToString();
                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region event
        protected void ddlVilleD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.serviceVille.loadDdlVille(ddlVilleF, ddlVilleD.SelectedValue);
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlItineraire itineraire = null;
            #endregion

            #region implementation
            itineraire = new crlItineraire();
            this.insertToObj(itineraire);

            itineraire.IdItineraire = serviceItineraire.isItineraireStr(itineraire);

            if (itineraire.IdItineraire != "")
            {
                //
                itineraire = serviceItineraire.selectItineraire(itineraire.IdItineraire);

            }
            else
            {
                itineraire.IdItineraire = serviceItineraire.insertItineraireAll(itineraire, agent.agence.SigleAgence);

                if (itineraire.IdItineraire != "")
                {
                    this.initialiseGVItineraire();
                    this.initialiseGVTrajet();
                    this.initialiseGVTrajetItineraire();
                    hfIdItineraire.Value = itineraire.IdItineraire;
                }
                else
                {
                    //
                }
            }

            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlItineraire itineraire = null;
            string numTrajet = "";
            string numInfoBagage = "";
            #endregion

            #region implementation
            if (hfIdItineraire.Value != "")
            {
                itineraire = serviceItineraire.selectItineraire(hfIdItineraire.Value);

                if (itineraire != null)
                {
                    this.insertToObjUpdate(itineraire);

                    #region update Trajet
                    if (itineraire.trajets[0].NumTrajet != "")
                    {
                        numTrajet = serviceTrajet.isTrajet(itineraire.trajets[0]);

                        if (numTrajet.Equals(""))
                        {
                            if (itineraire.trajets[0].tarifBaseBillet != null)
                            {
                                serviceTarifBaseBillet.updateTarifBaseBillet(itineraire.trajets[0].tarifBaseBillet);
                            }
                            if (itineraire.trajets[0].tarifBaseCommissions != null)
                            {
                                for (int i = 0; i < itineraire.trajets[0].tarifBaseCommissions.Count; i++)
                                {
                                    serviceTarifBaseCommission.updateTarifBaseCommission(itineraire.trajets[0].tarifBaseCommissions[i]);
                                }
                            }

                            if (serviceTrajet.updateTrajet(itineraire.trajets[0]))
                            {
                            }
                        }
                        else
                        {
                            //
                        }

                    }
                    else
                    {
                        numTrajet = serviceTrajet.isTrajet(itineraire.trajets[0]);
                        if (numTrajet.Equals(""))
                        {
                            numTrajet = serviceTrajet.insertTrajetAll(itineraire.trajets[0], agent.agence.SigleAgence);

                            if (numTrajet != "")
                            {
                                serviceItineraire.insertAssociationTrajetItineraire(numTrajet, itineraire.IdItineraire);
                            }
                        }
                        else
                        {
                            //
                        }
                    }

                    #endregion

                    #region update info bagage
                    if (itineraire.infoExedantBagage.NumInfoBagage != "")
                    {
                        if (serviceInfoExedantBagage.updateInfoExedantBagage(itineraire.infoExedantBagage))
                        {
                            numInfoBagage = itineraire.infoExedantBagage.NumInfoBagage;
                        }
                    }
                    else
                    {
                        numInfoBagage = serviceInfoExedantBagage.insertInfoExedantBagage(itineraire.infoExedantBagage, agent.agence.SigleAgence);
                    }
                    #endregion

                    #region update itineraire
                    if (numInfoBagage != "")
                    {
                        itineraire.NumInfoBagage = numInfoBagage;

                        if (serviceItineraire.updateItineraire(itineraire))
                        {
                            this.afficheItineraire(itineraire.IdItineraire);
                            this.initialiseGVItineraire();
                        }
                        else
                        {
                            //
                        }
                    }
                    #endregion
                }
            }
            #endregion
        }
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }

        protected void ddlTriRNItineraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGVRNItineraire();
        }
        protected void btnRechercheRNItineraire_Click(object sender, EventArgs e)
        {
            this.initialiseGVRNItineraire();
        }
        protected void ddlTriRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGVRN();
        }
        protected void btnRechercheRN_Click(object sender, EventArgs e)
        {
            this.initialiseGVRN();
        }

        protected void gvItineraire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItineraire.PageIndex = e.NewPageIndex;
            this.initialiseGVItineraire();
        }
        protected void gvItineraire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                this.afficheItineraire(e.CommandArgument.ToString());
                this.initialiseGVRN();
                this.initialiseGVRNItineraire();
                this.initialiseGVTrajetItineraire();
                this.initialiseGVTrajet();
            }
        }

        protected void gvRNItineraire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRNItineraire.PageIndex = e.NewPageIndex;
            this.initialiseGVRNItineraire();
        }
        protected void gvRNItineraire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("deleteV"))
            {
                if (hfIdItineraire.Value != "")
                {
                    serviceItineraire.deleteAssoItineraireRouteNationale(hfIdItineraire.Value, e.CommandArgument.ToString());
                    this.initialiseGVRN();
                    this.initialiseGVRNItineraire();
                    this.initialiseGVItineraire();
                }
            }
        }

        protected void gvRN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRN.PageIndex = e.NewPageIndex;
            this.initialiseGVRN();
        }
        protected void gvRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                if (hfIdItineraire.Value != "")
                {
                    serviceItineraire.insertAssoItineraireRouteNationale(hfIdItineraire.Value, e.CommandArgument.ToString());
                    this.initialiseGVRN();
                    this.initialiseGVRNItineraire();
                    this.initialiseGVItineraire();
                }
            }
        }

        protected void ddlTrajetVilleD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.serviceVille.loadDdlVille(ddlTrajetVilleF, ddlTrajetVilleD.SelectedValue);
        }

        protected void btnValiderTrajet_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTrajet trajet = null;
            #endregion

            #region implementation
            trajet = new crlTrajet();

            this.insertToObj(trajet);

            trajet.NumTrajet = serviceTrajet.isTrajet(trajet);

            if (trajet.NumTrajet != "")
            {
                //
            }
            else
            {
                trajet.NumTrajet = serviceTrajet.insertTrajetAll(trajet, agent.agence.SigleAgence);
                if (trajet.NumTrajet != "")
                {
                    if (hfIdItineraire.Value != "")
                    {
                        serviceItineraire.insertAssociationTrajetItineraire(trajet.NumTrajet, hfIdItineraire.Value);
                    }
                    this.initialiseGVTrajet();
                    this.initialiseGVTrajetItineraire();
                }
                else
                {
                    //
                }
            }
            #endregion
        }
        protected void btnModifierTrajet_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTrajet trajet = null;
            string numTrajet = "";
            #endregion

            #region implementation
            if (hfNumTrajet.Value != "")
            {
                trajet = serviceTrajet.selectTrajet(hfNumTrajet.Value);
                if (trajet != null)
                {
                    this.insertToObj(trajet);

                    numTrajet = serviceTrajet.isTrajet(trajet);

                    if (numTrajet.Equals(""))
                    {
                        if (trajet.tarifBaseBillet != null)
                        {
                            serviceTarifBaseBillet.updateTarifBaseBillet(trajet.tarifBaseBillet);
                        }
                        if (trajet.tarifBaseCommissions != null)
                        {
                            for (int i = 0; i < trajet.tarifBaseCommissions.Count; i++)
                            {
                                serviceTarifBaseCommission.updateTarifBaseCommission(trajet.tarifBaseCommissions[i]);
                            }
                        }

                        if (serviceTrajet.updateTrajet(trajet))
                        {
                            this.initialiseGVTrajet();
                            this.initialiseGVTrajetItineraire();
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
            #endregion
        }
        protected void btnAnnulerTrajet_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireTrajet();
        }
        protected void ddlTriTrajet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGVTrajet();
        }
        protected void btnRechercheTrajet_Click(object sender, EventArgs e)
        {
            this.initialiseGVTrajet();
        }
        protected void ddlTriTrajetItineraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGVTrajetItineraire();
        }
        protected void btnRechercheTrajetItineraire_Click(object sender, EventArgs e)
        {
            this.initialiseGVTrajetItineraire();
        }
        protected void gvTrajetItineraire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTrajetItineraire.PageIndex = e.NewPageIndex;
            this.initialiseGVTrajetItineraire();
        }
        protected void gvTrajetItineraire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("deleteV"))
            {
                if (hfIdItineraire.Value != "")
                {
                    serviceItineraire.deleteAssociationTrajetItineraire(e.CommandArgument.ToString(), hfIdItineraire.Value);
                    this.initialiseGVTrajet();
                    this.initialiseGVTrajetItineraire();
                }
            }
            else if (e.CommandName.Trim().Equals("select"))
            {
                this.afficheTrajet(e.CommandArgument.ToString());
            }
        }

        protected void gvTrajet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTrajet.PageIndex = e.NewPageIndex;
            this.initialiseGVTrajet();
        }
        protected void gvTrajet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                if (hfIdItineraire.Value != "")
                {
                    serviceItineraire.insertAssociationTrajetItineraire(e.CommandArgument.ToString(), hfIdItineraire.Value);
                    this.initialiseGVTrajet();
                    this.initialiseGVTrajetItineraire();
                }
            }
        }
        #endregion
    }
}