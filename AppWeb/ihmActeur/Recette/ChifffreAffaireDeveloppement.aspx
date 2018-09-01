<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/Recette/MasterRecette.Master" AutoEventWireup="true" CodeBehind="ChifffreAffaireDeveloppement.aspx.cs" Inherits="AppWeb.ihmActeur.Recette.ChifffreAffaireDeveloppement" %>
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
                &nbsp;&nbsp;Chiffre d'affaire développement
            </div>

            <div id="OneLayoteLeft50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Chiffre d'affaire développement 
                        <asp:Label ID="LabCA" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="formulaire">
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbCAParam" runat="server" AutoPostBack="True" 
                                        RepeatDirection="Horizontal" 
                                        onselectedindexchanged="rbCAParam_SelectedIndexChanged">
                                        <asp:ListItem Text="Par jour" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Par mois" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Par ans" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>

                        <table width="100%">
                            <tr>
                                <td class="titreTab" colspan="5">Date
                                </td>
                            </tr>
                            <tr>
                                <td>Date début:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextDateDebut" runat="server" AutoPostBack="True" 
                                        ontextchanged="TextDateDebut_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextDateDebut_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="TextDateDebut" Format="dd MMMM yyyy">
                                    </asp:CalendarExtender>
                                </td>
                                <td>&nbsp;&nbsp;
                                </td>
                                <td>
                                    Date fin:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextDateFin" runat="server" AutoPostBack="True" 
                                        ontextchanged="TextDateFin_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextDateFin_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="TextDateFin" Format="dd MMMM yyyy">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="titreTab" colspan="5">
                                    Chiffre d'affaire
                                </td>
                            </tr>
                            <tr>
                                <td>Montant total:
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="LabMontantTotalCA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">Montant total en lettre:
                                    <asp:Label ID="LabMontantTotalLettre" runat="server" Text="" Font-Bold="true" Font-Italic="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div id="OneLayoteRight50">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Liste
                    </div>
                    <div class="divListe">
                        <asp:GridView ID="gvCA" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" EnableModelValidation="True" 
                            onpageindexchanging="gvCA_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="numRecuAD" HeaderText="N°" />
                                <asp:BoundField DataField="dateRecu" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                <asp:BoundField DataField="montant" HeaderText="Montant" />
                                <asp:BoundField DataField="commentaire" HeaderText="Type" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="clear"></div>

        </div>
    </div>
</asp:Content>
