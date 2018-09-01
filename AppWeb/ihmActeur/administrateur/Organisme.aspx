<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Organisme.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Organisme" %>
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
                    &nbsp;&nbsp;Organisme
                </div>
                
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Organisme
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td colspan="6" class="titreTab">
                                                Information sur le premier responsable de l'organisme
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom organisme:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Mail organisme:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailOrganisme_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailOrganisme"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeOrganisme">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            
                                            <td>
                                            </td>
                                            <td>Civilité:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCiviliteRespOrganisme" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCiviliteRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCiviliteRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td colspan="3">
                                                <asp:Button ID="btnNouveauRespOrganisme" runat="server" 
                                                    Text="Nouveau responsable" onclick="btnNouveauRespOrganisme_Click"/>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Tél fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFixeOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobileOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenomRespOrganisme" runat="server"></asp:TextBox>
                                                    
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
                                            <td></td>
                                            <td>Bon de commande:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbBonCommande" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="cbBonCommande_CheckedChanged"/>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextCinRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextCinRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextCinRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Mail:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailRespOrganisme_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailRespOrganisme"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeOrganisme">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                            </td>
                                            <td>Tél fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFixeRespOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobileRespOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Adresse organisme
                                            </td>
                                            <td></td>
                                            <td colspan="6" class="titreTab">
                                                Adresse responsable organisme
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>Province:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProvinceOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlProvinceOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlProvinceOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlProvinceOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlRegionOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRegionOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlRegionOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Province:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlprovinceRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlprovinceRespOrganisme_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlprovinceRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlprovinceRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlRegionRespOrganisme_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRegionRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlRegionRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>District:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrictOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDistrictOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDistrictOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDistrictOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commune:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCommuneOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlCommuneOrganisme_SelectedIndexChanged"  >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCommuneOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCommuneOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>District:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrictRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlDistrictRespOrganisme_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDistrictRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDistrictRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commune:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCommuneRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlCommuneRespOrganisme_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCommuneRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCommuneRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Arrondissement:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArrondissementOrganisme" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlArrondissementOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlArrondissementOrganisme" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                        <td>Arrondissement:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArrondissementRespOrganisme" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlArrondissementRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlArrondissementRespOrganisme" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Adresse:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAdresseOrganisme" runat="server"></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresseOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresseOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Quartier:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextQuartierOrganisme" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextQuartierOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextQuartierOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Adresse:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAdresseRespOrganisme" runat="server" ></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresseRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresseRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Quartier:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextQuartierRespOrganisme" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextQuartierRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextQuartierRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                            
                                    <tr>
                                        <td colspan="13">
                                            <asp:ValidationSummary ID="ValidationSummary_FormulaireOrganisme" runat="server" ValidationGroup="groupeOrganisme"/>
                                            <asp:HiddenField ID="hfNumOrganisme" runat="server" />
                                            <asp:HiddenField ID="hfNumIndividuOrganisme" runat="server" />
                                            <div id="divIndication" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                                onclick="btnAnnuler_Click" SkinID="btnValidation"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" ValidationGroup="groupeOrganisme"
                                                onclick="btnModifier_Click" SkinID="btnValidation"/>
                                            <cc1:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server"
                                                    TargetControlID="btnModifier" ConfirmText="Voulez vous vraiment modifier?" />
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                ValidationGroup="groupeOrganisme" onclick="btnValider_Click" SkinID="btnValidation"/>
                                            
                                            
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            
                    </div>
                </div>
                
                <br />
                 
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste des organismes
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeOrganisme" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriOrganisme" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriOrganisme_SelectedIndexChanged">
                                    <asp:ListItem Text="N°" Value="numOrganisme"></asp:ListItem>
                                    <asp:ListItem Text="Nom organisme" Value="nomOrganisme"></asp:ListItem>
                                    <asp:ListItem Text="Adresse organisme" Value="adresseOrganisme"></asp:ListItem>
                                    <asp:ListItem Text="Prénom responsable" Value="prenomResponsable"></asp:ListItem>
                                    <asp:ListItem Text="Nom responsable" Value="nomResponsable"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheOrganisme" runat="server"></asp:TextBox>
                                <asp:Button ID="btnOrganisme" runat="server" Text="Rechercher" 
                                    onclick="btnOrganisme_Click" />
                                <asp:GridView ID="gvOrganisme" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvOrganisme_PageIndexChanging" 
                                    onrowcommand="gvOrganisme_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numOrganisme") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numOrganisme") %>' />
                                                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer organisme " + Eval("nomOrganisme") + "? \nN°: " + Eval("numOrganisme")%>' >
                                                </cc1:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numOrganisme" HeaderText="N°" />
                                        <asp:BoundField DataField="nomOrganisme" HeaderText="Nom organisme" />
                                        <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                            
                    </div>
                </div>
               

                <div class="clear"></div>
            </div>
    </div>
</asp:Content>
