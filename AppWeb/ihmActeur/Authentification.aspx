<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/MasterCNT.Master" AutoEventWireup="true" CodeBehind="Authentification.aspx.cs" Inherits="AppWeb.ihmActeur.Authentification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="OneLayote">
       <div class="formContent3">
            <div class="grandTitre">
                &nbsp;&nbsp;CONFÉDÉRATION NATIONALE DE TRANSPORT
            </div>
            
                
                <div id="OneLayoteLeft">
                    <div class="formContent">
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Authentification
                        </div>
                        <div class="formulaire">
                            <table>
                                <tr>
                                    <td>Login:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextLogin" runat="server"></asp:TextBox>
                                        <asp:Label ID="LabLogin" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td rowspan="4">
                                        <img src="../CssStyle/images/password.png" alt="image password"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mot de passe:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextMotDePasse" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:Label ID="LabMotDePasse" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="divIndication" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnConnection" runat="server" Text="Connexion" 
                                            onclick="btnConnection_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="OneLayoteRight">
                    
                </div>
                
                <div class="clear"></div>
            
           
        </div>
    </div>
</asp:Content>
