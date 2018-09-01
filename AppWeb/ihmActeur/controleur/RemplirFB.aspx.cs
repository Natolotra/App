using AjaxControlToolkit;
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
    public partial class RemplirFB : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;

        IntfDalCommission serviceCommission = null;
        IntfDalReceptionnaire servicePersonne = null;
        IntfDalRecuEncaisser serviceRecu = null;
        IntfDalIndividu serviceIndividu = null;
        IntfDalTypeCommission serviceTypeCommission = null;
        IntfDalBillet serviceBillet = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalPlaceFB servicePlaceFB = null;
        IntfDalVoyage serviceVoyage = null;
        IntfDalBagage serviceBagage = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalItineraire serviceItineraire = null;
        IntfDalLien serviceLien = null;

        crlAgent agent = null;
        string numerosFB = null;
        string idVoyage = null;
        List<crlPlaceFB> places = null;

        crlFicheBord ficheDebord;

        List<CheckBox> checks;
        List<ToggleButtonExtender> extends;
        CheckBox checkChauffeur;
        ToggleButtonExtender extendChauffeur;

        Table tableVoiture;
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
            serviceCommission = new ImplDalCommission();
            servicePersonne = new ImplDalReceptionnaire();
            serviceRecu = new ImplDalRecuEncaisser();
            serviceIndividu = new ImplDalIndividu();
            serviceTypeCommission = new ImplDalTypeCommission();
            serviceBillet = new ImplDalBillet();
            serviceFicheBord = new ImplDalFicheBord();
            servicePlaceFB = new ImplDalPlaceFB();
            serviceVoyage = new ImplDalVoyage();
            serviceBagage = new ImplDalBagage();
            serviceGeneral = new ImplDalGeneral();
            serviceItineraire = new ImplDalItineraire();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                hfIdVoyage.Value = "";
                numerosFB = Request.QueryString["numerosFB"];
                idVoyage = Request.QueryString["idVoyage"];

                if (idVoyage != null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "pdf",
                                string.Format("window.open('{0}','_blank','width={1},height={2},top={3},left={4}');", "../print/print.aspx?param=voyage&idVoyage=" + idVoyage, 700,
                                500, 10, 10), true);
                }

                if (numerosFB != null)
                {
                    LabNumerosFB.Text = numerosFB;
                    TextNumBillet.Text = "BI" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";
                    TextNumRecu.Text = "RE" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";
                    TextNumRecuModif.Text = "RE" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";

                    this.afficheAutorisationVoyage();
                    this.initialiseGridPassager();
                    this.initialiseGridCommission();
                    /*
                    this.initialiseGridBillet();
                    this.initialisePlaceFBLibre();
                    */
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            #endregion

            this.initialiseSiege();

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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "350"))
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
                divIndication.Style.Add("font-size", "14px");
                divIndication.Style.Add("color", color);
                divIndication.InnerText = str;
            }
            else
            {
                divIndication.InnerText = "";
            }
        }

        private void afficheAutorisationVoyage()
        {
            #region declaration
            List<string> strNomVille = null;
            string strListe = "";
            #endregion

            if (LabNumerosFB.Text != "")
            {
                ficheDebord = serviceFicheBord.selectFicheBord(LabNumerosFB.Text);

                if (ficheDebord != null)
                {
                    labDateAV.Text = ficheDebord.autorisationVoyage.Verification.DateVerification.ToString("dd MMMM yyyy");
                    labDateHeureDepart.Text = ficheDebord.DateHeurPrevue.ToString("dd MMMM yyyy") + " à " + ficheDebord.DateHeurPrevue.ToString("HH:mm");
                    labItineraire.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + ficheDebord.autorisationVoyage.Verification.Itineraire.villeF.NomVille;
                    labMarqueVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule;
                    labMatriculeVoiture.Text = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;
                    labNomChauffeur.Text = ficheDebord.autorisationVoyage.Verification.Chauffeur.nomChauffeur;
                    labNumAV.Text = ficheDebord.autorisationVoyage.NumerosAV;
                    labNumFB.Text = ficheDebord.NumerosFB;
                    labPrenomChauffeur.Text = ficheDebord.autorisationVoyage.Verification.Chauffeur.prenomChauffeur;
                    hfIdItineraire.Value = ficheDebord.autorisationVoyage.Verification.Itineraire.IdItineraire;

                    labDistance.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.DistanceParcour + "Km";
                    labDureeTrajet.Text = serviceGeneral.getTextTimeSpan(ficheDebord.autorisationVoyage.Verification.Itineraire.DureeTrajet);

                    LabDoitrBagage.Text = ficheDebord.autorisationVoyage.Verification.Itineraire.infoExedantBagage.PoidAutorise.ToString("0");
                    LabDroitBagModif.Text = LabDoitrBagage.Text;

                    strNomVille = serviceItineraire.getNonVille(agent.agence.NumVille, ficheDebord.autorisationVoyage.Verification.Itineraire.IdItineraire);

                    if (strNomVille != null)
                    {
                        for (int i = 0; i < strNomVille.Count; i++)
                        {
                            if (i == 0)
                            {
                                strListe += strNomVille[i];
                            }
                            else
                            {
                                strListe += " - " + strNomVille[i];
                            }

                        }
                    }

                    listeTrajet.Text = strListe;
                }
            }
        }

        private void initialiseSiege()
        {
            #region siege
            if (LabNumerosFB.Text != "")
            {
                places = servicePlaceFB.selectPlaceFB(LabNumerosFB.Text);
                ficheDebord = serviceFicheBord.selectFicheBord(LabNumerosFB.Text);

                if (places != null)
                {
                    int colone = ficheDebord.autorisationVoyage.Verification.Licence.vehicule.NombreColoneVehicule;
                    int nbPlace = places.Count;

                    #region initialise check

                    checks = new List<CheckBox>();
                    extends = new List<ToggleButtonExtender>();
                    checkChauffeur = new CheckBox();
                    extendChauffeur = new ToggleButtonExtender();
                    checkChauffeur.ID = "ckChauffeur";
                    checkChauffeur.Enabled = false;
                    extendChauffeur.ID = "exChauffeur";
                    extendChauffeur.TargetControlID = "ckChauffeur";
                    extendChauffeur.ImageHeight = 32;
                    extendChauffeur.ImageWidth = 32;
                    extendChauffeur.UncheckedImageUrl = "~/CssStyle/images/bleu.png";
                    extendChauffeur.CheckedImageUrl = "~/CssStyle/images/bleu.png";

                    for (int i = 0; i < nbPlace; i++)
                    {
                        checks.Add(new CheckBox());
                    }
                    for (int i = 0; i < nbPlace; i++)
                    {
                        checks[i].ID = "ck" + i;
                        if (places[i].IsOccuper == 1)
                        {
                            checks[i].Enabled = false;
                            checks[i].Checked = true;
                        }
                    }
                    for (int i = 0; i < nbPlace; i++)
                    {
                        extends.Add(new ToggleButtonExtender());
                    }
                    for (int i = 0; i < nbPlace; i++)
                    {
                        if (places[i].IsOccuper == 1)
                        {
                            extends[i].ID = "ex" + i;
                            extends[i].TargetControlID = "ck" + i;
                            extends[i].ImageHeight = 32;
                            extends[i].ImageWidth = 32;
                            extends[i].UncheckedImageUrl = "~/CssStyle/images/vert.png";
                            extends[i].CheckedImageUrl = "~/CssStyle/images/rouge.png";
                        }
                        else
                        {
                            extends[i].ID = "ex" + i;
                            extends[i].TargetControlID = "ck" + i;
                            extends[i].ImageHeight = 32;
                            extends[i].ImageWidth = 32;
                            extends[i].UncheckedImageUrl = "~/CssStyle/images/vert.png";
                            extends[i].CheckedImageUrl = "~/CssStyle/images/orange.png";
                        }


                    }
                    #endregion

                    #region initialisetableau
                    List<TableCell> cells = new List<TableCell>();
                    List<TableRow> rows = new List<TableRow>();
                    TableCell cellChauffeur = new TableCell();
                    /*Image imageChauffeur = new Image();
                    imageChauffeur.ImageUrl = "~/CssStyle/images/bleu.png";*/
                    cellChauffeur.Controls.Add(checkChauffeur);
                    cellChauffeur.Controls.Add(extendChauffeur);
                    cellChauffeur.ColumnSpan = colone - 2;

                    Label LabHead = new Label();
                    LabHead.Text = " " + ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule + " <br/>" + ficheDebord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule + " <br/>" + ficheDebord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule + " ";
                    LabHead.CssClass = "voitureText";

                    TableCell cellHead = new TableCell();
                    cellHead.ColumnSpan = colone;
                    cellHead.Height = 25;
                    cellHead.CssClass = "voitureHead";
                    cellHead.Controls.Add(LabHead);
                    TableRow rowHead = new TableRow();
                    rowHead.Cells.Add(cellHead);
                    TableCell cellFoot = new TableCell();
                    cellFoot.ColumnSpan = colone;
                    cellFoot.Height = 10;
                    cellFoot.CssClass = "voitureFoot";

                    #region foot
                    TableCell cellFootItem11 = new TableCell();
                    cellFootItem11.ColumnSpan = colone - 1;
                    cellFootItem11.CssClass = "voitureFootItem";
                    TableCell cellFootItem12 = new TableCell();
                    cellFootItem12.CssClass = "voitureFootItem";
                    TableCell cellFootItem22 = new TableCell();
                    cellFootItem22.ColumnSpan = colone;
                    cellFootItem22.CssClass = "voitureFootItem";

                    Image imgIndicateur = new Image();

                    int nbPlaceOccuper = serviceFicheBord.getNombreTotalPassager(ficheDebord.NumerosFB);
                    double poidBagage = serviceFicheBord.getPoidTotalBagage(ficheDebord.NumerosFB) + serviceFicheBord.getPoidTotalCommission(ficheDebord.NumerosFB);

                    if (ficheDebord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.NbPassagerMin > nbPlaceOccuper)
                    {
                        imgIndicateur.ImageUrl = "~/CssStyle/images/rouge.png";
                    }
                    else if (nbPlaceOccuper == ficheDebord.autorisationVoyage.Verification.Licence.NombrePlacePayante)
                    {
                        imgIndicateur.ImageUrl = "~/CssStyle/images/vert.png";
                    }
                    else
                    {
                        imgIndicateur.ImageUrl = "~/CssStyle/images/bleu.png";
                    }

                    Label labelIndicateur = new Label();
                    labelIndicateur.Text = "Indicateur:";
                    Label labelBagVar = new Label();
                    labelBagVar.Text = "<br/>Bag:" + poidBagage.ToString("0") + "Kg/" + ficheDebord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.PoidBagageMax.ToString("0") + "Kg";

                    cellFootItem11.Controls.Add(labelIndicateur);
                    cellFootItem12.Controls.Add(imgIndicateur);

                    cellFootItem22.Controls.Add(labelBagVar);

                    TableRow rowFootItem1 = new TableRow();
                    rowFootItem1.Cells.Add(cellFootItem11);
                    rowFootItem1.Cells.Add(cellFootItem12);
                    TableRow rowFootItem2 = new TableRow();
                    rowFootItem2.Cells.Add(cellFootItem22);
                    #endregion

                    TableRow rowFoot = new TableRow();
                    rowFoot.Cells.Add(cellFoot);

                    for (int i = 0; i < nbPlace; i++)
                    {
                        cells.Add(new TableCell());
                    }
                    for (int i = 0; i < nbPlace; i++)
                    {
                        cells[i].Controls.Add(checks[i]);
                        cells[i].Controls.Add(extends[i]);
                        cells[i].Width = 35;
                        cells[i].Height = 35;
                    }

                    rows = new List<TableRow>();
                    rows.Add(new TableRow());
                    rows[0].Cells.Add(cellChauffeur);
                    if (cells.Count >= 2)
                    {
                        rows[0].Cells.Add(cells[0]);
                        rows[0].Cells.Add(cells[1]);
                    }

                    int iColone = 1;
                    int iPlace = 2;
                    int iRow = 1;

                    while (iPlace < nbPlace)
                    {
                        if (iColone == 1)
                        {
                            rows.Add(new TableRow());
                            rows[iRow].Cells.Add(cells[iPlace]);

                            iPlace = iPlace + 1;
                            iColone = iColone + 1;
                        }
                        else
                        {
                            rows[iRow].Cells.Add(cells[iPlace]);

                            iPlace = iPlace + 1;
                            iColone = iColone + 1;
                        }

                        if (iColone > colone)
                        {
                            iRow = iRow + 1;
                            iColone = 1;
                        }


                    }

                    tableVoiture = new Table();
                    tableVoiture.CellSpacing = 0;

                    tableVoiture.Rows.Add(rowHead);
                    for (int i = 0; i < iRow; i++)
                    {
                        rows[i].CssClass = "trVoiture";
                        tableVoiture.Rows.Add(rows[i]);
                    }
                    tableVoiture.Rows.Add(rowFootItem2);
                    tableVoiture.Rows.Add(rowFootItem1);
                    tableVoiture.Rows.Add(rowFoot);

                    //ableVoiture.CssClass = "tableImgVoiture"; 

                    siegeVoiture.Controls.Add(tableVoiture);
                    #endregion

                }
            }
            #endregion
        }

        #region passager
        private void afficheBillet(string numBillet)
        {
            #region declaration
            crlBillet billet = null;
            #endregion

            #region implementation
            if (numBillet != "")
            {

                billet = serviceBillet.isValide(numBillet, ficheDebord.autorisationVoyage.Verification.Itineraire.IdItineraire);

                if (billet != null)
                {
                    if (billet.individu != null)
                    {
                        TextNomClient.Text = billet.individu.NomIndividu;
                        TextPrenomClient.Text = billet.individu.PrenomIndividu;
                        TextCinClient.Text = billet.individu.CinIndividu;
                        /*if (billet.client.CinClient == "")
                        {
                            ddlPassager.SelectedValue = "Mineur";
                        }*/
                        TextAdresseClient.Text = billet.individu.Adresse;
                        TextFixeClient.Text = billet.individu.TelephoneFixeIndividu;
                        TextPortableClient.Text = billet.individu.TelephoneMobileIndividu;
                    }

                    if (billet.calculCategorieBillet != null)
                    {
                        TextBilletPour.Text = billet.calculCategorieBillet.IndicateurPrixBillet;
                    }

                    if (billet.trajet != null)
                    {

                        if (billet.trajet.NumVilleD != agent.agence.NumVille)
                        {
                            TextDestination.Text = billet.trajet.villeD.NomVille;
                        }
                        else
                        {
                            TextDestination.Text = billet.trajet.villeF.NomVille;
                        }
                        this.divIndicationText("", "");
                    }
                    else
                    {
                        this.divIndicationText(ReRemplirFB.erreuSurAffiche, "red");
                    }
                }
                else
                {
                    this.initialiseFormulairePassager();
                    this.divIndicationText(ReRemplirFB.billetNonValide, "red");
                    //billet non valide
                }
            }
            #endregion
        }

        private void initialiseFormulairePassager()
        {
            TextDestination.Text = "";
            TextNomClient.Text = "";
            TextPrenomClient.Text = "";
            TextCinClient.Text = "";
            TextAdresseClient.Text = "";
            TextFixeClient.Text = "";
            TextPortableClient.Text = "";

            TextBilletPour.Text = "";

            TextPoidBagage.Text = "";
            TextNumRecu.Text = "RE" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";
            TextExedentPoid.Text = "";
            TextPrixEcedent.Text = "";
        }

        private void initialiseFormulairePassagerAll()
        {
            TextNumBillet.Text = "BI" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";
            TextDestination.Text = "";
            TextNomClient.Text = "";
            TextPrenomClient.Text = "";
            TextCinClient.Text = "";
            TextAdresseClient.Text = "";
            TextFixeClient.Text = "";
            TextPortableClient.Text = "";

            TextBilletPour.Text = "";

            TextPoidBagage.Text = "";
            TextNumRecu.Text = "RE" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";
            TextExedentPoid.Text = "";
            TextPrixEcedent.Text = "";

            divIndication.InnerText = "";
        }

        private bool testFormulaire()
        {
            #region declaration
            bool isTest = false;
            #endregion

            #region implementation
            if (TextNumBillet.Text != "")
            {
                if (TextDestination.Text != "")
                {
                    if (TextBilletPour.Text != "")
                    {
                        if (TextNomClient.Text != "")
                        {
                            isTest = true;
                        }
                        else
                        {
                            this.divIndicationText(ReRemplirFB.nomClientNonVide, "red");
                        }
                    }
                    else
                    {
                        this.divIndicationText(ReRemplirFB.billetPourNonVide, "red");
                    }
                }
                else
                {
                    this.divIndicationText(ReRemplirFB.destinationNonVide, "red");
                }
            }
            else
            {
                this.divIndicationText(ReRemplirFB.numBilletNonVide, "red");
            }
            #endregion

            return isTest;
        }

        private void initialiseErrorMessage()
        {
            TextNumBillet_RequiredFieldValidator.ErrorMessage = ReRemplirFB.numBilletNonVide;
            TextDestination_RequiredFieldValidator.ErrorMessage = ReRemplirFB.destinationNonVide;
            TextBilletPour_RequiredFieldValidator.ErrorMessage = ReRemplirFB.billetPourNonVide;
            TextNomClient_RequiredFieldValidator.ErrorMessage = ReRemplirFB.nomClientNonVide;
            TextPoidBagage_RequiredFieldValidator.ErrorMessage = ReRemplirFB.poidNonVide;

            TextNomModif_RequiredFieldValidator.ErrorMessage = ReRemplirFB.nomClientNonVide;
            TextPoidModif_RequiredFieldValidator.ErrorMessage = ReRemplirFB.poidNonVide;
        }

        private bool testFormulaireModif()
        {
            #region declaration
            bool isTest = false;
            #endregion

            #region implementation

            if (TextNomModif.Text != "")
            {
                isTest = true;
            }
            else
            {
                this.divIndicationText(ReRemplirFB.nomClientNonVide, "red");
            }

            #endregion

            return isTest;
        }

        private bool testRecu()
        {
            #region declaration
            bool isTest = false;
            crlRecuEncaisser recu = null;


            double montant = 0.00;
            double prix = 0.00;
            #endregion

            #region implementation
            if (TextExedentPoid.Text != "" && TextPrixEcedent.Text != "")
            {
                if (TextNumRecu.Text != "")
                {
                    recu = serviceRecu.isValideRecu(TextNumRecu.Text);

                    if (recu != null)
                    {
                        try
                        {
                            montant = recu.MontantRecuEncaisser;
                            prix = double.Parse(TextPrixEcedent.Text.Trim());
                        }
                        catch (Exception)
                        {
                        }

                        if (montant >= prix)
                        {
                            isTest = true;
                        }
                        else
                        {
                            this.divIndicationText(ReRemplirFB.recuNonValide, "red");
                        }
                    }
                    else
                    {
                        this.divIndicationText(ReRemplirFB.recuNonValide, "red");
                    }
                }
                else
                {
                    this.divIndicationText(ReRemplirFB.numRecuNonVide, "red");
                }
            }
            else
            {
                isTest = true;
            }
            #endregion

            return isTest;
        }

        private bool testRecuModif()
        {
            #region declaration
            bool isTest = false;
            crlRecuEncaisser recu = null;


            double montant = 0.00;
            double prix = 0.00;
            #endregion

            #region implementation
            if (TextExcedentModif.Text != "" && TextPrixExcedentModif.Text != "")
            {
                if (TextNumRecuModif.Text != "")
                {
                    recu = serviceRecu.isValideRecu(TextNumRecuModif.Text);

                    if (recu != null)
                    {
                        try
                        {
                            montant = recu.MontantRecuEncaisser;
                            prix = double.Parse(TextPrixExcedentModif.Text.Trim());
                        }
                        catch (Exception)
                        {
                        }

                        if (montant >= prix)
                        {
                            isTest = true;
                        }
                        else
                        {
                            this.divIndicationText(ReRemplirFB.recuNonValide, "red");
                        }
                    }
                    else
                    {
                        this.divIndicationText(ReRemplirFB.recuNonValide, "red");
                    }
                }
                else
                {
                    this.divIndicationText(ReRemplirFB.numRecuNonVide, "red");
                }
            }
            else
            {
                isTest = true;
            }
            #endregion

            return isTest;
        }

        private string testPlace()
        {
            #region decalration
            string isTest = "";
            int nbChecked = 0;
            #endregion

            #region implementation
            for (int i = 0; i < checks.Count; i++)
            {
                if (checks[i].Enabled)
                {
                    if (checks[i].Checked)
                    {
                        nbChecked = nbChecked + 1;
                        isTest = places[i].NumPlace;
                    }
                }
            }

            if (nbChecked != 1)
            {
                isTest = "";
                this.divIndicationText(ReRemplirFB.placeNonVide, "red");
            }
            #endregion

            return isTest;
        }

        private string testPlaceModif()
        {
            #region decalration
            string isTest = "";
            int nbChecked = 0;
            #endregion

            #region implementation
            for (int i = 0; i < checks.Count; i++)
            {
                if (checks[i].Enabled)
                {
                    if (checks[i].Checked)
                    {
                        nbChecked = nbChecked + 1;
                        isTest = places[i].NumPlace;
                    }
                }
            }

            if (nbChecked == 0)
            {
                isTest = "";
            }
            if (nbChecked > 1)
            {
                isTest = "";
                this.divIndicationText(ReRemplirFB.placeNonVide, "red");
            }
            #endregion

            return isTest;
        }

        private void insertObjetVoyage(crlVoyage voyage)
        {
            if (voyage != null)
            {
                voyage.Destination = TextDestination.Text;
                voyage.NumBillet = TextNumBillet.Text;
                voyage.NumerosFB = LabNumerosFB.Text;
                voyage.NumPlace = this.testPlace();
                if (TextCinClient.Text.Trim() != "")
                {
                    voyage.PieceIdentite = TextCinClient.Text.Trim();
                }
                else
                {
                    voyage.PieceIdentite = TextBilletPour.Text;
                }

                try
                {
                    voyage.PoidBagage = double.Parse(TextPoidBagage.Text);
                }
                catch (Exception)
                {
                }

                voyage.individu = new crlIndividu();

                voyage.individu.NomIndividu = TextNomClient.Text;
                voyage.individu.PrenomIndividu = TextPrenomClient.Text;
                voyage.individu.CinIndividu = TextCinClient.Text;
                voyage.individu.Adresse = TextAdresseClient.Text;
                voyage.individu.TelephoneFixeIndividu = TextFixeClient.Text;
                voyage.individu.TelephoneMobileIndividu = TextPortableClient.Text;

                voyage.placeFB = new crlPlaceFB();

                voyage.placeFB.NumPlace = voyage.NumPlace;
                voyage.placeFB.NumerosFB = LabNumerosFB.Text;
                voyage.placeFB.IsOccuper = 1;

            }
        }

        private void initialiseGridPassager()
        {
            this.serviceVoyage.insertToGridVoyageFB(gvPassager, ddlTriRecherche.SelectedValue, ddlTriRecherche.SelectedValue, TextRecherche.Text, LabNumerosFB.Text);
        }

        private void afficheVoyage(string idVoyage)
        {
            #region declaration
            crlVoyage voyage = null;
            #endregion

            #region implementation
            if (idVoyage != "")
            {
                voyage = serviceVoyage.selectVoyage(idVoyage);
                if (voyage != null)
                {
                    if (voyage.individu != null)
                    {
                        TextNomModif.Text = voyage.individu.NomIndividu;
                        TextPrenomModif.Text = voyage.individu.PrenomIndividu;
                        TextCinModif.Text = voyage.individu.CinIndividu;
                        TextAdresseModif.Text = voyage.individu.Adresse;
                        TextTelephoneModif.Text = voyage.individu.TelephoneFixeIndividu;
                        TextPortebleModif.Text = voyage.individu.TelephoneMobileIndividu;

                        TextNumSiegeModif.Text = voyage.NumPlace;
                    }
                    TextPoidModif.Text = voyage.PoidBagage.ToString("0.00");

                    if (voyage.bagage != null)
                    {
                        TextNumRecuModif.Text = voyage.bagage.NumRecu;
                        TextExcedentModif.Text = voyage.bagage.ExcedentPoid.ToString("0");
                        TextPrixExcedentModif.Text = voyage.bagage.recu.MontantRecuEncaisser.ToString("0");
                        //TextExcedentModif.Text
                    }
                    else
                    {
                        TextNumRecuModif.Text = "RE" + DateTime.Now.ToString("yyMM") + "/" + agent.agence.SigleAgence + "/";
                        TextExcedentModif.Text = "";
                        TextPrixExcedentModif.Text = "";
                    }

                }
            }
            #endregion
        }
        #endregion

        #region commission
        private void initialiseGridCommission()
        {
            serviceCommission.insertToGridCommissionFB(gvCom, ddlTriCom.SelectedValue, ddlTriCom.SelectedValue, TextRechercheCom.Text, labNumFB.Text);
            serviceCommission.insertToGridCommissionNonFB(gvCommission, ddlTriRechercheCommission.SelectedValue, ddlTriRechercheCommission.SelectedValue, TextRechercheCommission.Text, hfIdItineraire.Value, agent.numAgence);
        }


        private bool testFormulaireCommission()
        {
            bool isTest = false;

            #region implementation

            #endregion

            return isTest;
        }


        #endregion

        #endregion

        #region even


        #region passager
        protected void TextNumBillet_TextChanged(object sender, EventArgs e)
        {
            if (TextNumBillet.Text != "")
            {
                this.afficheBillet(TextNumBillet.Text);
            }
        }

        protected void TextPoidBagage_TextChanged(object sender, EventArgs e)
        {
            #region declaration
            double poidBagage = 0.00;
            double poidLimite = 0.00;

            crlFicheBord FicheBord = null;
            #endregion

            #region implementation
            try
            {
                poidBagage = double.Parse(TextPoidBagage.Text);
            }
            catch (Exception)
            {
            }

            FicheBord = serviceFicheBord.selectFicheBord(LabNumerosFB.Text);
            if (FicheBord != null)
            {
                poidLimite = FicheBord.autorisationVoyage.Verification.Itineraire.infoExedantBagage.PoidAutorise;
            }

            if (poidBagage > poidLimite)
            {
                TextExedentPoid.Text = (poidBagage - poidLimite).ToString("0");
                TextPrixEcedent.Text = ((poidBagage - poidLimite) * double.Parse(FicheBord.autorisationVoyage.Verification.Itineraire.infoExedantBagage.PrixExedantBagage)).ToString("0");
            }
            else
            {
                TextExedentPoid.Text = "";
                TextPrixEcedent.Text = "";
            }
            #endregion
        }

        protected void TextPoidModif_TextChanged(object sender, EventArgs e)
        {
            #region declaration
            double poidBagage = 0.00;
            double poidLimite = 0.00;

            crlFicheBord FicheBord = null;
            #endregion

            #region implementation
            try
            {
                poidBagage = double.Parse(TextPoidModif.Text);
            }
            catch (Exception)
            {
            }

            FicheBord = serviceFicheBord.selectFicheBord(LabNumerosFB.Text);
            if (FicheBord != null)
            {
                poidLimite = FicheBord.autorisationVoyage.Verification.Itineraire.infoExedantBagage.PoidAutorise;
            }
            if (poidBagage > poidLimite)
            {
                TextExcedentModif.Text = (poidBagage - poidLimite).ToString("0");
                TextPrixExcedentModif.Text = ((poidBagage - poidLimite) * double.Parse(FicheBord.autorisationVoyage.Verification.Itineraire.infoExedantBagage.PrixExedantBagage)).ToString("0");
            }
            else
            {
                TextExcedentModif.Text = "";
                TextPrixExcedentModif.Text = "";
            }
            #endregion
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulairePassagerAll();
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            string numPlace = "";
            crlVoyage voyage = null;
            crlRecuEncaisser recu = null;
            crlBagage bagage = null;
            #endregion

            #region implementation
            if (this.testFormulaire())
            {
                if (this.testRecu())
                {
                    numPlace = this.testPlace();
                    if (numPlace != "")
                    {
                        voyage = new crlVoyage();
                        this.insertObjetVoyage(voyage);

                        voyage.individu.NumIndividu = serviceIndividu.isIndividu(voyage.individu);

                        if (voyage.individu.NumIndividu != "")
                        {
                            serviceIndividu.updateIndividu(voyage.individu);
                            voyage.NumIndividu = voyage.individu.NumIndividu;
                        }
                        else
                        {
                            voyage.individu.NumIndividu = serviceIndividu.insertIndividu(voyage.individu, agent.agence.SigleAgence);
                            voyage.NumIndividu = voyage.individu.NumIndividu;
                        }

                        servicePlaceFB.updatePlaceFB(voyage.placeFB);

                        voyage.IdVoyage = serviceVoyage.insertVoyage(voyage, agent.agence.SigleAgence);

                        if (voyage.IdVoyage != "")
                        {
                            if (TextExedentPoid.Text != "" && TextPrixEcedent.Text != "")
                            {
                                recu = serviceRecu.isValideRecu(TextNumRecu.Text);
                                if (recu != null)
                                {
                                    bagage = new crlBagage();
                                    try
                                    {
                                        bagage.ExcedentPoid = double.Parse(TextExedentPoid.Text);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    bagage.NumRecu = recu.NumRecuEncaisser;
                                    bagage.PrixExcedent = TextPrixEcedent.Text;

                                    bagage.IdBagage = serviceBagage.insertBagage(bagage, agent.agence.SigleAgence);
                                    serviceBagage.insertAssociationBagageVoyage(voyage.IdVoyage, bagage.IdBagage);
                                }
                            }

                            this.initialiseGridPassager();


                            Response.Redirect("RemplirFB.aspx?numerosFB=" + LabNumerosFB.Text + "&idVoyage=" + voyage.IdVoyage);
                        }
                    }
                }
            }
            #endregion
        }

        protected void btnAnnulerModif_Click(object sender, EventArgs e)
        {
            divModif.Visible = false;
            divModifBtn.Visible = false;
            divValide.Visible = true;
            divValidBtn.Visible = true;
            hfIdVoyage.Value = "";

            this.divIndicationText("", "Red");
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            string numPlace = "";
            crlVoyage voyage = null;
            crlIndividu individu = null;
            crlRecuEncaisser recu = null;
            crlBagage bagage = null;
            #endregion

            #region implementation
            if (this.testFormulaireModif())
            {

                numPlace = this.testPlaceModif();
                if (numPlace != "")
                {
                    if (hfIdVoyage.Value != "")
                    {
                        voyage = serviceVoyage.selectVoyage(hfIdVoyage.Value);

                        if (voyage != null)
                        {

                            if (voyage.bagage != null)
                            {
                                serviceBagage.deleteAssociationBagageVoyage(hfIdVoyage.Value);
                                serviceBagage.deleteBagage(voyage.bagage);
                            }

                            if (this.testRecuModif())
                            {

                                individu = new crlIndividu();
                                individu.Adresse = TextAdresseModif.Text;
                                individu.CinIndividu = TextCinModif.Text;
                                individu.TelephoneMobileIndividu = TextPortebleModif.Text;
                                individu.NomIndividu = TextNomModif.Text;
                                individu.PrenomIndividu = TextPrenomModif.Text;
                                individu.TelephoneFixeIndividu = TextTelephoneModif.Text;

                                individu.NumIndividu = serviceIndividu.isIndividu(individu);
                                if (individu.NumIndividu.Equals(""))
                                {
                                    individu.NumIndividu = serviceIndividu.insertIndividu(individu, agent.agence.SigleAgence);
                                }
                                else
                                {
                                    serviceIndividu.updateIndividu(individu);
                                }

                                if (individu.NumIndividu != "")
                                {
                                    voyage.NumIndividu = individu.NumIndividu;
                                }

                                voyage.placeFB.IsOccuper = 0;
                                servicePlaceFB.updatePlaceFB(voyage.placeFB);

                                voyage.NumPlace = numPlace;

                                voyage.placeFB = servicePlaceFB.selectPlaceFB(LabNumerosFB.Text, numPlace);
                                voyage.placeFB.IsOccuper = 1;


                                try
                                {
                                    voyage.PoidBagage = double.Parse(TextPoidModif.Text);
                                }
                                catch (Exception)
                                {
                                }

                                servicePlaceFB.updatePlaceFB(voyage.placeFB);
                                serviceVoyage.updateVoyage(voyage);

                                if (TextExcedentModif.Text != "" && TextPrixExcedentModif.Text != "")
                                {
                                    recu = serviceRecu.isValideRecu(TextNumRecuModif.Text);
                                    if (recu != null)
                                    {
                                        bagage = new crlBagage();
                                        try
                                        {
                                            bagage.ExcedentPoid = double.Parse(TextExcedentModif.Text);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        bagage.NumRecu = recu.NumRecuEncaisser;
                                        bagage.PrixExcedent = TextPrixExcedentModif.Text;

                                        bagage.IdBagage = serviceBagage.insertBagage(bagage, agent.agence.SigleAgence);
                                        serviceBagage.insertAssociationBagageVoyage(voyage.IdVoyage, bagage.IdBagage);
                                    }
                                }
                                Response.Redirect("RemplirFB.aspx?numerosFB=" + LabNumerosFB.Text);
                            }
                            else
                            {
                                if (voyage.bagage != null)
                                {
                                    string idBag = serviceBagage.insertBagage(voyage.bagage, agent.agence.SigleAgence);
                                    serviceBagage.insertAssociationBagageVoyage(voyage.IdVoyage, idBag);
                                }
                            }


                        }
                    }
                }
                else
                {
                    if (hfIdVoyage.Value != "")
                    {
                        voyage = serviceVoyage.selectVoyage(hfIdVoyage.Value);
                        if (voyage != null)
                        {
                            //serviceBagage.deleteAssociationBagageVoyage(hfIdVoyage.Value);
                            if (voyage.bagage != null)
                            {
                                serviceBagage.deleteAssociationBagageVoyage(hfIdVoyage.Value);
                                serviceBagage.deleteBagage(voyage.bagage);
                            }

                            if (this.testRecuModif())
                            {
                                individu = new crlIndividu();
                                individu.Adresse = TextAdresseModif.Text;
                                individu.CinIndividu = TextCinModif.Text;
                                individu.TelephoneMobileIndividu = TextPortebleModif.Text;
                                individu.NomIndividu = TextNomModif.Text;
                                individu.PrenomIndividu = TextPrenomModif.Text;
                                individu.TelephoneFixeIndividu = TextTelephoneModif.Text;

                                individu.NumIndividu = serviceIndividu.isIndividu(individu);
                                if (individu.NumIndividu.Equals(""))
                                {
                                    individu.NumIndividu = serviceIndividu.insertIndividu(individu, agent.agence.SigleAgence);
                                }
                                else
                                {
                                    serviceIndividu.updateIndividu(individu);
                                }

                                if (individu.NumIndividu != "")
                                {
                                    voyage.NumIndividu = individu.NumIndividu;
                                }

                                //voyage.placeFB.IsOccuper = 0;
                                //servicePlaceFB.updatePlaceFB(voyage.placeFB);

                                //voyage.NumPlace = numPlace;

                                //voyage.placeFB = servicePlaceFB.selectPlaceFB(LabNumerosFB.Text, numPlace);
                                //voyage.placeFB.IsOccuper = 1;

                                try
                                {
                                    voyage.PoidBagage = double.Parse(TextPoidModif.Text);
                                }
                                catch (Exception)
                                {
                                }

                                //servicePlaceFB.updatePlaceFB(voyage.placeFB);
                                serviceVoyage.updateVoyage(voyage);

                                if (TextExcedentModif.Text != "" && TextPrixExcedentModif.Text != "")
                                {
                                    recu = serviceRecu.isValideRecu(TextNumRecuModif.Text);
                                    if (recu != null)
                                    {
                                        bagage = new crlBagage();
                                        try
                                        {
                                            bagage.ExcedentPoid = double.Parse(TextExcedentModif.Text);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        bagage.NumRecu = recu.NumRecuEncaisser;
                                        bagage.PrixExcedent = TextPrixExcedentModif.Text;

                                        bagage.IdBagage = serviceBagage.insertBagage(bagage, agent.agence.SigleAgence);
                                        serviceBagage.insertAssociationBagageVoyage(voyage.IdVoyage, bagage.IdBagage);
                                    }
                                }

                                Response.Redirect("RemplirFB.aspx?numerosFB=" + LabNumerosFB.Text);
                            }
                            else
                            {
                                if (voyage.bagage != null)
                                {
                                    string idBag2 = serviceBagage.insertBagage(voyage.bagage, agent.agence.SigleAgence);
                                    serviceBagage.insertAssociationBagageVoyage(voyage.IdVoyage, idBag2);
                                }
                            }
                        }
                    }
                }

            }
            #endregion
        }

        protected void ddlTriRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridPassager();
        }

        protected void btnRecherche_Click(object sender, EventArgs e)
        {
            this.initialiseGridPassager();
        }

        protected void gvPassager_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region declaration
            crlVoyage voyage = null;
            #endregion

            #region implementation
            if (e.CommandName.Trim().Equals("deleteV"))
            {
                voyage = serviceVoyage.selectVoyage(e.CommandArgument.ToString());
                if (voyage != null)
                {
                    if (voyage.bagage != null)
                    {
                        serviceBagage.deleteAssociationBagageVoyage(voyage.IdVoyage);
                        serviceBagage.deleteBagage(voyage.bagage.IdBagage);
                    }
                    voyage.placeFB.IsOccuper = 0;
                    servicePlaceFB.updatePlaceFB(voyage.placeFB);
                }
                serviceVoyage.deleteAllVoyage(e.CommandArgument.ToString());
                this.initialiseGridPassager();
                Response.Redirect("RemplirFB.aspx?numerosFB=" + LabNumerosFB.Text);
            }
            else if (e.CommandName.Trim().Equals("select"))
            {
                divModif.Visible = true;
                divModifBtn.Visible = true;
                divValide.Visible = false;
                divValidBtn.Visible = false;
                hfIdVoyage.Value = e.CommandArgument.ToString();
                this.afficheVoyage(hfIdVoyage.Value);
            }
            #endregion
        }

        protected void gvPassager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPassager.PageIndex = e.NewPageIndex;
            this.initialiseGridPassager();
        }
        #endregion

        #region commission
        protected void ddlTriRechercheCommission_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }
        protected void btnRechercheCommission_Click(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }

        protected void ddlTriCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }
        protected void btnRechercheCom_Click(object sender, EventArgs e)
        {
            this.initialiseGridCommission();
        }

        protected void gvCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommission.PageIndex = e.NewPageIndex;
            this.initialiseGridCommission();
        }
        protected void gvCommission_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                serviceCommission.insertCommissionToFB(e.CommandArgument.ToString(), LabNumerosFB.Text);
                this.initialiseGridCommission();
            }
        }

        protected void gvCom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCom.PageIndex = e.NewPageIndex;
            this.initialiseGridCommission();
        }
        protected void gvCom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("select"))
            {
                serviceCommission.deleteCommissionToFB(e.CommandArgument.ToString(), LabNumerosFB.Text);
                this.initialiseGridCommission();
            }
        }
        #endregion

        #endregion
    }
}