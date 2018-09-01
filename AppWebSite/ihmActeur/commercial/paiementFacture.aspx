<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.Master" AutoEventWireup="true" CodeBehind="paiementFacture.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.paiementFacture" %>
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
                &nbsp;&nbsp;Paiement facture
            </div>

            <div id="OneLayoteLeft">
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Information
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_InformationProforma" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfNumFacture" runat="server" />
                                
                                <asp:Panel ID="Panel_Individu" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" class="titreTab">
                                                Individu
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelNomIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelPrenomIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelCINIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        <tr>
                                            <td>Adresse:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelAdresseIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                            
                                        </tr>
                                        <tr>
                                            <td>Téléphone fixe:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelFixeIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone mobile:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMobileIndividu" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_Societe" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" class="titreTab">
                                                Société
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>Nom société:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelNomSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Secteur d'activité:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelSecteurActiviteSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Adresse:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelAdresseSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mail:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMailSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Téléphone fixe:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelFixeSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone mobile:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMobileSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                         </tr>
                                        <tr>
                                            <td class="titreTab" colspan="2">
                                                Responsable
                                            </td>
                                         </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelNomRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                          </tr>
                                            
                                        <tr>
                                             <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelPrenomRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                           
                                        <tr>
                                            <td>
                                                CIN:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelCINRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Adresse:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelAdresseRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Téléphone fixe:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelFixeRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Téléphone mobile:
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelMobileRespSociete" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_Organisme" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" class="titreTab">
                                                    Organisme
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td>Nom organisme:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td>
                                                    Adresse:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelAdresseOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                             </tr>
                                            <tr>
                                                <td>Mail:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMailOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="titreTab">
                                                    Responsable
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNomRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                              </tr>
                                            <tr>
                                                <td>Prénom:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPrenomRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>CIN:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelCINRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>Adresse:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelAdresseRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td>
                                                    Téléphone fixe:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFixeRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Téléphone mobile:
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMobileRespOrganisme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
            </div>

            <div id="OneLayoteRight">
                <div class="formContent">
                    
                    <asp:UpdatePanel ID="UpdatePanel_TeteFacture" runat="server">
                        <ContentTemplate>
                            <div class="collapsePanelHeader">
                                &nbsp;&nbsp;Facture 
                                <asp:Label ID="LabNumFacture" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_FormulaireFacture" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Montant:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextMontantFacture" runat="server" ReadOnly="true"></asp:TextBox>Ar
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Date:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnValider" runat="server" Text="Payer" SkinID="btnValidation"
                                        ValidationGroup="groupeFacture" onclick="btnValider_Click"/>
                                    <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" SkinID="btnValidation"
                                        onclick="btnAnnuler_Click" />
                                </td>
                            </tr>
                         </table>
                    </div>
                </div>
            </div>

            <div class="clear">
            </div>
            <br />
            <div class="formContent">
                <asp:UpdatePanel ID="UpdatePanel_ListeFacture" runat="server">
                    <ContentTemplate>
                        <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Liste facture
                        </div>
                        <div class="divListe">
                            <asp:DropDownList ID="ddlTriFacture" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriFacture_SelectedIndexChanged">
                                <asp:ListItem Text="N°" Value="facture.numFacture"></asp:ListItem>
                                <asp:ListItem Text="Montant" Value="facture.montant"></asp:ListItem>
                                <asp:ListItem Text="Date" Value="dateFacturation"></asp:ListItem>
                                <asp:ListItem Text="Nom individu" Value="nomIndividu"></asp:ListItem>
                                <asp:ListItem Text="Prénom individu" Value="prenomIndividu"></asp:ListItem>
                                <asp:ListItem Text="CIN individu" Value="cinIndividu"></asp:ListItem>
                                <asp:ListItem Text="Société" Value="nomSociete"></asp:ListItem>
                                <asp:ListItem Text="Adresse société" Value="adresseSociete"></asp:ListItem>
                                <asp:ListItem Text="Organisme" Value="nomOrganisme"></asp:ListItem>
                                <asp:ListItem Text="Adresse organisme" Value="adresseOrganisme"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheFacture" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercheFacture" runat="server" Text="Rechercher" 
                                onclick="btnRechercheFacture_Click" />
                            <asp:GridView ID="gvFacture" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                onpageindexchanging="gvFacture_PageIndexChanging" 
                                onrowcommand="gvFacture_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("numFacture") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="numFacture" HeaderText="N°" />
                                    <asp:BoundField DataField="montant" HeaderText="Montant" />
                                    <asp:BoundField DataField="dateFacturation" HeaderText="Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                    <asp:BoundField DataField="Individu" HeaderText="Individu" />
                                    <asp:BoundField DataField="societe" HeaderText="Société" />
                                    <asp:BoundField DataField="organisme" HeaderText="Organisme" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>
