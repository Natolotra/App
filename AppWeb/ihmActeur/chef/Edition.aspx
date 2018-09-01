<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/chef/MasterChef.Master" AutoEventWireup="true" CodeBehind="Edition.aspx.cs" Inherits="AppWeb.ihmActeur.chef.Edition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="OneLayote">
        <div class="formContent3">
            <div class="grandTitre">
                &nbsp;&nbsp;Autorisation de départ
            </div>

            <div class="contentFormulaires">
                <div id="OneLayoteLeft">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Information
                        </div>
                        <div class="formulaire">
                        <asp:HiddenField ID="hfAutorisationDepart" runat="server" />
                        <asp:HiddenField ID="hfNumerosFB" runat="server" />
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
                                    Numeros:
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
                                <td>Numeros:
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
                        </table>

                    </div>
                </div>
                </div>

                <div id="OneLayoteRight">
                    
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Liste des passagers
                        </div>
                        <div class="divListe">
                             <asp:DropDownList ID="ddlTriListePassager" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriListePassager_SelectedIndexChanged">
                                <asp:ListItem Text="N°Siège" Value="numPlace"></asp:ListItem>
                                <asp:ListItem Text="Nom" Value="nomClient"></asp:ListItem>
                                <asp:ListItem Text="Prénom" Value="prenomClient"></asp:ListItem>
                                <asp:ListItem Text="Pièce d'identité" Value="pieceIdentite"></asp:ListItem>
                                <asp:ListItem Text="Destination" Value="destination"></asp:ListItem>
                                <asp:ListItem Text="N°Billet" Value="billet.numBillet"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheListePassager" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRecherchePassager" runat="server" Text="Rechercher" 
                                onclick="btnRecherchePassager_Click" />
                            <asp:GridView ID="gvPassager" runat="server" AutoGenerateColumns="False" 
                                EnableModelValidation="True" AllowPaging="True" 
                                 onpageindexchanging="gvPassager_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="passager" HeaderText="Nom et Prénom" />
                                    <asp:BoundField DataField="pieceIdentite" HeaderText="Pièce d'identité" />
                                    <asp:BoundField DataField="destination" HeaderText="Destination" />
                                    <asp:BoundField DataField="numPlace" HeaderText="N°Siège" />
                                    <asp:BoundField DataField="poidBagage" HeaderText="Bagage" />
                                    <asp:BoundField DataField="prixTrajet" HeaderText="Prix du billet" />
                                    <asp:BoundField DataField="numBillet" HeaderText="N°Billet" />
                                    <asp:BoundField DataField="excedent" HeaderText="Excédent" />
                                    <asp:BoundField DataField="somme" HeaderText="Somme reçu" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="formulaire">
                            <table>
                                <tr>
                                    <td>Nombre des passagers:
                                    </td>
                                    <td>
                                        <asp:Label ID="labNbPassager" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>Poids total bagages:
                                    </td>
                                    <td>
                                        <asp:Label ID="labPoidTotalBagage" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>Total somme reçu:
                                    </td>
                                    <td>
                                        <asp:Label ID="labSommeRecu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Commission
                        </div>
                        <div class="divListe">
                            <asp:DropDownList ID="ddlTriCommission" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriCommission_SelectedIndexChanged">
                                <asp:ListItem Text="Type" Value="commission.typeCommission"></asp:ListItem>
                                <asp:ListItem Text="Désignation" Value="designation"></asp:ListItem>
                                <asp:ListItem Text="Poids" Value="poids"></asp:ListItem>
                                <asp:ListItem Text="Nom expéditeur" Value="nomClient"></asp:ListItem>
                                <asp:ListItem Text="Prénom expéditeur" Value="prenomClient"></asp:ListItem>
                                <asp:ListItem Text="Nom réceptionnaire" Value="nomPersonne"></asp:ListItem>
                                <asp:ListItem Text="Prénom réceptionnaire" Value="prenomPersonne"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheCommission" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheCommission" runat="server" Text="Rechercher" 
                                onclick="btnRechercheCommission_Click" />
                            <asp:GridView ID="gvCommission" runat="server" AutoGenerateColumns="False" 
                                EnableModelValidation="True" AllowPaging="True" 
                                onpageindexchanging="gvCommission_PageIndexChanging" PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="type" HeaderText="Type" />
                                    <asp:BoundField DataField="designation" HeaderText="Désignation" />
                                    <asp:BoundField DataField="poids" HeaderText="Poids" />
                                    <asp:BoundField DataField="expediteur" HeaderText="Nom de l'expéditeur" />
                                    <asp:BoundField DataField="recepteur" HeaderText="Nom de réceptionnaire" />
                                    <asp:BoundField DataField="piece" HeaderText="Pièce justificatif" />
                                    <asp:BoundField DataField="frais" HeaderText="Frais d'envoi" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="formulaire">
                            <table>
                                <tr>
                                    <td>Poids total commissions:
                                    </td>
                                    <td>
                                        <asp:Label ID="labPoidTotalCommission" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>Total frais d'envoi:
                                    </td>
                                    <td>
                                        <asp:Label ID="labTotalFraisCommission" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="formContent">
                        <div class="collapsePanelHeader">&nbsp;&nbsp;Recette</div>
                        <div class="formulaire">
                            <table>
                                <tr>
                                    <td>Montant:
                                    </td>
                                    <td>
                                        <asp:Label ID="labMotant" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>Reste:
                                    </td>
                                    <td>
                                        <asp:Label ID="labReste" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                                EnableModelValidation="True" AllowPaging="True" 
                                onpageindexchanging="gvPrelevement_PageIndexChanging" PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="numPrelevement" HeaderText="N°" />
                                    <asp:BoundField DataField="montantPrelevement" HeaderText="Montant" />
                                    <asp:BoundField DataField="commentaire" HeaderText="Type" />
                                    <asp:BoundField DataField="datePrelevement" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                </Columns>
                            </asp:GridView>
                            <table>
                                <tr>
                                    <td>Total montant prélèvement:
                                    </td>
                                    <td>
                                        <asp:Label ID="LabTotalPrelevement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="divListe">
                            <table width="100%">
                                <tr>
                                    <td class="titreTab">Reçu</td>
                                </tr>
                            </table><br />
                            <asp:GridView ID="gvRecuAD" runat="server" AutoGenerateColumns="False" 
                                EnableModelValidation="True" AllowPaging="True" 
                                onpageindexchanging="gvRecuAD_PageIndexChanging" PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="numRecuAD" HeaderText="N°" />
                                    <asp:BoundField DataField="typeRecuAD" HeaderText="Type" />
                                    <asp:BoundField DataField="libele" HeaderText="Libellé" />
                                    <asp:BoundField DataField="montant" HeaderText="Montant" />
                                    <asp:BoundField DataField="dateRecu" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                </Columns>
                            </asp:GridView>
                            <table>
                                <tr>
                                    <td>Total montant reçu:
                                    </td>
                                    <td>
                                        <asp:Label ID="LabTotalRecu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                    </div>
                </div>

                <div class="clear">
                </div>
                <br />
            </div>
            
            <div class="formulaire">
                <asp:Button ID="btnEditerFicheBord" runat="server" Text="Editer fiche de bord" 
                    onclick="btnEditerFicheBord_Click" SkinID="btnValidation"/>
                
            </div>
            <br />
            <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Autorisation de départ
                    </div>
            <div class="divListe">
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
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibSelectPdf" runat="server" ImageUrl="~/CssStyle/images/pdf.png" CommandName="selectPdf"
                                    CommandArgument='<%# Eval("numAutorisationDepart") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </div>
        </div>
    </div>
</asp:Content>
