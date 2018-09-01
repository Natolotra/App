<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="typeCommission.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.typeCommission" %>
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
                &nbsp;&nbsp;Type, désignation commission
            </div>

            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Type commission
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_TypeCommissionFormulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Type:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTypeCommission" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextTypeCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextTypeCommission" ValidationGroup="gTypeCommission">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Commentaire:
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextCommentaireTypeCommission" runat="server" TextMode="MultiLine" 
                                                    Width="145px" Height="40px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:ValidationSummary ID="ValidationSummary_TypeCommission" runat="server" ValidationGroup="gTypeCommission"/>
                                            <asp:HiddenField ID="hfTypeCommission" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveau_Click"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                SkinID="btnValidation" onclick="btnModifier_Click"/>
                                            <asp:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                SkinID="btnValidation" onclick="btnValider_Click" ValidationGroup="gTypeCommission"/>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeTypeCommission" runat="server">   
                            <ContentTemplate>
                                <asp:GridView ID="gvTypeCommission" runat="server" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" AllowPaging="True" 
                                    onpageindexchanging="gvTypeCommission_PageIndexChanging" 
                                    onrowcommand="gvTypeCommission_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("typeCommission") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="typeCommission" HeaderText="Type" />
                                        <asp:BoundField DataField="commentaireTypeCommission" 
                                            HeaderText="Commentaire" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("typeCommission") %>' />
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                        TargetControlID="ibDelete"
                                                        ConfirmText='<%# "Voulez vous vraiment supprimer le type commission " + Eval("typeCommission") + "?" %>' >
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

            <div id="OneLayoteListeRight">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Désignation commission
                    </div>

                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_DesignationFormulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Désignation:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDesignation" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDesignation_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextDesignation" ValidationGroup="gDesignation">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Type commission:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeCommission" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlTypeCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlTypeCommission" ValidationGroup="gDesignation">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Mode de calcul:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlModeCalcul" runat="server">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Interne" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Par Kg" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Par pièce" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="4">
                                            <asp:RequiredFieldValidator ID="ddlModeCalcul_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlModeCalcul" ValidationGroup="gDesignation">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="gDesignation"/>
                                            <asp:HiddenField ID="hfNumDesignation" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNouveauDesigantion" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveauDesigantion_Click"/>
                                            <asp:Button ID="btnModifierDesignation" runat="server" Text="Modifier" 
                                                SkinID="btnValidation" onclick="btnModifierDesignation_Click"/>
                                            <asp:ConfirmButtonExtender ID="btnModifierDesignation_ConfirmButtonExtenderbtnModifierDesignation" runat="server" 
                                                TargetControlID="btnModifierDesignation"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValiderDesignation" runat="server" Text="Valider" 
                                                SkinID="btnValidation" ValidationGroup="gDesignation" 
                                                onclick="btnValiderDesignation_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeDesignation" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriDesignation" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriDesignation_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="numDesignation"></asp:ListItem>
                                    <asp:ListItem Text="Désignation" Value="designation"></asp:ListItem>
                                    <asp:ListItem Text="Type commission" Value="typeCommission"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheDesigantion" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheDesignation" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheDesignation_Click" />
                                <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" AllowPaging="True" 
                                    onpageindexchanging="gvDesignation_PageIndexChanging" 
                                    onrowcommand="gvDesignation_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numDesignation") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="designation" HeaderText="Désignation" />
                                        <asp:BoundField DataField="typeCommission" HeaderText="Type commission" />
                                        <asp:BoundField DataField="paiement" HeaderText="Mode de calcul" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numDesignation") %>' />
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                        TargetControlID="ibDelete"
                                                        ConfirmText='<%# "Voulez vous vraiment supprimer le désignation " + Eval("typeCommission") + "?" %>' >
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

            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
