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
    /// Implementation du service ImplDalProprietaire
    /// </summary>
    public class ImplDalProprietaire : IntfDalProprietaire
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalProprietaire(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalProprietaire()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region methode
        crlProprietaire IntfDalProprietaire.selectProprietaire(string numProprietaire)
        {
            #region declaration
            crlProprietaire proprietaire = null;

            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalTypeProprietaire serviceTypeProprietaire = new ImplDalTypeProprietaire();
            IntfDalAgence serviceAgence = new ImplDalAgence();
            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT * FROM `proprietaire` WHERE (`numProprietaire`='" + numProprietaire + "')";

                this.serviceConnection.openConnection();
                if (this.serviceConnection.IsConnection)
                {
                    this.reader = this.serviceConnection.select(this.strCommande);
                    if (this.reader != null)
                    {
                        if (this.reader.HasRows)
                        {
                            if (this.reader.Read())
                            {
                                proprietaire = new crlProprietaire();
                                proprietaire.NumProprietaire = this.reader["numProprietaire"].ToString();
                                proprietaire.NumIndividu = this.reader["numIndividu"].ToString();
                                proprietaire.NumOrganisme = this.reader["numOrganisme"].ToString();
                                proprietaire.NumSociete = this.reader["numSociete"].ToString();
                                proprietaire.TypeProprietaire = this.reader["typeProprietaire"].ToString();
                                proprietaire.NumAgence = this.reader["numAgence"].ToString();
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

                if (proprietaire != null)
                {
                    if (proprietaire.NumIndividu != "")
                    {
                        proprietaire.Individu = serviceIndividu.selectIndividu(proprietaire.NumIndividu);
                    }
                    if (proprietaire.TypeProprietaire != "")
                    {
                        proprietaire.typeProprietaireObj = serviceTypeProprietaire.selectTypeProprietaire(proprietaire.TypeProprietaire);
                    }
                    if (proprietaire.NumAgence != "")
                    {
                        proprietaire.agence = serviceAgence.selectAgence(proprietaire.NumAgence);
                    }
                    if (proprietaire.NumOrganisme != "")
                    {
                        proprietaire.organisme = serviceOrganisme.selectOrganisme(proprietaire.NumOrganisme);
                    }
                    if (proprietaire.NumSociete != "")
                    {
                        proprietaire.societe = serviceSociete.selectSociete(proprietaire.NumSociete);
                    }
                }
            }
            #endregion

            return proprietaire;
        }

        string IntfDalProprietaire.insertProprietaire(crlProprietaire proprietaire, string sigleAgence)
        {
            #region declaration
            string numProprietaire = "";
            IntfDalProprietaire serviceProprietaire = new ImplDalProprietaire();
            int nombreInsert = 0;
            string numIndividu = "";
            string numOrganisme = "";
            string numSociete = "";
            #endregion

            #region implementation
            if (proprietaire != null)
            {
                if(proprietaire.NumIndividu != "")
                {
                    numIndividu = "'" + proprietaire.NumIndividu + "'";
                }
                else
                {
                    numIndividu = "NULL";
                }
                if (proprietaire.NumOrganisme != "")
                {
                    numOrganisme = "'" + proprietaire.NumOrganisme + "'";
                }
                else
                {
                    numOrganisme = "NULL";
                }
                if (proprietaire.NumSociete != "")
                {
                    numSociete = "'" + proprietaire.NumSociete + "'";
                }
                else
                {
                    numSociete = "NULL";
                }

                proprietaire.NumProprietaire = serviceProprietaire.getNumProprietaire(sigleAgence);
                this.strCommande = "INSERT INTO `proprietaire` (`numProprietaire`,`typeProprietaire`,";
                this.strCommande += " `numOrganisme`,`numSociete`,`numIndividu`,`numAgence`)";
                this.strCommande += " VALUES ('" + proprietaire.NumProprietaire + "',";
                this.strCommande += " '" + proprietaire.TypeProprietaire + "'," + numOrganisme + ",";
                this.strCommande += " " + numSociete + "," + numIndividu + ",";
                this.strCommande += " '" +  proprietaire.NumAgence + "')";

                this.serviceConnection.openConnection();
                nombreInsert = this.serviceConnection.requete(this.strCommande);
                if (nombreInsert == 1)
                {
                    numProprietaire = proprietaire.NumProprietaire;
                }
                this.serviceConnection.closeConnection();

            }
            #endregion

            return numProprietaire;
        }

        string IntfDalProprietaire.getNumProprietaire(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numProprietaire = "00001";
            string[] tempNumProprietaire = null;
            string strDate = "PR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT proprietaire.numProprietaire AS maxNum FROM proprietaire";
            this.strCommande += " WHERE proprietaire.numProprietaire LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumProprietaire = reader["maxNum"].ToString().ToString().Split('/');
                        numProprietaire = tempNumProprietaire[tempNumProprietaire.Length - 1];
                    }
                    numTemp = double.Parse(numProprietaire) + 1;
                    if (numTemp < 10)
                        numProprietaire = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numProprietaire = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numProprietaire = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numProprietaire = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numProprietaire = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            numProprietaire = strDate + "/" + sigleAgence + "/" + numProprietaire;
            #endregion

            return numProprietaire;
        }

        string IntfDalProprietaire.isProprietaire(crlProprietaire proprietaire)
        {
            #region declaration
            string numProprietaire = "";
            #endregion

            #region implementation
            if (proprietaire != null)
            {
                this.strCommande = "SELECT * FROM `proprietaire` WHERE";
                this.strCommande += " (proprietaire.numOrganisme = '" + proprietaire.NumOrganisme + "' OR";
                this.strCommande += " proprietaire.numSociete =  '" + proprietaire.NumSociete + "' OR";
                this.strCommande += " proprietaire.numIndividu =  '" + proprietaire.NumIndividu + "') AND";
                this.strCommande += " proprietaire.numProprietaire <> '" + proprietaire.NumProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numProprietaire = this.reader["numProprietaire"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return numProprietaire;
        }



        double IntfDalProprietaire.getTotalPrixBillet(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(billet.prixBillet) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join voyage ON voyage.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join billet ON billet.numBillet = voyage.numBillet";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        double IntfDalProprietaire.getTotalPrixBagage(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(recu.montant) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join voyage ON voyage.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join associationvoyagebagage ON associationvoyagebagage.idVoyage = voyage.idVoyage";
                this.strCommande += " Inner Join bagage ON bagage.idBagage = associationvoyagebagage.idBagage";
                this.strCommande += " Inner Join recu ON recu.numRecu = bagage.numRecu";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        double IntfDalProprietaire.getTotalPrixCommission(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(recu.montant) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join associationfichebordcommission ON associationfichebordcommission.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join commission ON commission.idCommission = associationfichebordcommission.idCommission";
                this.strCommande += " Inner Join recu ON recu.numRecu = commission.numRecu";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        double IntfDalProprietaire.getTotalMontantRecu(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join prelevement ON prelevement.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " Inner Join recuad ON recuad.numPrelevement = prelevement.numPrelevement";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        double IntfDalProprietaire.getTotalPrelevemen(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(prelevement.montantPrelevement) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join prelevement ON prelevement.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        double IntfDalProprietaire.getTotalRecette(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        double IntfDalProprietaire.getTotalReste(string numProprietaire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numProprietaire != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.resteRegle) AS prixTotal FROM proprietaire";
                this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["prixTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return prixTotal;
        }
        #endregion

        #region Insert to grid
        void IntfDalProprietaire.insertToGridProprietaire(GridView gridView, string param, string paramLike, string valueLike, string typeProprietaire)
        {
            #region declaration
            IntfDalProprietaire serviceProprietaire = new ImplDalProprietaire();
            #endregion

            #region implementation

            this.strCommande = "SELECT proprietaire.numProprietaire, Individu.numIndividu, Individu.civiliteIndividu,";
            this.strCommande += " Individu.nomIndividu, Individu.prenomIndividu, Individu.cinIndividu,";
            this.strCommande += " Individu.adresse, Individu.profession, Individu.telephoneFixeIndividu,";
            this.strCommande += " Individu.telephoneMobileIndividu FROM proprietaire";
            this.strCommande += " Inner Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " WHERE proprietaire.typeProprietaire = '" + typeProprietaire + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceProprietaire.getDataTableProprietaire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalProprietaire.getDataTableProprietaire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProprietaire", typeof(string));
            dataTable.Columns.Add("Individu", typeof(string));
            dataTable.Columns.Add("cinIndividu", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("telephoneFixeIndividu", typeof(string));
            dataTable.Columns.Add("telephoneMobileIndividu", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numProprietaire"] = reader["numProprietaire"].ToString();
                        dr["Individu"] = reader["civiliteIndividu"].ToString() + " " + reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();
                        dr["cinIndividu"] = reader["cinIndividu"].ToString();
                        dr["adresse"] = reader["adresse"].ToString();
                        dr["telephoneFixeIndividu"] = reader["telephoneFixeIndividu"].ToString();
                        dr["telephoneMobileIndividu"] = reader["telephoneMobileIndividu"].ToString();


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalProprietaire.insertToGridListeVoyageVoitureProprietaire(GridView gridView, string param, string paramLike, string valueLike, string numProprietaire)
        {
            #region declaration
            IntfDalProprietaire serviceProprietaire = new ImplDalProprietaire();
            #endregion

            #region implementation

            this.strCommande = "SELECT vehicule.matriculeVehicule, chauffeur.nomChauffeur, chauffeur.prenomChauffeur,";
            this.strCommande += " (autorisationdepart.numAutorisationDepart) AS numAD, vehicule.marqueVehicule, itineraire.numVilleItineraireDebut,";
            this.strCommande += " itineraire.numVilleItineraireFin, (fichebord.numerosFB) AS numFB, proprietaire.numProprietaire,";
            this.strCommande += " autorisationdepart.recetteTotale, autorisationdepart.resteRegle,";
            this.strCommande += " fichebord.dateHeurDepart FROM proprietaire";
            this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
            this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
            this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
            this.strCommande += " Left Join assocautorisationdepartfacture ON assocautorisationdepartfacture.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " WHERE assocautorisationdepartfacture.numAutorisationDepart IS NULL AND";
            this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceProprietaire.getDataTableVoyageVoitureProprietaire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalProprietaire.getDataTableVoyageVoitureProprietaire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalFicheBord serviceFicheBord = new ImplDalFicheBord();
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = new crlVille();
            crlVille villeF = new crlVille();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("dateVoyage", typeof(DateTime));
            dataTable.Columns.Add("Itineraire", typeof(string));
            dataTable.Columns.Add("recette", typeof(string));
            dataTable.Columns.Add("reste", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"];
                        dr["chauffeur"] = reader["nomChauffeur"].ToString() + " " + reader["prenomChauffeur"].ToString();
                        dr["dateVoyage"] = Convert.ToDateTime(reader["dateHeurDepart"].ToString());

                        villeD = serviceVille.selectVille(reader["numVilleItineraireDebut"].ToString());
                        villeF = serviceVille.selectVille(reader["numVilleItineraireFin"].ToString());
                        if (villeD != null && villeF != null)
                        {
                            dr["Itineraire"] = villeD.NomVille + "-" + villeF.NomVille;
                        }
                        else
                        {
                            dr["Itineraire"] = reader["numVilleItineraireDebut"].ToString() + "-" + reader["numVilleItineraireFin"].ToString();
                        }


                        dr["recette"] = serviceGeneral.separateurDesMilles(reader["recetteTotale"].ToString()) + "Ar";

                        dr["reste"] = serviceGeneral.separateurDesMilles(reader["resteRegle"].ToString()) + "Ar";

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalProprietaire.insertToGridProprietaireAll(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalProprietaire serviceProprietaire = new ImplDalProprietaire();
            #endregion

            #region implementation

            this.strCommande = "SELECT proprietaire.numProprietaire, proprietaire.typeProprietaire, proprietaire.numOrganisme,";
            this.strCommande += " proprietaire.numSociete, proprietaire.numIndividu, proprietaire.numAgence FROM proprietaire";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proprietaire.numOrganisme";
            this.strCommande += " Left Join societe ON societe.numSociete = proprietaire.numSociete";
            this.strCommande += " Left Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " WHERE proprietaire.numAgence = '" + numAgence + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceProprietaire.getDataTableProprietaireAll(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalProprietaire.getDataTableProprietaireAll(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlOrganisme organisme = null;
            crlSociete societe = null;
            crlIndividu Individu = null;

            IntfDalSociete serviceSociete = new ImplDalSociete();
            IntfDalOrganisme serviceOrganisme = new ImplDalOrganisme();
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numProprietaire", typeof(string));
            dataTable.Columns.Add("proprietaire", typeof(string));
            dataTable.Columns.Add("adresse", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("respSociete", typeof(string));
            dataTable.Columns.Add("respContact", typeof(string));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numProprietaire"] = reader["numProprietaire"].ToString();

                        if (reader["numIndividu"].ToString() != "")
                        {
                            Individu = serviceIndividu.selectIndividu(reader["numIndividu"].ToString());
                        }
                        if (reader["numOrganisme"].ToString() != "")
                        {
                            organisme = serviceOrganisme.selectOrganisme(reader["numOrganisme"].ToString());
                        }
                        if (reader["numSociete"].ToString() != "")
                        {
                            societe = serviceSociete.selectSociete(reader["numSociete"].ToString());
                        }

                        if (Individu != null)
                        {
                            dr["proprietaire"] = Individu.PrenomIndividu + " " + Individu.NomIndividu;

                            dr["adresse"] = Individu.Adresse;

                            dr["contact"] = Individu.TelephoneFixeIndividu + " / " + Individu.TelephoneMobileIndividu;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["proprietaire"] = societe.NomSociete;

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
                            dr["proprietaire"] = organisme.NomOrganisme;

                            dr["adresse"] = organisme.AdresseOrganisme;

                            dr["contact"] = organisme.TelephoneFixeOrganisme + " / " + organisme.TelephoneMobileOrganisme;

                            if (organisme.individuResponsable != null)
                            {
                                dr["respSociete"] = organisme.individuResponsable.PrenomIndividu + " " + organisme.individuResponsable.NomIndividu;

                                dr["respContact"] = organisme.individuResponsable.TelephoneFixeIndividu + " / " + organisme.individuResponsable.TelephoneMobileIndividu;
                            }
                        }

                        Individu = null;
                        societe = null;
                        organisme = null;
                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalProprietaire.insertToGridRecu(GridView gridView, string param, string paramLike, string valueLike, string numProprietaire)
        {
            #region declaration
            IntfDalProprietaire serviceProprietaire = new ImplDalProprietaire();
            #endregion

            #region implementation

            this.strCommande = "SELECT (recuad.numRecuAD) AS numR, typeprelevement.commentaire, recuad.matriculeAgent,";
            this.strCommande += " recuad.libele, recuad.montant, recuad.dateRecu FROM proprietaire";
            this.strCommande += " Inner Join vehicule ON vehicule.numProprietaire = proprietaire.numProprietaire";
            this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
            this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
            this.strCommande += " Inner Join prelevement ON prelevement.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
            this.strCommande += " Inner Join recuad ON recuad.numPrelevement = prelevement.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
            this.strCommande += " proprietaire.numProprietaire = '" + numProprietaire + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceProprietaire.getDataTableRecu(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalProprietaire.getDataTableRecu(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numR", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("libele", typeof(string));
            dataTable.Columns.Add("typeRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            DataRow dr;
            #endregion

            this.serviceConnection.openConnection();
            this.reader = this.serviceConnection.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numR"] = reader["numR"].ToString();
                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["libele"] = reader["libele"].ToString();
                        dr["typeRecuAD"] = reader["commentaire"].ToString();
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }
        #endregion
 
    }
}