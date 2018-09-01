<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/chef/MasterChef.Master" AutoEventWireup="true" CodeBehind="FacturationProprietaire.aspx.cs" Inherits="AppWeb.ihmActeur.chef.FacturationProprietaire" %>
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
                &nbsp;&nbsp;Facturation
            </div>

            <div class="contentFormulaires">
                <div id="OneLayoteLeft">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Information
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_InormationVehicule" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td class="titreTab" colspan="2">
                                                Véhicule
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Matrcicule:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMatricule" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Marque:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMarque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Couleur:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelCouleur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:Panel ID="Panel_Individu" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td class="titreTab" colspan="2">
                                                    Propriétaire
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPrenomIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelCINIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:Panel ID="Panel_Societe" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td class="titreTab" colspan="2">
                                                    Société
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Société:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="titreTab">
                                                    Responsable société
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPrenomRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:Panel ID="Panel_Organisme" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td class="titreTab" colspan="2">
                                                    Organisme
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Organisme:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="titreTab">
                                                    Responsable organisme
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPrenomRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:HiddenField ID="hfNumVehicule" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>
                    <br />
                </div>

                <div id="OneLayoteRight">
                    <asp:UpdatePanel ID="UpdatePanel_DetailFacture" runat="server">
                        <ContentTemplate>
                            <div class="formContent">
                         
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Liste voyage
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriVoyage" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriVoyage_SelectedIndexChanged">
                                        <asp:ListItem Text="Matricule vehicule" Value="matriculeVehicule"></asp:ListItem>
                                        <asp:ListItem Text="Nom chauffeur" Value="nomChauffeur"></asp:ListItem>
                                        <asp:ListItem Text="Prénom chauffeur" Value="prenomChauffeur"></asp:ListItem>
                                        <asp:ListItem Text="Date" Value="dateHeurDepart"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheVoyage" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercheVoyage" runat="server" Text="Rechercher" 
                                        onclick="btnRechercheVoyage_Click" />
                                    <asp:GridView ID="gvListeVoyage" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvListeVoyage_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="vehicule" HeaderText="Vehicule" />
                                            <asp:BoundField DataField="chauffeur" HeaderText="Chauffeur" />
                                            <asp:BoundField DataField="date" HeaderText="Date" 
                                                DataFormatString="{0:dd MMMM yyyy HH:mm}" />
                                            <asp:BoundField DataField="itineraire" HeaderText="Itineraire" />
                                            <asp:BoundField DataField="recette" HeaderText="Recette" />
                                            <asp:BoundField DataField="reste" HeaderText="Reste" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="formulaire">
                                    Total recette:<asp:Label ID="LabTotalRecettes" runat="server" Text="" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                    Total reste:<asp:Label ID="LabTotalReste" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Liste Reçue
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriRecu" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriRecu_SelectedIndexChanged">
                                        <asp:ListItem Text="N°" Value="recuad.numRecuAD"></asp:ListItem>
                                        <asp:ListItem Text="Montant" Value="montant"></asp:ListItem>
                                        <asp:ListItem Text="Libellé" Value="libele"></asp:ListItem>
                                        <asp:ListItem Text="Type" Value="typeRecuAD"></asp:ListItem>
                                        <asp:ListItem Text="Date" Value="dateRecu"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheRecu" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercheRecu" runat="server" Text="Rechercher" 
                                        onclick="btnRechercheRecu_Click" />
                                    <asp:GridView ID="gvRecu" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvRecu_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="numR" HeaderText="N°" />
                                            <asp:BoundField DataField="montant" HeaderText="Montant" />
                                            <asp:BoundField DataField="libele" HeaderText="Libellé" />
                                            <asp:BoundField DataField="typeRecuAD" HeaderText="Type" />
                                            <asp:BoundField DataField="dateRecu" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="formulaire">
                                    Total montant reçu:<asp:Label ID="LabTotalMontantRecu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Facture
                                </div>
                                <div class="formulaire">
                                    <table>
                                        <tr>
                                            <td>Montant total:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabMontantTotalFacture" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total en lettre:
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="LabMontantTotalFactutreLettre" runat="server" Text="" Font-Bold="true" Font-Italic="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>

                <div class="clear"></div>
            </div>
            <div class="formulaire">
                <asp:Button ID="btnFacturer" runat="server" Text="Facturer" 
                    onclick="btnFacturer_Click" SkinID="btnValidation" />
            </div>
            <br />
            <div class="formContent">
                <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Proprietaire
                </div>
                <div class="divListe">
                    
                    <asp:UpdatePanel ID="UpdatePanel_ListeVehicule" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTriVehicule" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriVehicule_SelectedIndexChanged">
                                <asp:ListItem Text="" Value="vehicule.numVehicule"></asp:ListItem>
                                <asp:ListItem Text="Matrcicule" Value="matriculeVehicule"></asp:ListItem>
                                <asp:ListItem Text="Marque" Value="marqueVehicule"></asp:ListItem>
                                <asp:ListItem Text="Couleur" Value="couleurVehicule"></asp:ListItem>
                                <asp:ListItem Text="Nom Individu" Value="nomIndividu"></asp:ListItem>
                                <asp:ListItem Text="Prénom Individu" Value="prenomIndividu"></asp:ListItem>
                                <asp:ListItem Text="Société" Value="nomSociete"></asp:ListItem>
                                <asp:ListItem Text="Nom responsable société" Value="societe.nomResponsable"></asp:ListItem>
                                <asp:ListItem Text="Prénom responsable société" Value="societe.prenomResponsable"></asp:ListItem>
                                <asp:ListItem Text="Organisme" Value="nomOrganisme"></asp:ListItem>
                                <asp:ListItem Text="Nom responsable organisme" Value="organisme.nomResponsable"></asp:ListItem>
                                <asp:ListItem Text="Prénom responsable organisme" Value="organisme.prenomResponsable"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheVehicule" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheVehicule" runat="server" Text="Rechercher" 
                                onclick="btnRechercheVehicule_Click" />
                            <asp:GridView ID="gvVehicule" runat="server" AutoGenerateColumns="False" 
                                EnableModelValidation="True" AllowPaging="True" 
                                onpageindexchanging="gvVehicule_PageIndexChanging" 
                                onrowcommand="gvVehicule_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("numVehicule") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="vehicule" HeaderText="Véhicule" />
                                    <asp:BoundField DataField="Individu" HeaderText="Individu/Soc/Org" />
                                    <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                    <asp:BoundField DataField="contact" HeaderText="Contact" />
                                    <asp:BoundField DataField="respSociete" HeaderText="Responsable Soc/Org" />
                                    <asp:BoundField DataField="respContact" HeaderText="Contact" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        
        
    </div>
</asp:Content>
