<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.Master" AutoEventWireup="true" CodeBehind="Prelevement.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.Prelevement" %>
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
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Information
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_Information" runat="server">
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
                                                    Facture
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
                            
                        </div>
                    </div>
                </div>
                <div id="OneLayoteRight60">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Prélèvement
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>Type:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextType" runat="server" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextType_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextType" ValidationGroup="gPrelevement">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Libellé:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLibelle" runat="server">
                                                </asp:DropDownList>
                                        
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlLibelle_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlLibelle" ValidationGroup="gPrelevement">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMontant" runat="server" ReadOnly="true"></asp:TextBox>
                                                <cc1:filteredtextboxextender
                                                ID="TextMontant_FilteredTextBoxExtender"
                                                runat="server" 
                                                TargetControlID="TextMontant"
                                                FilterType="Custom, Numbers"
                                                ValidChars="," />
                                            </td>
                                            <td colspan="4">
                                                <asp:RequiredFieldValidator ID="TextMontant_RequiredFieldValidator" runat="server" ErrorMessage="" 
                                                ControlToValidate="TextMontant" ValidationGroup="gPrelevement">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <div id="divIndication" runat="server">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:HiddenField ID="hfPrelevement" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            <table>
                                <tr>
                                    <td>
                                        
                                        <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" 
                                            onclick="btnAnnuler_Click"  SkinID="btnValidation"/>
                                        <asp:Button ID="btnValideAvance" runat="server" Text="Payer" 
                                            onclick="btnValideAvance_Click" SkinID="btnValidation" ValidationGroup="gPrelevement"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel_ListeRecu" runat="server">
                            <ContentTemplate>
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
                <asp:UpdatePanel ID="UpdatePanel_ListeAD" runat="server">
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
