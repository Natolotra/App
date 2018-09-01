<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.Master" AutoEventWireup="true" CodeBehind="sessionCaisseVue.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.sessionCaisseVue" %>
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
                                            <asp:TextBox ID="TextFondCaisse" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
                                            
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
                                        <div class="titreLG">&nbsp;&nbsp;Billet
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                        
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalBillet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDetailBillet" runat="server" Text="Détail" 
                                                            onclick="btnDetailBillet_Click" />
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalLettreBillet" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td></td>
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
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDetailCommission" runat="server" Text="Détail" 
                                                            onclick="btnDetailCommission_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalCommissionLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td></td>
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
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDetailtADT" runat="server" Text="Détail" 
                                                            onclick="btnDetailtADT_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalDureeAbonnementLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td></td>
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
                                                    <td>&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnDetailANV" runat="server" Text="Détail" 
                                                            onclick="btnDetailANV_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalVoyageAbonnementLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
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
                                            <table>
                                                <tr>
                                                    <td class="titreTab" colspan="3">
                                                        Montant reçu en espèce
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalRecuEncaisser" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDetailRecuEspece" runat="server" Text="Détail" 
                                                            onclick="btnDetailRecuEspece_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalRecuEncaisserLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="titreTab" colspan="3">
                                                        Montant reçu en chèque
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Montant:
                                                        <asp:Label ID="LabelMontantTotalRecuEnCaisserCheque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDetailRecuCheque" runat="server" Text="Détail" 
                                                            onclick="btnDetailRecuCheque_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalRecuEnCaisserChequeLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td></td>
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
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDetailRecuAD" runat="server" Text="Détail" 
                                                            onclick="btnDetailRecuAD_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelMontantTotalRecuDecaisserLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td></td>
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


            <asp:UpdatePanel ID="UpdatePanel_ListeSession" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="Panel_BilletListe" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Billet
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvBillet" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvBillet_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="numBillet" HeaderText="N°" />
                                            <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                            <asp:BoundField DataField="prixBillet" HeaderText="Montant" />
                                            <asp:BoundField DataField="dateValidite" HeaderText="Valide au" DataFormatString="{0:dd MMMM yyyy}"/>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermerBillet" runat="server" Text="Fermer" 
                                        onclick="btnFermerBillet_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel_CommissionListe" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Commission
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvCommission" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvCommission_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idCommission" HeaderText="N°" />
                                            <asp:BoundField DataField="dateCommission" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="typeCommission" HeaderText="Type" />
                                            <asp:BoundField DataField="destination" HeaderText="Destination" />
                                            <asp:BoundField DataField="fraisEnvoi" HeaderText="Frais" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermerCommssion" runat="server" Text="Fermer" 
                                        onclick="btnFermerCommssion_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel_AbonnementDureeTemps" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Abonnement par durée de temps
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvADT" runat="server" AutoGenerateColumns="False" 
                                        EnableModelValidation="True" onpageindexchanging="gvADT_PageIndexChanging" 
                                        AllowPaging="True">
                                        <Columns>
                                            <asp:BoundField DataField="numDureeAbonnement" HeaderText="N°" />
                                            <asp:BoundField DataField="zone" HeaderText="Zone" />
                                            <asp:BoundField DataField="valideDu" HeaderText="Valide du" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="valideAu" HeaderText="Au" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="prixTotal" HeaderText="Montant" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermerADT" runat="server" Text="Fermer" 
                                        onclick="btnFermerADT_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel_ANVListe" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Abonnement par nombre de voyage
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvANV" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvANV_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="numVoyageAbonnement" HeaderText="N°" />
                                            <asp:BoundField DataField="zone" HeaderText="Zone" />
                                            <asp:BoundField DataField="dateVoyageAbonnement" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="montant" HeaderText="Motant" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermerANV" runat="server" Text="Fermer" 
                                        onclick="btnFermerANV_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel_RecuEspece" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Reçu
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvRecuEspece" runat="server" AutoGenerateColumns="False" 
                                        EnableModelValidation="True" AllowPaging="True" 
                                        onpageindexchanging="gvRecuEspece_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="numRecuEncaisser" HeaderText="N°" />
                                            <asp:BoundField DataField="modePaiement" HeaderText="Mode de paiement" />
                                            <asp:BoundField DataField="dateRecuEncaisser" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="montantRecuEncaisser" HeaderText="Montant" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermerRecuEspece" runat="server" Text="Fermer" 
                                        onclick="btnFermerRecuEspece_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel_RecuCheque" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Reçu
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvRecuCheque" runat="server" AutoGenerateColumns="False" 
                                        EnableModelValidation="True" AllowPaging="True" 
                                        onpageindexchanging="gvRecuCheque_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="numRecuEncaisser" HeaderText="N°" />
                                            <asp:BoundField DataField="modePaiement" HeaderText="Mode de paiement" />
                                            <asp:BoundField DataField="dateRecuEncaisser" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="montantRecuEncaisser" HeaderText="Montant" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermeRecuCheque" runat="server" Text="Fermer" 
                                        onclick="btnFermeRecuCheque_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel_RecuDecaisser" runat="server" CssClass="" Visible="false" Width="50%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Reçu
                                </div>
                                <div class="divListe">
                                    <asp:GridView ID="gvRecuAD" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvRecuAD_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="numRecuAD" HeaderText="N°" />
                                            <asp:BoundField DataField="libele" HeaderText="Libelle" />
                                            <asp:BoundField DataField="dateRecu" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="montant" HeaderText="Montant" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnFermerRecuAD" runat="server" Text="Fermer" 
                                        onclick="btnFermerRecuAD_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
