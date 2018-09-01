<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Itineraire.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Itineraire" %>
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
                &nbsp;&nbsp;Itinéraire
            </div>

            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="Formulaire_UpdatePanel" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                
                                    <tr>
                                        <td class="titreTab" colspan="6">
                                            Itinéraire
                                        </td>
                                    </tr>
                                    <tr>
                                         <td colspan="6">&nbsp;
                                         </td>
                                    </tr>
                                    <tr>
                                        <td>Itinéraire:
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="ddlVilleD" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlVilleD_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlVilleD_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlVilleD" PromptText="Recherche">
                                            </asp:ListSearchExtender>-
                                            <asp:DropDownList ID="ddlVilleF" runat="server">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlVilleF_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlVilleF" PromptText="Recherche">
                                            </asp:ListSearchExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Distance:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDistance" runat="server" Width="120"></asp:TextBox>Km
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextDistance_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextDistance"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDistance_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextDistance" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>Nombre repos:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNbRepos" runat="server" Width="120"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextNbRepos_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextNbRepos"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNbRepos_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextNbRepos" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Durée trajet:
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="TextDureeTrajetJ" runat="server" Width="20px" Columns="0" MaxLength="2">00</asp:TextBox>J
                                            <asp:RequiredFieldValidator ID="TextDureeTrajetJ_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextDureeTrajetJ" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender
                                                    ID="TextDureeTrajetJ_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextDureeTrajetJ"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                            <asp:TextBox ID="TextDureeTrajetH" runat="server" Width="20px" MaxLength="2">00</asp:TextBox>H
                                            <asp:RequiredFieldValidator ID="TextDureeTrajetH_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextDureeTrajetH" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender
                                                    ID="TextDureeTrajetH_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextDureeTrajetH"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                            <asp:TextBox ID="TextDureeTrajetM" runat="server" Width="20px" MaxLength="2">00</asp:TextBox>Min
                                            <asp:RequiredFieldValidator ID="TextDureeTrajetM_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextDureeTrajetM" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender
                                                    ID="TextDureeTrajetM_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextDureeTrajetM"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">Information sur excédent de bagage
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Poids autorisé:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPoidsAutorise" runat="server" Width="120"></asp:TextBox>Kg
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextPoidsAutorise_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextPoidsAutorise"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPoidsAutorise_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextPoidsAutorise" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td>Prix excédent:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrixExcedent" runat="server" Width="120"></asp:TextBox>Ar/Kg
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextPrixExcedent_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextPrixExcedent"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPrixExcedent_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextPrixExcedent" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">Tarif de base commission
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Interne:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCommissionInterne" runat="server" Width="120">0</asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextCommissionInterne_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextCommissionInterne"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCommissionInterne_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextCommissionInterne" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabCommentaireInterne" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Par Kg:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCommissionParKg" runat="server" Width="120"></asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextCommissionParKg_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextCommissionParKg"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCommissionParKg_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextCommissionParKg" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabCommentaireParKg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Par pièce:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCommissionParPiece" runat="server" Width="120"></asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextCommissionParPiece_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextCommissionParPiece"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCommissionParPiece_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextCommissionParPiece" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabCommissionParPiece" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Tarif base billet
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td>Tarif billet:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTarifBillet" runat="server" Width="120"></asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTarifBillet_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTarifBillet"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td colspan="4">
                                            <asp:RequiredFieldValidator ID="TextTarifBillet_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextTarifBillet" ValidationGroup="groupeItineraire">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:ValidationSummary ID="Itineraire_ValidationSummary" runat="server" 
                                            ValidationGroup="groupeItineraire"/>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                                onclick="btnAnnuler_Click" SkinId="btnValidation"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                SkinId="btnValidation" ValidationGroup="groupeItineraire" 
                                                onclick="btnModifier_Click"/>
                                            <asp:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider"  
                                                ValidationGroup="groupeItineraire" onclick="btnValider_Click" SkinId="btnValidation"/>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                    
                    <div class="divListe">
                        <div class="formContent2">
                            <asp:UpdatePanel ID="UpdatePanelListeRNItineraire" runat="server">
                                <ContentTemplate>
                                    <div class="titreLG">
                                        &nbsp;&nbsp;Passant par la route nationale
                                    </div>
                                    <div class="divListe">
                                        <asp:HiddenField ID="hfIdItineraire" runat="server" />
                                        <asp:DropDownList ID="ddlTriRNItineraire" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriRNItineraire_SelectedIndexChanged">
                                            <asp:ListItem Text="Route nationale" Value="routenationale.routeNationale"></asp:ListItem>
                                            <asp:ListItem Text="Distance" Value="routenationale.distanceRN"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheRNItineraire" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheRNItineraire" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheRNItineraire_Click" />
                                        <asp:GridView ID="gvRNItineraire" runat="server" AllowPaging="True" 
                                            onpageindexchanging="gvRNItineraire_PageIndexChanging" 
                                            onrowcommand="gvRNItineraire_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("Route nationale") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="divListe">
                        <div class="formContent2">
                            <asp:UpdatePanel ID="UpdatePanelListeRN" runat="server">
                                <ContentTemplate>
                                    <div class="titreLG">
                                        &nbsp;&nbsp;Liste route nationale
                                    </div>
                                    <div class="divListe">
                                        <asp:DropDownList ID="ddlTriRN" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriRN_SelectedIndexChanged">
                                            <asp:ListItem Text="Route nationale" Value="routenationale.routeNationale"></asp:ListItem>
                                            <asp:ListItem Text="Distance" Value="routenationale.distanceRN"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheRN" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheRN" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheRN_Click" />
                                        <asp:GridView ID="gvRN" runat="server" AllowPaging="True" 
                                            onpageindexchanging="gvRN_PageIndexChanging" 
                                            onrowcommand="gvRN_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("Route nationale") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
              
            </div>

             <div id="OneLayoteListeRight">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Trajet
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_FormulaireTrajet" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Trajet
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Trajet:
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="ddlTrajetVilleD" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTrajetVilleD_SelectedIndexChanged">
                                            </asp:DropDownList>-
                                            <asp:DropDownList ID="ddlTrajetVilleF" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Distance:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDistanceTrajet" runat="server" Width="120"></asp:TextBox>
                                            Km
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextDistanceTrajet_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextDistanceTrajet"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDistanceTrajet_RequiredFieldValidator" runat="server" 
                                            ControlToValidate="TextDistanceTrajet"
                                            ErrorMessage="" ValidationGroup="groupeTrajet">*
                                            </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>Durée trajet:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTrajetDureeJ" runat="server" Width="20px" MaxLength="2">00</asp:TextBox>J
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTrajetDureeJ_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTrajetDureeJ"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                            <asp:RequiredFieldValidator ID="TextTrajetDureeJ_RequiredFieldValidator" runat="server" 
                                            ControlToValidate="TextTrajetDureeJ"
                                            ErrorMessage="" ValidationGroup="groupeTrajet">*
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextTrajetDureeH" runat="server" Width="20px" MaxLength="2">00</asp:TextBox>H
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTrajetDureeH_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTrajetDureeH"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                            <asp:RequiredFieldValidator ID="TextTrajetDureeH_RequiredFieldValidator" runat="server" 
                                            ControlToValidate="TextTrajetDureeH"
                                            ErrorMessage="" ValidationGroup="groupeTrajet">*
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextTrajetDureeM" runat="server" Width="20px" MaxLength="2">00</asp:TextBox>Min
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTrajetDureeM_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTrajetDureeM"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                            <asp:RequiredFieldValidator ID="TextTrajetDureeM_RequiredFieldValidator" runat="server" 
                                            ControlToValidate="TextTrajetDureeM"
                                            ErrorMessage="" ValidationGroup="groupeTrajet">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Tarif base commission
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Interne:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTCInterne" runat="server" Width="120">0</asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTCInterne_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTCInterne"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextTCInterne_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextTCInterne" ValidationGroup="groupeTrajet">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabCommentaireTCInterne" runat="server" Text=""></asp:Label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Par Kg:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTCParKg" runat="server" Width="120"></asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTCParKg_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTCParKg"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextTCParKg_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextTCParKg" ValidationGroup="groupeTrajet">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabCommentaireTCParKg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Par pièce:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTCParPiece" runat="server" Width="120"></asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTCParPiece_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTCParPiece"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextTCParPiece_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextTCParPiece" ValidationGroup="groupeTrajet">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabCommentaireTCParPiece" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Tarif base billet
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td>Tarif billet:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTarifBilletTrajet" runat="server" Width="120"></asp:TextBox>Ar
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextTarifBilletTrajet_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextTarifBilletTrajet"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                        </td>
                                        <td colspan="4">
                                            <asp:RequiredFieldValidator ID="TextTarifBilletTrajet_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextTarifBilletTrajet" ValidationGroup="groupeTrajet">*
                                             </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_Trajet" runat="server" ValidationGroup="groupeTrajet"/>
                                            <asp:HiddenField ID="hfNumTrajet" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAnnulerTrajet" runat="server" Text="Nouveau" 
                                                onclick="btnAnnulerTrajet_Click" SkinId="btnValidation"/>
                                            <asp:Button ID="btnModifierTrajet" runat="server" Text="Modifier" 
                                                SkinId="btnValidation" onclick="btnModifierTrajet_Click" ValidationGroup="groupeTrajet"/>
                                            <asp:ConfirmButtonExtender ID="btnModifierTrajet_ConfirmButtonExtender" runat="server" 
                                                TargetControlID="btnModifierTrajet"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValiderTrajet" runat="server" Text="Valider" 
                                                onclick="btnValiderTrajet_Click" ValidationGroup="groupeTrajet" SkinId="btnValidation"/>
                                    
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                     <div class="divListe">
                            <asp:UpdatePanel ID="UpdatePanel_ListeTrajetItineraire" runat="server">
                                <ContentTemplate>
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Liste de trajet de l'itineraire
                                        </div>
                                        <div class="divListe">
                                            <asp:DropDownList ID="ddlTriTrajetItineraire" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlTriTrajetItineraire_SelectedIndexChanged">
                                                <asp:ListItem Text="N°" Value="trajet.numTrajet"></asp:ListItem>
                                               <asp:ListItem Text="Distance" Value="distanceTrajet"></asp:ListItem>
                                               <asp:ListItem Text="Durée" Value="dureeTrajet"></asp:ListItem>
                                               <asp:ListItem Text="Ville" Value="ville.nomVille"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TextRechercheTrajetItineraire" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnRechercheTrajetItineraire" runat="server" Text="Rechercher" 
                                                onclick="btnRechercheTrajetItineraire_Click" />
                                            <asp:GridView ID="gvTrajetItineraire" runat="server" 
                                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                                AllowPaging="True" onpageindexchanging="gvTrajetItineraire_PageIndexChanging" 
                                                onrowcommand="gvTrajetItineraire_RowCommand" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                                CommandArgument='<%# Eval("numTrajet") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("numTrajet") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="numTrajet" HeaderText="N°" />
                                                    <asp:BoundField DataField="distanceTrajet" HeaderText="Distance" />
                                                    <asp:BoundField DataField="dureeTrajet" HeaderText="Durée" />
                                                    <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                    <div class="divListe">
                        <div class="formContent2">
                            <div class="titreLG">
                                &nbsp;&nbsp;Liste trajet
                            </div>
                            <div class="formulaire">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlTriTrajet" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriTrajet_SelectedIndexChanged">
                                               <asp:ListItem Text="N°" Value="trajet.numTrajet"></asp:ListItem>
                                               <asp:ListItem Text="Distance" Value="distanceTrajet"></asp:ListItem>
                                               <asp:ListItem Text="Durée" Value="dureeTrajet"></asp:ListItem>
                                               <asp:ListItem Text="Ville" Value="ville.nomVille"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheTrajet" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheTrajet" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheTrajet_Click" />
                                        <asp:GridView ID="gvTrajet" runat="server" AutoGenerateColumns="False" 
                                            EnableModelValidation="True" AllowPaging="True" 
                                            onpageindexchanging="gvTrajet_PageIndexChanging" 
                                            onrowcommand="gvTrajet_RowCommand" PageSize="5" >
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("numTrajet") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numTrajet" HeaderText="N°" />
                                                <asp:BoundField DataField="distanceTrajet" HeaderText="Distance" />
                                                <asp:BoundField DataField="dureeTrajet" HeaderText="Durée" />
                                                <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            
             </div>

            <div class="clear">
                <br />
            </div>

            <div class="formContent">
                <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Liste itineraire
                </div>
                <div class="divListe">
                    <asp:UpdatePanel ID="UpdatePanel_ListeItineraire" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTriItineraire" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="N°" Value="idItineraire"></asp:ListItem>
                                <asp:ListItem Text="Distance" Value="distanceParcour"></asp:ListItem>
                                <asp:ListItem Text="Durée" Value="dureeTrajet"></asp:ListItem>
                                <asp:ListItem Text="Poids bagage autorisé" Value="poidAutorise"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheItineraire" runat="server"></asp:TextBox>
                            <asp:Button ID="btnItineraire" runat="server" Text="Rechercher" />
                            <asp:GridView ID="gvItineraire" runat="server" AutoGenerateColumns="False" 
                                EnableModelValidation="True" AllowPaging="True" 
                                onpageindexchanging="gvItineraire_PageIndexChanging" 
                                onrowcommand="gvItineraire_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("idItineraire") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="idItineraire" HeaderText="N°" />
                                    <asp:BoundField DataField="distanceParcour" HeaderText="Distance" />
                                    <asp:BoundField DataField="dureeTrajet" HeaderText="Durée" />
                                    <asp:BoundField DataField="poidAutorise" HeaderText="Poids bagage autorisé" />
                                    <asp:BoundField DataField="itineraire" HeaderText="Itineraire" />
                                    <asp:BoundField DataField="axe" HeaderText="Axe" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
            </div>
        </div>
        
     </div>
</asp:Content>
