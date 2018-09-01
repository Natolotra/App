<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ObservationAgent.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.ObservationAgent" %>
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
                Observation agent
            </div>
            
            
            <div id="OneLayoteFormulaireLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Matricule:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelMatriculeAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td rowspan="6">
                                            <asp:Image ID="ImageAgent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nom:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNomAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Prénom:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPrenomAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Adresse:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAdresseAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Contact:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelContactAgent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Observation:
                                        </td>
                                        <td></td>
                                        <td></td>
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
                                            <asp:HiddenField ID="hfObservationAgent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" 
                                                SkinID="btnValidation" onclick="btnNouveau_Click" />
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                SkinID="btnValidation" ValidationGroup="gObservation" 
                                                onclick="btnModifier_Click" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </asp:ConfirmButtonExtender>
                                            <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                SkinID="btnValidation" ValidationGroup="gObservation" 
                                                onclick="btnValider_Click" />
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
                        &nbsp;&nbsp;Agent
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeAgent" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriAgent" runat="server" 
                                    onselectedindexchanged="ddlTriAgent_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                    <asp:ListItem Text="Matricule" Value="agent.matriculeAgent"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="agent.nomAgent"></asp:ListItem>
                                    <asp:ListItem Text="Prénom" Value="agent.prenomAgent"></asp:ListItem>
                                    <asp:ListItem Text="Type agent" Value="agent.typeAgent"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheAgent" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheAgent" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheAgent_Click" />
                                <asp:GridView ID="gvAgent" runat="server" AllowPaging="True" 
                                    onpageindexchanging="gvAgent_PageIndexChanging" 
                                    onrowcommand="gvAgent_RowCommand" AutoGenerateColumns="False" 
                                    EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("matriculeAgent") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="matriculeAgent" HeaderText="Matricule" />
                                        <asp:BoundField DataField="agent" HeaderText="Agent" />
                                        <asp:BoundField DataField="type" HeaderText="Type agent" />
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
                                &nbsp;&nbsp;Observation <asp:Label ID="LabelNomAgentObservation" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="divListe">
                                <asp:GridView ID="gvObservationAgent" runat="server" AllowPaging="True" 
                                    onpageindexchanging="gvObservationAgent_PageIndexChanging" 
                                    onrowcommand="gvObservationAgent_RowCommand" AutoGenerateColumns="False" 
                                    EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("numObservation") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="agent" HeaderText="Agent" />
                                        <asp:BoundField DataField="textObesvation" HeaderText="Observation" />
                                        <asp:BoundField DataField="dateObservation" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                    CommandArgument='<%# Eval("numObservation") %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                    TargetControlID="ibDelete"
                                                    ConfirmText='<%# "Voulez vous vraiment supprimer observation? \nAgent: " + Eval("agent")%>' >
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
                        &nbsp;&nbsp;Liste noire agent
                    </div>
                    <div class="divListe">
                        <asp:UpdatePanel ID="UpdatePanel_ListeNoireAgent" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlTriListeNoireAgent" runat="server" 
                                    onselectedindexchanged="ddlTriListeNoireAgent_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                    <asp:ListItem Text="Matricule" Value="agent.matriculeAgent"></asp:ListItem>
                                    <asp:ListItem Text="Nom" Value="agent.nomAgent"></asp:ListItem>
                                    <asp:ListItem Text="Prénom" Value="agent.prenomAgent"></asp:ListItem>
                                    <asp:ListItem Text="Type agent" Value="agent.typeAgent"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextRechercheListeNoireAgent" runat="server"></asp:TextBox>
                                <asp:Button ID="btnRechercheListeNoireAgent" runat="server" Text="Rechercher" 
                                    onclick="btnRechercheListeNoireAgent_Click" />
                                <asp:GridView ID="gvListeNoireAgent" runat="server" AllowPaging="True" 
                                    onpageindexchanging="gvListeNoireAgent_PageIndexChanging" 
                                    onrowcommand="gvListeNoireAgent_RowCommand" AutoGenerateColumns="False" 
                                    EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                    CommandArgument='<%# Eval("matriculeAgent") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="matriculeAgent" HeaderText="Matricule" />
                                        <asp:BoundField DataField="agent" HeaderText="Agent" />
                                        <asp:BoundField DataField="type" HeaderText="Type agent" />
                                        <asp:BoundField DataField="contact" HeaderText="Contact" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="clear"></div>


        </div>
    </div>
</asp:Content>
