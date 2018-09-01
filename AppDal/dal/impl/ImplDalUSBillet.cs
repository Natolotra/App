using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service billet urbaine suburbaine
    /// </summary>
    public class ImplDalUSBillet : IntfDalUSBillet
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSBillet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSBillet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSBillet Members

        string IntfDalUSBillet.insertUSBillet(crlUSBillet billet, string sigleAgence)
        {
            #region declaration
            string numBillet = "";
            IntfDalUSBillet serviceUSBillet = new ImplDalUSBillet();
            string numLieuD = "NULL";
            string numLieuF = "NULL";
            string numCarteReduction = "NULL";
            string numAbonnementNV = "NULL";
            string numReductionBillet = "NULL";
            string numCategorieBillet = "NULL";
            string montant = "NULL";
            string numTicket = "NULL";
            int nbInsert = 0;
            #endregion

            #region implementation
            if (billet != null) 
            {
                if (billet.NumLieuD != "") 
                {
                    numLieuD = "'" + billet.NumLieuD + "'";
                }
                if (billet.NumLieuF != "") 
                {
                    numLieuF = "'" + billet.NumLieuF + "'";
                }
                if (billet.NumCarteReduction != "") 
                {
                    numCarteReduction = "'" + billet.NumCarteReduction + "'";
                }
                if (billet.NumAbonnementNV != "") 
                {
                    numAbonnementNV = "'" + billet.NumAbonnementNV + "'";
                }
                if (billet.NumReductionBillet != "") 
                {
                    numReductionBillet = "'" + billet.NumReductionBillet + "'";
                }
                if (billet.NumCategorieBillet != "") 
                {
                    numCategorieBillet = "'" + billet.NumCategorieBillet + "'";
                }
                if (billet.Montant > 0)
                {
                    montant = "'" + billet.Montant.ToString("0") + "'";
                }
                if (billet.NumTicket != "") 
                {
                    numTicket = "'" + billet.NumTicket + "'";
                }
                billet.NumBillet = serviceUSBillet.getNumUSBillet(sigleAgence);
                this.strCommande = "INSERT INTO `usbillet` (`numBillet`,`dateBillet`,`valideAu`,`montant`,";
                this.strCommande += " `numZoneD`,`numZoneF`,`numLieuD`,`numLieuF`,`niveau`,`matriculeAgent`,`numTicket`,";
                this.strCommande += " `modeDePaiement`,`numCarteReduction`,`numAbonnementNV`,`numReductionBillet`,`numCategorieBillet`) VALUES ";
                this.strCommande += " ('" + billet.NumBillet + "','" + billet.DateBillet.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " '" + billet.ValideAu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " " + montant + ",'" + billet.NumZoneD + "',";
                this.strCommande += " '" + billet.NumZoneF + "'," + numLieuD + "," + numLieuF + ",";
                this.strCommande += " '" + billet.Niveau.ToString("0") + "','" + billet.MatriculeAgent + "'," + numTicket + ",";
                this.strCommande += " '" + billet.ModeDePaiement + "'," + numCarteReduction + "," + numAbonnementNV + ",";
                this.strCommande += " " + numReductionBillet + ", " + numCategorieBillet + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numBillet = billet.NumBillet;
                }
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numBillet;
        }

        bool IntfDalUSBillet.updateUSBillet(crlUSBillet billet)
        {
            #region declaration
            bool isUpdate = false;
            string numLieuD = "NULL";
            string numLieuF = "NULL";
            string numCarteReduction = "NULL";
            string numAbonnementNV = "NULL";
            string numReductionBillet = "NULL";
            string numCategorieBillet = "NULL";
            string montant = "NULL";
            string numTicket = "NULL";
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (billet != null) 
            {
                if (billet.NumLieuD != "")
                {
                    numLieuD = "'" + billet.NumLieuD + "'";
                }
                if (billet.NumLieuF != "")
                {
                    numLieuF = "'" + billet.NumLieuF + "'";
                }
                if (billet.NumCarteReduction != "")
                {
                    numCarteReduction = "'" + billet.NumCarteReduction + "'";
                }
                if (billet.NumAbonnementNV != "")
                {
                    numAbonnementNV = "'" + billet.NumAbonnementNV + "'";
                }
                if (billet.NumReductionBillet != "")
                {
                    numReductionBillet = "'" + billet.NumReductionBillet + "'";
                }
                if (billet.NumCategorieBillet != "")
                {
                    numCategorieBillet = "'" + billet.NumCategorieBillet + "'";
                }
                if (billet.Montant > 0)
                {
                    montant = "'" + billet.Montant.ToString("0") + "'";
                }
                if (billet.NumTicket != "") 
                {
                    numTicket = "'" + billet.NumTicket + "'";
                }
                this.strCommande = "UPDATE `usbillet` SET `dateBillet`='" + billet.DateBillet.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `valideAu`='" + billet.ValideAu.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                this.strCommande += " `montant`=" + montant + ", `numZoneD`='" + billet.NumZoneD + "',";
                this.strCommande += " `numZoneF`='" + billet.NumZoneF + "', `numLieuD`=" + numLieuD + ", `numLieuF`=" + numLieuF + ",";
                this.strCommande += " `niveau`='" + billet.Niveau + "', `matriculeAgent`='" + billet.MatriculeAgent + "',";
                this.strCommande += " `modeDePaiement`='" + billet.ModeDePaiement + "', `numCarteReduction`=" + numCarteReduction + ",";
                this.strCommande += " `numAbonnementNV`=" + numAbonnementNV + ", `numReductionBillet`=" + numReductionBillet + ",";
                this.strCommande += " `numCategorieBillet`=" + numCategorieBillet + ",`numTicket`=" + numTicket;
                this.strCommande += " WHERE `numBillet`='" + billet.NumBillet + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1) 
                {
                    isUpdate = true;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        crlUSBillet IntfDalUSBillet.selectUSBillet(string numBillet)
        {
            #region declaration
            crlUSBillet billet = null;
            IntfDalUSZone serviceUSZone = new ImplDalUSZone();
            #endregion

            #region implementation
            if (numBillet != "") 
            {
                this.strCommande = "SELECT * FROM `usbillet` WHERE `numBillet`='" + numBillet + "'";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            billet = new crlUSBillet();
                            try
                            {
                                billet.DateBillet = Convert.ToDateTime(this.reader["dateBillet"].ToString());
                            }
                            catch (Exception) { }
                            billet.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            billet.ModeDePaiement = this.reader["modeDePaiement"].ToString();
                            try
                            {
                                billet.Montant = double.Parse(this.reader["montant"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                billet.Niveau = int.Parse(this.reader["niveau"].ToString());
                            }
                            catch (Exception) { }
                            billet.NumCarteReduction = this.reader["numCarteReduction"].ToString();
                            billet.NumAbonnementNV = this.reader["numAbonnementNV"].ToString();
                            billet.NumBillet = this.reader["numBillet"].ToString();
                            billet.NumLieuD = this.reader["numLieuD"].ToString();
                            billet.NumLieuF = this.reader["numLieuF"].ToString();
                            billet.NumZoneD = this.reader["numZoneD"].ToString();
                            billet.NumZoneF = this.reader["numZoneF"].ToString();
                            try
                            {
                                billet.ValideAu = Convert.ToDateTime(this.reader["valideAu"].ToString());
                            }
                            catch (Exception) { }
                            billet.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            billet.NumReductionBillet = this.reader["numReductionBillet"].ToString();
                            billet.NumTicket = this.reader["numTicket"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (billet != null)
                {
                    if (billet.NumZoneD != "")
                    {
                        billet.zoneD = serviceUSZone.selectUSZone(billet.NumZoneD);
                    }
                    if (billet.NumZoneF != "")
                    {
                        billet.zoneF = serviceUSZone.selectUSZone(billet.NumZoneF);
                    }
                }
            }
            #endregion

            return billet;
        }

        string IntfDalUSBillet.getNumUSBillet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numBillet = "000001";
            string[] tempNumBillet = null;
            string strDate = sigleAgence + DateTime.Now.ToString("yyMMddHH");
            #endregion

            #region implementation
            this.strCommande = "SELECT usbillet.numBillet AS maxNum FROM usbillet";
            this.strCommande += " WHERE usbillet.numBillet LIKE '%" + strDate + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumBillet = reader["maxNum"].ToString().ToString().Split('/');
                        numBillet = tempNumBillet[tempNumBillet.Length - 1];
                    }
                    numTemp = double.Parse(numBillet) + 1;

                    numBillet = numTemp.ToString("000000");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numBillet = strDate + "/" + numBillet;
            #endregion

            return numBillet;
        }

        bool IntfDalUSBillet.insertUSAssocVoyageBillet(string numVoyage, string numBillet)
        {
            #region declaration
            bool isInsert = false;
            int nbInsert = 0;
            #endregion

            #region implementation
            if (numVoyage != "" && numBillet != "")
            {
                this.strCommande = "INSERT INTO `usassocvoyagebillet` (`numVoyage`,`numBillet`,`dateHeure`)";
                this.strCommande += " VALUES ('" + numVoyage + "','" + numBillet + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

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

    }
}