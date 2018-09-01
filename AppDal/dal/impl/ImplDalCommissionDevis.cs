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
using MySql.Data.MySqlClient;
using arch.crl;


namespace arch.dal.impl
{
    /// <summary>
    /// Summary description for ImplDalCommissionDevis
    /// </summary>
    public class ImplDalCommissionDevis : IntfDalCommissionDevis
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalCommissionDevis()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalCommissionDevis(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion



        #region IntfDalCommissionDevis Members

        string IntfDalCommissionDevis.insertCommissionDevis(crlCommissionDevis CommissionDevis, string sigleAgence)
        {
            #region declaration
            IntfDalCommissionDevis serviceCommissionDevis = new ImplDalCommissionDevis();
            int nombreInsertion = 0;
            string idCommissionDevis = "";
            string numRecepteur = "NULL";
            #endregion

            #region implementation
            if (CommissionDevis != null)
            {
                if (CommissionDevis.NumRecepteur != "")
                {
                    numRecepteur = "'" + CommissionDevis.NumRecepteur + "'";
                }

                CommissionDevis.IdCommissionDevis = serviceCommissionDevis.getidCommissionDevis(sigleAgence);
                this.strCommande = "INSERT INTO `commissiondevis` (`idCommissionDevis`,`destination`,`poids`,`nombre`,`pieceJustificatif`,";
                this.strCommande += " `fraisEnvoi`,`numDesignation`,`typeCommission`,`numTrajet`,`numProforma`,`numExpediteur`,`numRecepteur`)";
                this.strCommande += " VALUES ('" + CommissionDevis.IdCommissionDevis + "','" + CommissionDevis.Destination + "',";
                this.strCommande += "'" + CommissionDevis.Poids + "','" + CommissionDevis.Nombre + "','" + CommissionDevis.PieceJustificatif + "',";
                this.strCommande += "'" + CommissionDevis.FraisEnvoi.ToString("0") + "','" + CommissionDevis.NumDesignation + "',";
                this.strCommande += "'" + CommissionDevis.TypeCommission + "','" + CommissionDevis.NumTrajet + "','" + CommissionDevis.NumProforma + "',";
                this.strCommande += "'" + CommissionDevis.NumExpediteur + "'," + numRecepteur + ")";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idCommissionDevis = CommissionDevis.IdCommissionDevis;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return idCommissionDevis;
        }

        bool IntfDalCommissionDevis.updateCommissionDevis(crlCommissionDevis CommissionDevis)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            string numRecepteur = "NULL";
            #endregion

            #region implementation
            if (CommissionDevis != null)
            {
                if (CommissionDevis.IdCommissionDevis != "")
                {
                    if (CommissionDevis.NumRecepteur != "")
                    {
                        numRecepteur = "'" + CommissionDevis.NumRecepteur + "'";
                    }

                    this.strCommande = "UPDATE `commissiondevis` SET ";
                    this.strCommande += "`destination`='" + CommissionDevis.Destination + "', `poids`='" + CommissionDevis.Poids + "', `nombre`='" + CommissionDevis.Nombre + "',";
                    this.strCommande += "`pieceJustificatif`='" + CommissionDevis.PieceJustificatif + "', `fraisEnvoi`='" + CommissionDevis.FraisEnvoi.ToString("0") + "', ";
                    this.strCommande += "`numDesignation`='" + CommissionDevis.NumDesignation + "', `typeCommission`='" + CommissionDevis.TypeCommission + "', ";
                    this.strCommande += "`numProforma`='" + CommissionDevis.NumProforma + "', `numTrajet`='" + CommissionDevis.NumTrajet + "',";
                    this.strCommande += "`numExpediteur`='" + CommissionDevis.NumExpediteur + "', `numRecepteur`=" + numRecepteur;
                    this.strCommande += " WHERE (`idCommissionDevis`='" + CommissionDevis.IdCommissionDevis + "')";

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

        crlCommissionDevis IntfDalCommissionDevis.selectCommissionDevis(string idCommissionDevis)
        {
            #region initialisation
            IntfDalDesignationCommission serviceDesignationCommission = new ImplDalDesignationCommission();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalClient serviceClient = new ImplDalClient();
            IntfDalReceptionnaire serviceReceptionnaire = new ImplDalReceptionnaire();

            crlCommissionDevis CommissionDevis = null;
            #endregion

            #region implementation
            if (idCommissionDevis != "")
            {
                this.strCommande = "SELECT * FROM `commissiondevis` WHERE (`idCommissionDevis`='" + idCommissionDevis + "')";
                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        CommissionDevis = new crlCommissionDevis();
                        CommissionDevis.Destination = reader["destination"].ToString();
                        try
                        {
                            CommissionDevis.FraisEnvoi = double.Parse(reader["fraisEnvoi"].ToString());
                        }
                        catch (Exception) { }
                        CommissionDevis.IdCommissionDevis = reader["idCommissionDevis"].ToString();
                        CommissionDevis.PieceJustificatif = reader["pieceJustificatif"].ToString();
                        try
                        {
                            CommissionDevis.Poids = double.Parse(reader["poids"].ToString());
                        }
                        catch(Exception) { }
                        CommissionDevis.TypeCommission = reader["typeCommission"].ToString();
                        CommissionDevis.NumDesignation = reader["numDesignation"].ToString();
                        try
                        {
                            CommissionDevis.Nombre = int.Parse(reader["nombre"].ToString());
                        }
                        catch (Exception) { }
                        CommissionDevis.NumTrajet = reader["numTrajet"].ToString();
                        CommissionDevis.NumProforma = reader["numProforma"].ToString();
                        CommissionDevis.NumExpediteur = reader["numExpediteur"].ToString();
                        CommissionDevis.NumRecepteur = reader["numRecepteur"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (CommissionDevis != null)
                {
                    if (CommissionDevis.NumDesignation != "") 
                    {
                        CommissionDevis.designationCommission = serviceDesignationCommission.selectDesignationCommission(CommissionDevis.NumDesignation);
                    }
                    if (CommissionDevis.NumTrajet != "") 
                    {
                        CommissionDevis.trajet = serviceTrajet.selectTrajet(CommissionDevis.NumTrajet);
                    }
                    if (CommissionDevis.NumExpediteur != "")
                    {
                        CommissionDevis.expediteur= serviceClient.selectClient(CommissionDevis.NumExpediteur);
                    }
                    if (CommissionDevis.NumRecepteur != "")
                    {
                        CommissionDevis.recepteur = serviceReceptionnaire.selectPersonne(CommissionDevis.NumRecepteur);
                    }
                }
            }
            #endregion

            return CommissionDevis;
        }

        string IntfDalCommissionDevis.getidCommissionDevis(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string idCommissionDevis = "00001";
            string[] tempIdCommissionDevis = null;
            string strDate = "CD" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT commissiondevis.idCommissionDevis AS maxNum FROM commissiondevis";
            this.strCommande += " WHERE commissiondevis.idCommissionDevis LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdCommissionDevis = reader["maxNum"].ToString().ToString().Split('/');
                        idCommissionDevis = tempIdCommissionDevis[tempIdCommissionDevis.Length - 1];
                    }
                    numTemp = double.Parse(idCommissionDevis) + 1;
                    if (numTemp < 10)
                        idCommissionDevis = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        idCommissionDevis = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        idCommissionDevis = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        idCommissionDevis = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        idCommissionDevis = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idCommissionDevis = strDate + "/" + sigleAgence + "/" + idCommissionDevis;
            #endregion

            return idCommissionDevis;
        }

        crlCommission IntfDalCommissionDevis.getCommission(crlCommissionDevis CommissionDevis, crlAgent agent)
        {
            #region declaration
            crlCommission commission = null;
            #endregion

            #region implementation
            if (CommissionDevis != null && agent != null)
            {
                commission = new crlCommission();
                commission.agent = agent;
                commission.designationCommission = CommissionDevis.designationCommission;
                commission.Destination = CommissionDevis.Destination;
                commission.expediteur = CommissionDevis.expediteur;
                commission.FraisEnvoi = CommissionDevis.FraisEnvoi.ToString();
                commission.MatriculeAgent = agent.matriculeAgent;
                commission.ModePaiement = "Commande";
                commission.Nombre = CommissionDevis.Nombre;
                commission.NumDesignation = CommissionDevis.NumDesignation;
                commission.NumExpediteur = CommissionDevis.NumExpediteur;
                commission.NumRecepteur = CommissionDevis.NumRecepteur;
                commission.NumTrajet = CommissionDevis.NumTrajet;
                commission.PieceJustificatif = CommissionDevis.PieceJustificatif;
                commission.Poids = CommissionDevis.Poids.ToString("0.00");
                commission.recepteur = CommissionDevis.recepteur;
                commission.TypeCommission = CommissionDevis.TypeCommission;
                commission.typeCommssionObjet = CommissionDevis.typeCommssionObjet;
            }
            #endregion

            return commission;
        }
        #endregion

        #region insert to grid

        void IntfDalCommissionDevis.insertToGridCommissionDevis(GridView gridView, string param, string paramLike, string valueLike, string numProforma)
        {
            #region declaration
            IntfDalCommissionDevis serviceCommissionDevis = new ImplDalCommissionDevis();
            #endregion

            #region implementation

            this.strCommande = "SELECT commissiondevis.idCommissionDevis, commissiondevis.destination, commissiondevis.poids,";
            this.strCommande += " commissiondevis.nombre, commissiondevis.pieceJustificatif, commissiondevis.fraisEnvoi,";
            this.strCommande += " commissiondevis.numDesignation, commissiondevis.typeCommission, commissiondevis.numTrajet,";
            this.strCommande += " commissiondevis.numProforma FROM commissiondevis";
            this.strCommande += " WHERE commissiondevis.numProforma = '" + numProforma + "' AND";
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;

            gridView.DataSource = serviceCommissionDevis.getDataTableCommissionDevis(this.strCommande);
            gridView.DataBind();

            #endregion
        }

        DataTable IntfDalCommissionDevis.getDataTableCommissionDevis(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            crlTrajet trajet = null;
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("idCommissionDevis", typeof(string));
            dataTable.Columns.Add("destination", typeof(string));
            dataTable.Columns.Add("prixTotalfraisEnvoi", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));
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

                        dr["idCommissionDevis"] = reader["idCommissionDevis"].ToString();
                        dr["destination"] = reader["destination"].ToString();

                        dr["prixTotalfraisEnvoi"] = serviceGeneral.separateurDesMilles(this.reader["fraisEnvoi"].ToString()) + "Ar";

                        trajet = serviceTrajet.selectTrajet(this.reader["numTrajet"].ToString());
                        if (trajet != null)
                        {
                            dr["trajet"] = trajet.villeD.NomVille + "-" + trajet.villeF.NomVille;
                        }
                        else
                        {
                            dr["trajet"] = "";
                        }
                        trajet = null;
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
