<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.master" AutoEventWireup="true" CodeBehind="DevisAbonnement.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.DevisAbonnement" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
     <div id="OneLayote">
        <div class="formContent3">
            <asp:UpdatePanel ID="UpdatePanel_Titre" runat="server">
                <ContentTemplate>
                     <div class="grandTitre">
                        &nbsp;&nbsp;Abonnement
                        &nbsp;&nbsp;<asp:LinkButton ID="btnAbonner" runat="server" CssClass="linkClass" 
                             onclick="btnAbonner_Click"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:Button ID="btnAbonnerListe" runat="server" Text="Abonner" 
                             onclick="btnAbonnerListe_Click" />    
                        
                         <asp:HiddenField ID="hfNumAbonnement" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="OneLayoteLeft50">   
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Abonnement par nombre de voyage
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_formulaireVoyageAbonnement" runat="server" 
                            RenderMode="Inline">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Zone:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlZoneNombreVoyage" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlZoneNombreVoyage_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlZoneNombreVoyage_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ValidationGroup="gNombreVoyage" ControlToValidate="ddlZoneNombreVoyage">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Panel ID="Panel_FormulaireTrajetRNVoyageAbonnement" runat="server">
                                    <div class="formContent2">
                                         <div class="titreLG">
                                            &nbsp;&nbsp;Trajet
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                                <tr>
                                                    <td colspan="6">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>Ville de départ:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlVilleDepart" runat="server" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlVilleDepart_SelectedIndexChanged" >
                                                        </asp:DropDownList>
                                                        <asp:ListSearchExtender ID="ddlVilleDepart_ListSearchExtender" runat="server" 
                                                            Enabled="True" TargetControlID="ddlVilleDepart" PromptText="Recherche">
                                                        </asp:ListSearchExtender>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                        Destination:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDestination" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlDestination_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                    <asp:ListSearchExtender ID="ddlDestination_ListSearchExtender" runat="server" 
                                                        Enabled="True" PromptText="Recherche" TargetControlID="ddlDestination">
                                                    </asp:ListSearchExtender>
                                                </td>
                                                <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Catégorie:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCalculCategorieBillet" runat="server" 
                                                            AutoPostBack="True" 
                                                            onselectedindexchanged="ddlCalculCategorieBillet_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="ddlCalculCategorieBillet_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlCalculCategorieBillet">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>Prix unitaire:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextPrixBilletUnitaire" runat="server" ReadOnly="true" Width="120"></asp:TextBox>Ar
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:RequiredFieldValidator ID="TextPrixBilletUnitaire_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextPrixBilletUnitaire">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>    
                                                    <td colspan="6">
                                                        <asp:HiddenField ID="hfNumTrajet" runat="server" />
                                                    </td>
                                                </tr>
                                          </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="Panel_SUVoyageAbonnement" runat="server">
                                    <div class="formContent2">
                                         <div class="titreLG">
                                            &nbsp;&nbsp;Urbaine et suburbaine
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                                <tr>
                                                    <td>Prix unitaire:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextPrixUnitaireSU" runat="server" Text="300" 
                                                            AutoPostBack="True" ontextchanged="TextPrixUnitaireSU_TextChanged" Width="120"></asp:TextBox>
                                                        Ar
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="TextPrixUnitaireSU_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextPrixUnitaireSU">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <table>
                                                
                                    <tr>
                                        <td>Nombre de voyage:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNbVoyageVA" runat="server" AutoPostBack="True" 
                                                ontextchanged="TextNbVoyageVA_TextChanged" Width="120"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextNbVoyageVA_FilteredTextBoxExtender"
                                                    runat="server"
                                                    TargetControlID="TextNbVoyageVA"
                                                    FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNbVoyageVA_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNbVoyageVA" ValidationGroup="gNombreVoyage">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                        </td>
                                        <td>Prix total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrixTotalVA" runat="server" ReadOnly="true" Width="120"></asp:TextBox>Ar
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPrixTotalVA_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPrixTotalVA" ValidationGroup="gNombreVoyage">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_NombreVoyage" runat="server" ValidationGroup="gNombreVoyage"/>
                                            <div id="divIndicationAV" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfANV" runat="server" />
                                        </td>
                                    </tr>
                                   
                                </table>
                                <asp:Button ID="btnNouveauNombreVoyage" runat="server" Text="Nouveau" 
                                        onclick="btnNouveauNombreVoyage_Click" SkinID="btnValidation"/>
                                <asp:Button ID="btnModifierNombreVoyage" runat="server" Text="Modifier" SkinID="btnValidation"
                                    ValidationGroup="gNombreVoyage" onclick="btnModifierNombreVoyage_Click"/>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifierNombreVoyage" runat="server" 
                                    TargetControlID="btnModifierNombreVoyage"
                                    ConfirmText="">
                                </asp:ConfirmButtonExtender>
                                            
                                <asp:Button ID="btnAjouterNombreVoyage" runat="server" Text="   Devis   " 
                                    ValidationGroup="gNombreVoyage" onclick="btnAjouterNombreVoyage_Click" SkinID="btnValidation"/>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnValideANV" runat="server" Text="Valider" 
                            ValidationGroup="gNombreVoyage" onclick="btnValideANV_Click" SkinID="btnValidation"/>
                    </div>
                </div> 
                <br />
            </div>

            <div id="OneLayoteRight50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Abonnement par durée de temps
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_formulaireDureeAbonnement" runat="server" 
                            RenderMode="Inline">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Zone:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlZoneDureeTemps" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlZoneDureeTemps_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlZoneDureeTemps_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ValidationGroup="gDureeTemps" ControlToValidate="ddlZoneDureeTemps">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Panel ID="Panel_FormulaireTrajetRNDureeTemps" runat="server">
                                    <div class="formContent2">
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Trajet
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                                <tr>
                                                    <td colspan="6">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>Ville de départ:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDepartDureeTemps" runat="server" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlDepartDureeTemps_SelectedIndexChanged" >
                                                        </asp:DropDownList>
                                                        <asp:ListSearchExtender ID="ddlDepartDureeTemps_ListSearchExtender" runat="server" 
                                                            Enabled="True" TargetControlID="ddlDepartDureeTemps" PromptText="Recherche">
                                                        </asp:ListSearchExtender>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                        Destination:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDestinationDureeTemps" runat="server" 
                                                        AutoPostBack="True" 
                                                        onselectedindexchanged="ddlDestinationDureeTemps_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                    <asp:ListSearchExtender ID="ddlDestinationDureeTemps_ListSearchExtender" runat="server" 
                                                        Enabled="True" PromptText="Recherche" TargetControlID="ddlDestinationDureeTemps">
                                                    </asp:ListSearchExtender>
                                                </td>
                                                <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Catégorie:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCategorieDureeTemps" runat="server" 
                                                            AutoPostBack="True" 
                                                            onselectedindexchanged="ddlCategorieDureeTemps_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="ddlCategorieDureeTemps_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlCategorieDureeTemps">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>Prix unitaire:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Text_PrixUnitaireDureeTemps" runat="server" ReadOnly="true" Width="120"></asp:TextBox>
                                                        Ar
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:RequiredFieldValidator ID="Text_PrixUnitaireDureeTemps_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="Text_PrixUnitaireDureeTemps">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>    
                                                    <td colspan="6">
                                                        <asp:HiddenField ID="hfNumTrajetDureeTemps" runat="server" />
                                                    </td>
                                                </tr>
                                          </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="Panel_SUDureeTemps" runat="server">
                                    <div class="formContent2">
                                         <div class="titreLG">
                                            &nbsp;&nbsp;Urbaine et suburbaine
                                        </div>
                                        <div class="formulaire">
                                            <table>
                                                <tr>
                                                    <td>Prix unitaire:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextPrixUnitaireSUDureeTemps" runat="server" Text="300" 
                                                            AutoPostBack="True"></asp:TextBox>
                                                    </td>
                                                    <td>Ar
                                                        <asp:RequiredFieldValidator ID="TextPrixUnitaireSUDureeTemps_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextPrixUnitaireSUDureeTemps">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <table>
                                        
                                    <tr>
                                        <td>Valide du:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateDuDA" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextDateDuDA_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateDuDA" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDateDuDA_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextDateDuDA" ValidationGroup="gDureeTemps">*</asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                        </td>
                                        <td>au:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateAuDA" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextDateAuDA_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateAuDA" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextDateAuDA_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextDateAuDA" ValidationGroup="gDureeTemps">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td>Prix:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMontantADT" runat="server" AutoPostBack="True" 
                                                ontextchanged="TextMontantADT_TextChanged" Width="120"></asp:TextBox>Ar
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextMontantADT_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextMontantADT" ValidationGroup="gDureeTemps">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td>
                                            Nombre de personne:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNbPersonne" runat="server" AutoPostBack="True" 
                                                ontextchanged="TextNbPersonne_TextChanged"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender
                                                    ID="TextNbPersonne_FilteredTextBoxExtender"
                                                    runat="server"
                                                    TargetControlID="TextNbPersonne"
                                                    FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNbPersonne_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNbPersonne" ValidationGroup="gDureeTemps">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Prix total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrixTotalDA" runat="server" ReadOnly="true" Width="120"></asp:TextBox>
                                            Ar
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextPrixTotalDA_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextPrixTotalDA" ValidationGroup="gDureeTemps">*</asp:RequiredFieldValidator>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:ValidationSummary ID="ValidationSummary_DureeTemps" runat="server" ValidationGroup="gDureeTemps"/>
                                            <div id="divIndicationAD" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfADT" runat="server" />
                                        </td>
                                    </tr>
                                   
                                </table>
                                <asp:Button ID="btnNouveauDureeTemps" runat="server" Text="Nouveau" SkinID="btnValidation"
                                        onclick="btnNouveauDureeTemps_Click" />
                                <asp:Button ID="btnModifierDureeTemps" runat="server" Text="Modifier" 
                                    ValidationGroup="gDureeTemps" onclick="btnModifierDureeTemps_Click" SkinID="btnValidation"/>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifierDureeTemps" runat="server" 
                                    TargetControlID="btnModifierDureeTemps"
                                    ConfirmText="">
                                </asp:ConfirmButtonExtender>
                                            
                                <asp:Button ID="btnAjouterDureeTemps" runat="server" Text="   Devis   " 
                                    ValidationGroup="gDureeTemps" onclick="btnAjouterDureeTemps_Click" SkinID="btnValidation"/>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnValiderADT" runat="server" Text="Valider" 
                                ValidationGroup="gDureeTemps" onclick="btnValiderADT_Click" SkinID="btnValidation"/>
                    </div>
                </div>
                <br />
            </div>

            <div class="clear">
            </div>

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
                                        Abonnement par nombre de voyage
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td class="titreTab">
                                        Abonnement par durée de temps
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:GridView ID="gvANV" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" EnableModelValidation="True" PageSize="5" 
                                                onpageindexchanging="gvANV_PageIndexChanging" onrowcommand="gvANV_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                                CommandArgument='<%# Eval("numVoyageAbonnementDevis") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="zone" HeaderText="Zone" />
                                                    <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                                    <asp:BoundField DataField="nbVoyageAbonnement" HeaderText="Nb voyage" />
                                                    <asp:BoundField DataField="prixTotal" HeaderText="Montant" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("numVoyageAbonnementDevis") %>' />
                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                                TargetControlID="ibDelete"
                                                                ConfirmText='<%# "Voulez vous vraiment supprimer abonnement du zone " + Eval("zone") + "? \nMontant: " + Eval("prixTotal")%>' >
                                                            </asp:ConfirmButtonExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:GridView ID="gvADT" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" EnableModelValidation="True" PageSize="5" 
                                                onpageindexchanging="gvADT_PageIndexChanging" onrowcommand="gvADT_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                                CommandArgument='<%# Eval("numDureeAbonnementDevis") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="zone" HeaderText="Zone" />
                                                    <asp:BoundField DataField="trajet" HeaderText="Trajet" />
                                                    <asp:BoundField DataField="valideDu" HeaderText="Valide du" DataFormatString="{0:dd MMMM yyyy}"/>
                                                    <asp:BoundField DataField="valideAu" HeaderText="au" DataFormatString="{0:dd MMMM yyyy}"/>
                                                    <asp:BoundField DataField="prixTotal" HeaderText="Montant" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("numDureeAbonnementDevis") %>' />
                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                                TargetControlID="ibDelete"
                                                                ConfirmText='<%# "Voulez vous vraiment supprimer abonnement du zone " + Eval("zone") + "? \nMontant: " + Eval("prixTotal")%>' >
                                                            </asp:ConfirmButtonExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>Montant ANV:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelPrixTotalANV" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                                                    <td>Montant ADT:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelPrixTotaADT" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                                                        <asp:Label ID="LabelTotalDevis" runat="server" Text="" SkinID="LabelMontantTotal"></asp:Label>
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
                            <asp:Button ID="btnNouveauDevis" runat="server" Text="Nouveau" 
                                onclick="btnNouveauDevis_Click" SkinID="btnValidation"/>
                            <asp:Button ID="btnAnnulerDevis" runat="server" Text="Supprimer" 
                                onclick="btnAnnulerDevis_Click" SkinID="btnValidation"/>
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnAnnulerDevis" runat="server" 
                                TargetControlID="btnAnnulerDevis"
                                ConfirmText="Voulez vous vraiment supprimer ces enregistrements?">
                            </asp:ConfirmButtonExtender>
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
                                            &nbsp;&nbsp;Information bon de commande
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
                                            &nbsp;&nbsp;Information chèque
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
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                    </div>

                                   <div class="clear"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnValiderPaiement" runat="server" Text="Valider" SkinID="btnValidation"
                                        ValidationGroup="gProformaPaiement" onclick="btnValiderPaiement_Click"/>
                                    
                                </td>
                            </tr>
                        </table>
                        </div>
                    </div>

                </div>
            </div>
            <br />
            <div class="formContent">
                <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Proforma 
                </div>
                
                    <asp:UpdatePanel ID="UpdatePanel_ListeProforma" runat="server">
                        <ContentTemplate>
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
                                    onclick="btnRechercheProforma_Click"/>
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



            <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel_FormulaireAbonner" runat="server" CssClass="" Visible="false" Width="90%">
                        <div class="panIntern">
                            <div class="formContent">
                                <div class="collapsePanelHeader">
                                   &nbsp;&nbsp;Abonner
                                </div>
                                <div class="formulaire">
                                    <table> 
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RadioListeAbonnement" runat="server" 
                                                    AutoPostBack="True" 
                                                    RepeatDirection="Horizontal" Enabled="false">
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
                                                    <asp:TextBox ID="TextNomClient" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextPrenom" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinClient" runat="server" ReadOnly="true"></asp:TextBox>
                                                
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
                                                        Width="145px" Height="40px" ReadOnly="true"></asp:TextBox>
                                                
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
                                                    <asp:TextBox ID="TextTelephoneFixeClient" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Téléphone mobile
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTelephoneMobile" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextNomSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                        Width="145px" Height="40px" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                    <asp:TextBox ID="TextMailSociete" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextSecteurSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                    <asp:TextBox ID="TextTelephoneSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile société:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMobileSociete" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextNomResponsableSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextNomResponsableSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextNomResponsableSociete">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Prénom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextPrenomRespSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinRespSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextCinRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextCinRespSociete">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>Adresse:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseRespSociete" runat="server" TextMode="MultiLine"
                                                    Width="145px" Height="40px" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                    <asp:TextBox ID="TextMailRespSociete" runat="server" ReadOnly="true"></asp:TextBox>

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
                                                    <asp:TextBox ID="TextFixeRespSociete" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMobileRespSociete" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextNomOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                        Width="145px" Height="40px" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                    <asp:TextBox ID="TextMailOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextFixeOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile organisme:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMobileOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TextNomRespOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextNomRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextNomRespOrganisme">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Prénom:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextPrenomRespOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextCinRespOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="TextCinRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextCinRespOrganisme">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>Adresse:
                                                </td>
                                                <td rowspan="2">
                                                    <asp:TextBox ID="TextAdresseRespOrganisme" runat="server" TextMode="MultiLine"
                                                    Width="145px" Height="40px" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                    <asp:TextBox ID="TextMailRespOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                    
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
                                                    <asp:TextBox ID="TextFixeRespOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>Tél mobile:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextMobileRespOrganisme" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary_Abonner" runat="server" ValidationGroup="groupAbonnement"/>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnValiderAbonner" runat="server" Text="Valider" 
                                                    ValidationGroup="groupAbonnement" onclick="btnValiderAbonner_Click"/>
                                                <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" 
                                                    onclick="btnAnnuler_Click" />
                                                <asp:HiddenField ID="hfNumAbonnementTemp" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="divListe">
                                    <asp:DropDownList ID="ddlTriAbonnement" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriAbonnement_SelectedIndexChanged">
                                        <asp:ListItem Text="N° Abonnement" Value="numAbonnement"></asp:ListItem>
                                        <asp:ListItem Text="Nom" Value="nomClient"></asp:ListItem>
                                        <asp:ListItem Text="Prénom" Value="prenomClient"></asp:ListItem>
                                        <asp:ListItem Text="Nom société" Value="nomSociete"></asp:ListItem>
                                        <asp:ListItem Text="Nom responsable société" Value="societe.nomResponsable"></asp:ListItem>
                                        <asp:ListItem Text="Prénom responsable société" Value="societe.prenomResponsable"></asp:ListItem>
                                        <asp:ListItem Text="Nom organisme" Value="nomOrganisme"></asp:ListItem>
                                        <asp:ListItem Text="Nom responsable organisme" Value="organisme.nomResponsable"></asp:ListItem>
                                        <asp:ListItem Text="Prénom responsable organisme" Value="organisme.prenomResponsable"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheAbonnement" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercherAbonnement" runat="server" Text="Rechercher" 
                                        onclick="btnRechercherAbonnement_Click"/>
                                    <asp:GridView ID="gvAbonnement" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" EnableModelValidation="True" 
                                        onpageindexchanging="gvAbonnement_PageIndexChanging" 
                                        onrowcommand="gvAbonnement_RowCommand" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("numAbonnement") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numAbonnement" HeaderText="N° Abonnement" />
                                            <asp:BoundField DataField="client" HeaderText="Individu/Société/Organisme" />
                                            <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                            <asp:BoundField DataField="contact" HeaderText="Contact" />
                                            <asp:BoundField DataField="respSociete" HeaderText="Resonsable société/Organisme" />
                                            <asp:BoundField DataField="respContact" HeaderText="Contact Responsable"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
