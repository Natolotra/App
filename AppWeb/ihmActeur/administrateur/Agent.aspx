<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Agent.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.Agent" %>
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
                &nbsp;&nbsp;Agent
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
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Type agent:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeAgent" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTypeAgent_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlTypeAgent_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlTypeAgent" ValidationGroup="gAgent">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>Agence:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAgence" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlAgence_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ValidationGroup="gAgent" ControlToValidate="ddlAgence">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td rowspan="6">
                                            <asp:Image ID="imageAgent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Situation familiale:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSituationFamiliale" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlSituationFamiliale_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlSituationFamiliale" ValidationGroup="gAgent">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextNomAgent_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextNomAgent" ValidationGroup="gAgent">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Prénom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenomAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date de naissance:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateNaissanceAgent" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextDateNaissanceAgent_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateNaissanceAgent" Format="dd MMMM yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Lieu de naissance:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextLieuNaissanceAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>CIN:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCINAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextCINAgent_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextCINAgent" ValidationGroup="gAgent">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Adresse:
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="TextAdresse" runat="server" TextMode="MultiLine" 
                                                    Width="145px" Height="40px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresse_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresse" ValidationGroup="gAgent">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Téléphone fixe:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TexttelephoneFixeAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Téléphone portable:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TexttelephonePortableAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Image:
                                        </td>
                                        <td colspan="5">
                                            <asp:FileUpload ID="FileUpload_ImageAgent" runat="server" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Login:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextLoginAgent" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Mot de passe:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMotDePasseAgent" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <asp:ValidationSummary ID="ValidationSummary_Agent" runat="server" ValidationGroup="gAgent"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:HiddenField ID="hfMatriculeAgent" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="Nouveau" onclick="btnNew_Click" 
                                        SkinID="btnValidation"/>
                                    <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                        ValidationGroup="gAgent" onclick="btnModifier_Click" SkinID="btnValidation"/>
                                    <asp:ConfirmButtonExtender runat="server" TargetControlID="btnModifier"
                                        ID="btnModifier_ConfirmButtonExtender" ConfirmText="Voulez vous vraiment modifier?"></asp:ConfirmButtonExtender>
                                    <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                        ValidationGroup="gAgent" onclick="btnValider_Click" SkinID="btnValidation"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

           

            <div class="clear">
                <br />
            </div>

            <div class="formContent">
                <div class="collapsePanelHeader">
                    &nbsp;&nbsp;Liste agents
                </div>
                <div class="divListe">
                    <asp:UpdatePanel ID="UpdatePanel_Liste" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTriAgent" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriAgent_SelectedIndexChanged">
                                <asp:ListItem Text="Matricule" Value="agent.matriculeAgent"></asp:ListItem>
                                <asp:ListItem Text="Nom" Value="nomAgent"></asp:ListItem>
                                <asp:ListItem Text="Prénom" Value="prenomAgent"></asp:ListItem>
                                <asp:ListItem Text="type" Value="typeAgent"></asp:ListItem>
                                <asp:ListItem Text="Adresse" Value="adresseAgent"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheAgent" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheAgent" runat="server" Text="Rechercher" 
                                onclick="btnRechercheAgent_Click" />
                            <asp:GridView ID="gvAgent" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                onpageindexchanging="gvAgent_PageIndexChanging" 
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
                                    <asp:BoundField DataField="type" HeaderText="Type" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        
                </div>
            </div>

        </div>
    </div>
</asp:Content>
