<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/controleur/MasterControleur.Master" AutoEventWireup="true" CodeBehind="RemplirFB.aspx.cs" Inherits="AppWeb.ihmActeur.controleur.RemplirFB" %>
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
                &nbsp;&nbsp;Fiche de bord N° <asp:Label ID="LabNumerosFB" runat="server" Text=""></asp:Label>
            </div>
            
            <div id="OneLayoteLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Information
                    </div>
                    <div class="formulaire">
                        <table width="100%">
                            <tr>
                                <td colspan="2" class="titreTab">
                                    Autorisation de voyage
                                </td>
                            </tr>
                            <tr>
                                <td>N°:
                                </td>
                                <td>
                                    <asp:Label ID="labNumAV" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Du:
                                </td>
                                <td>
                                    <asp:Label ID="labDateAV" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="titreTab">
                                    Fiche de bord
                                </td>
                            </tr>
                            <tr>
                                <td>N°:
                                </td>
                                <td>
                                    <asp:Label ID="labNumFB" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Pour le:
                                </td>
                                <td>
                                    <asp:Label ID="labDateHeureDepart" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="titreTab">
                                    Vehicule
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
                                <td colspan="2" class="titreTab">
                                    Chauffeur
                                </td>
                            </tr>
                            <tr>
                                <td>Nom:
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
                                <td colspan="2" class="titreTab">
                                    Voyage
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
                                <td>Durée du voyage:
                                </td>
                                <td>
                                    <asp:Label ID="labDureeTrajet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Distance:
                                </td>
                                <td>
                                    <asp:Label ID="labDistance" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:HiddenField ID="hfIdItineraire" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Trajet:
                                </td>
                                <td>
                                    <asp:Label ID="listeTrajet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Passagers
                    </div>
                    <div class="formulaire">
                        <div class="formulairePassager" id="bgPassager">
                            <asp:UpdatePanel ID="UpdatePanel_Passager" runat="server">
                                <ContentTemplate>
                                

                            <div runat="server" id="divValide">
                            <table width="100%">
                                <tr>
                                    <td colspan="5">
                                        <div class="titreTab">
                                               Billet
                                         </div>
                                    </td>
                                    
                                    <td></td>
                                   
                                </tr>
                                <tr>
                                    <td>N°Billet:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNumBillet" runat="server" 
                                            ontextchanged="TextNumBillet_TextChanged" AutoPostBack="True">
                                        </asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="TextNumBillet_RequiredFieldValidator" runat="server" ErrorMessage=""
                                        ControlToValidate="TextNumBillet" ValidationGroup="groupePassager">*
                                        </asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>Destination:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextDestination" runat="server" ReadOnly="true">
                                        </asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="TextDestination_RequiredFieldValidator" runat="server" ErrorMessage=""
                                        ControlToValidate="TextDestination" ValidationGroup="groupePassager">*
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>Type:</td>
                                    <td>
                                        <asp:TextBox ID="TextBilletPour" runat="server" ReadOnly="true">
                                        </asp:TextBox>
                                        
                                    </td>
                                    <td colspan="4">
                                        <asp:RequiredFieldValidator ID="TextBilletPour_RequiredFieldValidator" runat="server" 
                                        ErrorMessage="" ControlToValidate="TextBilletPour" ValidationGroup="groupePassager">*
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <div class="titreTab">
                                                            Passager
                                         </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Nom:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNomClient" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="TextNomClient_RequiredFieldValidator" runat="server" ErrorMessage=""
                                        ControlToValidate="TextNomClient" ValidationGroup="groupePassager">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                    </td>
                                    <td>Prénom:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPrenomClient" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>CIN:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextCinClient" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>Adresse:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextAdresseClient" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Téléphone:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextFixeClient" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>Portable:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPortableClient" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <div class="titreTab">Bagage</div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Poids:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPoidBagage" runat="server" AutoPostBack="True" 
                                            ontextchanged="TextPoidBagage_TextChanged" Width="120"></asp:TextBox>Kg
                                        
                                        <cc1:FilteredTextBoxExtender
                                            ID="TextPoidBagage_FilteredTextBoxExtender"
                                            runat="server" 
                                            TargetControlID="TextPoidBagage"
                                            FilterType="Custom, Numbers"
                                            ValidChars="," />
                                    </td>
                                    <td>
                                                <asp:RequiredFieldValidator ID="TextPoidBagage_RequiredFieldValidator" runat="server" ErrorMessage=""
                                        ValidationGroup="groupePassager" ControlToValidate="TextPoidBagage">*
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3">
                                        Droit de bagage:
                                   
                                        <asp:Label ID="LabDoitrBagage" runat="server" Text=""></asp:Label> Kg
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>N°Reçu:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNumRecu" runat="server"></asp:TextBox>
                                    </td>
                                    <td colspan="4">
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>Excédent:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextExedentPoid" runat="server" ReadOnly="true" Width="120"></asp:TextBox>Kg
                                    </td>
                                    <td>
                                    </td>
                                    <td>Motant:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPrixEcedent" runat="server" ReadOnly="true" Width="120"></asp:TextBox>Ar
                                    </td>
                                    <td></td>
                                </tr>
                                
                                
                            </table>
                            </div>

                            <div runat="server" id="divModif" visible="false">
                                <table width="100%">
                                <tr>
                                    <td colspan="5">
                                        <div class="titreTab">
                                                            Passager
                                         </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Nom:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNomModif" runat="server"></asp:TextBox>
                                       
                                    </td>
                                    <td>
                                         <asp:RequiredFieldValidator ID="TextNomModif_RequiredFieldValidator" runat="server" ErrorMessage=""
                                        ControlToValidate="TextNomModif" ValidationGroup="groupePModif">*
                                        </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                    </td>
                                    <td>Prénom:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPrenomModif" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>CIN:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextCinModif" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>Adresse:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextAdresseModif" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Téléphone:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTelephoneModif" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>Portable:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPortebleModif" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                
                                <tr>
                                    <td>Siège N°:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNumSiegeModif" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <div class="titreTab">Bagage</div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Poids:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPoidModif" runat="server" AutoPostBack="True" 
                                            ontextchanged="TextPoidModif_TextChanged" Width="120">
                                        </asp:TextBox>Kg
                                        
                                        <cc1:FilteredTextBoxExtender
                                            ID="TextPoidModif_FilteredTextBoxExtender"
                                            runat="server" 
                                            TargetControlID="TextPoidModif"
                                            FilterType="Custom, Numbers"
                                            ValidChars="," />
                                    </td>
                                    <td>
                                                <asp:RequiredFieldValidator ID="TextPoidModif_RequiredFieldValidator" runat="server" ErrorMessage=""
                                        ControlToValidate="TextPoidModif" ValidationGroup="groupePModif">*
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3">
                                        Droit de bagage:
                                   
                                        <asp:Label ID="LabDroitBagModif" runat="server" Text=""></asp:Label> Kg
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>N°Reçu:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNumRecuModif" runat="server"></asp:TextBox>
                                    </td>
                                    <td colspan="4">
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>Excédent:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextExcedentModif" runat="server" ReadOnly="true" Width="120"></asp:TextBox>Kg
                                    </td>
                                    <td>
                                    </td>
                                    <td>Montant:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextPrixExcedentModif" runat="server" ReadOnly="true" Width="120"></asp:TextBox>Ar
                                    </td>
                                    <td></td>
                                </tr>
                                
                                </table>    
                            </div>

                            <table> 
                                <tr>
                                    <td>
                                        <div id="divIndication" runat="server" class="divIndicationClass">
                                        </div>
                                        <asp:ValidationSummary ID="ValidationSummary_Passager" runat="server" ValidationGroup="groupePassager"/>
                                        <asp:ValidationSummary ID="ValidationSummary_PassagerModif" runat="server" ValidationGroup="groupePModif"/>
                                    </td>
                                </tr>
                            </table>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender_UpdatePanel_Passager" runat="server" BehaviorID="animationPassager" TargetControlID="UpdatePanel_Passager">
                            <Animations>
                                    <OnUpdating>
                                        <Sequence>
                                            <%-- Store the original height of the panel --%>
                                            <ScriptAction Script="var a = $find('animationPassager'); a._originalHeight = a._element.offsetHeight;" />
                                
                                            <%-- Disable all the controls --%>
                                            <Parallel duration="0">
                                            </Parallel>
                                            <StyleAction Attribute="overflow" Value="hidden" />
                                
                                            <%-- Do each of the selected effects --%>
                                            <Parallel duration="0" Fps="30">
                                                <FadeOut AnimationTarget="bgPassager" minimumOpacity=".2" />
                                            </Parallel>
                                        </Sequence>
                                    </OnUpdating>
                                    <OnUpdated>
                                        <Sequence>
                                            <Parallel duration="0" Fps="30">
                                                <FadeIn AnimationTarget="bgPassager" minimumOpacity=".2" />
                                            </Parallel>
                                            <Parallel duration="0">
                                            </Parallel>                            
                                        </Sequence>
                                    </OnUpdated>
                                </Animations>
                        </cc1:UpdatePanelAnimationExtender>
                        </div>
                        
                        <div class="imageVoiture">
                            <table width="100%">
                                <tr>
                                    
                                    <td class="titreTab">
                                        Siège
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                       
                                        <div id="siegeVoiture" runat="server">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="clear">
		                </div>
                            <div runat="server" id="divValidBtn">
                        <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                            onclick="btnValider_Click"  ValidationGroup="groupePassager" SkinID="btnValidation"/>
                                        
                                        <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" SkinID="btnValidation"
                                            onclick="btnAnnuler_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hfIdVoyage" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                            <div runat="server" id="divModifBtn" visible="false">
                        <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                onclick="btnModifier_Click" ValidationGroup="groupePModif" SkinID="btnValidation"/>
                                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                            TargetControlID="btnModifier"
                                            ConfirmText="Voulez vous vraiment modifier?" >
                                            </cc1:ConfirmButtonExtender>
                                            <asp:Button ID="btnAnnulerModif" runat="server" Text="Annuler" SkinID="btnValidation"
                                                onclick="btnAnnulerModif_Click" />
                                        </td>
                                    </tr>
                                </table>
                                </div>
                    </div>

                    <div class="divListe">
                        <asp:DropDownList ID="ddlTriRecherche" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlTriRecherche_SelectedIndexChanged">
                            <asp:ListItem Text="N°Siège" Value="numPlace"></asp:ListItem>
                            <asp:ListItem Text="Nom" Value="nomClient"></asp:ListItem>
                            <asp:ListItem Text="Prénom" Value="prenomClient"></asp:ListItem>
                            <asp:ListItem Text="Pièce d'identité" Value="pieceIdentite"></asp:ListItem>
                            <asp:ListItem Text="Destination" Value="destination"></asp:ListItem>
                            <asp:ListItem Text="Bagage" Value="poidBagage"></asp:ListItem>
                            <asp:ListItem Text="Prix du billet" Value="prixBillet"></asp:ListItem>
                            <asp:ListItem Text="N°Billet" Value="billet.numBillet"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextRecherche" runat="server"></asp:TextBox>
                        <asp:Button ID="btnRecherche" runat="server" Text="Rechercher" 
                            onclick="btnRecherche_Click" />
                        <asp:GridView ID="gvPassager" runat="server" AutoGenerateColumns="False" 
                            EnableModelValidation="True" AllowPaging="True" 
                            onpageindexchanging="gvPassager_PageIndexChanging" 
                            onrowcommand="gvPassager_RowCommand" PageSize="5">
                            <Columns>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                            CommandArgument='<%# Eval("idVoyage") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                            CommandArgument='<%# Eval("idVoyage") %>' />
                                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                            TargetControlID="ibDelete"
                                            ConfirmText='<%# "Vouler vous vraiment supprimer le passager " + Eval("client")%>' >
                                            </cc1:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="client" HeaderText="Nom et Prénom" />
                                <asp:BoundField DataField="pieceIdentite" HeaderText="Piéce d'identité" />
                                <asp:BoundField DataField="destination" HeaderText="Destination" />
                                <asp:BoundField DataField="numPlace" HeaderText="N°Siège" />
                                <asp:BoundField DataField="poidBagage" HeaderText="Bagage" />
                                <asp:BoundField DataField="prixTrajet" HeaderText="Prix du billet" />
                                <asp:BoundField DataField="numBillet" HeaderText="N°Billet" />
                                
                            </Columns>
                        </asp:GridView>
                        
                        
                    </div>
                </div>

                <br />

                <div class="formContent">
                    <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Commission
                    </div>
                    
                    <div class="divListe">
                        <table width="100%">
                            <tr>
                                <td class="titreTab">
                                    Liste des commissions abord
                                </td>
                            </tr>
                        </table>
                        <asp:DropDownList ID="ddlTriCom" runat="server" 
                            onselectedindexchanged="ddlTriCom_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem Text="N°Commission" Value="commission.idCommission"></asp:ListItem>
                            <asp:ListItem Text="Type" Value="typeCommission"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextRechercheCom" runat="server"></asp:TextBox>
                        <asp:Button ID="btnRechercheCom" runat="server" Text="Rechercher" 
                            onclick="btnRechercheCom_Click" />
                        <asp:GridView ID="gvCom" runat="server" AutoGenerateColumns="False" 
                            EnableModelValidation="True" AllowPaging="True" 
                            onpageindexchanging="gvCom_PageIndexChanging" onrowcommand="gvCom_RowCommand" 
                            PageSize="5">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="select"
                                            CommandArgument='<%# Eval("id") %>' />
                                        <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                        TargetControlID="ibSelect"
                                        ConfirmText='<%# "Vouler vous vraiment supprimer le commission N°" + Eval("id")%>' >
                                        </cc1:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="N°Commission" />
                                <asp:BoundField DataField="type" HeaderText="Type" />
                                <asp:BoundField DataField="poids" HeaderText="Poids" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="client" HeaderText="Expéditeur" />
                                <asp:BoundField DataField="recepteur" HeaderText="Réceptionnaire" />
                                <asp:BoundField DataField="frais" HeaderText="Frais" />
                                <asp:BoundField DataField="destination" HeaderText="Destination" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="formulaire">
                        <table width="100%">
                            <tr>
                                <td class="titreTab">
                                    Liste des commissions disponibles
                                </td>
                            </tr>
                        </table>
                        <asp:DropDownList ID="ddlTriRechercheCommission" runat="server" 
                            onselectedindexchanged="ddlTriRechercheCommission_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem Text="N°Commission" Value="commission.idCommission"></asp:ListItem>
                            <asp:ListItem Text="Type" Value="commission.typeCommission"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextRechercheCommission" runat="server"></asp:TextBox>
                        <asp:Button ID="btnRechercheCommission" runat="server" Text="Rechercher" 
                            onclick="btnRechercheCommission_Click" />
                        <asp:GridView ID="gvCommission" runat="server" AllowPaging="True" PageSize="5" 
                            AutoGenerateColumns="False" EnableModelValidation="True" 
                            onpageindexchanging="gvCommission_PageIndexChanging" 
                            onrowcommand="gvCommission_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                            CommandArgument='<%# Eval("id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="N°Commission" />
                                <asp:BoundField DataField="type" HeaderText="Type" />
                                <asp:BoundField DataField="poids" HeaderText="Poids" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="client" HeaderText="Expéditeur" />
                                <asp:BoundField DataField="recepteur" HeaderText="Réceptionnaire" />
                                <asp:BoundField DataField="frais" HeaderText="Frais" />
                                <asp:BoundField DataField="destination" HeaderText="Destination" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            
            
            <div class="clear">
            </div>
            

            
            
           
        </div>
    </div>
</asp:Content>
