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
    public partial class Agent : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAgent serviceAgent = null;
        IntfDalTypeAgent serviceTypeAgent = null;
        IntfDalAgence serviceAgence = null;
        IntfDalLien serviceLien = null;
        IntfDalSituationFamiliale serviceSituationFamiliale = null;

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
            serviceAgent = new ImplDalAgent();
            serviceTypeAgent = new ImplDalTypeAgent();
            serviceAgence = new ImplDalAgence();
            serviceSituationFamiliale = new ImplDalSituationFamiliale();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                this.initialiseGridAgent();
                serviceTypeAgent.loadDddlTypeAgent(ddlTypeAgent);
                serviceAgence.loadDdlAgence(ddlAgence);
                try
                {
                    ddlAgence.SelectedValue = agent.numAgence;
                }
                catch (Exception)
                {
                }

                serviceSituationFamiliale.loadDddlSituationFamiliale(ddlSituationFamiliale);
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "043"))
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
            #region implementation
            ddlTypeAgent_RequiredFieldValidator.ErrorMessage = ReAgent.typeAgentNonVide;
            ddlAgence_RequiredFieldValidator.ErrorMessage = ReAgent.agenceNonVide;
            TextNomAgent_RequiredFieldValidator.ErrorMessage = ReAgent.nomNonVide;
            TextCINAgent_RequiredFieldValidator.ErrorMessage = ReAgent.cinNonVide;
            TextAdresse_RequiredFieldValidator.ErrorMessage = ReAgent.adresseNonVide;
            ddlSituationFamiliale_RequiredFieldValidator.ErrorMessage = ReAgent.situationFamilialeNonVide;
            #endregion
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

        private void initialiseGridAgent()
        {
            serviceAgent.insertToGridAgentListe(gvAgent, ddlTriAgent.SelectedValue, ddlTriAgent.SelectedValue, TextRechercheAgent.Text, agent.numAgence);
        }

        private void afficheAgent(string matriculeAgent)
        {
            #region declaration
            crlAgent agent = null;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                agent = serviceAgent.selectAgent(matriculeAgent);
                if (agent != null)
                {
                    try
                    {
                        ddlTypeAgent.SelectedValue = agent.typeAgent;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        ddlAgence.SelectedValue = agent.numAgence;
                    }
                    catch (Exception)
                    {
                    }

                    TextNomAgent.Text = agent.nomAgent;
                    TextPrenomAgent.Text = agent.prenomAgent;
                    TextDateNaissanceAgent.Text = agent.dateNaissanceAgent.ToString("dd MMMM yyyy");
                    TextLieuNaissanceAgent.Text = agent.lieuNaissanceAgent;
                    TextCINAgent.Text = agent.cinAgent;
                    TextAdresse.Text = agent.adresseAgent;
                    TexttelephoneFixeAgent.Text = agent.telephoneAgent;
                    TexttelephonePortableAgent.Text = agent.telephoneMobileAgent;
                    TextLoginAgent.Text = agent.loginAgent;

                    try
                    {
                        ddlSituationFamiliale.SelectedValue = agent.SituationFamilialeAgent;
                    }
                    catch (Exception)
                    {
                    }

                    hfMatriculeAgent.Value = agent.matriculeAgent;

                    imageAgent.ImageUrl = ConfigurationManager.AppSettings["urlImageAgent"] + agent.ImageAgent;

                    this.afficheCheck(agent.matriculeAgent);
                }
            }
            #endregion
        }

        private void initialiseFormulaireAgent()
        {
            try
            {
                ddlTypeAgent.SelectedValue = "";
            }
            catch (Exception)
            {
            }
            try
            {
                ddlAgence.SelectedValue = agent.numAgence;
            }
            catch (Exception)
            {
            }

            TextNomAgent.Text = "";
            TextPrenomAgent.Text = "";
            TextDateNaissanceAgent.Text = "";
            TextLieuNaissanceAgent.Text = "";
            TextCINAgent.Text = "";
            TextAdresse.Text = "";
            TexttelephoneFixeAgent.Text = "";
            TexttelephonePortableAgent.Text = "";
            TextLoginAgent.Text = "";

            try
            {
                ddlSituationFamiliale.SelectedValue = "";
            }
            catch (Exception)
            {
            }

            hfMatriculeAgent.Value = "";
            imageAgent.ImageUrl = "";
            this.divIndicationText("", "Red");

            this.initialiseCheck();
        }

        private void insertToObjetAgent(crlAgent agentObj)
        {
            #region implementation
            if (agentObj != null)
            {
                agentObj.agence = agent.agence;
                agentObj.adresseAgent = TextAdresse.Text;
                agentObj.cinAgent = TextCINAgent.Text;
                try
                {
                    agentObj.dateNaissanceAgent = Convert.ToDateTime(TextDateNaissanceAgent.Text);
                }
                catch (Exception)
                {
                }
                agentObj.lieuNaissanceAgent = TextLieuNaissanceAgent.Text;
                if (TextLoginAgent.Text != "")
                {
                    agentObj.loginAgent = TextLoginAgent.Text;
                }
                if (TextMotDePasseAgent.Text != "")
                {
                    agentObj.motDePasseAgent = TextMotDePasseAgent.Text;
                }
                agentObj.nomAgent = TextNomAgent.Text;
                agentObj.numAgence = ddlAgence.SelectedValue;
                agentObj.prenomAgent = TextPrenomAgent.Text;
                agentObj.telephoneAgent = TexttelephoneFixeAgent.Text;
                agentObj.telephoneMobileAgent = TexttelephonePortableAgent.Text;
                agentObj.typeAgent = ddlTypeAgent.SelectedValue;
                agentObj.SituationFamilialeAgent = ddlSituationFamiliale.SelectedValue;
            }
            #endregion
        }

        private void insertAgent(crlAgent agentObj)
        {
            #region delcaration
            string strIndication = "";
            int isAgent = 0;
            string fileName = "";
            string[] tableFileName;
            string urlSaving = "";
            crlAgent agentTemp = null;
            #endregion

            #region implementation
            if (agentObj != null)
            {
                isAgent = serviceAgent.isAgent(agentObj);

                if (isAgent == 0)
                {
                    agentObj.matriculeAgent = serviceAgent.insertAgent(agentObj);

                    if (agentObj.matriculeAgent != "")
                    {
                        this.enregistrePageAgent(agentObj.matriculeAgent);

                        strIndication = "Agent " + agentObj.prenomAgent + " " + agentObj.nomAgent + " bien enregistrer!";
                        this.initialiseGridAgent();
                        this.divIndicationText(strIndication, "Black");

                        if (FileUpload_ImageAgent.HasFile)
                        {
                            fileName = FileUpload_ImageAgent.FileName;

                            tableFileName = fileName.Split('.');
                            if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg"))
                            {
                                urlSaving = this.urlSavingImageAgent(agentObj.matriculeAgent, "jpg");

                                FileUpload_ImageAgent.SaveAs(urlSaving);

                                agentObj.ImageAgent = agentObj.matriculeAgent.Replace('/', '_') + ".jpg";
                            }
                            else if (tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                            {
                                urlSaving = this.urlSavingImageAgent(agentObj.matriculeAgent, "png");

                                FileUpload_ImageAgent.SaveAs(urlSaving);

                                agentObj.ImageAgent = agentObj.matriculeAgent.Replace('/', '_') + ".png";
                            }

                            serviceAgent.updateAgent(agentObj);


                        }
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else if (isAgent == 1)
                {
                    agentTemp = serviceAgent.selectAgent("cinAgent", agentObj.cinAgent);

                    if (agentTemp != null)
                    {
                        strIndication = "Le CIN est déjà enregistrer sous le nom de " + agentTemp.prenomAgent + " " + agentTemp.nomAgent + "!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else if (isAgent == 2)
                {
                    strIndication = "Login déjà prise par un autre utilisateur!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            #endregion
        }

        private void updateAgent(crlAgent agentObj)
        {
            #region delcaration
            string strIndication = "";
            int isAgent = 0;
            string fileName = "";
            string[] tableFileName;
            string urlSaving = "";
            crlAgent agentTemp = null;
            #endregion

            #region implementation
            if (agentObj != null)
            {
                isAgent = serviceAgent.isAgent(agentObj);

                if (isAgent == 0)
                {

                    if (agentObj.matriculeAgent != "")
                    {

                        if (FileUpload_ImageAgent.HasFile)
                        {
                            fileName = FileUpload_ImageAgent.FileName;

                            tableFileName = fileName.Split('.');
                            if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg"))
                            {
                                urlSaving = this.urlSavingImageAgent(agentObj.matriculeAgent, "jpg");

                                FileUpload_ImageAgent.SaveAs(urlSaving);

                                agentObj.ImageAgent = agentObj.matriculeAgent.Replace('/', '_') + ".jpg";
                            }
                            else if (tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                            {
                                urlSaving = this.urlSavingImageAgent(agentObj.matriculeAgent, "png");

                                FileUpload_ImageAgent.SaveAs(urlSaving);

                                agentObj.ImageAgent = agentObj.matriculeAgent.Replace('/', '_') + ".png";
                            }

                        }
                    }

                    if (serviceAgent.updateAgent(agentObj))
                    {
                        this.enregistrePageAgent(agentObj.matriculeAgent);

                        strIndication = "Modification de l'agent " + agentObj.prenomAgent + " " + agentObj.nomAgent + " bien enregistrer!";
                        this.divIndicationText(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant l'enregistrement!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else if (isAgent == 1)
                {
                    agentTemp = serviceAgent.selectAgent("cinAgent", agentObj.cinAgent);

                    if (agentTemp != null)
                    {
                        strIndication = "Le CIN est déjà enregistrer sous le nom de " + agentTemp.prenomAgent + " " + agentTemp.nomAgent + "!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
                else if (isAgent == 2)
                {
                    strIndication = "Login déjà prise par un autre utilisateur!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            #endregion
        }

        private bool testImageAgent()
        {
            #region declaration
            bool isFileImage = false;
            string fileName = "";
            string[] tableFileName;
            #endregion

            #region implementation
            if (FileUpload_ImageAgent.HasFile)
            {
                fileName = FileUpload_ImageAgent.FileName;

                tableFileName = fileName.Split('.');
                if (tableFileName[tableFileName.Length - 1].ToLower().Equals("jpg") || tableFileName[tableFileName.Length - 1].ToLower().Equals("png"))
                {
                    isFileImage = true;
                }

            }
            else
            {
                isFileImage = true;
            }
            #endregion

            return isFileImage;
        }

        private string urlSavingImageAgent(string matriculeAgent, string fichier)
        {
            #region declaration
            string urlImageSaving = "";
            string urlSaving = "";
            #endregion

            #region implementation
            urlImageSaving = ConfigurationManager.AppSettings["urlImageAgent"] + matriculeAgent.Replace('/', '_') + "." + fichier;
            urlSaving = @Server.MapPath(urlImageSaving);
            #endregion

            return urlSaving;
        }

        private void initialiseCheck()
        {
            Check_010.Checked = false;
            Check_011.Checked = false;
            Check_012.Checked = false;
            Check_013.Checked = false;
            Check_014.Checked = false;
            Check_020.Checked = false;
            Check_021.Checked = false;
            Check_022.Checked = false;
            Check_023.Checked = false;
            Check_030.Checked = false;
            Check_031.Checked = false;
            Check_032.Checked = false;
            Check_033.Checked = false;
            Check_034.Checked = false;
            Check_040.Checked = false;
            Check_041.Checked = false;
            Check_042.Checked = false;
            Check_043.Checked = false;
            Check_050.Checked = false;
            Check_051.Checked = false;
            Check_060.Checked = false;
            Check_061.Checked = false;
            Check_062.Checked = false;
            Check_063.Checked = false;

            Check_100.Checked = false;
            Check_101.Checked = false;

            Check_200.Checked = false;
            Check_201.Checked = false;
            Check_202.Checked = false;
            Check_203.Checked = false;
            Check_210.Checked = false;
            Check_211.Checked = false;
            Check_212.Checked = false;
            Check_220.Checked = false;
            Check_221.Checked = false;
            Check_230.Checked = false;
            Check_231.Checked = false;
            Check_300.Checked = false;
            Check_301.Checked = false;
            Check_302.Checked = false;
            Check_310.Checked = false;
            Check_311.Checked = false;
            Check_320.Checked = false;
            Check_321.Checked = false;
            Check_330.Checked = false;
            Check_331.Checked = false;
            Check_332.Checked = false;
            Check_340.Checked = false;
            Check_341.Checked = false;
            Check_350.Checked = false;

            Check_400.Checked = false;
            Check_401.Checked = false;
            Check_410.Checked = false;
            Check_411.Checked = false;
            Check_420.Checked = false;
            Check_421.Checked = false;
            Check_422.Checked = false;
            Check_430.Checked = false;
            Check_431.Checked = false;
            Check_432.Checked = false;
        }

        private void afficheCheck(string matriculeAgent)
        {
            if (serviceLien.isPageAgent(matriculeAgent, "010"))
            {
                Check_010.Checked = true;
            }
            else
            {
                Check_010.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "011"))
            {
                Check_011.Checked = true;
            }
            else
            {
                Check_011.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "012"))
            {
                Check_012.Checked = true;
            }
            else
            {
                Check_012.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "013"))
            {
                Check_013.Checked = true;
            }
            else
            {
                Check_013.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "014"))
            {
                Check_014.Checked = true;
            }
            else
            {
                Check_014.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "020"))
            {
                Check_020.Checked = true;
            }
            else
            {
                Check_020.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "021"))
            {
                Check_021.Checked = true;
            }
            else
            {
                Check_021.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "022"))
            {
                Check_022.Checked = true;
            }
            else
            {
                Check_022.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "023"))
            {
                Check_023.Checked = true;
            }
            else
            {
                Check_023.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "030"))
            {
                Check_030.Checked = true;
            }
            else
            {
                Check_030.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "031"))
            {
                Check_031.Checked = true;
            }
            else
            {
                Check_031.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "032"))
            {
                Check_032.Checked = true;
            }
            else
            {
                Check_032.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "033"))
            {
                Check_033.Checked = true;
            }
            else
            {
                Check_033.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "034"))
            {
                Check_034.Checked = true;
            }
            else
            {
                Check_034.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "040"))
            {
                Check_040.Checked = true;
            }
            else
            {
                Check_040.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "041"))
            {
                Check_041.Checked = true;
            }
            else
            {
                Check_041.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "042"))
            {
                Check_042.Checked = true;
            }
            else
            {
                Check_042.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "043"))
            {
                Check_043.Checked = true;
            }
            else
            {
                Check_043.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "050"))
            {
                Check_050.Checked = true;
            }
            else
            {
                Check_050.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "051"))
            {
                Check_051.Checked = true;
            }
            else
            {
                Check_051.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "060"))
            {
                Check_060.Checked = true;
            }
            else
            {
                Check_060.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "061"))
            {
                Check_061.Checked = true;
            }
            else
            {
                Check_061.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "062"))
            {
                Check_062.Checked = true;
            }
            else
            {
                Check_062.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "063"))
            {
                Check_063.Checked = true;
            }
            else
            {
                Check_063.Checked = false;
            }


            if (serviceLien.isPageAgent(matriculeAgent, "100"))
            {
                Check_100.Checked = true;
            }
            else
            {
                Check_100.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "101"))
            {
                Check_101.Checked = true;
            }
            else
            {
                Check_101.Checked = false;
            }

            if (serviceLien.isPageAgent(matriculeAgent, "200"))
            {
                Check_200.Checked = true;
            }
            else
            {
                Check_200.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "201"))
            {
                Check_201.Checked = true;
            }
            else
            {
                Check_201.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "202"))
            {
                Check_202.Checked = true;
            }
            else
            {
                Check_202.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "203"))
            {
                Check_203.Checked = true;
            }
            else
            {
                Check_203.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "210"))
            {
                Check_210.Checked = true;
            }
            else
            {
                Check_210.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "211"))
            {
                Check_211.Checked = true;
            }
            else
            {
                Check_211.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "212"))
            {
                Check_212.Checked = true;
            }
            else
            {
                Check_212.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "220"))
            {
                Check_220.Checked = true;
            }
            else
            {
                Check_220.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "221"))
            {
                Check_221.Checked = true;
            }
            else
            {
                Check_221.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "230"))
            {
                Check_230.Checked = true;
            }
            else
            {
                Check_230.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "231"))
            {
                Check_231.Checked = true;
            }
            else
            {
                Check_231.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "300"))
            {
                Check_300.Checked = true;
            }
            else
            {
                Check_300.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "301"))
            {
                Check_301.Checked = true;
            }
            else
            {
                Check_301.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "302"))
            {
                Check_302.Checked = true;
            }
            else
            {
                Check_302.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "310"))
            {
                Check_310.Checked = true;
            }
            else
            {
                Check_310.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "311"))
            {
                Check_311.Checked = true;
            }
            else
            {
                Check_311.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "320"))
            {
                Check_320.Checked = true;
            }
            else
            {
                Check_320.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "321"))
            {
                Check_321.Checked = true;
            }
            else
            {
                Check_321.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "330"))
            {
                Check_330.Checked = true;
            }
            else
            {
                Check_330.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "331"))
            {
                Check_331.Checked = true;
            }
            else
            {
                Check_331.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "332"))
            {
                Check_332.Checked = true;
            }
            else
            {
                Check_332.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "340"))
            {
                Check_340.Checked = true;
            }
            else
            {
                Check_340.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "341"))
            {
                Check_341.Checked = true;
            }
            else
            {
                Check_341.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "350"))
            {
                Check_350.Checked = true;
            }
            else
            {
                Check_350.Checked = false;
            }

            if (serviceLien.isPageAgent(matriculeAgent, "400"))
            {
                Check_400.Checked = true;
            }
            else
            {
                Check_400.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "401"))
            {
                Check_401.Checked = true;
            }
            else
            {
                Check_401.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "410"))
            {
                Check_410.Checked = true;
            }
            else
            {
                Check_410.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "411"))
            {
                Check_411.Checked = true;
            }
            else
            {
                Check_411.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "420"))
            {
                Check_420.Checked = true;
            }
            else
            {
                Check_420.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "421"))
            {
                Check_421.Checked = true;
            }
            else
            {
                Check_421.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "422"))
            {
                Check_422.Checked = true;
            }
            else
            {
                Check_422.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "430"))
            {
                Check_430.Checked = true;
            }
            else
            {
                Check_430.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "431"))
            {
                Check_431.Checked = true;
            }
            else
            {
                Check_431.Checked = false;
            }
            if (serviceLien.isPageAgent(matriculeAgent, "432"))
            {
                Check_432.Checked = true;
            }
            else
            {
                Check_432.Checked = false;
            }

        }

        private void afficheCheckTypeAgent(string typeAgent)
        {
            if (serviceLien.isPageTypeAgent(typeAgent, "010"))
            {
                Check_010.Checked = true;
            }
            else
            {
                Check_010.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "011"))
            {
                Check_011.Checked = true;
            }
            else
            {
                Check_011.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "012"))
            {
                Check_012.Checked = true;
            }
            else
            {
                Check_012.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "013"))
            {
                Check_013.Checked = true;
            }
            else
            {
                Check_013.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "014"))
            {
                Check_014.Checked = true;
            }
            else
            {
                Check_014.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "020"))
            {
                Check_020.Checked = true;
            }
            else
            {
                Check_020.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "021"))
            {
                Check_021.Checked = true;
            }
            else
            {
                Check_021.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "022"))
            {
                Check_022.Checked = true;
            }
            else
            {
                Check_022.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "023"))
            {
                Check_023.Checked = true;
            }
            else
            {
                Check_023.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "030"))
            {
                Check_030.Checked = true;
            }
            else
            {
                Check_030.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "031"))
            {
                Check_031.Checked = true;
            }
            else
            {
                Check_031.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "032"))
            {
                Check_032.Checked = true;
            }
            else
            {
                Check_032.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "033"))
            {
                Check_033.Checked = true;
            }
            else
            {
                Check_033.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "034"))
            {
                Check_034.Checked = true;
            }
            else
            {
                Check_034.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "040"))
            {
                Check_040.Checked = true;
            }
            else
            {
                Check_040.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "041"))
            {
                Check_041.Checked = true;
            }
            else
            {
                Check_041.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "042"))
            {
                Check_042.Checked = true;
            }
            else
            {
                Check_042.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "043"))
            {
                Check_043.Checked = true;
            }
            else
            {
                Check_043.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "050"))
            {
                Check_050.Checked = true;
            }
            else
            {
                Check_050.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "051"))
            {
                Check_051.Checked = true;
            }
            else
            {
                Check_051.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "060"))
            {
                Check_060.Checked = true;
            }
            else
            {
                Check_060.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "061"))
            {
                Check_061.Checked = true;
            }
            else
            {
                Check_061.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "062"))
            {
                Check_062.Checked = true;
            }
            else
            {
                Check_062.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "063"))
            {
                Check_063.Checked = true;
            }
            else
            {
                Check_063.Checked = false;
            }


            if (serviceLien.isPageTypeAgent(typeAgent, "100"))
            {
                Check_100.Checked = true;
            }
            else
            {
                Check_100.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "101"))
            {
                Check_101.Checked = true;
            }
            else
            {
                Check_101.Checked = false;
            }

            if (serviceLien.isPageTypeAgent(typeAgent, "200"))
            {
                Check_200.Checked = true;
            }
            else
            {
                Check_200.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "201"))
            {
                Check_201.Checked = true;
            }
            else
            {
                Check_201.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "202"))
            {
                Check_202.Checked = true;
            }
            else
            {
                Check_202.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "203"))
            {
                Check_203.Checked = true;
            }
            else
            {
                Check_203.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "210"))
            {
                Check_210.Checked = true;
            }
            else
            {
                Check_210.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "211"))
            {
                Check_211.Checked = true;
            }
            else
            {
                Check_211.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "212"))
            {
                Check_212.Checked = true;
            }
            else
            {
                Check_212.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "220"))
            {
                Check_220.Checked = true;
            }
            else
            {
                Check_220.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "221"))
            {
                Check_221.Checked = true;
            }
            else
            {
                Check_221.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "230"))
            {
                Check_230.Checked = true;
            }
            else
            {
                Check_230.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "231"))
            {
                Check_231.Checked = true;
            }
            else
            {
                Check_231.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "300"))
            {
                Check_300.Checked = true;
            }
            else
            {
                Check_300.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "301"))
            {
                Check_301.Checked = true;
            }
            else
            {
                Check_301.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "302"))
            {
                Check_302.Checked = true;
            }
            else
            {
                Check_302.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "310"))
            {
                Check_310.Checked = true;
            }
            else
            {
                Check_310.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "311"))
            {
                Check_311.Checked = true;
            }
            else
            {
                Check_311.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "320"))
            {
                Check_320.Checked = true;
            }
            else
            {
                Check_320.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "321"))
            {
                Check_321.Checked = true;
            }
            else
            {
                Check_321.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "330"))
            {
                Check_330.Checked = true;
            }
            else
            {
                Check_330.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "331"))
            {
                Check_331.Checked = true;
            }
            else
            {
                Check_331.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "332"))
            {
                Check_332.Checked = true;
            }
            else
            {
                Check_332.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "340"))
            {
                Check_340.Checked = true;
            }
            else
            {
                Check_340.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "341"))
            {
                Check_341.Checked = true;
            }
            else
            {
                Check_341.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "350"))
            {
                Check_350.Checked = true;
            }
            else
            {
                Check_350.Checked = false;
            }

            if (serviceLien.isPageTypeAgent(typeAgent, "400"))
            {
                Check_400.Checked = true;
            }
            else
            {
                Check_400.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "401"))
            {
                Check_401.Checked = true;
            }
            else
            {
                Check_401.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "410"))
            {
                Check_410.Checked = true;
            }
            else
            {
                Check_410.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "411"))
            {
                Check_411.Checked = true;
            }
            else
            {
                Check_411.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "420"))
            {
                Check_420.Checked = true;
            }
            else
            {
                Check_420.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "421"))
            {
                Check_421.Checked = true;
            }
            else
            {
                Check_421.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "422"))
            {
                Check_422.Checked = true;
            }
            else
            {
                Check_422.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "430"))
            {
                Check_430.Checked = true;
            }
            else
            {
                Check_430.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "431"))
            {
                Check_431.Checked = true;
            }
            else
            {
                Check_431.Checked = false;
            }
            if (serviceLien.isPageTypeAgent(typeAgent, "432"))
            {
                Check_432.Checked = true;
            }
            else
            {
                Check_432.Checked = false;
            }
        }

        private void enregistrePageAgent(string matriculeAgent)
        {
            serviceLien.insertAssocAgentLien(matriculeAgent, "001");


            if (Check_010.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "010");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "010");
            }
            if (Check_011.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "011");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "011");
            }
            if (Check_012.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "012");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "012");
            }
            if (Check_013.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "013");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "013");
            }
            if (Check_014.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "014");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "014");
            }
            if (Check_020.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "020");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "020");
            }
            if (Check_021.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "021");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "021");
            }
            if (Check_022.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "022");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "022");
            }
            if (Check_023.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "023");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "023");
            }
            if (Check_030.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "030");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "030");
            }
            if (Check_031.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "031");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "031");
            }
            if (Check_032.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "032");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "032");
            }
            if (Check_033.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "033");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "033");
            }
            if (Check_034.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "034");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "034");
            }
            if (Check_040.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "040");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "040");
            }
            if (Check_041.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "041");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "041");
            }
            if (Check_042.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "042");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "042");
            }
            if (Check_043.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "043");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "043");
            }
            if (Check_050.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "050");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "050");
            }
            if (Check_051.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "051");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "051");
            }
            if (Check_060.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "060");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "060");
            }
            if (Check_061.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "061");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "061");
            }
            if (Check_062.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "062");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "062");
            }
            if (Check_063.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "063");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "063");
            }


            if (Check_100.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "100");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "100");
            }
            if (Check_101.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "101");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "101");
            }

            if (Check_200.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "200");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "200");
            }

            if (Check_201.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "201");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "201");
            }
            if (Check_202.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "202");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "202");
            }
            if (Check_203.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "203");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "203");
            }
            if (Check_210.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "210");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "210");
            }
            if (Check_211.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "211");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "211");
            }
            if (Check_212.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "212");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "212");
            }
            if (Check_220.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "220");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "220");
            }
            if (Check_221.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "221");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "221");
            };
            if (Check_230.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "230");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "230");
            }
            if (Check_231.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "231");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "231");
            }
            if (Check_300.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "300");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "300");
            }
            if (Check_301.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "301");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "301");
            }
            if (Check_302.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "302");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "302");
            }
            if (Check_310.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "310");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "310");
            }
            if (Check_311.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "311");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "311");
            }
            if (Check_320.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "320");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "320");
            }
            if (Check_321.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "321");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "321");
            }
            if (Check_330.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "330");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "330");
            }
            if (Check_331.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "331");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "331");
            }
            if (Check_332.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "332");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "332");
            }
            if (Check_340.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "340");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "340");
            }
            if (Check_341.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "341");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "341");
            }
            if (Check_350.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "350");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "350");
            }


            if (Check_400.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "400");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "400");
            }
            if (Check_401.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "401");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "401");
            }
            if (Check_410.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "410");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "410");
            }
            if (Check_411.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "411");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "411");
            }
            if (Check_420.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "420");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "420");
            }
            if (Check_421.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "421");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "421");
            }
            if (Check_422.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "422");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "422");
            }
            if (Check_430.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "430");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "430");
            }
            if (Check_431.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "431");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "431");
            }
            if (Check_432.Checked)
            {
                serviceLien.insertAssocAgentLien(matriculeAgent, "432");
            }
            else
            {
                serviceLien.deleteAssocAgentLien(matriculeAgent, "432");
            }
        }
        #endregion

        #region event
        protected void gvAgent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAgent.PageIndex = e.NewPageIndex;
            this.initialiseGridAgent();
        }
        protected void gvAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheAgent(e.CommandArgument.ToString());
            }
        }
        protected void btnRechercheAgent_Click(object sender, EventArgs e)
        {
            this.initialiseGridAgent();
        }
        protected void ddlTriAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridAgent();
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAgent agentObj = null;
            #endregion

            #region implementation
            agentObj = new crlAgent();
            this.insertToObjetAgent(agentObj);
            this.insertAgent(agentObj);
            #endregion
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlAgent agentObj = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfMatriculeAgent.Value != "")
            {
                agentObj = serviceAgent.selectAgent(hfMatriculeAgent.Value);
                if (agentObj != null)
                {
                    this.insertToObjetAgent(agentObj);
                    this.updateAgent(agentObj);
                }
                else
                {
                    //
                }
            }
            else
            {
                strIndication = "Veuillez séléctionner un agent dans la liste avant de faire une modification!";
                this.divIndicationText(strIndication, "Red");
            }
            #endregion
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireAgent();
        }


        protected void onCheck_CheckedChanged(object sender, EventArgs e)
        {
            #region declaration
            CheckBox checkB = (CheckBox)sender;
            #endregion

            #region implementation
            if (checkB.ID.Equals("Check_010"))
            {
                if (!checkB.Checked)
                {
                    Check_011.Checked = false;
                    Check_012.Checked = false;
                    Check_013.Checked = false;
                    Check_014.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_011") || checkB.ID.Equals("Check_012") ||
                        checkB.ID.Equals("Check_013") || checkB.ID.Equals("Check_014"))
            {
                if (checkB.Checked)
                {
                    Check_010.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_020"))
            {
                if (!checkB.Checked)
                {
                    Check_021.Checked = false;
                    Check_022.Checked = false;
                    Check_023.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_021") || checkB.ID.Equals("Check_022") ||
                        checkB.ID.Equals("Check_023"))
            {
                if (checkB.Checked)
                {
                    Check_020.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_030"))
            {
                if (!checkB.Checked)
                {
                    Check_031.Checked = false;
                    Check_032.Checked = false;
                    Check_033.Checked = false;
                    Check_034.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_031") || checkB.ID.Equals("Check_032") ||
                        checkB.ID.Equals("Check_033") || checkB.ID.Equals("Check_034"))
            {
                if (checkB.Checked)
                {
                    Check_030.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_040"))
            {
                if (!checkB.Checked)
                {
                    Check_041.Checked = false;
                    Check_042.Checked = false;
                    Check_043.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_041") || checkB.ID.Equals("Check_042") ||
                        checkB.ID.Equals("Check_043"))
            {
                if (checkB.Checked)
                {
                    Check_040.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_050"))
            {
                if (!checkB.Checked)
                {
                    Check_051.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_051"))
            {
                if (checkB.Checked)
                {
                    Check_050.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_060"))
            {
                if (!checkB.Checked)
                {
                    Check_061.Checked = false;
                    Check_062.Checked = false;
                    Check_063.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_061") || checkB.ID.Equals("Check_062") ||
                        checkB.ID.Equals("Check_063") || checkB.ID.Equals("Check_064"))
            {
                if (checkB.Checked)
                {
                    Check_060.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_100"))
            {
                if (!checkB.Checked)
                {
                    Check_101.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_101"))
            {
                if (checkB.Checked)
                {
                    Check_100.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_200"))
            {
                if (!checkB.Checked)
                {
                    Check_201.Checked = false;
                    Check_202.Checked = false;
                    Check_203.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_201") || checkB.ID.Equals("Check_202") ||
                        checkB.ID.Equals("Check_203"))
            {
                if (checkB.Checked)
                {
                    Check_200.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_210"))
            {
                if (!checkB.Checked)
                {
                    Check_211.Checked = false;
                    Check_212.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_211") || checkB.ID.Equals("Check_212"))
            {
                if (checkB.Checked)
                {
                    Check_210.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_220"))
            {
                if (!checkB.Checked)
                {
                    Check_221.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_221"))
            {
                if (checkB.Checked)
                {
                    Check_220.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_230"))
            {
                if (!checkB.Checked)
                {
                    Check_231.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_231"))
            {
                if (checkB.Checked)
                {
                    Check_230.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_300"))
            {
                if (!checkB.Checked)
                {
                    Check_301.Checked = false;
                    Check_302.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_301") || checkB.ID.Equals("Check_302"))
            {
                if (checkB.Checked)
                {
                    Check_300.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_310"))
            {
                if (!checkB.Checked)
                {
                    Check_311.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_311"))
            {
                if (checkB.Checked)
                {
                    Check_310.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_320"))
            {
                if (!checkB.Checked)
                {
                    Check_321.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_321"))
            {
                if (checkB.Checked)
                {
                    Check_320.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_330"))
            {
                if (!checkB.Checked)
                {
                    Check_331.Checked = false;
                    Check_332.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_331") || checkB.ID.Equals("Check_332"))
            {
                if (checkB.Checked)
                {
                    Check_330.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_340"))
            {
                if (!checkB.Checked)
                {
                    Check_341.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_341"))
            {
                if (checkB.Checked)
                {
                    Check_340.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_400"))
            {
                if (!checkB.Checked)
                {
                    Check_401.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_401"))
            {
                if (checkB.Checked)
                {
                    Check_400.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_410"))
            {
                if (!checkB.Checked)
                {
                    Check_411.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_411"))
            {
                if (checkB.Checked)
                {
                    Check_410.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_420"))
            {
                if (!checkB.Checked)
                {
                    Check_421.Checked = false;
                    Check_422.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_421") || checkB.ID.Equals("Check_422"))
            {
                if (checkB.Checked)
                {
                    Check_420.Checked = true;
                }
            }
            else if (checkB.ID.Equals("Check_430"))
            {
                if (!checkB.Checked)
                {
                    Check_431.Checked = false;
                    Check_432.Checked = false;
                }
            }
            else if (checkB.ID.Equals("Check_431") || checkB.ID.Equals("Check_432"))
            {
                if (checkB.Checked)
                {
                    Check_430.Checked = true;
                }
            }

            #endregion
        }
        #endregion

        protected void ddlTypeAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region implementation
            if (ddlTypeAgent.SelectedValue.Equals(""))
            {
                this.initialiseCheck();
            }
            else
            {
                this.afficheCheckTypeAgent(ddlTypeAgent.SelectedValue);
            }
            #endregion
        }
    }
}