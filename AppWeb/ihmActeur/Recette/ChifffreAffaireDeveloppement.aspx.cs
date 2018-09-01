using arc.utile;
using arch.dal.impl;
using arch.dal.intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb.ihmActeur.Recette
{
    public partial class ChifffreAffaireDeveloppement : System.Web.UI.Page
    {
        #region declaration
        IntfDalCA serviceCA = null;
        IntfDalGeneral serviceGeneral = null;

        Convertisseuse convertisseuse = null;
        #endregion

        #region event page
        protected void Page_Load(object sender, EventArgs e)
        {

            #region initialisation
            serviceCA = new ImplDalCA();
            serviceGeneral = new ImplDalGeneral();

            convertisseuse = new Convertisseuse();
            #endregion

            #region
            if (!IsPostBack)
            {
                TextDateDebut.Text = DateTime.Now.ToString("dd MMMM yyyy");
                TextDateFin.Text = DateTime.Now.ToString("dd MMMM yyyy");

                try
                {
                    rbCAParam.SelectedValue = "0";
                }
                catch (Exception)
                {
                }

                this.initialise();
            }
            #endregion
        }
        #endregion


        #region methode page
        private void initialise()
        {
            #region declaration
            DateTime dateDebut;
            DateTime dateFin;
            #endregion

            #region implementation
            try
            {


                if (rbCAParam.SelectedValue == "0")
                {
                    dateDebut = Convert.ToDateTime(TextDateDebut.Text);
                    dateFin = Convert.ToDateTime(TextDateFin.Text);

                    serviceCA.insertToGridCADeveloppement(gvCA, dateDebut.ToString("yyyy-MM-dd"), dateFin.ToString("yyyy-MM-dd"));
                    LabMontantTotalCA.Text = serviceGeneral.separateurDesMilles(serviceCA.montantCADeveloppement(dateDebut.ToString("yyyy-MM-dd"), dateFin.ToString("yyyy-MM-dd")).ToString("0")) + "Ar";
                    LabMontantTotalLettre.Text = convertisseuse.convertion(serviceCA.montantCADeveloppement(dateDebut.ToString("yyyy-MM-dd"), dateFin.ToString("yyyy-MM-dd")).ToString("0")) + " Ariary";
                }
                else if (rbCAParam.SelectedValue == "1")
                {
                    dateDebut = Convert.ToDateTime(TextDateDebut.Text);
                    dateFin = Convert.ToDateTime(TextDateFin.Text);

                    serviceCA.insertToGridCADeveloppement(gvCA, dateDebut.ToString("yyyy-MM"), dateFin.ToString("yyyy-MM") + "-32");
                    LabMontantTotalCA.Text = serviceGeneral.separateurDesMilles(serviceCA.montantCADeveloppement(dateDebut.ToString("yyyy-MM"), dateFin.ToString("yyyy-MM") + "-32").ToString()) + "Ar";
                    LabMontantTotalLettre.Text = convertisseuse.convertion(serviceCA.montantCADeveloppement(dateDebut.ToString("yyyy-MM"), dateFin.ToString("yyyy-MM") + "-32").ToString()) + " Ariary";
                }
                else if (rbCAParam.SelectedValue == "2")
                {
                    int anneeD = int.Parse(TextDateDebut.Text);
                    int anneeF = int.Parse(TextDateFin.Text);
                    dateDebut = new DateTime(anneeD, 1, 1);
                    dateFin = new DateTime(anneeF, 1, 1); ;

                    serviceCA.insertToGridCADeveloppement(gvCA, dateDebut.ToString("yyyy"), dateFin.ToString("yyyy") + "-13");
                    LabMontantTotalCA.Text = serviceGeneral.separateurDesMilles(serviceCA.montantCADeveloppement(dateDebut.ToString("yyyy"), dateFin.ToString("yyyy") + "-13").ToString("0")) + "Ar";
                    LabMontantTotalLettre.Text = convertisseuse.convertion(serviceCA.montantCADeveloppement(dateDebut.ToString("yyyy"), dateFin.ToString("yyyy") + "-13").ToString("0")) + " Ariary";
                }
            }
            catch (Exception)
            {
            }


            #endregion
        }
        #endregion


        #region event
        protected void rbCAParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbCAParam.SelectedValue == "0")
            {
                TextDateDebut_CalendarExtender.Format = "dd MMMM yyyy";
                TextDateFin_CalendarExtender.Format = "dd MMMM yyyy";
                TextDateDebut.Text = DateTime.Now.ToString("dd MMMM yyyy");
                TextDateFin.Text = DateTime.Now.ToString("dd MMMM yyyy");
            }
            else if (rbCAParam.SelectedValue == "1")
            {
                TextDateDebut_CalendarExtender.Format = "MMMM yyyy";
                TextDateFin_CalendarExtender.Format = "MMMM yyyy";
                TextDateDebut.Text = DateTime.Now.ToString("MMMM yyyy");
                TextDateFin.Text = DateTime.Now.ToString("MMMM yyyy");
            }
            else
            {
                TextDateDebut_CalendarExtender.Format = "yyyy";
                TextDateFin_CalendarExtender.Format = "yyyy";
                TextDateDebut.Text = DateTime.Now.ToString("yyyy");
                TextDateFin.Text = DateTime.Now.ToString("yyyy");
            }

            this.initialise();
        }
        protected void TextDateDebut_TextChanged(object sender, EventArgs e)
        {
            this.initialise();
        }
        protected void TextDateFin_TextChanged(object sender, EventArgs e)
        {
            this.initialise();
        }

        protected void gvCA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCA.PageIndex = e.NewPageIndex;
            this.initialise();
        }
        #endregion
    }
}