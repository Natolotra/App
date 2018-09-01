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
    /// Implementation du service recuAD
    /// </summary>
    public class ImplDalRecuAD : IntfDalRecuAD
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRecuAD(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalRecuAD() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion


        #region IntfDalFacture Members
        string IntfDalRecuAD.insertRecuAD(crlRecuAD RecuAD)
        {
            #region declaration
            IntfDalRecuAD serviceRecuAD = new ImplDalRecuAD();
            int nombreInsertion = 0;
            string numRecu = "";

            string numPrelevement = "NULL";
            string numFacture = "NULL";
            #endregion

            #region implementation
            if (RecuAD != null)
            {
                if (RecuAD.NumPrelevement != "")
                {
                    numPrelevement = "'" + RecuAD.NumPrelevement + "'";
                }
                if (RecuAD.NumFacture != "")
                {
                    numFacture = "'" + RecuAD.NumFacture + "'";
                }

                RecuAD.NumRecuAD = serviceRecuAD.getNumRecuAD(RecuAD.agent.agence.SigleAgence);
                this.strCommande = "INSERT INTO `recuad` (`numRecuAD`,`numPrelevement`,`matriculeAgent`,`libele`,`montant`,`dateRecu`,`numFacture`)";
                this.strCommande += " VALUES ('" + RecuAD.NumRecuAD +"', " + numPrelevement + ", '" + RecuAD.MatriculeAgent + "',";
                this.strCommande += " '" + RecuAD.Libele + "','" + RecuAD.Montant + "','" + RecuAD.Date.ToString("yyyy-MM-dd") + "'," + numFacture + ")";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numRecu = RecuAD.NumRecuAD;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numRecu;
        }

        string IntfDalRecuAD.insertRecuADAssociation(crlRecuAD RecuAD, string numAutorisationDepart)
        {
            #region declaration
            string numRecuAD = "";
            IntfDalRecuAD serviceRecuAD = new ImplDalRecuAD();
            #endregion

            #region implementation
            if (RecuAD != null && numAutorisationDepart != "")
            {
                RecuAD.NumRecuAD = serviceRecuAD.insertRecuAD(RecuAD);

                if (RecuAD.NumRecuAD != "")
                {
                    if (serviceRecuAD.insertAssociationRecuADAD(RecuAD.NumRecuAD, numAutorisationDepart))
                    {
                        numRecuAD = RecuAD.NumRecuAD;
                    }
                }
            }
            #endregion

            return numRecuAD;
        }

        bool IntfDalRecuAD.insertAssociationRecuADAD(string numRecuAD, string numerosnumAutorisationDepart)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numerosnumAutorisationDepart != "" && numRecuAD != "")
            {
                this.strCommande = "INSERT INTO `asociationautorisationdepartrecu` (`numRecuAD`,`numAutorisationDepart`)";
                this.strCommande += " VALUES ('" + numRecuAD + "','" + numerosnumAutorisationDepart + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalRecuAD.deleteRecuAD(crlRecuAD RecuAD)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (RecuAD != null)
            {
                if (RecuAD.NumRecuAD != "")
                {
                    this.strCommande = "DELETE FROM `recuad` WHERE (`numRecuAD` = '" + RecuAD.NumRecuAD + "')";
                    this.serviceConnectBase.openConnection();
                    nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreDelete == 1)
                        isDelete = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isDelete;
        }

        bool IntfDalRecuAD.deleteRecuAD(string numRecuAD)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (numRecuAD != "")
            {
                this.strCommande = "DELETE FROM `recuad` WHERE (`numRecuAD` = '" + numRecuAD + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalRecuAD.updateRecuAD(crlRecuAD RecuAD)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;

            string numPrelevement = "NULL";
            string numFacture = "NULL";
            #endregion

            #region implementation
            if (RecuAD != null)
            {
                if (RecuAD.NumRecuAD != "")
                {
                    if (RecuAD.NumPrelevement != "")
                    {
                        numPrelevement = "'" + RecuAD.NumPrelevement + "'";
                    }
                    if (RecuAD.NumFacture != "")
                    {
                        numFacture = "'" + RecuAD.NumFacture + "'";
                    }

                    this.strCommande = "UPDATE `recuad` SET `numPrelevement`=" + numPrelevement + ", `matriculeAgent`='" + RecuAD.MatriculeAgent + "', ";
                    this.strCommande += "`libele`='" + RecuAD.Libele + "', `montant`='" + RecuAD.Montant + "',`dateRecu`='" + RecuAD.Date.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += "`numFacture`=" + numFacture + " ";
                    this.strCommande += "WHERE (`numRecuAD`='" + RecuAD.NumRecuAD + "')";

                    this.serviceConnectBase.openConnection();
                    nombreUpdate = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreUpdate == 1)
                        isUpdate = true;
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return isUpdate;
        }

        crlRecuAD IntfDalRecuAD.selectRecuAD(string numRecuAD)
        {
            #region declaration
            crlRecuAD RecuAD = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalPrelevement servicePrelevement = new ImplDalPrelevement();
            #endregion

            #region implementation
            if (numRecuAD != "")
            {
                this.strCommande = "SELECT * FROM `recuad` WHERE (`numRecuAD`='" + numRecuAD + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        RecuAD = new crlRecuAD();
                        reader.Read();
                        RecuAD.NumRecuAD = reader["numRecuAD"].ToString();
                        RecuAD.MatriculeAgent = reader["matriculeAgent"].ToString();
                        RecuAD.NumPrelevement = reader["numPrelevement"].ToString();
                        RecuAD.Libele = reader["libele"].ToString();
                        RecuAD.Montant = reader["montant"].ToString();
                        RecuAD.NumFacture = reader["numFacture"].ToString();
                        
                        try
                        {
                            RecuAD.Date = Convert.ToDateTime(reader["dateRecu"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (RecuAD != null)
                {
                    if (RecuAD.MatriculeAgent != "")
                    {
                        RecuAD.agent = serviceAgent.selectAgent(RecuAD.MatriculeAgent);
                    }
                    if (RecuAD.NumPrelevement != "")
                    {
                        RecuAD.prelevement = servicePrelevement.selectPrelevement(RecuAD.NumPrelevement);
                    }
                }
            }
            #endregion

            return RecuAD;
        }

        List<crlRecuAD> IntfDalRecuAD.selectRecuADProprietaireFactureIsNull(string numProprietaire)
        {
            #region declaration
            crlRecuAD tempRecuAD = null;
            List<crlRecuAD> recuADs = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT (recuad.numRecuAD) AS numR, recuad.numPrelevement, recuad.matriculeAgent, recuad.libele,";
                this.strCommande += " recuad.montant, recuad.dateRecu FROM recuad";
                this.strCommande += " Inner Join asociationautorisationdepartrecu ON asociationautorisationdepartrecu.numRecuAD = recuad.numRecuAD";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = asociationautorisationdepartrecu.numAutorisationDepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " Left Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numAutorisationDepart IS NULL  AND";
                this.strCommande += " vehicule.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        recuADs = new List<crlRecuAD>();
                        while (reader.Read())
                        {
                            tempRecuAD = new crlRecuAD();
                            tempRecuAD.NumRecuAD = reader["numR"].ToString();
                            tempRecuAD.MatriculeAgent = reader["matriculeAgent"].ToString();
                            tempRecuAD.NumPrelevement = reader["numPrelevement"].ToString();
                            tempRecuAD.Libele = reader["libele"].ToString();
                            tempRecuAD.Montant = reader["montant"].ToString();

                            try
                            {
                                tempRecuAD.Date = Convert.ToDateTime(reader["dateRecu"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            recuADs.Add(tempRecuAD);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (recuADs != null)
                {
                    for (int i = 0; i < recuADs.Count; i++)
                    {
                        if (recuADs[i].MatriculeAgent != "")
                        {
                            recuADs[i].agent = serviceAgent.selectAgent(recuADs[i].MatriculeAgent);
                        }
                    }
                        
                }
            }
            #endregion

            return recuADs;
        }

        List<crlRecuAD> IntfDalRecuAD.selectRecuADProprietaireResteNonNull(string numProprietaire)
        {
            #region declaration
            crlRecuAD tempRecuAD = null;
            List<crlRecuAD> recuADs = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT (recuad.numRecuAD) AS numR, recuad.numPrelevement, recuad.matriculeAgent, recuad.libele,";
                this.strCommande += " recuad.montant, recuad.dateRecu FROM recuad";
                this.strCommande += " Inner Join asociationautorisationdepartrecu ON asociationautorisationdepartrecu.numRecuAD = recuad.numRecuAD";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = asociationautorisationdepartrecu.numAutorisationDepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0  AND";
                this.strCommande += " vehicule.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        recuADs = new List<crlRecuAD>();
                        while (reader.Read())
                        {
                            tempRecuAD = new crlRecuAD();
                            tempRecuAD.NumRecuAD = reader["numR"].ToString();
                            tempRecuAD.MatriculeAgent = reader["matriculeAgent"].ToString();
                            tempRecuAD.NumPrelevement = reader["numPrelevement"].ToString();
                            tempRecuAD.Libele = reader["libele"].ToString();
                            tempRecuAD.Montant = reader["montant"].ToString();

                            try
                            {
                                tempRecuAD.Date = Convert.ToDateTime(reader["dateRecu"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            recuADs.Add(tempRecuAD);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (recuADs != null)
                {
                    for (int i = 0; i < recuADs.Count; i++)
                    {
                        if (recuADs[i].MatriculeAgent != "")
                        {
                            recuADs[i].agent = serviceAgent.selectAgent(recuADs[i].MatriculeAgent);
                        }
                    }

                }
            }
            #endregion

            return recuADs;
        }

        List<crlRecuAD> IntfDalRecuAD.selectRecuADFacture(string numFacture)
        {
            #region declaration
            crlRecuAD tempRecuAD = null;
            List<crlRecuAD> recuADs = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalPrelevement servicePrelevement = new ImplDalPrelevement();
            #endregion

            #region implementation
            if (numFacture != "")
            {
                this.strCommande = "SELECT (recuad.numRecuAD) AS numR, recuad.numPrelevement, recuad.matriculeAgent, recuad.libele,";
                this.strCommande += " recuad.montant, recuad.dateRecu FROM recuad";
                this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
                this.strCommande += " Inner Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE assocautorisationdepartfacture.numFacture = '" + numFacture + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        recuADs = new List<crlRecuAD>();
                        while (reader.Read())
                        {
                            tempRecuAD = new crlRecuAD();
                            tempRecuAD.NumRecuAD = reader["numR"].ToString();
                            tempRecuAD.MatriculeAgent = reader["matriculeAgent"].ToString();
                            tempRecuAD.NumPrelevement = reader["numPrelevement"].ToString();
                            tempRecuAD.Libele = reader["libele"].ToString();
                            tempRecuAD.Montant = reader["montant"].ToString();

                            try
                            {
                                tempRecuAD.Date = Convert.ToDateTime(reader["dateRecu"].ToString());
                            }
                            catch (Exception)
                            {
                            }

                            recuADs.Add(tempRecuAD);
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (recuADs != null)
                {
                    for (int i = 0; i < recuADs.Count; i++)
                    {
                        if (recuADs[i].MatriculeAgent != "")
                        {
                            recuADs[i].agent = serviceAgent.selectAgent(recuADs[i].MatriculeAgent);
                        }

                        if (recuADs[i].NumPrelevement != "")
                        {
                            recuADs[i].prelevement = servicePrelevement.selectPrelevement(recuADs[i].NumPrelevement);
                        }
                    }

                }
            }
            #endregion

            return recuADs;
        }

        crlRecuAD IntfDalRecuAD.isValideRecuAD(string numRecuAD)
        {
            throw new NotImplementedException();
        }

        bool IntfDalRecuAD.isValideMontant(double montant, string numAutorisationDepart)
        {
            #region declaration
            bool isValide = false;
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            crlAutorisationDepart autorisationDepart = null;

            double montantFacture = 0.00;
            double montantReste = 0.00;
            #endregion

            #region implementation
            if (numAutorisationDepart != "")
            {
                autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(numAutorisationDepart);

                if (autorisationDepart != null)
                {
                    /*montantFacture = serviceFicheBord.getPrixTotalBagage(autorisationDepart.NumerosFB) +
                                                serviceFicheBord.getPrixTotalBillet(autorisationDepart.NumerosFB) +
                                                serviceFicheBord.getPrixTotalCommission(autorisationDepart.NumerosFB);

                    montantReste = montantFacture - serviceAutorisationDepart.getMontanRecu(numAutorisationDepart);*/
                    if (autorisationDepart.ResteRegle >= montant)
                        isValide = true;
                }
            }
            #endregion

            return isValide;
        }

        string IntfDalRecuAD.getNumRecuAD(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numRecuAD = "00001";
            string[] tempNumRecuAD = null;
            string strDate = "RD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD AS maxNum FROM recuad";
            this.strCommande += " WHERE recuad.numRecuAD LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumRecuAD = reader["maxNum"].ToString().ToString().Split('/');
                        numRecuAD = tempNumRecuAD[tempNumRecuAD.Length - 1];
                    }
                    numTemp = double.Parse(numRecuAD) + 1;
                    if (numTemp < 10)
                        numRecuAD = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numRecuAD = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numRecuAD = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numRecuAD = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numRecuAD = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numRecuAD = strDate + "/" + sigleAgence + "/" + numRecuAD;
            #endregion

            return numRecuAD;
        }

        void IntfDalRecuAD.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        void IntfDalRecuAD.loadDdlLibelle(DropDownList ddlTri)
        {
            #region implementation
            this.strCommande = "SELECT  * FROM `libellerecuad`";
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    ddlTri.Items.Clear();
                    ddlTri.Items.Add("");
                    while(this.reader.Read())
                    {
                        ddlTri.Items.Add(this.reader["contenueLibelleRecuAD"].ToString());
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion
        }

        double IntfDalRecuAD.getTotalRecuADDecaisser(string matriculeAgent, DateTime dateDebut, DateTime dateFin)
        {
            #region declaration
            double totalEncaisser = 0.00;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS totalDecaisser FROM recuad";
                this.strCommande += " WHERE recuad.matriculeAgent = '" + matriculeAgent + "' AND";
                this.strCommande += " recuad.dateRecu <= '" + dateFin.ToString("yyyy-MM-dd") + "' AND";
                this.strCommande += " recuad.dateRecu >= '" + dateDebut.ToString("yyyy-MM-dd") + "'";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            try
                            {
                                totalEncaisser = double.Parse(reader["totalDecaisser"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return totalEncaisser;
        }
        #endregion

        #region insert to grid autorisation de voyage
        void IntfDalRecuAD.insertToGridAvanceAutorisationDepart(GridView gridView, string numAutorisationDepart)
        {
            #region declaration
            IntfDalRecuAD serviceRecuAD = new ImplDalRecuAD();
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.montant,";
            this.strCommande += " recuad.dateRecu, recuad.numPrelevement, typeprelevement.commentaire FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE prelevement.numAutorisationDepart = '" + numAutorisationDepart + "'";

            gridView.DataSource = serviceRecuAD.getDataTableAvanceAutorisationDepart(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalRecuAD.getDataTableAvanceAutorisationDepart(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("libele", typeof(string));
            dataTable.Columns.Add("typeRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
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

                        dr["numRecuAD"] = reader["numRecuAD"].ToString();
                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["libele"] = reader["libele"].ToString();
                        dr["typeRecuAD"] = reader["commentaire"].ToString();
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalRecuAD.insertToGridRecuADDecaisser(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent, DateTime dateDebut, DateTime dateFin)
        {
            #region declaration
            IntfDalRecuAD serviceRecuAD = new ImplDalRecuAD();
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.montant,";
            this.strCommande += " recuad.dateRecu, recuad.numPrelevement, typeprelevement.commentaire FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE recuad.matriculeAgent = '" + matriculeAgent + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateFin.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateDebut.ToString("yyyy-MM-dd") + "'";

            gridView.DataSource = serviceRecuAD.getDataTableRecuADDecaisser(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalRecuAD.getDataTableRecuADDecaisser(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("libele", typeof(string));
            dataTable.Columns.Add("typeRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
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

                        dr["numRecuAD"] = reader["numRecuAD"].ToString();
                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["libele"] = reader["libele"].ToString();
                        dr["typeRecuAD"] = reader["commentaire"].ToString();
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());


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