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

namespace AppWeb.ihmActeur
{
    public partial class Authentification : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalAgent serviceAgent = null;
        crlAgent agent = null;
        #endregion

        #region event page
        protected void Page_Load(object sender, EventArgs e)

        {
            #region initialisation ressource
            serviceRessource = new ImplDalServiceRessource();
            #endregion

            #region verification
            this.verification();
            #endregion

            #region initialisation
            serviceAgent = new ImplDalAgent(serviceRessource.getDefaultStrConnection());
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseFormulaire();
            }
            #endregion



        }
        #endregion

        #region methode page
        private void indicationFormulaire()
        {
            LabLogin.Text = "*";
            LabMotDePasse.Text = "*";
        }
        private void initialiseFormulaire()
        {
            TextLogin.Text = "";
            TextMotDePasse.Text = "";
            LabLogin.Text = "";
            LabMotDePasse.Text = "";
            this.divIndicationText("", "black");
        }
        private void verification()
        {
            if (!serviceRessource.testBase(serviceRessource.getDefaultStrConnection()))
            {
                Session.Clear();
                Response.Redirect("ConfigurationBD.aspx");
            }
            else
            {
                if (Session["agent"] != null)
                {
                    agent = (crlAgent)Session["agent"];

                    Response.Redirect("LGTrans.aspx");


                }
            }
        }
        private bool testFormulaire()
        {
            if (TextLogin.Text.Trim() != "")
            {
                if (TextMotDePasse.Text.Trim() != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
        #endregion

        #region event
        protected void btnConnection_Click(object sender, EventArgs e)
        {
            #region implementation
            if (this.testFormulaire())
            {
                agent = serviceAgent.login(TextLogin.Text, TextMotDePasse.Text);
                if (agent.matriculeAgent != "")
                {
                    Session["agent"] = agent;
                    this.verification();
                }
                else
                {
                    this.divIndicationText(ReAuthentification.ErreurLogin, "red");
                }
            }
            else
            {
                this.indicationFormulaire();
            }
            #endregion
        }
        #endregion
    }
}