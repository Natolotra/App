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
    /// Description résumée de ImplDalRecuEncaisser
    /// </summary>
    public class ImplDalRecuEncaisser : IntfDalRecuEncaisser
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRecuEncaisser()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalRecuEncaisser(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlRecuEncaisser IntfDalRecuEncaisser.selectRecuEncaisser(string numRecuEncaisser)
        {
            #region declaration
            crlRecuEncaisser recuEncaisser = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalCheque serviceCheque = new ImplDalCheque();
            #endregion

            #region implementation
            if(numRecuEncaisser != "")
            {
                this.strCommande = "SELECT * FROM `recuencaisser` WHERE (`numRecuEncaisser`='" + numRecuEncaisser + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        recuEncaisser = new crlRecuEncaisser();
                        if (this.reader.Read())
                        {
                            try
                            {
                                recuEncaisser.DateRecuEncaisser = Convert.ToDateTime(this.reader["ateRecuEncaisser"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            recuEncaisser.LibelleRecuEncaisser = this.reader["libelleRecuEncaisser"].ToString();
                            recuEncaisser.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            recuEncaisser.ModePaiement = this.reader["modePaiement"].ToString();
                            try
                            {
                                recuEncaisser.MontantRecuEncaisser = double.Parse(this.reader["montantRecuEncaisser"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            recuEncaisser.NumCheque = this.reader["numCheque"].ToString();
                            recuEncaisser.NumRecuEncaisser = this.reader["numRecuEncaisser"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (recuEncaisser != null)
                {
                    if (recuEncaisser.MatriculeAgent != "")
                    {
                        recuEncaisser.agent = serviceAgent.selectAgent(recuEncaisser.MatriculeAgent);
                    }
                    if (recuEncaisser.NumCheque != "")
                    {
                        recuEncaisser.cheque = serviceCheque.selectCheque(recuEncaisser.NumCheque);
                    }
                }
            }
            #endregion

            return recuEncaisser;
        }

        string IntfDalRecuEncaisser.insertRecuEncaisser(crlRecuEncaisser recuEncaisser)
        {
            #region declaration
            int nombreInsert = 0;
            string numRecuEncaisser = "";
            string numCheque = "NULL";
            IntfDalRecuEncaisser serviceRecuEncaisser = new ImplDalRecuEncaisser();
            #endregion

            #region implementation
            if (recuEncaisser != null)
            {
                if (recuEncaisser.agent != null)
                {
                    if (recuEncaisser.NumCheque != "")
                    {
                        numCheque = "'" + recuEncaisser.NumCheque + "'";
                    }

                    recuEncaisser.NumRecuEncaisser = serviceRecuEncaisser.getNumRecuEncaisser(recuEncaisser.agent.agence.SigleAgence);
                    this.strCommande = "INSERT INTO `recuencaisser` (`numRecuEncaisser`,`matriculeAgent`,`numCheque`,";
                    this.strCommande += " `modePaiement`,`dateRecuEncaisser`,`montantRecuEncaisser`,`libelleRecuEncaisser`)";
                    this.strCommande += " VALUES ('" + recuEncaisser.NumRecuEncaisser + "','" + recuEncaisser.MatriculeAgent + "',";
                    this.strCommande += " " + numCheque + ",'" + recuEncaisser.ModePaiement + "',";
                    this.strCommande += " '" + recuEncaisser.DateRecuEncaisser.ToString("yyyy-MM-dd") + "',";
                    this.strCommande += " '" + recuEncaisser.MontantRecuEncaisser + "','" + recuEncaisser.LibelleRecuEncaisser + "')";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numRecuEncaisser = recuEncaisser.NumRecuEncaisser;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numRecuEncaisser;
        }

        string IntfDalRecuEncaisser.insertRecuEncaisserCheque(crlRecuEncaisser recuEncaisser)
        {
            #region declaration
            string numRecuEncaisser = "";
            IntfDalCheque serviceCheque = new ImplDalCheque();
            IntfDalRecuEncaisser serviceRecuEncaisser = new ImplDalRecuEncaisser();
            #endregion

            #region implementation
            if (numRecuEncaisser != null)
            {
                if (recuEncaisser.cheque != null)
                {
                    recuEncaisser.cheque.NumCheque = serviceCheque.insertCheque(recuEncaisser.cheque, recuEncaisser.agent.agence.SigleAgence);
                    if (recuEncaisser.cheque.NumCheque != "")
                    {
                        recuEncaisser.NumCheque = recuEncaisser.cheque.NumCheque;
                        recuEncaisser.NumRecuEncaisser = serviceRecuEncaisser.insertRecuEncaisser(recuEncaisser);

                        if (recuEncaisser.NumRecuEncaisser != "")
                        {
                            numRecuEncaisser = recuEncaisser.NumRecuEncaisser;
                        }
                    }
                }
            }
            #endregion

            return numRecuEncaisser;
        }

        bool IntfDalRecuEncaisser.updateRecuEncaisser(crlRecuEncaisser recuEncaisser)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numCheque = "NULL";
            #endregion

            #region implementation
            if (recuEncaisser != null)
            {
                if (recuEncaisser.NumCheque != "")
                {
                    numCheque = "'" + recuEncaisser.NumCheque + "'";
                }

                this.strCommande = "UPDATE `recuencaisser` SET `matriculeAgent`='" + recuEncaisser.MatriculeAgent + "',";
                this.strCommande += " `dateRecuEncaisser`='" + recuEncaisser.DateRecuEncaisser.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `libelleRecuEncaisser`='" + recuEncaisser.LibelleRecuEncaisser + "',";
                this.strCommande += " `numCheque`=" + numCheque + ",`modePaiement`='" + recuEncaisser.ModePaiement + "',";
                this.strCommande += " `montantRecuEncaisser`='" + recuEncaisser.MontantRecuEncaisser + "'";
                this.strCommande += " WHERE `numRecuEncaisser`='" + recuEncaisser.NumRecuEncaisser + "'";

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

        string IntfDalRecuEncaisser.getNumRecuEncaisser(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numRecuEncaisser = "00001";
            string[] tempNumRecuEncaisser = null;
            string strDate = "RE" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT recuencaisser.numRecuEncaisser AS maxNum FROM recuencaisser";
            this.strCommande += " WHERE recuencaisser.numRecuEncaisser LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumRecuEncaisser = reader["maxNum"].ToString().ToString().Split('/');
                        numRecuEncaisser = tempNumRecuEncaisser[tempNumRecuEncaisser.Length - 1];
                    }
                    numTemp = double.Parse(numRecuEncaisser) + 1;
                    if (numTemp < 10)
                        numRecuEncaisser = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numRecuEncaisser = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numRecuEncaisser = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numRecuEncaisser = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numRecuEncaisser = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numRecuEncaisser = strDate + "/" + sigleAgence + "/" + numRecuEncaisser;
            #endregion

            return numRecuEncaisser;
        }

        bool IntfDalRecuEncaisser.insertAssocRecuEncaisserProformaBonDeCommande(string numProforma, string numRecuEncaisser, string numBonDeCommande)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            string numRE = "NULL";
            string numBC = "NULL";
            #endregion

            #region implementation
            if (numProforma != "")
            {
                if (numRecuEncaisser != "")
                {
                    numRE = "'" + numRecuEncaisser + "'";
                }
                if (numBonDeCommande != "")
                {
                    numBC = "'" + numBonDeCommande + "'";
                }

                this.strCommande = "INSERT INTO `assocrecuencaisserproformabondecommande` (`numProforma`,";
                this.strCommande += " `numRecuEncaisser`,`numBonDeCommande`) VALUES ('" + numProforma + "',";
                this.strCommande += " " + numRE + ", " + numBC + ")";

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

        crlRecuEncaisser IntfDalRecuEncaisser.isValideRecu(string numRecu)
        {
            #region declaration
            crlRecuEncaisser Recu = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalCheque serviceCheque = new ImplDalCheque();
            #endregion

            #region implementation
            if (numRecu != "")
            {
                this.strCommande = "SELECT recuencaisser.numRecuEncaisser, recuencaisser.matriculeAgent, recuencaisser.numCheque, recuencaisser.modePaiement,";
                this.strCommande += "  recuencaisser.dateRecuEncaisser, recuencaisser.montantRecuEncaisser, recuencaisser.libelleRecuEncaisser FROM recuencaisser";
                this.strCommande += " Left Join assocrecuencaisserproformabondecommande ON assocrecuencaisserproformabondecommande.numRecuEncaisser = recuencaisser.numRecuEncaisser";
                this.strCommande += " Left Join bagage ON bagage.numRecu = recuencaisser.numRecuEncaisser";
                this.strCommande += " WHERE assocrecuencaisserproformabondecommande.numRecuEncaisser IS NULL  AND";
                this.strCommande += " bagage.numRecu IS NULL  AND";
                this.strCommande += " recuencaisser.numRecuEncaisser = '" + numRecu + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Recu = new crlRecuEncaisser();
                        reader.Read();
                        Recu.NumRecuEncaisser = reader["numRecuEncaisser"].ToString();
                        Recu.LibelleRecuEncaisser = reader["libelleRecuEncaisser"].ToString();
                        try
                        {
                            Recu.MontantRecuEncaisser = double.Parse(reader["montantRecuEncaisser"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        Recu.ModePaiement = reader["modePaiement"].ToString();
                        Recu.MatriculeAgent = reader["matriculeAgent"].ToString();
                        try
                        {
                            Recu.DateRecuEncaisser = Convert.ToDateTime(reader["dateRecuEncaisser "].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        Recu.NumCheque = reader["numCheque"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (Recu != null)
                {
                    if (Recu.MatriculeAgent != "")
                    {
                        Recu.agent = serviceAgent.selectAgent(Recu.MatriculeAgent);
                    }
                    if (Recu.NumCheque != "")
                    {
                        Recu.cheque = serviceCheque.selectCheque(Recu.NumCheque);
                    }
                }
            }
            #endregion

            return Recu;
        }

        bool IntfDalRecuEncaisser.insertAssocRecuEncaisserCarte(string numRecuEncaisser, string numCarte)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numRecuEncaisser != "" && numCarte != "")
            {
                this.strCommande = "INSERT INTO `assocrecuencaisseruscarte` (`numRecuEncaisser`,";
                this.strCommande += " `numCarte`) VALUES ('" + numRecuEncaisser + "',";
                this.strCommande += " '" + numCarte + "')";

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
        #endregion



        void IntfDalRecuEncaisser.insertToGridRecuEncaisser(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            throw new NotImplementedException();
        }

        DataTable IntfDalRecuEncaisser.getDataTableRecuEncaisser(string strRqst)
        {
            throw new NotImplementedException();
        }

    }
}