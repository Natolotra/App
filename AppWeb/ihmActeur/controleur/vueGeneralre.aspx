<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/controleur/MasterControleur.Master" AutoEventWireup="true" CodeBehind="vueGeneralre.aspx.cs" Inherits="AppWeb.ihmActeur.controleur.vueGeneralre" %>
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
                &nbsp;&nbsp;Vue générale
            </div>
            <div class="formulaire">
                <div>
                    Date:
                    <asp:TextBox ID="TextDateCalendar" runat="server" AutoPostBack="True" 
                        ontextchanged="TextDateCalendar_TextChanged"></asp:TextBox>
                    <cc1:CalendarExtender ID="TextDateCalendar_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TextDateCalendar" Format="dd MMMM yyyy">
                    </cc1:CalendarExtender>&nbsp;&nbsp;
                    Heure:
                    <asp:DropDownList ID="ddlHeure" runat="server" 
                        onselectedindexchanged="ddlHeure_SelectedIndexChanged" AutoPostBack="True" SkinID="ddlStandard">
                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
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
                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    </asp:DropDownList>H&nbsp;&nbsp;
                    Itineraire:
                    <asp:DropDownList ID="ddlDebutItineraire" runat="server" 
                        onselectedindexchanged="ddlDebutItineraire_SelectedIndexChanged">
                    </asp:DropDownList>-
                    <asp:DropDownList ID="ddlFinItineraire" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFinItineraire_SelectedIndexChanged" >
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    Axe:
                    <asp:Label ID="LabAxe" runat="server" Text="" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="formulaire" id="contentDiv" runat="server">
                
            </div>
        </div>
    </div>
</asp:Content>
