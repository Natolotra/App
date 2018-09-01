<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/planificateur/MasterPlanificateur.Master" AutoEventWireup="true" CodeBehind="AutorisationDeVoyage.aspx.cs" Inherits="AppWeb.ihmActeur.planificateur.AutorisationDeVoyage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <div id="OneLayote">
        <div class="formContent3">
            <div class="grandTitre">
                &nbsp;&nbsp;Realiser un fiche de bord
            </div>

            <div class="contentFormulaires">
                <div id="OneLayoteLeft">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Information
                        </div>
                    </div>
                </div>
                <div id="OneLayoteRight">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Realiser un fiche de bord
                        </div>
                        <div class="contentFormulaires">
                            <div class="formulaire">
                                <table>
                                    <tr>
                                        <td>Nom agent technique:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomAgentTechnique" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td>Nom agent verificateur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomAgentVerificateur" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Prénom agent technique:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenomAT" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Prénom agent verificateur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenomAV" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Matricule voiture:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMatriculVoiture" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Nom chauffeur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextNomChauffeur" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                        
                        
                                    </tr>
                                    <tr>
                                        <td>Date prévue de depart:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDatePrevueDepart" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td> 
                                        <td>
                                        </td>
                                        <td>Prénom chauffeur:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextPrenomChauffeur" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                        
                                               
                                    </tr>
                                    <tr>
                                        <td>Itinéraire:
                                        </td>
                                        <td colspan="4">
                                            <asp:TextBox ID="TextItineraire" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date de depart prévue:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDateDepart" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="TextDateDepart_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="TextDateDepart" Format="dd MMMM yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                        </td>
                                        <td>Heure de depart prévue:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlHeure" runat="server">
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="ddlMinute" runat="server">
                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div id="divIndication" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                                onclick="btnAnnuler_Click" SkinID="btnValidation"/>
                                            <asp:Button ID="btnValide" runat="server" Text="Etablir une fiche de bord" 
                                                onclick="btnValide_Click" SkinID="btnValidation"/>
                                            
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfNumerosAV" runat="server" />
                            </div>
                            
                            <div class="divListe">
                                <asp:DropDownList ID="ddlTriAutorisation" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTriAutorisation_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheAutorisation" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheAutorisation" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheAutorisation_Click"/>
                                <asp:GridView ID="gvAutorisationVoyageNonFiche" runat="server" 
                                    AllowPaging="True" AutoGenerateColumns="False" 
                                    EnableModelValidation="True" 
                                    onpageindexchanging="gvAutorisationVoyageNonFiche_PageIndexChanging" 
                                    onrowcommand="gvAutorisationVoyageNonFiche_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numerosAV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numerosAV" HeaderText="N°" />
                                        <asp:BoundField DataField="numImmatricule" HeaderText="Vehicule" />
                                        <asp:BoundField DataField="chauffeur" HeaderText="Chauffeur" />
                        
                                        <asp:BoundField DataField="datePrevueDepart" DataFormatString="{0:dd MMMM yyyy}" 
                                            HeaderText="Date prevue de départ" />
                                        <asp:BoundField DataField="itineraire" HeaderText="Itineraire" />
                       
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <br />
                        </div>
                    </div>
                    <br />
                </div>

                <div class="clear"></div>
            </div>

            
        </div>
     </div>
</asp:Content>
