<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Cooperative.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Cooperative" %>
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
                &nbsp;&nbsp;Coopérative
            </div>
                

            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Fomulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_FormulaireCooperative" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Coopérative:
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextNomCooperative" runat="server" TextMode="MultiLine" 
                                                    Width="145px" Height="40px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomCooperative_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomCooperative" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>Adresse
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextAdresse" runat="server" TextMode="MultiLine" 
                                                    Width="145px" Height="40px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresse_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresse" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sigle:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextSigle" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextSigle_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextSigle" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Zone:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlZone" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlZone_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlZone" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Ville:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlVille" runat="server">
                                            </asp:DropDownList>
                                            <cc1:ListSearchExtender ID="ddlVille_ListSearchExtender" runat="server" 
                                                Enabled="True" PromptText="Recherche" TargetControlID="ddlVille">
                                            </cc1:ListSearchExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlVille_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlVille" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Téléphone:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextFixeCooperative" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Mobile:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMobileCooperative" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="titreTab" colspan="6">
                                            Responsable
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomResponsable" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomResponsable_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomResponsable" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Prénom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenomResponsable" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>CIN:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCINResponsable" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCINResponsable_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextCINResponsable" ValidationGroup="gCooperative">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Adresse:
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextAdresseResponsable" runat="server" TextMode="MultiLine" 
                                                    Width="145px" Height="40px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Téléphone:</td>
                                        <td>
                                            <asp:TextBox ID="TextTelephoneResponsable" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Mobile:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMobileResponsable" runat="server"></asp:TextBox>
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_Cooperative" runat="server" ValidationGroup="gCooperative"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfNumCooperative" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNew" runat="server" Text="Nouvelle" onclick="btnNew_Click" SkinID="btnValidation"/>
                                            
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                ValidationGroup="gCooperative" onclick="btnModifier_Click" SkinID="btnValidation"/>
                                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                ValidationGroup="gCooperative" onclick="btnValider_Click" SkinID="btnValidation"/>
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
                        &nbsp;&nbsp;Liste des coopératives
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeCooperative" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddltriCooperative" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddltriCooperative_SelectedIndexChanged">
                                    <asp:ListItem Text="N°" Value="numCooperative"></asp:ListItem>
                                    <asp:ListItem Text="Coopérative" Value="nomCooperative"></asp:ListItem>
                                    <asp:ListItem Text="Sigle" Value="sigleCooperative"></asp:ListItem>
                                    <asp:ListItem Text="Adresse" Value="adresseCooperative"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheCooperative" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheCooperative" runat="server" Text="Recherche" 
                                    onclick="btnRechercheCooperative_Click" />
                                <asp:GridView ID="gvCooperative" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvCooperative_PageIndexChanging" 
                                    onrowcommand="gvCooperative_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numCooperative") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nomCooperative" HeaderText="Coopérative" />
                                        <asp:BoundField DataField="adresseCooperative" HeaderText="Adresse" />
                                        <asp:BoundField DataField="zone" HeaderText="Zone" />
                                        <asp:BoundField DataField="responsable" HeaderText="Responsable" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numCooperative") %>' />
                                                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer le coopérative " + Eval("nomCooperative") + "?" %>' >
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

            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
