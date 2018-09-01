<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Vehicule.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Vehicule" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>

    <div id="OneLayote">
        <div class="formContent3">
            <asp:UpdatePanel ID="UpdatePanel_Titre" runat="server">
                <ContentTemplate>
                    <div class="grandTitre">
                        <asp:LinkButton ID="btnProprietaire" runat="server" 
                            CssClass="linkClass" onclick="btnProprietaire_Click"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:Button ID="btnProprietaireListe" runat="server" 
                            Text="Propriétaire" onclick="btnProprietaireListe_Click" />
                            Autre véhicule:<asp:DropDownList ID="ddlListeVehicule" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlListeVehicule_SelectedIndexChanged"></asp:DropDownList>
                        <asp:HiddenField ID="hfNumProprietaire" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <div id="OneLayoteLeft50">   
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Véhicule
                    </div>

                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_FormulaireVehicule" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Véhicule<asp:HiddenField ID="hfNumVehicule" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Matricule:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMatricule" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextMatricule_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextMatricule" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="TextMatricule_RegularExpressionValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextMatricule" ValidationGroup="gVehicule" ValidationExpression="\d{4} \w{2,3}">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>Marque:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMarque" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextMarque_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextMarque" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Type:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextType" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextType_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextType" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>N°Série:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNumSerie" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNumSerie_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNumSerie" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Energie:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSourceEnergie" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>N°Moteur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNumMoteur" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNumMoteur_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNumMoteur" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Puissance:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPuissance" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Couleur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCouleur" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Place assise:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPlaceAssise" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextPlaceAssise_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextPlaceAssise"
                                                FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPlaceAssise_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPlaceAssise" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Nombre colonne:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNbColonne" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextNbColonne_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextNbColonne"
                                                FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNbColonne_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNbColonne" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Poids total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPoidTotal" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextPoidTotal_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextPoidTotal"
                                                FilterType="Numbers" />
                                        </td>
                                        <td></td>
                                        <td>Poids vide:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPoidVide" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextPoidVide_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextPoidVide"
                                                FilterType="Numbers" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Image:
                                        </td>
                                        <td colspan="3">
                                            <asp:FileUpload ID="FileUpload_Image" runat="server" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Licence<asp:HiddenField ID="hfNumLicence" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Numeros:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNumerosLicence" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNumerosLicence_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNumerosLicence" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="checkProvisoire" runat="server" Text="Licence provisoire"/>
                                        </td>
                                        
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
                                            ControlToValidate="ddlZone" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Cooperative:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCooperative" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCooperative_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlCooperative" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date de mise en circulation:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateMiseCirculation" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextDateMiseCirculation_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateMiseCirculation" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDateMiseCirculation_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextDateMiseCirculation" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Date première exploitation:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPremiereExploitation" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextPremiereExploitation_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextPremiereExploitation" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPremiereExploitation_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPremiereExploitation" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Valide du:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateValideDu" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextDateValideDu_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateValideDu" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDateValideDu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextDateValideDu" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>au:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateValideAu" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextDateValideAu_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateValideAu" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDateValideAu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextDateValideAu" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nombre de place payante:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNbPlacePayante" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextNbPlacePayante_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextNbPlacePayante"
                                                FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNbPlacePayante_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNbPlacePayante" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Itinéraire 1:
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="ddlDebutItineraire1" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDebutItineraire1_SelectedIndexChanged">
                                            </asp:DropDownList>-
                                            <asp:DropDownList ID="ddlFinItineraire1" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlFinItineraire1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlDebutItineraire1_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlDebutItineraire1" PromptText="Recherche">
                                            </asp:ListSearchExtender>
                                            <asp:ListSearchExtender ID="ddlFinItineraire1_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlFinItineraire1" PromptText="Recherche">
                                            </asp:ListSearchExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlFinItineraire1_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlFinItineraire1" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Itineraire 2:
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="ddlDebutItineraire2" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDebutItineraire2_SelectedIndexChanged">
                                            </asp:DropDownList>-
                                            <asp:DropDownList ID="ddlFinItineraire2" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlFinItineraire2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlDebutItineraire2_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlDebutItineraire2" PromptText="Recherche">
                                            </asp:ListSearchExtender>
                                            <asp:ListSearchExtender ID="ddlFinItineraire2_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlFinItineraire2" PromptText="Recherche">
                                            </asp:ListSearchExtender>
                                        </td>
                                        
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Condition minimum de voyage
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nombre de place min:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNbPlaceMin" runat="server"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextNbPlaceMin_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextNbPlaceMin"
                                                FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNbPlaceMin_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNbPlaceMin" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Poids bagage max:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPoidsBagageMax" runat="server" Width="120"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextPoidsBagageMax_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextPoidsBagageMax"
                                                FilterType="Numbers" />
                                                Kg
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPoidsBagageMax_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPoidsBagageMax" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Avance sur carburant:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAvanceCarburant" runat="server" Width="120"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                ID="TextAvanceCarburant_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextAvanceCarburant"
                                                FilterType="Numbers" />
                                                Ar
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAvanceCarburant_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAvanceCarburant" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Avance pour chauffeur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAvanceChauffeur" runat="server" Width="120"></asp:TextBox>
                                            Ar
                                            <asp:FilteredTextBoxExtender
                                                ID="TextAvanceChauffeur_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextAvanceChauffeur"
                                                FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAvanceChauffeur_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAvanceChauffeur" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Fond:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextFond" runat="server" Width="120"></asp:TextBox>
                                            Ar
                                            <asp:FilteredTextBoxExtender
                                                ID="TextFond_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextFond"
                                                FilterType="Numbers" />
                                        </td>
                                        <td colspan="4">
                                            <asp:RequiredFieldValidator ID="TextFond_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextFond" ValidationGroup="gVehicule">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="VSVehicule" runat="server" ValidationGroup="gVehicule"/>
                                            <div id="indicationVehicule" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                        onclick="btnNouveau_Click" SkinID="btnValidation"/>
                                    <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                        ValidationGroup="gVehicule" onclick="btnModifier_Click" SkinID="btnValidation"/>
                                    <asp:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server" 
                                        TargetControlID="btnModifier"
                                        ConfirmText="Voulez vous vraiment modifier le véhicule?">
                                    </asp:ConfirmButtonExtender>
                                    <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" 
                                        ValidationGroup="gVehicule" onclick="btnEnregistrer_Click" SkinID="btnValidation"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Chauffeur
                    </div>
                    
                    
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Chauffeur" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfIdChauffeur" runat="server" />
                                <table>
                                    <tr>
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
                                        <td colspan="3"></td>
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
                                            <asp:TextBox ID="TextPrenomChauffeur" runat="server"></asp:TextBox>
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
                                        <td colspan="3">
                           
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Image:
                                        </td>
                                        <td colspan="4">
                                            <asp:FileUpload ID="FileUpload_Chauffeur" runat="server" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="VSChauffeur" runat="server" ValidationGroup="gChauffeur"/>
                                            <div id="indicationChauffeur" runat="server">
                                            </div>
                                            
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNouveauChauffeur" runat="server" Text="Nouveau" 
                                        onclick="btnNouveauChauffeur_Click" SkinID="btnValidation"/>
                                    <asp:Button ID="btnModiffierChauffeur" runat="server" Text="Modiffier" 
                                        onclick="btnModiffierChauffeur_Click" ValidationGroup="gChauffeur" SkinID="btnValidation"/>
                                    <asp:ConfirmButtonExtender ID="btnModiffierChauffeur_ConfirmButtonExtender" runat="server" 
                                        TargetControlID="btnModiffierChauffeur"
                                        ConfirmText="Voulez vous vraiment modifier?">
                                    </asp:ConfirmButtonExtender>
                                    <asp:Button ID="btnInsererChauffeur" runat="server" Text="Insérer" 
                                        onclick="btnInsererChauffeur_Click" ValidationGroup="gChauffeur" SkinID="btnValidation"/>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeChauffeur" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriChauffeur" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriChauffeur_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheChauffeur" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheChauffeur" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheChauffeur_Click" />
                                <asp:GridView ID="gvChauffeur" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" PageSize="5" 
                                    onpageindexchanging="gvChauffeur_PageIndexChanging" 
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
                                        <asp:BoundField DataField="adresseChauffeur" HeaderText="Adresse" />
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

            <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel_FormulaireProprietaire" runat="server" CssClass="" Visible="false" Width="90%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                   &nbsp;&nbsp;Abonner
                                </div>
                                <div class="formulaire">
                                    <table> 
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RadioListeProprietaire" runat="server" 
                                                    AutoPostBack="True" 
                                                    RepeatDirection="Horizontal" 
                                                    onselectedindexchanged="RadioListeProprietaire_SelectedIndexChanged">
                                                    <asp:ListItem Text="Individu" Value="individu" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Société" Value="societe"></asp:ListItem>
                                                    <asp:ListItem Text="Organisme" Value="organisme"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>      
                                    </table>
                                    <asp:Panel ID="PanelIndividu" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="6" class="titreTab">
                                                    Individu
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td colspan="6" class="titreTab">
                                                    Adresse
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Civilité:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCiviliteIndividu" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlCiviliteIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlCiviliteIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td colspan="4"></td>
                                                <td>Province:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProvinceIndividu" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlProvinceIndividu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlProvinceIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlProvinceIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    Region:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlRegionIndividu" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlRegionIndividu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlRegionIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlRegionIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextNomClient" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextNomClient_RequiredFieldValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextNomClient" ValidationGroup="groupeIndividu">*
                                                    </asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextPrenom" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>District:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDistrictIndividu" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlDistrictIndividu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlDistrictIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlDistrictIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>Commune:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCommuneIndividu" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlCommuneIndividu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlCommuneIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlCommuneIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Né le:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextDateNaissanceIndividu" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TextDateNaissanceIndividu_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="TextDateNaissanceIndividu" Format="dd MMMM yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td></td>
                                                <td>à:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextLieuNaissanceIndividu" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>Arrondissement:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlArrondissementIndividu" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlArrondissementIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlArrondissementIndividu" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinClient" runat="server"></asp:TextBox>
                                                
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextCinClient_RequiredFieldValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextCinClient" ValidationGroup="groupeIndividu">*
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>e-Mail:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMailIndividu" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="TextMailIndividu_RegularExpressionValidator" runat="server" 
                                                        ErrorMessage="" ControlToValidate="TextMailIndividu"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeIndividu">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td></td>
                                                <td>
                                                    Adresse:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextAdresseClient" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextAdresseClient_RequiredFieldValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextAdresseClient" ValidationGroup="groupeIndividu">*
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>Quartier:</td>
                                                <td>
                                                    <asp:TextBox ID="TextQuartierIndividu" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextQuartierIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextQuartierIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Profession:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextProfessionIndividu" runat="server"></asp:TextBox>
                                                </td>
                                                <td colspan="11">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tél fixe:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneFixeClient" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneMobile" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td colspan="7"></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelSociete" runat="server">
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
                                                <td>Société:
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
                                                        Text="Nouveau responsable" onclick="btnNouveauRespSociete_Click"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>e-Mail:
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
                                                        oncheckedchanged="cbReductionUS_CheckedChanged"/>
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
                                                <td>Tél fixe:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneSociete" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile:
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
                                                        runat="server" ControlToValidate="TextCinRespSociete" ErrorMessage="" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
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
                                                <td colspan="7"></td>
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
                                                        onselectedindexchanged="ddlProvinceSociete_SelectedIndexChanged">
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
                                                        onselectedindexchanged="ddlRegionRespSociete_SelectedIndexChanged">
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
                                                        onselectedindexchanged="ddlCommuneSociete_SelectedIndexChanged">
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
                                                <td>Adresse:
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
                                                        runat="server" ControlToValidate="TextAdresseRespSociete" ErrorMessage="" ValidationGroup="groupeSociete">*
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
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelOrganisme" runat="server">
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
                                                <td>e-Mail:
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
                                                <td colspan="7">
                                                </td>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextCinRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextCinRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>e-Mail:
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
                                                    onselectedindexchanged="ddlprovinceRespOrganisme_SelectedIndexChanged">
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
                                                    onselectedindexchanged="ddlRegionRespOrganisme_SelectedIndexChanged">
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
                                                    onselectedindexchanged="ddlCommuneOrganisme_SelectedIndexChanged">
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
                                                    onselectedindexchanged="ddlDistrictRespOrganisme_SelectedIndexChanged">
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
                                                    onselectedindexchanged="ddlCommuneRespOrganisme_SelectedIndexChanged">
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
                                        </table>
                                    </asp:Panel>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary_Abonner" runat="server" ValidationGroup="groupAbonnement"/>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnValiderProprietaire" runat="server" Text="Valider" 
                                                    ValidationGroup="groupProprietaire" onclick="btnValiderProprietaire_Click"/>
                                                <asp:Button ID="btnModifierProprietaire" runat="server" Text="Modifier" 
                                                    onclick="btnModifierProprietaire_Click" />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifierProprietaire" runat="server" 
                                                    TargetControlID="btnModifierProprietaire"
                                                    ConfirmText="">
                                                </asp:ConfirmButtonExtender>
                                                <asp:Button ID="btnAjouterProprietaire" runat="server" 
                                                    Text="Enregistrer nouveau proprietaire" ValidationGroup="groupProprietaire" 
                                                    onclick="btnAjouterProprietaire_Click"/>
                                                <asp:Button ID="btnNouveauProprietaire" runat="server" 
                                                    Text="Nouveau propriétaire" onclick="btnNouveauProprietaire_Click" />
                                                <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" onclick="btnAnnuler_Click"/>
                                                <asp:HiddenField ID="hfNumProprietaireTemp" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriProprietaire" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriProprietaire_SelectedIndexChanged">
                                        <asp:ListItem Text="N° Propriétaire" Value="numProprietaire"></asp:ListItem>
                                        <asp:ListItem Text="Nom" Value="nomIndividu"></asp:ListItem>
                                        <asp:ListItem Text="Prénom" Value="prenomIndividu"></asp:ListItem>
                                        <asp:ListItem Text="Nom société" Value="nomSociete"></asp:ListItem>
                                        <asp:ListItem Text="Nom responsable société" Value="societe.nomResponsable"></asp:ListItem>
                                        <asp:ListItem Text="Prénom responsable société" Value="societe.prenomResponsable"></asp:ListItem>
                                        <asp:ListItem Text="Nom organisme" Value="nomOrganisme"></asp:ListItem>
                                        <asp:ListItem Text="Nom responsable organisme" Value="organisme.nomResponsable"></asp:ListItem>
                                        <asp:ListItem Text="Prénom responsable organisme" Value="organisme.prenomResponsable"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheProprietaire" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercherProprietaire" runat="server" Text="Rechercher" 
                                        onclick="btnRechercherProprietaire_Click"/>
                                    <asp:GridView ID="gvProprietaire" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" PageSize="5" 
                                        onpageindexchanging="gvProprietaire_PageIndexChanging" 
                                        onrowcommand="gvProprietaire_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("numProprietaire") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numProprietaire" HeaderText="N° Propriétaire" />
                                            <asp:BoundField DataField="proprietaire" HeaderText="Individu/Société/Organisme" />
                                            <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                            <asp:BoundField DataField="contact" HeaderText="Contact" />
                                            <asp:BoundField DataField="respSociete" HeaderText="Resonsable société/Organisme" />
                                            <asp:BoundField DataField="respContact" HeaderText="Contact Responsable"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
