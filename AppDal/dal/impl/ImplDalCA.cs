using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;
using arch.crl;

namespace arch.dal.impl
{
    /// <summary>
    /// Description résumée de ImplDalCA
    /// </summary>
    public class ImplDalCA : IntfDalCA
    {
        #region declaration variable
        ImplDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCA()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCA(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region generale
        double IntfDalCA.montantCAGenerale(string dateDebut, string dateFin)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (dateDebut != "" && dateFin != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS montantTotal";
                this.strCommande += " FROM autorisationdepart";
                this.strCommande += " WHERE autorisationdepart.dateAD >= '" + dateDebut + "' AND";
                this.strCommande += " autorisationdepart.dateAD <= '" + dateFin + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["montantTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        void IntfDalCA.insertToGridCAGenerale(GridView gridView, string dateDebut, string dateFin)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationdepart.numAutorisationDepart, autorisationdepart.dateAD,";
            this.strCommande += " autorisationdepart.recetteTotale, vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.couleurVehicule FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE autorisationdepart.dateAD >= '" + dateDebut + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateFin + "'";

            gridView.DataSource = serviceCA.getDataTableCAGenerale(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableCAGenerale(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAutorisationDepart", typeof(string));
            dataTable.Columns.Add("dateAD", typeof(DateTime));
            dataTable.Columns.Add("recetteTotale", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
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

                        dr["numAutorisationDepart"] = reader["numAutorisationDepart"].ToString();
                        dr["dateAD"] = Convert.ToDateTime(reader["dateAD"].ToString());

                        dr["recetteTotale"] = serviceGeneral.separateurDesMilles(reader["recetteTotale"].ToString()) + "Ar";
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"] + " " + reader["couleurVehicule"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }



        double IntfDalCA.montantCAGeneraleVoiture(string dateDebut, string dateFin, string numVehicule)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (dateDebut != "" && dateFin != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS montantTotal FROM autorisationdepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " WHERE autorisationdepart.dateAD >= '" + dateDebut + "' AND";
                this.strCommande += " autorisationdepart.dateAD <= '" + dateFin + "' AND";
                this.strCommande += " vehicule.numVehicule = '" + numVehicule + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["montantTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        void IntfDalCA.insertToGridCAGeneraleVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationdepart.numAutorisationDepart, autorisationdepart.dateAD,";
            this.strCommande += " autorisationdepart.recetteTotale, vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.couleurVehicule FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE autorisationdepart.dateAD >= '" + dateDebut + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateFin + "' AND";
            this.strCommande += " vehicule.numVehicule = '" + numVehicule + "'";

            gridView.DataSource = serviceCA.getDataTableCAGeneraleVoiture(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableCAGeneraleVoiture(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAutorisationDepart", typeof(string));
            dataTable.Columns.Add("dateAD", typeof(DateTime));
            dataTable.Columns.Add("recetteTotale", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
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

                        dr["numAutorisationDepart"] = reader["numAutorisationDepart"].ToString();
                        dr["dateAD"] = Convert.ToDateTime(reader["dateAD"].ToString());

                        dr["recetteTotale"] = serviceGeneral.separateurDesMilles(reader["recetteTotale"].ToString()) + "Ar";
                        dr["vehicule"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"] + " " + reader["couleurVehicule"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }



        double IntfDalCA.montantCAGeneraleAxe(string dateDebut, string dateFin, string idItineraire)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (dateDebut != "" && dateFin != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS montantTotal FROM autorisationdepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
                this.strCommande += " WHERE autorisationdepart.dateAD >= '" + dateDebut + "' AND";
                this.strCommande += " autorisationdepart.dateAD <= '" + dateFin + "' AND";
                this.strCommande += " itineraire.idItineraire = '" + idItineraire + "'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["montantTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        void IntfDalCA.insertToGridCAGeneraleAxe(GridView gridView, string dateDebut, string dateFin, string idItineraire)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationdepart.numAutorisationDepart, autorisationdepart.dateAD, autorisationdepart.recetteTotale,";
            this.strCommande += " itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join itineraire ON itineraire.idItineraire = verification.idItineraire";
            this.strCommande += " WHERE autorisationdepart.dateAD >= '" + dateDebut + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateFin + "' AND";
            this.strCommande += " itineraire.idItineraire = '" + idItineraire + "'";

            gridView.DataSource = serviceCA.getDataTableCAGeneraleAxe(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableCAGeneraleAxe(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            crlVille VilleD = new crlVille();
            crlVille VilleF = new crlVille();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            IntfDalVille serviceVille = new ImplDalVille();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAutorisationDepart", typeof(string));
            dataTable.Columns.Add("dateAD", typeof(DateTime));
            dataTable.Columns.Add("recetteTotale", typeof(string));
            dataTable.Columns.Add("itineraire", typeof(string));
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

                        dr["numAutorisationDepart"] = reader["numAutorisationDepart"].ToString();
                        dr["dateAD"] = Convert.ToDateTime(reader["dateAD"].ToString());

                        dr["recetteTotale"] = serviceGeneral.separateurDesMilles(reader["recetteTotale"].ToString()) + "Ar";

                        VilleD = serviceVille.selectVille(reader["numVilleItineraireDebut"].ToString());
                        VilleF = serviceVille.selectVille(reader["numVilleItineraireFin"].ToString());

                        if (VilleD != null && VilleF != null)
                        {
                            dr["itineraire"] = VilleD.NomVille + "-" + VilleF.NomVille;
                        }
                        else
                        {
                            dr["itineraire"] = reader["numVilleItineraireDebut"].ToString() + "-" + reader["numVilleItineraireFin"].ToString();
                        }
                        


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

        #region developpement
        double IntfDalCA.montantCADeveloppement(string dateDebut, string dateFin)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (dateDebut != "" && dateFin != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS montantTotal FROM recuad";
                this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
                this.strCommande += " WHERE recuad.dateRecu >= '" + dateDebut + "' AND";
                this.strCommande += " recuad.dateRecu <= '" + dateFin + "' AND";
                this.strCommande += " prelevement.typePrelevement = 'Développement'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["montantTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        void IntfDalCA.insertToGridCADeveloppement(GridView gridView, string dateDebut, string dateFin)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.montant,";
            this.strCommande += " recuad.dateRecu, recuad.numPrelevement, typeprelevement.commentaire FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE typeprelevement.typePrelevement = 'Développement' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateDebut + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateFin + "'";

            gridView.DataSource = serviceCA.getDataTableCADeveloppement(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableCADeveloppement(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));
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
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["commentaire"] = reader["commentaire"].ToString();

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

        #region carburant
        double IntfDalCA.montantCACarburant(string dateDebut, string dateFin)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (dateDebut != "" && dateFin != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS montantTotal FROM recuad";
                this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
                this.strCommande += " WHERE recuad.dateRecu >= '" + dateDebut + "' AND";
                this.strCommande += " recuad.dateRecu <= '" + dateFin + "' AND";
                this.strCommande += " prelevement.typePrelevement = 'AvanceCarburant'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["montantTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        void IntfDalCA.insertToGridCACarburant(GridView gridView, string dateDebut, string dateFin)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.montant,";
            this.strCommande += " recuad.dateRecu, recuad.numPrelevement, typeprelevement.commentaire FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE typeprelevement.typePrelevement = 'AvanceCarburant' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateDebut + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateFin + "'";

            gridView.DataSource = serviceCA.getDataTableCACarburant(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableCACarburant(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));
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
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["commentaire"] = reader["commentaire"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        double IntfDalCA.montantCACarburantVoiture(string dateDebut, string dateFin, string numVehicule)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (dateDebut != "" && dateFin != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS montantTotal FROM recuad";
                this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
                this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " WHERE vehicule.numVehicule = '" + numVehicule + "' AND";
                this.strCommande += " recuad.dateRecu >= '" + dateDebut + "' AND";
                this.strCommande += " recuad.dateRecu <= '" + dateFin + "' AND";
                this.strCommande += " prelevement.typePrelevement = 'AvanceCarburant'";

                this.serviceConnectBase.openConnection();
                this.reader = serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            try
                            {
                                prixTotal = double.Parse(this.reader["montantTotal"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return prixTotal;
        }

        void IntfDalCA.insertToGridCACarburantVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.montant,";
            this.strCommande += " recuad.dateRecu, recuad.numPrelevement, typeprelevement.commentaire FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE vehicule.numVehicule = '" + numVehicule + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateDebut + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateFin + "' AND";
            this.strCommande += " prelevement.typePrelevement = 'AvanceCarburant'";

            gridView.DataSource = serviceCA.getDataTableCACarburantVoiture(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableCACarburantVoiture(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));
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
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["commentaire"] = reader["commentaire"].ToString();

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

        #region CA Par agence
        double IntfDalCA.montantCAGenerale(string dateDebut, string dateFin, string numAgence)
        {
            throw new NotImplementedException();
        }

        void IntfDalCA.insertToGridCAGenerale(GridView gridView, string dateDebut, string dateFin, string numAgence)
        {
            throw new NotImplementedException();
        }

        double IntfDalCA.montantCAGeneraleVoiture(string dateDebut, string dateFin, string numVehicule, string numAgence)
        {
            throw new NotImplementedException();
        }

        void IntfDalCA.insertToGridCAGeneraleVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule, string numAgence)
        {
            throw new NotImplementedException();
        }

        double IntfDalCA.montantCAGeneraleAxe(string dateDebut, string dateFin, string idItineraire, string numAgence)
        {
            throw new NotImplementedException();
        }

        void IntfDalCA.insertToGridCAGeneraleAxe(GridView gridView, string dateDebut, string dateFin, string idItineraire, string numAgence)
        {
            throw new NotImplementedException();
        }

        double IntfDalCA.montantCACarburant(string dateDebut, string dateFin, string numAgence)
        {
            throw new NotImplementedException();
        }

        void IntfDalCA.insertToGridCACarburant(GridView gridView, string dateDebut, string dateFin, string numAgence)
        {
            throw new NotImplementedException();
        }

        double IntfDalCA.montantCACarburantVoiture(string dateDebut, string dateFin, string numVehicule, string numAgence)
        {
            throw new NotImplementedException();
        }

        void IntfDalCA.insertToGridCACarburantVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule, string numAgence)
        {
            throw new NotImplementedException();
        }

        double IntfDalCA.montantCADeveloppement(string dateDebut, string dateFin, string numAgence)
        {
            throw new NotImplementedException();
        }

        void IntfDalCA.insertToGridCADeveloppement(GridView gridView, string dateDebut, string dateFin, string numAgence)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IntfDalCA Members


        double IntfDalCA.getCAGenerale(string dateD, string dateF, string numAgence)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS totalRecette";
            this.strCommande += " FROM autorisationdepart";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationdepart.matriculeAgent";
            this.strCommande += " WHERE agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateF + "' AND";
            this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null) 
            {
                if (this.reader.HasRows) 
                {
                    if (this.reader.Read()) 
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception) { }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        double IntfDalCA.getCAGeneraleVoiture(string dateD, string dateF, string numVehicule, string numAgence)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS totalRecette";
            this.strCommande += " FROM autorisationdepart";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationdepart.matriculeAgent";
            this.strCommande += "Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " WHERE agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateF + "' AND";
            this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "' AND";
            this.strCommande += " licence.numVehicule = '" + numVehicule + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception) { }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        double IntfDalCA.getCAGeneraleAxe(string dateD, string dateF, string idItineraire, string numAgence)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS totalRecette";
            this.strCommande += " FROM autorisationdepart";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = autorisationdepart.matriculeAgent";
            this.strCommande += "Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " WHERE agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateF + "' AND";
            this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "' AND";
            this.strCommande += " verification.idItineraire = '" + idItineraire + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception) { }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        double IntfDalCA.getCACarburant(string dateD, string dateF, string numAgence)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(recuad.montant) AS totalRecette FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = recuad.matriculeAgent";
            this.strCommande += " WHERE prelevement.typePrelevement = 'AvanceCarburant' AND";
            this.strCommande += " agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateF + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateD + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception) { }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        double IntfDalCA.getCACarburantVoiture(string dateD, string dateF, string numVehicule, string numAgence)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(recuad.montant) AS totalRecette FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = recuad.matriculeAgent";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " WHERE prelevement.typePrelevement = 'AvanceCarburant' AND";
            this.strCommande += " agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateF + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateD + "' AND";
            this.strCommande += " licence.numVehicule = '" + numVehicule + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception) { }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        double IntfDalCA.getCADeveloppement(string dateD, string dateF, string numAgence)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(recuad.montant) AS totalRecette FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = recuad.matriculeAgent";
            this.strCommande += " WHERE prelevement.typePrelevement =  'Développement' AND";
            this.strCommande += " agent.numAgence = '" + numAgence + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateF + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateD + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception) { }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        #endregion

        #region getCA par proprietaire
        double IntfDalCA.getRecetteProprietaire(string dateD, string dateF, string numProprietaire)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS totalRecette FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE vehicule.numProprietaire = '" + numProprietaire + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateF + "' AND";
            this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        double IntfDalCA.getResteProprietaire(string dateD, string dateF, string numProprietaire)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(autorisationdepart.resteRegle) AS totalRecette FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE vehicule.numProprietaire = '" + numProprietaire + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateF + "' AND";
            this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        void IntfDalCA.insertToGridListeAD(GridView gridView, string dateD, string dateF, string numProprietaire)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationdepart.numAutorisationDepart, autorisationdepart.numerosFB,";
            this.strCommande += " autorisationdepart.matriculeAgent, autorisationdepart.dateAD, autorisationdepart.recetteTotale,";
            this.strCommande += " autorisationdepart.resteRegle, vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.couleurVehicule FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE vehicule.numProprietaire = '" + numProprietaire + "' AND";
            this.strCommande += " autorisationdepart.dateAD <= '" + dateF + "' AND";
            this.strCommande += " autorisationdepart.dateAD >= '" + dateD + "'";
            this.strCommande += " GROUP BY autorisationdepart.numAutorisationDepart ORDER BY autorisationdepart.numAutorisationDepart ASC";

            gridView.DataSource = serviceCA.getDataTableListeAD(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableListeAD(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numAutorisationDepart", typeof(string));
            dataTable.Columns.Add("dateAD", typeof(DateTime));
            dataTable.Columns.Add("recette", typeof(string));
            dataTable.Columns.Add("reste", typeof(string));
            dataTable.Columns.Add("voiture", typeof(string));
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

                        dr["numAutorisationDepart"] = reader["numAutorisationDepart"].ToString();
                        dr["dateAD"] = Convert.ToDateTime(reader["dateAD"].ToString());



                        dr["recette"] = serviceGeneral.separateurDesMilles(reader["recetteTotale"].ToString()) + "Ar";
                        dr["reste"] = serviceGeneral.separateurDesMilles(reader["resteRegle"].ToString()) + "Ar";
                        dr["voiture"] = reader["matriculeVehicule"].ToString() + " " + reader["marqueVehicule"].ToString() + " " + reader["couleurVehicule"].ToString();


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

        #region getCA Fond par vehicule
        double IntfDalCA.getFondVehicule(string dateD, string dateF, string numVehicule)
        {
            #region declaration
            double totalRecette = 0.00;
            #endregion

            #region declaration
            this.strCommande = "SELECT Sum(recuad.montant) AS totalRecette FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE prelevement.typePrelevement = 'Fond' AND";
            this.strCommande += " vehicule.numVehicule = '" + numVehicule + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateD + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateF + "'";

            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(this.strCommande);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    if (this.reader.Read())
                    {
                        try
                        {
                            totalRecette = double.Parse(this.reader["totalRecette"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return totalRecette;
        }

        void IntfDalCA.insertToGridListeRecuAD(GridView gridView, string dateD, string dateF, string numVehicule)
        {
            #region declaration
            IntfDalCA serviceCA = new ImplDalCA();
            #endregion

            #region implementation
            this.strCommande = "SELECT recuad.numRecuAD, recuad.matriculeAgent, recuad.libele, recuad.dateRecu,";
            this.strCommande += " recuad.numPrelevement, recuad.montant FROM recuad";
            this.strCommande += " Inner Join prelevement ON prelevement.numPrelevement = recuad.numPrelevement";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numAutorisationDepart = prelevement.numAutorisationDepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " WHERE prelevement.typePrelevement = 'Fond' AND";
            this.strCommande += " vehicule.numVehicule =  '" + numVehicule + "' AND";
            this.strCommande += " recuad.dateRecu >= '" + dateD + "' AND";
            this.strCommande += " recuad.dateRecu <= '" + dateF + "'";
            this.strCommande += " GROUP BY recuad.numRecuAD ORDER BY recuad.numRecuAD ASC";

            gridView.DataSource = serviceCA.getDataTableListeRecuAD(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalCA.getDataTableListeRecuAD(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecuAD", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));
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
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());

                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["commentaire"] = reader["libele"].ToString();

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