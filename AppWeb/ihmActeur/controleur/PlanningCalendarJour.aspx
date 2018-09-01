<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/controleur/MasterControleur.Master" AutoEventWireup="true" CodeBehind="PlanningCalendarJour.aspx.cs" Inherits="AppWeb.ihmActeur.controleur.PlanningCalendarJour" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
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
                &nbsp;&nbsp;Calendrier jour
            </div>
            <div class="formulaire">
                <div>
                        Date:
                        <asp:TextBox ID="TextDateCalendar" runat="server" AutoPostBack="True" ontextchanged="TextDateCalendar_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextDateCalendar_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="TextDateCalendar" Format="dd MMMM yyyy">
                        </cc1:CalendarExtender>&nbsp;&nbsp;
                        nombre de jours:
                    <asp:DropDownList ID="ddlNbJour" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlNbJour_SelectedIndexChanged" SkinID="ddlStandard">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;Itineraire:
                    <asp:DropDownList ID="ddlDebutItineraire" runat="server">
                    </asp:DropDownList>-
                    <asp:DropDownList ID="ddlFinItineraire" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlFinItineraire_SelectedIndexChanged">
                    </asp:DropDownList>
                     &nbsp;&nbsp;
                    Axe:
                    <asp:Label ID="LabAxe" runat="server" Text="" Font-Bold="true"></asp:Label>
                    <br />

                    <div>
                        <DayPilot:DayPilotCalendar ID="Calendar" runat="server"
                            BusinessBeginsHour="8"
                            BusinessEndsHour="20"
                            Days="1" 
                            ScrollPositionHour="8"

                            DataEndField="end"
                            DataStartField="start" 
                            DataTextField="name" 
                            DataValueField="id" 

                            HeaderDateFormat="dd MMMM yyyy" 
                    
                            ClientObjectName="dps" 

                            ScrollLabelsVisible="false"
                            DurationBarVisible="false"
                            BackColor="white"
                            NonBusinessBackColor="white"
                            EventBackColor="#638EDE"
                            EventBorderColor="#2951A5"
                            HourNameBackColor="#F3F3F9"
                            HourFontColor="#42658C"
                            HeaderFontColor="#42658C"
                            EventFontColor="white"
                            EventFontFamily="Tahoma,Verdana,Sans-serif"
                            BorderColor="#CED2CE"
                            CellBorderColor="#DEDFDE"
                            AllDayEventBackColor="#2951a5"
                            AllDayEventFontColor="#ffffff"
                            AllDayEventBorderColor="transparent"
                            HourBorderColor="#DEDFDE"
                            HourHalfBorderColor="#EBEDEB"
                            HourNameBorderColor="#DEDFDE"
                            
                            EventCorners="Rounded"
                            MoveBy="Top"
                            CellSelectColor="#cccccc"
                            ShowAllDayEventStartEnd="false"
                            LoadingLabelText="<img src='../CssStyle/images/load.gif' />"
                            LoadingLabelBackColor=""
                            EventHeaderVisible="true"
                            ShowEventStartEnd="false"
                            ContextMenuID="menuEvent"
                            oneventdoubleclick="Calendar_EventDoubleClick" 
                            EventDoubleClickHandling="PostBack" 
                            BubbleID="BubbleFicheBord" 
                            ColumnBubbleID="BubbleFicheBord" DataServerTagFields="color" 
                            EventMoveHandling="PostBack" onbeforeeventrender="Calendar_BeforeEventRender" 
                            oneventmove="Calendar_EventMove" Width="100%" 
                            oneventmenuclick="Calendar_EventMenuClick">

                        </DayPilot:DayPilotCalendar>
                        <DayPilot:DayPilotBubble ID="BubbleFicheBord" runat="server" BackColor="#50504E"
                            BorderColor="Black" HideAfter="500" ShowAfter="500" UseBorder="True" UseShadow="True"
                            Width="400" ShowLoadingLabel="True" ClientObjectName="bubble" 
                            Corners="Rounded" onrendercontent="BubbleFicheBord_RenderContent">
                        </DayPilot:DayPilotBubble>
                        <DayPilot:DayPilotMenu ID="menuEvent" runat="server" CssClassPrefix="menu_">
                            <DayPilot:MenuItem Action="PostBack" Text="Remplire fiche de bord" 
                                Command="fb" />
                            <DayPilot:MenuItem Action="PostBack" Text="Realiser un autorisation de départ" 
                                Command="ad" />
                        </DayPilot:DayPilotMenu>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
