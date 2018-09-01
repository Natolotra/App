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

namespace AppWeb.ihmActeur
{
    public partial class MasterCNT : System.Web.UI.MasterPage
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

            #endregion

            #region 
            if (!IsPostBack)
            {
                this.initialiseMenu();
            }
            #endregion
        }
        #endregion

        #region methode page
        private void isSession()
        {
            LabLieuAgence.Text = ConfigurationManager.AppSettings["localiteServeur"];

            if (Session["agent"] != null)
            {
                agent = (crlAgent)Session["agent"];
                LabSession.Text = agent.prenomAgent + " " + agent.nomAgent;
                LabDeconnexion.Text = "Déconnexion";
            }
            else
            {
                LabSession.Text = "";
                LabDeconnexion.Text = "";
            }
        }
        private void initialiseMenu()
        {
            if (agent != null)
            {
                panUL.Visible = true;
                serviceLien.getMenu(panMenu, agent.matriculeAgent, "../", "../", "RN");
            }
            else
            {
                panUL.Visible = false;
            }
        }
        #endregion
    }
}