using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using arch.dal.intf;
using arch.crl;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using AppRessources.Ressources;

namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalBilletCommande
    /// </summary>
    public class ImplDalBilletCommande : IntfDalBilletCommande
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalBilletCommande()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalBilletCommande(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion



        #region IntfDalBilletCommande Members

        crlBilletCommande IntfDalBilletCommande.selectBilletCommande(string numBilletCommande)
        {
            #region declaration
            crlBilletCommande billetCommande = null;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalProforma serviceProforma = new ImplDalProforma();
            IntfDalCalculCategorieBillet serviceCalculCategorieBillet = new ImplDalCalculCategorieBillet();
            IntfDalCalculReductionBillet serviceCalculReductionBillet = new ImplDalCalculReductionBillet();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation
            if (numBilletCommande != "")
            {
                this.strCommande = "SELECT * FROM `billetcommande` WHERE (`numBilletCommande`='" + numBilletCommande + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            billetCommande = new crlBilletCommande();
                            billetCommande.NumBilletCommande = this.reader["numBilletCommande"].ToString();
                            billetCommande.NumTrajet = this.reader["numTrajet"].ToString();
                            
                            billetCommande.NumProforma = this.reader["numProforma"].ToString();
                            try
                            {
                                billetCommande.MontantBilletCommande = double.Parse(this.reader["montantBilletCommande"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                billetCommande.NombreBilletCommande = int.Parse(this.reader["nombreBilletCommande"].ToString());
                            }
                            catch (Exception) { }
                            billetCommande.NumCalculCategorieBillet = this.reader["numCalculCategorieBillet"].ToString();
                            billetCommande.NumCalculReductionBillet = this.reader["numCalculReductionBillet"].ToString();
                            billetCommande.NumIndividu = this.reader["numIndividu"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (billetCommande != null)
                {
                    if (billetCommande.NumTrajet != "")
                    {
                        billetCommande.trajet = serviceTrajet.selectTrajet(billetCommande.NumTrajet);
                    }
                    if (billetCommande.NumCalculCategorieBillet != "") 
                    {
                        billetCommande.calculCategorieBillet = serviceCalculCategorieBillet.selectCalculCategorieBillet(billetCommande.NumCalculCategorieBillet);
                    }
                    if (billetCommande.NumCalculReductionBillet != "") 
                    {
                        billetCommande.calculReductionBillet = serviceCalculReductionBillet.selectCalculReductionBillet(billetCommande.NumCalculReductionBillet);
                    }
                    if (billetCommande.NumIndividu != "")
                    {
                        billetCommande.individu = serviceIndividu.selectIndividu(billetCommande.NumIndividu);
                    }
                }

            }
            #endregion

            return billetCommande;
        }

        string IntfDalBilletCommande.insertBilletCommande(crlBilletCommande billetCommande, string sigleAgence)
        {
            #region declaration
            string numBilletCommande = "";
            int nombreInsert = 0;
            IntfDalBilletCommande serviceChequeBilletCommande = new ImplDalBilletCommande();
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            string numTrajet = "";
            string numIndividu = "NULL";
            #endregion

            #region implementation
            if (billetCommande != null)
            {

                if (sigleAgence != "")
                {
                    if (billetCommande.NumCalculCategorieBillet != "")
                    {
                        numCalculCategorieBillet = "'" + billetCommande.NumCalculCategorieBillet + "'";
                    }
                    else 
                    {
                        numCalculCategorieBillet = "NULL";
                    }
                    if (billetCommande.NumCalculReductionBillet != "")
                    {
                        numCalculReductionBillet = "'" + billetCommande.NumCalculReductionBillet + "'";
                    }
                    else 
                    {
                        numCalculReductionBillet = "NULL";
                    }
                    if (billetCommande.NumTrajet != "")
                    {
                        numTrajet = "'" + billetCommande.NumTrajet + "'";
                    }
                    else 
                    {
                        numTrajet = "NULL";
                    }
                    if (billetCommande.NumIndividu != "")
                    {
                        numIndividu = "'" + billetCommande.NumIndividu + "'";
                    }

                    billetCommande.NumBilletCommande = serviceChequeBilletCommande.getNumBilletCommande(sigleAgence);
                    this.strCommande = "INSERT INTO `billetcommande` (`numBilletCommande`,`numTrajet`,`numProforma`,`montantBilletCommande`,";
                    this.strCommande += " `nombreBilletCommande`,`numCalculCategorieBillet`,`numCalculReductionBillet`,`numIndividu`)";
                    this.strCommande += " VALUES ('" + billetCommande.NumBilletCommande + "'," + numTrajet + ",";
                    this.strCommande += " '" + billetCommande.NumProforma + "','" + billetCommande.MontantBilletCommande.ToString("0") + "',";
                    this.strCommande += " '" + billetCommande.NombreBilletCommande + "'," + numCalculCategorieBillet + ",";
                    this.strCommande += " " + numCalculReductionBillet + "," + numIndividu + ")";

                    this.serviceConnectBase.openConnection();
                    nombreInsert = this.serviceConnectBase.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numBilletCommande = billetCommande.NumBilletCommande;
                    }
                    this.serviceConnectBase.closeConnection();
                }
            }
            #endregion

            return numBilletCommande;
        }

        bool IntfDalBilletCommande.updateBilletCommande(crlBilletCommande billetCommande)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numCalculCategorieBillet = "";
            string numCalculReductionBillet = "";
            string numTrajet = "";
            string numIndividu = "NULL";
            #endregion

            #region implementation
            if (billetCommande != null)
            {
                if (billetCommande.NumCalculCategorieBillet != "")
                {
                    numCalculCategorieBillet = "'" + billetCommande.NumCalculCategorieBillet + "'";
                }
                else
                {
                    numCalculCategorieBillet = "NULL";
                }
                if (billetCommande.NumCalculReductionBillet != "")
                {
                    numCalculReductionBillet = "'" + billetCommande.NumCalculReductionBillet + "'";
                }
                else
                {
                    numCalculReductionBillet = "NULL";
                }
                if (billetCommande.NumTrajet != "")
                {
                    numTrajet = "'" + billetCommande.NumTrajet + "'";
                }
                else
                {
                    numTrajet = "NULL";
                }
                if (billetCommande.NumIndividu != "")
                {
                    numIndividu = "'" + billetCommande.NumIndividu + "'";
                }

                this.strCommande = "UPDATE `billetcommande` SET `numTrajet`=" + numTrajet + ",";
                this.strCommande += " `numProforma`='" + billetCommande.NumProforma + "', `montantBilletCommande`='" + billetCommande.MontantBilletCommande.ToString("0") + "',";
                this.strCommande += " `nombreBilletCommande`='" + billetCommande.NombreBilletCommande + "',`numCalculCategorieBillet`=" + numCalculCategorieBillet + ",";
                this.strCommande += " `numCalculReductionBillet`=" + numCalculReductionBillet + ",`numIndividu`=" + numIndividu;
                this.strCommande += " WHERE `numBilletCommande`='" + billetCommande.NumBilletCommande + "'";

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

        string IntfDalBilletCommande.getNumBilletCommande(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numBilletCommande = "00001";
            string[] tempNumBilletCommande = null;
            string strDate = "BC" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT billetcommande.numBilletCommande AS maxNum FROM billetcommande";
            this.strCommande += " WHERE billetcommande.numBilletCommande LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumBilletCommande = reader["maxNum"].ToString().ToString().Split('/');
                        numBilletCommande = tempNumBilletCommande[tempNumBilletCommande.Length - 1];
                    }
                    numTemp = double.Parse(numBilletCommande) + 1;
                    if (numTemp < 10)
                        numBilletCommande = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numBilletCommande = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numBilletCommande = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numBilletCommande = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numBilletCommande = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numBilletCommande = strDate + "/" + sigleAgence + "/" + numBilletCommande;
            #endregion

            return numBilletCommande;
        }

        public List<crlBillet> getBillet(crlBilletCommande billetCommande, crlAgent agent)
        {
            #region declaration
            List<crlBillet> billets = null;
            crlBillet tempBillet = null;
            #endregion

            #region implementation
            if (billetCommande != null && agent != null)
            {
                if (billetCommande.NombreBilletCommande > 0)
                {
                    billets = new List<crlBillet>();

                    for (int i = 0; i < billetCommande.NombreBilletCommande; i++)
                    {
                        tempBillet = new crlBillet();
                        tempBillet.agent = agent;
                        tempBillet.calculCategorieBillet = billetCommande.calculCategorieBillet;
                        tempBillet.calculReductionBillet = billetCommande.calculReductionBillet;
                        try
                        {
                            tempBillet.DateDeValidite = DateTime.Now.AddMonths(int.Parse(ReGlobalParam.nbValiditeBillet));
                        }
                        catch (Exception)
                        {
                            tempBillet.DateDeValidite = DateTime.Now.AddMonths(1);
                        }
                        tempBillet.MatriculeAgent = agent.matriculeAgent;
                        tempBillet.ModePaiement = "Commande";
                        tempBillet.NumCalculCategorieBillet = billetCommande.NumCalculCategorieBillet;
                        tempBillet.NumCalculReductionBillet = billetCommande.NumCalculReductionBillet;
                        tempBillet.NumTrajet = billetCommande.NumTrajet;
                        tempBillet.PrixBillet = billetCommande.MontantBilletCommande.ToString("0");
                        tempBillet.trajet = billetCommande.trajet;
                        tempBillet.NumIndividu = billetCommande.NumIndividu;
                        tempBillet.individu = billetCommande.individu;
                        tempBillet.NumBilletCommande = billetCommande.NumBilletCommande;

                        billets.Add(tempBillet);
                        tempBillet = null;
                    }
                }
            }
            #endregion

            return billets;
        }

        #endregion


        
    }
}
