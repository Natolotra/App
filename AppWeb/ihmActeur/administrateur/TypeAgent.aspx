<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TypeAgent.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.TypeAgent" %>
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
                &nbsp;&nbsp;Type agent
            </div>
            <div id="OneLayoteLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;page
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_PageAgent" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td class="titreTab">
                                            Technique
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Check_100" runat="server" Text="Autorisation de voyage" Font-Bold="true" 
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_101" runat="server" Text="Vérification"
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titreTab">
                                            Administrateur
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Check_010" runat="server" Text="Paramètre" Font-Bold="true" 
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_011" runat="server" 
                                                Text="Catégorie billet" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_012" runat="server" 
                                                Text="Type et désignation commission" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_013" runat="server" Text="Tarif développement"
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_014" runat="server" Text="Type agent"
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_020" runat="server" 
                                                Text="Axe" AutoPostBack="True" Font-Bold="true"
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_021" runat="server" 
                                                Text="Ville" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_022" runat="server" 
                                                Text="Route nationale" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_023" runat="server" 
                                                Text="Itinéraire" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_030" runat="server" 
                                                Text="Etablissement" AutoPostBack="True" Font-Bold="true"
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_031" runat="server" 
                                                Text="Société" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_032" runat="server" 
                                                Text="Organime" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                             &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_033" runat="server" 
                                                Text="Coopérative" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                             &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_034" runat="server" 
                                                Text="Agence" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_040" runat="server" Font-Bold="true"
                                                Text="Acteur" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_041" runat="server" 
                                                Text="Chauffeur" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_042" runat="server" 
                                                Text="Client" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_043" runat="server" Text="Agent"
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_050" runat="server" Font-Bold="true"
                                                Text="Logistique" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_051" runat="server" 
                                                Text="Véhicule et licence" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_060" runat="server" Font-Bold="true"
                                                Text="Observation" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_061" runat="server" 
                                                Text="Véhicule" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_062" runat="server" 
                                                Text="Chauffeur RN" AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_063" runat="server" Text="Agent"
                                                AutoPostBack="True" oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titreTab">
                                            Commercial
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Check_200" runat="server" Text="Vente" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_201" runat="server" 
                                                Text="Billet et Commission" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_202" runat="server" 
                                                Text="Abonnement" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_203" runat="server" 
                                                Text="Excédent bagage" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_210" runat="server" Text="Paiement" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_211" runat="server" 
                                                Text="Prélèvement" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_212" runat="server" 
                                                Text="Facture" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_220" runat="server" Text="Abonnement" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_221" runat="server" 
                                                Text="Enregistrement" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_230" runat="server" Text="Compte" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_231" runat="server" 
                                                Text="encaisse" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titreTab">
                                            Planning
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Check_300" runat="server" Text="Fiche de bord" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_301" runat="server" 
                                                Text="Etablir" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_302" runat="server" 
                                                Text="Remplir" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_310" runat="server" Text="Autorisation de départ" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_311" runat="server" 
                                                Text="Etablir" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_320" runat="server" Text="Vue générale" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_321" runat="server" 
                                                Text="Fiche de bord" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_330" runat="server" Text="Calendrier (FB modifiable)" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_331" runat="server" 
                                                Text="Fiche de bord" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_332" runat="server" 
                                                Text="Autorisation de départ" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_340" runat="server" 
                                                Text="Calendrier (FB non modifiable)" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_341" runat="server" 
                                                Text="Fiche de bord" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            <asp:CheckBox ID="Check_350" runat="server" Text="Remplir Fiche de bord" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titreTab">
                                            Responsable agence
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Check_400" runat="server" Text="Edition" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                             &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_401" runat="server" 
                                                Text="Fiche de bord" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_410" runat="server" Text="Session agent" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                             &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_411" runat="server" 
                                                Text="Ouverture / Fermeture" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_420" runat="server" Text="Session agence" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                             &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_421" runat="server" 
                                                Text="Somme encaissée" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_422" runat="server" 
                                                Text="Session Agence" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />

                                            <asp:CheckBox ID="Check_430" runat="server" Text="Avance et Facturation" AutoPostBack="true"
                                                Font-Bold="true" oncheckedchanged="onCheck_CheckedChanged"/><br />
                                             &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_431" runat="server" 
                                                Text="Prélèvement" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Check_432" runat="server" 
                                                Text="Facturation" AutoPostBack="True" 
                                                oncheckedchanged="onCheck_CheckedChanged"/><br />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    
                    </div>
                    
                    <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                        <ContentTemplate>
                            <div class="formulaire">
                                <table>
                                    <tr>
                                        <td>Type agent:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextTypeAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextTypeAgent_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextTypeAgent" ValidationGroup="gTypeAgent">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commentaire
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextCommentaire" runat="server" TextMode="MultiLine" Height="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:ValidationSummary ID="ValidationSummary_Formulaire" runat="server" ValidationGroup="gTypeAgent"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfTypeAgent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveau_Click"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" SkinID="btnValidation"
                                                ValidationGroup="gTypeAgent" onclick="btnModifier_Click"/>
                                            <asp:ConfirmButtonExtender runat="server" TargetControlID="btnModifier"
                                                ID="btnModifier_ConfirmButtonExtender" ConfirmText=""></asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" SkinID="btnValidation"
                                                ValidationGroup="gTypeAgent" onclick="btnValider_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="divListe">
                                <asp:GridView ID="gvTypeAgent" runat="server" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" AllowPaging="True" 
                                    onpageindexchanging="gvTypeAgent_PageIndexChanging" 
                                    onrowcommand="gvTypeAgent_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("typeAgent") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="typeAgent" HeaderText="Type agent" />
                                        <asp:BoundField DataField="commentaireTypeAgent" HeaderText="Commentaire" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("typeAgent") %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer le type agent " + Eval("typeAgent") + "?" %>' >
                                                </asp:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
            </div>

            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
