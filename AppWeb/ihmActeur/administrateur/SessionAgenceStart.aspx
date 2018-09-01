<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SessionAgenceStart.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.SessionAgenceStart" %>
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
                
                        &nbsp;&nbsp;Session agence&nbsp;
                        
            </div>
            
            <div id="OneLayoteLeft50"> 
                <asp:UpdatePanel ID="UpdatePanel_Left" runat="server">
                    <ContentTemplate>
                        <div class="formContent">
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;<asp:Label ID="LabelSessionStatu" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="formulaire">
                                <table>
                                    <tr>
                                        <td>Agence:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNomGare" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Type:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelType" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sigle:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelSigle" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Localisation:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelLocalisation" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ville:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelVille" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:HiddenField ID="hfNumAgence" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Statut:
                                        </td>
                                        <td>
                                            <asp:Image ID="imageStatut" runat="server" ImageUrl=""/>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnOuvrirSession" runat="server" Text="Démarrer une session" 
                                                SkinID="btnValidation" onclick="btnOuvrirSession_Click"/>
                                            <asp:Button ID="btnFermerSession" runat="server" Text="Fermer la session" 
                                                SkinID="btnValidation" onclick="btnFermerSession_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <asp:Panel ID="Panel_MontantTotalSessionCaisse" runat="server">
                            <br />
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Montant total de la session du 
                                        <asp:Label ID="LabelDateDebutSession" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="formulaire">
                                    <table width="100%">
                                        <tr>
                                            <td class="titreTab" colspan="2">
                                                Montant total de la session en espèce
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMontantTotalSession" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total en lettre:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMontantTotalSessionLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titreTab" colspan="2">
                                                Montant total de la session en chèque
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMontantTotalSessionCheque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total en lettre:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMontantTotalSessionChequelettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                                    
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="OneLayoteRight50">
                <asp:UpdatePanel ID="UpdatePanel_Right" runat="server">
                    <ContentTemplate>
                        <div class="formContent">
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;Détail
                            </div>
                            
                            <div class="formulaire">
                                <asp:Panel ID="Panel_BilletMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">&nbsp;&nbsp;Billet
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                        
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalBillet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalLettreBillet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                
                                <asp:Panel ID="Panel_CommissionMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">&nbsp;&nbsp;Commission
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalCommission" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalCommissionLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                        
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                
                                <asp:Panel ID="Panel_DureeAbonnementMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Abonnement par durée de temps
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                        
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMotantTotalDureeAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalDureeAbonnementLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>

                                <asp:Panel ID="Panel_VoyageAbonnementMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Abonnement par nombre de voyage
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                        
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalVoyageAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalVoyageAbonnementLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>

                                <asp:Panel ID="Panel_RecuEncaisserMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Reçu
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                                <tr>
                                                    <td class="titreTab" colspan="2">
                                                        Montant reçu en espèce
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuEncaisser" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuEncaisserLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="titreTab" colspan="2">
                                                        Montant reçu en chèque
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuEnCaisserCheque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuEnCaisserChequeLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>

                                
                            </div>
                        </div>
                        <br />
                        <div class="formContent">
                             
                            <div class="collapsePanelHeader">
                            </div>
                            
                            <div class="formulaire">
                                <asp:Panel ID="Panel_RecuADTotal" runat="server">
                                   
                                    <div class="formContent2">
                                        <div class="titreLG">&nbsp;&nbsp;Motant décaissée
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                        
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuDecaisser" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuDecaisserLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                            </div>
                       
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="clear">
                &nbsp;&nbsp;
            </div>

            <div class="formContent">
                <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Agence
                </div>
                
                <div class="divListe">
                    <asp:UpdatePanel ID="UpdatePanel_ListeAgence" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTriAgence" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriAgence_SelectedIndexChanged">
                                <asp:ListItem Text="" Value="agence.numAgence"></asp:ListItem>
                                <asp:ListItem Text="Type" Value="agence.typeAgence"></asp:ListItem>
                                <asp:ListItem Text="Nom" Value="agence.nomAgence"></asp:ListItem>
                                <asp:ListItem Text="Localisation" Value="agence.localisationAgence"></asp:ListItem>
                                <asp:ListItem Text="Sigle" Value="agence.sigleAgence"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheAgence" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheAgence" runat="server" Text="Rechercher" 
                                onclick="btnRechercheAgence_Click" />
                            <asp:GridView ID="gvAgence" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                onpageindexchanging="gvAgence_PageIndexChanging" 
                                onrowcommand="gvAgence_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("numAgence") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nomAgence" HeaderText="Nom agence" />
                                    <asp:BoundField DataField="typeAgence" HeaderText="Type" />
                                    <asp:BoundField DataField="sigleAgence" HeaderText="Sigle" />
                                    <asp:BoundField DataField="villeAgence" HeaderText="Ville" />
                                    <asp:BoundField DataField="localisation" HeaderText="Localisation" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibStatut" runat="server" ImageUrl='<%# "~/CssStyle/images/" + Eval("statut") %>' CommandName="statut"
                                                CommandArgument='<%# Eval("statut") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
