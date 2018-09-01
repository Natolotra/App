<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.Master" AutoEventWireup="true" CodeBehind="RecuExcedentBagage.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.RecuExcedentBagage" %>
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
                &nbsp;&nbsp;Reçu excédent bagage
            </div>

            <div id="OneLayoteLeft50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Excédent bagage
                    </div>
                    <div class="formulaire">
                        <table>
                            <tr>
                                <td>Montant:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextMontantExcedent" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="TextMontantExcedent_RequiredFieldValidator" runat="server" ErrorMessage=""
                                    ControlToValidate="TextMontantExcedent" ValidationGroup="groupeRecu">*</asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender
                                            ID="TextMontantExcedent_FilteredTextBoxExtender"
                                            runat="server" 
                                            TargetControlID="TextMontantExcedent"
                                            FilterType="Custom, Numbers"
                                            ValidChars="," />
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>Libellé:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextLibelleRecu" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Mode de paiement:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlModePaiement" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <div id="divIndication" runat="server">
                                    </div>
                                    <asp:ValidationSummary ID="ValidationSummary_Recu" runat="server" ValidationGroup="groupeRecu"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Button ID="btnValider" runat="server" Text="Valider" SkinID="btnValidation"
                                        onclick="btnValider_Click" ValidationGroup="groupeRecu"/>
                                    <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" 
                                        onclick="btnAnnuler_Click" SkinID="btnValidation"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight50">
                
            </div>

            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
