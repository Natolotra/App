<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Client" %>
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
                &nbsp;&nbsp;Client
            </div>

            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>

                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanelFormuaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Nom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomClient" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomClient_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomClient" ValidationGroup="gClient">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>Prénom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenom" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CIN:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCIN" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Adresse:
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextAdresse" runat="server" TextMode="MultiLine" 
                                                    Width="145px" Height="40px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Téléphone:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTelephone" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Mobile:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMobile" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Chèque:
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbCheque" runat="server" AutoPostBack="True" 
                                                oncheckedchanged="cbCheque_CheckedChanged"/>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Bon de commande:
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbBonDeCommande" runat="server" AutoPostBack="True" 
                                                oncheckedchanged="cbBonDeCommande_CheckedChanged" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_Client" runat="server" ValidationGroup="gClient"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnValide" runat="server" Text="Valider" 
                                                onclick="btnValide_Click" ValidationGroup="gClient"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                onclick="btnModifier_Click" ValidationGroup="gClient"/>
                                            <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                                onclick="btnAnnuler_Click" />
                                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:HiddenField ID="hfNumClient" runat="server" />
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
                        &nbsp;&nbsp;Liste clients
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanelListeClient" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriClient" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriClient_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="numClient"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="nomClient"></asp:ListItem>
                                    <asp:ListItem Text="Prénom" Value="prenomClient"></asp:ListItem>
                                    <asp:ListItem Text="Adresse" Value="adresseClient"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextrechercheClient" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheClient" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheClient_Click" />
                                <asp:GridView ID="gvClient" runat="server" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" AllowPaging="True" 
                                    onpageindexchanging="gvClient_PageIndexChanging" 
                                    onrowcommand="gvClient_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numClient") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="client" HeaderText="Client" />
                                        <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                        <asp:BoundField DataField="contact" HeaderText="Contact" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numClient") %>' />
                                                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer le client N° " + Eval("client") + "?" %>' >
                                                </cc1:ConfirmButtonExtender>
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
