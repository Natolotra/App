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
    public partial class vueGeneralre : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalItineraire serviceItineraire = null;
        IntfDalFicheBord serviceFicheBord = null;
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
            serviceItineraire = new ImplDalItineraire();
            serviceFicheBord = new ImplDalFicheBord();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                TextDateCalendar.Text = DateTime.Now.ToString("dd MMMM yyyy");
                ddlDebutItineraire.Items.Add(new ListItem(agent.agence.ville.NomVille, agent.agence.ville.NumVille));
                serviceItineraire.loadDdlItineraireForCalendar(ddlFinItineraire, ddlDebutItineraire.SelectedValue);

                try
                {
                    ddlHeure.SelectedValue = DateTime.Now.ToString("HH");
                }
                catch (Exception)
                {
                }

                this.initialiseVueG();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "321"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseVueG()
        {
            #region declaration
            crlItineraire itineraire = null;
            List<crlFicheBord> ficheBords = null;
            DateTime date;
            List<Panel> panels = null;

            string axeStr = "";
            #endregion

            #region implementation
            itineraire = serviceItineraire.selectItineraire(ddlFinItineraire.SelectedValue);
            if (itineraire != null)
            {
                if (itineraire.routeNationale != null)
                {
                    for (int i = 0; i < itineraire.routeNationale.Count; i++)
                    {
                        if (i == 0)
                        {
                            axeStr = itineraire.routeNationale[i].RouteNationale;
                        }
                        else
                        {
                            axeStr = axeStr + "-" + itineraire.routeNationale[i].RouteNationale;
                        }
                    }
                }
            }
            LabAxe.Text = axeStr;

            try
            {
                date = Convert.ToDateTime(TextDateCalendar.Text);
            }
            catch (Exception)
            {
                TextDateCalendar.Text = DateTime.Now.ToString("dd MMMM yyyy");
                date = DateTime.Now;
            }

            ficheBords = serviceFicheBord.selectFicheBord(date, ddlHeure.SelectedValue, ddlFinItineraire.SelectedValue, agent.numAgence);
            panels = new List<Panel>();

            if (ficheBords != null)
            {
                for (int i = 0; i < ficheBords.Count; i++)
                {
                    panels.Add(new Panel());
                    panels[i].Controls.Add(this.creeImageVoiture(ficheBords[i].placeFB.Count, ficheBords[i].autorisationVoyage.Verification.Licence.vehicule.NombreColoneVehicule, ficheBords[i].placeFB, ficheBords[i]));
                    panels[i].CssClass = "contentDivVoiture";
                    //contentDiv.Controls.Add();
                }

                for (int i = 0; i < ficheBords.Count; i++)
                {
                    contentDiv.Controls.Add(panels[i]);
                }
            }


            #endregion
        }

        private Table creeImageVoiture(int nbPlace, int nbColone, List<crlPlaceFB> placeFBs, crlFicheBord ficheBord)
        {
            #region declaration
            Table tableVoiture = null;
            #endregion

            #region implementation
            if (nbPlace == placeFBs.Count)
            {
                LinkButton btnSelectHead = new LinkButton();
                btnSelectHead.Text = ficheBord.autorisationVoyage.Verification.Licence.vehicule.MatriculeVehicule + " <br/>" + ficheBord.autorisationVoyage.Verification.Licence.vehicule.MarqueVehicule + " <br/>" + ficheBord.autorisationVoyage.Verification.Licence.vehicule.CouleurVehicule;
                btnSelectHead.PostBackUrl = "RemplirFB.aspx?numerosFB=" + ficheBord.NumerosFB;
                btnSelectHead.CssClass = "voitureText";
                btnSelectHead.Width = new Unit(80);


                List<TableCell> cells = new List<TableCell>();
                List<TableRow> rows = new List<TableRow>();
                List<Image> images = new List<Image>();

                TableCell cellChauffeur = new TableCell();
                Image imageChauffeur = new Image();
                imageChauffeur.ImageUrl = "~/CssStyle/images/bleu.png";

                cellChauffeur.Controls.Add(imageChauffeur);
                cellChauffeur.ColumnSpan = nbColone - 2;

                TableCell cellHead = new TableCell();
                cellHead.ColumnSpan = nbColone;
                cellHead.Height = 25;
                cellHead.CssClass = "voitureHead";
                cellHead.Controls.Add(btnSelectHead);
                //cellHead.Controls.Add(btnSelectHeadMarque);
                //cellHead.Controls.Add(btnSelectHeadCouleur);

                TableRow rowHead = new TableRow();
                rowHead.Cells.Add(cellHead);
                TableCell cellFoot = new TableCell();
                cellFoot.ColumnSpan = nbColone;
                cellFoot.Height = 10;
                cellFoot.CssClass = "voitureFoot";

                #region foot
                TableCell cellFootItem11 = new TableCell();
                cellFootItem11.ColumnSpan = nbColone - 1;
                cellFootItem11.CssClass = "voitureFootItem";
                TableCell cellFootItem12 = new TableCell();
                cellFootItem12.CssClass = "voitureFootItem";
                TableCell cellFootItem22 = new TableCell();
                cellFootItem22.ColumnSpan = nbColone;
                cellFootItem22.CssClass = "voitureFootItem";

                Image imgIndicateur = new Image();

                int nbPlaceOccuper = serviceFicheBord.getNombreTotalPassager(ficheBord.NumerosFB);
                double poidBagage = serviceFicheBord.getPoidTotalBagage(ficheBord.NumerosFB) + serviceFicheBord.getPoidTotalCommission(ficheBord.NumerosFB);

                if (ficheBord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.NbPassagerMin > nbPlaceOccuper)
                {
                    imgIndicateur.ImageUrl = "~/CssStyle/images/rouge.png";
                }
                else if (nbPlaceOccuper == ficheBord.autorisationVoyage.Verification.Licence.NombrePlacePayante)
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
                labelBagVar.Text = "Bag:" + poidBagage.ToString("0") + "Kg/" + ficheBord.autorisationVoyage.Verification.Licence.vehicule.paramVehicule.PoidBagageMax.ToString("0") + "Kg";

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
                    images.Add(new Image());
                }
                for (int i = 0; i < nbPlace; i++)
                {
                    if (placeFBs[i].IsOccuper == 1)
                    {
                        images[i].ImageUrl = "~/CssStyle/images/rouge.png";
                    }
                    else
                    {
                        images[i].ImageUrl = "~/CssStyle/images/vert.png";
                    }

                    cells[i].Controls.Add(images[i]);
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

                    if (iColone > nbColone)
                    {
                        iRow = iRow + 1;
                        iColone = 1;
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

                }


            }
            #endregion

            return tableVoiture;
        }
        #endregion

        #region event
        protected void TextDateCalendar_TextChanged(object sender, EventArgs e)
        {
            this.initialiseVueG();
        }
        protected void ddlHeure_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseVueG();
        }
        protected void ddlFinItineraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseVueG();
        }

        protected void btn_Click(Object sender, System.EventArgs e)
        {
            Button btnSelect = (Button)sender;

            if (btnSelect != null)
            {
                Response.Redirect("RemplirFB.aspx?numerosFB=" + btnSelect.ID);
            }
        }
        #endregion

        protected void ddlDebutItineraire_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}