<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/administrateur/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="RouteNationale.aspx.cs" Inherits="AppWeb.ihmActeur.administrateur.RouteNationale" %>
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
                    &nbsp;&nbsp;Route nationale
                </div>
                

                <div id="OneLayoteFormulaireLeft">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Formulaire
                        </div>
                        <div class="formulaire">
                            <asp:UpdatePanel ID="UpdatePanel_FormulaireRN" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>Route nationale:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextRN" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextRN_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextRN" ValidationGroup="groupeRN">*
                                                </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                            </td>
                                            <td>Distance:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextDistance" runat="server" Width="120"></asp:TextBox>
                                                Km
                                                <asp:FilteredTextBoxExtender
                                                    ID="TextDistance_FilteredTextBoxExtender"
                                                    runat="server" 
                                                    TargetControlID="TextDistance"
                                                    FilterType="Numbers"
                                                    ValidChars="" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextDistance_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextDistance" ValidationGroup="groupeRN">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:ValidationSummary ID="RN_ValidationSummary" runat="server" 
                                                    ValidationGroup="groupeRN"/>
                                                <div id="divIndication" runat="server"></div>
                                            </td>
                                        </tr>

                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAnnuler" runat="server" Text="Nouveau" 
                                                    onclick="btnAnnuler_Click" SkinId="btnValidation"/>
                                                <asp:Button ID="btnModifier" runat="server" Text="Modifier" 
                                                    onclick="btnModifier_Click" SkinId="btnValidation"/>
                                                <asp:ConfirmButtonExtender ID="btnModifier_ConfirmButtonExtender" runat="server" 
                                                    TargetControlID="btnModifier"
                                                    ConfirmText="">
                                                </asp:ConfirmButtonExtender>
                                                <asp:Button ID="btnValider" runat="server" Text="Valider" 
                                                    ValidationGroup="groupeRN" onclick="btnValider_Click" SkinId="btnValidation"/>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                    
                            
                        </div>

                        <div class="formulaire">
                            <div class="formContent2">
                                 <asp:UpdatePanel ID="UpdatePanelListeVilleRN" runat="server">
                                    <ContentTemplate>
                                        <div class="titreLG">
                                            &nbsp;&nbsp;Liste villes de la route nationale 
                                            <asp:Label ID="LabRN" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="divListe">
                                            <asp:DropDownList ID="ddlTriVilleRN" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTriVilleRN_SelectedIndexChanged">
                                                <asp:ListItem Text="N°" Value="ville.numVille"></asp:ListItem>
                                                <asp:ListItem Text="Ville" Value="nomVille"></asp:ListItem>
                                                <asp:ListItem Text="Region" Value="nomRegion"></asp:ListItem>
                                                <asp:ListItem Text="Province" Value="nomProvince"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TextRechercheVilleRN" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnRechercheVilleRN" runat="server" Text="Rechercher" 
                                                onclick="btnRechercheVilleRN_Click" />
                                            <asp:GridView ID="gvListeVilleRN" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                                onpageindexchanging="gvListeVilleRN_PageIndexChanging" 
                                                onrowcommand="gvListeVilleRN_RowCommand" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                                CommandArgument='<%# Eval("numVille") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nomVille" HeaderText="Ville" />
                                                    <asp:BoundField DataField="nomRegion" HeaderText="Region" />
                                                    <asp:BoundField DataField="nomProvince" HeaderText="Province" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="formulaire">
                            <div class="formContent2">
                                <div class="titreLG">
                                    &nbsp;&nbsp;Liste villes
                                </div>
                                <div class="divListe">
                                    <asp:UpdatePanel ID="UpdatePanelListeVille" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlTriVille" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTriVille_SelectedIndexChanged">
                                                <asp:ListItem Text="N°" Value="ville.numVille"></asp:ListItem>
                                                <asp:ListItem Text="Ville" Value="nomVille"></asp:ListItem>
                                                <asp:ListItem Text="Region" Value="nomRegion"></asp:ListItem>
                                                <asp:ListItem Text="Province" Value="nomProvince"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TextRechercheVille" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnRechercheVille" runat="server" Text="Rechercher" 
                                                onclick="btnRechercheVille_Click" />
                                            <asp:GridView ID="gvListeVille" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                                onpageindexchanging="gvListeVille_PageIndexChanging" 
                                                onrowcommand="gvListeVille_RowCommand" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                                CommandArgument='<%# Eval("numVille") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nomVille" HeaderText="Ville" />
                                                    <asp:BoundField DataField="nomRegion" HeaderText="Region" />
                                                    <asp:BoundField DataField="nomProvince" HeaderText="Province" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    
                </div>

                <div id="OneLayoteListeRight">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Liste route nationale
                        </div>
                        <div class="divListe">
                                          
                            <asp:UpdatePanel ID="UpdatePanel_ListRN" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTriRN" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTriRN_SelectedIndexChanged">
                                        <asp:ListItem Text="Route nationale" Value="routeNationale"></asp:ListItem>
                                        <asp:ListItem Text="Distance" Value="distanceRN"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextRechercheRN" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnRechercheRN" runat="server" Text="Rechercher" 
                                        onclick="btnRechercheRN_Click" />
                                    <asp:GridView ID="gvRN" runat="server" AllowPaging="True" 
                                        onpageindexchanging="gvRN_PageIndexChanging" onrowcommand="gvRN_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                        CommandArgument='<%# Eval("Route nationale") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/CssStyle/images/delete.png" CommandName="deleteV"
                                                        CommandArgument='<%# Eval("Route nationale") %>' />
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_ibDelete" runat="server" 
                                                            TargetControlID="ibDelete"
                                                            ConfirmText='<%# "Voulez vous vraiment supprimer la route nationale " + Eval("Route nationale") + "?" %>' >
                                                        </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
