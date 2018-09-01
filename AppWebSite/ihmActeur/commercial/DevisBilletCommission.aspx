<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.master" AutoEventWireup="true" CodeBehind="DevisBilletCommission.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.DevisBilletCommission" %>
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
                &nbsp;&nbsp;Billet / Commission
            </div>
            
                
            <div id="OneLayoteLeft50">  
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Billet
                    </div>    
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanelFormulaireBillet" runat="server" 
                            RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Panel ID="Panel_FormulaireBillet" runat="server">
                                    <table width="100%">
                                    
                                        <tr>
                               
                                            <td colspan="3">
                                                <asp:Button ID="btnBilletAuNomDe" runat="server" Text="Client" 
                                                    onclick="btnBilletAuNomDe_Click" />&nbsp;
                                                <asp:Label ID="LabelClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                <asp:HiddenField ID="hfNumClient" runat="server" />
                                            </td>
                                            <td>
                                                N°Abonnement:
                                            </td>
                                            <td>
                                                 <asp:TextBox ID="TextNumAbonnement" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAbonnement" runat="server" Text="Ok" 
                                                    onclick="btnAbonnement_Click" />
                                            </td>
                                        </tr>
                                    
                                        <tr>
                                            <td colspan="6">&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Départ à:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlVilleDepart" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlVilleDepart_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                                <asp:ListSearchExtender ID="ddlVilleDepart_ListSearchExtender" runat="server" 
                                                    Enabled="True" TargetControlID="ddlVilleDepart" PromptText="Recherche">
                                                </asp:ListSearchExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlVilleDepart_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlVilleDepart" ValidationGroup="gBillet">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                    Destination:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDestination" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlDestination_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:ListSearchExtender ID="ddlDestination_ListSearchExtender" runat="server" 
                                                    Enabled="True" PromptText="Recherche" TargetControlID="ddlDestination">
                                                </asp:ListSearchExtender>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlDestination_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDestination" ValidationGroup="gBillet">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    
                                        <tr>
                                            <td>Catégorie:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCalculePrixBillet" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlCalculePrixBillet_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCalculePrixBillet_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCalculePrixBillet" ValidationGroup="gBillet">*</asp:RequiredFieldValidator>
                                            &nbsp;
                                            </td>
                                            <td>Prix:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrixBillet" runat="server" ReadOnly="true" Width="120"></asp:TextBox>
                                                Ar
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextPrixBillet_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextPrixBillet" ValidationGroup="gBillet">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                               
                                            <td>Nombre:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNombreBillet" runat="server"  AutoPostBack="True" 
                                                    ontextchanged="TextNombreBillet_TextChanged"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender
                                                        ID="TextNombreBillet_FilteredTextBoxExtender"
                                                        runat="server"
                                                        TargetControlID="TextNombreBillet"
                                                        FilterType="Numbers" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNombreBillet_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNombreBillet" ValidationGroup="gBillet" >*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Prix total:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrixTotal" runat="server" ReadOnly="true" Width="120"></asp:TextBox>
                                                Ar
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextPrixTotal_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextPrixTotal" ValidationGroup="gBillet">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="3">
                                                <asp:HiddenField ID="hfNumTrajet" runat="server" />
                                            </td>
                                            <td colspan="3">
                                                <asp:HiddenField ID="hfNumBilletCommande" runat="server" />
                                            </td>
                                        </tr>
                            
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="Panel_AbonnementNbVoyage" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                Zone:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelZoneVoyageAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Trajet:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTrajetVoyageAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Catégorie:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelCategorieVoyageAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                                Nombre:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNbBillet" runat="server">1</asp:TextBox>
                                                <asp:FilteredTextBoxExtender
                                                ID="TextNbBillet_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextNbBillet"
                                                FilterType="Numbers" />
                                            </td>
                                            <td >
                                                <asp:RequiredFieldValidator ID="TextNbBillet_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNbBillet">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="6">
                                                <asp:HiddenField ID="hfNumVoyageAbonnement" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_AbonnementDureeTemps" runat="server">
                                    <table>
                                        <tr>
                                            <td>Zone:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelZoneDureeAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                Trajet:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTrajetDureeAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Catégorie:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelCategorieDureeAbonnement" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:HiddenField ID="hfNumDureeAbonnement" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary_Billet" runat="server"  ValidationGroup="gBillet"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                onclick="btnAnnuler_Click" SkinID="btnValidation"/>
                                <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                    ValidationGroup="gBillet" onclick="btnModifier_Click" SkinID="btnValidation"/>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                TargetControlID="btnModifier"
                                ConfirmText="">
                                </asp:ConfirmButtonExtender>
                                <asp:Button ID="btnAjouter" runat="server" Text="   Devis   " 
                                ValidationGroup="gBillet" onclick="btnAjouter_Click" SkinID="btnValidation"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Button ID="btnValideBillet" runat="server" Text="Valider" 
                                onclick="btnValideBillet_Click" ValidationGroup="gBillet" SkinID="btnValidation"/>
                    
                    </div>
                </div>
                <br />       
            </div>
            
            
            <div id="OneLayoteRight50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Commission
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Right" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                        
                                <table width="100%">
                                     <tr>
                                        <td colspan="3">
                                            <asp:Button ID="btnExpediteur" runat="server" Text="Expéditeur" 
                                                onclick="btnExpediteur_Click" />&nbsp;
                                            <asp:Label ID="LabelExpediteur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hfNumExpediteur" runat="server" />
                                        </td>
                                        <td colspan="3">
                                            <asp:Button ID="btnRecepteur" runat="server" Text="Réceptionnaire" 
                                                onclick="btnRecepteur_Click" />&nbsp;
                                            <asp:Label ID="LabelRecepteur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hfNumReceptionnaire" runat="server" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan="6">&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ville de départ:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlVilleDepartCommission" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlVilleDepartCommission_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlVilleDepartCommission_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="ddlVilleDepartCommission" PromptText="Recherche">
                                            </asp:ListSearchExtender>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                                Destination:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDestinationCommission" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlDestinationCommission_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlDestinationCommission_ListSearchExtender" runat="server" 
                                                Enabled="True" PromptText="Recherche" TargetControlID="ddlDestinationCommission">
                                            </asp:ListSearchExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDestinationCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlDestinationCommission" ValidationGroup="groupeCommission">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Type:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeCommission" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTypeCommission_SelectedIndexChanged"  >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Désignation:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDesignation" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDesignation_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nombre:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNombreCommission" runat="server" AutoPostBack="True" 
                                                ontextchanged="TextNombreCommission_TextChanged">
                                            </asp:TextBox>
                                            
                                            <asp:FilteredTextBoxExtender
                                                ID="TextNombreCommission_FilteredTextBoxExtender"
                                                runat="server"
                                                TargetControlID="TextNombreCommission"
                                                FilterType="Numbers" />

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNombreCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNombreCommission" ValidationGroup="groupeCommission">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Poids:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPoidsCommission" runat="server" AutoPostBack="True" 
                                                ontextchanged="TextPoidsCommission_TextChanged"  Width="120">
                                            </asp:TextBox>
                                            Kg
                                            <asp:FilteredTextBoxExtender
                                                ID="TextPoidsCommission_FilteredTextBoxExtender"
                                                runat="server" 
                                                TargetControlID="TextPoidsCommission"
                                                FilterType="Custom, Numbers"
                                                ValidChars="," />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPoidsCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPoidsCommission" ValidationGroup="groupeCommission">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pièce justificative:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPieceJustificatifCommission" runat="server"></asp:TextBox>
                                            
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPieceJustificatifCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPieceJustificatifCommission" ValidationGroup="groupeCommission">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Frais d'envoi:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextFraisCommission" runat="server" ReadOnly="True" Width="120"></asp:TextBox>
                                            Ar
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextFraisCommission_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextFraisCommission"  ValidationGroup="groupeCommission">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    
                            
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div id="div2" runat="server">
                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary_Commission" runat="server" 
                                            ValidationGroup="groupeCommission"/>
                                            <div id="divIndicationCommission" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfIdCommissionDevis" runat="server" />
                                        </td>
                                    </tr>
                                    
                                </table>
                                <asp:Button ID="btnNewCommission" runat="server" Text="Nouveau" 
                                    onclick="btnNewCommission_Click"  SkinID="btnValidation"/>
                                            
                                <asp:Button ID="btnModifierCommissionDevis" runat="server" Text="Modifier" 
                                    onclick="btnModifierCommissionDevis_Click" SkinID="btnValidation"/>
                                <asp:ConfirmButtonExtender ID="btnModifierCommissionDevis_ConfirmButtonExtender" runat="server" 
                                    TargetControlID="btnModifierCommissionDevis"
                                    ConfirmText="">
                                    </asp:ConfirmButtonExtender>
                                <asp:Button ID="btnAjouterCommissionDevis" runat="server" 
                                    Text="   Devis   " onclick="btnAjouterCommissionDevis_Click" 
                                    ValidationGroup="groupeCommission" SkinID="btnValidation"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        <asp:Button ID="btnValiderCommission" runat="server" Text="Valider"  
                                ValidationGroup="groupeCommission" onclick="btnValiderCommission_Click" SkinID="btnValidation"/>
                                
                    </div>
                </div>
                <br />
            </div>
                
            
            <div class="clear"></div>
            
            
            <div class="formContent">
                <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Proforma/Devis
                </div>
                <div class="divListe">
                    <asp:UpdatePanel ID="UpdatePanel_Devis" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                        
                            
                            <table width="100%">
                                <tr>
                                    <td class="titreTab">
                                        Billet
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td class="titreTab">
                                        Commission
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:GridView ID="gvBilletProforma" runat="server" AutoGenerateColumns="False" 
                                                EnableModelValidation="True" AllowPaging="True" 
                                                onpageindexchanging="gvBilletProforma_PageIndexChanging" 
                                                onrowcommand="gvBilletProforma_RowCommand" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                                CommandArgument='<%# Eval("numBilletCommande") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        
                                                    <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                                    <asp:BoundField DataField="categorie" HeaderText="Catégorie"/>
                                                    <asp:BoundField DataField="prix" HeaderText="Montant" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="prixTotal" HeaderText="Prix total"/>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("numBilletCommande") %>' />
                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                                TargetControlID="ibDelete"
                                                                ConfirmText='<%# "Voulez vous vraiment supprimer le billet N° " + Eval("numBilletCommande") + "? \nMontant total: " + Eval("prixTotal")%>' >
                                                            </asp:ConfirmButtonExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                                
                                        </div>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:GridView ID="gvCommissionProforma" runat="server" 
                                            AutoGenerateColumns="False" EnableModelValidation="True" 
                                            AllowPaging="True" onpageindexchanging="gvCommissionProforma_PageIndexChanging" 
                                            onrowcommand="gvCommissionProforma_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                                CommandArgument='<%# Eval("idCommissionDevis") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="destination" HeaderText="Destination" />
                                                <asp:BoundField DataField="prixTotalfraisEnvoi" HeaderText="Frais d'envoi" />
                                                <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                                <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("idCommissionDevis") %>' />
                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                                TargetControlID="ibDelete"
                                                                ConfirmText='<%# "Voulez vous vraiment supprimer le commission N° " + Eval("idCommissionDevis") + "? \nMontant total: " + Eval("prixTotalfraisEnvoi")%>' >
                                                            </asp:ConfirmButtonExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                            
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>Montant total billet:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelPrixTotalProforma" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                         <asp:HiddenField ID="hfNumProforma" runat="server" />
                                                    </td>
                                                </tr>
                                                
                                                
                                            </table>
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>Montant total Commission:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelPrixDevisCommission" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                    
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="3">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        MONTANT TOTAL DE DEVIS:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelTotalDevis" runat="server" SkinID="LabelMontantTotal" 
                                                            ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelTotalDevisLettre" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                                
                            
                                        
                            <asp:Button ID="btnNouvelleCommande" runat="server" Text="Nouveau" 
                                onclick="btnNouvelleCommande_Click" SkinID="btnValidation"/>
                            <asp:Button ID="btnAnnulerProforma" runat="server" Text="Annuler" 
                                onclick="btnAnnulerProforma_Click" SkinID="btnValidation"/>
                            <asp:ConfirmButtonExtender ID="btnAnnulerProforma_ConfirmButtonExtender" runat="server" 
                                TargetControlID="btnAnnulerProforma"
                                ConfirmText="Voulez vous vraiment annuler le devis?">
                            </asp:ConfirmButtonExtender>
                            <asp:Button ID="btnEnregistrerProforma" runat="server" Text="Enregistrer" 
                                onclick="btnEnregistrerProforma_Click" SkinID="btnValidation"/>
                                    
                           
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:Button ID="btnEditer" runat="server" Text="Editer" SkinID="btnValidation" 
                            onclick="btnEditer_Click"/>
                    <br /><br />

                    <div class="formContent2">
                        <div class="titreLG">
                            &nbsp;&nbsp;Paiement
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_FormulaireProforma" runat="server">
                                <ContentTemplate>
                                    <div class="divLeft">
                                    <asp:Panel ID="Panel_Individu" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="5" class="titreTab">
                                                    Individu
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Adresse:
                                                </td>
                                                
                                                <td>
                                                    <asp:Label ID="LabelAdresseClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPrenomClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelCINClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileClient" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_Societe" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="5" class="titreTab">
                                                    Société
                                                </td>
                                            </tr>
                                            <tr>
                                            
                                                <td>Nom société:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Mail:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMailSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Secteur d'activité:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelSecteurActiviteSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Adresse:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelAdresseSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="titreTab" colspan="5">
                                                    Responsable
                                                </td>
                                             </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Adresse:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelAdresseRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                              </tr>
                                            
                                            <tr>
                                                 <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPrenomRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td>
                                                    CIN:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelCINRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_Organisme" runat="server">
                                            <table>
                                                <tr>
                                                    <td colspan="5" class="titreTab">
                                                        Organisme
                                                    </td>
                                                </tr>
                                                <tr>
                                                
                                                    <td>Nom organisme:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelNomOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>Téléphone fixe:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelFixeOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                 </tr>
                                                <tr>
                                                    <td>
                                                        Adresse:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelAdresseOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>Téléphone mobile:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMobileOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                 </tr>
                                                <tr>
                                                    <td>Mail:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMailOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td colspan="3"></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="5" class="titreTab">
                                                        Responsable
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Nom:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelNomRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>Adresse:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelAdresseRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                  </tr>
                                                <tr>
                                                    <td>Prénom:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelPrenomRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        Téléphone fixe:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelFixeRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>CIN:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelCINRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        Téléphone mobile:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelMobileRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </asp:Panel>
                                        <br />
                                        <table>
                                        <tr>
                                            <td>Montant:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMontantProforma" runat="server" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>Ar
                                                <asp:RequiredFieldValidator ID="TextMontantProforma_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextMontantProforma" ValidationGroup="gProformaPaiement">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Mode de paiement:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlModePaiement" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlModePaiement_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlModePaiement_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlModePaiement" ValidationGroup="gProformaPaiement">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>

                                    <div class="divRight">
                                    <asp:Panel ID="Panel_BonDeCommande" runat="server">
                                    <div class="formContent">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Bon de commande
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                                <tr>
                                                    <td>Date de paiement:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextDatePaiementBC" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextDatePaiementBC_CalendarExtender" runat="server" 
                                                            Enabled="True" TargetControlID="TextDatePaiementBC" Format="dd MMMM yyyy">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextDatePaiementBC_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextDatePaiementBC" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Description:
                                                    </td>
                                                    <td rowspan="2" colspan="2">
                                                        <asp:TextBox ID="TextDescriptionBC" runat="server" TextMode="MultiLine" 
                                                            Width="300px"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                    <asp:Panel ID="Panel_Cheque" runat="server">
                                    <div class="formContent">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Chèque
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                                <tr>
                                                    <td>Banque:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBanque" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextBanque_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextBanque" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>Montant:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextMontant" runat="server"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender
                                                        ID="TextMontant_FilteredTextBoxExtender"
                                                        runat="server" 
                                                        TargetControlID="TextMontant"
                                                        FilterType="Custom, Numbers"
                                                        ValidChars="" />
                                                    </td>
                                                    <td>
                                                        Ar
                                                        <asp:RequiredFieldValidator ID="TextMontant_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextMontant" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>N° de chèque:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextNumeroCheque" runat="server"></asp:TextBox>
                                            
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextNumeroCheque_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextNumeroCheque" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;
                                                    </td>

                                                    <td>Du:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextDateCheque" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextDateCheque_CalendarExtender" runat="server" 
                                                            Enabled="True" TargetControlID="TextDateCheque" Format="dd MMMM yyyy">
                                                        </asp:CalendarExtender>
                                            
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextDateCheque_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextDateCheque" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>N° de compte:
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="TextNumCompte" runat="server" Width="380px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextNumCompte_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextNumCompte" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Titulaire:
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="TextTitulaire" runat="server" Width="380px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextTitulaire_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextTitulaire" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Adresse titulaire:
                                                    </td>
                                                    <td  rowspan="2" colspan="4">
                                                        <asp:TextBox ID="TextAdresseTutulaire" runat="server" Height="40px" Width="380px" 
                                                            TextMode="MultiLine"></asp:TextBox>
                                            
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextAdresseTutulaire_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextAdresseTutulaire" ValidationGroup="gTemp">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                    </div>

                                <div class="clear"></div>
                                <table>
                                    <tr>
                                        <td>
                                            <div id="divIndicationPaiement" runat="server"></div>
                                            <asp:ValidationSummary ID="ValidationSummary_FormulairePaiement" runat="server" ValidationGroup="gProformaPaiement"/>
                                        </td>
                                    </tr>
                                </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnValiderPaiement" runat="server" Text="Valider" 
                                        ValidationGroup="gProformaPaiement" onclick="btnValiderPaiement_Click" SkinID="btnValidation"/>
                                    
                                </td>
                            </tr>
                        </table>
                        </div>
                    </div>

                </div>
            </div>
            <br />
            
            <div class="formContent">
                <asp:UpdatePanel ID="UpdatePanel_ListeProforma" runat="server">
                    <ContentTemplate>
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Proforma
                        </div>
                        <div class="divListe">
                            <asp:DropDownList ID="ddlTriProforma" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriProforma_SelectedIndexChanged">
                                <asp:ListItem Text="N° Pro forma" Value="proforma.numProforma"></asp:ListItem>
                                <asp:ListItem Text="Nom" Value="nomClient"></asp:ListItem>
                                <asp:ListItem Text="Prénom" Value="prenomClient"></asp:ListItem>
                                <asp:ListItem Text="Nom société" Value="nomSociete"></asp:ListItem>
                                <asp:ListItem Text="Nom responsable société" Value="societe.nomResponsable"></asp:ListItem>
                                <asp:ListItem Text="Prénom responsable société" Value="societe.prenomResponsable"></asp:ListItem>
                                <asp:ListItem Text="Nom organisme" Value="nomOrganisme"></asp:ListItem>
                                <asp:ListItem Text="Nom responsable organisme" Value="organisme.nomResponsable"></asp:ListItem>
                                <asp:ListItem Text="Prénom responsable organisme" Value="organisme.prenomResponsable"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheProforma" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheProforma" runat="server" Text="Rechercher" 
                                onclick="btnRechercheProforma_Click" />
                            <asp:GridView ID="gvProforma" runat="server" AutoGenerateColumns="False" 
                                AllowPaging="True" onpageindexchanging="gvProforma_PageIndexChanging" 
                                onrowcommand="gvProforma_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("numProforma") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="numProforma" HeaderText="N°" />
                                    <asp:BoundField DataField="client" HeaderText="Individu/Société/Organisme" />
                                    <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                    <asp:BoundField DataField="contact" HeaderText="Contact" />
                                    <asp:BoundField DataField="respSociete" HeaderText="Responsable Soc/Org" />
                                    <asp:BoundField DataField="respContact" HeaderText="Responsable contact" />
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                CommandArgument='<%# Eval("numProforma") %>' />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                TargetControlID="ibDelete"
                                                ConfirmText='<%# "Voulez vous vraiment supprimer le commande N° " + Eval("numProforma") + "? \nDe: " + Eval("client")%>' >
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            
            
             <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel_Formulaire" runat="server" CssClass="" Visible="false" Width="70%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Proforma au nom de
                                </div>
                                <div class="formulaire">
                                    <table> 
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RadioListeAbonnement" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="RadioListeAbonnement_SelectedIndexChanged" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Individu" Value="individu" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Société" Value="societe"></asp:ListItem>
                                                    <asp:ListItem Text="Organisme" Value="organisme"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>      
                                    </table>
                                    <asp:Panel ID="PanelClient" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="6" class="titreTab">
                                                    Individu
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
                                                    ErrorMessage="" ControlToValidate="TextNomClient">*
                                                    </asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextPrenom" runat="server"></asp:TextBox>
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
                                                    <asp:RequiredFieldValidator ID="TextCinClient_RequiredFieldValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextCinClient">*
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Adresse:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseClient" runat="server" TextMode="MultiLine"
                                                        Width="145px" Height="40px"></asp:TextBox>
                                                
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextAdresseClient_RequiredFieldValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextAdresseClient">*
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneFixeClient" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Téléphone mobile
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneMobile" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelSociete" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="6" class="titreTab">
                                                    Société
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom société:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextNomSociete" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextNomSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextNomSociete">*
                                                    </asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>Adresse société:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseSociete" runat="server" TextMode="MultiLine" 
                                                        Width="145px" Height="40px"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextAdresseSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextAdresseSociete">*
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>Mail société:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMailSociete" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="TextMailSociete_RegularExpressionValidator" runat="server" 
                                                        ErrorMessage="" ControlToValidate="TextMailSociete"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Secteur d'activité:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextSecteurSociete" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextSecteurSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextSecteurSociete">
                                                    *</asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td>Tél fixe société:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneSociete" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile société:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMobileSociete" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="titreTab">
                                                    Information sur le premier responsable de la société
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextNomResponsableSociete" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextNomResponsableSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextNomResponsableSociete">*</asp:RequiredFieldValidator>
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
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinRespSociete" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextCinRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextCinRespSociete">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>Adresse:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseRespSociete" runat="server" TextMode="MultiLine"
                                                    Width="145px" Height="40px"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextAdresseRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextAdresseRespSociete">*
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Mail:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMailRespSociete" runat="server"></asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="TextMailRespSociete_RegularExpressionValidator" runat="server" 
                                                        ErrorMessage="" ControlToValidate="TextMailRespSociete"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
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
                                                <td></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelOrganisme" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="6" class="titreTab">
                                                    Organisme
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
                                                    ControlToValidate="TextNomOrganisme">*</asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>Adresse organisme:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseOrganisme" runat="server" TextMode="MultiLine" 
                                                        Width="145px" Height="40px"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextAdresseOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextAdresseOrganisme">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>Mail organisme:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMailOrganisme" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="TextMailOrganisme_RegularExpressionValidator" runat="server" 
                                                        ErrorMessage="" ControlToValidate="TextMailOrganisme"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                </td>
                                                <td></td>
                                            </tr>
                                            
                                            <tr>
                                                <td>Tél fixe organisme:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextFixeOrganisme" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile organisme:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMobileOrganisme" runat="server"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="titreTab">
                                                    Information sur le premier responsable de l'organisme
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextNomRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextNomRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextNomRespOrganisme">*</asp:RequiredFieldValidator>
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
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextCinRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextCinRespOrganisme">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>Adresse:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseRespOrganisme" runat="server" TextMode="MultiLine"
                                                    Width="145px" Height="40px"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextAdresseRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextAdresseRespOrganisme">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Mail:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMailRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="TextMailRespOrganisme_RegularExpressionValidator" runat="server" 
                                                        ErrorMessage="" ControlToValidate="TextMailRespOrganisme"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
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
                                        </table>
                                    </asp:Panel>
                                    <table>
                                         <tr>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary_FormaulaireAbonnement" runat="server" 
                                                ValidationGroup="groupAbonnement"/>
                                                <div id="div1" runat="server">
                                                </div>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnValideEnregistrementProforma" runat="server" Text="Valider" 
                                                    onclick="btnValideEnregistrementProforma_Click" ValidationGroup="gProforma"/> 
                                                <asp:Button ID="btnAnnulerEnregistrementProforma" runat="server" Text="Annuler" 
                                                    onclick="btnAnnulerEnregistrementProforma_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:Panel ID="PanelListeClient" runat="server">
                                        <asp:DropDownList ID="ddlTriClient" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriClient_SelectedIndexChanged">
                                            <asp:ListItem Text="N°" Value="individu.numIndividu"></asp:ListItem>
                                            <asp:ListItem Text="Prénom" Value="individu.prenomIndividu"></asp:ListItem>
                                            <asp:ListItem Text="Nom" Value="individu.nomIndividu"></asp:ListItem>
                                            <asp:ListItem Text="Adresse" Value="individu.adresse"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheClient" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheClient" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheClient_Click" />
                                        <asp:GridView ID="gvClient" runat="server" AutoGenerateColumns="False" 
                                            EnableModelValidation="True" AllowPaging="True" 
                                            onpageindexchanging="gvClient_PageIndexChanging" 
                                            onrowcommand="gvClient_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("numIndividu") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                <asp:BoundField DataField="numIndividu" HeaderText="N°" />
                                                <asp:BoundField DataField="individu" HeaderText="Individu" />
                                                <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                                <asp:BoundField DataField="contact" HeaderText="Contact" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelListeSociete" runat="server">
                                        <asp:DropDownList ID="ddlTriSociete" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriSociete_SelectedIndexChanged">
                                            <asp:ListItem Text="N°" Value="numSociete"></asp:ListItem>
                                            <asp:ListItem Text="Société" Value="nomSociete"></asp:ListItem>
                                            <asp:ListItem Text="Prénom responsable" Value="prenomResponsable"></asp:ListItem>
                                            <asp:ListItem Text="Nom responsable" Value="nomResponsable"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheSociete" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheSociete" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheSociete_Click" />
                                        <asp:GridView ID="gvSociete" runat="server" AutoGenerateColumns="False" 
                                            EnableModelValidation="True" AllowPaging="True" 
                                            onpageindexchanging="gvSociete_PageIndexChanging" 
                                            onrowcommand="gvSociete_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("numSociete") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numSociete" HeaderText="N°" />
                                                <asp:BoundField DataField="nomSociete" HeaderText="Société" />
                                                <asp:BoundField DataField="secteurActiviteSociete" 
                                                    HeaderText="Secteur d'activité" />
                                                <asp:BoundField DataField="adresseSociete" HeaderText="Adresse" />
                                                <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                                <asp:BoundField DataField="adresseResponsable" HeaderText="Adresse" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelListeOrganisme" runat="server">
                                        <asp:DropDownList ID="ddlTriOrganisme" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriOrganisme_SelectedIndexChanged">
                                            <asp:ListItem Text="N°" Value="numOrganisme"></asp:ListItem>
                                            <asp:ListItem Text="Organisme" Value="nomOrganisme"></asp:ListItem>
                                            <asp:ListItem Text="Prénom responsable" Value="prenomResponsable"></asp:ListItem>
                                            <asp:ListItem Text="Nom responsable" Value="nomResponsable"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheOrganisme" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheOrganisme" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheOrganisme_Click" />
                                        <asp:GridView ID="gvOrganisme" runat="server" AutoGenerateColumns="False" 
                                            EnableModelValidation="True" 
                                            onpageindexchanging="gvOrganisme_PageIndexChanging" 
                                            onrowcommand="gvOrganisme_RowCommand" AllowPaging="True" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("numOrganisme") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numOrganisme" HeaderText="N°" />
                                                <asp:BoundField DataField="nomOrganisme" HeaderText="Organisme" />
                                                <asp:BoundField DataField="adresseOrganisme" HeaderText="Adresse" />
                                                <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                                <asp:BoundField DataField="adresseResponsable" HeaderText="Adresse" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="Panel_FormulaireClient" runat="server" CssClass="" Visible="false" Width="60%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Billet au nom de
                                </div>
                                <div class="formulaire">
                                    <table>
                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Client
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextClientNom" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextClientNom_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextClientNom" ValidationGroup="gFormClient">*
                                                </asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextClientPrenom" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextClientCin" runat="server"></asp:TextBox>
                                            
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextClientCin_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextClientCin" ValidationGroup="gFormClient">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Adresse:
                                            </td>
                                            <td rowspan="2">
                                                <asp:TextBox ID="TextClientAdesse" runat="server" TextMode="MultiLine"
                                                    Width="145px" Height="40px"></asp:TextBox>
                                            
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextClientAdesse_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextClientAdesse" ValidationGroup="gFormClient">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">&nbsp;</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextClientFixe" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Téléphone mobile
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextClientMobile" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:ValidationSummary ID="ValidationSummaryClientForm" runat="server" ValidationGroup="gFormClient"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Button ID="btnValiderFormulaireClient" runat="server" Text="Valider" 
                                                    onclick="btnValiderFormulaireClient_Click" ValidationGroup="gFormClient"/>
                                                <asp:Button ID="btnAnnulerFormulaireClient" runat="server" Text="Annuler" 
                                                    onclick="btnAnnulerFormulaireClient_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriClientListe" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriClientListe_SelectedIndexChanged">
                                        <asp:ListItem Text="N°" Value="individu.numIndividu"></asp:ListItem>
                                        <asp:ListItem Text="Prénom" Value="individu.prenomIndividu"></asp:ListItem>
                                        <asp:ListItem Text="Nom" Value="individu.nomIndividu"></asp:ListItem>
                                        <asp:ListItem Text="Adresse" Value="individu.adresse"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheClientListe" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercheClientListe" runat="server" Text="Rechercher" 
                                        onclick="btnRechercheClientListe_Click" />
                                    <asp:GridView ID="gvClientListe" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="gvClientListe_PageIndexChanging" 
                                        onrowcommand="gvClientListe_RowCommand" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("numIndividu") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                            <asp:BoundField DataField="numIndividu" HeaderText="N°" />
                                            <asp:BoundField DataField="individu" HeaderText="Individu" />
                                            <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                            <asp:BoundField DataField="contact" HeaderText="Contact" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_FormulaireExpediteurCommission" runat="server" CssClass="" Visible="false" Width="60%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Commission / Expéditeur
                                </div>
                                <div class="formulaire">
                                    <table>
                                        <tr>
                                            <td colspan="5">
                                                <div class="titreTab">Expéditeur</div>
                                            </td>
                                                
                                        </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomExpediteur" runat="server"></asp:TextBox>
                                                
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomExpediteur_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomExpediteur" ValidationGroup="gExpediteur">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                            </td>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenomExpediteur" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextCINExpediteur" runat="server"></asp:TextBox>
                                                
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextCINExpediteur_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextCINExpediteur" ValidationGroup="gExpediteur">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Adresse:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextAdresseExpediteur" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFixeExpediteur" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Portable:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPortableExpediteur" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:ValidationSummary ID="ValidationSummary_Expediteur" runat="server" ValidationGroup="gExpediteur"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Button ID="btnValideExpediteur" runat="server" Text="Valider" 
                                                    ValidationGroup="gExpediteur" onclick="btnValideExpediteur_Click"/>
                                                <asp:Button ID="btnAnnulerExpediteur" runat="server" Text="Annuler" 
                                                    onclick="btnAnnulerExpediteur_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriListeExpediteur" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriListeExpediteur_SelectedIndexChanged">
                                        <asp:ListItem Text="N°" Value="numClient"></asp:ListItem>
                                        <asp:ListItem Text="Prénom" Value="prenomClient"></asp:ListItem>
                                        <asp:ListItem Text="Nom" Value="nomClient"></asp:ListItem>
                                        <asp:ListItem Text="Adresse" Value="adresseClient"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheListeExpediteur" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercheListeExpediteur" runat="server" Text="Rechercher" 
                                        onclick="btnRechercheListeExpediteur_Click"  />
                                    <asp:GridView ID="gvListeExpediteur" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="gvListeExpediteur_PageIndexChanging" 
                                        onrowcommand="gvListeExpediteur_RowCommand" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("numClient") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                            <asp:BoundField DataField="numClient" HeaderText="N°" />
                                            <asp:BoundField DataField="client" HeaderText="Individu" />
                                            <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                            <asp:BoundField DataField="contact" HeaderText="Contact" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_FormulaireRecepteurCommission" runat="server"  CssClass="" Visible="false" Width="60%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Commission / Réceptionnaire
                                </div>
                                <div class="formulaire">
                                    <table>
                                        <tr>
                                            <td colspan="5">
                                                <div class="titreTab">Réceptionnaire</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomRecepteur" runat="server"></asp:TextBox>
                                                
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomRecepteur_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomRecepteur" ValidationGroup="gRecepteur">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenomRecepteur" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextTelephoneRecepteur" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Adresse:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextAdresseRecepteur" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:ValidationSummary ID="ValidationSummary_Recepteur" runat="server" ValidationGroup="gRecepteur"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Button ID="btnValideRecepteurCommission" runat="server" Text="Valider" 
                                                    ValidationGroup="gRecepteur" onclick="btnValideRecepteurCommission_Click"/>
                                                <asp:Button ID="btnAnnulerRecepteurCommission" runat="server" Text="Annuler" 
                                                    onclick="btnAnnulerRecepteurCommission_Click"/>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriRecepteur" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriRecepteur_SelectedIndexChanged">
                                        <asp:ListItem Text="N°" Value="idPersonne"></asp:ListItem>
                                        <asp:ListItem Text="Nom" Value="nomPersonne"></asp:ListItem>
                                        <asp:ListItem Text="Prénom" Value="prenomPersonne"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheRecepteur" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercheRecepteur" runat="server" Text="Rechercher" 
                                        onclick="btnRechercheRecepteur_Click" />
                                    <asp:GridView ID="gvRecepteur" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvRecepteur_PageIndexChanging" 
                                        onrowcommand="gvRecepteur_RowCommand" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("idPersonne") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="idPersonne" HeaderText="N°" />
                                            <asp:BoundField DataField="receptionnaire" HeaderText="Réceptionnaire" />
                                            <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                            <asp:BoundField DataField="contact" HeaderText="Contact" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_FormulaireAbonnement" runat="server" CssClass="" Visible="false" Width="60%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                    &nbsp;&nbsp;Abonnement
                                </div>
                                <div class="formulaire">
                                    <asp:RadioButtonList ID="RadioButtonList_TypeAbonnement" runat="server" 
                                        RepeatDirection="Horizontal" AutoPostBack="True" 
                                        onselectedindexchanged="RadioButtonList_TypeAbonnement_SelectedIndexChanged">
                                        <asp:ListItem Text="Abonnement par nombre de voyage" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Abonnement par durée de temps" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:HiddenField ID="hfNumAbonnement" runat="server" />
                                    
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAnnulerAbonnement" runat="server" Text="Annuler" 
                                                    onclick="btnAnnulerAbonnement_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:Panel ID="Panel_AbonnementNbVoyageListe" runat="server">
                                        <asp:DropDownList ID="ddlTriVoyageAbonnement" runat="server" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriVoyageAbonnement_SelectedIndexChanged">
                                            <asp:ListItem Text="N°" Value="voyageabonnement.numVoyageAbonnement"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheVoyageAbonnement" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheVoyageAbonnement" runat="server" Text="Rechercher" 
                                            onclick="btnRechercheVoyageAbonnement_Click" />
                                        <asp:GridView ID="gvVoyageAbonnement" runat="server" 
                                            AutoGenerateColumns="False" EnableModelValidation="True" 
                                            AllowPaging="True" onpageindexchanging="gvVoyageAbonnement_PageIndexChanging" 
                                            onrowcommand="gvVoyageAbonnement_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("numVoyageAbonnement") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numVoyageAbonnement" HeaderText="N°" />
                                                <asp:BoundField DataField="zone" HeaderText="Zone" />
                                                <asp:BoundField DataField="dateVoyageAbonnement" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}" />
                                                <asp:BoundField DataField="nbVoyageAbonnement" HeaderText="Nb" />
                                                <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_AbonnementDureeTempsListe" runat="server">
                                        <asp:DropDownList ID="ddlTriAbonnementDureeTemps" runat="server" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddlTriAbonnementDureeTemps_SelectedIndexChanged">
                                            <asp:ListItem Text="N°" Value="dureeabonnement.numDureeAbonnement"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextRechercheAbonnementDureeTemps" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnRechercheAbonnementDureeTemps" runat="server" 
                                            Text="Rechercher" onclick="btnRechercheAbonnementDureeTemps_Click" />
                                        <asp:GridView ID="gvAbonnementDureeTemps" runat="server" 
                                            AutoGenerateColumns="False" EnableModelValidation="True" 
                                            AllowPaging="True" 
                                            onpageindexchanging="gvAbonnementDureeTemps_PageIndexChanging" 
                                            onrowcommand="gvAbonnementDureeTemps_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                            CommandArgument='<%# Eval("numDureeAbonnement") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="numDureeAbonnement" HeaderText="N°" />
                                                <asp:BoundField DataField="zone" HeaderText="Zone" />
                                                <asp:BoundField DataField="valideAu" HeaderText="Valide au" DataFormatString="{0:dd MMMM yyyy}"/>
                                                <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
               
        </div>
    </div>
</asp:Content>
