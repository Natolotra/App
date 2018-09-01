<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TarifDeveloppement.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.TarifDeveloppement" %>
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
                &nbsp;&nbsp;Tarif développement
            </div>

             <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>

                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Zone:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlZone" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlZone_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlZone" ValidationGroup="gTD">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Montant:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMontant" runat="server" Width="120"></asp:TextBox>
                                            Ar
                                        </td>
                                        <td>
                                    
                                            <asp:RequiredFieldValidator ID="TextMontant_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextMontant" ValidationGroup="gTD">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Commentaire:
                                        </td>
                                        <td colspan="4" rowspan="2">
                                            <asp:TextBox ID="TextCommentaire" runat="server" TextMode="MultiLine" 
                                                Width="300px" Height="40px"></asp:TextBox>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:ValidationSummary ID="TDValidationSummary" runat="server" ValidationGroup="gTD"/>
                                            <asp:HiddenField ID="hfNumTarifDeveloppement" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveau_Click"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                SkinID="btnValidation" onclick="btnModifier_Click" ValidationGroup="gTD"/>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                    TargetControlID="btnModifier"
                                                    ConfirmText=""/>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                SkinID="btnValidation" onclick="btnValider_Click" ValidationGroup="gTD"/>
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
                        &nbsp;&nbsp;Liste
                    </div>

                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Liste" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriTarifD" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriTarifD_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="numTarifDeveloppement"></asp:ListItem>
                                    <asp:ListItem Text="Zone" Value="zone"></asp:ListItem>
                                    <asp:ListItem Text="Montant" Value="montantTarifDeveloppement"></asp:ListItem>
                                    <asp:ListItem Text="Commentaire" Value="commentaireTarifDeveloppement"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheTD" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheTD" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheTD_Click" />
                                <asp:GridView ID="gvTD" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvTD_PageIndexChanging" onrowcommand="gvTD_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numTarifDeveloppement") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="zone" HeaderText="Zone" />
                                        <asp:BoundField DataField="montantTarifDeveloppement" HeaderText="Montant" />
                                        <asp:BoundField DataField="commentaireTarifDeveloppement" 
                                            HeaderText="Commentaire" />
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
