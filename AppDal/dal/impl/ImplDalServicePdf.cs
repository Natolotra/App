using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.dal.intf;
using arch.crl;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Generic;
using arc.utile;

namespace arch.dal.impl
{
    /// <summary>
    /// implementation du service pdf
    /// </summary>
    public class ImplDalServicePdf : IntfDalServicePdf
    {
        #region declaration variable
        IntfAutorisationVoyage serviceAuytorisationVoyage = null;
        IntfDalAutorisationDepart serviceAutorisationDepart = null;
        IntfDalFicheBord serviceFicheBord = null;
        IntfDalRecu serviceRecu = null;
        IntfDalRecuEncaisser serviceRecuEncaisser = null;
        IntfDalRecuDecaisser serviceRecuDecaisser = null;
        IntfDalRecuAD serviceRecuAD = null;
        IntfDalFacture serviceFacture = null;
        IntfDalProprietaire serviceProprietaire = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalVoyage serviceVoyage = null;
        IntfDalVerification serviceVerification = null;

        crlAutorisationVoyage AutorisationVoyage = null;
        crlAutorisationDepart AutorisationDepart = null;
        crlFacture Facture = null;
        crlRecu Recu = null;
        crlRecuEncaisser RecuEncaisser = null;
        crlRecuDecaisser RecuDecaisser = null;
        crlRecuAD RecuAD = null;
        crlVerification verification = null;
        #endregion

        #region construsteur
        public ImplDalServicePdf()
        {
        }
        #endregion

        #region IntfDalServicePdf Members

