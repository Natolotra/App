<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Societe.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Societe" %>
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
                    &nbsp;&nbsp;Société
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
                                            Société
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td colspan="6" class="titreTab">
                                            Information sur le premier responsable de la société
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nom société:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomSociete" runat="server"></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomSociete" ValidationGroup="groupeSociete">*
                                            </asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>Secteur d'activité:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextSecteurSociete" runat="server"></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextSecteurSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextSecteurSociete" ValidationGroup="groupeSociete">
                                            *</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Civilité:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCiviliteRespSociete" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCiviliteRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCiviliteRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td colspan="3">
                                            <asp:Button ID="btnNouveauRespSociete" runat="server" 
                                                Text="Nouveau responsable" onclick="btnNouveauRespSociete_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>e-Mail société:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMailSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="TextMailSociete_RegularExpressionValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextMailSociete"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeSociete">*
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>Reduction US:
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbReductionUS" runat="server" AutoPostBack="True" 
                                                oncheckedchanged="cbReductionUS_CheckedChanged" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td>Nom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomResponsableSociete" runat="server"></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomResponsableSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomResponsableSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Prénom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenomRespSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Tél fixe société:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTelephoneSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Tél mobile société:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMobileSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            CIN:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCinRespSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCinRespSociete_RequiredFieldValidator" 
                                                runat="server" ControlToValidate="TextCinRespSociete" ErrorMessage="" 
                                                ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>e-Mail:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMailRespSociete" runat="server"></asp:TextBox>

                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="TextMailRespSociete_RegularExpressionValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextMailRespSociete"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeSociete">*
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        
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
                                        <td>Tél fixe:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextFixeRespSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Tél mobile:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMobileRespSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Adresse société
                                        </td>
                                        <td></td>
                                        <td colspan="6" class="titreTab">
                                            Adresse responsable société
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Province:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProvinceSociete" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlProvinceSociete_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlProvinceSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlProvinceSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionSociete" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlRegionSociete_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRegionSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlRegionSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Province:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlprovinceRespSociete" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlprovinceRespSociete_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlprovinceRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlprovinceRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionRespSociete" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlRegionRespSociete_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRegionRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlRegionRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>District:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrictSociete" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDistrictSociete_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDistrictSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDistrictSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commune:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCommuneSociete" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlCommuneSociete_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCommuneSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCommuneSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>District:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrictRespSociete" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlDistrictRespSociete_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDistrictRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDistrictRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commune:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCommuneRespSociete" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlCommuneRespSociete_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCommuneRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCommuneRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Arrondissement:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArrondissementSociete" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlArrondissementSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlArrondissementSociete" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                        <td>Arrondissement:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArrondissementRespSociete" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlArrondissementRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlArrondissementRespSociete" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Adresse société:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAdresseSociete" runat="server"></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresseSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresseSociete" ValidationGroup="groupeSociete">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        
                                        <td>Quartier:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextQuartierSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextQuartierSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextQuartierSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Adresse
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAdresseRespSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresseRespSociete_RequiredFieldValidator" 
                                                runat="server" ControlToValidate="TextAdresseRespSociete" ErrorMessage="" 
                                                ValidationGroup="groupeSociete">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Quartier:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextQuartierRespSociete" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextQuartierRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextQuartierRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="13">
                                            <asp:ValidationSummary ID="ValidationSummary_Societe" runat="server" ValidationGroup="groupeSociete"/>
                                            <asp:HiddenField ID="hfNumSociete" runat="server" />
                                            <asp:HiddenField ID="hfNomSociete" runat="server" />
                                            <asp:HiddenField ID="hfNumIndividuResponsable" runat="server" />
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
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" ValidationGroup="groupeSociete"
                                                onclick="btnModifier_Click" SkinID="btnValidation"/>
                                             <cc1:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server"
                                                    TargetControlID="btnModifier" ConfirmText="Voulez vous vraiment modifier?" />
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                ValidationGroup="groupeSociete" onclick="btnValider_Click" SkinID="btnValidation"/>
                                            
                                            
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            
                    </div>
                </div>

                <div class="clear"><br /></div>

                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste des sociétés
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_Liste" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriSociete" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriSociete_SelectedIndexChanged">
                                    <asp:ListItem Text="N°" Value="numSociete"></asp:ListItem>
                                    <asp:ListItem Text="Nom société" Value="nomSociete"></asp:ListItem>
                                    <asp:ListItem Text="Secteur d'activité" Value="secteurActiviteSociete"></asp:ListItem>
                                    <asp:ListItem Text="Prénom responsable" Value="individu.prenomIndividu"></asp:ListItem>
                                    <asp:ListItem Text="Nom responsable" Value="individu.nomIndividu"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheSociete" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheSociete" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheSociete_Click" />
                                <asp:GridView ID="gvSociete" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvSociete_PageIndexChanging" 
                                    onrowcommand="gvSociete_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numSociete") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numSociete") %>' />
                                                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer la société " + Eval("nomSociete") + "? \nN°: " + Eval("numSociete")%>' >
                                                </cc1:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numSociete" HeaderText="N°" />
                                        <asp:BoundField DataField="nomSociete" HeaderText="Nom société" />
                                        <asp:BoundField DataField="secteurActiviteSociete" HeaderText="Secteur d'activité" />
                                        <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                
            </div>
    </div>
</asp:Content>
