<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/chef/MasterChef.Master" AutoEventWireup="true" CodeBehind="SessionStart.aspx.cs" Inherits="AppWeb.ihmActeur.chef.SessionStart" %>
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
                <asp:UpdatePanel ID="UpdatePanel_Titre" runat="server">
                    <ContentTemplate>
                        &nbsp;&nbsp;Session&nbsp;
                        <asp:HiddenField ID="hfMatriculeAgent" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <div id="OneLayoteLeft50">  
                <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                    <ContentTemplate> 
                        <div class="formContent">
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;<asp:Label ID="LabelSessionStatu" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="formulaire">
                        
                                <table>
                                    <tr>
                                        <td rowspan="6">
                                            <asp:ImageButton ID="ImageAgent" runat="server" AlternateText=" " />
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelMatriculeAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTypeAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNomPrenomAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCINAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAdresseAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelContactAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                </table>
                                
                                <table>
                                    <tr>
                                        <td>Fond de caisse:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextFondCaisse" runat="server" Text="0"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextFondCaisse_FilteredTextBoxExtender"
                                                    runat="server"
                                                    TargetControlID="TextFondCaisse"
                                                    FilterType="Numbers" />
                                        </td>
                                        <td>
                                            Ar
                                            <asp:RequiredFieldValidator ID="TextFondCaisse_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextFondCaisse" ValidationGroup="gSessionCaisse">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Statut:
                                        </td>
                                        <td>
                                            <asp:Image ID="imageStatut" runat="server" ImageUrl=""/>
                                        </td>
                                    </tr>
                                </table>
                                
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSessionValider" runat="server" Text="Démarrer une session" 
                                                onclick="btnSessionValider_Click" SkinID="btnValidation" ValidationGroup="gSessionCaisse"/>
                                            <asp:Button ID="btnFermerSession" runat="server" Text="Fermer la session" 
                                                onclick="btnFermerSession_Click" SkinID="btnValidation"/>
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
                <br />
                
            </div>
            
            <div id="OneLayoteRight50">
                <div class="formContent">
                    <asp:UpdatePanel ID="UpdatePanel_MontantTotalSession" runat="server">
                        <ContentTemplate>
                    
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;Détail
                            </div>
                            
                            <div class="formulaire">
                                <asp:Panel ID="Panel_BilletMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">&nbsp;&nbsp;Billet R/N
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
                                            &nbsp;&nbsp;Abonnement par nombre de voyage R/N
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

                                <asp:Panel ID="Panel_BilletUS" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Billet U/S
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                        
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantBilletUS" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantBilletUSLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>

                                <asp:Panel ID="Panel_AbonnementNbVoyageUS" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Abonnement par nombre de voyage U/S
                                        </div>
                                        <div class="formulaire">
                                            <table width="100%">
                                        
                                                <tr>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantAbonnementNBVoyageUS" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant en lettre:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMontantAbonnementNBVoyageUSLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                                
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                <br />
                <div class="formContent">
                    <asp:UpdatePanel ID="UpdatePanel_MontantTotalDeCaisser" runat="server">
                        <ContentTemplate>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br />
            </div>
            
            <div class="clear">
            </div>
            
            <div class="formContent">
                <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Agent
                </div>
                
                <div class="divListe">
                    <asp:UpdatePanel ID="UpdatePanel_ListAgent" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTriAgent" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriAgent_SelectedIndexChanged">
                                <asp:ListItem Text="Matricule" Value="matriculeAgent"></asp:ListItem>
                                <asp:ListItem Text="Prénom" Value="prenomAgent"></asp:ListItem>
                                <asp:ListItem Text="Nom" Value="nomAgent"></asp:ListItem>
                                <asp:ListItem Text="Adresse" Value="adresseAgent"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheAgent" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheAgent" runat="server" Text="Recherche" 
                                onclick="btnRechercheAgent_Click" />
                            <asp:GridView ID="gvAgent" runat="server" AutoGenerateColumns="False" 
                                AllowPaging="True" onpageindexchanging="gvAgent_PageIndexChanging" 
                                onrowcommand="gvAgent_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("matriculeAgent") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="matriculeAgent" HeaderText="Matricule" />
                                    <asp:BoundField DataField="agent" HeaderText="Agent" />
                                    <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                    <asp:BoundField DataField="contact" HeaderText="Contact" />
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
