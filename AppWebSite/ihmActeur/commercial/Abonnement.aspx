<%@ Page Title="" Language="C#" MasterPageFile="~/ihmActeur/commercial/MasterCommerciale.Master" AutoEventWireup="true" CodeBehind="Abonnement.aspx.cs" Inherits="AppWeb.ihmActeur.commercial.Abonnement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="true">
    </asp:ScriptManager>

    <div id="OneLayote">
        <div class="formContent3">
            <asp:UpdatePanel ID="UpdatePanel_Titre" runat="server">
                <ContentTemplate>
                     <div class="grandTitre">
                        &nbsp;&nbsp;Abonnement&nbsp;&nbsp;<asp:Label ID="LabAbonnement" runat="server" Text=""></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
           
           

           
                
                <div class="formContent">
                    <div class="collapsePanelHeader">
                        &nbsp;&nbsp;Formulaire
                    </div>
                    <div class="formulaire">
                        <asp:UpdatePanel ID="UpdatePanel_Formulaire" runat="server">
                            <ContentTemplate>
                                <table> 
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RadioListeAbonnement" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="RadioListeAbonnement_SelectedIndexChanged" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Individu" Value="individu" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Société" Value="societe"></asp:ListItem>
                                                <asp:ListItem Text="Organisme" Value="organisme"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>      
                                </table>
                                <asp:Panel ID="PanelClient" runat="server">
                                    <table>
                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Individu
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td colspan="6" class="titreTab">
                                                Adresse
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Civilité:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCiviliteIndividu" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCiviliteIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCiviliteIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td colspan="4"></td>
                                            <td>Province:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProvinceIndividu" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlProvinceIndividu_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlProvinceIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlProvinceIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                Region:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegionIndividu" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlRegionIndividu_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlRegionIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlRegionIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomClient" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomClient_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextNomClient" ValidationGroup="groupeIndividu">*
                                                </asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenom" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>District:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDistrictIndividu" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlDistrictIndividu_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlDistrictIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlDistrictIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Commune:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCommuneIndividu" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlCommuneIndividu_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCommuneIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCommuneIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Né le:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextDateNaissanceIndividu" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextDateNaissanceIndividu_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="TextDateNaissanceIndividu" Format="dd MMMM yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td></td>
                                            <td>à:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextLieuNaissanceIndividu" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>Arrondissement:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlArrondissementIndividu" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlArrondissementIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlArrondissementIndividu" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextCinClient" runat="server"></asp:TextBox>
                                                
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextCinClient_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextCinClient" ValidationGroup="groupeIndividu">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>e-Mail:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailIndividu" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailIndividu_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailIndividu"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeIndividu">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            <td></td>
                                            <td>
                                                Adresse:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextAdresseClient" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextAdresseClient_RequiredFieldValidator" runat="server" 
                                                ErrorMessage="" ControlToValidate="TextAdresseClient" ValidationGroup="groupeIndividu">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>Quartier:</td>
                                            <td>
                                                <asp:TextBox ID="TextQuartierIndividu" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextQuartierIndividu_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="TextQuartierIndividu" ValidationGroup="groupeIndividu">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Profession:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextProfessionIndividu" runat="server"></asp:TextBox>
                                            </td>
                                            <td colspan="11">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tél fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextTelephoneFixeClient" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextTelephoneMobile" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td colspan="7"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PanelSociete" runat="server">
                                    <table>
                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Société
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td colspan="6" class="titreTab">
                                                Information sur le premier responsable de la société
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom société:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomSociete" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomSociete" ValidationGroup="groupeSociete">*
                                                </asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Secteur d'activité:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextSecteurSociete" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextSecteurSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextSecteurSociete" ValidationGroup="groupeSociete">
                                                *</asp:RequiredFieldValidator>
                                            </td>
                                            <td></td>
                                            <td>Civilité:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCiviliteRespSociete" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCiviliteRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCiviliteRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td colspan="3">
                                                <asp:Button ID="btnNouveauRespSociete" runat="server" 
                                                    Text="Nouveau responsable" onclick="btnNouveauRespSociete_Click"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>e-Mail société:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailSociete_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailSociete"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeSociete">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            <td>Reduction US:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbReductionUS" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="cbReductionUS_CheckedChanged"/>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomResponsableSociete" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomResponsableSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomResponsableSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenomRespSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Tél fixe société:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextTelephoneSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile société:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobileSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextCinRespSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextCinRespSociete_RequiredFieldValidator" 
                                                    runat="server" ControlToValidate="TextCinRespSociete" ErrorMessage="" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>e-Mail:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailRespSociete" runat="server"></asp:TextBox>

                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailRespSociete_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailRespSociete"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeSociete">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                        
                                        </tr>
                                        <tr>
                                            <td colspan="7"></td>
                                            <td>Tél fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFixeRespSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobileRespSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Adresse société
                                            </td>
                                            <td></td>
                                            <td colspan="6" class="titreTab">
                                                Adresse responsable société
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Province:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProvinceSociete" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlProvinceSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlProvinceSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlProvinceSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                Region:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegionSociete" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlRegionSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlRegionSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlRegionSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td></td>
                                            <td>Province:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlprovinceRespSociete" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddlprovinceRespSociete_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlprovinceRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlprovinceRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                Region:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegionRespSociete" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddlRegionRespSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlRegionRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlRegionRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>District:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDistrictSociete" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlDistrictSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlDistrictSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlDistrictSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Commune:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCommuneSociete" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlCommuneSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCommuneSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCommuneSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td></td>
                                            <td>District:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDistrictRespSociete" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddlDistrictRespSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlDistrictRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlDistrictRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Commune:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCommuneRespSociete" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddlCommuneRespSociete_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCommuneRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCommuneRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Arrondissement:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlArrondissementSociete" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlArrondissementSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlArrondissementSociete" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="4">
                                            </td>
                                            <td>Arrondissement:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlArrondissementRespSociete" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlArrondissementRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlArrondissementRespSociete" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Adresse société:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextAdresseSociete" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextAdresseSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextAdresseSociete" ValidationGroup="groupeSociete">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        
                                            <td>Quartier:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextQuartierSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextQuartierSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextQuartierSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td></td>
                                            <td>Adresse
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextAdresseRespSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextAdresseRespSociete_RequiredFieldValidator" 
                                                    runat="server" ControlToValidate="TextAdresseRespSociete" ErrorMessage="" ValidationGroup="groupeSociete">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>Quartier:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextQuartierRespSociete" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextQuartierRespSociete_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                        ControlToValidate="TextQuartierRespSociete" ValidationGroup="groupeSociete">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hfNumResponsableSociete" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="PanelOrganisme" runat="server">
                                    <table>
                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Organisme
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td colspan="6" class="titreTab">
                                                Information sur le premier responsable de l'organisme
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nom organisme:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Mail organisme:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailOrganisme_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailOrganisme"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeOrganisme">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            
                                            <td>
                                            </td>
                                            <td>Civilité:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCiviliteRespOrganisme" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ddlCiviliteRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlCiviliteRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td colspan="3">
                                                <asp:Button ID="btnNouveauRespOrganisme" runat="server" 
                                                    Text="Nouveau responsable" onclick="btnNouveauRespOrganisme_Click"/>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Tél fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFixeOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobileOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>Nom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextNomRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextNomRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextNomRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Prénom:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextPrenomRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                            </td>
                                            <td>CIN:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextCinRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="TextCinRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextCinRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>Mail:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMailRespOrganisme" runat="server"></asp:TextBox>
                                                    
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="TextMailRespOrganisme_RegularExpressionValidator" runat="server" 
                                                    ErrorMessage="" ControlToValidate="TextMailRespOrganisme"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="groupeOrganisme">*
                                                </asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                            </td>
                                            <td>Tél fixe:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFixeRespOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>Tél mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextMobileRespOrganisme" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td colspan="6" class="titreTab">
                                                Adresse organisme
                                            </td>
                                            <td></td>
                                            <td colspan="6" class="titreTab">
                                                Adresse responsable organisme
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>Province:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProvinceOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlProvinceOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlProvinceOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlProvinceOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlRegionOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRegionOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlRegionOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Province:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlprovinceRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlprovinceRespOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlprovinceRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlprovinceRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlRegionRespOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRegionRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlRegionRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td>District:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrictOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDistrictOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDistrictOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDistrictOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commune:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCommuneOrganisme" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlCommuneOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCommuneOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCommuneOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>District:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrictRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlDistrictRespOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlDistrictRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlDistrictRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Commune:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCommuneRespOrganisme" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlCommuneRespOrganisme_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlCommuneRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlCommuneRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td>Arrondissement:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArrondissementOrganisme" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlArrondissementOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlArrondissementOrganisme" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                        <td>Arrondissement:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArrondissementRespOrganisme" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlArrondissementRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlArrondissementRespOrganisme" ValidationGroup="gNull">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                        <tr>
                                        <td>Adresse:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAdresseOrganisme" runat="server"></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresseOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresseOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Quartier:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextQuartierOrganisme" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextQuartierOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextQuartierOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td>Adresse:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextAdresseRespOrganisme" runat="server" ></asp:TextBox>
                                                    
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextAdresseRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                            ControlToValidate="TextAdresseRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>Quartier:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextQuartierRespOrganisme" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="TextQuartierRespOrganisme_RequiredFieldValidator" runat="server" ErrorMessage=""
                                                ControlToValidate="TextQuartierRespOrganisme" ValidationGroup="groupeOrganisme">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    </table>
                                    <asp:HiddenField ID="hfNumResponsableOrganisme" runat="server" />
                                </asp:Panel>
                                <table>
                                        <tr>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary_FormaulaireAbonnement" runat="server" 
                                            ValidationGroup="groupAbonnement"/>
                                            <div id="divIndication" runat="server">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hfNumAbonnement" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" SkinID="btnValidation"
                                                onclick="btnNouveau_Click" />
                                           
                                            <asp:Button ID="btnModifier" runat="server" Text="Modifier" SkinID="btnValidation"
                                                onclick="btnModifier_Click" ValidationGroup="groupAbonnement"/>
                                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender_btnModifier" runat="server" 
                                                TargetControlID="btnModifier"
                                                ConfirmText="">
                                            </cc1:ConfirmButtonExtender>
                                             <asp:Button ID="btnValider" runat="server" Text="Abonner" SkinID="btnValidation"
                                                ValidationGroup="groupAbonnement" onclick="btnValider_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            
                    </div>
                </div>
                    
               

                <div class="clear">
                </div>
           
            <br />
            <div class="formContent">
                <div class="collapsePanelHeader">
                            &nbsp;&nbsp;Liste abonnement
                </div>
                <div class="divListe">
                    <asp:UpdatePanel ID="UpdatePanel_ListeAbonnement" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTriAbonnement" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlTriAbonnement_SelectedIndexChanged">
                                <asp:ListItem Text="N°" Value="numAbonnement"></asp:ListItem>
                                <asp:ListItem Text="Nom" Value="individu.nomIndividu"></asp:ListItem>
                                <asp:ListItem Text="Prénom" Value="individu.prenomIndividu"></asp:ListItem>
                                <asp:ListItem Text="Nom société" Value="nomSociete"></asp:ListItem>
                                <asp:ListItem Text="Nom organisme" Value="nomOrganisme"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextRechercheAbonnement" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRechercherAbonnement" runat="server" Text="Rechercher" 
                                onclick="btnRechercherAbonnement_Click" />
                            <asp:GridView ID="gvAbonnement" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                onpageindexchanging="gvAbonnement_PageIndexChanging" 
                                onrowcommand="gvAbonnement_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/CssStyle/images/select.png" CommandName="select"
                                                CommandArgument='<%# Eval("numAbonnement") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="numAbonnement" HeaderText="N°" />
                                    <asp:BoundField DataField="client" HeaderText="Individu/Société/Organisme" />
                                    <asp:BoundField DataField="adresse" HeaderText="Adresse" />
                                    <asp:BoundField DataField="contact" HeaderText="Contact" />
                                    <asp:BoundField DataField="respSociete" HeaderText="Resonsable société/Organisme" />
                                    <asp:BoundField DataField="respContact" HeaderText="Contact Responsable"/>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
