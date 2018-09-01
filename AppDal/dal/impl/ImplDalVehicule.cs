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
    /// Description résumée de ImplDalVehicule
    /// </summary>
    public class ImplDalVehicule : IntfDalVehicule
    {
        #region declaration
        ImplDalConnectBase serviceConnection = null;
        IntfDalServiceRessource serviceRessource = null;
        MySqlDataReader reader = null;

        string strCommande = "";
        string strConnection = "";
        #endregion

        #region constructeur
        public ImplDalVehicule(string strConnection)
        {
            this.strConnection = strConnection;
            serviceConnection = new ImplDalConnectBase(strConnection);
        }
        public ImplDalVehicule()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.strConnection = this.serviceRessource.getDefaultStrConnection();
            this.serviceConnection = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region methode
        crlVehicule IntfDalVehicule.selectVehicule(string numVehicule)
        {
            #region declaration
            crlVehicule vehicule = null;

            IntfDalProprietaire serviceProprietaire = new ImplDalProprietaire();
            IntfDalSourceEnergie serviceSourceEnergie = new ImplDalSourceEnergie();
            IntfDalParamVehicule serviceParamVehicule = new ImplDalParamVehicule();
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT * FROM `vehicule` WHERE (`numVehicule`='" + numVehicule + "')";

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
                                vehicule = new crlVehicule();
                                vehicule.MarqueVehicule = this.reader["marqueVehicule"].ToString();
                                vehicule.NumParamVehicule = this.reader["numParamVehicule"].ToString();
                                vehicule.MatriculeVehicule = this.reader["matriculeVehicule"].ToString();
                                try
                                {
                                    vehicule.NombreColoneVehicule = int.Parse(this.reader["nombreColoneVehicule"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                                vehicule.NumMoteurVehicule = this.reader["numMoteurVehicule"].ToString();
                                vehicule.NumProprietaire = this.reader["numProprietaire"].ToString();
                                vehicule.NumSerieVehicule = this.reader["numSerieVehicule"].ToString();
                                vehicule.NumVehicule = this.reader["numVehicule"].ToString();
                                vehicule.CouleurVehicule = this.reader["couleurVehicule"].ToString();
                                try
                                {
                                    vehicule.PlacesAssiseVehicule = int.Parse(this.reader["placesAssiseVehicule"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                                try
                                {
                                    vehicule.PoidsTotalVehicule = double.Parse(this.reader["poidsTotalVehicule"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                                try
                                {
                                    vehicule.PoidsVideVehicule = double.Parse(this.reader["poidsVideVehicule"].ToString());
                                }
                                catch (Exception)
                                {
                                }
                                vehicule.PuissanceVehicule = this.reader["puissanceVehicule"].ToString();
                                vehicule.SourceEnergie = this.reader["sourceEnergie"].ToString();
                                vehicule.TypeVehicule = this.reader["typeVehicule"].ToString();
                                vehicule.ImageVehicule = this.reader["imageVehicule"].ToString();
                            }
                        }
                        this.reader.Dispose();
                    }

                    while (this.serviceConnection.IsConnection)
                        this.serviceConnection.closeConnection();
                }

                if (vehicule != null)
                {
                    if (vehicule.SourceEnergie != "")
                    {
                        vehicule.sourceEnergieObj = serviceSourceEnergie.selectSourceEnergie(vehicule.SourceEnergie);
                    }
                    if (vehicule.NumProprietaire != "")
                    {
                        vehicule.proprietaire = serviceProprietaire.selectProprietaire(vehicule.NumProprietaire);
                    }
                    if (vehicule.NumParamVehicule != "")
                    {
                        vehicule.paramVehicule = serviceParamVehicule.selectParamVehicule(vehicule.NumParamVehicule);
                    }
                }
            }
            #endregion

            return vehicule;
        }

        string IntfDalVehicule.insertVehicule(crlVehicule vehicule, string sigleAgence)
        {
            #region declaration
            string numVehicule = "";
            string numParamVehicule = "NULL"; 
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            int nombreInsert = 0;
            #endregion

            #region implementation
            if (vehicule != null)
            {
                if (vehicule.NumParamVehicule != "")
                {
                    numParamVehicule = "'" + vehicule.NumParamVehicule + "'";
                }

                if (sigleAgence != "")
                {
                    vehicule.NumVehicule = serviceVehicule.getNumVehicule(sigleAgence);

                    this.strCommande = "INSERT INTO `vehicule` (`numVehicule`,`numParamVehicule`,`sourceEnergie`,";
                    this.strCommande += " `numProprietaire`,`matriculeVehicule`,`marqueVehicule`,`typeVehicule`,";
                    this.strCommande += " `numSerieVehicule`,`numMoteurVehicule`,`puissanceVehicule`,`couleurVehicule`,";
                    this.strCommande += " `placesAssiseVehicule`,`nombreColoneVehicule`,`poidsTotalVehicule`,`poidsVideVehicule`,";
                    this.strCommande += " `imageVehicule`) VALUES ('" + vehicule.NumVehicule + "', " + numParamVehicule + ",";
                    this.strCommande += " '" + vehicule.SourceEnergie + "','" + vehicule.NumProprietaire + "',";
                    this.strCommande += " '" + vehicule.MatriculeVehicule.ToUpper() + "','" + vehicule.MarqueVehicule + "','" + vehicule.TypeVehicule + "',";
                    this.strCommande += " '" + vehicule.NumSerieVehicule + "','" + vehicule.NumMoteurVehicule + "',";
                    this.strCommande += " '" + vehicule.PuissanceVehicule + "','" + vehicule.CouleurVehicule + "','" + vehicule.PlacesAssiseVehicule + "',";
                    this.strCommande += " '" + vehicule.NombreColoneVehicule + "','" + vehicule.PoidsTotalVehicule + "',";
                    this.strCommande += " '" + vehicule.PoidsVideVehicule + "','" + vehicule.ImageVehicule + "')";

                    this.serviceConnection.openConnection();
                    nombreInsert = this.serviceConnection.requete(this.strCommande);
                    if (nombreInsert == 1)
                    {
                        numVehicule = vehicule.NumVehicule;
                    }
                    this.serviceConnection.closeConnection();
                }
            }
            #endregion

            return numVehicule;
        }

        bool IntfDalVehicule.upDateVehicule(crlVehicule vehicule)
        {
            #region declaration
            bool isUpdate = false;
            string numParamVehicule = "NULL"; 
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (vehicule != null)
            {
                if (vehicule.NumParamVehicule != "")
                {
                    numParamVehicule = "'" + vehicule.NumParamVehicule + "'";
                }

                this.strCommande = "UPDATE `vehicule` SET `couleurVehicule`='" + vehicule.CouleurVehicule + "',";
                this.strCommande += " `imageVehicule`='" + vehicule.ImageVehicule + "', `marqueVehicule`='" + vehicule.MarqueVehicule + "',";
                this.strCommande += " `matriculeVehicule`='" + vehicule.MatriculeVehicule + "',`nombreColoneVehicule`='" + vehicule.NombreColoneVehicule + "',";
                this.strCommande += " `numMoteurVehicule`='" + vehicule.NumMoteurVehicule + "',`numParamVehicule`=" + numParamVehicule + ",";
                this.strCommande += " `numProprietaire`='" + vehicule.NumProprietaire + "',`numSerieVehicule`='" + vehicule.NumSerieVehicule + "',";
                this.strCommande += " `placesAssiseVehicule`='" + vehicule.PlacesAssiseVehicule + "',`poidsTotalVehicule`='" + vehicule.PoidsTotalVehicule + "',";
                this.strCommande += " `poidsVideVehicule`='" + vehicule.PoidsVideVehicule + "',`puissanceVehicule`='" + vehicule.PuissanceVehicule + "',";
                this.strCommande += " `sourceEnergie`='" + vehicule.SourceEnergie + "',`typeVehicule`='" + vehicule.TypeVehicule + "'";
                this.strCommande += " WHERE `numVehicule`='" + vehicule.NumVehicule + "'";

                this.serviceConnection.openConnection();
                nombreUpdate = this.serviceConnection.requete(this.strCommande);
                if (nombreUpdate == 1)
                {
                    isUpdate = true;
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return isUpdate;
        }

        string IntfDalVehicule.isVehicule(crlVehicule vehicule)
        {
            #region declaration
            string numVehicule = "";
            #endregion

            #region implementation
            if (vehicule != null)
            {
                this.strCommande = "SELECT * FROM `vehicule` WHERE";
                this.strCommande += " `matriculeVehicule`='" + vehicule.MatriculeVehicule + "' AND";
                this.strCommande += " `numVehicule`<>'" + vehicule.NumVehicule + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            numVehicule = this.reader["numVehicule"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return numVehicule;
        }

        string IntfDalVehicule.getNumVehicule(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numVehicule = "00001";
            string[] tempNumVehicule = null;
            string strDate = "VE" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT vehicule.numVehicule AS maxNum FROM vehicule";
            this.strCommande += " WHERE vehicule.numVehicule LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnection.openConnection();
            reader = this.serviceConnection.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumVehicule = reader["maxNum"].ToString().ToString().Split('/');
                        numVehicule = tempNumVehicule[tempNumVehicule.Length - 1];
                    }
                    numTemp = double.Parse(numVehicule) + 1;
                    if (numTemp < 10)
                        numVehicule = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numVehicule = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numVehicule = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numVehicule = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numVehicule = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnection.closeConnection();
            numVehicule = strDate + "/" + sigleAgence + "/" + numVehicule;
            #endregion

            return numVehicule;
        }


        double IntfDalVehicule.getTotalRecette(string numVehicule)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.recetteTotale) AS prixTotal FROM vehicule";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " vehicule.numVehicule = '" + numVehicule + "'";

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

        double IntfDalVehicule.getTotalReste(string numVehicule)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT Sum(autorisationdepart.resteRegle) AS prixTotal FROM vehicule";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " vehicule.numVehicule = '" + numVehicule + "'";

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

        double IntfDalVehicule.getTotalRecu(string numVehicule)
        {
            #region declaration
            double prixTotal = 0.00;
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT Sum(recuad.montant) AS prixTotal FROM vehicule";
                this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
                this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
                this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
                this.strCommande += " Inner Join prelevement ON prelevement.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
                this.strCommande += " Inner Join recuad ON recuad.numPrelevement = prelevement.numPrelevement";
                this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
                this.strCommande += " vehicule.numVehicule = '" + numVehicule + "'";

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

        List<crlAutorisationDepart> IntfDalVehicule.getAutorisationDepartsForFacture(string numVehicule)
        {
            #region declaration
            List<crlAutorisationDepart> autorisationDepart = null;
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            #endregion

            #region implementation
            if (numVehicule != "")
            {
                this.strCommande = "SELECT autorisationdepart.numAutorisationDepart FROM autorisationdepart";
                this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
                this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
                this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
                this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
                this.strCommande += " WHERE licence.numVehicule = '" + numVehicule + "' AND";
                this.strCommande += " autorisationdepart.resteRegle > '0'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        autorisationDepart = new List<crlAutorisationDepart>();
                        while (this.reader.Read())
                        {
                            autorisationDepart.Add(serviceAutorisationDepart.selectAutorisationDepart(this.reader["numAutorisationDepart"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion

            return autorisationDepart;
        }

        void IntfDalVehicule.loadDdlVehiculeProprietaire(DropDownList ddl,string numProprietaire)
        {
            #region declaration
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT licence.numLicence, vehicule.matriculeVehicule FROM licence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " WHERE vehicule.numProprietaire = '" + numProprietaire + "'";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["matriculeVehicule"].ToString(), this.reader["numLicence"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion
        }

        void IntfDalVehicule.loadDdlVehiculeProprietaireUS(DropDownList ddl, string numProprietaire)
        {
            #region declaration
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add("");

                this.strCommande = "SELECT licence.numLicence, vehicule.matriculeVehicule FROM licence";
                this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
                this.strCommande += " WHERE vehicule.numProprietaire = '" + numProprietaire + "' AND";
                this.strCommande += " (licence.zone = 'Suburbaine' OR licence.zone = 'Urbaine')";

                this.serviceConnection.openConnection();
                this.reader = this.serviceConnection.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        while (this.reader.Read())
                        {
                            ddl.Items.Add(new ListItem(this.reader["matriculeVehicule"].ToString(), this.reader["numLicence"].ToString()));
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnection.closeConnection();
            }
            #endregion
        }

        #endregion

        #region insert to grid
        void IntfDalVehicule.insertToGridVehicule(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            #endregion

            #region implementation
            this.strCommande = "SELECT vehicule.numVehicule, vehicule.sourceEnergie, vehicule.matriculeVehicule,";
            this.strCommande += " vehicule.marqueVehicule, vehicule.typeVehicule, vehicule.couleurVehicule,";
            this.strCommande += " Individu.nomIndividu, Individu.prenomIndividu FROM vehicule";
            this.strCommande += " Inner Join proprietaire ON proprietaire.numProprietaire = vehicule.numProprietaire";
            this.strCommande += " Inner Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceVehicule.getDataTableVehicule(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVehicule.getDataTableVehicule(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numVehicule", typeof(string));
            dataTable.Columns.Add("matriculeVehicule", typeof(string));
            dataTable.Columns.Add("marqueVehicule", typeof(string));
            dataTable.Columns.Add("couleurVehicule", typeof(string));
            dataTable.Columns.Add("sourceEnergie", typeof(string));
            dataTable.Columns.Add("typeVehicule", typeof(string));
            dataTable.Columns.Add("propietaire", typeof(string));
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

                        dr["numVehicule"] = reader["numVehicule"].ToString();
                        dr["matriculeVehicule"] = reader["matriculeVehicule"].ToString();
                        dr["marqueVehicule"] = reader["marqueVehicule"].ToString();
                        dr["couleurVehicule"] = reader["couleurVehicule"].ToString();
                        dr["sourceEnergie"] = reader["sourceEnergie"].ToString();
                        dr["typeVehicule"] = reader["typeVehicule"].ToString();
                        dr["propietaire"] = reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalVehicule.insertToGridVehiculeForFacture(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            #endregion

            #region implementation
            this.strCommande = "SELECT vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.couleurVehicule, vehicule.numVehicule, proprietaire.numOrganisme,";
            this.strCommande += " proprietaire.numSociete, proprietaire.numIndividu FROM vehicule";
            this.strCommande += " Inner Join proprietaire ON proprietaire.numProprietaire = vehicule.numProprietaire";
            this.strCommande += " Left Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " Left Join societe ON societe.numSociete = proprietaire.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proprietaire.numOrganisme";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceVehicule.getDataTableVehiculeForFacture(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVehicule.getDataTableVehiculeForFacture(string strRqst)
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
            dataTable.Columns.Add("numVehicule", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("Individu", typeof(string));
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

                        dr["numVehicule"] = this.reader["numVehicule"].ToString();
                        dr["vehicule"] = this.reader["matriculeVehicule"].ToString() + " " + this.reader["marqueVehicule"].ToString() + " " + this.reader["couleurVehicule"].ToString();


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
                            dr["Individu"] = Individu.PrenomIndividu + " " + Individu.NomIndividu;

                            dr["adresse"] = Individu.Adresse;

                            dr["contact"] = Individu.TelephoneFixeIndividu + " / " + Individu.TelephoneMobileIndividu;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["Individu"] = societe.NomSociete;

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
                            dr["Individu"] = organisme.NomOrganisme;

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

        void IntfDalVehicule.insertToGridVehiculeListeNoire(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            #endregion

            #region implementation
            this.strCommande = "SELECT vehicule.matriculeVehicule, vehicule.marqueVehicule,";
            this.strCommande += " vehicule.couleurVehicule, vehicule.numVehicule, proprietaire.numOrganisme,";
            this.strCommande += " proprietaire.numSociete, proprietaire.numIndividu FROM vehicule";
            this.strCommande += " Inner Join proprietaire ON proprietaire.numProprietaire = vehicule.numProprietaire";
            this.strCommande += " Left Join Individu ON Individu.numIndividu = proprietaire.numIndividu";
            this.strCommande += " Left Join societe ON societe.numSociete = proprietaire.numSociete";
            this.strCommande += " Left Join organisme ON organisme.numOrganisme = proprietaire.numOrganisme";
            this.strCommande += " Inner Join observationvehicule ON observationvehicule.numVehicule = vehicule.numVehicule";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " observationvehicule.isListeNoire = '2'";
            this.strCommande += " GROUP BY vehicule.numVehicule";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceVehicule.getDataTableVehiculeListeNoire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVehicule.getDataTableVehiculeListeNoire(string strRqst)
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
            dataTable.Columns.Add("numVehicule", typeof(string));
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("Individu", typeof(string));
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

                        dr["numVehicule"] = this.reader["numVehicule"].ToString();
                        dr["vehicule"] = this.reader["matriculeVehicule"].ToString() + " " + this.reader["marqueVehicule"].ToString() + " " + this.reader["couleurVehicule"].ToString();


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
                            dr["Individu"] = Individu.PrenomIndividu + " " + Individu.NomIndividu;

                            dr["adresse"] = Individu.Adresse;

                            dr["contact"] = Individu.TelephoneFixeIndividu + " / " + Individu.TelephoneMobileIndividu;

                            dr["respSociete"] = "-";

                            dr["respContact"] = "-";
                        }
                        else if (societe != null)
                        {
                            dr["Individu"] = societe.NomSociete;

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
                            dr["Individu"] = organisme.NomOrganisme;

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
        #endregion

        void IntfDalVehicule.insertToGridADForVehicule(GridView gridView, string param, string paramLike, string valueLike, string numVehicule)
        {
            #region declaration
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            #endregion

            #region implementation
            this.strCommande = "SELECT autorisationdepart.recetteTotale, autorisationdepart.resteRegle,";
            this.strCommande += " vehicule.matriculeVehicule, vehicule.marqueVehicule, vehicule.couleurVehicule,";
            this.strCommande += " chauffeur.nomChauffeur, chauffeur.prenomChauffeur, fichebord.dateHeurDepart,";
            this.strCommande += " autorisationdepart.dateAD, verification.idItineraire FROM autorisationdepart";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosFB = autorisationdepart.numerosFB";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.numerosAV = fichebord.numerosAV";
            this.strCommande += " Inner Join verification ON verification.idVerification = autorisationvoyage.idVerification";
            this.strCommande += " Inner Join licence ON licence.numLicence = verification.numLicence";
            this.strCommande += " Inner Join vehicule ON vehicule.numVehicule = licence.numVehicule";
            this.strCommande += " Inner Join chauffeur ON chauffeur.idChauffeur = verification.idChauffeur";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " autorisationdepart.resteRegle > '0' AND";
            this.strCommande += " vehicule.numVehicule = '" + numVehicule + "'";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceVehicule.getDataTableADForVehicule(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVehicule.getDataTableADForVehicule(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            crlItineraire itineraire = null;
            IntfDalItineraire serviceItineraire = new ImplDalItineraire();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("vehicule", typeof(string));
            dataTable.Columns.Add("chauffeur", typeof(string));
            dataTable.Columns.Add("date", typeof(DateTime));
            dataTable.Columns.Add("itineraire", typeof(string));
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

                        itineraire = serviceItineraire.selectItineraire(this.reader["idItineraire"].ToString());

                        dr["vehicule"] = this.reader["matriculeVehicule"].ToString() + " " + this.reader["marqueVehicule"].ToString() + " " + this.reader["couleurVehicule"].ToString();
                        dr["chauffeur"] = this.reader["prenomChauffeur"].ToString() + " " + this.reader["nomChauffeur"].ToString();
                        try
                        {
                            dr["date"] = Convert.ToDateTime(this.reader["dateHeurDepart"].ToString());
                        }
                        catch (Exception)
                        {
                            dr["date"] = DateTime.Now;
                        }
                        if (itineraire != null)
                        {
                            dr["itineraire"] = itineraire.villeD.NomVille + "-" + itineraire.villeF.NomVille;
                        }
                        else
                        {
                            dr["itineraire"] = "-";
                        }
                        dr["recette"] = serviceGeneral.separateurDesMilles(this.reader["recetteTotale"].ToString()) + "Ar";
                        dr["reste"] = serviceGeneral.separateurDesMilles(this.reader["resteRegle"].ToString()) + "Ar";
                        

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnection.closeConnection();

            #endregion

            return dataTable;
        }


        void IntfDalVehicule.insertToGridRecuForVehicule(GridView gridView, string param, string paramLike, string valueLike, string numVehicule)
        {
            #region declaration
            IntfDalVehicule serviceVehicule = new ImplDalVehicule();
            #endregion

            #region implementation

            this.strCommande = "SELECT (recuad.numRecuAD) AS numR, typeprelevement.commentaire, recuad.matriculeAgent,";
            this.strCommande += " recuad.libele, recuad.montant, recuad.dateRecu FROM vehicule";
            this.strCommande += " Inner Join licence ON licence.numVehicule = vehicule.numVehicule";
            this.strCommande += " Inner Join verification ON verification.numLicence = licence.numLicence";
            this.strCommande += " Inner Join autorisationvoyage ON autorisationvoyage.idVerification = verification.idVerification";
            this.strCommande += " Inner Join fichebord ON fichebord.numerosAV = autorisationvoyage.numerosAV";
            this.strCommande += " Inner Join autorisationdepart ON autorisationdepart.numerosFB = fichebord.numerosFB";
            this.strCommande += " Inner Join prelevement ON prelevement.numAutorisationDepart = autorisationdepart.numAutorisationDepart";
            this.strCommande += " Inner Join recuad ON recuad.numPrelevement = prelevement.numPrelevement";
            this.strCommande += " Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE autorisationdepart.resteRegle > 0 AND";
            this.strCommande += " vehicule.numVehicule = '" + numVehicule + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceVehicule.getDataTableRecuForVehicule(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalVehicule.getDataTableRecuForVehicule(string strRqst)
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



        
    }
}