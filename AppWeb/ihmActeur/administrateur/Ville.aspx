<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Ville.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Ville" %>
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
                Ville
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
                                        <td>Province
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProvince" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_ddlProvince" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlProvince" ValidationGroup="gVille">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegion" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_ddlRegion" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlRegion" ValidationGroup="gVille">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ville:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomVille" runat="server"></asp:TextBox>
                                        </td>
                                        <td colspan="4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextNomVille" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomVille" ValidationGroup="gVille">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_Ville" runat="server" ValidationGroup="gVille"/>
                                            <div id="divIndication" runat="server"></div>
                                            <asp:HiddenField ID="hfNumVille" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNew" runat="server" Text="Nouveau" onclick="btnNew_Click" SkinId="btnValidation"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                ValidationGroup="gVille" onclick="btnModifier_Click" SkinId="btnValidation"/>
                                            <asp:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                ValidationGroup="gVille" onclick="btnValider_Click" SkinId="btnValidation"/>
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
                        <asp:UpdatePanel ID="UpdatePanel_Liste" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriVille" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriVille_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="ville.numVille"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="nomVille"></asp:ListItem>
                                    <asp:ListItem Text="Region" Value="nomRegion"></asp:ListItem>
                                    <asp:ListItem Text="Province" Value="nomProvince"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheVille" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheVille" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheVille_Click" />
                                <asp:GridView ID="gvVille" runat="server" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" AllowPaging="True" 
                                    onpageindexchanging="gvVille_PageIndexChanging" 
                                    onrowcommand="gvVille_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numVille") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nomVille" HeaderText="Ville" />
                                        <asp:BoundField DataField="nomRegion" HeaderText="Region" />
                                        <asp:BoundField DataField="nomProvince" HeaderText="Province" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numVille") %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer le ville " + Eval("nomVille") + "?" %>' >
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
