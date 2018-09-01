<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/chef/MasterChef.Master" AutoEventWireup="true" CodeBehind="Prelevement.aspx.cs" Inherits="AppWeb.ihmActeur.chef.Prelevement" %>
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
                &nbsp;&nbsp;Prélèvement
            </div>
            <div class="contentFormulaires">
                <div id="OneLayoteLeft40">
                    <div class="formContent" id="bgInformation">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Information
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_InformationFB" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hfAutorisationDepart" runat="server" />
                                    <table width="100%">
                                         <tr>
                                            <td colspan="2">
                                                <div class="titreTab">
                                                    Autorisation de voyage
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                N°Autorisation de voyage:
                                            </td>
                                            <td>
                                                <asp:Label ID="labNumAV" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>du:
                                            </td>
                                            <td>
                                                <asp:Label ID="labDateAV" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="titreTab">
                                                    Fiche de bord
                                                </div>
                                            </td>
                            
                                        </tr>
                                        <tr>
                                            <td>N°Fiche de bord:
                                            </td>
                                            <td>
                                                <asp:Label ID="labNumFB" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                            
                                        </tr>
                                        <tr>
                                            <td>Date heure de départ:
                                            </td>
                                            <td>
                                                <asp:Label ID="labDateHeureFB" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                            
                                        </tr>
                               
                                        <tr>
                                            <td colspan="2">
                                                <div class="titreTab">
                                                    Chauffeur
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nom:
                                            </td>
                                            <td>
                                                <asp:Label ID="labNomChauffeur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:Label ID="labPrenomChauffeur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="titreTab">
                                                     Voiture
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Matricule:
                                            </td>
                                            <td>
                                                <asp:Label ID="labMatriculeVoiture" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Marque:
                                            </td>
                                            <td>
                                                <asp:Label ID="labMarqueVoiture" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Couleur:
                                            </td>
                                            <td>
                                                <asp:Label ID="labCouleurVoiture" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Poids autorisé:
                                            </td>
                                            <td>
                                                <asp:Label ID="labPoidsAutoriseVoiture" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="titreTab">
                                                    Voyage
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Itinéraire:
                                            </td>
                                            <td>
                                                <asp:Label ID="labItineraire" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Distance à parcourire:
                                            </td>
                                            <td>
                                                <asp:Label ID="labDistance" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Durée du trajet:
                                            </td>
                                            <td>
                                                <asp:Label ID="labDureeTrajet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nombre de repos:
                                            </td>
                                            <td>
                                                <asp:Label ID="labNombreRepos" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="titreTab">
                                                    Recette
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant:
                                            </td>
                                            <td>
                                                <asp:Label ID="labMotant" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Reste:
                                            </td>
                                            <td>
                                                <asp:Label ID="labReste" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <cc1:UpdatePanelAnimationExtender ID="UpdatePanel_InformationFB_UpdatePanelAnimationExtender" runat="server" BehaviorID="animationInformation" TargetControlID="UpdatePanel_InformationFB">
                                <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Store the original height of the panel --%>
                                                <ScriptAction Script="var a = $find('animationInformation'); a._originalHeight = a._element.offsetHeight;" />
                                
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="bgInformation" Enabled="false" />
                                                </Parallel>
                                                <StyleAction Attribute="overflow" Value="hidden" />
                                
                                                <%-- Do each of the selected effects --%>
                                                <Parallel duration="0" Fps="30">
                                                    <FadeOut AnimationTarget="bgInformation" minimumOpacity=".2" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <Parallel duration="0" Fps="30">
                                                    <FadeIn AnimationTarget="bgInformation" minimumOpacity=".2" />
                                                </Parallel>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="bgInformation" Enabled="true" />
                                                </Parallel>                            
                                            </Sequence>
                                        </OnUpdated>
                                    </Animations>
                            </cc1:UpdatePanelAnimationExtender>
                        </div>
                    </div>
                </div>
                <div id="OneLayoteRight60">
                    <div class="formContent" id="bgPrelevement">
                        <asp:UpdatePanel ID="UpdatePanel_Prelevement" runat="server">
                            <ContentTemplate>
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Prélèvement
                                </div>
                                <div class="formulaire">
                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                Avance sur carburant maximum:
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="LabCarburantMax" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                Avance pour chauffeur maximum:
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="LabChauffeurMax" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTypeRecuAD" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ddlTypeRecuAD_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ValidationGroup="groupePrelevement" ControlToValidate="ddlTypeRecuAD">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>Montant:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMontant" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="TextMontant_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ValidationGroup="groupePrelevement" ControlToValidate="TextMontant">*
                                                </asp:RequiredFieldValidator>
                                                <cc1:filteredtextboxextender
                                                ID="TextMontant_FilteredTextBoxExtender"
                                                runat="server" 
                                                TargetControlID="TextMontant"
                                                FilterType="Custom, Numbers"
                                                ValidChars="," />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:HiddenField ID="hfPrelevement" runat="server" />
                                                <div id="divIndication" runat="server" class="divIndicationClass">
                                                </div>
                                                <asp:ValidationSummary ID="ValidationSummary_Prelevement" runat="server" 
                                                ValidationGroup="groupePrelevement"/>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="5">
                                                <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                                    onclick="btnAnnuler_Click" SkinID="btnValidation"/>
                                                <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                    onclick="btnModifier_Click" Enabled="false" SkinID="btnValidation"/>
                                                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                    TargetControlID="btnModifier" 
                                                    ConfirmText="Voulez vous vraiment modifier?" >
                                                </cc1:ConfirmButtonExtender>
                                                <asp:Button ID="btnValideAvance" runat="server" Text="Valider" 
                                                    onclick="btnValideAvance_Click" SkinID="btnValidation" ValidationGroup="groupePrelevement"/>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <table width="100%">
                                        <tr>
                                            <td class="titreTab">Prélèvement</td>
                                        </tr>
                                    </table><br />
                                    <asp:GridView ID="gvPrelevement" runat="server" AutoGenerateColumns="False" 
                                        EnableModelValidation="True" onrowcommand="gvPrelevement_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("numPrelevement") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                        CommandArgument='<%# Eval("numPrelevement") %>' />
                                                    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                        TargetControlID="ibDelete"
                                                        ConfirmText='<%# "Vouler vous vraiment supprimer le prélèvement N°" + Eval("numPrelevement") + "? \nMontant: " + Eval("montantPrelevement")  + " \nType: " +  Eval("commentaire")%>' >
                                                        </cc1:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numPrelevement" HeaderText="N°" />
                                            <asp:BoundField DataField="montantPrelevement" HeaderText="Montant" />
                                            <asp:BoundField DataField="commentaire" HeaderText="Type" />
                                            <asp:BoundField DataField="datePrelevement" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="divListe">
                                    <table width="100%">
                                        <tr>
                                            <td class="titreTab">Reçu</td>
                                        </tr>
                                    </table><br />
                                    <asp:GridView ID="gvRecuAD" runat="server" AutoGenerateColumns="False" 
                                        EnableModelValidation="True">
                                        <Columns>
                                            <asp:BoundField DataField="numRecuAD" HeaderText="N°" />
                                            <asp:BoundField DataField="typeRecuAD" HeaderText="Type" />
                                            <asp:BoundField DataField="libele" HeaderText="Libellé" />
                                            <asp:BoundField DataField="montant" HeaderText="Montant" />
                                            <asp:BoundField DataField="dateRecu" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_UpdatePanel_Prelevement" runat="server" BehaviorID="animationPrelevement" TargetControlID="UpdatePanel_Prelevement">
                            <Animations>
                                    <OnUpdating>
                                        <Sequence>
                                            <%-- Store the original height of the panel --%>
                                            <ScriptAction Script="var a = $find('animationPrelevement'); a._originalHeight = a._element.offsetHeight;" />
                                
                                            <%-- Disable all the controls --%>
                                            <Parallel duration="0">
                                                <EnableAction AnimationTarget="gvPrelevement" Enabled="false" />
                                            </Parallel>
                                            <StyleAction Attribute="overflow" Value="hidden" />
                                
                                            <%-- Do each of the selected effects --%>
                                            <Parallel duration="0" Fps="30">
                                                <FadeOut AnimationTarget="bgPrelevement" minimumOpacity=".2" />
                                            </Parallel>
                                        </Sequence>
                                    </OnUpdating>
                                    <OnUpdated>
                                        <Sequence>
                                            <Parallel duration="0" Fps="30">
                                                <FadeIn AnimationTarget="bgPrelevement" minimumOpacity=".2" />
                                            </Parallel>
                                            <Parallel duration="0">
                                                <EnableAction AnimationTarget="gvPrelevement" Enabled="true" />
                                            </Parallel>                            
                                        </Sequence>
                                    </OnUpdated>
                                </Animations>
                        </cc1:UpdatePanelAnimationExtender>
                    </div>
                </div>
                <div class="clear"></div>
                <br />
            </div>
            <br />
            <div class="formContent">
            <div class="collapsePanelHeader">
                 &nbsp;&nbsp;Autorisation de départ
            </div>
            <div class="divListe">
                <asp:UpdatePanel ID="UpdatePanel_ListeAutorisationDepart" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlTriRechercheAD" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlTriRechercheAD_SelectedIndexChanged">
                            <asp:ListItem Text="N°Autorisation départ" Value="autorisationdepart.numAutorisationDepart"></asp:ListItem>
                            <asp:ListItem Text="Date de départ" Value="dateHeurDepart"></asp:ListItem>
                            <asp:ListItem Text="Nom chauffeur" Value="nomChauffeur"></asp:ListItem>
                            <asp:ListItem Text="Prénom chauffeur" Value="prenomChauffeur"></asp:ListItem>
                            <asp:ListItem Text="Matricule voiture" Value="numImmatricule"></asp:ListItem>
                            <asp:ListItem Text="Marque voiture" Value="marque"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextRechercheAD" runat="server"></asp:TextBox>
                        <asp:Button ID="btnRechercheAD" runat="server" Text="Rechercher" 
                            onclick="btnRechercheAD_Click" />
                        <asp:GridView ID="gvAutorisationDeVoyage" runat="server" 
                            AutoGenerateColumns="False" EnableModelValidation="True" 
                            AllowPaging="True" 
                            onpageindexchanging="gvAutorisationDeVoyage_PageIndexChanging" 
                            onrowcommand="gvAutorisationDeVoyage_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                            CommandArgument='<%# Eval("numAutorisationDepart") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numAutorisationDepart" HeaderText="N°Autorisation départ" />
                                <asp:BoundField DataField="dateDepart" HeaderText="Date de départ" DataFormatString="{0:dd MMMM yyyy HH:mm}"/>
                                <asp:BoundField DataField="itineraire" HeaderText="Itineraire" />
                                <asp:BoundField DataField="chauffeur" HeaderText="Chauffeur" />
                                <asp:BoundField DataField="voiture" HeaderText="Voiture" />
                                <asp:BoundField DataField="montant" HeaderText="Montant" />
                                <asp:BoundField DataField="montantRecu" HeaderText="Montant reçu" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            </div>
        </div>
    </div>
</asp:Content>
