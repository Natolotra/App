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
    public partial class typeCommission : System.Web.UI.Page
    {
        #region declaration variable
        IntfDalServiceRessource serviceRessource = null;
        IntfDalTypeCommission serviceTypeCommission = null;
        IntfDalGeneral serviceGeneral = null;
        IntfDalDesignationCommission serviceDesignationCommission = null;
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
            serviceTypeCommission = new ImplDalTypeCommission();
            serviceGeneral = new ImplDalGeneral();
            serviceDesignationCommission = new ImplDalDesignationCommission();
            #endregion

            #region !IsPostBack 
            if (!IsPostBack)
            {
                this.initialiseGridTypeCommission();
                this.initialiseFormulaireTypeCommission();

                this.serviceTypeCommission.loadDddlTypeCommission(ddlTypeCommission);
                this.initialiseGridDesignation();
                this.initialiseFormulaireDesigantion();
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
                    if (!serviceLien.isPageAgent(agent.matriculeAgent, "012"))
                        Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
                else
                {
                    Response.Redirect("~/ihmActeur/Authentification.aspx");
                }
            }
        }

        private void initialiseGridTypeCommission()
        {
            serviceTypeCommission.insertToGridTypeCommission(gvTypeCommission, "typeCommission", "typeCommission", "");
        }

        private void initialiseFormulaireTypeCommission()
        {
            TextCommentaireTypeCommission.Text = "";
            TextTypeCommission.Text = "";
            hfTypeCommission.Value = "";

            btnModifier.Enabled = false;
            btnValider.Enabled = true;
        }

        private void afficheTypeCommission(string typeCommissionStr)
        {
            #region declaration
            crlTypeCommssion typeCommission = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (typeCommissionStr != "")
            {
                typeCommission = serviceTypeCommission.selectTypeCommission(typeCommissionStr);

                if (typeCommission != null)
                {
                    TextCommentaireTypeCommission.Text = typeCommission.CommentaireTypeCommission;
                    TextTypeCommission.Text = typeCommission.TypeCommission;

                    hfTypeCommission.Value = typeCommission.TypeCommission;

                    btnModifier.Enabled = true;
                    btnValider.Enabled = false;

                    strConfirm = "Voulez vous vraiment modifier le type commission " + typeCommission.TypeCommission + "?";
                    btnModifier_ConfirmButtonExtender.ConfirmText = strConfirm;
                }
            }
            #endregion
        }

        private void insertToObjTypeCommission(crlTypeCommssion typeCommission)
        {
            #region implementation
            if (typeCommission != null)
            {
                typeCommission.TypeCommission = TextTypeCommission.Text;
                typeCommission.CommentaireTypeCommission = TextCommentaireTypeCommission.Text;
            }
            #endregion
        }



        private void initialiseGridDesignation()
        {
            serviceDesignationCommission.insertToGridDesignationCommission(gvDesignation, ddlTriDesignation.SelectedValue, ddlTriDesignation.SelectedValue, TextRechercheDesigantion.Text);
        }

        private void initialiseFormulaireDesigantion()
        {
            TextDesignation.Text = "";
            ddlModeCalcul.SelectedValue = "";

            hfNumDesignation.Value = "";

            btnModifierDesignation.Enabled = false;
            btnValiderDesignation.Enabled = true;
        }

        private void afficheDesignation(string numDesignation)
        {
            #region declaration
            crlDesignationCommission designation = null;
            string strConfirm = "";
            #endregion

            #region implementation
            if (numDesignation != "")
            {
                designation = serviceDesignationCommission.selectDesignationCommission(numDesignation);

                if (designation != null)
                {
                    TextDesignation.Text = designation.Designation;
                    ddlTypeCommission.SelectedValue = designation.TypeCommission;
                    ddlModeCalcul.SelectedValue = designation.Paiement.ToString();

                    hfNumDesignation.Value = designation.NumDesignation;

                    strConfirm = "Voulez vous vraiment modifier le designation " + designation.Designation + "?";
                    btnModifierDesignation_ConfirmButtonExtenderbtnModifierDesignation.ConfirmText = strConfirm;

                    btnModifierDesignation.Enabled = true;
                    btnValiderDesignation.Enabled = false;
                }
            }
            #endregion
        }

        private void insertToObjDesigantion(crlDesignationCommission designationCommission)
        {
            #region implementation
            if (designationCommission != null)
            {
                designationCommission.Designation = TextDesignation.Text;
                try
                {
                    designationCommission.Paiement = int.Parse(ddlModeCalcul.SelectedValue);
                }
                catch (Exception)
                {
                }

                designationCommission.TypeCommission = ddlTypeCommission.SelectedValue;
            }
            #endregion
        }
        #endregion

        #region event
        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireTypeCommission();
        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTypeCommssion typeCommission = null;
            string typeCommissionStr = "";
            #endregion

            #region implementation
            if (hfTypeCommission.Value != "")
            {
                typeCommission = serviceTypeCommission.selectTypeCommission(hfTypeCommission.Value);

                if (typeCommission != null)
                {
                    this.insertToObjTypeCommission(typeCommission);

                    typeCommissionStr = serviceTypeCommission.isTypeCommission(typeCommission, hfTypeCommission.Value);

                    if (typeCommissionStr.Equals(""))
                    {
                        if (serviceTypeCommission.updateTypeCommission(typeCommission, hfTypeCommission.Value))
                        {
                            this.initialiseGridTypeCommission();
                        }
                        else
                        {
                            //
                        }
                    }
                    else
                    {
                        //deja dans la base
                    }
                }
            }
            #endregion
        }
        protected void btnValider_Click(object sender, EventArgs e)
        {
            #region declaration
            crlTypeCommssion typeCommission = null;
            string typeCommissionStr = "";
            #endregion

            #region implementation
            typeCommission = new crlTypeCommssion();
            this.insertToObjTypeCommission(typeCommission);

            typeCommissionStr = serviceTypeCommission.isTypeCommission(typeCommission, typeCommission.TypeCommission);

            if (typeCommissionStr.Equals(""))
            {
                typeCommissionStr = serviceTypeCommission.insertTypeCommission(typeCommission);

                if (typeCommissionStr != "")
                {
                    this.initialiseGridTypeCommission();
                }
                else
                {
                    //
                }
            }
            else
            {
                //deja dans la base
                this.afficheTypeCommission(typeCommissionStr);
            }
            #endregion
        }
        protected void gvTypeCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTypeCommission.PageIndex = e.NewPageIndex;
            this.initialiseGridTypeCommission();
        }
        protected void gvTypeCommission_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheTypeCommission(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("typecommission", "typeCommission", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridTypeCommission();
                }
                else
                {
                    //
                }
            }
        }

        protected void ddlTriDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initialiseGridDesignation();
        }
        protected void btnRechercheDesignation_Click(object sender, EventArgs e)
        {
            this.initialiseGridDesignation();
        }
        protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDesignation.PageIndex = e.NewPageIndex;
            this.initialiseGridDesignation();
        }
        protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("select"))
            {
                this.afficheDesignation(e.CommandArgument.ToString());
            }
            else if (e.CommandName.Equals("deleteV"))
            {
                if (serviceGeneral.delete("designationcommission", "numDesignation", e.CommandArgument.ToString()) == 1)
                {
                    this.initialiseGridDesignation();
                }
                else
                {
                }
            }
        }

        protected void btnNouveauDesigantion_Click(object sender, EventArgs e)
        {
            this.initialiseFormulaireDesigantion();
        }
        protected void btnModifierDesignation_Click(object sender, EventArgs e)
        {
            #region declaration
            crlDesignationCommission designationCommission = new crlDesignationCommission();
            string strNumDesignation = "";
            #endregion

            #region implementation
            if (hfNumDesignation.Value != "")
            {
                designationCommission = serviceDesignationCommission.selectDesignationCommission(hfNumDesignation.Value);

                if (designationCommission != null)
                {
                    this.insertToObjDesigantion(designationCommission);

                    strNumDesignation = serviceDesignationCommission.isDesignationCommission(designationCommission);

                    if (strNumDesignation.Equals(""))
                    {
                        if (serviceDesignationCommission.updateDesignationCommission(designationCommission))
                        {
                            this.initialiseGridDesignation();
                        }
                        else
                        {
                            //
                        }
                    }
                    else
                    {
                        //
                    }
                }
            }
            #endregion
        }
        protected void btnValiderDesignation_Click(object sender, EventArgs e)
        {
            #region declaration
            crlDesignationCommission designationCommission = new crlDesignationCommission();
            #endregion

            #region implementation
            this.insertToObjDesigantion(designationCommission);

            designationCommission.NumDesignation = serviceDesignationCommission.isDesignationCommission(designationCommission);

            if (designationCommission.NumDesignation.Equals(""))
            {
                designationCommission.NumDesignation = serviceDesignationCommission.insertDesignationCommission(designationCommission, agent.agence.SigleAgence);

                if (designationCommission.NumDesignation != "")
                {
                    this.initialiseGridDesignation();
                }
                else
                {
                    //
                }
            }
            else
            {
                //
            }
            #endregion
        }
        #endregion

    }
}