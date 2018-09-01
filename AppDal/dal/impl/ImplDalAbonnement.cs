using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalAbonnement
    /// </summary>
    public class ImplDalAbonnement : IntfDalAbonnement
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalAbonnement()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalAbonnement(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        string IntfDalAbonnement.insertAbonnement(crlAbonnement Abonnement)
        {
            #region declaration
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            int nombreInsertion = 0;
            string numAbonnement = "";
            string strValueNumSociete = "";
            string strValueNumOrganisme = "";
            string strIndividu = "";
            #endregion

            #region implementation
            if (Abonnement != null)
            {
                Abonnement.NumAbonnement = serviceAbonnement.getNumAbonnement(Abonnement.agent.agence.SigleAgence);

                if (Abonnement.NumSociete != "")
                {
                    strValueNumSociete = "'" + Abonnement.NumSociete + "'";
                }
                else 
                {
                    strValueNumSociete = "NULL";
                }
                if (Abonnement.NumOrganisme != "")
                {
                    strValueNumOrganisme = "'" + Abonnement.NumOrganisme + "'";
                }
                else 
                {
                    strValueNumOrganisme = "NULL";
                }
                if (Abonnement.NumIndividu != "")
                {
                    strIndividu = "'" + Abonnement.NumIndividu + "'";
                }
                else
                {
                    strIndividu = "NULL";
                }

                this.strCommande = "INSERT INTO `abonnement` (`numAbonnement`,`numIndividu`,`matriculeAgent`,`numSociete`,`numOrganisme`,`dateAbonnement`,";
                this.strCommande += " `imageAbonner`) VALUES ('" + Abonnement.NumAbonnement + "'," + strIndividu + ",";
                this.strCommande += " '" + Abonnement.MatriculeAgent + "'," + strValueNumSociete + "," + strValueNumOrganisme + ",'" + Abonnement.DateAbonnement.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " '" + Abonnement.ImageAbonner + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numAbonnement = Abonnement.NumAbonnement;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAbonnement;
        }

        string IntfDalAbonnement.insertAbonnementAll(crlAbonnement Abonnement)
        {
            #region declaration
            string numAbonnement = "";
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            if (Abonnement != null)
            {
                if (Abonnement.agent != null)
                {
                    #region client
                    if (Abonnement.individu != null)
                    {
                        Abonnement.individu.NumIndividu = serviceIndividu.isIndividu(Abonnement.individu);
                        if (Abonnement.individu.NumIndividu != "")
                        {
                            serviceIndividu.updateIndividu(Abonnement.individu);
                            Abonnement.NumIndividu = Abonnement.individu.NumIndividu;
                        }
                        else
                        {
                            Abonnement.individu.NumIndividu = serviceIndividu.insertIndividu(Abonnement.individu, Abonnement.agent.agence.SigleAgence);
                            if (Abonnement.individu.NumIndividu != "")
                            {
                                Abonnement.NumIndividu = Abonnement.individu.NumIndividu;
                            }
                        }
                    }
                    #endregion

                    #region SOciete
                    if (Abonnement.societe != null) 
                    {
                        Abonnement.societe.NumSociete = serviceSociete.isSociete(Abonnement.societe);
                        if (Abonnement.societe.NumSociete != "")
                        {
                            serviceSociete.updateSociete(Abonnement.societe);
                            Abonnement.NumSociete = Abonnement.societe.NumSociete;
                        }
                        else 
                        {
                            Abonnement.societe.NumSociete = serviceSociete.insertSociete(Abonnement.societe, Abonnement.agent.agence.SigleAgence);
                            if (Abonnement.societe.NumSociete != "") 
                            {
                                Abonnement.NumSociete = Abonnement.societe.NumSociete;
                            }
                        }
                    }
                    #endregion

                    #region Organisme
                    if (Abonnement.organisme != null) 
                    {
                        Abonnement.organisme.NumOrganisme = serviceOrganisme.isOrganisme(Abonnement.organisme);
                        if (Abonnement.organisme.NumOrganisme != "")
                        {
                            serviceOrganisme.updateOrganisme(Abonnement.organisme);
                            Abonnement.NumOrganisme = Abonnement.organisme.NumOrganisme;
                        }
                        else 
                        {
                            Abonnement.organisme.NumOrganisme = serviceOrganisme.insertOrganisme(Abonnement.organisme, Abonnement.agent.agence.SigleAgence);
                            if (Abonnement.organisme.NumOrganisme != "") 
                            {
                                Abonnement.NumOrganisme = Abonnement.organisme.NumOrganisme;
                            }
                        }
                    }
                    #endregion

                    Abonnement.NumAbonnement = serviceAbonnement.isAbonnement(Abonnement);

                    if (Abonnement.NumAbonnement != "")
                    {
                        Abonnement = serviceAbonnement.selectAbonnement(Abonnement.NumAbonnement);
                    }
                    else
                    {
                        Abonnement.NumAbonnement = serviceAbonnement.insertAbonnement(Abonnement);
                    }

                    if (Abonnement.NumAbonnement != "")
                    {
                        numAbonnement = Abonnement.NumAbonnement;
                    }
                }

                
            }
            #endregion

            return numAbonnement;
        }

        bool IntfDalAbonnement.updateAbonnement(crlAbonnement Abonnement)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string strValueNumSociete = "";
            string strValueNumOrganisme = "";
            string strIndividu = "";
            #endregion

            #region implementation
            if (Abonnement != null)
            {
                if (Abonnement.NumSociete != "")
                {
                    strValueNumSociete = "'" + Abonnement.NumSociete + "'";
                }
                else
                {
                    strValueNumSociete = "NULL";
                }
                if (Abonnement.NumOrganisme != "")
                {
                    strValueNumOrganisme = "'" + Abonnement.NumOrganisme + "'";
                }
                else
                {
                    strValueNumOrganisme = "NULL";
                }
                if (Abonnement.NumIndividu != "")
                {
                    strIndividu = "'" + Abonnement.NumIndividu + "'";
                }
                else
                {
                    strIndividu = "NULL";
                }

                this.strCommande = "UPDATE `abonnement` SET `matriculeAgent`='" + Abonnement.MatriculeAgent + "',";
                this.strCommande += " `numIndividu`=" + strIndividu + ", `numOrganisme`=" + strValueNumOrganisme + ",";
                this.strCommande += " `numSociete`=" + strValueNumSociete + ", `dateAbonnement`='" + Abonnement.DateAbonnement.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `imageAbonner`='" + Abonnement.ImageAbonner + "'";
                this.strCommande += " WHERE `numAbonnement`='" + Abonnement.NumAbonnement + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if(nbUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        bool IntfDalAbonnement.updateAbonnementAll(crlAbonnement Abonnement)
        {
            #region declaration
            bool isUpdate = false;
            string numAbonnement = "";
            string numSociete = "";
            string numOrganisme = "";
            string numClient = "";
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            if (Abonnement != null)
            {
                if (Abonnement.agent != null)
                {
                    #region client
                    if (Abonnement.individu != null)
                    {
                        if (Abonnement.individu.NumIndividu != "")
                        {
                            numClient = serviceIndividu.isIndividu(Abonnement.individu);
                            if (numClient == "")
                            {
                                serviceIndividu.updateIndividu(Abonnement.individu);
                            }
                        }
                        else
                        {
                            Abonnement.individu.NumIndividu = serviceIndividu.isIndividu(Abonnement.individu);
                            if (Abonnement.individu.NumIndividu == "")
                            {
                                Abonnement.individu.NumIndividu = serviceIndividu.insertIndividu(Abonnement.individu, Abonnement.agent.agence.SigleAgence);
                            }
                            else
                            {
                                serviceIndividu.updateIndividu(Abonnement.individu);
                            }
                        }

                        if (Abonnement.individu.NumIndividu != "")
                        {
                            Abonnement.NumIndividu = Abonnement.individu.NumIndividu;
                        }
                    }
                    #endregion

                    #region SOciete
                    if (Abonnement.societe != null)
                    {
                        if (Abonnement.societe.NumSociete != "")
                        {
                            numSociete = serviceSociete.isSociete(Abonnement.societe);
                            if (numSociete == "")
                            {
                                serviceSociete.updateSociete(Abonnement.societe);
                            }
                        }
                        else
                        {
                            Abonnement.societe.NumSociete = serviceSociete.isSociete(Abonnement.societe);
                            if (Abonnement.societe.NumSociete == "")
                            {
                                Abonnement.societe.NumSociete = serviceSociete.insertSociete(Abonnement.societe, Abonnement.agent.agence.SigleAgence);
                            }
                            else
                            {
                                serviceSociete.updateSociete(Abonnement.societe);
                            }

                        }



                        if (Abonnement.societe.NumSociete != "")
                        {
                            Abonnement.NumSociete = Abonnement.societe.NumSociete;
                        }
                    }
                    
                    #endregion

                    #region Organisme
                    if (Abonnement.organisme != null)
                    {
                        if (Abonnement.organisme.NumOrganisme != "")
                        {
                            numOrganisme = serviceOrganisme.isOrganisme(Abonnement.organisme);
                            if (numOrganisme == "")
                            {
                                serviceOrganisme.updateOrganisme(Abonnement.organisme);
                            }
                            
                        }
                        else
                        {
                            Abonnement.organisme.NumOrganisme = serviceOrganisme.isOrganisme(Abonnement.organisme);
                            if (Abonnement.organisme.NumOrganisme == "")
                            {
                                Abonnement.organisme.NumOrganisme = serviceOrganisme.insertOrganisme(Abonnement.organisme, Abonnement.agent.agence.SigleAgence);
                            }
                            else
                            {
                                serviceOrganisme.updateOrganisme(Abonnement.organisme);
                            }
                        }

                        if (Abonnement.organisme.NumOrganisme != "")
                        {
                            Abonnement.NumOrganisme = Abonnement.organisme.NumOrganisme;
                        }
                       
                    }
                    #endregion

                    if (Abonnement.MatriculeAgent != "")
                    {
                        numAbonnement = serviceAbonnement.isAbonnement(Abonnement);

                        if (numAbonnement == "")
                        {
                            isUpdate = serviceAbonnement.updateAbonnement(Abonnement);
                        }
                    }

                }
            }
            #endregion

            return isUpdate;
        }

        bool IntfDalAbonnement.insertAssocAbonnementBillet(string numAbonnement, string numBillet)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsert = 0;
            #endregion

            #region implementation
            if (numAbonnement != "" && numBillet != "") 
            {
                this.strCommande = "INSERT INTO `assoabonnementbillet` (`numAbonnement`,`numBillet`)";
                this.strCommande += " VALUES ('" + numAbonnement + "','" + numBillet + "')";

                this.serviceConnectBase.openConnection();
                nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsert == 1) 
                {
                    isInsert = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalAbonnement.deleteAssocAbonnementBillet(string numAbonnement, string numBillet)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (numAbonnement != "" && numBillet != "")
            {
                this.strCommande = "DELETE FROM `assoabonnementbillet` WHERE";
                this.strCommande += " `numAbonnement`='" + numAbonnement + "' AND";
                this.strCommande += " `numBillet`='" + numBillet + "'";

                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1) 
                {
                    isDelete = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        crlAbonnement IntfDalAbonnement.selectAbonnement(string numAbonnement)
        {
            #region declaration
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            crlAbonnement Abonnement = null;
            #endregion

            #region implementation
            if(numAbonnement != "")
            {
                this.strCommande = "SELECT * FROM `abonnement` WHERE `numAbonnement`='" + numAbonnement + "' OR";
                this.strCommande += " `numIndividu`='" + numAbonnement + "' OR `numSociete`='" + numAbonnement + "' OR";
                this.strCommande += " `numOrganisme`='" + numAbonnement + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        Abonnement = new crlAbonnement();
                        if (this.reader.Read())
                        {
                            Abonnement.NumAbonnement = this.reader["numAbonnement"].ToString();
                            Abonnement.NumIndividu = this.reader["numIndividu"].ToString();
                            Abonnement.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            Abonnement.NumOrganisme = this.reader["numOrganisme"].ToString();
                            Abonnement.NumSociete = this.reader["numSociete"].ToString();
                            try
                            {
                                Abonnement.DateAbonnement = Convert.ToDateTime(this.reader["dateAbonnement"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            Abonnement.ImageAbonner = this.reader["imageAbonner"].ToString();
                            
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (Abonnement != null)
                {
                    if (Abonnement.NumIndividu != "")
                    {
                        Abonnement.individu = serviceIndividu.selectIndividu(Abonnement.NumIndividu);
                    }
                    if (Abonnement.MatriculeAgent != "")
                    {
                        Abonnement.agent = serviceAgent.selectAgent(Abonnement.MatriculeAgent);
                    }
                    if (Abonnement.NumOrganisme != "") 
                    {
                        Abonnement.organisme = serviceOrganisme.selectOrganisme(Abonnement.NumOrganisme);
                    }
                    if (Abonnement.NumSociete != "") 
                    {
                        Abonnement.societe = serviceSociete.selectSociete(Abonnement.NumSociete);
                    }
                }
            }
            #endregion

            return Abonnement;
        }

        string IntfDalAbonnement.isAbonnement(crlAbonnement abonnement)
        {
            #region declaration
            string numAbonnement = "";
            string strWhereSociete = "";
            string strWhereOrganisme = "";
            string strWhereClient = "";
            #endregion


            #region implementation
            if (abonnement != null)
            {
                if (abonnement.NumSociete != "") 
                {
                    strWhereSociete = "`numSociete`='" + abonnement.NumSociete + "'";
                }
                else if (abonnement.NumOrganisme != "") 
                {
                    strWhereOrganisme = "`numOrganisme`='" + abonnement.NumOrganisme + "'";
                }
                else if (abonnement.NumIndividu != "")
                {
                    strWhereClient = "`numIndividu`='" + abonnement.NumIndividu + "'";
                }

                this.strCommande = "SELECT * FROM `abonnement` WHERE " + strWhereSociete + " " + strWhereOrganisme + " " + strWhereClient + " AND";
                this.strCommande += " `numAbonnement` <> '" + abonnement.NumAbonnement + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numAbonnement = this.reader["numAbonnement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numAbonnement;
        }

        string IntfDalAbonnement.getNumAbonnement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numAbonnement = "00001";
            string[] tempNumAbonnement = null;
            string strDate = "AB" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT abonnement.numAbonnement AS maxNum FROM abonnement";
            this.strCommande += " WHERE abonnement.numAbonnement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumAbonnement = reader["maxNum"].ToString().ToString().Split('/');
                        numAbonnement = tempNumAbonnement[tempNumAbonnement.Length - 1];
                    }
                    numTemp = double.Parse(numAbonnement) + 1;
                    if (numTemp < 10)
                        numAbonnement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numAbonnement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numAbonnement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numAbonnement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numAbonnement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numAbonnement = strDate + "/" + sigleAgence + "/" + numAbonnement;
            #endregion

            return numAbonnement;
        }
        #endregion

        #region insert to grid
        void IntfDalAbonnement.insertToGridAbonnement(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalAbonnement serviceAbonnement = new ImplDalAbonnement();
            #endregion

            #region implementation

            this.strCommande = "SELECT abonnement.numAbonnement, abonnement.matriculeAgent,";
            this.strCommande += " abonnement.numSociete, abonnement.numOrganisme,";
            this.strCommande += " abonnement.numIndividu, abonnement.dateAbonnement,";
            this.strCommande += " abonnement.imageAbonner FROM abonnement";
            this.strCommande += " Left Join individu ON individu.numIndividu = abonnement.numIndividu";
            this.strCommande += " Left Join societe ON societe.numSociete = abonnement.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = abonnement.numOrganisme";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceAbonnement.getDataTableAbonnement(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalAbonnement.getDataTableAbonnement(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            string telephone = "";
            string mobile = "";
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAbonnement", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("respSociete", typeof(string));
            dataTable.Columns.Add("respContact", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numAbonnement"] = reader["numAbonnement"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (individu != null)
                        {
                            dr["client"] = individu.PrenomIndividu + " " + individu.NomIndividu;

                            dr["adresse"] = individu.Adresse;

                            dr["contact"] = individu.TelephoneFixeIndividu + " / " + individu.TelephoneMobileIndividu;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["client"] = societe.NomSociete;

                            dr["adresse"] = societe.AdresseSociete;

                            dr["contact"] = societe.TelephoneFixeSociete + " / " + societe.TelephoneMobileSociete;

                            if (societe.individuResponsable != null)
                            {
                                dr["respSociete"] = societe.individuResponsable.PrenomIndividu + " " + societe.individuResponsable.NomIndividu;

                                dr["respContact"] = societe.individuResponsable.TelephoneFixeIndividu + " / " + societe.individuResponsable.TelephoneMobileIndividu;
                            }
                        }
                        else if (organisme != null)
                        {
                            dr["client"] = organisme.NomOrganisme;

                            dr["adresse"] = organisme.AdresseOrganisme;

                            dr["contact"] = organisme.TelephoneFixeOrganisme + " / " + organisme.TelephoneMobileOrganisme;

                            if (organisme.individuResponsable != null)
                            {
                                dr["respSociete"] = organisme.individuResponsable.PrenomIndividu + " " + organisme.individuResponsable.NomIndividu;

                                dr["respContact"] = organisme.individuResponsable.TelephoneFixeIndividu + " / " + organisme.individuResponsable.TelephoneMobileIndividu;
                            }
                        }

                        individu = null;
                        societe = null;
                        organisme = null;
                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion
    }
}