        bool IntfDalServicePdf.printAutorisationVoyage(string numerosAV, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceAuytorisationVoyage = new ImplAutorisationVoyage();
            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (numerosAV != "")
            {
                AutorisationVoyage = serviceAuytorisationVoyage.selectAutorisationVoyage(numerosAV);
                if (AutorisationVoyage != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numAV = "AUTORISATION DE VOYAGE N°";
                    string numAVVar = AutorisationVoyage.NumerosAV;

                    string strImformation = "";

                    string nomAgentTechnique = "Agent technique:";
                    string nomAgentTechniqueVar = AutorisationVoyage.Agent.nomAgent + " " + AutorisationVoyage.Agent.prenomAgent;

                    string numMatrAuto = "Numéro d'Immatriculation:";
                    string numMatrAutoVar = AutorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;

                    string nomChauffeur = "Nom chauffeur:";
                    string nomChauffeurVar = AutorisationVoyage.Verification.Chauffeur.nomChauffeur + " " + AutorisationVoyage.Verification.Chauffeur.prenomChauffeur;

                    string datePrevu = "Date prévue de départ:";
                    string datePrevuVar = AutorisationVoyage.DatePrevueDepart.ToString("dd MMMM yyyy");

                    string itineraire = "Itinéraire:";
                    string itineraireVar = AutorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + AutorisationVoyage.Verification.Itineraire.villeF.NomVille;

                    string nomAgentVerificateur = "Agent vérificateur:";
                    string nomAgentVerificateurVar = AutorisationVoyage.Verification.Agent.nomAgent + " " + AutorisationVoyage.Verification.Agent.prenomAgent;

                    string verificationTchnique = "Vérification technique:";
                    string verificationTchniqueVar = "OK";

                    string avoireVT = "A voir:";
                    string avoireVTVar = AutorisationVoyage.Verification.AVoireVT;

                    string verificationPapier = "Vérification papier:";
                    string verificationPapierVar = "OK";

                    string avoireVP = "A voir:";
                    string avoireVPVar = AutorisationVoyage.Verification.AVoireVP;

                    string ObservationProfassionnelle = "Observation professionnelle:";

                    string planDepart = "Plan de départ:";
                    string planDepartVar = "";
                    if (AutorisationVoyage.Verification.PlanDepart == 1)
                    {
                        planDepartVar = "Respecter";
                    }
                    else if (AutorisationVoyage.Verification.PlanDepart == 2)
                    {
                        planDepartVar = "Attente 24H";
                    }
                    else if (AutorisationVoyage.Verification.PlanDepart == 3)
                    {
                        planDepartVar = "Attente 48H";
                    }

                    string dateVerification = "Date de verification:";
                    string dateVerificationVar = AutorisationVoyage.Verification.DateVerification.ToString("dd MMMM yyyy");

                    string observationVisa = "OBSERVATION FINALE ET VISA:";
                   
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region creePdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numAV);
                    Phrase paraNumFBVar = new Phrase(numAVVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region information
                    Paragraph paraInfo = new Paragraph(strImformation);
                    paraInfo.Font.Size = 5;
                    #endregion

                    #region AV
                    iTextSharp.text.Table tableAV = new iTextSharp.text.Table(6, 6);
                    tableAV.Border = 0;
                    tableAV.DefaultCellBorder = 0;
                    tableAV.Padding = 0;
                    tableAV.Width = 100;
                    tableAV.Widths = new float[] { 15,25,15,5,10,30};

                    Phrase paraAgentTech = new Phrase(nomAgentTechnique);
                    Phrase paraAgentTechVar = new Phrase(nomAgentTechniqueVar);
                    paraAgentTech.Font.Size = 6;
                    paraAgentTechVar.Font.Size = 6;

                    Phrase paraNumMatr = new Phrase(numMatrAuto);
                    Phrase paraNumMatrVar = new Phrase(numMatrAutoVar);
                    paraNumMatr.Font.Size = 6;
                    paraNumMatrVar.Font.Size = 6;

                    Phrase paraNomChauffeur = new Phrase(nomChauffeur);
                    Phrase paraNomChauffeurVar = new Phrase(nomChauffeurVar);
                    paraNomChauffeur.Font.Size = 6;
                    paraNomChauffeurVar.Font.Size = 6;

                    Phrase paraDatePrevue = new Phrase(datePrevu);
                    Phrase paraDatePrevueVar = new Phrase(datePrevuVar);
                    paraDatePrevue.Font.Size = 6;
                    paraDatePrevueVar.Font.Size = 6;

                    Phrase paraItineraire = new Phrase(itineraire);
                    Phrase paraItineraireVar = new Phrase(itineraireVar);
                    paraItineraire.Font.Size = 6;
                    paraItineraireVar.Font.Size = 6;

                    Phrase paraAgentVerificateur = new Phrase(nomAgentVerificateur);
                    Phrase paraAgentVerificateurVar = new Phrase(nomAgentVerificateurVar);
                    paraAgentVerificateur.Font.Size = 6;
                    paraAgentVerificateurVar.Font.Size = 6;

                    Phrase paraVT = new Phrase(verificationTchnique);
                    paraVT.Font.Size = 6;
                    Phrase paraVTVar = new Phrase(verificationTchniqueVar);
                    paraVTVar.Font.Size = 6;

                    Phrase paraVP = new Phrase(verificationPapier);
                    paraVP.Font.Size = 6;
                    Phrase paraVPVar = new Phrase(verificationPapierVar);
                    paraVPVar.Font.Size = 6;

                    Phrase paraObservation = new Phrase(ObservationProfassionnelle);
                    paraObservation.Font.Size = 6;

                    Phrase paraPlanDepart = new Phrase(planDepart);
                    paraPlanDepart.Font.Size = 6;
                    Phrase paraPlanDepartVar = new Phrase(planDepartVar);
                    paraPlanDepartVar.Font.Size = 6;

                    Phrase paraDateVerification = new Phrase(dateVerification);
                    Phrase paraDateVerificationVar = new Phrase(dateVerificationVar);
                    paraDateVerification.Font.Size = 6;
                    paraDateVerificationVar.Font.Size = 6;

                    Phrase paraAvoirVT = new Phrase(avoireVT);
                    Phrase paraAvoirVTVar = new Phrase(avoireVTVar);
                    paraAvoirVT.Font.Size = 6;
                    paraAvoirVTVar.Font.Size = 6;

                    Phrase paraAvoirVP = new Phrase(avoireVP);
                    Phrase paraAvoirVPVar = new Phrase(avoireVPVar);
                    paraAvoirVP.Font.Size = 6;
                    paraAvoirVPVar.Font.Size = 6;

                    Cell cellAgentTech = new Cell(paraAgentTech);
                    Cell cellAgentTechVar = new Cell(paraAgentTechVar);

                    Cell cellNumMatrAuto = new Cell(paraNumMatr);
                    Cell cellNumMatrAutoVar = new Cell(paraNumMatrVar);

                    Cell cellChauffeur = new Cell(paraNomChauffeur);
                    Cell cellChauffeurVar = new Cell(paraNomChauffeurVar);

                    Cell cellDatePrevu = new Cell(paraDatePrevue);
                    Cell cellDatePrevuVar = new Cell(paraDatePrevueVar);

                    Cell cellItineraire = new Cell(paraItineraire);
                    Cell cellItineraireVar = new Cell(paraItineraireVar);

                    Cell cellAgentVerificateur = new Cell(paraAgentVerificateur);
                    Cell cellAgentVerificateurVar = new Cell(paraAgentVerificateurVar);
                   

                    Cell cellVT = new Cell(paraVT);
                    Cell cellVTVar = new Cell(paraVTVar);

                    Cell cellVP = new Cell(paraVP);
                    Cell cellVPVar = new Cell(paraVPVar);

                    Cell cellObservation = new Cell(paraObservation);
                    Cell cellObservationVar = new Cell();
                    cellObservationVar.Colspan = 3;

                    Cell cellPlanDepart = new Cell(paraPlanDepart);
                    Cell cellPlanDepartVar = new Cell(paraPlanDepartVar);
                    cellPlanDepartVar.Colspan = 3;

                    Cell cellDateVerification = new Cell(paraDateVerification);
                    Cell cellDateVerificationVar = new Cell(paraDateVerificationVar);
                    cellDateVerificationVar.Colspan = 3;

                    Cell cellAvoirVT = new Cell(paraAvoirVT);
                    Cell cellAvoirVTVar = new Cell(paraAvoirVTVar);

                    Cell cellAvoirVP = new Cell(paraAvoirVP);
                    Cell cellAvoirVPVar = new Cell(paraAvoirVPVar);

                    Cell cellVide = new Cell("");
                    cellVide.Colspan = 4;
                   

                    tableAV.AddCell(cellAgentTech);
                    tableAV.AddCell(cellAgentTechVar);
                    tableAV.AddCell(cellVT);
                    tableAV.AddCell(cellVTVar);
                    tableAV.AddCell(cellAvoirVT);
                    tableAV.AddCell(cellAvoirVTVar);

                    tableAV.AddCell(cellNumMatrAuto);
                    tableAV.AddCell(cellNumMatrAutoVar);
                    tableAV.AddCell(cellVP);
                    tableAV.AddCell(cellVPVar);
                    tableAV.AddCell(cellAvoirVP);
                    tableAV.AddCell(cellAvoirVPVar);

                    tableAV.AddCell(cellChauffeur);
                    tableAV.AddCell(cellChauffeurVar);
                    tableAV.AddCell(cellObservation);
                    tableAV.AddCell(cellObservationVar);
                    //tableAV.AddCell("");
                    //tableAV.AddCell("");

                    tableAV.AddCell(cellDatePrevu);
                    tableAV.AddCell(cellDatePrevuVar);
                    tableAV.AddCell(cellPlanDepart);
                    tableAV.AddCell(cellPlanDepartVar);
                    //tableAV.AddCell("");
                    //tableAV.AddCell("");

                    tableAV.AddCell(cellItineraire);
                    tableAV.AddCell(cellItineraireVar);
                    tableAV.AddCell(cellDateVerification);
                    tableAV.AddCell(cellDateVerificationVar);
                    //tableAV.AddCell("");
                    //tableAV.AddCell("");

                    tableAV.AddCell(cellAgentVerificateur);
                    tableAV.AddCell(cellAgentVerificateurVar);
                    tableAV.AddCell(cellVide);
                   
                  
                    #endregion

                    #region observation et visa
                    observationVisa += "\n\n\n\n\n\n\n\n\n\n";
                    Paragraph paraObservationFinal = new Paragraph(observationVisa);
                    paraObservationFinal.Font.Size = 10;

                    iTextSharp.text.Table tableObservation = new iTextSharp.text.Table(1, 1);
                    tableObservation.Border = 0;
                    tableObservation.DefaultCellBorder = 0;
                    tableObservation.Padding = 4;
                    tableObservation.Width = 100;

                    Cell cellObservationFinal = new Cell(paraObservationFinal);
                    cellObservationFinal.BorderWidthBottom = 1;
                    cellObservationFinal.BorderWidthTop = 1;

                    tableObservation.AddCell(cellObservationFinal);
                    #endregion

                    #region separateur
                    Paragraph paraSeparateur = new Paragraph("\n\n\n\n");
                    #endregion

                    #region ajout des element dans document
                    document.Add(tableLogo);
                    document.Add(paraInfo);
                    document.Add(tableAV);
                    document.Add(tableObservation);
                    document.Add(paraSeparateur);
                    document.Add(tableLogo);
                    document.Add(paraInfo);
                    document.Add(tableAV);
                    document.Add(tableObservation);

                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();

                }
            } 
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printVerification(string idVerification, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceVerification = new ImplDalVerification();
            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (idVerification != "")
            {
                verification = serviceVerification.selectVerification(idVerification);

                if (verification != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string idVerificationStr = "";

                    if (verification.VerificationPapier == 0 || verification.VerificationTechnique == 0)
                    {
                        idVerificationStr = "AUTORISATION DE VOYAGE NON ACCORDER";
                    }
                    else
                    {
                        idVerificationStr = "";
                    }

                    string observationVisa = "OBSERVATION FINALE ET VISA:";
                   
                    string numMatrAuto = "Vehicule:";
                    string numMatrAutoVar = verification.Licence.vehicule.MatriculeVehicule + " " + verification.Licence.vehicule.MarqueVehicule;

                    string nomChauffeur = "Nom chauffeur:";
                    string nomChauffeurVar = verification.Chauffeur.nomChauffeur + " " + verification.Chauffeur.prenomChauffeur;

                    string dateVer = "Date de vérification:";
                    string dateVerVar = verification.DateVerification.ToString("dd MMMM yyyy");

                    string itineraire = "Itinéraire:";
                    string itineraireVar = verification.Itineraire.villeD.NomVille + "-" + verification.Itineraire.villeF.NomVille;

                    string nomAgentVerificateur = "Agent vérificateur:";
                    string nomAgentVerificateurVar = verification.Agent.nomAgent + " " + verification.Agent.prenomAgent;

                    string verificationTchnique = "Vérification technique:";
                    string verificationTchniqueVar = "";

                    if (verification.VerificationTechnique == 0)
                    {
                        verificationTchniqueVar = "Non OK";
                    }
                    else
                    {
                        verificationTchniqueVar = "OK";
                    }

                    string avoireVT = "A voir:";
                    string avoireVTVar = verification.AVoireVT;

                    string verificationPapier = "Vérification papier:";
                    string verificationPapierVar = "";

                    if (verification.VerificationPapier == 0)
                    {
                        verificationPapierVar = "Non OK";
                    }
                    else
                    {
                        verificationPapierVar = "OK";
                    }

                    string avoireVP = "A voir:";
                    string avoireVPVar = verification.AVoireVP;

                    Phrase phraseAlaLigne = new Phrase("\n");
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region creePdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(idVerificationStr);
                    Phrase paraNumFBVar = new Phrase("");
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion


                    #region AV
                    iTextSharp.text.Table tableAV = new iTextSharp.text.Table(5, 5);
                    tableAV.Border = 0;
                    tableAV.DefaultCellBorder = 0;
                    tableAV.Padding = 0;
                    tableAV.Width = 100;
                    tableAV.Widths = new float[] { 15, 25, 5, 15, 40};

                    Phrase phraseNumMatrAuto = new Phrase(numMatrAuto);
                    Phrase phraseNummatrAutoVar = new Phrase(numMatrAutoVar);
                    phraseNumMatrAuto.Font.Size = 6;
                    phraseNummatrAutoVar.Font.Size = 6;


                    Phrase phraseNomChauffeur = new Phrase(nomChauffeur);
                    Phrase phraseNomChauffeurVar = new Phrase(nomChauffeurVar);
                    phraseNomChauffeur.Font.Size = 6;
                    phraseNomChauffeurVar.Font.Size = 6;

                    Phrase phraseDate = new Phrase(dateVer);
                    Phrase phraseDateVar = new Phrase(dateVerVar);
                    phraseDate.Font.Size = 6;
                    phraseDateVar.Font.Size = 6;

                    Phrase phraseItineraire = new Phrase(itineraire);
                    Phrase phraseItineraireVar = new Phrase(itineraireVar);
                    phraseItineraire.Font.Size = 6;
                    phraseItineraireVar.Font.Size = 6;

                    Phrase phraseAgentVer = new Phrase(nomAgentVerificateur);
                    Phrase phraseAgentVerVar = new Phrase(nomAgentVerificateurVar);
                    phraseAgentVer.Font.Size = 6;
                    phraseAgentVerVar.Font.Size = 6;

                    Phrase phraseVerTech = new Phrase(verificationTchnique);
                    Phrase phraseVerTechVar = new Phrase(verificationTchniqueVar);
                    phraseVerTech.Font.Size = 6;
                    phraseVerTechVar.Font.Size = 6;

                    Phrase phraseAvoireVT = new Phrase(avoireVT);
                    Phrase phraseAvoireVTVar = new Phrase(avoireVTVar);
                    phraseAvoireVT.Font.Size = 6;
                    phraseAvoireVTVar.Font.Size = 6;

                    Phrase phraseVerPap = new Phrase(verificationPapier);
                    Phrase phraseVerPapVar = new Phrase(verificationPapierVar);
                    phraseVerPap.Font.Size = 6;
                    phraseVerPapVar.Font.Size = 6;

                    Phrase phraseAvoireVP = new Phrase(avoireVP);
                    Phrase phraseAvoireVPVar = new Phrase(avoireVPVar);
                    phraseAvoireVP.Font.Size = 6;
                    phraseAvoireVPVar.Font.Size = 6;

                    Cell cellVide = new Cell("");

                    tableAV.AddCell(phraseDate);
                    tableAV.AddCell(phraseDateVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseVerTech);
                    tableAV.AddCell(phraseVerTechVar);

                    tableAV.AddCell(phraseNumMatrAuto);
                    tableAV.AddCell(phraseNummatrAutoVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseAvoireVT);
                    tableAV.AddCell(phraseAvoireVTVar);

                    tableAV.AddCell(phraseNomChauffeur);
                    tableAV.AddCell(phraseNomChauffeurVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseVerPap);
                    tableAV.AddCell(phraseVerPapVar);

                    tableAV.AddCell(phraseItineraire);
                    tableAV.AddCell(phraseItineraireVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseAvoireVP);
                    tableAV.AddCell(phraseAvoireVPVar);

                    tableAV.AddCell(phraseAgentVer);
                    tableAV.AddCell(phraseAgentVerVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(cellVide);
                    #endregion

                    #region observation et visa
                    observationVisa += "\n\n\n\n\n\n\n\n\n\n";
                    Paragraph paraObservationFinal = new Paragraph(observationVisa);
                    paraObservationFinal.Font.Size = 10;

                    iTextSharp.text.Table tableObservation = new iTextSharp.text.Table(1, 1);
                    tableObservation.Border = 0;
                    tableObservation.DefaultCellBorder = 0;
                    tableObservation.Padding = 4;
                    tableObservation.Width = 100;

                    Cell cellObservationFinal = new Cell(paraObservationFinal);
                    cellObservationFinal.BorderWidthBottom = 1;
                    cellObservationFinal.BorderWidthTop = 1;

                    tableObservation.AddCell(cellObservationFinal);
                    #endregion

                    #region separateur
                    Paragraph paraSeparateur = new Paragraph("\n\n\n\n");
                    #endregion

                    #region ajout des element dans document
                    document.Add(tableLogo);
                    document.Add(tableAV);
                    document.Add(phraseAlaLigne);
                    document.Add(tableObservation);
                    document.Add(paraSeparateur);
                    document.Add(tableLogo);
                    document.Add(tableAV);
                    document.Add(tableObservation);

                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printFicheBord(string numAutorisationDepart, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceAutorisationDepart = new ImplDalAutorisationDepart();
            serviceGeneral = new ImplDalGeneral();
            serviceFicheBord = new ImplDalFicheBord();
            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                AutorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(numAutorisationDepart);
                if (AutorisationDepart != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numFB = "FICHE DE BORD N°";
                    string numFBVar = AutorisationDepart.ficheBord.NumerosFB;

                    string strImformation = "";

                    string agent = "AGENT CONTROLEUR DE TRANSPORT:";
                    string agentVar = AutorisationDepart.agent.nomAgent + " " + AutorisationDepart.agent.prenomAgent + " matricule:" + AutorisationDepart.agent.matriculeAgent;

                    string nomSociete = "Nom société de transport:";
                    string nomSocieteVar = AutorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.cooperative.NomCooperative;

                    string autorisationVoyage = "Autorisation de voyage:";
                    string autorisationVoyageVar = AutorisationDepart.ficheBord.autorisationVoyage.NumerosAV + " du " + AutorisationDepart.ficheBord.autorisationVoyage.Verification.DateVerification.ToString("dd MMMM yyyy");

                    string itineraire = "Itinéraire:";
                    string itineraireVar = AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.villeF.NomVille;

                    if (AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.routeNationale != null)
                    {
                        for (int i = 0; i < AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.routeNationale.Count; i++)
                        {
                            if (i == 0)
                            {
                                itineraireVar = itineraireVar + " Axe " + AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.routeNationale[i].RouteNationale;
                            }
                            else
                            {
                                itineraireVar = itineraireVar + "-" + AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.routeNationale[i].RouteNationale;
                            }
                        }
                    }

                    string dateHeureDepart = "Date et heure de départ:";
                    string dateHeureDepartVar = AutorisationDepart.ficheBord.DateHeurDepart.ToString("dd MMMM yyyy") + " à " + AutorisationDepart.ficheBord.DateHeurDepart.ToString("HH:mm");

                    string voiture = "Numéros de la voiture:";
                    string voitureVar = AutorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule + " couleur " + AutorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule + " " + AutorisationDepart.ficheBord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule;

                    string chauffeur = "Nom du chauffeur";
                    string chauffeurVar = AutorisationDepart.ficheBord.autorisationVoyage.Verification.Chauffeur.nomChauffeur + " " + AutorisationDepart.ficheBord.autorisationVoyage.Verification.Chauffeur.prenomChauffeur;

                    string listePassager = "LISTE DES PASSAGERS:";

                    string nomPrenom = "Nom et prénom";
                    string pieceIdentite = "Pièce d'identité";
                    string destination = "Destination";
                    string numSiege = "N°Siège";
                    string bagage = "Bagage(Kg)";
                    string prixDuBillet = "Prix du billet(Ar)";
                    string numBillet = "N°Billet";
                    string excedentBag = "Excédent bags";
                    string sommeRecu = "Somme reçu";

                    string nombreTotalPassagers = "Nombre total des passagers:";

                    string listeCommission = "COMMISSION:";

                    string type = "Type";
                    string designation = "Désignation";
                    string poids = "Poids";
                    string nomExpediteur = "Nom de l'expéditeur";
                    string nomRecepteur = "Nom de réceptionnaire";
                    string pieceJustificatif = "Pièce justificatif";
                    string fraisEnvoi = "Frais d'envoi";

                    string observation = "OBSERVATIONS:";

                    string distanceParcourir = "Distance à parcourir:";
                    string distanceParcourirVar = AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.DistanceParcour + "Km";

                    string dureTrajet = "Durée approximative du trajet:";
                    string dureTrajetVar = serviceGeneral.getTextTimeSpan(AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.DureeTrajet);

                    string nombreRepos = "Nombre de repos obligatoire:";
                    string nombreReposVar = AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.NombreRepos.ToString();

                    TimeSpan timeSpanDuree = serviceGeneral.getTimeSpan(AutorisationDepart.ficheBord.autorisationVoyage.Verification.Itineraire.DureeTrajet);
                    DateTime dateTimeArrive = AutorisationDepart.ficheBord.DateHeurDepart.AddMinutes(timeSpanDuree.TotalMinutes);
                    string heureArrive = "Date et heure estimée à l'arrivé:";
                    string heureArriveVar = dateTimeArrive.ToString("dd MMMM yyyy") + " à " + dateTimeArrive.ToString("HH:mm");

                    string fraiVoyage = "Frais de voyage et diverse participation:";
                    double montantDeveloppement = serviceAutorisationDepart.getMontanDevelopement(AutorisationDepart.NumAutorisationDepart);
                    string fraiVoyageVar = serviceGeneral.separateurDesMilles(montantDeveloppement.ToString("0"));

                    string acompteVoyage = "Acompte de voyage y  compris carburant:";
                    double montantCarburant = serviceAutorisationDepart.getMontanRecu(AutorisationDepart.NumAutorisationDepart) - montantDeveloppement;
                    string acompteVoyageVar = serviceGeneral.separateurDesMilles(montantCarburant.ToString("0"));

                    string resteRegler = "Reste à régler:";
                    double montantResteRegler = AutorisationDepart.ResteRegle;

                    string resteReglerVar = serviceGeneral.separateurDesMilles(montantResteRegler.ToString("0"));

                    string listeEscorte = "ESCORTE:";

                    string matricule = "Matricule";
                    string trajet = "Trajet";
                    string observationAgentEscorte = "Observation de l'agent d'escorte";

                    string visas = "VISAS:";

                    string chefGare = "Chef de gare de départ";
                    string chauffeurSignature = "Chauffeur";
                    string arret1 = "Arrêt intermediaire et repos légal N°1";
                    string heurePassage1 = "Heure de passage";
                    string heureDepart1 = "Heure de départ";
                    string arret2 = "Arrêt intermediaire et repos légal N°2";
                    string heurePassage2 = "Heure de passage";
                    string heureDepart2 = "Heure de départ";
                    string visasArrive = "Visa à l'arrivé";
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region cree pdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numFB);
                    Phrase paraNumFBVar = new Phrase(numFBVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region information
                    Paragraph paraInfo = new Paragraph(strImformation);
                    paraInfo.Font.Size = 5;
                    #endregion

                    #region autorisationVoyage
                    Phrase phraseAgent = new Phrase("\n" + agent);
                    phraseAgent.Font.Size = 8;
                    phraseAgent.Font.SetStyle("underline");
                    Phrase phraseAgentVar = new Phrase(agentVar);
                    phraseAgentVar.Font.Size = 6;

                    iTextSharp.text.Table tableAV = new iTextSharp.text.Table(5, 3);
                    tableAV.Padding = 1;
                    tableAV.Width = 100;
                    tableAV.Widths = new float[] { 18,30,4,18,30};
                    tableAV.BorderWidth = 0;

                    Cell cellVide = new Cell();
                    cellVide.BorderWidthTop = 0;
                    cellVide.BorderWidthBottom = 0;

                    Phrase phraseNomSociete = new Phrase(nomSociete);
                    Phrase phraseNomSocieteVar = new Phrase(nomSocieteVar);
                    phraseNomSociete.Font.Size = 6;
                    phraseNomSocieteVar.Font.Size = 6;

                    Phrase phraseAV = new Phrase(autorisationVoyage);
                    Phrase phraseAVVar = new Phrase(autorisationVoyageVar);
                    phraseAV.Font.Size = 6;
                    phraseAVVar.Font.Size = 6;

                    Phrase phraseItineraire = new Phrase(itineraire);
                    Phrase phraseItineraireVar = new Phrase(itineraireVar);
                    phraseItineraire.Font.Size = 6;
                    phraseItineraireVar.Font.Size = 6;

                    Phrase phraseDateDepart = new Phrase(dateHeureDepart);
                    Phrase phraseDateDepartVar = new Phrase(dateHeureDepartVar);
                    phraseDateDepart.Font.Size = 6;
                    phraseDateDepartVar.Font.Size = 6;

                    Phrase phraseVoiture = new Phrase(voiture);
                    Phrase phraseVoitureVar = new Phrase(voitureVar);
                    phraseVoiture.Font.Size = 6;
                    phraseVoitureVar.Font.Size = 6;

                    Phrase phraseChauffeur = new Phrase(chauffeur);
                    Phrase phraseChauffeurVar = new Phrase(chauffeurVar);
                    phraseChauffeur.Font.Size = 6;
                    phraseChauffeurVar.Font.Size = 6;

                    tableAV.AddCell(phraseNomSociete);
                    tableAV.AddCell(phraseNomSocieteVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseDateDepart);
                    tableAV.AddCell(phraseDateDepartVar);

                    tableAV.AddCell(phraseAV);
                    tableAV.AddCell(phraseAVVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseVoiture);
                    tableAV.AddCell(phraseVoitureVar);

                    tableAV.AddCell(phraseItineraire);
                    tableAV.AddCell(phraseItineraireVar);
                    tableAV.AddCell(cellVide);
                    tableAV.AddCell(phraseChauffeur);
                    tableAV.AddCell(phraseChauffeurVar);
                    #endregion

                    #region liste passager
                    Phrase phraseListePassager = new Phrase(listePassager);
                    phraseListePassager.Font.Size = 8;
                    phraseListePassager.Font.SetStyle("underline");

                    iTextSharp.text.Table tablePassagers = new iTextSharp.text.Table(9, (2 + AutorisationDepart.ficheBord.voyage.Count));
                    tablePassagers.Padding = 1;
                    tablePassagers.Width = 100;
                    tablePassagers.Widths = new float[] { 21, 15,12,6,8,9,12,9,8};

                    Phrase phraseNomPrenom = new Phrase(nomPrenom);
                    phraseNomPrenom.Font.Size = 6;
                    Phrase phrasePieceIdentite = new Phrase(pieceIdentite);
                    phrasePieceIdentite.Font.Size = 6;
                    Phrase phraseDestination = new Phrase(destination);
                    phraseDestination.Font.Size = 6;
                    Phrase phraseNumSiege = new Phrase(numSiege);
                    phraseNumSiege.Font.Size = 6;
                    Phrase phraseBagage = new Phrase(bagage);
                    phraseBagage.Font.Size = 6;
                    Phrase phrasePrixBillet = new Phrase(prixDuBillet);
                    phrasePrixBillet.Font.Size = 6;
                    Phrase phraseNumBillet = new Phrase(numBillet);
                    phraseNumBillet.Font.Size = 6;
                    Phrase phraseExcedent = new Phrase(excedentBag);
                    phraseExcedent.Font.Size = 6;
                    Phrase phraseSommeRecu = new Phrase(sommeRecu);
                    phraseSommeRecu.Font.Size = 6;

                    tablePassagers.AddCell(phraseNomPrenom);
                    tablePassagers.AddCell(phrasePieceIdentite);
                    tablePassagers.AddCell(phraseDestination);
                    tablePassagers.AddCell(phraseNumSiege);
                    tablePassagers.AddCell(phraseBagage);
                    tablePassagers.AddCell(phrasePrixBillet);
                    tablePassagers.AddCell(phraseNumBillet);
                    tablePassagers.AddCell(phraseExcedent);
                    tablePassagers.AddCell(phraseSommeRecu);

                    Phrase phrase1;
                    Phrase phrase2;
                    Phrase phrase3;
                    Phrase phrase4;
                    Phrase phrase5;
                    Phrase phrase6;
                    Phrase phrase7;
                    Phrase phrase8;
                    Phrase phrase9;

                    for (int i = 0; i < AutorisationDepart.ficheBord.voyage.Count; i++)
                    {
                        phrase1 = new Phrase(AutorisationDepart.ficheBord.voyage[i].individu.NomIndividu + " " + AutorisationDepart.ficheBord.voyage[i].individu.PrenomIndividu);
                        phrase1.Font.Size = 6;

                        phrase2 = new Phrase(AutorisationDepart.ficheBord.voyage[i].PieceIdentite);
                        phrase2.Font.Size = 6;

                        phrase3 = new Phrase(AutorisationDepart.ficheBord.voyage[i].Destination);
                        phrase3.Font.Size = 6;

                        phrase4 = new Phrase(AutorisationDepart.ficheBord.voyage[i].NumPlace);
                        phrase4.Font.Size = 6;

                        phrase5 = new Phrase(AutorisationDepart.ficheBord.voyage[i].PoidBagage + "Kg");
                        phrase5.Font.Size = 6;

                        phrase6 = new Phrase(serviceGeneral.separateurDesMilles(AutorisationDepart.ficheBord.voyage[i].billet.PrixBillet) + "Ar");
                        phrase6.Font.Size = 6;

                        phrase7 = new Phrase(AutorisationDepart.ficheBord.voyage[i].billet.NumBillet);
                        phrase7.Font.Size = 6;

                        if (AutorisationDepart.ficheBord.voyage[i].bagage != null)
                        {
                            phrase8 = new Phrase(serviceGeneral.separateurDesMilles(AutorisationDepart.ficheBord.voyage[i].bagage.PrixExcedent) + "Ar(" + AutorisationDepart.ficheBord.voyage[i].bagage.ExcedentPoid + "Kg)");
                            phrase8.Font.Size = 6;

                            double montant = 0.00;
                            try
                            {
                                montant = double.Parse(AutorisationDepart.ficheBord.voyage[i].bagage.PrixExcedent) + double.Parse(AutorisationDepart.ficheBord.voyage[i].billet.PrixBillet);
                            }
                            catch (Exception)
                            {
                            }

                            phrase9 = new Phrase(serviceGeneral.separateurDesMilles(montant.ToString("0")) + "Ar");
                            phrase9.Font.Size = 6;
                        }
                        else
                        {
                            phrase8 = new Phrase("-");
                            phrase8.Font.Size = 6;

                            phrase9 = new Phrase(serviceGeneral.separateurDesMilles(AutorisationDepart.ficheBord.voyage[i].billet.PrixBillet) + "Ar");
                            phrase9.Font.Size = 6;
                        }

                        tablePassagers.AddCell(phrase1);
                        tablePassagers.AddCell(phrase2);
                        tablePassagers.AddCell(phrase3);
                        tablePassagers.AddCell(phrase4);
                        tablePassagers.AddCell(phrase5);
                        tablePassagers.AddCell(phrase6);
                        tablePassagers.AddCell(phrase7);
                        tablePassagers.AddCell(phrase8);
                        tablePassagers.AddCell(phrase9);
                    }

                    Phrase phraseNombrePassager = new Phrase(nombreTotalPassagers + serviceFicheBord.getNombreTotalPassager(AutorisationDepart.ficheBord.NumerosFB));
                    phraseNombrePassager.Font.Size = 6;
                    Phrase phrasePoidBagage = new Phrase(serviceFicheBord.getPoidTotalBagage(AutorisationDepart.ficheBord.NumerosFB).ToString("0") + "Kg");
                    phrasePoidBagage.Font.Size = 6;
                    Phrase phraseTotalPrixBillet = new Phrase(serviceGeneral.separateurDesMilles(serviceFicheBord.getPrixTotalBillet(AutorisationDepart.ficheBord.NumerosFB).ToString("0")) + "Ar");
                    phraseTotalPrixBillet.Font.Size = 6;
                    Phrase phraseTotalBagage = new Phrase(serviceGeneral.separateurDesMilles(serviceFicheBord.getPrixTotalBagage(AutorisationDepart.ficheBord.NumerosFB).ToString("0")) + "Ar");
                    phraseTotalBagage.Font.Size = 6;

                    double montantTotalPassagers = serviceFicheBord.getPrixTotalBagage(AutorisationDepart.ficheBord.NumerosFB) + serviceFicheBord.getPrixTotalBillet(AutorisationDepart.ficheBord.NumerosFB);
                    Phrase phraseTotalRecu = new Phrase(serviceGeneral.separateurDesMilles(montantTotalPassagers.ToString("0")) + "Ar");
                    phraseTotalRecu.Font.Size = 6;

                    tablePassagers.AddCell(phraseNombrePassager);
                    tablePassagers.AddCell("");
                    tablePassagers.AddCell("");
                    tablePassagers.AddCell("");
                    tablePassagers.AddCell(phrasePoidBagage);
                    tablePassagers.AddCell(phraseTotalPrixBillet);
                    tablePassagers.AddCell("");
                    tablePassagers.AddCell(phraseTotalBagage);
                    tablePassagers.AddCell(phraseTotalRecu);
                    #endregion

                    #region liste commission
                    Phrase phraseListeCommissions = new Phrase(listeCommission);
                    phraseListeCommissions.Font.Size = 8;
                    phraseListeCommissions.Font.SetStyle("underline");

                    iTextSharp.text.Table tableCommissions = new iTextSharp.text.Table(7, (2 + AutorisationDepart.ficheBord.commission.Count));
                    tableCommissions.Padding = 1;
                    tableCommissions.Width = 100;
                    tableCommissions.Widths = new float[] { 10, 20, 10, 20,20,10,10};

                    Phrase phraseType = new Phrase(type);
                    phraseType.Font.Size = 6;
                    Phrase phraseDesignation = new Phrase(designation);
                    phraseDesignation.Font.Size = 6;
                    Phrase phrasePoids = new Phrase(poids);
                    phrasePoids.Font.Size = 6;
                    Phrase phraseNomExpediteur = new Phrase(nomExpediteur);
                    phraseNomExpediteur.Font.Size = 6;
                    Phrase phraseNomRecepteur = new Phrase(nomRecepteur);
                    phraseNomRecepteur.Font.Size = 6;
                    Phrase phrasePieceJustificatif = new Phrase(pieceJustificatif);
                    phrasePieceJustificatif.Font.Size = 6;
                    Phrase phraseFrais = new Phrase(fraisEnvoi);
                    phraseFrais.Font.Size = 6;

                    tableCommissions.AddCell(phraseType);
                    tableCommissions.AddCell(phraseDesignation);
                    tableCommissions.AddCell(phrasePoids);
                    tableCommissions.AddCell(phraseNomExpediteur);
                    tableCommissions.AddCell(phraseNomRecepteur);
                    tableCommissions.AddCell(phrasePieceJustificatif);
                    tableCommissions.AddCell(phraseFrais);

                    Phrase phraseC1;
                    Phrase phraseC2;
                    Phrase phraseC3;
                    Phrase phraseC4;
                    Phrase phraseC5;
                    Phrase phraseC6;
                    Phrase phraseC7;

                    for (int i = 0; i < AutorisationDepart.ficheBord.commission.Count; i++)
                    {
                        phraseC1 = new Phrase(AutorisationDepart.ficheBord.commission[i].TypeCommission);
                        phraseC1.Font.Size = 6;

                        phraseC2 = new Phrase(AutorisationDepart.ficheBord.commission[i].designationCommission.Designation);
                        phraseC2.Font.Size = 6;

                        phraseC3 = new Phrase(AutorisationDepart.ficheBord.commission[i].Poids + "Kg");
                        phraseC3.Font.Size = 6;

                        phraseC4 = new Phrase(AutorisationDepart.ficheBord.commission[i].expediteur.NomClient + " " + AutorisationDepart.ficheBord.commission[i].expediteur.PrenomClient);
                        phraseC4.Font.Size = 6;

                        if (AutorisationDepart.ficheBord.commission[i].recepteur != null)
                        {
                            phraseC5 = new Phrase(AutorisationDepart.ficheBord.commission[i].recepteur.NomPersonne + " " + AutorisationDepart.ficheBord.commission[i].recepteur.PrenomPersonne);
                            phraseC5.Font.Size = 6;
                        }
                        else
                        {
                            phraseC5 = new Phrase("-");
                            phraseC5.Font.Size = 6;
                        }

                        phraseC6 = new Phrase(AutorisationDepart.ficheBord.commission[i].PieceJustificatif);
                        phraseC6.Font.Size = 6;

                        phraseC7 = new Phrase(serviceGeneral.separateurDesMilles(AutorisationDepart.ficheBord.commission[i].FraisEnvoi) + "Ar");
                        phraseC7.Font.Size = 6;

                        tableCommissions.AddCell(phraseC1);
                        tableCommissions.AddCell(phraseC2);
                        tableCommissions.AddCell(phraseC3);
                        tableCommissions.AddCell(phraseC4);
                        tableCommissions.AddCell(phraseC5);
                        tableCommissions.AddCell(phraseC6);
                        tableCommissions.AddCell(phraseC7);
                    }

                    Phrase phrasePoidsCommission = new Phrase(serviceFicheBord.getPoidTotalCommission(AutorisationDepart.ficheBord.NumerosFB).ToString("0") + "Kg");
                    phrasePoidsCommission.Font.Size = 6;
                    Phrase phraseTotalPrixCommission = new Phrase(serviceGeneral.separateurDesMilles(serviceFicheBord.getPrixTotalCommission(AutorisationDepart.ficheBord.NumerosFB).ToString("0")) + "Ar");
                    phraseTotalPrixCommission.Font.Size = 6;

                    tableCommissions.AddCell("");
                    tableCommissions.AddCell("");
                    tableCommissions.AddCell(phrasePoidsCommission);
                    tableCommissions.AddCell("");
                    tableCommissions.AddCell("");
                    tableCommissions.AddCell("");
                    tableCommissions.AddCell(phraseTotalPrixCommission);
                    #endregion

                    #region total recette
                    iTextSharp.text.Table tableTotal = new iTextSharp.text.Table(3, 1);
                    tableTotal.Padding = 1;
                    tableTotal.Width = 100;
                    tableTotal.Widths = new float[] {70,15,15};
                    tableTotal.BorderWidth = 0;

                    double montantRecettes = montantTotalPassagers + serviceFicheBord.getPrixTotalCommission(AutorisationDepart.ficheBord.NumerosFB);

                    Phrase phraseTotalRecettes = new Phrase("TOTAL RECETTES:");
                    phraseTotalRecettes.Font.Size = 8;
                    Phrase phraseTotalRecettesVar = new Phrase(serviceGeneral.separateurDesMilles(montantRecettes.ToString("0")) + "Ar");
                    phraseTotalRecettesVar.Font.Size = 8;

                    Cell cellVideComm = new Cell();
                    cellVideComm.BorderWidthLeft = 0;
                    cellVideComm.BorderWidthTop = 0;
                    cellVideComm.BorderWidthBottom = 0;

                    tableTotal.AddCell(cellVideComm);
                    tableTotal.AddCell(phraseTotalRecettes);
                    tableTotal.AddCell(phraseTotalRecettesVar);
                    #endregion

                    #region observation
                    Phrase phraseObservation = new Phrase(observation);
                    phraseObservation.Font.Size = 8;
                    phraseObservation.Font.SetStyle("underline");

                    iTextSharp.text.Table tableObservation = new iTextSharp.text.Table(8, 2);
                    tableObservation.Width = 100;
                    tableObservation.Widths = new float[] { 10,12,10,12,10,12,22,12};
                    tableObservation.BorderWidth = 0;
                    tableObservation.DefaultCellBorder = 0;

                    Phrase phraseDistance = new Phrase(distanceParcourir);
                    phraseDistance.Font.Size = 6;
                    phraseDistance.Font.SetStyle("underline");
                    Phrase phraseDistanceVar = new Phrase(distanceParcourirVar);
                    phraseDistanceVar.Font.Size = 6;

                    Phrase phraseDuree = new Phrase(dureTrajet);
                    phraseDuree.Font.Size = 6;
                    phraseDuree.Font.SetStyle("underline");
                    Phrase phraseDureeVar = new Phrase(dureTrajetVar);
                    phraseDureeVar.Font.Size = 6;

                    Phrase phraseNombreRepos = new Phrase(nombreRepos);
                    phraseNombreRepos.Font.Size = 6;
                    phraseNombreRepos.Font.SetStyle("underline");
                    Phrase phraseNombreReposVar = new Phrase(nombreReposVar);
                    phraseNombreReposVar.Font.Size = 6;

                    //Heure a l'arrive mbola tsy vita//
                    Phrase phraseHeureArrive = new Phrase(heureArrive);
                    phraseHeureArrive.Font.Size = 6;
                    phraseHeureArrive.Font.SetStyle("underline");
                    Phrase phraseHeureArriveVar = new Phrase(heureArriveVar);
                    phraseHeureArriveVar.Font.Size = 6;

                    Phrase phraseFraisVoyage = new Phrase(fraiVoyage);
                    phraseFraisVoyage.Font.Size = 6;
                    phraseFraisVoyage.Font.SetStyle("underline");
                    Phrase phraseFraisVoyageVar = new Phrase(fraiVoyageVar);
                    phraseFraisVoyageVar.Font.Size = 6;

                    Phrase phraseAcompteVoyage = new Phrase(acompteVoyage);
                    phraseAcompteVoyage.Font.Size = 6;
                    phraseAcompteVoyage.Font.SetStyle("underline");
                    Phrase phraseAcompteVoyageVar = new Phrase(acompteVoyageVar);
                    phraseAcompteVoyageVar.Font.Size = 6;

                    Phrase phraseResteRegler = new Phrase(resteRegler);
                    phraseResteRegler.Font.Size = 6;
                    phraseResteRegler.Font.SetStyle("underline");
                    Phrase phraseResteReglerVar = new Phrase(resteReglerVar);
                    phraseResteReglerVar.Font.Size = 6;

                    Cell cellDistance = new Cell();
                    cellDistance.AddElement(phraseDistance);
                    cellDistance.AddElement(phraseDistanceVar);
                    cellDistance.Colspan = 2;

                    Cell cellDuree = new Cell();
                    cellDuree.AddElement(phraseDuree);
                    cellDuree.AddElement(phraseDureeVar);
                    cellDuree.Colspan = 2;

                    Cell cellNombreRepos = new Cell();
                    cellNombreRepos.AddElement(phraseNombreRepos);
                    cellNombreRepos.AddElement(phraseNombreReposVar);
                    cellNombreRepos.Colspan = 2;

                    Cell cellHeureArrive = new Cell();
                    cellHeureArrive.AddElement(phraseHeureArrive);
                    cellHeureArrive.AddElement(phraseHeureArriveVar);
                    cellHeureArrive.Colspan = 2;

                    Cell cellFrais = new Cell();
                    cellFrais.AddElement(phraseFraisVoyage);
                    cellFrais.AddElement(phraseFraisVoyageVar);
                    cellFrais.Colspan = 3;

                    Cell cellAcompte = new Cell();
                    cellAcompte.AddElement(phraseAcompteVoyage);
                    cellAcompte.AddElement(phraseAcompteVoyageVar);
                    cellAcompte.Colspan = 3;

                    Cell cellReste = new Cell();
                    cellReste.AddElement(phraseResteRegler);
                    cellReste.AddElement(phraseResteReglerVar);
                    cellReste.Colspan = 2;

                    tableObservation.AddCell(cellDistance);
                    tableObservation.AddCell(cellDuree);
                    tableObservation.AddCell(cellNombreRepos);
                    tableObservation.AddCell(cellHeureArrive);
                    tableObservation.AddCell(cellFrais);
                    tableObservation.AddCell(cellAcompte);
                    tableObservation.AddCell(cellReste);
                    #endregion

                    #region escorte
                    Phrase phraseEscorte = new Phrase(listeEscorte);
                    phraseEscorte.Font.Size = 8;
                    phraseEscorte.Font.SetStyle("underline");

                    iTextSharp.text.Table tableEscorte = new iTextSharp.text.Table(4, 3);
                    tableEscorte.Padding = 1;
                    tableEscorte.Width = 100;
                    tableEscorte.Widths = new float[] { 25,10,25,40 };

                    Phrase phraseMatricule = new Phrase(matricule);
                    phraseMatricule.Font.Size = 6;
                    Phrase phraseTrajet = new Phrase(trajet);
                    phraseTrajet.Font.Size = 6;
                    Phrase phraseObservationEscort = new Phrase(observationAgentEscorte);
                    phraseObservationEscort.Font.Size = 6;

                    tableEscorte.AddCell(phraseNomPrenom);
                    tableEscorte.AddCell(phraseMatricule);
                    tableEscorte.AddCell(phraseTrajet);
                    tableEscorte.AddCell(phraseObservationEscort);

                    Phrase phraseVide = new Phrase("\n");
                    phraseVide.Font.Size = 6;

                    tableEscorte.AddCell(phraseVide);
                    tableEscorte.AddCell("");
                    tableEscorte.AddCell("");
                    tableEscorte.AddCell("");

                    tableEscorte.AddCell(phraseVide);
                    tableEscorte.AddCell("");
                    tableEscorte.AddCell("");
                    tableEscorte.AddCell("");

                    #endregion

                    #region visas
                    Phrase phraseVisas = new Phrase(visas);
                    phraseVisas.Font.Size = 8;
                    phraseVisas.Font.SetStyle("underline");

                    iTextSharp.text.Table tableVisas = new iTextSharp.text.Table(7, 3);
                    tableVisas.Padding = 1;
                    tableVisas.Width = 100;
                    tableVisas.Widths = new float[] { 30,15,10,10,10,10,15};

                    Phrase phraseChefGare = new Phrase(chefGare);
                    phraseChefGare.Font.Size = 6;
                    Phrase phraseChauffeurSignature = new Phrase(chauffeurSignature);
                    phraseChauffeurSignature.Font.Size = 6;
                    Phrase phraseArret1 = new Phrase(arret1);
                    phraseArret1.Font.Size = 6;
                    Phrase phraseHeurePassage1 = new Phrase(heurePassage1);
                    phraseHeurePassage1.Font.Size = 6;
                    Phrase phraseHeureDepart1 = new Phrase(heureDepart1);
                    phraseHeureDepart1.Font.Size = 6;
                    Phrase phraseArret2 = new Phrase(arret2);
                    phraseArret2.Font.Size = 6;
                    Phrase phraseHeurePassage2 = new Phrase(heurePassage2);
                    phraseHeurePassage2.Font.Size = 6;
                    Phrase phraseHeureDepart2 = new Phrase(heureDepart2);
                    phraseHeureDepart2.Font.Size = 6;
                    Phrase phraseVisasArrive = new Phrase(visasArrive);
                    phraseVisasArrive.Font.Size = 6;

                    Cell cellChefGare = new Cell();
                    cellChefGare.Add(phraseChefGare);
                    cellChefGare.Rowspan = 2;

                    Cell cellChauffeurSignature = new Cell();
                    cellChauffeurSignature.Add(phraseChauffeurSignature);
                    cellChauffeurSignature.Rowspan = 2;

                    Cell cellArret1 = new Cell();
                    cellArret1.Add(phraseArret1);
                    cellArret1.Colspan = 2;
                    Cell cellHeurePassage1 = new Cell();
                    cellHeurePassage1.Add(phraseHeurePassage1);
                    Cell cellHeureDepart1 = new Cell();
                    cellHeureDepart1.Add(phraseHeureDepart1);

                    Cell cellArret2 = new Cell();
                    cellArret2.Add(phraseArret2);
                    cellArret2.Colspan = 2;
                    Cell cellHeurePassage2 = new Cell();
                    cellHeurePassage2.Add(phraseHeurePassage2);
                    Cell cellHeureDepart2 = new Cell();
                    cellHeureDepart2.Add(phraseHeureDepart2);

                    Cell cellVisasArrive = new Cell();
                    cellVisasArrive.Add(phraseVisasArrive);
                    cellVisasArrive.Rowspan = 2;

                    tableVisas.AddCell(cellChefGare);
                    tableVisas.AddCell(cellChauffeurSignature);
                    tableVisas.AddCell(cellArret1);
                    tableVisas.AddCell(cellArret2);
                    tableVisas.AddCell(cellVisasArrive);

                    tableVisas.AddCell(cellHeurePassage1);
                    tableVisas.AddCell(cellHeureDepart1);
                    tableVisas.AddCell(cellHeurePassage2);
                    tableVisas.AddCell(cellHeureDepart2);

                    tableVisas.AddCell("\n\n\n");
                    tableVisas.AddCell("\n\n\n");
                    tableVisas.AddCell("\n\n\n");
                    tableVisas.AddCell("\n\n\n");
                    tableVisas.AddCell("\n\n\n");
                    tableVisas.AddCell("\n\n\n");
                    tableVisas.AddCell("\n\n\n");
                    #endregion

                    #region ajout des element dans document
                    document.Add(tableLogo);
                    document.Add(paraInfo);
                    document.Add(phraseAgent);
                    document.Add(phraseAgentVar);
                    document.Add(tableAV);
                    document.Add(phraseListePassager);
                    document.Add(tablePassagers);
                    document.Add(phraseListeCommissions);
                    document.Add(tableCommissions);
                    document.Add(tableTotal);
                    document.Add(phraseObservation);
                    document.Add(tableObservation);
                    document.Add(phraseEscorte);
                    document.Add(tableEscorte);
                    document.Add(phraseVisas);
                    document.Add(tableVisas);

                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printFacture(string numFacture, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;

            serviceFacture = new ImplDalFacture();
            serviceProprietaire = new ImplDalProprietaire();
            serviceRecuAD = new ImplDalRecuAD();
            serviceGeneral = new ImplDalGeneral();
            List<crlRecuAD> recuADs = null;
            //serviceFicheBord = new ImplDalFicheBord();

            double montantRecette = 0.00;
            double montantRecu = 0.00;
            //double montantReste = 0.00;
            Convertisseuse convertisseuse = new Convertisseuse();


            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (numFacture != "")
            {
                Facture = serviceFacture.selectFacture(numFacture);
                if (Facture != null)
                {
                    if (Facture.autorisationDeparts != null)
                    {
                        #region initialise pdf
                        iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                        string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                        string numFactureStr = "FACTURE N°";
                        string numFactureVar = numFacture;

                        string proprietaire = "Transporteur:";
                        string proprietaireVar = "";

                        string vehicule = "Véhicule:";
                        string vehiculeVar = "";

                        if (Facture.autorisationDeparts != null)
                        {
                            if (Facture.autorisationDeparts.Count > 0)
                            {
                                if (Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.Individu != null)
                                {
                                    proprietaireVar = Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.Individu.CiviliteIndividu + " " +
                                                            Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.Individu.NomIndividu + " " +
                                                            Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.Individu.PrenomIndividu + " /" +
                                                            Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.Individu.CinIndividu;

                                    vehiculeVar = Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule + " " +
                                                         Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule + " " +
                                                         Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule;

                                    //mbola ts ampy io facture io ra sendra misy facture efa vo paié nefa mbola ts zero ny AD
                                }
                                else if (Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.organisme != null)
                                {
                                    proprietaireVar = Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.organisme.NomOrganisme;
                                }
                                else if (Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.societe != null)
                                {
                                    proprietaireVar = proprietaireVar = Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.societe.NomSociete;
                                }

                                montantRecette = serviceFacture.getMontantRecette(Facture.NumFacture);

                                montantRecu = serviceFacture.getMontantRecu(Facture.NumFacture);

                                //montantReste = serviceProprietaire.getTotalReste(Facture.autorisationDeparts[0].ficheBord.autorisationVoyage.Verification.Licence.vehicule.proprietaire.NumProprietaire);
                                recuADs = serviceRecuAD.selectRecuADFacture(Facture.NumFacture);
                            }
                        }

                        string dateFacture = "Date:";
                        string dateFactureVar = Facture.DateFacturation.ToString("dd MMMM yyyy");

                        string agent = "Agent:";
                        string agentVar = Facture.agent.nomAgent + " " + Facture.agent.prenomAgent;

                        string recette = "Recette brut";
                        string recetteVar = serviceGeneral.separateurDesMilles(montantRecette.ToString("0")) + "Ar";

                        string recu = "Reçu";


                        string recuVar = serviceGeneral.separateurDesMilles(montantRecu.ToString("0")) + "Ar";

                        string designation = "Libellé";
                        string reference = "Référence";
                        string detail = "Détail";
                        string prixTotal = "Prix total";
                        string montantTotal = "Montant total";
                        string montantTotalVar = serviceGeneral.separateurDesMilles((montantRecette - montantRecu).ToString("0")) + "Ar";
                        string montantTotalLettre = "Montant total en lettre: ";
                        string montantTotalLettreVar = convertisseuse.convertion((montantRecette - montantRecu).ToString("0")) + " Ariary\n\n";

                        string visas = "VISAS:";

                        string chefGare = "Chef de gare";

                        string caisse = "Caisse";

                        Phrase phraseAlaligne = new Phrase("\n");

                        #endregion

                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                        document.Open();
                        PdfContentByte cb = writer.DirectContent;

                        #region cree pdf

                        #region logo
                        iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                        tableLogo.Border = 0;
                        tableLogo.DefaultCellBorder = 0;
                        tableLogo.Padding = 2;
                        tableLogo.Width = 100;
                        tableLogo.Widths = new float[] { 15, 85 };

                        Phrase paraGranTitre = new Phrase(grandTitre);
                        paraGranTitre.Font.Size = 16;
                        paraGranTitre.Font.SetStyle(1);


                        Phrase paraNumFB = new Phrase(numFactureStr);
                        Phrase paraNumFBVar = new Phrase(numFactureVar);
                        paraNumFB.Font.Size = 14;
                        paraNumFB.Font.SetStyle(1);
                        paraNumFBVar.Font.Size = 14;

                        Cell cellImg = new Cell();
                        cellImg.AddElement(imageLogo);
                        cellImg.Rowspan = 2;
                        Cell cellGTitre = new Cell();
                        cellGTitre.AddElement(paraGranTitre);
                        cellGTitre.BorderWidthBottom = 1;

                        Cell cellTitre = new Cell();
                        cellTitre.AddElement(paraNumFB);
                        cellTitre.AddElement(paraNumFBVar);


                        tableLogo.AddCell(cellImg);
                        tableLogo.AddCell(cellGTitre);
                        tableLogo.AddCell(cellTitre);

                        #endregion

                        #region tableau transporteur
                        iTextSharp.text.Table tableTranporteur = new iTextSharp.text.Table(2, 5);
                        tableTranporteur.Border = 0;
                        tableTranporteur.DefaultCellBorder = 0;
                        tableTranporteur.Padding = 2;
                        tableTranporteur.Width = 100;
                        tableTranporteur.Widths = new float[] { 15, 85 };

                        Phrase phraseProprietaire = new Phrase(proprietaire);
                        Phrase phraseProprietaireVar = new Phrase(proprietaireVar);
                        phraseProprietaire.Font.Size = 8;
                        phraseProprietaireVar.Font.Size = 8;
                        phraseProprietaireVar.Font.SetStyle(1);

                        Phrase phraseVehicule = new Phrase(vehicule);
                        Phrase phraseVehiculeVar = new Phrase(vehiculeVar);
                        phraseVehicule.Font.Size = 8;
                        phraseVehiculeVar.Font.Size = 8;
                        phraseVehiculeVar.Font.SetStyle(1);

                        Phrase phraseDateFacture = new Phrase(dateFacture);
                        Phrase phraseDateFactureVar = new Phrase(dateFactureVar);
                        phraseDateFacture.Font.Size = 8;
                        phraseDateFactureVar.Font.Size = 8;
                        phraseDateFactureVar.Font.SetStyle(1);

                        Phrase phraseNumFac = new Phrase(numFactureStr);
                        Phrase phraseNumFacVar = new Phrase(numFactureVar);
                        phraseNumFac.Font.Size = 8;
                        phraseNumFacVar.Font.Size = 8;
                        phraseNumFacVar.Font.SetStyle(1);

                        Phrase phraseAgent = new Phrase(agent);
                        Phrase phraseAgentVar = new Phrase(agentVar);
                        phraseAgent.Font.Size = 8;
                        phraseAgentVar.Font.Size = 8;
                        phraseAgentVar.Font.SetStyle(1);

                        tableTranporteur.AddCell(phraseProprietaire);
                        tableTranporteur.AddCell(phraseProprietaireVar);
                        tableTranporteur.AddCell(phraseVehicule);
                        tableTranporteur.AddCell(phraseVehiculeVar);
                        tableTranporteur.AddCell(phraseDateFacture);
                        tableTranporteur.AddCell(phraseDateFactureVar);
                        tableTranporteur.AddCell(phraseNumFac);
                        tableTranporteur.AddCell(phraseNumFacVar);
                        tableTranporteur.AddCell(phraseAgent);
                        tableTranporteur.AddCell(phraseAgentVar);

                        #endregion

                        #region tableau detail

                        iTextSharp.text.Table tableDetail = new iTextSharp.text.Table(4, (Facture.autorisationDeparts.Count + recuADs.Count + 2));
                        tableDetail.Padding = 2;
                        tableDetail.Width = 100;
                        tableDetail.Widths = new float[] { 15, 20, 50, 15 };

                        Phrase phraseRecette = new Phrase(recette);
                        Phrase phraseRecetteVar = new Phrase(recetteVar);
                        phraseRecette.Font.Size = 8;
                        phraseRecetteVar.Font.Size = 8;

                        Phrase phraseRecu = new Phrase(recu);
                        Phrase phraseRecuVar = new Phrase(recuVar);
                        phraseRecu.Font.Size = 8;
                        phraseRecuVar.Font.Size = 8;

                        Phrase phraseMontantTotal = new Phrase(montantTotal);
                        Phrase phraseMontantTotalVar = new Phrase(montantTotalVar);
                        phraseMontantTotal.Font.Size = 8;
                        phraseMontantTotalVar.Font.Size = 8;

                        Phrase phraseDesignation = new Phrase(designation);
                        phraseDesignation.Font.Size = 8;
                        phraseDesignation.Font.SetStyle(1);

                        Phrase phraseReference = new Phrase(reference);
                        phraseReference.Font.Size = 8;
                        phraseReference.Font.SetStyle(1);

                        Phrase phraseDetail = new Phrase(detail);
                        phraseDetail.Font.Size = 8;
                        phraseDetail.Font.SetStyle(1);

                        Phrase phrasePrixTotal = new Phrase(prixTotal);
                        phrasePrixTotal.Font.Size = 8;
                        phrasePrixTotal.Font.SetStyle(1);



                        Cell cellerecette = new Cell();
                        cellerecette.Rowspan = Facture.autorisationDeparts.Count;
                        cellerecette.AddElement(phraseRecette);

                        tableDetail.AddCell(phraseDesignation);
                        tableDetail.AddCell(phraseReference);
                        tableDetail.AddCell(phraseDetail);
                        tableDetail.AddCell(phrasePrixTotal);
                        tableDetail.AddCell(cellerecette);

                        //Cell cellDetailFacture = new Cell();
                        Phrase phraseTemp = null;
                        Phrase phraseReferenceTemp = null;
                        Phrase phrasePrixTotalTemp = null;

                        if (Facture.autorisationDeparts != null)
                        {

                            for (int i = 0; i < Facture.autorisationDeparts.Count; i++)
                            {
                                phraseReferenceTemp = new Phrase(Facture.autorisationDeparts[i].ficheBord.NumerosFB);
                                phraseTemp = new Phrase("Voyage du " + Facture.autorisationDeparts[i].ficheBord.DateHeurDepart.ToString("dd MMMM yyyy HH:mm") + " Trajet: " + Facture.autorisationDeparts[i].ficheBord.autorisationVoyage.Verification.Itineraire.villeD.NomVille + "-" + Facture.autorisationDeparts[i].ficheBord.autorisationVoyage.Verification.Itineraire.villeF.NomVille);
                                phrasePrixTotalTemp = new Phrase(serviceGeneral.separateurDesMilles(Facture.autorisationDeparts[i].RecetteTotale.ToString("0")) + "Ar");
                                phraseTemp.Font.Size = 6;
                                phraseReferenceTemp.Font.Size = 6;
                                phrasePrixTotalTemp.Font.Size = 6;

                                tableDetail.AddCell(phraseReferenceTemp);
                                tableDetail.AddCell(phraseTemp);
                                tableDetail.AddCell(phrasePrixTotalTemp);

                                //cellDetailFacture.AddElement(phraseTemp);
                            }
                        }

                        Cell celleRecu = new Cell();
                        celleRecu.Rowspan = recuADs.Count;
                        celleRecu.AddElement(phraseRecu);

                        tableDetail.AddCell(celleRecu);

                        //Cell cellDetailRecu = new Cell();


                        if (recuADs != null)
                        {
                            for (int i = 0; i < recuADs.Count; i++)
                            {
                                phraseReferenceTemp = new Phrase(recuADs[i].NumRecuAD);
                                phraseTemp = new Phrase(recuADs[i].Libele + " du " + recuADs[i].Date.ToString("dd MMMM yyyy"));
                                phrasePrixTotalTemp = new Phrase(serviceGeneral.separateurDesMilles(recuADs[i].Montant) + "Ar");
                                phraseTemp.Font.Size = 6;
                                phraseReferenceTemp.Font.Size = 6;
                                phrasePrixTotalTemp.Font.Size = 6;

                                tableDetail.AddCell(phraseReferenceTemp);
                                tableDetail.AddCell(phraseTemp);
                                tableDetail.AddCell(phrasePrixTotalTemp);

                            }
                        }

                        Cell cellMontantTotal = new Cell();
                        cellMontantTotal.AddElement(phraseMontantTotal);
                        cellMontantTotal.Colspan = 3;


                        //tableDetail.AddCell(cellDetailFacture);
                        //tableDetail.AddCell(phraseRecetteVar);
                        //tableDetail.AddCell(phraseRecu);
                        //tableDetail.AddCell(cellDetailRecu);
                        //tableDetail.AddCell(phraseRecuVar);

                        tableDetail.AddCell(cellMontantTotal);
                        tableDetail.AddCell(phraseMontantTotalVar);


                        Phrase phraseMontantLettre = new Phrase(montantTotalLettre);
                        phraseMontantLettre.Font.Size = 8;
                        Phrase phraseMontantLettreVar = new Phrase(montantTotalLettreVar);
                        phraseMontantLettreVar.Font.Size = 12;
                        phraseMontantLettreVar.Font.SetStyle(2);
                        #endregion

                        #region tableau Visa
                        iTextSharp.text.Table tableVisa = new iTextSharp.text.Table(3, 2);
                        tableVisa.Padding = 2;
                        tableVisa.Width = 100;
                        tableVisa.Widths = new float[] { 33, 33, 34 };

                        Phrase phraseVisas = new Phrase(visas);
                        phraseVisas.Font.Size = 8;
                        phraseVisas.Font.SetStyle("underline");

                        Phrase phraseChefGare = new Phrase(chefGare);
                        phraseChefGare.Font.Size = 8;

                        Phrase phraseCaisse = new Phrase(caisse);
                        phraseCaisse.Font.Size = 8;

                        Phrase phraseTrans = new Phrase(proprietaire);
                        phraseTrans.Font.Size = 8;

                        tableVisa.AddCell(phraseChefGare);
                        tableVisa.AddCell(phraseCaisse);
                        tableVisa.AddCell(phraseTrans);
                        tableVisa.AddCell("\n\n\n\n");
                        tableVisa.AddCell("\n\n\n\n");
                        tableVisa.AddCell("\n\n\n\n");
                        #endregion

                        #region ajout des element dans document
                        document.Add(tableLogo);
                        document.Add(tableTranporteur);
                        document.Add(phraseAlaligne);
                        document.Add(tableDetail);
                        document.Add(phraseMontantLettre);
                        document.Add(phraseMontantLettreVar);
                        document.Add(phraseVisas);
                        document.Add(tableVisa);

                        isPrint = true;
                        #endregion

                        #endregion

                        document.Close();
                    }
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printProforma(string numProforma, string urlSaving)
        {
            #region initialisation
            double montantTotalD = 0;
            bool isPrint = false;
            crlProforma proforma = null;

            IntfDalProforma serviceProforma = new ImplDalProforma();
            serviceGeneral = new ImplDalGeneral();

            Convertisseuse convertisseuse = new Convertisseuse();

            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (numProforma != "")
            {
                proforma = serviceProforma.selectProforma(numProforma);
                if (proforma != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numProformaStr = "PROFORMA N°";
                    string numProformaVar = numProforma;

                    string nomSociete = "Société:";
                    string nomSocieteVar = "";
                    string adresseSociete = "Adresse:";
                    string adresseSocieteVar = "";
                    string mailSociete = "Mail:";
                    string mailSocieteVar = "";
                    string telephoneSociete = "Téléphone:";
                    string telephoneSocieteVar = "";
                    string responsableSociete = "Responsable:";
                    string responsableSocieteVar = "";
                    string adresseResponsableSociete = "Adresse:";
                    string adresseResponsableSocieteVar = "";
                    string telephoneResponsableSociete = "Téléphone:";
                    string telephoneResponsableSocieteVar = "";

                    string nomOrganisme = "Organisme:";
                    string nomOrganismeVar = "";
                    string adresseOrganisme = "Adresse:";
                    string adresseOrganismeVar = "";
                    string mailOrganisme = "Mail:";
                    string mailOrganismeVar = "";
                    string telephoneOrganisme = "Téléphone:";
                    string telephoneOrganismeVar = "";
                    string responsableOrganisme = "Responsable:";
                    string responsableOrganismeVar = "";
                    string adresseResponsableOrganisme = "Adresse:";
                    string adresseResponsableOrganismeVar = "";
                    string telephoneResponsableOrganisme = "Téléphone:";
                    string telephoneResponsableOrganismeVar = "";

                    string nomClient = "Client:";
                    string nomClientVar = "";
                    string adresseClient = "Adresse:";
                    string adresseClientVar = "";
                    string telephoneClient = "Téléphone:";
                    string telephoneClientVar = "";

                    if (proforma.individu != null)
                    {
                        nomClientVar = proforma.individu.PrenomIndividu + " " + proforma.individu.NomIndividu;
                        adresseClientVar = proforma.individu.Adresse;
                        telephoneClientVar = proforma.individu.TelephoneFixeIndividu + " " + proforma.individu.TelephoneMobileIndividu;
                    }
                    if (proforma.organisme != null)
                    {
                        nomOrganismeVar = proforma.organisme.NomOrganisme;
                        adresseOrganismeVar = proforma.organisme.AdresseOrganisme;
                        mailOrganismeVar = proforma.organisme.MailOrganisme;
                        telephoneOrganismeVar = proforma.organisme.TelephoneFixeOrganisme + " " + proforma.organisme.TelephoneMobileOrganisme;
                        if (proforma.organisme.individuResponsable != null)
                        {
                            responsableOrganismeVar = proforma.organisme.individuResponsable.PrenomIndividu + " " + proforma.organisme.individuResponsable.NomIndividu;
                            adresseResponsableOrganismeVar = proforma.organisme.individuResponsable.Adresse;
                            telephoneResponsableOrganismeVar = proforma.organisme.individuResponsable.TelephoneFixeIndividu + " " + proforma.organisme.individuResponsable.TelephoneMobileIndividu;
                        }
                        
                    }
                    if (proforma.societe != null)
                    {
                        nomSocieteVar = proforma.societe.NomSociete;
                        adresseSocieteVar = proforma.societe.AdresseSociete;
                        mailSocieteVar = proforma.societe.MailSociete;
                        telephoneSocieteVar = proforma.societe.TelephoneFixeSociete + " " + proforma.societe.TelephoneMobileSociete;
                        if (proforma.societe.individuResponsable != null)
                        {
                            responsableSocieteVar = proforma.societe.individuResponsable.PrenomIndividu + " " + proforma.societe.individuResponsable.NomIndividu;
                            adresseResponsableSocieteVar = proforma.societe.individuResponsable.Adresse;
                            telephoneResponsableSocieteVar = proforma.societe.individuResponsable.TelephoneFixeIndividu + " " + proforma.societe.individuResponsable.TelephoneMobileIndividu;
                        }
                    }

                    montantTotalD += serviceProforma.getPrixTotalBilletCommandeProforma(proforma.NumProforma);
                    montantTotalD += serviceProforma.getPrixTotalCommissionDevisProforma(proforma.NumProforma);
                    montantTotalD += serviceProforma.getPrixTotalDureeAbonnementProforma(proforma.NumProforma);
                    montantTotalD += serviceProforma.getPrixTotalVoyageAbonnementProforma(proforma.NumProforma);
                    montantTotalD += serviceProforma.getPrixTotalAbonnementUSCarteProforma(proforma.NumProforma);
                    montantTotalD += serviceProforma.getPrixTotalAbonnementUSNVProforma(proforma.NumProforma);

                    string designation = "Référence";
                    string detail = "Détail";
                    string prixTotal = "Montant";
                    string montantTotal = "Montant total";
                    string montantTotalVar = serviceGeneral.separateurDesMilles(montantTotalD.ToString("0")) + " Ar";

                    string tempDetailBilletCommande = "";
                    string tempDetailCommissionDevis = "";
                    string tempDetailVoyageAbonnement = "";
                    string tempDetailDureeAbonnement = "";
                    string tempDetailUSAbonnementNV = "";

                    Phrase phraseVide = new Phrase("");
                    Phrase phraseAlaLigne = new Phrase("\n");
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region creePdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numProformaStr);
                    Phrase paraNumFBVar = new Phrase(numProformaVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region tableau Client,Societe,Organisme
                    iTextSharp.text.Table tableClient = new iTextSharp.text.Table(2, 3);
                    tableClient.Border = 0;
                    tableClient.DefaultCellBorder = 0;
                    tableClient.Padding = 2;
                    tableClient.Width = 100;
                    tableClient.Widths = new float[] { 15, 85 };

                    iTextSharp.text.Table tableSociete = new iTextSharp.text.Table(5, 4);
                    tableSociete.Border = 0;
                    tableSociete.DefaultCellBorder = 0;
                    tableSociete.Padding = 2;
                    tableSociete.Width = 100;
                    tableSociete.Widths = new float[] { 15, 33, 4,15, 33};

                    iTextSharp.text.Table tableOrganisme = new iTextSharp.text.Table(5, 4);
                    tableOrganisme.Border = 0;
                    tableOrganisme.DefaultCellBorder = 0;
                    tableOrganisme.Padding = 2;
                    tableOrganisme.Width = 100;
                    tableOrganisme.Widths = new float[] { 15, 33, 4, 15, 33 };

                    Phrase phraseNomClient = new Phrase(nomClient);
                    Phrase phraseNomClientVar = new Phrase(nomClientVar);
                    phraseNomClient.Font.Size = 8;
                    phraseNomClientVar.Font.Size = 8;
                    phraseNomClientVar.Font.SetStyle(1);

                    Phrase phraseAdresseClient = new Phrase(adresseClient);
                    Phrase phraseAdresseClientVar = new Phrase(adresseClientVar);
                    phraseAdresseClient.Font.Size = 8;
                    phraseAdresseClientVar.Font.Size = 8;
                    phraseAdresseClientVar.Font.SetStyle(1);

                    Phrase phraseTelephoneClient = new Phrase(telephoneClient);
                    Phrase phraseTelephoneClientVar = new Phrase(telephoneClientVar);
                    phraseTelephoneClient.Font.Size = 8;
                    phraseTelephoneClientVar.Font.Size = 8;
                    phraseTelephoneClientVar.Font.SetStyle(1);

                    tableClient.AddCell(phraseNomClient);
                    tableClient.AddCell(phraseNomClientVar);
                    tableClient.AddCell(phraseAdresseClient);
                    tableClient.AddCell(phraseAdresseClientVar);
                    tableClient.AddCell(phraseTelephoneClient);
                    tableClient.AddCell(phraseTelephoneClientVar);

                    Phrase phraseNomSociete = new Phrase(nomSociete);
                    Phrase phraseNomSocieteVar = new Phrase(nomSocieteVar);
                    phraseNomSociete.Font.Size = 8;
                    phraseNomSocieteVar.Font.Size = 8;
                    phraseNomSocieteVar.Font.SetStyle(1);

                    Phrase phraseAdresseSociete = new Phrase(adresseSociete);
                    Phrase phraseAdresseSocieteVar = new Phrase(adresseSocieteVar);
                    phraseAdresseSociete.Font.Size = 8;
                    phraseAdresseSocieteVar.Font.Size = 8;
                    phraseAdresseSocieteVar.Font.SetStyle(1);

                    Phrase phraseMailSociete = new Phrase(mailSociete);
                    Phrase phraseMailSocieteVar = new Phrase(mailSocieteVar);
                    phraseMailSociete.Font.Size = 8;
                    phraseMailSocieteVar.Font.Size = 8;
                    phraseMailSocieteVar.Font.SetStyle(1);

                    Phrase phraseTelephoneSociete = new Phrase(telephoneSociete);
                    Phrase phraseTelephoneSocieteVar = new Phrase(telephoneSocieteVar);
                    phraseTelephoneSociete.Font.Size = 8;
                    phraseTelephoneSocieteVar.Font.Size = 8;
                    phraseTelephoneSocieteVar.Font.SetStyle(1);

                    Phrase phraseResponsableSociete = new Phrase(responsableSociete);
                    Phrase phraseResponsableSocieteVar = new Phrase(responsableSocieteVar);
                    phraseResponsableSociete.Font.Size = 8;
                    phraseResponsableSocieteVar.Font.Size = 8;
                    phraseResponsableSocieteVar.Font.SetStyle(1);

                    Phrase phraseAdresseResponsableSociete = new Phrase(adresseResponsableSociete);
                    Phrase phraseAdresseResponsableSocieteVar = new Phrase(adresseResponsableSocieteVar);
                    phraseAdresseResponsableSociete.Font.Size = 8;
                    phraseAdresseResponsableSocieteVar.Font.Size = 8;
                    phraseAdresseResponsableSocieteVar.Font.SetStyle(1);

                    Phrase phraseTelephoneResponsableSociete = new Phrase(telephoneResponsableSociete);
                    Phrase phraseTelephoneResponsableSocieteVar = new Phrase(telephoneResponsableSocieteVar);
                    phraseTelephoneResponsableSociete.Font.Size = 8;
                    phraseTelephoneResponsableSocieteVar.Font.Size = 8;
                    phraseTelephoneResponsableSocieteVar.Font.SetStyle(1);

                    tableSociete.AddCell(phraseNomSociete);
                    tableSociete.AddCell(phraseNomSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseResponsableSociete);
                    tableSociete.AddCell(phraseResponsableSocieteVar);
                    tableSociete.AddCell(phraseAdresseSociete);
                    tableSociete.AddCell(phraseAdresseSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseAdresseResponsableSociete);
                    tableSociete.AddCell(phraseAdresseResponsableSocieteVar);
                    tableSociete.AddCell(phraseTelephoneSociete);
                    tableSociete.AddCell(phraseTelephoneSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseTelephoneResponsableSociete);
                    tableSociete.AddCell(phraseTelephoneResponsableSocieteVar);
                    tableSociete.AddCell(phraseMailSociete);
                    tableSociete.AddCell(phraseMailSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseVide);

                    Phrase phraseNomOrganisme = new Phrase(nomOrganisme);
                    Phrase phraseNomOrganismeVar = new Phrase(nomOrganismeVar);
                    phraseNomOrganisme.Font.Size = 8;
                    phraseNomOrganismeVar.Font.Size = 8;
                    phraseNomOrganismeVar.Font.SetStyle(1);

                    Phrase phraseAdresseOrganisme = new Phrase(adresseOrganisme);
                    Phrase phraseAdresseOrganismeVar = new Phrase(adresseOrganismeVar);
                    phraseAdresseOrganisme.Font.Size = 8;
                    phraseAdresseOrganismeVar.Font.Size = 8;
                    phraseAdresseOrganismeVar.Font.SetStyle(1);

                    Phrase phraseMailOrganisme = new Phrase(mailOrganisme);
                    Phrase phraseMailOrganismeVar = new Phrase(mailOrganismeVar);
                    phraseMailOrganisme.Font.Size = 8;
                    phraseMailOrganismeVar.Font.Size = 8;
                    phraseMailOrganismeVar.Font.SetStyle(1);

                    Phrase phraseTelephoneOrganisme = new Phrase(telephoneOrganisme);
                    Phrase phraseTelephoneOrganismeVar = new Phrase(telephoneOrganismeVar);
                    phraseTelephoneOrganisme.Font.Size = 8;
                    phraseTelephoneOrganismeVar.Font.Size = 8;
                    phraseTelephoneOrganismeVar.Font.SetStyle(1);

                    Phrase phraseResponsableOrganisme = new Phrase(responsableOrganisme);
                    Phrase phraseResponsableOrganismeVar = new Phrase(responsableOrganismeVar);
                    phraseResponsableOrganisme.Font.Size = 8;
                    phraseResponsableOrganismeVar.Font.Size = 8;
                    phraseResponsableOrganismeVar.Font.SetStyle(1);

                    Phrase phraseAdresseResponsableOrganisme = new Phrase(adresseResponsableOrganisme);
                    Phrase phraseAdresseResponsableOrganismeVar = new Phrase(adresseResponsableOrganismeVar);
                    phraseAdresseResponsableOrganisme.Font.Size = 8;
                    phraseAdresseResponsableOrganismeVar.Font.Size = 8;
                    phraseAdresseResponsableOrganismeVar.Font.SetStyle(1);

                    Phrase phraseTelephoneResponsableOrganisme = new Phrase(telephoneResponsableOrganisme);
                    Phrase phraseTelephoneResponsableOrganismeVar = new Phrase(telephoneResponsableOrganismeVar);
                    phraseTelephoneResponsableOrganisme.Font.Size = 8;
                    phraseTelephoneResponsableOrganismeVar.Font.Size = 8;
                    phraseTelephoneResponsableOrganismeVar.Font.SetStyle(1);

                    tableOrganisme.AddCell(phraseNomOrganisme);
                    tableOrganisme.AddCell(phraseNomOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseResponsableOrganisme);
                    tableOrganisme.AddCell(phraseResponsableOrganismeVar);
                    tableOrganisme.AddCell(phraseAdresseOrganisme);
                    tableOrganisme.AddCell(phraseAdresseOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseAdresseResponsableOrganisme);
                    tableOrganisme.AddCell(phraseAdresseResponsableOrganismeVar);
                    tableOrganisme.AddCell(phraseTelephoneOrganisme);
                    tableOrganisme.AddCell(phraseTelephoneOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseTelephoneResponsableOrganisme);
                    tableOrganisme.AddCell(phraseTelephoneResponsableOrganismeVar);
                    tableOrganisme.AddCell(phraseMailOrganisme);
                    tableOrganisme.AddCell(phraseMailOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseVide);

                    #endregion

                    #region tableau commande
                    int nombreLigne = 0;
                    if (proforma.billetCommande != null)
                    {
                        nombreLigne += proforma.billetCommande.Count;
                    }
                    if (proforma.commissionDevis != null)
                    {
                        nombreLigne += proforma.commissionDevis.Count;
                    }
                    if (proforma.dureeAbonnementDevis != null)
                    {
                        nombreLigne += proforma.dureeAbonnementDevis.Count;
                    }
                    if (proforma.voyageAbonnementDevis != null)
                    {
                        nombreLigne += proforma.voyageAbonnementDevis.Count;
                    }
                    if (proforma.uSAbonnementNVDevis != null)
                    {
                        nombreLigne += proforma.uSAbonnementNVDevis.Count;
                    }

                    iTextSharp.text.Table tableCommande = new iTextSharp.text.Table(3, nombreLigne + 1);
                    //tableCommande.Border = 1;
                    //tableCommande.DefaultCellBorder = 1;
                    tableCommande.Padding = 1;
                    tableCommande.Width = 100;
                    tableCommande.Widths = new float[] { 20, 60,20 };

                    Phrase phraseDesigantion = new Phrase(designation);
                    phraseDesigantion.Font.Size = 8;
                    phraseDesigantion.Font.SetStyle(1);

                    Phrase phraseDetaile = new Phrase(detail);
                    phraseDetaile.Font.Size = 8;
                    phraseDetaile.Font.SetStyle(1);

                    Phrase phrasePrix = new Phrase(prixTotal);
                    phrasePrix.Font.Size = 8;
                    phrasePrix.Font.SetStyle(1);

                    tableCommande.AddCell(phraseDesigantion);
                    tableCommande.AddCell(phraseDetaile);
                    tableCommande.AddCell(phrasePrix);

                    Phrase phraseTempDesignation = null;
                    Phrase phraseTempDetail = null;
                    Phrase phraseTempPrix = null;

                    if (proforma.billetCommande != null)
                    {
                        for (int i = 0; i < proforma.billetCommande.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(proforma.billetCommande[i].NumBilletCommande);
                            phraseTempDesignation.Font.Size = 8;

                            if (proforma.billetCommande[i].trajet != null)
                            {
                                tempDetailBilletCommande = proforma.billetCommande[i].NombreBilletCommande + " billet(s) pour " + proforma.billetCommande[i].trajet.villeD.NomVille + "-" + proforma.billetCommande[i].trajet.villeF.NomVille;
                            }
                            if (proforma.billetCommande[i].calculCategorieBillet != null)
                            {
                                tempDetailBilletCommande += " Categorie: " + proforma.billetCommande[i].calculCategorieBillet.IndicateurPrixBillet;
                            }
                            

                            phraseTempDetail = new Phrase(tempDetailBilletCommande);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((proforma.billetCommande[i].NombreBilletCommande * proforma.billetCommande[i].MontantBilletCommande).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailBilletCommande = "";
                        }
                    }

                    if (proforma.commissionDevis != null)
                    {
                        for (int i = 0; i < proforma.commissionDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(proforma.commissionDevis[i].IdCommissionDevis);
                            phraseTempDesignation.Font.Size = 8;

                            tempDetailCommissionDevis = "Commission, Type: " +  proforma.commissionDevis[i].TypeCommission + " ";
                            tempDetailCommissionDevis += " Destination: " + proforma.commissionDevis[i].Destination;

                            phraseTempDetail = new Phrase(tempDetailCommissionDevis);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles(proforma.commissionDevis[i].FraisEnvoi.ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailCommissionDevis = "";
                        }
                    }

                    if (proforma.dureeAbonnementDevis != null)
                    {
                        for (int i = 0; i < proforma.dureeAbonnementDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(proforma.dureeAbonnementDevis[i].NumDureeAbonnementDevis);
                            phraseTempDesignation.Font.Size = 8;

                            if (proforma.dureeAbonnementDevis[i].trajet != null)
                            {
                                tempDetailDureeAbonnement = "Abonnement par durée de temp pour " + proforma.dureeAbonnementDevis[i].trajet.villeD.NomVille + "-" + proforma.dureeAbonnementDevis[i].trajet.villeF.NomVille + " ";
                                tempDetailDureeAbonnement += " Zone: " + proforma.dureeAbonnementDevis[i].Zone;
                            }
                            else
                            {
                                tempDetailDureeAbonnement = "Abonnement par durée de temp pour le zone " + proforma.dureeAbonnementDevis[i].Zone;
                            }


                            phraseTempDetail = new Phrase(tempDetailDureeAbonnement);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((proforma.dureeAbonnementDevis[i].NombreDureeAbonnement * proforma.dureeAbonnementDevis[i].PrixTotal).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailDureeAbonnement = "";
                        }
                    }

                    if (proforma.voyageAbonnementDevis != null)
                    {
                        for (int i = 0; i < proforma.voyageAbonnementDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(proforma.voyageAbonnementDevis[i].NumVoyageAbonnementDevis);
                            phraseTempDesignation.Font.Size = 8;

                            if (proforma.voyageAbonnementDevis[i].trajet != null)
                            {
                                tempDetailVoyageAbonnement = "Abonnement, " + proforma.voyageAbonnementDevis[i].NbVoyageAbonnement + " voyage(s) pour " + proforma.voyageAbonnementDevis[i].trajet.villeD.NomVille + "-" + proforma.voyageAbonnementDevis[i].trajet.villeF.NomVille + " ";
                                tempDetailVoyageAbonnement += " Zone: " + proforma.voyageAbonnementDevis[i].Zone;
                            }
                            else
                            {
                                tempDetailVoyageAbonnement = "Abonnement, " + proforma.voyageAbonnementDevis[i].NbVoyageAbonnement + " voyage(s), zone: " + proforma.voyageAbonnementDevis[i].Zone;

                            }


                            phraseTempDetail = new Phrase(tempDetailVoyageAbonnement);
                            phraseTempDetail.Font.Size = 8;

                            
                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((proforma.voyageAbonnementDevis[i].NbVoyageAbonnement * proforma.voyageAbonnementDevis[i].PrixUnitaire).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailVoyageAbonnement = "";
                        }
                    }

                    if (proforma.uSAbonnementNVDevis != null)
                    {
                        for (int i = 0; i < proforma.uSAbonnementNVDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(proforma.uSAbonnementNVDevis[i].NumAbonnementNVDevis);
                            phraseTempDesignation.Font.Size = 8;

                            
                            if (proforma.uSAbonnementNVDevis[i].zoneD != null && proforma.uSAbonnementNVDevis[i].zoneF != null)
                            {
                                tempDetailUSAbonnementNV = "Abonnement, " + proforma.uSAbonnementNVDevis[i].infoPasse.NombrePasse.ToString("0") + " voyage(s) pour " + proforma.uSAbonnementNVDevis[i].zoneD.NomZone + "-" + proforma.uSAbonnementNVDevis[i].zoneF.NomZone;
                                if (proforma.uSAbonnementNVDevis[i].MontantCarte > 0)
                                {
                                    tempDetailUSAbonnementNV += " Carte:" + serviceGeneral.separateurDesMilles(proforma.uSAbonnementNVDevis[i].MontantCarte.ToString("0")) + " Ar";
                                }
                            }
                            else
                            {
                                tempDetailUSAbonnementNV = "Abonnement, " + proforma.uSAbonnementNVDevis[i].infoPasse.NombrePasse.ToString("0") + " voyage(s)";
                                if (proforma.uSAbonnementNVDevis[i].MontantCarte > 0)
                                {
                                    tempDetailUSAbonnementNV += " Carte:" + serviceGeneral.separateurDesMilles(proforma.uSAbonnementNVDevis[i].MontantCarte.ToString("0")) + " Ar";
                                }
                            }

                            phraseTempDetail = new Phrase(tempDetailUSAbonnementNV);
                            phraseTempDetail.Font.Size = 8;


                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((proforma.uSAbonnementNVDevis[i].MontantNV + proforma.uSAbonnementNVDevis[i].MontantCarte).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailUSAbonnementNV = "";
                        }
                    }


                    Phrase phraseMontantTotal = new Phrase(montantTotal);
                    phraseMontantTotal.Font.Size = 8;
                    phraseMontantTotal.Font.SetStyle(1);
                    Phrase phraseMontantTotalVar = new Phrase(montantTotalVar);
                    phraseMontantTotalVar.Font.Size = 8;
                    phraseMontantTotalVar.Font.SetStyle(1);

                    Cell celleMontantTotal = new Cell();
                    celleMontantTotal.Colspan = 2;
                    celleMontantTotal.AddElement(phraseMontantTotal);

                    tableCommande.AddCell(celleMontantTotal);
                    tableCommande.AddCell(phraseMontantTotalVar);
                    #endregion

                    #region ajout des element dans document
                    document.Add(tableLogo);

                    if (proforma.individu != null)
                    {
                        document.Add(tableClient);
                    }
                    if(proforma.societe != null)
                    {
                        document.Add(tableSociete);
                    }
                    if (proforma.organisme != null)
                    {
                        document.Add(tableOrganisme);
                    }
                    document.Add(phraseAlaLigne);
                    document.Add(tableCommande);
                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printRecu(string numRecu, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceRecu = new ImplDalRecu();
            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (numRecu != "")
            {
                Recu = serviceRecu.selectRecu(numRecu);
                if (Recu != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numRecuStr = "Reçu N°";
                    string numRecuStrVar = Recu.NumRecu;

                    string sommeLettre = "La somme de (en lettre):";
                    string sommeLettreVar = "";
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region cree pdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numRecuStr);
                    Phrase paraNumFBVar = new Phrase(numRecuStrVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #endregion

                    document.Close();

                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printRecuEncaisser(string numRecuEncaisser, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceRecuEncaisser = new ImplDalRecuEncaisser();
            serviceGeneral = new ImplDalGeneral();
            Document document = new Document(PageSize.A4);
            Convertisseuse convertiseur = new Convertisseuse();
            #endregion

            #region implementation
            if (numRecuEncaisser != "")
            {
                RecuEncaisser = serviceRecuEncaisser.selectRecuEncaisser(numRecuEncaisser);
                if (RecuEncaisser != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numRecuStr = "Reçu N°";
                    string numRecuStrVar = RecuEncaisser.NumRecuEncaisser;

                    string somme = "La somme de:";
                    string sommeVar = serviceGeneral.separateurDesMilles(RecuEncaisser.MontantRecuEncaisser.ToString("0")) + " Ar";

                    string sommeLettre = "La somme de (en lettre):";
                    string sommeLettreVar = convertiseur.convertion(RecuEncaisser.MontantRecuEncaisser.ToString("0")) + " Ariary";

                    string libelle = "Libellé:";
                    string libelleVar = RecuEncaisser.LibelleRecuEncaisser;

                    string modePaiement = "Mode de paiement:";
                    string modePaiementVar = "";

                    if (RecuEncaisser.cheque != null)
                    {
                        modePaiementVar = "Chèque " + RecuEncaisser.cheque.Banque + " N°" + RecuEncaisser.cheque.NumerosCheque;
                        modePaiementVar += " du " + RecuEncaisser.cheque.DateCheque.ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        modePaiementVar = "Espèces";
                    }

                    string faitA = "Fait à " + RecuEncaisser.agent.agence.ville.NomVille + " le " + RecuEncaisser.DateRecuEncaisser.ToString("dd MMMM yyyy");
                    string signature = "Signature";

                    Phrase phraseVide = new Phrase("");
                    Phrase phraseALaLigne = new Phrase("\n");
                    #endregion
                   
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;
                    
                    #region cree pdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numRecuStr);
                    Phrase paraNumFBVar = new Phrase(numRecuStrVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region somme
                    iTextSharp.text.Table tableSomme = new iTextSharp.text.Table(2, 4);
                    tableSomme.Border = 0;
                    tableSomme.DefaultCellBorder = 0;
                    tableSomme.Padding = 2;
                    tableSomme.Width = 100;
                    tableSomme.Widths = new float[] { 20, 80 };

                    Phrase phraseSomme = new Phrase(somme);
                    phraseSomme.Font.Size = 8;
                    Phrase phraseSommeVar = new Phrase(sommeVar);
                    phraseSommeVar.Font.Size = 8;
                    phraseSommeVar.Font.SetStyle(1);

                    Phrase phraseSommeLettre = new Phrase(sommeLettre);
                    phraseSommeLettre.Font.Size = 8;
                    Phrase phraseSommeLettreVar = new Phrase(sommeLettreVar);
                    phraseSommeLettreVar.Font.Size = 8;
                    phraseSommeLettreVar.Font.SetStyle(1);

                    Phrase phraseLibelle = new Phrase(libelle);
                    phraseLibelle.Font.Size = 8;
                    Phrase phraseLibelleVar = new Phrase(libelleVar);
                    phraseLibelleVar.Font.Size = 8;
                    phraseLibelleVar.Font.SetStyle(1);

                    Phrase phraseModePaiement = new Phrase(modePaiement);
                    phraseModePaiement.Font.Size = 8;
                    Phrase phraseModePaiementVar = new Phrase(modePaiementVar);
                    phraseModePaiementVar.Font.Size = 8;
                    phraseModePaiementVar.Font.SetStyle(1);

                    tableSomme.AddCell(phraseSomme);
                    tableSomme.AddCell(phraseSommeVar);
                    tableSomme.AddCell(phraseSommeLettre);
                    tableSomme.AddCell(phraseSommeLettreVar);
                    tableSomme.AddCell(phraseLibelle);
                    tableSomme.AddCell(phraseLibelleVar);
                    tableSomme.AddCell(phraseModePaiement);
                    tableSomme.AddCell(phraseModePaiementVar);
                    #endregion

                    #region bas de recu
                    iTextSharp.text.Table tableBas = new iTextSharp.text.Table(2, 2);
                    tableBas.Border = 0;
                    tableBas.DefaultCellBorder = 0;
                    tableBas.Padding = 2;
                    tableBas.Width = 100;
                    tableBas.Widths = new float[] { 60, 40 };

                    Phrase phraseFait = new Phrase(faitA);
                    phraseFait.Font.Size = 6;

                    Phrase phraseSignature = new Phrase(signature);
                    phraseSignature.Font.Size = 8;
                    phraseSignature.Font.SetStyle(1);

                    tableBas.AddCell(phraseVide);
                    tableBas.AddCell(phraseFait);
                    tableBas.AddCell(phraseVide);
                    tableBas.AddCell(phraseSignature);
                    #endregion

                    #region insertToDoc
                    document.Add(tableLogo);
                    document.Add(phraseVide);
                    document.Add(tableSomme);
                    document.Add(phraseVide);
                    document.Add(tableBas);

                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();

                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printRecuDecaisser(string numRecuDecaisser, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceRecuDecaisser = new ImplDalRecuDecaisser();
            serviceGeneral = new ImplDalGeneral();
            Document document = new Document(PageSize.A4);
            Convertisseuse convertiseur = new Convertisseuse();
            #endregion

            #region implementation
            if (numRecuDecaisser != "")
            {
                RecuDecaisser = serviceRecuDecaisser.selectRecuDecaisser(numRecuDecaisser);
                if (RecuDecaisser != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numRecuStr = "Reçu N°";
                    string numRecuStrVar = RecuDecaisser.NumRecuDecaisser;

                    string somme = "La somme de:";
                    string sommeVar = serviceGeneral.separateurDesMilles(RecuDecaisser.MotantRecuDecaisser.ToString("0")) + " Ar";

                    string sommeLettre = "La somme de (en lettre):";
                    string sommeLettreVar = convertiseur.convertion(RecuDecaisser.MotantRecuDecaisser.ToString("0")) + " Ariary";

                    string libelle = "Libellé:";
                    string libelleVar = RecuDecaisser.LibelleRecuDecaisser;

                    
                    

                    string faitA = "Fait à " + RecuDecaisser.agent.agence.ville.NomVille + " le " + RecuDecaisser.DateRecuDecaisser.ToString("dd MMMM yyyy");
                    string signature = "Signature";

                    Phrase phraseVide = new Phrase("");
                    Phrase phraseALaLigne = new Phrase("\n");
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region cree pdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numRecuStr);
                    Phrase paraNumFBVar = new Phrase(numRecuStrVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region somme
                    iTextSharp.text.Table tableSomme = new iTextSharp.text.Table(2, 3);
                    tableSomme.Border = 0;
                    tableSomme.DefaultCellBorder = 0;
                    tableSomme.Padding = 2;
                    tableSomme.Width = 100;
                    tableSomme.Widths = new float[] { 20, 80 };

                    Phrase phraseSomme = new Phrase(somme);
                    phraseSomme.Font.Size = 8;
                    Phrase phraseSommeVar = new Phrase(sommeVar);
                    phraseSommeVar.Font.Size = 8;
                    phraseSommeVar.Font.SetStyle(1);

                    Phrase phraseSommeLettre = new Phrase(sommeLettre);
                    phraseSommeLettre.Font.Size = 8;
                    Phrase phraseSommeLettreVar = new Phrase(sommeLettreVar);
                    phraseSommeLettreVar.Font.Size = 8;
                    phraseSommeLettreVar.Font.SetStyle(1);

                    Phrase phraseLibelle = new Phrase(libelle);
                    phraseLibelle.Font.Size = 8;
                    Phrase phraseLibelleVar = new Phrase(libelleVar);
                    phraseLibelleVar.Font.Size = 8;
                    phraseLibelleVar.Font.SetStyle(1);

                    

                    tableSomme.AddCell(phraseSomme);
                    tableSomme.AddCell(phraseSommeVar);
                    tableSomme.AddCell(phraseSommeLettre);
                    tableSomme.AddCell(phraseSommeLettreVar);
                    tableSomme.AddCell(phraseLibelle);
                    tableSomme.AddCell(phraseLibelleVar);
                    #endregion

                    #region bas de recu
                    iTextSharp.text.Table tableBas = new iTextSharp.text.Table(2, 2);
                    tableBas.Border = 0;
                    tableBas.DefaultCellBorder = 0;
                    tableBas.Padding = 2;
                    tableBas.Width = 100;
                    tableBas.Widths = new float[] { 60, 40 };

                    Phrase phraseFait = new Phrase(faitA);
                    phraseFait.Font.Size = 6;

                    Phrase phraseSignature = new Phrase(signature);
                    phraseSignature.Font.Size = 8;
                    phraseSignature.Font.SetStyle(1);

                    tableBas.AddCell(phraseVide);
                    tableBas.AddCell(phraseFait);
                    tableBas.AddCell(phraseVide);
                    tableBas.AddCell(phraseSignature);
                    #endregion

                    #region insertToDoc
                    document.Add(tableLogo);
                    document.Add(phraseVide);
                    document.Add(tableSomme);
                    document.Add(phraseVide);
                    document.Add(tableBas);

                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();

                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printRecuAD(string numRecuAD, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            serviceRecuAD = new ImplDalRecuAD();
            serviceGeneral = new ImplDalGeneral();
            Document document = new Document(PageSize.A4);
            Convertisseuse convertiseur = new Convertisseuse();
            #endregion

            #region implementation
            if (numRecuAD != "")
            {
                RecuAD = serviceRecuAD.selectRecuAD(numRecuAD);
                if (RecuAD != null)
                {
                    #region initialise pdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numRecuStr = "Reçu N°";
                    string numRecuStrVar = RecuAD.NumRecuAD;

                    string somme = "La somme de:";
                    string sommeVar = serviceGeneral.separateurDesMilles(RecuAD.Montant) + " Ar";

                    string sommeLettre = "La somme de (en lettre):";
                    string sommeLettreVar = convertiseur.convertion(RecuAD.Montant) + " Ariary";

                    string libelle = "Libellé:";
                    string libelleVar = RecuAD.Libele;


                    string faitA = "Fait à " + RecuAD.agent.agence.ville.NomVille + " le " + RecuAD.Date.ToString("dd MMMM yyyy");
                    string signature = "Signature";

                    Phrase phraseVide = new Phrase("");
                    Phrase phraseALaLigne = new Phrase("\n");
                    #endregion

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region cree pdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numRecuStr);
                    Phrase paraNumFBVar = new Phrase(numRecuStrVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region somme
                    iTextSharp.text.Table tableSomme = new iTextSharp.text.Table(2, 3);
                    tableSomme.Border = 0;
                    tableSomme.DefaultCellBorder = 0;
                    tableSomme.Padding = 2;
                    tableSomme.Width = 100;
                    tableSomme.Widths = new float[] { 20, 80 };

                    Phrase phraseSomme = new Phrase(somme);
                    phraseSomme.Font.Size = 8;
                    Phrase phraseSommeVar = new Phrase(sommeVar);
                    phraseSommeVar.Font.Size = 8;
                    phraseSommeVar.Font.SetStyle(1);

                    Phrase phraseSommeLettre = new Phrase(sommeLettre);
                    phraseSommeLettre.Font.Size = 8;
                    Phrase phraseSommeLettreVar = new Phrase(sommeLettreVar);
                    phraseSommeLettreVar.Font.Size = 8;
                    phraseSommeLettreVar.Font.SetStyle(1);

                    Phrase phraseLibelle = new Phrase(libelle);
                    phraseLibelle.Font.Size = 8;
                    Phrase phraseLibelleVar = new Phrase(libelleVar);
                    phraseLibelleVar.Font.Size = 8;
                    phraseLibelleVar.Font.SetStyle(1);

                    

                    tableSomme.AddCell(phraseSomme);
                    tableSomme.AddCell(phraseSommeVar);
                    tableSomme.AddCell(phraseSommeLettre);
                    tableSomme.AddCell(phraseSommeLettreVar);
                    tableSomme.AddCell(phraseLibelle);
                    tableSomme.AddCell(phraseLibelleVar);
                    #endregion

                    #region bas de recu
                    iTextSharp.text.Table tableBas = new iTextSharp.text.Table(2, 2);
                    tableBas.Border = 0;
                    tableBas.DefaultCellBorder = 0;
                    tableBas.Padding = 2;
                    tableBas.Width = 100;
                    tableBas.Widths = new float[] { 60, 40 };

                    Phrase phraseFait = new Phrase(faitA);
                    phraseFait.Font.Size = 6;

                    Phrase phraseSignature = new Phrase(signature);
                    phraseSignature.Font.Size = 8;
                    phraseSignature.Font.SetStyle(1);

                    tableBas.AddCell(phraseVide);
                    tableBas.AddCell(phraseFait);
                    tableBas.AddCell(phraseVide);
                    tableBas.AddCell(phraseSignature);
                    #endregion

                    #region insertToDoc
                    document.Add(tableLogo);
                    document.Add(phraseVide);
                    document.Add(tableSomme);
                    document.Add(phraseVide);
                    document.Add(tableBas);

                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();

                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printCommission(string numCommission, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            crlCommission commission = null;
            string[] tabIdCommission;

            IntfDalCommission serviceCommission = new ImplDalCommission();
            serviceGeneral = new ImplDalGeneral();

            Convertisseuse convertisseuse = new Convertisseuse();

            Document document = new Document(PageSize.A5);
            #endregion

            #region implementation
            if (numCommission != "")
            {
                tabIdCommission = numCommission.Split(';');

                if (tabIdCommission.Length > 0)
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    for (int i = 0; i < tabIdCommission.Length; i++)
                    {
                        commission = serviceCommission.selectCommission(tabIdCommission[i]);
                        if (commission != null)
                        {
                            #region initialise pdf
                            iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                            string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                            string idCommission = "Commission N°";
                            string idCommissionVar = tabIdCommission[i];

                            string strExpediteur = "Expéditeur";
                            string nomExpediteur = "Nom:";
                            string nomExpediteurVar = "";
                            string prenomExpediteur = "Prénom:";
                            string prenomExpediteurVar = "";
                            string adresseExpediteur = "Adresse:";
                            string adresseExpediteurVar = "";
                            string telephoneExpediteur = "Téléphone:";
                            string telephoneExpediteurVar = "";

                            if (commission.expediteur != null)
                            {
                                nomExpediteurVar = commission.expediteur.NomClient;
                                prenomExpediteurVar = commission.expediteur.PrenomClient;
                                adresseExpediteurVar = commission.expediteur.AdresseClient;
                                telephoneExpediteurVar = commission.expediteur.TelephoneClient + " / " + commission.expediteur.MobileClient;
                            }

                            string strReceptionnaire = "Réceptionnaire";
                            string nomReceptionnaire = "Nom:";
                            string nomReceptionnaireVar = "";
                            string prenomReceptionnaire = "Prénom:";
                            string prenomReceptionnaireVar = "";
                            string adresseReceptionnaire = "Adresse:";
                            string adresseReceptionnaireVar = "";
                            string telephoneReceptionnaire = "Téléphone:";
                            string telephoneReceptionnaireVar = "";

                            if (commission.recepteur != null)
                            {
                                nomReceptionnaireVar = commission.recepteur.NomPersonne;
                                prenomReceptionnaireVar = commission.recepteur.PrenomPersonne;
                                adresseReceptionnaireVar = commission.recepteur.AdressePersonne;
                                telephoneReceptionnaireVar = commission.recepteur.Telephone;
                            }

                            string strCommission = "Commission";
                            string destination = "Destination:";
                            string destinationVar = commission.Destination;
                            string poids = "Poids:";
                            string poidsVar = commission.Poids + "Kg";
                            string nombre = "Nombre:";
                            string nombreVar = commission.Nombre.ToString("0");
                            string pieceJustificative = "Pièce justificative:";
                            string pieceJustificativeVar = commission.PieceJustificatif;
                            string fraisEnvoi = "Frais:";
                            string fraisEnvoiVar = serviceGeneral.separateurDesMilles(commission.FraisEnvoi) + "Ar";
                            string fraisEnvoiEnLettre = "Frais en lettre:";
                            string fraisEnvoiEnLettreVar = convertisseuse.convertion(commission.FraisEnvoi) + " Ariary";
                            string designation = "Désignation:";
                            string designationVar = commission.designationCommission.Designation;
                            string type = "Type:";
                            string typeVar = commission.TypeCommission;
                            string date = "Date:";
                            string dateVar = commission.DateCommission.ToString("dd MMMM yyyy");

                            Phrase phraseVide = new Phrase("");
                            Phrase phraseALaLigne = new Phrase("\n");
                            #endregion

                            #region creePdf

                            #region logo
                            iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                            tableLogo.Border = 0;
                            tableLogo.DefaultCellBorder = 0;
                            tableLogo.Padding = 2;
                            tableLogo.Width = 100;
                            tableLogo.Widths = new float[] { 15, 85 };

                            Phrase paraGranTitre = new Phrase(grandTitre);
                            paraGranTitre.Font.Size = 16;
                            paraGranTitre.Font.SetStyle(1);


                            Phrase paraIdCommission = new Phrase(idCommission);
                            Phrase paraIdCommissionVar = new Phrase(idCommissionVar);
                            paraIdCommission.Font.Size = 14;
                            paraIdCommission.Font.SetStyle(1);
                            paraIdCommissionVar.Font.Size = 14;

                            Cell cellImg = new Cell();
                            cellImg.AddElement(imageLogo);
                            cellImg.Rowspan = 2;
                            Cell cellGTitre = new Cell();
                            cellGTitre.AddElement(paraGranTitre);
                            cellGTitre.BorderWidthBottom = 1;

                            Cell cellTitre = new Cell();
                            cellTitre.AddElement(paraIdCommission);
                            cellTitre.AddElement(paraIdCommissionVar);


                            tableLogo.AddCell(cellImg);
                            tableLogo.AddCell(cellGTitre);
                            tableLogo.AddCell(cellTitre);

                            #endregion

                            #region expediteur
                            Phrase phraseExpediteur = new Phrase(strExpediteur);
                            phraseExpediteur.Font.Size = 8;
                            phraseExpediteur.Font.SetStyle("underline");

                            iTextSharp.text.Table tableExpediteur = new iTextSharp.text.Table(5, 2);
                            tableExpediteur.Border = 0;
                            tableExpediteur.DefaultCellBorder = 0;
                            tableExpediteur.Padding = 2;
                            tableExpediteur.Width = 100;
                            tableExpediteur.Widths = new float[] { 15, 33, 4, 15, 33 };

                            Phrase phraseNomExpediteur = new Phrase(nomExpediteur);
                            Phrase phraseNomExpediteurVar = new Phrase(nomExpediteurVar);
                            phraseNomExpediteur.Font.Size = 6;
                            phraseNomExpediteurVar.Font.Size = 6;
                            phraseNomExpediteurVar.Font.SetStyle(1);

                            Phrase phrasePrenomExpediteur = new Phrase(prenomExpediteur);
                            Phrase phrasePrenomExpediteurVar = new Phrase(prenomExpediteurVar);
                            phrasePrenomExpediteur.Font.Size = 6;
                            phrasePrenomExpediteurVar.Font.Size = 6;
                            phrasePrenomExpediteurVar.Font.SetStyle(1);

                            Phrase phraseAdresseExpediteur = new Phrase(adresseExpediteur);
                            Phrase phraseAdresseExpediteurVar = new Phrase(adresseExpediteurVar);
                            phraseAdresseExpediteur.Font.Size = 6;
                            phraseAdresseExpediteurVar.Font.Size = 6;
                            phraseAdresseExpediteurVar.Font.SetStyle(1);

                            Phrase phraseTelephoneExpediteur = new Phrase(telephoneExpediteur);
                            Phrase phraseTelephoneExpediteurVar = new Phrase(telephoneExpediteurVar);
                            phraseTelephoneExpediteur.Font.Size = 6;
                            phraseTelephoneExpediteurVar.Font.Size = 6;
                            phraseTelephoneExpediteurVar.Font.SetStyle(1);

                            tableExpediteur.AddCell(phraseNomExpediteur);
                            tableExpediteur.AddCell(phraseNomExpediteurVar);
                            tableExpediteur.AddCell(phraseVide);
                            tableExpediteur.AddCell(phraseAdresseExpediteur);
                            tableExpediteur.AddCell(phraseAdresseExpediteurVar);
                            tableExpediteur.AddCell(phrasePrenomExpediteur);
                            tableExpediteur.AddCell(phrasePrenomExpediteurVar);
                            tableExpediteur.AddCell(phraseVide);
                            tableExpediteur.AddCell(phraseTelephoneExpediteur);
                            tableExpediteur.AddCell(phraseTelephoneExpediteurVar);
                            #endregion

                            #region receptionnaire
                            Phrase phraseReceptionnaire = new Phrase(strReceptionnaire);
                            phraseReceptionnaire.Font.Size = 8;
                            phraseReceptionnaire.Font.SetStyle("underline");

                            iTextSharp.text.Table tableReceptionnaire = new iTextSharp.text.Table(5, 2);
                            tableReceptionnaire.Border = 0;
                            tableReceptionnaire.DefaultCellBorder = 0;
                            tableReceptionnaire.Padding = 2;
                            tableReceptionnaire.Width = 100;
                            tableReceptionnaire.Widths = new float[] { 15, 33, 4, 15, 33 };

                            Phrase phraseNomReceptionnaire = new Phrase(nomReceptionnaire);
                            Phrase phraseNomReceptionnaireVar = new Phrase(nomReceptionnaireVar);
                            phraseNomReceptionnaire.Font.Size = 6;
                            phraseNomReceptionnaireVar.Font.Size = 6;
                            phraseNomReceptionnaireVar.Font.SetStyle(1);

                            Phrase phrasePrenomReceptionnaire = new Phrase(prenomReceptionnaire);
                            Phrase phrasePrenomReceptionnaireVar = new Phrase(prenomReceptionnaireVar);
                            phrasePrenomReceptionnaire.Font.Size = 6;
                            phrasePrenomReceptionnaireVar.Font.Size = 6;
                            phrasePrenomReceptionnaireVar.Font.SetStyle(1);

                            Phrase phraseAdresseReceptionnaire = new Phrase(adresseReceptionnaire);
                            Phrase phraseAdresseReceptionnaireVar = new Phrase(adresseReceptionnaireVar);
                            phraseAdresseReceptionnaire.Font.Size = 6;
                            phraseAdresseReceptionnaireVar.Font.Size = 6;
                            phraseAdresseReceptionnaireVar.Font.SetStyle(1);

                            Phrase phraseTelephoneReceptionnaire = new Phrase(telephoneReceptionnaire);
                            Phrase phraseTelephoneReceptionnaireVar = new Phrase(telephoneReceptionnaireVar);
                            phraseTelephoneReceptionnaire.Font.Size = 6;
                            phraseTelephoneReceptionnaireVar.Font.Size = 6;
                            phraseTelephoneReceptionnaireVar.Font.SetStyle(1);

                            tableReceptionnaire.AddCell(phraseNomReceptionnaire);
                            tableReceptionnaire.AddCell(phraseNomReceptionnaireVar);
                            tableReceptionnaire.AddCell(phraseVide);
                            tableReceptionnaire.AddCell(phraseAdresseReceptionnaire);
                            tableReceptionnaire.AddCell(phraseAdresseReceptionnaireVar);
                            tableReceptionnaire.AddCell(phrasePrenomReceptionnaire);
                            tableReceptionnaire.AddCell(phrasePrenomReceptionnaireVar);
                            tableReceptionnaire.AddCell(phraseVide);
                            tableReceptionnaire.AddCell(phraseTelephoneReceptionnaire);
                            tableReceptionnaire.AddCell(phraseTelephoneReceptionnaireVar);

                            #endregion

                            #region commission
                            Phrase phraseCommission = new Phrase(strCommission);
                            phraseCommission.Font.Size = 8;
                            phraseCommission.Font.SetStyle("underline");

                            iTextSharp.text.Table tableCommission = new iTextSharp.text.Table(5, 5);
                            tableCommission.Border = 0;
                            tableCommission.DefaultCellBorder = 0;
                            tableCommission.Padding = 2;
                            tableCommission.Width = 100;
                            tableCommission.Widths = new float[] { 15, 33, 4, 15, 33 };

                            Phrase phraseDate = new Phrase(date);
                            Phrase phraseDateVar = new Phrase(dateVar);
                            phraseDate.Font.Size = 6;
                            phraseDateVar.Font.Size = 6;
                            phraseDateVar.Font.SetStyle(1);

                            Phrase phraseDestination = new Phrase(destination);
                            Phrase phraseDestinationVar = new Phrase(destinationVar);
                            phraseDestination.Font.Size = 6;
                            phraseDestinationVar.Font.Size = 6;
                            phraseDestinationVar.Font.SetStyle(1);

                            Phrase phraseType = new Phrase(type);
                            Phrase phraseTypeVar = new Phrase(typeVar);
                            phraseType.Font.Size = 6;
                            phraseTypeVar.Font.Size = 6;
                            phraseTypeVar.Font.SetStyle(1);

                            Phrase phraseDesignation = new Phrase(designation);
                            Phrase phraseDesignationVar = new Phrase(designationVar);
                            phraseDesignation.Font.Size = 6;
                            phraseDesignationVar.Font.Size = 6;
                            phraseDesignationVar.Font.SetStyle(1);

                            Phrase phrasePoids = new Phrase(poids);
                            Phrase phrasePoidsVar = new Phrase(poidsVar);
                            phrasePoids.Font.Size = 6;
                            phrasePoidsVar.Font.Size = 6;
                            phrasePoidsVar.Font.SetStyle(1);

                            Phrase phraseNombre = new Phrase(nombre);
                            Phrase phraseNombreVar = new Phrase(nombreVar);
                            phraseNombre.Font.Size = 6;
                            phraseNombreVar.Font.Size = 6;
                            phraseNombreVar.Font.SetStyle(1);

                            Phrase phraseFrais = new Phrase(fraisEnvoi);
                            Phrase phraseFraisVar = new Phrase(fraisEnvoiVar);
                            phraseFrais.Font.Size = 6;
                            phraseFraisVar.Font.Size = 6;
                            phraseFraisVar.Font.SetStyle(1);

                            Phrase phrasePiece = new Phrase(pieceJustificative);
                            Phrase phrasePieceVar = new Phrase(pieceJustificativeVar);
                            phrasePiece.Font.Size = 6;
                            phrasePieceVar.Font.Size = 6;
                            phrasePieceVar.Font.SetStyle(1);

                            Phrase phraseFraisLettreVar = new Phrase(fraisEnvoiEnLettreVar);
                            phraseFraisLettreVar.Font.Size = 6;
                            phraseFraisLettreVar.Font.SetStyle(1);

                            tableCommission.AddCell(phraseDate);
                            tableCommission.AddCell(phraseDateVar);
                            tableCommission.AddCell(phraseVide);
                            tableCommission.AddCell(phraseDestination);
                            tableCommission.AddCell(phraseDestinationVar);
                            tableCommission.AddCell(phraseType);
                            tableCommission.AddCell(phraseTypeVar);
                            tableCommission.AddCell(phraseVide);
                            tableCommission.AddCell(phraseDesignation);
                            tableCommission.AddCell(phraseDesignationVar);
                            tableCommission.AddCell(phrasePoids);
                            tableCommission.AddCell(phrasePoidsVar);
                            tableCommission.AddCell(phraseVide);
                            tableCommission.AddCell(phraseNombre);
                            tableCommission.AddCell(phraseNombreVar);
                            tableCommission.AddCell(phraseFrais);
                            tableCommission.AddCell(phraseFraisVar);
                            tableCommission.AddCell(phraseVide);
                            tableCommission.AddCell(phrasePiece);
                            tableCommission.AddCell(phrasePieceVar);
                            Cell celleFraisEnLettre = new Cell();
                            celleFraisEnLettre.Colspan = 4;
                            celleFraisEnLettre.AddElement(phraseFraisLettreVar);
                            tableCommission.AddCell(phraseVide);
                            tableCommission.AddCell(celleFraisEnLettre);
                            #endregion

                            #region ajout des element dans document
                            document.Add(tableLogo);
                            document.Add(phraseALaLigne);
                            document.Add(phraseExpediteur);
                            document.Add(tableExpediteur);

                            if (commission.recepteur != null)
                            {
                                document.Add(phraseReceptionnaire);
                                document.Add(tableReceptionnaire);
                            }

                            document.Add(phraseCommission);
                            document.Add(tableCommission);

                            isPrint = true;
                            #endregion

                            #endregion


                            commission = null;
                            if (i < (tabIdCommission.Length - 1))
                            {
                                document.NewPage();
                            }
                        }
                    }

                    document.Close();

                }

            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printBillet(string numBillet, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            crlBillet billet = null;
            string[] tabNumBillet;

            IntfDalBillet serviceBillet = new ImplDalBillet();
            serviceGeneral = new ImplDalGeneral();

            Convertisseuse convertisseuse = new Convertisseuse();

            Document document = new Document(PageSize.A5);
            #endregion

            #region implementation
            if (numBillet != "")
            {
                tabNumBillet = numBillet.Split(';');

                if (tabNumBillet.Length > 0)
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();

                    PdfContentByte cb = writer.DirectContent;

                    for (int i = 0; i < tabNumBillet.Length ; i++)
                    {
                        billet = serviceBillet.selectBillet(tabNumBillet[i]);

                        if (billet != null)
                        {
                            #region initialisepdf
                            iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                            string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";
                            string numBilletTxt = "Billet N°";
                            string numBilletVar = billet.NumBillet;

                            string trajet = "Trajet: ";
                            string trajetVar = "";
                            if (billet.trajet != null)
                            {
                                trajetVar = billet.trajet.villeD.NomVille + "-" + billet.trajet.villeF.NomVille;
                            }
                            string categorie = "Catégorie: ";

                            string categorieVar = "";

                            if (billet.calculCategorieBillet != null)
                            {
                                categorieVar = billet.calculCategorieBillet.IndicateurPrixBillet;
                            }

                            string prixBillet = "Prix: ";
                            string prixBilletVar = serviceGeneral.separateurDesMilles(billet.PrixBillet) + " Ar";

                            string valideAu = "Billet valide au: ";
                            string valideAuVar = billet.DateDeValidite.ToString("dd MMMM yyyy");

                            string strBasPage = "Tehirizo ity tapakila ity alohan'ny andehananao. Misaotra tompoko!";
                            //Phrase phraseALaLigne = new Phrase("\n");
                            Phrase phraseVide = new Phrase(" ");

                            #endregion

                            #region creePdf
                            iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                            tableLogo.Border = 0;
                            tableLogo.DefaultCellBorder = 0;
                            tableLogo.Padding = 2;
                            tableLogo.Width = 100;
                            tableLogo.Widths = new float[] { 15, 85 };

                            Phrase paraGranTitre = new Phrase(grandTitre);
                            paraGranTitre.Font.Size = 16;
                            paraGranTitre.Font.SetStyle(1);

                            Phrase phraseNumBillet = new Phrase(numBilletTxt);
                            Phrase phraseNumBilletVar = new Phrase(numBilletVar);
                            phraseNumBillet.Font.Size = 14;
                            phraseNumBillet.Font.SetStyle(1);
                            phraseNumBilletVar.Font.Size = 14;

                            Cell cellImg = new Cell();
                            cellImg.AddElement(imageLogo);
                            cellImg.Rowspan = 2;
                            Cell cellGTitre = new Cell();
                            cellGTitre.AddElement(paraGranTitre);
                            cellGTitre.BorderWidthBottom = 1;

                            Cell cellTitre = new Cell();
                            cellTitre.AddElement(phraseNumBillet);
                            cellTitre.AddElement(phraseNumBilletVar);


                            tableLogo.AddCell(cellImg);
                            tableLogo.AddCell(cellGTitre);
                            tableLogo.AddCell(cellTitre);

                            /*
                            iTextSharp.text.Table tableNumBillet = new iTextSharp.text.Table(1, 1);
                            tableNumBillet.Border = 0;
                            tableNumBillet.DefaultCellBorder = 0;
                            tableNumBillet.Padding = 0;
                            tableNumBillet.Width = 100;
                            tableNumBillet.Widths = new float[] { 100 };

                            
                            Cell cellNumBillet = new Cell();
                            cellNumBillet.AddElement(phraseNumBillet);
                            cellNumBillet.AddElement(phraseNumBilletVar);

                            tableNumBillet.AddCell(cellNumBillet);
                            */

                            iTextSharp.text.Table tableContent = new iTextSharp.text.Table(2, 4);
                            tableContent.Border = 0;
                            tableContent.DefaultCellBorder = 0;
                            tableContent.Padding = 0;
                            tableContent.Width = 100;
                            tableContent.Widths = new float[] { 40, 60 };

                            Phrase phraseTrajet = new Phrase(trajet);
                            Phrase phraseTrajetVar = new Phrase(trajetVar);
                            phraseTrajet.Font.Size = 8;
                            phraseTrajetVar.Font.Size = 8;
                            phraseTrajetVar.Font.SetStyle(1);

                            Phrase phraseCategorie = new Phrase(categorie);
                            Phrase phraseCategorieVar = new Phrase(categorieVar);
                            phraseCategorie.Font.Size = 8;
                            phraseCategorieVar.Font.Size =8;
                            phraseCategorieVar.Font.SetStyle(1);

                            Phrase phrasePrix = new Phrase(prixBillet);
                            Phrase phrasePrixVar = new Phrase(prixBilletVar);
                            phrasePrix.Font.Size = 8;
                            phrasePrixVar.Font.Size = 8;
                            phrasePrixVar.Font.SetStyle(1);

                            Phrase phraseValide = new Phrase(valideAu);
                            Phrase phraseValideVar = new Phrase(valideAuVar);
                            phraseValide.Font.Size = 8;
                            phraseValideVar.Font.Size = 8;
                            phraseValideVar.Font.SetStyle(1);

                            tableContent.AddCell(phraseTrajet);
                            tableContent.AddCell(phraseTrajetVar);
                            tableContent.AddCell(phraseCategorie);
                            tableContent.AddCell(phraseCategorieVar);
                            tableContent.AddCell(phrasePrix);
                            tableContent.AddCell(phrasePrixVar);
                            tableContent.AddCell(phraseValide);
                            tableContent.AddCell(phraseValideVar);


                            #region bas
                            iTextSharp.text.Table tableBas = new iTextSharp.text.Table(1, 1);
                            tableBas.Border = 0;
                            tableBas.DefaultCellBorder = 0;
                            tableBas.Padding = 0;
                            tableBas.Width = 100;
                            tableBas.Widths = new float[] { 100 };

                            Phrase phraseBas = new Phrase(strBasPage);
                            phraseBas.Font.Size = 6;

                            tableBas.AddCell(phraseBas);
                            #endregion

                            #region ajout des element dans document
                            document.Add(tableLogo);
                            //document.Add(tableNumBillet);
                            //document.Add(phraseVide);
                            document.Add(tableContent);
                            document.Add(tableBas);

                            isPrint = true;
                            #endregion

                            #endregion
                        }


                        billet = null;
                        if (i < (tabNumBillet.Length - 1))
                        {
                            document.NewPage();
                        }
                    }
                    document.Close();
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printVoyage(string idVoyage, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            crlVoyage voyage = null;
            crlFicheBord ficheBord = null;
            string[] tabNumBillet;

            serviceVoyage = new ImplDalVoyage();
            serviceGeneral = new ImplDalGeneral();
            serviceFicheBord = new ImplDalFicheBord();

            Convertisseuse convertisseuse = new Convertisseuse();

            Document document = new Document(PageSize.A8);
            document.SetMargins(5F, 5F, 5F, 5F);
            #endregion

            #region implementation
            if (idVoyage != "")
            {
                voyage = serviceVoyage.selectVoyage(idVoyage);
                

                if (voyage != null)
                {
                    ficheBord = serviceFicheBord.selectFicheBord(voyage.NumerosFB);

                    if (ficheBord != null)
                    {
                        #region initialise pdf
                        iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                        string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                        string idVoyageStr = "Ticket N°";
                        string idVoyageVar = idVoyage;

                        string trajet = "Trajet:";
                        string trajetVar = voyage.billet.trajet.villeD.NomVille + "-" + voyage.billet.trajet.villeF.NomVille;

                        string vehicule = "Véhicule:";
                        string vehiculeVar = ficheBord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule + " ";
                        vehiculeVar += ficheBord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule + " ";
                        vehiculeVar += ficheBord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule;

                        string numPlace = "Place n° ";
                        string numPlaceVar = voyage.placeFB.NumPlace;

                        string strBasPage = "Tehirizo ny tapakila fa hisy fisavana. Misaotra tompoko!";

                        Phrase phraseALaLigne = new Phrase(" ");
                        #endregion

                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                        document.Open();
                        PdfContentByte cb = writer.DirectContent;

                        #region cree pdf

                        #region logo
                        iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(1, 1);
                        tableLogo.Border = 0;
                        tableLogo.DefaultCellBorder = 0;
                        tableLogo.Padding = 0;
                        tableLogo.Width = 100;
                        tableLogo.Widths = new float[] { 100 };

                        Phrase phraseGrandTitre = new Phrase(grandTitre);
                        phraseGrandTitre.Font.Size = 6;
                        phraseGrandTitre.Font.SetStyle("underline");

                        tableLogo.AddCell(phraseGrandTitre);

                        iTextSharp.text.Table tableNumTicket = new iTextSharp.text.Table(1, 1);
                        tableNumTicket.Border = 0;
                        tableNumTicket.DefaultCellBorder = 0;
                        tableNumTicket.Padding = 0;
                        tableNumTicket.Width = 100;
                        tableNumTicket.Widths = new float[] { 100 };

                        Phrase phraseNumTicket = new Phrase(idVoyageStr);
                        Phrase phraseNumTicketVar = new Phrase(idVoyageVar);
                        phraseNumTicket.Font.Size = 8;
                        phraseNumTicket.Font.SetStyle(1);
                        phraseNumTicketVar.Font.Size = 8;
                        Cell cellNumTicket = new Cell();
                        cellNumTicket.AddElement(phraseNumTicket);
                        cellNumTicket.AddElement(phraseNumTicketVar);

                        tableNumTicket.AddCell(cellNumTicket);

                        #endregion

                        #region cotenu
                        iTextSharp.text.Table tableContenu = new iTextSharp.text.Table(2, 3);
                        tableContenu.Border = 0;
                        tableContenu.DefaultCellBorder = 0;
                        tableContenu.Padding = 0;
                        tableContenu.Width = 100;
                        tableContenu.Widths = new float[] {40, 60};

                        Phrase phraseTrajet = new Phrase(trajet);
                        Phrase phraseTrajetVar = new Phrase(trajetVar);
                        phraseTrajet.Font.Size = 6;
                        phraseTrajetVar.Font.Size = 6;
                        phraseTrajetVar.Font.SetStyle(1);

                        Phrase phraseVehicule = new Phrase(vehicule);
                        Phrase phraseVehiculeVar = new Phrase(vehiculeVar);
                        phraseVehicule.Font.Size = 6;
                        phraseVehiculeVar.Font.Size = 6;
                        phraseVehiculeVar.Font.SetStyle(1);

                        Phrase phraseNumPlace = new Phrase(numPlace);
                        Phrase phraseNumPlaceVar = new Phrase(numPlaceVar);
                        phraseNumPlace.Font.Size = 6;
                        phraseNumPlaceVar.Font.Size = 6;
                        phraseNumPlaceVar.Font.SetStyle(1);

                        tableContenu.AddCell(phraseTrajet);
                        tableContenu.AddCell(phraseTrajetVar);
                        tableContenu.AddCell(phraseVehicule);
                        tableContenu.AddCell(phraseVehiculeVar);
                        tableContenu.AddCell(phraseNumPlace);
                        tableContenu.AddCell(phraseNumPlaceVar);

                        #endregion

                        #region bas
                        iTextSharp.text.Table tableBas = new iTextSharp.text.Table(1, 1);
                        tableBas.Border = 0;
                        tableBas.DefaultCellBorder = 0;
                        tableBas.Padding = 0;
                        tableBas.Width = 100;
                        tableBas.Widths = new float[] { 100 };

                        Phrase phraseBas = new Phrase(strBasPage);
                        phraseBas.Font.Size = 4;

                        tableBas.AddCell(phraseBas);
                        #endregion

                        #region insert To pdf
                        document.Add(tableLogo);
                        document.Add(tableNumTicket);
                        document.Add(phraseALaLigne);
                        document.Add(tableContenu);
                        document.Add(phraseALaLigne);
                        document.Add(tableBas);

                        isPrint = true;
                        #endregion

                        #endregion

                        document.Close();

                    }
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printBonCommande(string numBonDeCommande, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;

            crlBonDeCommande bonDeCommande = null;
            IntfDalBonDeCommande serviceBonDeCommande = new ImplDalBonDeCommande();
            Convertisseuse convertisseuse = new Convertisseuse();
            serviceGeneral = new ImplDalGeneral();

            Document document = new Document(PageSize.A4);
            #endregion

            #region implementation
            if (numBonDeCommande != "")
            {
                bonDeCommande = serviceBonDeCommande.selectBonDeCommande(numBonDeCommande);
                if (bonDeCommande != null)
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    #region initialisePdf
                    iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CssStyle\\images\\logoPdf.png");
                    string grandTitre = "CONFEDERATION NATIONALE DE TRANSPORT";

                    string numBonDeCommandeStr = "Bon de commande N°";
                    string numBonDeCommandeStrVar = bonDeCommande.NumBonDeCommande;

                    string nomSociete = "Société:";
                    string nomSocieteVar = "";
                    string adresseSociete = "Adresse:";
                    string adresseSocieteVar = "";
                    string mailSociete = "Mail:";
                    string mailSocieteVar = "";
                    string telephoneSociete = "Téléphone:";
                    string telephoneSocieteVar = "";
                    string responsableSociete = "Responsable:";
                    string responsableSocieteVar = "";
                    string adresseResponsableSociete = "Adresse:";
                    string adresseResponsableSocieteVar = "";
                    string telephoneResponsableSociete = "Téléphone:";
                    string telephoneResponsableSocieteVar = "";

                    string nomOrganisme = "Organisme:";
                    string nomOrganismeVar = "";
                    string adresseOrganisme = "Adresse:";
                    string adresseOrganismeVar = "";
                    string mailOrganisme = "Mail:";
                    string mailOrganismeVar = "";
                    string telephoneOrganisme = "Téléphone:";
                    string telephoneOrganismeVar = "";
                    string responsableOrganisme = "Responsable:";
                    string responsableOrganismeVar = "";
                    string adresseResponsableOrganisme = "Adresse:";
                    string adresseResponsableOrganismeVar = "";
                    string telephoneResponsableOrganisme = "Téléphone:";
                    string telephoneResponsableOrganismeVar = "";

                    string nomClient = "Client:";
                    string nomClientVar = "";
                    string adresseClient = "Adresse:";
                    string adresseClientVar = "";
                    string telephoneClient = "Téléphone:";
                    string telephoneClientVar = "";

                    if (bonDeCommande.proforma != null)
                    {
                        if (bonDeCommande.proforma.individu != null)
                        {
                            nomClientVar = bonDeCommande.proforma.individu.PrenomIndividu + " " + bonDeCommande.proforma.individu.NomIndividu;
                            adresseClientVar = bonDeCommande.proforma.individu.Adresse;
                            telephoneClientVar = bonDeCommande.proforma.individu.TelephoneFixeIndividu + " " + bonDeCommande.proforma.individu.TelephoneMobileIndividu;
                        }
                        if (bonDeCommande.proforma.organisme != null)
                        {
                            nomOrganismeVar = bonDeCommande.proforma.organisme.NomOrganisme;
                            adresseOrganismeVar = bonDeCommande.proforma.organisme.AdresseOrganisme;
                            mailOrganismeVar = bonDeCommande.proforma.organisme.MailOrganisme;
                            telephoneOrganismeVar = bonDeCommande.proforma.organisme.TelephoneFixeOrganisme + " " + bonDeCommande.proforma.organisme.TelephoneMobileOrganisme;
                            if (bonDeCommande.proforma.organisme.individuResponsable != null)
                            {
                                responsableOrganismeVar = bonDeCommande.proforma.organisme.individuResponsable.PrenomIndividu + " " + bonDeCommande.proforma.organisme.individuResponsable.NomIndividu;
                                adresseResponsableOrganismeVar = bonDeCommande.proforma.organisme.individuResponsable.Adresse;
                                telephoneResponsableOrganismeVar = bonDeCommande.proforma.organisme.individuResponsable.TelephoneFixeIndividu + " " + bonDeCommande.proforma.organisme.individuResponsable.TelephoneMobileIndividu;
                            }
                        }
                        if (bonDeCommande.proforma.societe != null)
                        {
                            nomSocieteVar = bonDeCommande.proforma.societe.NomSociete;
                            adresseSocieteVar = bonDeCommande.proforma.societe.AdresseSociete;
                            mailSocieteVar = bonDeCommande.proforma.societe.MailSociete;
                            telephoneSocieteVar = bonDeCommande.proforma.societe.TelephoneFixeSociete + " " + bonDeCommande.proforma.societe.TelephoneMobileSociete;
                            if (bonDeCommande.proforma.societe.individuResponsable != null)
                            {
                                responsableSocieteVar = bonDeCommande.proforma.societe.individuResponsable.PrenomIndividu + " " + bonDeCommande.proforma.societe.individuResponsable.NomIndividu;
                                adresseResponsableSocieteVar = bonDeCommande.proforma.societe.individuResponsable.Adresse;
                                telephoneResponsableSocieteVar = bonDeCommande.proforma.societe.individuResponsable.TelephoneFixeIndividu + " " + bonDeCommande.proforma.societe.individuResponsable.TelephoneMobileIndividu;
                            }
                        }
                    }

                    string datePaiement = "Date de paiement:";
                    string datePaiementVar = bonDeCommande.DatePaiementBC.ToString("dd MMMM yyyy");

                    string description = "Description:";
                    string descritpionVar = bonDeCommande.DescriptionBC;

                    string montant = "Montant total:";
                    string montantVar = serviceGeneral.separateurDesMilles(bonDeCommande.MontantBC.ToString("0")) + "Ar";

                    string montantLettre = "Montant total en lettre:";
                    string montantLettreVar = convertisseuse.convertion(bonDeCommande.MontantBC.ToString("0")) + " Ariary";

                    string designation = "Référence";
                    string detail = "Détail";
                    string prixTotal = "Montant";
                    string montantTotal = "Montant total";

                    string tempDetailBilletCommande = "";
                    string tempDetailCommissionDevis = "";
                    string tempDetailVoyageAbonnement = "";
                    string tempDetailDureeAbonnement = "";
                    string tempDetailUSAbonnementNV = "";

                    Phrase phraseVide = new Phrase("");
                    Phrase phraseAlaLigne = new Phrase("\n");
                    #endregion

                    #region creePdf

                    #region logo
                    iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(2, 2);
                    tableLogo.Border = 0;
                    tableLogo.DefaultCellBorder = 0;
                    tableLogo.Padding = 2;
                    tableLogo.Width = 100;
                    tableLogo.Widths = new float[] { 15, 85 };

                    Phrase paraGranTitre = new Phrase(grandTitre);
                    paraGranTitre.Font.Size = 16;
                    paraGranTitre.Font.SetStyle(1);


                    Phrase paraNumFB = new Phrase(numBonDeCommandeStr);
                    Phrase paraNumFBVar = new Phrase(numBonDeCommandeStrVar);
                    paraNumFB.Font.Size = 14;
                    paraNumFB.Font.SetStyle(1);
                    paraNumFBVar.Font.Size = 14;

                    Cell cellImg = new Cell();
                    cellImg.AddElement(imageLogo);
                    cellImg.Rowspan = 2;
                    Cell cellGTitre = new Cell();
                    cellGTitre.AddElement(paraGranTitre);
                    cellGTitre.BorderWidthBottom = 1;

                    Cell cellTitre = new Cell();
                    cellTitre.AddElement(paraNumFB);
                    cellTitre.AddElement(paraNumFBVar);


                    tableLogo.AddCell(cellImg);
                    tableLogo.AddCell(cellGTitre);
                    tableLogo.AddCell(cellTitre);

                    #endregion

                    #region tableau Client,Societe,Organisme
                    iTextSharp.text.Table tableClient = new iTextSharp.text.Table(2, 3);
                    tableClient.Border = 0;
                    tableClient.DefaultCellBorder = 0;
                    tableClient.Padding = 2;
                    tableClient.Width = 100;
                    tableClient.Widths = new float[] { 15, 85 };

                    iTextSharp.text.Table tableSociete = new iTextSharp.text.Table(5, 4);
                    tableSociete.Border = 0;
                    tableSociete.DefaultCellBorder = 0;
                    tableSociete.Padding = 2;
                    tableSociete.Width = 100;
                    tableSociete.Widths = new float[] { 15, 33, 4, 15, 33 };

                    iTextSharp.text.Table tableOrganisme = new iTextSharp.text.Table(5, 4);
                    tableOrganisme.Border = 0;
                    tableOrganisme.DefaultCellBorder = 0;
                    tableOrganisme.Padding = 2;
                    tableOrganisme.Width = 100;
                    tableOrganisme.Widths = new float[] { 15, 33, 4, 15, 33 };

                    Phrase phraseNomClient = new Phrase(nomClient);
                    Phrase phraseNomClientVar = new Phrase(nomClientVar);
                    phraseNomClient.Font.Size = 8;
                    phraseNomClientVar.Font.Size = 8;
                    phraseNomClientVar.Font.SetStyle(1);

                    Phrase phraseAdresseClient = new Phrase(adresseClient);
                    Phrase phraseAdresseClientVar = new Phrase(adresseClientVar);
                    phraseAdresseClient.Font.Size = 8;
                    phraseAdresseClientVar.Font.Size = 8;
                    phraseAdresseClientVar.Font.SetStyle(1);

                    Phrase phraseTelephoneClient = new Phrase(telephoneClient);
                    Phrase phraseTelephoneClientVar = new Phrase(telephoneClientVar);
                    phraseTelephoneClient.Font.Size = 8;
                    phraseTelephoneClientVar.Font.Size = 8;
                    phraseTelephoneClientVar.Font.SetStyle(1);

                    tableClient.AddCell(phraseNomClient);
                    tableClient.AddCell(phraseNomClientVar);
                    tableClient.AddCell(phraseAdresseClient);
                    tableClient.AddCell(phraseAdresseClientVar);
                    tableClient.AddCell(phraseTelephoneClient);
                    tableClient.AddCell(phraseTelephoneClientVar);

                    Phrase phraseNomSociete = new Phrase(nomSociete);
                    Phrase phraseNomSocieteVar = new Phrase(nomSocieteVar);
                    phraseNomSociete.Font.Size = 8;
                    phraseNomSocieteVar.Font.Size = 8;
                    phraseNomSocieteVar.Font.SetStyle(1);

                    Phrase phraseAdresseSociete = new Phrase(adresseSociete);
                    Phrase phraseAdresseSocieteVar = new Phrase(adresseSocieteVar);
                    phraseAdresseSociete.Font.Size = 8;
                    phraseAdresseSocieteVar.Font.Size = 8;
                    phraseAdresseSocieteVar.Font.SetStyle(1);

                    Phrase phraseMailSociete = new Phrase(mailSociete);
                    Phrase phraseMailSocieteVar = new Phrase(mailSocieteVar);
                    phraseMailSociete.Font.Size = 8;
                    phraseMailSocieteVar.Font.Size = 8;
                    phraseMailSocieteVar.Font.SetStyle(1);

                    Phrase phraseTelephoneSociete = new Phrase(telephoneSociete);
                    Phrase phraseTelephoneSocieteVar = new Phrase(telephoneSocieteVar);
                    phraseTelephoneSociete.Font.Size = 8;
                    phraseTelephoneSocieteVar.Font.Size = 8;
                    phraseTelephoneSocieteVar.Font.SetStyle(1);

                    Phrase phraseResponsableSociete = new Phrase(responsableSociete);
                    Phrase phraseResponsableSocieteVar = new Phrase(responsableSocieteVar);
                    phraseResponsableSociete.Font.Size = 8;
                    phraseResponsableSocieteVar.Font.Size = 8;
                    phraseResponsableSocieteVar.Font.SetStyle(1);

                    Phrase phraseAdresseResponsableSociete = new Phrase(adresseResponsableSociete);
                    Phrase phraseAdresseResponsableSocieteVar = new Phrase(adresseResponsableSocieteVar);
                    phraseAdresseResponsableSociete.Font.Size = 8;
                    phraseAdresseResponsableSocieteVar.Font.Size = 8;
                    phraseAdresseResponsableSocieteVar.Font.SetStyle(1);

                    Phrase phraseTelephoneResponsableSociete = new Phrase(telephoneResponsableSociete);
                    Phrase phraseTelephoneResponsableSocieteVar = new Phrase(telephoneResponsableSocieteVar);
                    phraseTelephoneResponsableSociete.Font.Size = 8;
                    phraseTelephoneResponsableSocieteVar.Font.Size = 8;
                    phraseTelephoneResponsableSocieteVar.Font.SetStyle(1);

                    tableSociete.AddCell(phraseNomSociete);
                    tableSociete.AddCell(phraseNomSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseResponsableSociete);
                    tableSociete.AddCell(phraseResponsableSocieteVar);
                    tableSociete.AddCell(phraseAdresseSociete);
                    tableSociete.AddCell(phraseAdresseSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseAdresseResponsableSociete);
                    tableSociete.AddCell(phraseAdresseResponsableSocieteVar);
                    tableSociete.AddCell(phraseTelephoneSociete);
                    tableSociete.AddCell(phraseTelephoneSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseTelephoneResponsableSociete);
                    tableSociete.AddCell(phraseTelephoneResponsableSocieteVar);
                    tableSociete.AddCell(phraseMailSociete);
                    tableSociete.AddCell(phraseMailSocieteVar);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseVide);
                    tableSociete.AddCell(phraseVide);

                    Phrase phraseNomOrganisme = new Phrase(nomOrganisme);
                    Phrase phraseNomOrganismeVar = new Phrase(nomOrganismeVar);
                    phraseNomOrganisme.Font.Size = 8;
                    phraseNomOrganismeVar.Font.Size = 8;
                    phraseNomOrganismeVar.Font.SetStyle(1);

                    Phrase phraseAdresseOrganisme = new Phrase(adresseOrganisme);
                    Phrase phraseAdresseOrganismeVar = new Phrase(adresseOrganismeVar);
                    phraseAdresseOrganisme.Font.Size = 8;
                    phraseAdresseOrganismeVar.Font.Size = 8;
                    phraseAdresseOrganismeVar.Font.SetStyle(1);

                    Phrase phraseMailOrganisme = new Phrase(mailOrganisme);
                    Phrase phraseMailOrganismeVar = new Phrase(mailOrganismeVar);
                    phraseMailOrganisme.Font.Size = 8;
                    phraseMailOrganismeVar.Font.Size = 8;
                    phraseMailOrganismeVar.Font.SetStyle(1);

                    Phrase phraseTelephoneOrganisme = new Phrase(telephoneOrganisme);
                    Phrase phraseTelephoneOrganismeVar = new Phrase(telephoneOrganismeVar);
                    phraseTelephoneOrganisme.Font.Size = 8;
                    phraseTelephoneOrganismeVar.Font.Size = 8;
                    phraseTelephoneOrganismeVar.Font.SetStyle(1);

                    Phrase phraseResponsableOrganisme = new Phrase(responsableOrganisme);
                    Phrase phraseResponsableOrganismeVar = new Phrase(responsableOrganismeVar);
                    phraseResponsableOrganisme.Font.Size = 8;
                    phraseResponsableOrganismeVar.Font.Size = 8;
                    phraseResponsableOrganismeVar.Font.SetStyle(1);

                    Phrase phraseAdresseResponsableOrganisme = new Phrase(adresseResponsableOrganisme);
                    Phrase phraseAdresseResponsableOrganismeVar = new Phrase(adresseResponsableOrganismeVar);
                    phraseAdresseResponsableOrganisme.Font.Size = 8;
                    phraseAdresseResponsableOrganismeVar.Font.Size = 8;
                    phraseAdresseResponsableOrganismeVar.Font.SetStyle(1);

                    Phrase phraseTelephoneResponsableOrganisme = new Phrase(telephoneResponsableOrganisme);
                    Phrase phraseTelephoneResponsableOrganismeVar = new Phrase(telephoneResponsableOrganismeVar);
                    phraseTelephoneResponsableOrganisme.Font.Size = 8;
                    phraseTelephoneResponsableOrganismeVar.Font.Size = 8;
                    phraseTelephoneResponsableOrganismeVar.Font.SetStyle(1);

                    tableOrganisme.AddCell(phraseNomOrganisme);
                    tableOrganisme.AddCell(phraseNomOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseResponsableOrganisme);
                    tableOrganisme.AddCell(phraseResponsableOrganismeVar);
                    tableOrganisme.AddCell(phraseAdresseOrganisme);
                    tableOrganisme.AddCell(phraseAdresseOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseAdresseResponsableOrganisme);
                    tableOrganisme.AddCell(phraseAdresseResponsableOrganismeVar);
                    tableOrganisme.AddCell(phraseTelephoneOrganisme);
                    tableOrganisme.AddCell(phraseTelephoneOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseTelephoneResponsableOrganisme);
                    tableOrganisme.AddCell(phraseTelephoneResponsableOrganismeVar);
                    tableOrganisme.AddCell(phraseMailOrganisme);
                    tableOrganisme.AddCell(phraseMailOrganismeVar);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseVide);
                    tableOrganisme.AddCell(phraseVide);

                    #endregion

                    #region tableau commande
                    int nombreLigne = 0;
                    if (bonDeCommande.proforma.billetCommande != null)
                    {
                        nombreLigne += bonDeCommande.proforma.billetCommande.Count;
                    }
                    if (bonDeCommande.proforma.commissionDevis != null)
                    {
                        nombreLigne += bonDeCommande.proforma.commissionDevis.Count;
                    }
                    if (bonDeCommande.proforma.dureeAbonnementDevis != null)
                    {
                        nombreLigne += bonDeCommande.proforma.dureeAbonnementDevis.Count;
                    }
                    if (bonDeCommande.proforma.voyageAbonnementDevis != null)
                    {
                        nombreLigne += bonDeCommande.proforma.voyageAbonnementDevis.Count;
                    }
                    if (bonDeCommande.proforma.uSAbonnementNVDevis != null)
                    {
                        nombreLigne += bonDeCommande.proforma.uSAbonnementNVDevis.Count;
                    }

                    iTextSharp.text.Table tableCommande = new iTextSharp.text.Table(3, nombreLigne);
                    //tableCommande.Border = 1;
                    //tableCommande.DefaultCellBorder = 1;
                    tableCommande.Padding = 1;
                    tableCommande.Width = 100;
                    tableCommande.Widths = new float[] { 20, 60, 20 };

                    Phrase phraseDesigantion = new Phrase(designation);
                    phraseDesigantion.Font.Size = 8;
                    phraseDesigantion.Font.SetStyle(1);

                    Phrase phraseDetaile = new Phrase(detail);
                    phraseDetaile.Font.Size = 8;
                    phraseDetaile.Font.SetStyle(1);

                    Phrase phrasePrix = new Phrase(prixTotal);
                    phrasePrix.Font.Size = 8;
                    phrasePrix.Font.SetStyle(1);

                    tableCommande.AddCell(phraseDesigantion);
                    tableCommande.AddCell(phraseDetaile);
                    tableCommande.AddCell(phrasePrix);

                    Phrase phraseTempDesignation = null;
                    Phrase phraseTempDetail = null;
                    Phrase phraseTempPrix = null;

                    if (bonDeCommande.proforma.billetCommande != null)
                    {
                        for (int i = 0; i < bonDeCommande.proforma.billetCommande.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(bonDeCommande.proforma.billetCommande[i].NumBilletCommande);
                            phraseTempDesignation.Font.Size = 8;

                            if (bonDeCommande.proforma.billetCommande[i].trajet != null)
                            {
                                tempDetailBilletCommande = bonDeCommande.proforma.billetCommande[i].NombreBilletCommande + " billet(s) pour " + bonDeCommande.proforma.billetCommande[i].trajet.villeD.NomVille + "-" + bonDeCommande.proforma.billetCommande[i].trajet.villeF.NomVille;
                            }
                            if (bonDeCommande.proforma.billetCommande[i].calculCategorieBillet != null)
                            {
                                tempDetailBilletCommande += " Categorie: " + bonDeCommande.proforma.billetCommande[i].calculCategorieBillet.IndicateurPrixBillet;
                            }

                            phraseTempDetail = new Phrase(tempDetailBilletCommande);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((bonDeCommande.proforma.billetCommande[i].NombreBilletCommande * bonDeCommande.proforma.billetCommande[i].MontantBilletCommande).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailBilletCommande = "";
                        }
                    }

                    if (bonDeCommande.proforma.commissionDevis != null)
                    {
                        for (int i = 0; i < bonDeCommande.proforma.commissionDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(bonDeCommande.proforma.commissionDevis[i].IdCommissionDevis);
                            phraseTempDesignation.Font.Size = 8;

                            tempDetailCommissionDevis = "Commission, Type: " + bonDeCommande.proforma.commissionDevis[i].TypeCommission + " ";
                            tempDetailCommissionDevis += " Destination: " + bonDeCommande.proforma.commissionDevis[i].Destination;

                            phraseTempDetail = new Phrase(tempDetailCommissionDevis);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles(bonDeCommande.proforma.commissionDevis[i].FraisEnvoi.ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailCommissionDevis = "";
                        }
                    }

                    if (bonDeCommande.proforma.dureeAbonnementDevis != null)
                    {
                        for (int i = 0; i < bonDeCommande.proforma.dureeAbonnementDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(bonDeCommande.proforma.dureeAbonnementDevis[i].NumDureeAbonnementDevis);
                            phraseTempDesignation.Font.Size = 8;

                            if (bonDeCommande.proforma.dureeAbonnementDevis[i].trajet != null)
                            {
                                tempDetailDureeAbonnement = "Abonnement par durée de temp pour " + bonDeCommande.proforma.dureeAbonnementDevis[i].trajet.villeD.NomVille + "-" + bonDeCommande.proforma.dureeAbonnementDevis[i].trajet.villeF.NomVille + " ";
                                tempDetailDureeAbonnement += " Zone: " + bonDeCommande.proforma.dureeAbonnementDevis[i].Zone;
                            }
                            else
                            {
                                tempDetailDureeAbonnement = "Abonnement par durée de temp pour le zone " + bonDeCommande.proforma.dureeAbonnementDevis[i].Zone;
                            }

                            phraseTempDetail = new Phrase(tempDetailDureeAbonnement);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((bonDeCommande.proforma.dureeAbonnementDevis[i].NombreDureeAbonnement * bonDeCommande.proforma.dureeAbonnementDevis[i].PrixTotal).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailDureeAbonnement = "";
                        }
                    }

                    if (bonDeCommande.proforma.voyageAbonnementDevis != null)
                    {
                        for (int i = 0; i < bonDeCommande.proforma.voyageAbonnementDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(bonDeCommande.proforma.voyageAbonnementDevis[i].NumVoyageAbonnementDevis);
                            phraseTempDesignation.Font.Size = 8;

                            if (bonDeCommande.proforma.voyageAbonnementDevis[i].trajet != null)
                            {
                                tempDetailVoyageAbonnement = "Abonnement, " + bonDeCommande.proforma.voyageAbonnementDevis[i].NbVoyageAbonnement + " voyage(s) pour " + bonDeCommande.proforma.voyageAbonnementDevis[i].trajet.villeD.NomVille + "-" + bonDeCommande.proforma.voyageAbonnementDevis[i].trajet.villeF.NomVille + " ";
                                tempDetailVoyageAbonnement += " Zone: " + bonDeCommande.proforma.voyageAbonnementDevis[i].Zone;
                            }
                            else
                            {
                                tempDetailVoyageAbonnement = "Abonnement, " + bonDeCommande.proforma.voyageAbonnementDevis[i].NbVoyageAbonnement + " voyage(s), zone: " + bonDeCommande.proforma.voyageAbonnementDevis[i].Zone;

                            }

                            phraseTempDetail = new Phrase(tempDetailVoyageAbonnement);
                            phraseTempDetail.Font.Size = 8;

                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((bonDeCommande.proforma.voyageAbonnementDevis[i].NbVoyageAbonnement * bonDeCommande.proforma.voyageAbonnementDevis[i].PrixUnitaire).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailVoyageAbonnement = "";
                        }
                    }

                    if (bonDeCommande.proforma.uSAbonnementNVDevis != null)
                    {
                        for (int i = 0; i < bonDeCommande.proforma.uSAbonnementNVDevis.Count; i++)
                        {
                            phraseTempDesignation = new Phrase(bonDeCommande.proforma.uSAbonnementNVDevis[i].NumAbonnementNVDevis);
                            phraseTempDesignation.Font.Size = 8;

                            if (bonDeCommande.proforma.uSAbonnementNVDevis[i].zoneD != null && bonDeCommande.proforma.uSAbonnementNVDevis[i].zoneF != null)
                            {
                                tempDetailUSAbonnementNV = "Abonnement, " + bonDeCommande.proforma.uSAbonnementNVDevis[i].infoPasse.NombrePasse.ToString("0") + " voyage(s) pour " + bonDeCommande.proforma.uSAbonnementNVDevis[i].zoneD.NomZone + "-" + bonDeCommande.proforma.uSAbonnementNVDevis[i].zoneF.NomZone;
                                if (bonDeCommande.proforma.uSAbonnementNVDevis[i].MontantCarte > 0)
                                {
                                    tempDetailUSAbonnementNV += " Carte:" + serviceGeneral.separateurDesMilles(bonDeCommande.proforma.uSAbonnementNVDevis[i].MontantCarte.ToString("0")) + " Ar";
                                }
                            }
                            else
                            {
                                tempDetailUSAbonnementNV = "Abonnement, " + bonDeCommande.proforma.uSAbonnementNVDevis[i].infoPasse.NombrePasse.ToString("0") + " voyage(s)";
                                if (bonDeCommande.proforma.uSAbonnementNVDevis[i].MontantCarte > 0)
                                {
                                    tempDetailUSAbonnementNV += " Carte:" + serviceGeneral.separateurDesMilles(bonDeCommande.proforma.uSAbonnementNVDevis[i].MontantCarte.ToString("0")) + " Ar";
                                }
                            }

                            phraseTempDetail = new Phrase(tempDetailUSAbonnementNV);
                            phraseTempDetail.Font.Size = 8;


                            phraseTempPrix = new Phrase(serviceGeneral.separateurDesMilles((bonDeCommande.proforma.uSAbonnementNVDevis[i].MontantNV + bonDeCommande.proforma.uSAbonnementNVDevis[i].MontantCarte).ToString("0")) + "Ar");
                            phraseTempPrix.Font.Size = 8;

                            tableCommande.AddCell(phraseTempDesignation);
                            tableCommande.AddCell(phraseTempDetail);
                            tableCommande.AddCell(phraseTempPrix);

                            phraseTempDesignation = null;
                            phraseTempDetail = null;
                            phraseTempPrix = null;
                            tempDetailUSAbonnementNV = "";
                        }
                    }
                    #endregion

                    #region bon de commande
                    iTextSharp.text.Table tableBonCommande = new iTextSharp.text.Table(5, 2);
                    tableBonCommande.Border = 0;
                    tableBonCommande.DefaultCellBorder = 0;
                    tableBonCommande.Padding = 2;
                    tableBonCommande.Width = 100;
                    tableBonCommande.Widths = new float[] { 14,20,2,20,44};

                    Phrase phraseDatePaiement = new Phrase(datePaiement);
                    Phrase phraseDatePaiementVar = new Phrase(datePaiementVar);
                    phraseDatePaiement.Font.Size = 8;
                    phraseDatePaiementVar.Font.Size = 8;
                    phraseDatePaiementVar.Font.SetStyle(1);

                    Phrase phraseDescription = new Phrase(description);
                    Phrase phraseDescriptionVar = new Phrase(descritpionVar);
                    phraseDescription.Font.Size = 8;
                    phraseDescriptionVar.Font.Size = 8;
                    phraseDescriptionVar.Font.SetStyle(1);

                    Phrase phraseMontant = new Phrase(montant);
                    Phrase phraseMontantVar = new Phrase(montantVar);
                    phraseMontant.Font.Size = 8;
                    phraseMontantVar.Font.Size = 8;
                    phraseMontantVar.Font.SetStyle(1);

                    Phrase phraseMontantLettre = new Phrase(montantLettre);
                    Phrase phraseMontantLettreVar = new Phrase(montantLettreVar);
                    phraseMontantLettre.Font.Size = 8;
                    phraseMontantLettreVar.Font.Size = 8;
                    phraseMontantLettreVar.Font.SetStyle(1);

                    tableBonCommande.AddCell(phraseDatePaiement);
                    tableBonCommande.AddCell(phraseDatePaiementVar);
                    tableBonCommande.AddCell(phraseVide);
                    tableBonCommande.AddCell(phraseDescription);
                    tableBonCommande.AddCell(phraseDescriptionVar);
                    tableBonCommande.AddCell(phraseMontant);
                    tableBonCommande.AddCell(phraseMontantVar);
                    tableBonCommande.AddCell(phraseVide);
                    tableBonCommande.AddCell(phraseMontantLettre);
                    tableBonCommande.AddCell(phraseMontantLettreVar);

                    #endregion

                    #region ajout des element dans document
                    document.Add(tableLogo);

                    if (bonDeCommande.proforma.individu != null)
                    {
                        document.Add(tableClient);
                    }
                    if (bonDeCommande.proforma.societe != null)
                    {
                        document.Add(tableSociete);
                    }
                    if (bonDeCommande.proforma.organisme != null)
                    {
                        document.Add(tableOrganisme);
                    }
                    document.Add(phraseAlaLigne);
                    document.Add(tableCommande);
                    document.Add(tableBonCommande);
                    isPrint = true;
                    #endregion

                    #endregion

                    document.Close();
                }
            }
            #endregion

            return isPrint;
        }

        bool IntfDalServicePdf.printUSBillet(string numBillet, string urlSaving)
        {
            #region initialisation
            bool isPrint = false;
            crlUSBillet billet = null;
            string[] tabNumBillet;

            IntfDalUSBillet serviceUSBillet = new ImplDalUSBillet();
            serviceGeneral = new ImplDalGeneral();

            Convertisseuse convertisseuse = new Convertisseuse();

            Document document = new Document(PageSize.A8);
            document.SetMargins(5F, 5F, 5F, 5F);
            #endregion

            #region implementation
            if (numBillet != "")
            {
                tabNumBillet = numBillet.Split(';');

                if (tabNumBillet.Length > 0)
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(urlSaving, FileMode.Create));

                    document.Open();

                    PdfContentByte cb = writer.DirectContent;

                    for (int i = 0; i < tabNumBillet.Length ; i++)
                    {
                        billet = serviceUSBillet.selectUSBillet(tabNumBillet[i]);
                        if (billet != null)
                        {
                            #region initialisepdf
                            string grandTitre = "LG Trans";
                            string numBilletStr = "Billet N°";
                            string numBilletVar = billet.NumBillet;

                            string zoneStr = "Zone:";
                            string zoneVar = "";
                            if (billet.zoneD != null && billet.zoneF != null)
                            {
                                zoneVar = billet.zoneD.NomZone + "-" + billet.zoneF.NomZone;
                            }
                            else
                            {
                                zoneVar = billet.NumZoneD + "-" + billet.NumZoneF;
                            }

                            string prixBillet = "Prix: ";
                            string prixBilletVar = serviceGeneral.separateurDesMilles(billet.Montant.ToString("0")) + " Ar";

                            string valideAu = "Billet valide au: ";
                            string valideAuVar = billet.ValideAu.ToString("dd MMMM yyyy HH:mm");

                            string strBasPage = "Tehirizo ny tapakilanao!";
                            Phrase phraseVide = new Phrase(" ");
                            #endregion

                            #region cree pdf
                            iTextSharp.text.Table tableLogo = new iTextSharp.text.Table(1, 2);
                            tableLogo.Border = 0;
                            tableLogo.DefaultCellBorder = 0;
                            tableLogo.Padding = 2;
                            tableLogo.Width = 100;
                            tableLogo.Widths = new float[] { 100 };

                            Phrase paraGranTitre = new Phrase(grandTitre);
                            paraGranTitre.Font.Size = 12;
                            paraGranTitre.Font.SetStyle(1);

                            Phrase phraseNumBillet = new Phrase(numBilletStr);
                            Phrase phraseNumBilletVar = new Phrase(numBilletVar);
                            phraseNumBillet.Font.Size = 10;
                            phraseNumBillet.Font.SetStyle(1);
                            phraseNumBilletVar.Font.Size = 10;

                            tableLogo.AddCell(paraGranTitre);
                            tableLogo.AddCell(phraseNumBilletVar);

                            iTextSharp.text.Table tableContent = new iTextSharp.text.Table(2, 3);
                            tableContent.Border = 0;
                            tableContent.DefaultCellBorder = 0;
                            tableContent.Padding = 0;
                            tableContent.Width = 100;
                            tableContent.Widths = new float[] { 40, 60 };


                            Phrase phraseZone = new Phrase(zoneStr);
                            Phrase phraseZoneVar = new Phrase(zoneVar);
                            phraseZone.Font.Size = 8;
                            phraseZoneVar.Font.Size = 8;
                            phraseZoneVar.Font.SetStyle(1);

                            Phrase phrasePrix = new Phrase(prixBillet);
                            Phrase phrasePrixVar = new Phrase(prixBilletVar);
                            phrasePrix.Font.Size = 8;
                            phrasePrixVar.Font.Size = 8;
                            phrasePrixVar.Font.SetStyle(1);

                            Phrase phraseValide = new Phrase(valideAu);
                            Phrase phraseValideVar = new Phrase(valideAuVar);
                            phraseValide.Font.Size = 8;
                            phraseValideVar.Font.Size = 8;
                            phraseValideVar.Font.SetStyle(1);

                            tableContent.AddCell(phraseZone);
                            tableContent.AddCell(phraseZoneVar);
                            tableContent.AddCell(phrasePrix);
                            tableContent.AddCell(phrasePrixVar);
                            tableContent.AddCell(phraseValide);
                            tableContent.AddCell(phraseValideVar);

                            #region bas
                            iTextSharp.text.Table tableBas = new iTextSharp.text.Table(1, 1);
                            tableBas.Border = 0;
                            tableBas.DefaultCellBorder = 0;
                            tableBas.Padding = 0;
                            tableBas.Width = 100;
                            tableBas.Widths = new float[] { 100 };

                            Phrase phraseBas = new Phrase(strBasPage);
                            phraseBas.Font.Size = 6;

                            tableBas.AddCell(phraseBas);
                            #endregion

                            #region ajout des element dans document
                            document.Add(tableLogo);
                            //document.Add(tableNumBillet);
                            //document.Add(phraseVide);
                            document.Add(tableContent);
                            document.Add(tableBas);

                            isPrint = true;
                            #endregion

                            #endregion
                        }

                        billet = null;
                        if (i < (tabNumBillet.Length - 1))
                        {
                            document.NewPage();
                        }
                    }
                    document.Close();
                }
            }
            #endregion

            return isPrint;
        }
        #endregion



        
    }
}
