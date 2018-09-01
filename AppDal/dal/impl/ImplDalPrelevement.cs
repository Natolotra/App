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
    /// Description résumée de ImplDalPrelevement
    /// </summary>
    public class ImplDalPrelevement : IntfDalPrelevement
    {
        #region declaration variable
        ImplDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalPrelevement()
        {
            this.serviceRessource = new ImplDalServiceRessource();
            this.serviceConnectBase = new ImplDalConnectBase(this.serviceRessource.getDefaultStrConnection());
        }
        public ImplDalPrelevement(string strConnection)
        {
            this.serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion


        #region mothode
        string IntfDalPrelevement.insertPrelevement(crlPrelevement prelevement)
        {
            #region declaration
            int nombreInsertion = 0;
            string numPrelevement = "";
            IntfDalPrelevement servicePrelevement = new ImplDalPrelevement();
            #endregion

            #region implementation
            if (prelevement != null)
            {
                prelevement.NumPrelevement = servicePrelevement.getNumPrelevement(prelevement.agent.agence.SigleAgence);

                this.strCommande = "INSERT INTO `prelevement` (`numPrelevement`,`matriculeAgent`";
                this.strCommande += " ,`typePrelevement`,`numAutorisationDepart`,`montantPrelevement`,`datePrelevement`) ";
                this.strCommande += " VALUES ('" + prelevement.NumPrelevement + "','" + prelevement.MatriculeAgent + "'";
                this.strCommande += " ,'" + prelevement.TypePrelevement + "','" + prelevement.NumAutorisationDepart + "'";
                this.strCommande += " ,'" + prelevement.MontantPrelevement + "','" + prelevement.DatePrelevement.ToString("yyyy-MM-dd") + "')";


                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numPrelevement = prelevement.NumPrelevement;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return numPrelevement;
        }

        crlPrelevement IntfDalPrelevement.selectPrelevement(string numPrelevement)
        {
            #region declaration
            crlPrelevement Prelevement = null;
            IntfDalAgent serviceAgent = new ImplDalAgent();
            IntfDalTypePrelevement serviceTypePrelevement = new ImplDalTypePrelevement();
            IntfDalAutorisationDepart serviceAutorisationDepart = new ImplDalAutorisationDepart();
            #endregion

            #region implementation
            if (numPrelevement != "")
            {
                this.strCommande = "SELECT * FROM `prelevement` WHERE (`numPrelevement`='" + numPrelevement + "')";
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            Prelevement = new crlPrelevement();
                            Prelevement.NumPrelevement = this.reader["numPrelevement"].ToString();
                            Prelevement.DatePrelevement = Convert.ToDateTime(this.reader["datePrelevement"].ToString());
                            Prelevement.MatriculeAgent = this.reader["matriculeAgent"].ToString();
                            Prelevement.MontantPrelevement = double.Parse(this.reader["montantPrelevement"].ToString());
                            Prelevement.NumAutorisationDepart = this.reader["numAutorisationDepart"].ToString();
                            Prelevement.TypePrelevement = this.reader["typePrelevement"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Prelevement != null)
                {
                    if (Prelevement.MatriculeAgent != "")
                    {
                        Prelevement.agent = serviceAgent.selectAgent(Prelevement.MatriculeAgent);
                    }
                    if (Prelevement.NumAutorisationDepart != "")
                    {
                        Prelevement.autorisationDepart = serviceAutorisationDepart.selectAutorisationDepart(Prelevement.NumAutorisationDepart);
                    }
                    if (Prelevement.TypePrelevement != "")
                    {
                        Prelevement.objTypePrelevement = serviceTypePrelevement.selectTypePrelevement(Prelevement.TypePrelevement);
                    }
                }

            }
            #endregion

            return Prelevement;
        }

        bool IntfDalPrelevement.updatePrelevement(crlPrelevement prelevement)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            #endregion

            #region implementation
            if (prelevement != null)
            {
                this.strCommande = "UPDATE `prelevement` SET `matriculeAgent`='" + prelevement.MatriculeAgent + "',";
                this.strCommande += " `montantPrelevement`='" + prelevement.MontantPrelevement + "',";
                this.strCommande += " `numAutorisationDepart`='" + prelevement.NumAutorisationDepart + "',";
                this.strCommande += " `typePrelevement`='" + prelevement.TypePrelevement + "'";
                this.strCommande += " WHERE `numPrelevement`='" + prelevement.NumPrelevement + "'";

                this.serviceConnectBase.openConnection();
                nbUpdate = this.serviceConnectBase.requete(this.strCommande);
                if (nbUpdate == 1)
                    isUpdate = true;
                this.serviceConnectBase.closeConnection();

            }
            #endregion

            return isUpdate;
        }

        string IntfDalPrelevement.getNumPrelevement(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numPrelevement = "00001";
            string[] tempNumPrelevement = null;
            string strDate = "PV" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT prelevement.numPrelevement AS maxNum FROM prelevement";
            this.strCommande += " WHERE prelevement.numPrelevement LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumPrelevement = reader["maxNum"].ToString().ToString().Split('/');
                        numPrelevement = tempNumPrelevement[tempNumPrelevement.Length - 1];
                    }
                    numTemp = double.Parse(numPrelevement) + 1;
                    if (numTemp < 10)
                        numPrelevement = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numPrelevement = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numPrelevement = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numPrelevement = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numPrelevement = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numPrelevement = strDate + "/" + sigleAgence + "/" + numPrelevement;
            #endregion

            return numPrelevement;
        }
        #endregion

        #region insert to grid
        void IntfDalPrelevement.insertToGridPrelevement(GridView gridView, string numAutorisationDepart)
        {
            #region declaration
            IntfDalPrelevement servicePrelevement = new ImplDalPrelevement();
            #endregion

            #region implementation
            this.strCommande = "SELECT prelevement.typePrelevement, prelevement.numPrelevement,";
            this.strCommande += " prelevement.matriculeAgent, prelevement.numAutorisationDepart,";
            this.strCommande += " prelevement.montantPrelevement, prelevement.datePrelevement,";
            this.strCommande += "  typeprelevement.typePrelevement, typeprelevement.commentaire";
            this.strCommande += " FROM prelevement  Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " WHERE prelevement.numAutorisationDepart = '" + numAutorisationDepart + "'";

            gridView.DataSource = servicePrelevement.getDataTablePrelevement(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalPrelevement.getDataTablePrelevement(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numPrelevement", typeof(string));
            dataTable.Columns.Add("montantPrelevement", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));
            dataTable.Columns.Add("datePrelevement", typeof(DateTime));
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

                        dr["numPrelevement"] = reader["numPrelevement"].ToString();
                        dr["montantPrelevement"] = serviceGeneral.separateurDesMilles(reader["montantPrelevement"].ToString()) + "Ar";
                        dr["commentaire"] = reader["commentaire"].ToString();
                        dr["datePrelevement"] = Convert.ToDateTime(reader["datePrelevement"].ToString());


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


        void IntfDalPrelevement.insertToGridPrelevementNonPayer(GridView gridView, string numAutorisationDepart)
        {
            #region declaration
            IntfDalPrelevement servicePrelevement = new ImplDalPrelevement();
            #endregion

            #region implementation
            this.strCommande = "SELECT prelevement.typePrelevement, prelevement.numPrelevement,";
            this.strCommande += " prelevement.matriculeAgent, prelevement.numAutorisationDepart,";
            this.strCommande += " prelevement.montantPrelevement, prelevement.datePrelevement,";
            this.strCommande += "  typeprelevement.typePrelevement, typeprelevement.commentaire";
            this.strCommande += " FROM prelevement  Inner Join typeprelevement ON typeprelevement.typePrelevement = prelevement.typePrelevement";
            this.strCommande += " Left Join recuad ON recuad.numPrelevement = prelevement.numPrelevement";
            this.strCommande += " WHERE prelevement.numAutorisationDepart = '" + numAutorisationDepart + "'";
            this.strCommande += " AND recuad.numPrelevement IS NULL";

            gridView.DataSource = servicePrelevement.getDataTablePrelevementNonPayer(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalPrelevement.getDataTablePrelevementNonPayer(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numPrelevement", typeof(string));
            dataTable.Columns.Add("montantPrelevement", typeof(string));
            dataTable.Columns.Add("commentaire", typeof(string));
            dataTable.Columns.Add("datePrelevement", typeof(DateTime));
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

                        dr["numPrelevement"] = reader["numPrelevement"].ToString();
                        dr["montantPrelevement"] = serviceGeneral.separateurDesMilles(reader["montantPrelevement"].ToString()) + "Ar";
                        dr["commentaire"] = reader["commentaire"].ToString();
                        dr["datePrelevement"] = Convert.ToDateTime(reader["datePrelevement"].ToString());


                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();

            #endregion

            return dataTable;
        }


        
    }
}