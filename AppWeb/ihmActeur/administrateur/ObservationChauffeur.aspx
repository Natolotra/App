<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ObservationChauffeur.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.ObservationChauffeur" %>
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
                Observation chauffeur
            </div>
            
            
            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_ChauffeurFormulaire" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td class="titreTab" colspan="6">
                                            Chauffeur
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nom:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNomChauffeur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="6" colspan="3">
                                            <asp:Image ID="Image_Chauffeur" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Prénom
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPrenomChauffeur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Adresse:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAdresseChauffeur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                        
                                    </tr>
                                    <tr>
                                        <td>CIN:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCINAdresseChauffeur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Téléphone:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTelephone" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Observation:
                                        </td>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan="6">
                                            <asp:HiddenField ID="hfIdChauffeur" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td colspan="6">
                                            <asp:TextBox ID="TextObservation" runat="server" TextMode="MultiLine" Width="500px" Height="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="TextObservation_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextObservation" ValidationGroup="gObservation">*</asp:RequiredFieldValidator>
                                        </td>
                                        
                                    </tr>
                                   <tr>
                                        <td colspan="6">
                                            <asp:RadioButtonList ID="rbAvertissement" runat="server" Font-Bold="True" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Normal" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Avertissement" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Liste noire" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                   </tr>
                                    
                                    <tr>
                                        <td colspan="6">
                                            <div id="divIndication" runat="server">
                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary_FormulaireObservationChauffeur" runat="server" 
                                            ValidationGroup="gObservation" />
                                            <asp:HiddenField ID="hfObservationChauffeur" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveau_Click"/>
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                SkinID="btnValidation" ValidationGroup="gObservation" 
                                                onclick="btnModifier_Click"/>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                SkinID="btnValidation" ValidationGroup="gObservation" 
                                                onclick="btnValider_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Chauffeur
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeChauffeur" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriChauffeur" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriChauffeur_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="chauffeur.idChauffeur"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="chauffeur.nomChauffeur"></asp:ListItem>
                                    <asp:ListItem Text="Prénom" Value="chauffeur.prenomChauffeur"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheChauffeur" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheChauffeur" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheChauffeur_Click" />
                                <asp:GridView ID="gvChauffeur" runat="server" AutoGenerateColumns="False" 
                                    AllowPaging="True" onpageindexchanging="gvChauffeur_PageIndexChanging" 
                                    onrowcommand="gvChauffeur_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("idChauffeur") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nomChauffeur" HeaderText="Nom" />
                                        <asp:BoundField DataField="prenomChauffeur" HeaderText="Prénom" />
                                        <asp:BoundField DataField="adresseChauffeur" HeaderText="Adresse" />
                                        <asp:BoundField DataField="cinChauffeur" HeaderText="CIN" />
                                        
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>                        
            
            <div id="OneLayoteListeRight">
                 <div class="formContent">
                     <asp:UpdatePanel ID="UpdatePanel_Observation" runat="server">
                        <ContentTemplate>
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;Observation <asp:Label ID="LabelNomChauffeurObservation" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="divListe">
                                <asp:GridView ID="gvObservationChauffeur" runat="server" 
                                    AutoGenerateColumns="False" AllowPaging="True" 
                                    onpageindexchanging="gvObservationChauffeur_PageIndexChanging" 
                                    onrowcommand="gvObservationChauffeur_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numObservation") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="chauffeur" HeaderText="Chauffeur" />
                                        <asp:BoundField DataField="textObesvation" HeaderText="Observation" />
                                        <asp:BoundField DataField="dateObservation" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numObservation") %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer observation? \nChauffeur: " + Eval("chauffeur")%>' >
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
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste noire chauffeur
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeNoire" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriListeNoire" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriListeNoire_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="chauffeur.idChauffeur"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="chauffeur.nomChauffeur"></asp:ListItem>
                                    <asp:ListItem Text="Prénom" Value="chauffeur.prenomChauffeur"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheListeNoire" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheListeNoire" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheListeNoire_Click" />
                                <asp:GridView ID="gvListeNoire" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvListeNoire_PageIndexChanging" 
                                    onrowcommand="gvListeNoire_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("idChauffeur") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nomChauffeur" HeaderText="Nom" />
                                        <asp:BoundField DataField="prenomChauffeur" HeaderText="Prénom" />
                                        <asp:BoundField DataField="adresseChauffeur" HeaderText="Adresse" />
                                        <asp:BoundField DataField="cinChauffeur" HeaderText="CIN" />
                                        
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       
                    </div>
                </div>
            </div>
            
            <div class="clear">
            </div>      
            
        </div>
    </div>
</asp:Content>
