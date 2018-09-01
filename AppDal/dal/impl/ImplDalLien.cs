using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace arch.dal.impl
{
    /// <summary>
    /// Implementation
    /// du service lien
    /// </summary>
    public class ImplDalLien : IntfDalLien
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalLien()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalLien(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region IntfDalLien Members

        crlLien IntfDalLien.selectLien(string numLien)
        {
            #region declaration
            crlLien lien = null;
            #endregion

            #region implementation
            if (numLien != "")
            {
                this.strCommande = "SELECT * FROM `lien` WHERE (`numLien`='" + numLien + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            lien = new crlLien();
                            lien.NumLien = this.reader["numLien"].ToString();
                            lien.ImageUrl = this.reader["imageUrl"].ToString();
                            try
                            {
                                lien.Niveau = int.Parse(this.reader["niveau"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            lien.NumLien = this.reader["numLien"].ToString();
                            lien.NumLienMere = this.reader["numLienMere"].ToString();
                            lien.TextLien = this.reader["textLien"].ToString();
                            lien.Url = this.reader["url"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                
                
            }
            #endregion

            return lien;
        }

        string IntfDalLien.insertLien(crlLien lien)
        {
            #region declaration
            string numLien = "";
            int nombreInsert = 0;
            IntfDalLien serviceLien = new ImplDalLien();
            string numLienMere = "NULL";
            #endregion

            #region implementation
            if (lien != null)
            {

                lien.NumLien = serviceLien.getNumLien();

                if (lien.NumLienMere != "") 
                {
                    numLienMere = "'" + lien.NumLienMere + "'";
                }

                this.strCommande = "INSERT INTO `lien` (`numLien`,`url`,`imageUrl`,`textLien`,";
                this.strCommande += " `niveau`,`numLienMere`) VALUES";
                this.strCommande += " ('" + lien.NumLien + "','" + lien.Url + "',";
                this.strCommande += " '" + lien.ImageUrl + "','" + lien.TextLien + "',";
                this.strCommande += " '" + lien.Niveau.ToString("0") + "'," + numLienMere + ")";

                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1)
                {
                    numLien = lien.NumLien;
                }
                this.serviceConnectBase.closeConnection();
               
            }
            #endregion

            return numLien;
        }

        bool IntfDalLien.updateLien(crlLien lien)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numLienMere = "NULL";
            #endregion

            #region implementation
            if (lien != null)
            {
                if (lien.NumLienMere != "")
                {
                    numLienMere = "'" + lien.NumLienMere + "'";
                }

                this.strCommande = "UPDATE `lien` SET `url`='" + lien.Url + "',";
                this.strCommande += " `imageUrl`='" + lien.ImageUrl + "', `textLien`='" + lien.TextLien + "',";
                this.strCommande += " `niveau`='" + lien.Niveau.ToString("0") + "',`numLienMere`=" + numLienMere;
                this.strCommande += " WHERE `numLien`='" + lien.NumLien + "'";

                this.serviceConnectBase.openConnection();
                nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nombreUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalLien.isLien(crlLien lien)
        {
            #region declaration
            string numLien = "";
            #endregion

            #region implementation
            if (lien != null) 
            {
                this.strCommande = "SELECT lien.numLien FROM `lien` WHERE";
                this.strCommande += " lien.url = '" + lien.Url + "' AND lien.niveau = '" + lien.Niveau + "' AND";
                this.strCommande += " lien.numLien <> '" + lien.NumLien + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numLien = this.reader["numLien"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numLien;
        }


        bool IntfDalLien.insertAssocAgentLien(string matriculeAgent, string numLien)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (matriculeAgent != "" && numLien != "")
            {
                this.strCommande = "INSERT INTO `assocagentlien` (`matriculeAgent`,`numLien`)";
                this.strCommande += " VALUES ('" + matriculeAgent + "','" + numLien + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalLien.deleteAssocAgentLien(string matriculeAgent, string numLien)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (matriculeAgent != "" && numLien != "")
            {
                this.strCommande = "DELETE FROM `assocagentlien` WHERE";
                this.strCommande += " `matriculeAgent`='" + matriculeAgent + "' AND";
                this.strCommande += " `numLien`='" + numLien + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalLien.insertAssocTypeAgentLien(string typeAgent, string numLien)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (typeAgent != "" && numLien != "")
            {
                this.strCommande = "INSERT INTO `assoctypeagentlien` (`typeAgent`,`numLien`)";
                this.strCommande += " VALUES ('" + typeAgent + "','" + numLien + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalLien.deleteAssocTypeAgentLien(string typeAgent, string numLien)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (typeAgent != "" && numLien != "")
            {
                this.strCommande = "DELETE FROM `assoctypeagentlien` WHERE";
                this.strCommande += " `typeAgent`='" + typeAgent + "' AND";
                this.strCommande += " `numLien`='" + numLien + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalLien.deleteAssocTypeAgentLien(string typeAgent)
        {
            #region declaration
            bool isDelete = false;
            int nbDelete = 0;
            #endregion

            #region implementation
            if (typeAgent != "")
            {
                this.strCommande = "DELETE FROM `assoctypeagentlien` WHERE";
                this.strCommande += " `typeAgent`='" + typeAgent + "'";

                this.serviceConnectBase.openConnection();
                nbDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nbDelete == 1)
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        string IntfDalLien.getNumLien()
        {
            #region declaration
            double numTemp = 0;
            string numLien = "0001";
            #endregion

            #region implementation
            this.strCommande = "SELECT lien.numLien AS maxNum FROM lien";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        numLien = reader["maxNum"].ToString();
                    }
                    numTemp = double.Parse(numLien) + 1;
                    if (numTemp < 10)
                        numLien = "000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numLien = "00" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numLien = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numLien = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return numLien;
        }

        List<crlLien> IntfDalLien.selectLien(int niveau, string matriculeAgent)
        {
            #region declaration
            List<crlLien> liens = null;
            crlLien lienTemp = null;
            IntfDalLien serviceLien = new ImplDalLien();
            #endregion

            #region implementation
            if (matriculeAgent != "") 
            {
                this.strCommande = "SELECT lien.numLien, lien.url, lien.imageUrl, lien.textLien,";
                this.strCommande += " lien.niveau, lien.numLienMere FROM lien";
                this.strCommande += " Inner Join assocagentlien ON assocagentlien.numLien = lien.numLien";
                this.strCommande += " WHERE assocagentlien.matriculeAgent LIKE '%" + matriculeAgent + "%' AND";
                this.strCommande += " lien.niveau = '" + niveau + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        liens = new List<crlLien>();
                        while (this.reader.Read()) 
                        {
                            lienTemp = new crlLien();
                            lienTemp.ImageUrl = this.reader["imageUrl"].ToString();
                            lienTemp.Niveau = int.Parse(this.reader["niveau"].ToString());
                            lienTemp.NumLien = this.reader["numLien"].ToString();
                            lienTemp.NumLienMere = this.reader["numLienMere"].ToString();
                            lienTemp.TextLien = this.reader["textLien"].ToString();
                            lienTemp.Url = this.reader["url"].ToString();

                            liens.Add(lienTemp);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (liens != null) 
                {
                    for (int i = 0; i < liens.Count; i++) 
                    {
                        liens[i].liens = serviceLien.selectLien(niveau + 1, liens[i].NumLien, matriculeAgent);
                    }
                }
            }
            #endregion

            return liens;
        }

        List<crlLien> IntfDalLien.selectLien(int niveau, string numLien, string matriculeAgent)
        {
            #region declaration
            List<crlLien> liens = null;
            crlLien lienTemp = null;
            #endregion

            #region implementation
            if (numLien != "" && matriculeAgent != "") 
            {
                this.strCommande = "SELECT lien.numLien, lien.url, lien.imageUrl, lien.textLien,";
                this.strCommande += " lien.niveau, lien.numLienMere FROM lien";
                this.strCommande += " Inner Join assocagentlien ON assocagentlien.numLien = lien.numLien";
                this.strCommande += " WHERE assocagentlien.matriculeAgent LIKE '%" + matriculeAgent + "%' AND";
                this.strCommande += " lien.niveau = '" + niveau + "' AND";
                this.strCommande += " lien.numLienMere = '" + numLien + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        liens = new List<crlLien>();
                        while (this.reader.Read())
                        {
                            lienTemp = new crlLien();
                            lienTemp.ImageUrl = this.reader["imageUrl"].ToString();
                            lienTemp.Niveau = int.Parse(this.reader["niveau"].ToString());
                            lienTemp.NumLien = this.reader["numLien"].ToString();
                            lienTemp.NumLienMere = this.reader["numLienMere"].ToString();
                            lienTemp.TextLien = this.reader["textLien"].ToString();
                            lienTemp.Url = this.reader["url"].ToString();

                            liens.Add(lienTemp);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return liens;
        }

        List<crlLien> IntfDalLien.selectAllLien(string matriculeAgent)
        {
            #region declaration
            List<crlLien> liens = null;
            crlLien lienTemp = null;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT lien.numLien, lien.url, lien.imageUrl, lien.textLien,";
                this.strCommande += " lien.niveau, lien.numLienMere FROM lien";
                this.strCommande += " Inner Join assocagentlien ON assocagentlien.numLien = lien.numLien";
                this.strCommande += " WHERE assocagentlien.matriculeAgent LIKE '%" + matriculeAgent + "%'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        liens = new List<crlLien>();
                        while (this.reader.Read())
                        {
                            lienTemp = new crlLien();
                            lienTemp.ImageUrl = this.reader["imageUrl"].ToString();
                            lienTemp.Niveau = int.Parse(this.reader["niveau"].ToString());
                            lienTemp.NumLien = this.reader["numLien"].ToString();
                            lienTemp.NumLienMere = this.reader["numLienMere"].ToString();
                            lienTemp.TextLien = this.reader["textLien"].ToString();
                            lienTemp.Url = this.reader["url"].ToString();

                            liens.Add(lienTemp);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                
            }
            #endregion

            return liens;
        }

        bool IntfDalLien.isPageAgent(string matriculeAgent, string numLien)
        {
            #region declaration
            bool isPageAgent = false;
            #endregion

            #region implementation
            if (matriculeAgent != "" && numLien != "")
            {
                this.strCommande = "SELECT * FROM `assocagentlien` WHERE";
                this.strCommande += " `matriculeAgent`='" + matriculeAgent + "' AND";
                this.strCommande += " `numLien`='" + numLien + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isPageAgent = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion


            return isPageAgent;
        }

        bool IntfDalLien.isPageTypeAgent(string typeAgent, string numLien)
        {
            #region declaration
            bool isPageAgent = false;
            #endregion

            #region implementation
            if (typeAgent != "" && numLien != "")
            {
                this.strCommande = "SELECT * FROM `assoctypeagentlien` WHERE";
                this.strCommande += " `typeAgent`='" + typeAgent + "' AND";
                this.strCommande += " `numLien`='" + numLien + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isPageAgent = true;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion


            return isPageAgent;
        }
        #endregion

        #region IntfDalLien Members


        HtmlGenericControl IntfDalLien.getLiLien(crlLien lien, string preUrl, string preUrlImage)
        {
            #region declaration
            HtmlGenericControl liLien = null;
            HtmlGenericControl aLien = null;
            HtmlGenericControl imgLien = null;
            HtmlGenericControl divLien = null;
            string strInnerHtml = "";
            #endregion

            #region implementation
            if (lien != null) 
            {
                liLien = new HtmlGenericControl("li");

                aLien = new HtmlGenericControl("a");
                aLien.Attributes.Add("href", preUrl + lien.Url);
                aLien.Attributes.Add("style", "height:22px;line-height:22px;");

                /*imgLien = new HtmlGenericControl("img");
                imgLien.Attributes.Add("src", preUrlImage + lien.ImageUrl);
                imgLien.Attributes.Add("alt", "");*/

                //divLien = new HtmlGenericControl("div");
                //divLien.InnerText = lien.TextLien;

                strInnerHtml = "<img src=\"" + preUrlImage + lien.ImageUrl + "\" alt=\"\"/>" + lien.TextLien;

                /*aLien.Controls.Add(imgLien);
                aLien.InnerText = lien.TextLien;*/
                aLien.InnerHtml = strInnerHtml;

                liLien.Controls.Add(aLien);
            }
            #endregion

            return liLien;
        }


        void IntfDalLien.getMenu(HtmlGenericControl ulLien, string matriculeAgent, string preUrl, string preUrlImage)
        {
            #region declaration
            List<crlLien> liens = null;
            IntfDalLien serviceLien = new ImplDalLien();
            HtmlGenericControl ulLienTemp = null;
            HtmlGenericControl liLien = null;
            HtmlGenericControl aLien = null;
            HtmlGenericControl imgLien = null;
            HtmlGenericControl divLien = null;
            string strInnerHtml = "";
            #endregion

            #region implementation
            if (ulLien != null)
            {
                liens = serviceLien.selectLien(0, matriculeAgent);

                if (liens != null)
                {
                    for (int i = 0; i < liens.Count; i++)
                    {
                        liLien = new HtmlGenericControl("li");
                        liLien.Attributes.Add("class", "topmenu");

                        aLien = new HtmlGenericControl("a");
                        aLien.Attributes.Add("href", preUrl + liens[i].Url);
                        aLien.Attributes.Add("style", "height:22px;line-height:22px;");

                        /*imgLien = new HtmlGenericControl("img");
                        imgLien.Attributes.Add("src", liens[i].ImageUrl);
                        imgLien.Attributes.Add("alt", "");*/

                        //divLien = new HtmlGenericControl("div");
                        //divLien.InnerText = liens[i].TextLien;

                        strInnerHtml = "<img src=\"" + preUrlImage + liens[i].ImageUrl + "\" alt=\"\"/>" + liens[i].TextLien;
                         

                        /*aLien.InnerText = liens[i].TextLien;
                        aLien.Controls.Add(imgLien);*/
                        
                        aLien.InnerHtml = strInnerHtml;

                        liLien.Controls.Add(aLien);

                        if (liens[i].liens != null)
                        {
                            ulLienTemp = new HtmlGenericControl("ul");
                            for (int j = 0; j < liens[i].liens.Count; j++)
                            {
                                ulLienTemp.Controls.Add(serviceLien.getLiLien(liens[i].liens[j], preUrl, preUrlImage));
                            }
                            liLien.Controls.Add(ulLienTemp);
                        }


                        ulLien.Controls.Add(liLien);
                    }
                }
            }
            #endregion

        }
        #endregion





        #region IntfDalLien Members


        

        #endregion

        #region IntfDalLien Members


        List<crlLien> IntfDalLien.selectAllLien(string matriculeAgent, string indicateurZone)
        {
            #region declaration
            List<crlLien> liens = null;
            crlLien lienTemp = null;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT lien.numLien, lien.url, lien.imageUrl, lien.textLien,";
                this.strCommande += " lien.niveau, lien.numLienMere FROM lien";
                this.strCommande += " Inner Join assocagentlien ON assocagentlien.numLien = lien.numLien";
                this.strCommande += " WHERE assocagentlien.matriculeAgent LIKE '%" + matriculeAgent + "%' AND";
                this.strCommande += " lien.indicateurZone LIKE '%" + indicateurZone + "%'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        liens = new List<crlLien>();
                        while (this.reader.Read())
                        {
                            lienTemp = new crlLien();
                            lienTemp.ImageUrl = this.reader["imageUrl"].ToString();
                            lienTemp.Niveau = int.Parse(this.reader["niveau"].ToString());
                            lienTemp.NumLien = this.reader["numLien"].ToString();
                            lienTemp.NumLienMere = this.reader["numLienMere"].ToString();
                            lienTemp.TextLien = this.reader["textLien"].ToString();
                            lienTemp.Url = this.reader["url"].ToString();

                            liens.Add(lienTemp);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();


            }
            #endregion

            return liens;
        }

        List<crlLien> IntfDalLien.selectLienM(int niveau, string matriculeAgent, string indicateurZone)
        {
            #region declaration
            List<crlLien> liens = null;
            crlLien lienTemp = null;
            IntfDalLien serviceLien = new ImplDalLien();
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT lien.numLien, lien.url, lien.imageUrl, lien.textLien,";
                this.strCommande += " lien.niveau, lien.numLienMere FROM lien";
                this.strCommande += " Inner Join assocagentlien ON assocagentlien.numLien = lien.numLien";
                this.strCommande += " WHERE assocagentlien.matriculeAgent LIKE '%" + matriculeAgent + "%' AND";
                this.strCommande += " lien.niveau = '" + niveau + "' AND";
                this.strCommande += " lien.indicateurZone LIKE '%" + indicateurZone + "%'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        liens = new List<crlLien>();
                        while (this.reader.Read())
                        {
                            lienTemp = new crlLien();
                            lienTemp.ImageUrl = this.reader["imageUrl"].ToString();
                            lienTemp.Niveau = int.Parse(this.reader["niveau"].ToString());
                            lienTemp.NumLien = this.reader["numLien"].ToString();
                            lienTemp.NumLienMere = this.reader["numLienMere"].ToString();
                            lienTemp.TextLien = this.reader["textLien"].ToString();
                            lienTemp.Url = this.reader["url"].ToString();

                            liens.Add(lienTemp);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (liens != null)
                {
                    for (int i = 0; i < liens.Count; i++)
                    {
                        liens[i].liens = serviceLien.selectLienE(niveau + 1, liens[i].NumLien, matriculeAgent, indicateurZone);
                    }
                }
            }
            #endregion

            return liens;
        }

        List<crlLien> IntfDalLien.selectLienE(int niveau, string numLien, string matriculeAgent, string indicateurZone)
        {
            #region declaration
            List<crlLien> liens = null;
            crlLien lienTemp = null;
            #endregion

            #region implementation
            if (numLien != "" && matriculeAgent != "")
            {
                this.strCommande = "SELECT lien.numLien, lien.url, lien.imageUrl, lien.textLien,";
                this.strCommande += " lien.niveau, lien.numLienMere FROM lien";
                this.strCommande += " Inner Join assocagentlien ON assocagentlien.numLien = lien.numLien";
                this.strCommande += " WHERE assocagentlien.matriculeAgent LIKE '%" + matriculeAgent + "%' AND";
                this.strCommande += " lien.niveau = '" + niveau + "' AND";
                this.strCommande += " lien.numLienMere = '" + numLien + "' AND";
                this.strCommande += " lien.indicateurZone LIKE '%" + indicateurZone + "%'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        liens = new List<crlLien>();
                        while (this.reader.Read())
                        {
                            lienTemp = new crlLien();
                            lienTemp.ImageUrl = this.reader["imageUrl"].ToString();
                            lienTemp.Niveau = int.Parse(this.reader["niveau"].ToString());
                            lienTemp.NumLien = this.reader["numLien"].ToString();
                            lienTemp.NumLienMere = this.reader["numLienMere"].ToString();
                            lienTemp.TextLien = this.reader["textLien"].ToString();
                            lienTemp.Url = this.reader["url"].ToString();

                            liens.Add(lienTemp);
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return liens;
        }

        

        void IntfDalLien.getMenu(Panel panLien, string matriculeAgent, string preUrl, string preUrlImage, string indicateurZone)
        {
            #region declaration
            List<crlLien> liens = null;
            IntfDalLien serviceLien = new ImplDalLien();
            HtmlGenericControl ulLienTemp = null;
            HtmlGenericControl liLien = null;
            HtmlGenericControl aLien = null;
            HtmlGenericControl imgLien = null;
            HtmlGenericControl divLien = null;
            string strInnerHtml = "";
            #endregion

            #region implementation
            if (panLien != null)
            {
                liens = serviceLien.selectLienM(0, matriculeAgent, indicateurZone);

                if (liens != null)
                {
                    panLien.Controls.Clear();
                    for (int i = 0; i < liens.Count; i++)
                    {
                        liLien = new HtmlGenericControl("li");
                        liLien.Attributes.Add("class", "topmenu");

                        aLien = new HtmlGenericControl("a");
                        aLien.Attributes.Add("href", preUrl + liens[i].Url);
                        aLien.Attributes.Add("style", "height:22px;line-height:22px;");

                        /*imgLien = new HtmlGenericControl("img");
                        imgLien.Attributes.Add("src", liens[i].ImageUrl);
                        imgLien.Attributes.Add("alt", "");*/

                        //divLien = new HtmlGenericControl("div");
                        //divLien.InnerText = liens[i].TextLien;

                        strInnerHtml = "<img src=\"" + preUrlImage + liens[i].ImageUrl + "\" alt=\"\"/>" + liens[i].TextLien;


                        /*aLien.InnerText = liens[i].TextLien;
                        aLien.Controls.Add(imgLien);*/

                        aLien.InnerHtml = strInnerHtml;

                        liLien.Controls.Add(aLien);

                        if (liens[i].liens != null)
                        {
                            ulLienTemp = new HtmlGenericControl("ul");
                            for (int j = 0; j < liens[i].liens.Count; j++)
                            {
                                ulLienTemp.Controls.Add(serviceLien.getLiLien(liens[i].liens[j], preUrl, preUrlImage));
                            }
                            liLien.Controls.Add(ulLienTemp);
                        }


                        panLien.Controls.Add(liLien);
                    }
                }
            }
            #endregion
        }

        #endregion
    }
}