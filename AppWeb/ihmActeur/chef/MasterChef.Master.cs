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

namespace AppWeb.ihmActeur.chef
{
    public partial class MasterChef : System.Web.UI.MasterPage
    {
        #region declaration variable
        crlAgent agent = null;
        IntfDalLien serviceLien = null;
        #endregion

        #region event page
        protected void Page_Load(object sender, EventArgs e)
        {
            #region isSession
            this.isSession();
            #endregion

            #region initialisation
            serviceLien = new ImplDalLien();
            #endregion

            #region initialiseMenu
            this.initialiseMenu();
            #endregion

            #region
            if (!IsPostBack)
            {
                //this.secteur();

            }
            #endregion
        }
        #endregion

        #region methode page
        private void isSession()
        {
            if (Session["agent"] != null)
            {
                agent = (crlAgent)Session["agent"];
                LabSession.Text = agent.prenomAgent + " " + agent.nomAgent;
                LabLieuAgence.Text = ConfigurationManager.AppSettings["localiteServeur"];
            }
        }

        private void initialiseMenu()
        {
            if (agent != null)
            {
                panUL.Visible = true;
                serviceLien.getMenu(panMenu, agent.matriculeAgent, "../../", "../../", "RN");

                styleCss.Href = "../../CssStyle/Style.css";
                styleCssMenu.Href = "../../CssStyle/CssMenu/style.css";
            }
            else
            {
                panUL.Visible = false;
            }
        }
        #endregion
    }
}