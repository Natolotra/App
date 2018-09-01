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
    /// Summary description for ImplDalUSReductionParticulier
    /// </summary>
    public class ImplDalUSReductionParticulier : IntfDalUSReductionParticulier
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalUSReductionParticulier(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalUSReductionParticulier()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalUSReductionParticulier Members

        string IntfDalUSReductionParticulier.insertUSReductionParticulier(crlUSReductionParticulier reductionParticulier, string sigleAgence)
        {
            #region declaration
            int nbInsert = 0;
            string numUSReductionParticulier = "";
            string numEtablissementScolaire = "NULL";
            string numSociete = "NULL";
            IntfDalUSReductionParticulier serviceUSReductionParticulier = new ImplDalUSReductionParticulier();
            #endregion

            #region implementation
            if (reductionParticulier != null) 
            {
                if (reductionParticulier.NumEtablissementScolaire != "") 
                {
                    numEtablissementScolaire = "'" + reductionParticulier.NumEtablissementScolaire + "'";
                }
                if(reductionParticulier.NumSociete != "")
                {
                    numSociete = "'" + reductionParticulier.NumSociete + "'";
                }
                reductionParticulier.NumUSReductionParticulier = serviceUSReductionParticulier.getNumUSReductionParticulier(sigleAgence);

                this.strCommande = "INSERT INTO `usreductionparticulier` (`numUSReductionParticulier`,`numIndividu`,";
                this.strCommande += " `numEtablissementScolaire`,`numSociete`,`numCategorieBillet`,";
                this.strCommande += " `imageReductionParticulier`) VALUES ('" + reductionParticulier.NumUSReductionParticulier + "',";
                this.strCommande += " '" + reductionParticulier.NumIndividu + "'," + numEtablissementScolaire + ",";
                this.strCommande += " " + numSociete + ",'" + reductionParticulier.NumCategorieBillet + "',";
                this.strCommande += " '" + reductionParticulier.ImageReductionParticulier + "')";

                this.serviceConnectBase.openConnection();
                nbInsert = this.serviceConnectBase.requete(this.strCommande);
                if (nbInsert == 1) 
                {
                    numUSReductionParticulier = reductionParticulier.NumUSReductionParticulier;
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numUSReductionParticulier;
        }

        bool IntfDalUSReductionParticulier.updateUSReductionParticulier(crlUSReductionParticulier reductionParticulier)
        {
            #region declaration
            bool isUpdate = false;
            int nbUpdate = 0;
            string numEtablissementScolaire = "NULL";
            string numSociete = "NULL";
            #endregion

            #region implementation
            if (reductionParticulier != null) 
            {
                if (reductionParticulier.NumEtablissementScolaire != "")
                {
                    numEtablissementScolaire = "'" + reductionParticulier.NumEtablissementScolaire + "'";
                }
                if (reductionParticulier.NumSociete != "")
                {
                    numSociete = "'" + reductionParticulier.NumSociete + "'";
                }
                this.strCommande = "UPDATE `usreductionparticulier` SET `numIndividu`='" + reductionParticulier.NumIndividu + "',";
                this.strCommande += " `numEtablissementScolaire`=" + numEtablissementScolaire + ",";
                this.strCommande += " `numSociete`=" + numSociete + ",`numCategorieBillet`='" + reductionParticulier.NumCategorieBillet + "',";
                this.strCommande += " `imageReductionParticulier`='" + reductionParticulier.ImageReductionParticulier + "'";
                this.strCommande += " WHERE `numUSReductionParticulier`='" + reductionParticulier.NumUSReductionParticulier + "'";

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

        string IntfDalUSReductionParticulier.isUSReductionParticulier(crlUSReductionParticulier reductionParticulier)
        {
            #region declaration
            string numUSReductionParticulier = "";
            #endregion

            #region implementation
            if (reductionParticulier != null) 
            {
                this.strCommande = "SELECT usreductionparticulier.numUSReductionParticulier FROM `usreductionparticulier` WHERE";
                this.strCommande += " usreductionparticulier.numIndividu = '" + reductionParticulier.NumIndividu + "' AND";
                this.strCommande += " usreductionparticulier.numUSReductionParticulier <> '" + reductionParticulier.NumUSReductionParticulier + "'";
                
                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            numUSReductionParticulier = this.reader["numUSReductionParticulier"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return numUSReductionParticulier;
        }

        crlUSReductionParticulier IntfDalUSReductionParticulier.selectUSReductionParticulier(string numUSReductionParticulier)
        {
            #region declaration
            crlUSReductionParticulier reductionParticulier = null;
            IntfDalIndividu serviceIndividu = new ImplDalIndividu();
            IntfDalUSCategorieBillet serviceUSCategorieBillet = new ImplDalUSCategorieBillet();
            #endregion

            #region implementation
            if (numUSReductionParticulier != "") 
            {
                this.strCommande = "SELECT * FROM `usreductionparticulier` WHERE";
                this.strCommande += " usreductionparticulier.numUSReductionParticulier = '" + numUSReductionParticulier + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null) 
                {
                    if (this.reader.HasRows) 
                    {
                        if (this.reader.Read()) 
                        {
                            reductionParticulier = new crlUSReductionParticulier();
                            reductionParticulier.NumCategorieBillet = this.reader["numCategorieBillet"].ToString();
                            reductionParticulier.NumIndividu = this.reader["numIndividu"].ToString();
                            reductionParticulier.NumEtablissementScolaire = this.reader["numEtablissementScolaire"].ToString();
                            reductionParticulier.NumSociete = this.reader["numSociete"].ToString();
                            reductionParticulier.NumUSReductionParticulier = this.reader["numUSReductionParticulier"].ToString();
                            reductionParticulier.ImageReductionParticulier = this.reader["imageReductionParticulier"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();

                if (reductionParticulier != null) 
                {
                    if (reductionParticulier.NumIndividu != "") 
                    {
                        reductionParticulier.individu = serviceIndividu.selectIndividu(reductionParticulier.NumIndividu);
                    }
                    if (reductionParticulier.NumCategorieBillet != "") 
                    {
                        reductionParticulier.categorieBillet = serviceUSCategorieBillet.selectUSCategorieBillet(reductionParticulier.NumCategorieBillet);
                    }
                }
            }
            #endregion

            return reductionParticulier;
        }

        string IntfDalUSReductionParticulier.getNumUSReductionParticulier(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numUSReductionParticulier = "00001";
            string[] tempNumUSReductionParticulier = null;
            string strDate = "RP" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT usreductionparticulier.numUSReductionParticulier AS maxNum FROM usreductionparticulier";
            this.strCommande += " WHERE usreductionparticulier.numUSReductionParticulier LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumUSReductionParticulier = reader["maxNum"].ToString().ToString().Split('/');
                        numUSReductionParticulier = tempNumUSReductionParticulier[tempNumUSReductionParticulier.Length - 1];
                    }
                    numTemp = double.Parse(numUSReductionParticulier) + 1;
                    if (numTemp < 10)
                        numUSReductionParticulier = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numUSReductionParticulier = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numUSReductionParticulier = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numUSReductionParticulier = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numUSReductionParticulier = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numUSReductionParticulier = strDate + "/" + sigleAgence + "/" + numUSReductionParticulier;
            #endregion

            return numUSReductionParticulier;
        }

        void IntfDalUSReductionParticulier.insertToGridUSReductionParticulier(GridView gridView, string param, string paramLike, string valueLike)
        {
            #region declaration
            IntfDalUSReductionParticulier serviceUSReductionParticulier = new ImplDalUSReductionParticulier();
            #endregion

            #region implementation

            this.strCommande = "SELECT usreductionparticulier.numUSReductionParticulier, usreductionparticulier.numIndividu,";
            this.strCommande += " usreductionparticulier.numEtablissementScolaire, usreductionparticulier.numSociete,";
            this.strCommande += " usreductionparticulier.numCategorieBillet, individu.numIndividu, individu.civiliteIndividu,";
            this.strCommande += " individu.nomIndividu, individu.prenomIndividu, individu.cinIndividu, individu.adresse, individu.profession,";
            this.strCommande += " individu.telephoneFixeIndividu, individu.telephoneMobileIndividu, individu.dateNaissanceIndividu,";
            this.strCommande += " individu.lieuNaissanceIndividu, societe.nomSociete,";
            this.strCommande += " etablissementscolaire.etablissementScolaire, uscategoriebillet.categorieBillet FROM usreductionparticulier";
            this.strCommande += " Inner Join individu ON individu.numIndividu = usreductionparticulier.numIndividu";
            this.strCommande += " Left Join societe ON societe.numSociete = usreductionparticulier.numSociete";
            this.strCommande += " Left Join etablissementscolaire ON etablissementscolaire.numEtablissementScolaire = usreductionparticulier.numEtablissementScolaire";
            this.strCommande += " Inner Join uscategoriebillet ON uscategoriebillet.numCategorieBillet = usreductionparticulier.numCategorieBillet";
            this.strCommande += " WHERE " + paramLike + " LIKE  '%" + valueLike + "%'";
            this.strCommande += " ORDER BY " + param;


            gridView.DataSource = serviceUSReductionParticulier.getDataTableUSReductionParticulier(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalUSReductionParticulier.getDataTableUSReductionParticulier(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation

            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("numUSReductionParticulier", typeof(string));
            dataTable.Columns.Add("individu", typeof(string));
            dataTable.Columns.Add("contact", typeof(string));
            dataTable.Columns.Add("societe", typeof(string));
            dataTable.Columns.Add("etablissement", typeof(string));
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

                        dr["numUSReductionParticulier"] = reader["numUSReductionParticulier"].ToString();
                        dr["individu"] = reader["civiliteIndividu"].ToString() + " " + reader["nomIndividu"].ToString() + " " + reader["prenomIndividu"].ToString();
                        dr["contact"] = reader["telephoneFixeIndividu"].ToString() + "/" + reader["telephoneMobileIndividu"].ToString();
                        dr["societe"] = reader["nomSociete"].ToString();
                        dr["etablissement"] = reader["etablissementScolaire"].ToString();

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
