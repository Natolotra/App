<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ObservationVehicule.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.ObservationVehicule" %>
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
                Observation véhicule
            </div>
            
            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_FormulaireObservationVehicule" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td colspan="6" class="titreTab">
                                            Véhicule
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Matricule:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelMatricule" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td rowspan="7" colspan="3">
                                            <asp:Image ID="Image_Vehicule" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Marque:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelMarque" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Type:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelType" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>N°Série:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelSerie" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Couleur:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCouleur" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Source d'énergie:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelSouceEnergie" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    
                                    <tr>
                                        <td>Observation:
                                        </td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:HiddenField ID="hfNumVehicule" runat="server" />
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
                                            <asp:HiddenField ID="hfObservationVehicule" runat="server" />
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
                        &nbsp;&nbsp;Véhicule
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeVehicule" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriVehicule" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriVehicule_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="vehicule.numVehicule"></asp:ListItem>
                                    <asp:ListItem Text="Matricule" Value="vehicule.matriculeVehicule"></asp:ListItem>
                                    <asp:ListItem Text="Marque" Value="vehicule.marqueVehicule"></asp:ListItem>
                                    <asp:ListItem Text="Couleur" Value="vehicule.couleurVehicule"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheVehicule" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheVehicule" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheVehicule_Click" />
                                <asp:GridView ID="gvVehicule" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
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
                                        <asp:BoundField DataField="Individu" HeaderText="Proprietaire" />
                                        <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                        <asp:BoundField DataField="contact" HeaderText="Contact" />
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
                                 &nbsp;&nbsp;Observation 
                                <asp:Label ID="LabelVehiculeObservation" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="divListe">
                                <asp:GridView ID="gvObservationVehicule" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvObservationVehicule_PageIndexChanging" 
                                    onrowcommand="gvObservationVehicule_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numObservationVehicule") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="vehicule" HeaderText="Véhicule" />
                                        <asp:BoundField DataField="textObesvationVehicule" HeaderText="Observation" />
                                        <asp:BoundField DataField="dateObservation" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numObservationVehicule") %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer observation? \nVéhicule: " + Eval("vehicule")%>' >
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
                        &nbsp;&nbsp;Liste noire véhicule
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeNoireVehicule" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriListeNoireVehicule" runat="server" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriListeNoireVehicule_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="vehicule.numVehicule"></asp:ListItem>
                                    <asp:ListItem Text="Matricule" Value="vehicule.matriculeVehicule"></asp:ListItem>
                                    <asp:ListItem Text="Marque" Value="vehicule.marqueVehicule"></asp:ListItem>
                                    <asp:ListItem Text="Couleur" Value="vehicule.couleurVehicule"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheListeNoire" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheListeNoire" runat="server" Text="rechercher" 
                                    onclick="btnRechercheListeNoire_Click" />
                                <asp:GridView ID="gvListeNoireVehicule" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" EnableModelValidation="True" 
                                    onpageindexchanging="gvListeNoireVehicule_PageIndexChanging" 
                                    onrowcommand="gvListeNoireVehicule_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numVehicule") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="vehicule" HeaderText="Véhicule" />
                                        <asp:BoundField DataField="Individu" HeaderText="Proprietaire" />
                                        <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                        <asp:BoundField DataField="contact" HeaderText="Contact" />
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
