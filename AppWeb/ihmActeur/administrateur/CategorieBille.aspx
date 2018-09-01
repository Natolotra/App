<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CategorieBille.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.CategorieBille" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>

    <div id="OneLayote">
        <div class="formContent3">
            <div class="grandTitre">
                Catégorie billet
            </div>

            <div id="OneLayoteLeft50">   
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>

                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Catégorie:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCategorie" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCategorie_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextCategorie" ValidationGroup="gCategorie">*</asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Pourcentage:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPourcentage" runat="server" Width="120"></asp:TextBox>%
                                            <asp:FilteredTextBoxExtender
                                                ID="TextPourcentage_FilteredTextBoxExtender"
                                                runat="server" 
                                                TargetControlID="TextPourcentage"
                                                FilterType="Custom, Numbers"
                                                ValidChars="," />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPourcentage_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPourcentage" ValidationGroup="gCategorie">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_CategorieBillet" runat="server" ValidationGroup="gCategorie"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfNumCalculCategorieBillet" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                onclick="btnNouveau_Click" SkinID="btnValidation"/>
                                            
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                ValidationGroup="gCategorie" onclick="btnModifier_Click" SkinID="btnValidation"/>
                                            <asp:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server" 
                                            TargetControlID="btnModifier"
                                            ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                ValidationGroup="gCategorie" onclick="btnValider_Click" SkinID="btnValidation"/>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeCategorieBillet" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriCategorieBillet" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriCategorieBillet_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="calculcategoriebillet.numCalculCategorieBillet"></asp:ListItem>
                                    <asp:ListItem Text="Catégorie" Value="calculcategoriebillet.indicateurPrixBillet"></asp:ListItem>
                                    <asp:ListItem Text="Pourcentage" Value="calculcategoriebillet.pourcentagePrixBillet"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheCategorieBillet" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheCategorieBillet" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheCategorieBillet_Click" />
                                <asp:GridView ID="gvCategorieBillet" runat="server" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" AllowPaging="True" 
                                    onpageindexchanging="gvCategorieBillet_PageIndexChanging" 
                                    onrowcommand="gvCategorieBillet_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numCalculCategorieBillet") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="indicateurPrixBillet" HeaderText="Catégorie" />
                                        <asp:BoundField DataField="pourcentagePrixBillet" HeaderText="Poucentage" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numCalculCategorieBillet") %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer la catégorie " + Eval("indicateurPrixBillet") + "?" %>' >
                                                </asp:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
            </div>

            <div class="clear">
            </div>
        </div>
    </div>

</asp:Content>
