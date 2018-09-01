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
    /// Implementation du service carte de reduction
    /// </summary>
    public class ImplDalUSCarteReduction : IntfDalUSCarteReduction
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSCarteReduction(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSCarteReduction()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSCarteReduction Members

        string IntfDalUSCarteReduction.insertUSCarteReduction(crlUSCarteReduction carteReduction, string sigleAgence)
        {
            #region declaration
            string numCarteReduction = "";
            IntfDalUSCarteReduction serviceUSCarteReduction = new ImplDalUSCarteReduction();
            int nbInsert = 0;

            string dateDelivranceCertificatScolarite = "NULL";
            string dateAtestationEmployeur = "NULL";
            string numEtablissementScolaire = "NULL";
            string numSociete = "NULL";
            #endregion

            #region implemenbtation
            if (carteReduction != null) 
            {
                if (carteReduction.DateDelivranceCertificatScolarite.Year > 1)
                {
                    dateDelivranceCertificatScolarite = "'" + carteReduction.DateDelivranceCertificatScolarite.ToString("yyyy-MM-dd") + "'";
                }
                if (carteReduction.DateAtestationEmployeur.Year > 1)
                {
                    dateAtestationEmployeur = "'" + carteReduction.DateAtestationEmployeur.ToString("yyyy-MM-dd") + "'";
                }
                if (carteReduction.NumEtablissementScolaire != "") 
                {
                    numEtablissementScolaire = "'" + carteReduction.NumEtablissementScolaire + "'";
                }
                if (carteReduction.NumSociete != "") 
                {
                    numSociete = "'" + carteReduction.NumSociete + "'";
                }

                carteReduction.NumCarteReduction = serviceUSCarteReduction.getNumUSCarteReduction(sigleAgence);
                this.strCommande = "INSERT INTO `uscartereduction` (`numCarteReduction`,`numClient`,`numCategorieBillet`,";
                this.strCommande += " `dateValideDu`,`dateValideAu`,`isLundi`,`isMardi`,`isMercredi`,`isJeudi`,`isVendredi`,";
                this.strCommande += " `isSamedi`,`isDimanche`,`etatCivil`,`dateNaissance`,`imageCarteReduction`,";
                this.strCommande += " `dateDelivranceCertificatScolarite`,`dateAtestationEmployeur`,`numEtablissementScolaire`,";
                this.strCommande += " `numSociete`) VALUES ('" + carteReduction.NumCarteReduction + "',";
                this.strCommande += " '" + carteReduction.NumClient + "','" + carteReduction.NumCategorieBillet + "',";
                this.strCommande += " '" + carteReduction.DateValideDu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " '" + carteReduction.DateValideAu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " '" + carteReduction.IsLundi.ToString("0") + "','" + carteReduction.IsMardi.ToString("0") + "',";
                this.strCommande += " '" + carteReduction.IsMercredi.ToString("0") + "',";
                this.strCommande += " '" + carteReduction.IsJeudi.ToString("0") + "','" + carteReduction.IsVendredi.ToString("0") + "',";
                this.strCommande += " '" + carteReduction.IsSamedi.ToString("0") + "','" + carteReduction.IsDimanche.ToString("0") + "',";
                this.strCommande += " '" + carteReduction.EtatCivil + "',";
                this.strCommande += " '" + carteReduction.DateNaissance.ToString("yyyy-MM-dd") + "','" + carteReduction.ImageCarteReduction + "',";
                this.strCommande += " " + dateDelivranceCertificatScolarite + "," + dateAtestationEmployeur + ",";
                this.strCommande += " " + numEtablissementScolaire + "," + numSociete + ")";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numCarteReduction = carteReduction.NumCarteReduction;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCarteReduction;
        }

        bool IntfDalUSCarteReduction.updateUSCarteReduction(crlUSCarteReduction carteReduction)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;

            string dateDelivranceCertificatScolarite = "NULL";
            string dateAtestationEmployeur = "NULL";
            string numEtablissementScolaire = "NULL";
            string numSociete = "NULL";
            #endregion

            #region implementation
            if (carteReduction != null) 
            {
                if (carteReduction.DateDelivranceCertificatScolarite.Year > 1)
                {
                    dateDelivranceCertificatScolarite = "'" + carteReduction.DateDelivranceCertificatScolarite.ToString("yyyy-MM-dd") + "'";
                }
                if (carteReduction.DateAtestationEmployeur.Year > 1)
                {
                    dateAtestationEmployeur = "'" + carteReduction.DateAtestationEmployeur.ToString("yyyy-MM-dd") + "'";
                }
                if (carteReduction.NumEtablissementScolaire != "")
                {
                    numEtablissementScolaire = "'" + carteReduction.NumEtablissementScolaire + "'";
                }
                if (carteReduction.NumSociete != "")
                {
                    numSociete = "'" + carteReduction.NumSociete + "'";
                }

                this.strCommande = "UPDATE `uscartereduction` SET `numClient`='" + carteReduction.NumClient + "',";
                this.strCommande += " `numCategorieBillet`='" + carteReduction.NumCategorieBillet + "',";
                this.strCommande += " `dateValideDu`='" + carteReduction.DateValideDu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `dateValideAu`='" + carteReduction.DateValideAu.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `isLundi`='" + carteReduction.IsLundi.ToString("0") + "',";
                this.strCommande += " `isMardi`='" + carteReduction.IsMardi.ToString("0") + "',";
                this.strCommande += " `isMercredi`='" + carteReduction.IsMercredi.ToString("0") + "',";
                this.strCommande += " `isJeudi`='" + carteReduction.IsJeudi.ToString("0") + "',";
                this.strCommande += " `isVendredi`='" + carteReduction.IsVendredi.ToString("0") + "',";
                this.strCommande += " `isSamedi`='" + carteReduction.IsSamedi.ToString("0") + "',";
                this.strCommande += " `isDimanche`='" + carteReduction.IsDimanche.ToString("0") + "',";
                this.strCommande += " `etatCivil`='" + carteReduction.EtatCivil + "',";
                this.strCommande += " `dateNaissance`='" + carteReduction.DateNaissance.ToString("yyyy-MM-dd") + "',";
                this.strCommande += " `imageCarteReduction`='" + carteReduction.ImageCarteReduction + "',";
                this.strCommande += " `dateDelivranceCertificatScolarite`=" + dateDelivranceCertificatScolarite + ",";
                this.strCommande += " `dateAtestationEmployeur`=" + dateAtestationEmployeur + ",";
                this.strCommande += " `numEtablissementScolaire`=" + numEtablissementScolaire + ",";
                this.strCommande += " `numSociete`=" + numSociete;
                this.strCommande += " WHERE (`numCarteReduction`='" + carteReduction.NumCarteReduction + "')";

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

        string IntfDalUSCarteReduction.isUSCarteReduction(crlUSCarteReduction carteReduction)
        {
            #region declaration
            string numCarteReduction = "";
            #endregion

            #region implementation
            if (carteReduction != null) 
            {
                this.strCommande = "SELECT * FROM `uscartereduction` WHERE `numCarteReduction`<>'" + carteReduction.NumCarteReduction + "' AND";
                this.strCommande += " `numClient`='" + carteReduction.NumClient + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numCarteReduction = this.reader["numCarteReduction"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numCarteReduction;
        }

        crlUSCarteReduction IntfDalUSCarteReduction.selectUSCarteReduction(string numCarteReduction)
        {
            #region declaration
            crlUSCarteReduction carteReduction = null;
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalUSCategorieBillet serviceUSCategorieBillet = new ImplDalUSCategorieBillet();
            #endregion

            #region implementation
            if (numCarteReduction != "")
            {
                this.strCommande = "SELECT * FROM `uscartereduction` WHERE `numCarteReduction`='" + numCarteReduction + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            carteReduction = new crlUSCarteReduction();
                            try
                            {
                                carteReduction.DateValideAu = Convert.ToDateTime(this.reader["dateValideAu"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.DateValideDu = Convert.ToDateTime(this.reader["dateValideDu"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsDimanche = int.Parse(this.reader["isDimanche"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsJeudi = int.Parse(this.reader["isJeudi"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsLundi = int.Parse(this.reader["isLundi"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsMardi = int.Parse(this.reader["isMardi"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsMercredi = int.Parse(this.reader["isMercredi"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsSamedi = int.Parse(this.reader["isSamedi"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.IsVendredi = int.Parse(this.reader["isVendredi"].ToString());
                            }
                            catch (Exception) { }
                            carteReduction.NumCarteReduction = this.reader["numCarteReduction"].ToString();
                            carteReduction.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            carteReduction.NumClient = this.reader["numClient"].ToString();
                            carteReduction.EtatCivil = this.reader["etatCivil"].ToString();
                            try
                            {
                                carteReduction.DateNaissance = Convert.ToDateTime(this.reader["dateNaissance"].ToString());
                            }
                            catch (Exception) { }
                            try
                            {
                                carteReduction.DateDelivranceCertificatScolarite = Convert.ToDateTime(this.reader["dateDelivranceCertificatScolarite"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                carteReduction.DateAtestationEmployeur = Convert.ToDateTime(this.reader["dateAtestationEmployeur"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                            carteReduction.ImageCarteReduction = this.reader["imageCarteReduction"].ToString();
                            carteReduction.NumEtablissementScolaire = this.reader["numEtablissementScolaire"].ToString();
                            carteReduction.NumSociete = this.reader["numSociete"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (carteReduction != null) 
                {
                    if (carteReduction.NumCategorieBillet != "") 
                    {
                        carteReduction.categorieBillet = serviceUSCategorieBillet.selectUSCategorieBillet(carteReduction.NumCategorieBillet);
                    }
                    if (carteReduction.NumClient != "") 
                    {
                        carteReduction.client = serviceClient.selectClient(carteReduction.NumClient);
                    }
                }
            }
            #endregion

            return carteReduction;
        }

        string IntfDalUSCarteReduction.getNumUSCarteReduction(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numCarteReduction = "00001";
            string[] tempNumCarteReduction = null;
            string strDate = "CE" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT uscartereduction.numCarteReduction AS maxNum FROM uscartereduction";
            this.strCommande += " WHERE uscartereduction.numCarteReduction LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumCarteReduction = reader["maxNum"].ToString().ToString().Split('/');
                        numCarteReduction = tempNumCarteReduction[tempNumCarteReduction.Length - 1];
                    }
                    numTemp = double.Parse(numCarteReduction) + 1;
                    if (numTemp < 10)
                        numCarteReduction = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numCarteReduction = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numCarteReduction = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numCarteReduction = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numCarteReduction = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numCarteReduction = strDate + "/" + sigleAgence + "/" + numCarteReduction;
            #endregion

            return numCarteReduction;
        }

        bool IntfDalUSCarteReduction.isCarteReductionValide(string numCarteReduction, DateTime date)
        {
            #region declaration
            bool isValide = false;
            crlUSCarteReduction carteReduction = null;
            IntfDalUSCarteReduction serviceUSCarteReduction = new ImplDalUSCarteReduction();
            int jourSemaine = -1;
            #endregion

            #region implementation
            if (numCarteReduction != "")
            {
                carteReduction = serviceUSCarteReduction.selectUSCarteReduction(numCarteReduction);

                if (carteReduction != null)
                {
                    if (carteReduction.DateValideDu <= date && carteReduction.DateValideAu >= date)
                    {
                        try
                        {
                            jourSemaine = int.Parse(date.DayOfWeek.ToString("d"));
                        }
                        catch (Exception)
                        {
                        }
                        if (jourSemaine == 0 && carteReduction.IsDimanche == 1)
                        {
                            isValide = true;
                        }
                        if (jourSemaine == 1 && carteReduction.IsLundi == 1)
                        {
                            isValide = true;
                        }
                        if (jourSemaine == 2 && carteReduction.IsMardi == 1)
                        {
                            isValide = true;
                        }
                        if (jourSemaine == 3 && carteReduction.IsMercredi == 1)
                        {
                            isValide = true;
                        }
                        if (jourSemaine == 4 && carteReduction.IsJeudi == 1)
                        {
                            isValide = true;
                        }
                        if (jourSemaine == 5 && carteReduction.IsVendredi == 1)
                        {
                            isValide = true;
                        }
                        if (jourSemaine == 6 && carteReduction.IsSamedi == 1)
                        {
                            isValide = true;
                        }
                    }
                }
            }
            #endregion

            return isValide;
        }
        #endregion


        #region Insert to grid


        void IntfDalUSCarteReduction.insertToGridCarteReduction(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSCarteReduction serviceUSCarteReduction = new ImplDalUSCarteReduction();
            #endregion

            #region implementation

            this.strCommande = "SELECT client.numClient, client.nomClient, client.prenomClient, client.adresseClient,";
            this.strCommande += " client.cinClient, client.telephoneClient, client.mobileClient, client.isCheque,";
            this.strCommande += " client.isBonCommande, uscartereduction.numCarteReduction,";
            this.strCommande += " uscartereduction.numClient, uscartereduction.etatCivil,";
            this.strCommande += " uscartereduction.dateNaissance, dateAtestationEmployeur, dateDelivranceCertificatScolarite,";
            this.strCommande += " uscartereduction.dateValideDu, uscartereduction.dateValideAu,";
            this.strCommande += " uscartereduction.isLundi, uscartereduction.isMardi, uscartereduction.isMercredi,";
            this.strCommande += " uscartereduction.isJeudi, uscartereduction.isVendredi, uscartereduction.isSamedi,";
            this.strCommande += " uscartereduction.isDimanche, uscartereduction.numCategorieBillet,";
            this.strCommande += " uscartereduction.imageCarteReduction FROM uscartereduction";
            this.strCommande += " Inner Join client ON client.numClient = uscartereduction.numClient";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSCarteReduction.getDataTableCarteReduction(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSCarteReduction.getDataTableCarteReduction(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numCarteReduction", typeof(string));
            dataTable.Columns.Add("client", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("valideau", typeof(DateTime));
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

                        dr["numCarteReduction"] = reader["numCarteReduction"].ToString();
                        dr["client"] = reader["etatCivil"].ToString() + " " + reader["nomClient"].ToString() + " " + reader["prenomClient"].ToString();
                        dr["contact"] = reader["telephoneClient"].ToString() + "/" + reader["mobileClient"].ToString();
                        try
                        {
                            dr["valideau"] = Convert.ToDateTime(reader["dateValideAu"].ToString());
                        }
                        catch (Exception) { }

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