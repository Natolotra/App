<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Chauffeur.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Chauffeur" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>

    <div>
        <div id="OneLayote">
            <div class="formContent3">
            
                <div class="grandTitre">
                    &nbsp;&nbsp;Chauffeur
                </div>
                
                <div id="OneLayoteFormulaireLeft">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Formulaire
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_FormulaireChauffeur" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                Coopérative:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCooperative" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>
                                                Situation familiale:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSituationFamiliale" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlSituationFamiliale_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlSituationFamiliale" ValidationGroup="gChauffeur">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNom" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNom_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNom" ValidationGroup="gChauffeur">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenom" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Né le:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextDateNaissance" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="TextDateNaissance_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="TextDateNaissance" Format="dd MMMM yyyy">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                à
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextLieuNaissance" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextCIN" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextCIN_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextCIN" ValidationGroup="gChauffeur">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Adresse:
                                            </td>
                                            <td rowspan="2">
                                                <asp:TextBox ID="TextAdresse" runat="server" TextMode="MultiLine" Width="142px" Height="43"></asp:TextBox>
                                       
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextAdresse_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextAdresse" ValidationGroup="gChauffeur">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextTelephone" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobile" runat="server"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                           
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <div id="divIndication" runat="server">
                                                </div>
                                                <asp:ValidationSummary ID="ValidationSummary_Chauffeur" runat="server" 
                                                ValidationGroup="gChauffeur"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                    onclick="btnNouveau_Click" SkinID="btnValidation"/>
                                        
                                                <asp:Button ID="btnModiffier" runat="server" Text="Modifier" onclick="btnModiffier_Click" 
                                                    SkinID="btnValidation" ValidationGroup="gChauffeur"/>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModiffier" runat="server" 
                                                        TargetControlID="btnModiffier"
                                                        ConfirmText=""/>
                                                <asp:Button ID="btnInserer" runat="server" Text="Insérer" 
                                                    onclick="btnInserer_Click" SkinID="btnValidation" ValidationGroup="gChauffeur"/>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- HiddenField stokage de valeur -->
                                        <asp:HiddenField ID="hfId" runat="server" />
                                        <asp:HiddenField ID="hfValue" runat="server" />
                                    <!-- fin HiddenField stokage de valeur-->
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </div>
                    </div>
                </div>

                <div id="OneLayoteListeRight">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Liste
                        </div>
                        <div class="divListe">
                            <asp:UpdatePanel ID="UpdatePanel_ListeChauffeur" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTriChauffeur" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriChauffeur_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercher" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercher" runat="server" Text="Rechercher" 
                                        onclick="btnRechercher_Click" />
                                    <asp:GridView ID="gvChauffeur" runat="server" AutoGenerateColumns="False" 
                                        AllowPaging="True" onpageindexchanging="gvChauffeur_PageIndexChanging" 
                                        onrowcommand="gvChauffeur_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("idChauffeur") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nomChauffeur" HeaderText="Nom" />
                                            <asp:BoundField DataField="prenomChauffeur" HeaderText="Prénom" />
                                            <asp:BoundField DataField="telephoneChauffeur" HeaderText="Téléphone" />
                                            <asp:BoundField DataField="telephoneMobileChauffeur" HeaderText="Mobile" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                        CommandArgument='<%# Eval("idChauffeur") %>' />
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                        TargetControlID="ibDelete"
                                                        ConfirmText='<%# "Voulez vous vraiment supprimer le chauffeur " + Eval("nomChauffeur") + " " + Eval("prenomChauffeur") + "?" %>' >
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
    </div>
</asp:Content>
