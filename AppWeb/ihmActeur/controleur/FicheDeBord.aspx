<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/controleur/MasterControleur.Master" AutoEventWireup="true" CodeBehind="FicheDeBord.aspx.cs" Inherits="AppWeb.ihmActeur.controleur.FicheDeBord" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
     <div id="OneLayote">
        <div class="formContent3">
            <div class="grandTitre">
                &nbsp;&nbsp;Fiche de bord
            </div>

            <div id="OneLayoteLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Information
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight">
            <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste fiche de bord
                    </div>
          <div class="divListe">
              <asp:DropDownList ID="ddlTriListeFB" runat="server" AutoPostBack="True" 
                  onselectedindexchanged="ddlTriListeFB_SelectedIndexChanged">
                <asp:ListItem Text="N°Fiche de bord" Value="fichebord.numerosFB"></asp:ListItem>
                <asp:ListItem Text="Date de départ" Value="dateHeurPrevue"></asp:ListItem>
                <asp:ListItem Text="Matricule voiture" Value="matriculeVehicule"></asp:ListItem>
                <asp:ListItem Text="Nom chauffeur" Value="nomChauffeur"></asp:ListItem>
                <asp:ListItem Text="Prénom chauffeur" Value="prenomChauffeur"></asp:ListItem>
              </asp:DropDownList>
              <asp:TextBox ID="TextRechercheFB" runat="server"></asp:TextBox>
              <asp:Button ID="bvtnRechercheFB" runat="server" Text="Rechercher" 
                  onclick="bvtnRechercheFB_Click" />
              <asp:GridView ID="gvFicheBord" runat="server" AllowPaging="True" 
                  AutoGenerateColumns="False" EnableModelValidation="True" 
                  onpageindexchanging="gvFicheBord_PageIndexChanging" 
                  onrowcommand="gvFicheBord_RowCommand">
                  <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                CommandArgument='<%# Eval("numFb") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:BoundField DataField="numFb" HeaderText="N°Fiche de bord" />
                      <asp:BoundField DataField="dateHeurPrevue" HeaderText="Date de départ" DataFormatString="{0:dd MMMM yyyy HH:mm}" />
                      <asp:BoundField DataField="voiture" HeaderText="Vehicule" />
                      <asp:BoundField DataField="chauffeur" HeaderText="Chauffeur" />
                      <asp:BoundField DataField="itineraire" HeaderText="Itineraire" />
                      <asp:BoundField DataField="nbPlaceLibre" HeaderText="Nombre de place libre" />
                  </Columns>
              </asp:GridView>
          </div>  
           
            </div>
            </div>

            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
