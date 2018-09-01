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
    /// Implementation du service Recu
    /// </summary>
    public class ImplDalRecu : IntfDalRecu
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalRecu(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalRecu() 
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalRecu Members

        string IntfDalRecu.insertRecu(crlRecu Recu)
        {
            #region declaration
            IntfDalRecu serviceRecu = new ImplDalRecu();
            int nombreInsertion = 0;
            string numRecu = "";
            #endregion

            #region implementation
            if (Recu != null)
            {
                Recu.NumRecu = serviceRecu.getNumRecu();
                this.strCommande = "INSERT INTO `recu` (`numRecu`,`libele`,`montant`,`date`,`modePaiement`,`matriculeAgent`)";
                this.strCommande += " VALUES ('" + Recu.NumRecu + "', '" + Recu.Libele + "', '" + Recu.Montant + "',";
                this.strCommande += " '" + Recu.Date.ToString("yyyy-MM-dd") + "','" + Recu.ModePaiement + "','" + Recu.MatriculeAgent + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numRecu = Recu.NumRecu;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numRecu;
        }

        bool IntfDalRecu.deleteRecu(crlRecu Recu)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (Recu != null)
            {
                if (Recu.NumRecu != "")
                {
                    this.strCommande = "DELETE FROM `recu` WHERE (`numRecu` = '" + Recu.NumRecu + "')";
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

        bool IntfDalRecu.deleteRecu(string numRecu)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
           
            if (numRecu != "")
            {
                this.strCommande = "DELETE FROM `recu` WHERE (`numRecu` = '" + numRecu + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
          
            #endregion

            return isDelete;
        }

        bool IntfDalRecu.updateRecu(crlRecu Recu)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Recu != null)
            {
                if (Recu.NumRecu != "")
                {
                    this.strCommande = "UPDATE `recu` SET `libele`='" + Recu.Libele + "', `modePaiement`='" + Recu.ModePaiement + "', ";
                    this.strCommande += "`montant`='" + Recu.Montant + "', `date`='" + Recu.Date.ToString("yyyy-MM-dd") + "',`matriculeAgent`='" + Recu.MatriculeAgent + "'";
                    this.strCommande += "WHERE (`numRecu`='" + Recu.NumRecu + "')";

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

        crlRecu IntfDalRecu.selectRecu(string numRecu)
        {
            #region declaration
            crlRecu Recu = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numRecu != "")
            {
                this.strCommande = "SELECT * FROM `recu` WHERE (`numRecu`='" + numRecu + "')";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Recu = new crlRecu();
                        reader.Read();
                        Recu.NumRecu = reader["numRecu"].ToString();
                        Recu.Libele = reader["libele"].ToString();
                        Recu.Montant = reader["montant"].ToString();
                        Recu.ModePaiement = reader["modePaiement"].ToString();
                        Recu.MatriculeAgent = reader["matriculeAgent"].ToString();
                        try
                        {
                            Recu.Date = Convert.ToDateTime(reader["date"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (Recu != null)
                {
                    if(Recu.MatriculeAgent != "")
                    {
                        Recu.agent = serviceAgent.selectAgent(Recu.MatriculeAgent);
                    }
                }
            }
            #endregion

            return Recu;
        }

        crlRecu IntfDalRecu.isValideRecu(string numRecu)
        {
            #region declaration
            crlRecu Recu = null;

            IntfDalAgent serviceAgent = new ImplDalAgent();
            #endregion

            #region implementation
            if (numRecu != "")
            {
                this.strCommande = "SELECT recu.numRecu, recu.libele, recu.montant, recu.`date`,";
                this.strCommande += " recu.modePaiement, recu.matriculeAgent FROM recu";
                this.strCommande += " Left Join bagage ON bagage.numRecu = recu.numRecu";
                this.strCommande += " Left Join commission ON commission.numRecu = recu.numRecu";
                this.strCommande += " WHERE bagage.numRecu IS NULL AND commission.numRecu IS NULL AND";
                this.strCommande += " recu.numRecu ='" + numRecu + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Recu = new crlRecu();
                        reader.Read();
                        Recu.NumRecu = reader["numRecu"].ToString();
                        Recu.Libele = reader["libele"].ToString();
                        Recu.Montant = reader["montant"].ToString();
                        Recu.ModePaiement = reader["modePaiement"].ToString();
                        Recu.MatriculeAgent = reader["matriculeAgent"].ToString();
                        try
                        {
                            Recu.Date = Convert.ToDateTime(reader["date"].ToString());
                        }
                        catch (Exception)
                        {
                        }
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
                }
            }
            #endregion

            return Recu;
        }

        string IntfDalRecu.getNumRecu()
        {
            #region declaration
            int numTemp = 0;
            string NumRecu = "00001";
            string[] tempNumRecu = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT recu.numRecu AS maxNum FROM recu ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumRecu = reader["maxNum"].ToString().ToString().Split('/');
                        NumRecu = tempNumRecu[0];
                    }
                    numTemp = int.Parse(NumRecu) + 1;
                    if (numTemp < 10)
                        NumRecu = "0000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        NumRecu = "000" + numTemp;
                    if (numTemp < 1000 && numTemp >= 100)
                        NumRecu = "00" + numTemp;
                    if (numTemp < 10000 && numTemp >= 1000)
                        NumRecu = "0" + numTemp;
                    if (numTemp >= 10000)
                        NumRecu = "" + numTemp;
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            NumRecu = NumRecu + "/RE/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return NumRecu;
        }

        void IntfDalRecu.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        double IntfDalRecu.getTotalRecuEncaisser(string matriculeAgent, DateTime dateDebut, DateTime dateFin)
        {
            #region declaration
            double totalEncaisser = 0.00;
            #endregion

            #region implementation
            if (matriculeAgent != "")
            {
                this.strCommande = "SELECT Sum(recu.montant) AS totalEncaisser FROM recu";
                this.strCommande += " WHERE recu.matriculeAgent = '" + matriculeAgent + "' AND";
                this.strCommande += " recu.modePaiement =  'Espèce' AND";
                this.strCommande += " recu.`date` <= '" + dateFin.ToString("yyyy-MM-dd") + "' AND";
                this.strCommande += " recu.`date` >= '" + dateDebut.ToString("yyyy-MM-dd") + "'";

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
                                totalEncaisser = double.Parse(reader["totalEncaisser"].ToString());
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


        #region insert to grid
        void IntfDalRecu.insertToGridRecu(GridView gridView, string param, string paramLike, string valueLike, string numAgence)
        {
            #region declaration
            IntfDalRecu serviceRecu = new ImplDalRecu();
            #endregion

            #region implementation

            this.strCommande = "SELECT (recu.numRecu) As NRecu, recu.libele, recu.montant, (recu.`date`) As dateRecu,";
            this.strCommande += " recu.modePaiement, recu.matriculeAgent FROM recu";
            this.strCommande += " Left Join bagage ON bagage.numRecu = recu.numRecu";
            this.strCommande += " Left Join commission ON commission.numRecu = recu.numRecu";
            this.strCommande += " Inner Join agent ON agent.matriculeAgent = recu.matriculeAgent";
            this.strCommande += " WHERE bagage.numRecu IS NULL AND";
            this.strCommande += " commission.numRecu IS NULL  AND";
            this.strCommande += " agent.numAgence =  '" + numAgence + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceRecu.getDataTableRecu(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalRecu.getDataTableRecu(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("NRecu", typeof(string));
            dataTable.Columns.Add("libele", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("modePaiement", typeof(string));
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

                        dr["NRecu"] = reader["NRecu"].ToString();
                        dr["libele"] = reader["libele"].ToString();
                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());
                        dr["modePaiement"] = reader["modePaiement"].ToString();

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }

        void IntfDalRecu.insertToGridRecuEncaisser(GridView gridView, string param, string paramLike, string valueLike, string matriculeAgent, DateTime dateDebut, DateTime dateFin)
        {
            #region declaration
            IntfDalRecu serviceRecu = new ImplDalRecu();
            #endregion

            #region implementation

            this.strCommande = "SELECT recu.numRecu, recu.libele, recu.montant, (recu.`date`) As dateRecu,";
            this.strCommande += " recu.modePaiement, recu.matriculeAgent FROM recu";
            this.strCommande += " WHERE recu.matriculeAgent = '" + matriculeAgent + "' AND";
            this.strCommande += " recu.modePaiement = 'Espèce' AND";
            this.strCommande += " recu.`date` <= '" + dateFin.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " recu.`date` >= '" + dateDebut.ToString("yyyy-MM-dd") + "' AND";
            this.strCommande += " (" + paramLike + " LIKE  '%" + valueLike + "%')";
            this.strCommande += " ORDER BY " + param + " ASC";


            gridView.DataSource = serviceRecu.getDataTableRecuEncaisser(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalRecu.getDataTableRecuEncaisser(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numRecu", typeof(string));
            dataTable.Columns.Add("libele", typeof(string));
            dataTable.Columns.Add("montant", typeof(string));
            dataTable.Columns.Add("dateRecu", typeof(DateTime));
            dataTable.Columns.Add("modePaiement", typeof(string));
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

                        dr["numRecu"] = reader["numRecu"].ToString();
                        dr["libele"] = reader["libele"].ToString();
                        dr["montant"] = serviceGeneral.separateurDesMilles(reader["montant"].ToString()) + "Ar";
                        dr["dateRecu"] = Convert.ToDateTime(reader["dateRecu"].ToString());
                        dr["modePaiement"] = reader["modePaiement"].ToString();

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