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
    public partial class TypeAgent : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalLien serviceLien = null;
        IntfDalTypeAgent serviceTypeAgent = null;
        IntfDalGeneral serviceGeneral = null;

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
            serviceTypeAgent = new ImplDalTypeAgent();
            serviceGeneral = new ImplDalGeneral();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseFormulaire();
                this.initialiseGridTypeAgent();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "014"))
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
            TextTypeAgent_RequiredFieldValidator.ErrorMessage = ReTypeAgent.typeAgentNonVide;
        }

        private void initialiseFormulaire()
        {
            #region implementation
            TextCommentaire.Text = "";
            TextTypeAgent.Text = "";
            hfTypeAgent.Value = "";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;

            btnModifier_ConfirmButtonExtender.ConfirmText = "";

            this.divIndicationText("", "Red");
            this.initialiseCheck();
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

        private void initialiseGridTypeAgent()
        {
            serviceTypeAgent.insertToGridTypeAgent(gvTypeAgent, "typeagent.typeAgent", "typeagent.typeAgent", "");
        }

        private void insertToObjTypeAgent(crlTypeAgent typeAgent)
        {
            #region implementation
            if (typeAgent != null)
            {
                typeAgent.typeAgent = TextTypeAgent.Text;
                typeAgent.CommentaireTypeAgent = TextCommentaire.Text;
            }
            #endregion
        }

        private void afficheTypeAgent(string typeAgent)
        {
            #region declaration
            crlTypeAgent objTypeAgent = null;
            string strConfirm = "";
            #endregion

            #region implemenation
            if (typeAgent != "")
            {
                objTypeAgent = serviceTypeAgent.selectTypeAgent(typeAgent);
                if (objTypeAgent != null)
                {
                    TextCommentaire.Text = objTypeAgent.CommentaireTypeAgent;
                    TextTypeAgent.Text = objTypeAgent.typeAgent;
                    hfTypeAgent.Value = objTypeAgent.typeAgent;

                    strConfirm = "Voulez vous vraiment modifier le type agent " + objTypeAgent.typeAgent + "?";
                    btnModifier_ConfirmButtonExtender.ConfirmText = strConfirm;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;

                    this.afficheCheck(objTypeAgent.typeAgent);
                }
            }
            #endregion
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

        private void afficheCheck(string typeAgent)
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

        private void enregistrePageAgent(string typeAgent)
        {

            serviceLien.insertAssocTypeAgentLien(typeAgent, "001");

            if (Check_010.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "010");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "010");
            }
            if (Check_011.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "011");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "011");
            }
            if (Check_012.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "012");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "012");
            }
            if (Check_013.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "013");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "013");
            }
            if (Check_014.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "014");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "014");
            }
            if (Check_020.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "020");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "020");
            }
            if (Check_021.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "021");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "021");
            }
            if (Check_022.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "022");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "022");
            }
            if (Check_023.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "023");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "023");
            }
            if (Check_030.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "030");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "030");
            }
            if (Check_031.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "031");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "031");
            }
            if (Check_032.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "032");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "032");
            }
            if (Check_033.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "033");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "033");
            }
            if (Check_034.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "034");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "034");
            }
            if (Check_040.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "040");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "040");
            }
            if (Check_041.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "041");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "041");
            }
            if (Check_042.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "042");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "042");
            }
            if (Check_043.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "043");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "043");
            }
            if (Check_050.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "050");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "050");
            }
            if (Check_051.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "051");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "051");
            }
            if (Check_060.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "060");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "060");
            }
            if (Check_061.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "061");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "061");
            }
            if (Check_062.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "062");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "062");
            }
            if (Check_063.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "063");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "063");
            }


            if (Check_100.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "100");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "100");
            }
            if (Check_101.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "101");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "101");
            }

            if (Check_200.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "200");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "200");
            }

            if (Check_201.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "201");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "201");
            }
            if (Check_202.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "202");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "202");
            }
            if (Check_203.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "203");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "203");
            }
            if (Check_210.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "210");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "210");
            }
            if (Check_211.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "211");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "211");
            }
            if (Check_212.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "212");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "212");
            }
            if (Check_220.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "220");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "220");
            }
            if (Check_221.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "221");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "221");
            };
            if (Check_230.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "230");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "230");
            }
            if (Check_231.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "231");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "231");
            }
            if (Check_300.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "300");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "300");
            }
            if (Check_301.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "301");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "301");
            }
            if (Check_302.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "302");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "302");
            }
            if (Check_310.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "310");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "310");
            }
            if (Check_311.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "311");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "311");
            }
            if (Check_320.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "320");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "320");
            }
            if (Check_321.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "321");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "321");
            }
            if (Check_330.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "330");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "330");
            }
            if (Check_331.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "331");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "331");
            }
            if (Check_332.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "332");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "332");
            }
            if (Check_340.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "340");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "340");
            }
            if (Check_341.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "341");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "341");
            }
            if (Check_350.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "350");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "350");
            }


            if (Check_400.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "400");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "400");
            }
            if (Check_401.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "401");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "401");
            }
            if (Check_410.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "410");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "410");
            }
            if (Check_411.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "411");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "411");
            }
            if (Check_420.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "420");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "420");
            }
            if (Check_421.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "421");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "421");
            }
            if (Check_422.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "422");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "422");
            }
            if (Check_430.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "430");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "430");
            }
            if (Check_431.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "431");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "431");
            }
            if (Check_432.Checked)
            {
                serviceLien.insertAssocTypeAgentLien(typeAgent, "432");
            }
            else
            {
                serviceLien.deleteAssocTypeAgentLien(typeAgent, "432");
            }
        }
        #endregion

        #region event
        protected void gvTypeAgent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            divIndicationText("", "Red");
            gvTypeAgent.PageIndex = e.NewPageIndex;
            this.initialiseGridTypeAgent();
        }
        protected void gvTypeAgent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region declaration
            string strConfirm = "";
            #endregion

            #region implementation
            divIndicationText("", "Red");
            if (e.CommandName.Equals("select"))
            {
                this.afficheTypeAgent(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                this.serviceLien.deleteAssocTypeAgentLien(e.CommandArgument.ToString());
                if (serviceGeneral.delete("typeagent", "typeAgent", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridTypeAgent();
                }
                else
                {
                    strConfirm = "Type agent " + e.CommandArgument.ToString() + " ne peut pas être supprimer!";
                    divIndicationText(strConfirm, "Red");
                }
            }
            #endregion

        }

        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaire();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTypeAgent objTypeAgent = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (hfTypeAgent.Value != "")
            {
                objTypeAgent = serviceTypeAgent.selectTypeAgent(hfTypeAgent.Value);

                if (objTypeAgent != null)
                {
                    this.insertToObjTypeAgent(objTypeAgent);
                    this.serviceLien.deleteAssocTypeAgentLien(hfTypeAgent.Value);

                    if (serviceTypeAgent.isTypeAgent(objTypeAgent, hfTypeAgent.Value) != "")
                    {
                        strConfirm = "Le type agent " + objTypeAgent.typeAgent + " est déjà dans la base de donnée!";
                        this.divIndicationText(strConfirm, "Red");
                    }
                    else
                    {
                        if (serviceTypeAgent.updateTypeAgent(objTypeAgent, hfTypeAgent.Value))
                        {
                            this.enregistrePageAgent(objTypeAgent.typeAgent);
                            this.initialiseFormulaire();
                            this.initialiseGridTypeAgent();

                            strConfirm = "Type agent " + objTypeAgent.typeAgent + " bien modifier!";
                            this.divIndicationText(strConfirm, "Black");
                        }
                        else
                        {
                            strConfirm = "Une erreur ce produit durant la modification!";
                            this.divIndicationText(strConfirm, "Red");
                        }
                    }
                }
            }
            #endregion
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTypeAgent typeAgent = null;
            string strConfirm = "";
            #endregion

            #region implementation
            typeAgent = new crlTypeAgent();

            this.insertToObjTypeAgent(typeAgent);

            if (serviceTypeAgent.isTypeAgent(typeAgent, typeAgent.typeAgent) != "")
            {
                strConfirm = "Le type agent " + typeAgent.typeAgent + " est déjà dans la base de donnée!";
                this.divIndicationText(strConfirm, "Red");
            }
            else
            {
                if (serviceTypeAgent.insertTypeAgent(typeAgent))
                {
                    this.enregistrePageAgent(typeAgent.typeAgent);
                    this.initialiseFormulaire();
                    this.initialiseGridTypeAgent();

                    strConfirm = "Type agent " + typeAgent.typeAgent + " bien enregistrer!";
                    this.divIndicationText(strConfirm, "Black");
                }
                else
                {
                    strConfirm = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strConfirm, "Red");
                }
            }
            #endregion
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
    }
}