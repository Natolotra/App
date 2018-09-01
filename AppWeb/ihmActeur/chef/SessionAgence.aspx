<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/chef/MasterChef.Master" AutoEventWireup="true" CodeBehind="SessionAgence.aspx.cs" Inherits="AppWeb.ihmActeur.chef.SessionAgence" %>
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
                        &nbsp;&nbsp;MOUVEMENT
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <div id="OneLayoteLeft50">  
                <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                    <ContentTemplate>
                        <div class="formContent">
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;<asp:Label ID="LabelAgence" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="formulaire">
                                <table>
                                    <tr>
                                        <td>Statut:
                                        </td>
                                        <td>
                                            <asp:Image ID="imageStatut" runat="server" ImageUrl=""/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <asp:Panel ID="Panel_MontantTotalSessionAgence" runat="server">
                            <br />
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Montant total de la session du 
                                        <asp:Label ID="LabelDateDebutSession" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="formulaire">
                                    <table>
                                        <tr>
                                            <td class="titreTab">
                                                Montant total de la session en espèce
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total:
                                                <asp:Label ID="LabelMontantTotalSession" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelMontantTotalSessionLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titreTab">
                                                Montant total de la session en chèque
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Montant total:
                                                <asp:Label ID="LabelMontantTotalSessionCheque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
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
                <div class="formContent">
                    <asp:UpdatePanel ID="UpdatePanel_MontantTotalSession" runat="server">
                        <ContentTemplate>
                    
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;Détail
                            </div>
                            
                            <div class="formulaire">
                                <asp:Panel ID="Panel_BilletMontantTotal" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">&nbsp;&nbsp;Billet
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                        
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalBillet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
                                            <table>
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalCommission" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
                                            <table>
                                        
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMotantTotalDureeAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
                                            <table>
                                        
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalVoyageAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
                                            <table>
                                                <tr>
                                                    <td class="titreTab">
                                                        Montant reçu en espèce
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalRecuEncaisser" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelMontantTotalRecuEncaisserLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="titreTab">
                                                        Montant reçu en chèque
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalRecuEnCaisserCheque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
                                            <table>
                                        
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalRecuDecaisser" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
        </div>
    </div>
</asp:Content>
