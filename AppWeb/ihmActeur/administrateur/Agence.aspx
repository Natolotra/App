<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Agence.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Agence" %>
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
                &nbsp;&nbsp;Agence
            </div>

             <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_FormulaireAgence" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td colspan="6">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Type agence:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeAgence" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlTypeAgence_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlTypeAgence" ValidationGroup="gAgence">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>Ville:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlVille" runat="server">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlVille_ListSearchExtender" runat="server" 
                                                Enabled="True" PromptText="Recherche" TargetControlID="ddlVille">
                                            </asp:ListSearchExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlVille_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlVille" ValidationGroup="gAgence">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nom agence:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomAgence" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomAgence_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomAgence" ValidationGroup="gAgence">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Sigle:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextSigle" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                        ID="TextSigle_FilteredTextBoxExtender"
                                                        runat="server"
                                                        TargetControlID="TextSigle" FilterType="UppercaseLetters"/>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextSigle_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextSigle" ValidationGroup="gAgence">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Localisation:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextLocalisation" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextLocalisation_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextLocalisation" ValidationGroup="gAgence">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary_Agence" runat="server" ValidationGroup="gAgence"/>
                                            <asp:HiddenField ID="hfNumAgence" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveau_Click"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                SkinID="btnValidation" ValidationGroup="gAgence" onclick="btnModifier_Click"/>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText=""/>
                                            <asp:Button ID="btnVlider" runat="server" Text="Valider" SkinID="btnValidation" 
                                                ValidationGroup="gAgence" onclick="btnVlider_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div id="OneLayoteListeRight">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste agence
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeAgence" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriAgence" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriAgence_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="agence.numAgence"></asp:ListItem>
                                    <asp:ListItem Text="Type" Value="agence.typeAgence"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="agence.nomAgence"></asp:ListItem>
                                    <asp:ListItem Text="Localisation" Value="agence.localisationAgence"></asp:ListItem>
                                    <asp:ListItem Text="Sigle" Value="agence.sigleAgence"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheAgence" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheAgence" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheAgence_Click" />
                                <asp:GridView ID="gvAgence" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvAgence_PageIndexChanging" 
                                    onrowcommand="gvAgence_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numAgence") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nomAgence" HeaderText="Nom agence" />
                                        <asp:BoundField DataField="typeAgence" HeaderText="Type" />
                                        <asp:BoundField DataField="sigleAgence" HeaderText="Sigle" />
                                        <asp:BoundField DataField="villeAgence" HeaderText="Ville" />
                                        <asp:BoundField DataField="localisation" HeaderText="Localisation" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    
                    </div>
                </div>
            </div>

            <div class="clear"></div>

            
        </div>
    </div>
</asp:Content>
