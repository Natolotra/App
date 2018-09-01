using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.Recette
{
    public partial class MasterRecette : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabLieuAgence.Text = ConfigurationManager.AppSettings["localiteServeur"];
        }
    }
}