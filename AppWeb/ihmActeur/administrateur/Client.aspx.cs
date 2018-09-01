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
    public partial class Client : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalClient serviceClient = null;
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
            serviceClient = new ImplDalClient();
            #endregion

            #region !IsPostBack
            if (!IsPostBack)
            {
                this.initialiseErrorMessage();
                this.initialiseGridClient();
                this.initialiseFormulaireClient();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "042"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridClient()
        {
            serviceClient.insertToGridClient(gvClient, ddlTriClient.SelectedValue, ddlTriClient.SelectedValue, TextrechercheClient.Text);
        }

        private void initialiseCB()
        {
            if (cbBonDeCommande.Checked)
            {
                cbBonDeCommande.Text = "Accepter";
            }
            else
            {
                cbBonDeCommande.Text = "Refuser";
            }

            if (cbCheque.Checked)
            {
                cbCheque.Text = "Accepter";
            }
            else
            {
                cbCheque.Text = "Refuser";
            }
        }

        private void initialiseFormulaireClient()
        {
            TextNomClient.Text = "";
            TextPrenom.Text = "";
            TextCIN.Text = "";
            TextAdresse.Text = "";
            TextTelephone.Text = "";
            TextMobile.Text = "";

            hfNumClient.Value = "";

            cbBonDeCommande.Checked = false;
            cbCheque.Checked = false;
            this.initialiseCB();

            btnValide.Enabled = true;
            btnModifier.Enabled = false;

            this.divIndicationText("", "Red");
        }

        private void afficheClient(string numClient)
        {
            #region declaration
            crlClient client = null;
            string confirmText = "";
            #endregion

            #region implementation
            if (numClient != "")
            {
                client = serviceClient.selectClient(numClient);

                if (client != null)
                {
                    TextNomClient.Text = client.NomClient;
                    TextPrenom.Text = client.PrenomClient;
                    TextCIN.Text = client.CinClient;
                    TextAdresse.Text = client.AdresseClient;
                    TextTelephone.Text = client.TelephoneClient;
                    TextMobile.Text = client.MobileClient;

                    if (client.IsBonCommande > 0)
                    {
                        cbBonDeCommande.Checked = true;
                    }
                    else
                    {
                        cbBonDeCommande.Checked = false;
                    }

                    if (client.IsCheque > 0)
                    {
                        cbCheque.Checked = true;
                    }
                    else
                    {
                        cbCheque.Checked = false;
                    }
                    this.initialiseCB();

                    hfNumClient.Value = client.NumClient;

                    confirmText = "Voulez vous vraiment modifier le client \n" + client.PrenomClient + " " + client.NomClient + "?";
                    ConfirmButtonExtender_btnModifier.ConfirmText = confirmText;

                    btnValide.Enabled = false;
                    btnModifier.Enabled = true;
                }
            }
            #endregion
        }

        private void insertToObjetClient(crlClient client)
        {
            #region implementation
            if (client != null)
            {
                client.NomClient = TextNomClient.Text;
                client.PrenomClient = TextPrenom.Text;
                client.CinClient = TextCIN.Text;
                client.AdresseClient = TextAdresse.Text;
                client.TelephoneClient = TextTelephone.Text;
                client.MobileClient = TextMobile.Text;

                if (cbBonDeCommande.Checked)
                {
                    client.IsBonCommande = 1;
                }
                else
                {
                    client.IsBonCommande = 0;
                }

                if (cbCheque.Checked)
                {
                    client.IsCheque = 1;
                }
                else
                {
                    client.IsCheque = 0;
                }
            }
            #endregion
        }

        private void initialiseErrorMessage()
        {
            TextNomClient_RequiredFieldValidator.ErrorMessage = ReClient.nomNonVide;
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
        #endregion

        #region event
        protected void ddlTriClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridClient();
        }
        protected void btnRechercheClient_Click(object sender, EventArgs e)
        {
            this.initialiseGridClient();
        }
        protected void gvClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClient.PageIndex = e.NewPageIndex;
            this.initialiseGridClient();
        }
        protected void gvClient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region declaration
            crlClient client = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (e.CommandName.Equals("select"))
            {
                this.afficheClient(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                client = serviceClient.selectClient(e.CommandArgument.ToString());

                if (client != null)
                {
                    if (serviceClient.deleteClient(client))
                    {
                        this.initialiseGridClient();
                        strIndication = "Client " + client.PrenomClient + " " + client.NomClient + " supprimer!";
                        this.divIndicationText(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Impossible de supprimer le client " + client.PrenomClient + " " + client.NomClient;
                        this.divIndicationText(strIndication, "Red");
                    }
                }
            }
            #endregion
        }

        protected void cbBonDeCommande_CheckedChanged(object sender, EventArgs e)
        {
            this.initialiseCB();
        }

        protected void cbCheque_CheckedChanged(object sender, EventArgs e)
        {
            this.initialiseCB();
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireClient();
        }

        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlClient client = null;
            string strIndication = "";
            #endregion

            #region implementation
            if (hfNumClient.Value != "")
            {
                client = serviceClient.selectClient(hfNumClient.Value);
                if (client != null)
                {
                    this.insertToObjetClient(client);

                    if (serviceClient.updateClient(client))
                    {
                        this.afficheClient(client.NumClient);
                        this.initialiseGridClient();

                        strIndication = "Modification du client " + client.PrenomClient + " " + client.NomClient + " <br/>";
                        strIndication += "bien enregistrer!";
                        this.divIndicationText(strIndication, "Black");
                    }
                    else
                    {
                        strIndication = "Une erreur ce produit durant la modification!";
                        this.divIndicationText(strIndication, "Red");
                    }
                }
            }
            #endregion
        }

        protected void btnValide_Click(object sender, EventArgs e)
        {
            #region declaration
            crlClient client = null;
            string strIndication = "";
            #endregion

            #region implementation
            client = new crlClient();

            this.insertToObjetClient(client);

            client.NumClient = serviceClient.isClient(client);

            if (client.NumClient == "")
            {
                client.NumClient = serviceClient.insertClient(client, agent.agence.SigleAgence);

                if (client.NumClient != "")
                {
                    this.afficheClient(client.NumClient);
                    this.initialiseGridClient();

                    strIndication = "Client " + client.PrenomClient + " " + client.NomClient + "<br/> ";
                    strIndication += "bien enregistrer!";
                    this.divIndicationText(strIndication, "Black");
                }
                else
                {
                    strIndication = "Une erreur ce produit durant l'enregistrement!";
                    this.divIndicationText(strIndication, "Red");
                }
            }
            else
            {
                this.afficheClient(client.NumClient);
            }
            #endregion
        }
        #endregion
    }
}