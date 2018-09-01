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
using System.Collections.Generic;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service escorte voyage
    /// </summary>
    public class ImplDalEscorteVoyage : IntfDalEscorteVoyage
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region constructeur
        public ImplDalEscorteVoyage(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        public ImplDalEscorteVoyage()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        #endregion

        #region IntfDalEscorteVoyage Members

        string IntfDalEscorteVoyage.insertEscorteVoyage(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            IntfDalEscorteVoyage serviceEscorteVoyage = new ImplDalEscorteVoyage();
            int nombreInsertion = 0;
            string idEscorteVoyage = "";
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                EscorteVoyage.IdEscorteVoyage = serviceEscorteVoyage.getIdEscorteVoyage();
                this.strCommande = "INSERT INTO `escortevoyage` (`idEscorteVoyage`";
                this.strCommande += ",`matriculeEscorte`,`numerosFB`,`trajetEscorte`)";
                this.strCommande += " VALUES ('" + EscorteVoyage.IdEscorteVoyage + "'";
                this.strCommande += ",'" + EscorteVoyage.MatriculeEscorte + "','" + EscorteVoyage.NumerosFB + "'";
                this.strCommande += ",'" + EscorteVoyage.TrajetEscorte + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    idEscorteVoyage = EscorteVoyage.IdEscorteVoyage;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return idEscorteVoyage;
        }

        string IntfDalEscorteVoyage.insertEscorteVoyageAll(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            IntfDalEscorte serviceEscorte = new ImplDalEscorte();
            IntfDalEscorteVoyage serviceEscorteVoyage = new ImplDalEscorteVoyage();

            string idEscorteVoyage = "";
            int isEscorteVoyage = 0;
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                if (EscorteVoyage.Escorte != null)
                {
                    EscorteVoyage.Escorte.MatriculeEscorte = serviceEscorte.isEscorte(EscorteVoyage.Escorte);
                    if (EscorteVoyage.Escorte.MatriculeEscorte != "")
                    {
                        serviceEscorte.updateEscorte(EscorteVoyage.Escorte);
                    }
                    else
                    {
                        EscorteVoyage.Escorte.MatriculeEscorte = serviceEscorte.insertEscorte(EscorteVoyage.Escorte);
                    }

                    if (EscorteVoyage.Escorte.MatriculeEscorte != "")
                    {
                        EscorteVoyage.MatriculeEscorte = EscorteVoyage.Escorte.MatriculeEscorte;
                        EscorteVoyage.IdEscorteVoyage = serviceEscorteVoyage.insertEscorteVoyage(EscorteVoyage);

                        //isEscorteVoyage

                        if (EscorteVoyage.IdEscorteVoyage != "")
                        {
                            idEscorteVoyage = EscorteVoyage.IdEscorteVoyage;
                        }
                    }
                }
            }
            #endregion

            return idEscorteVoyage;
        }

        bool IntfDalEscorteVoyage.deleteEscorteVoyage(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                if (EscorteVoyage.IdEscorteVoyage != "")
                {
                    this.strCommande = "DELETE FROM `escortevoyage` WHERE (`idEscorteVoyage` = '" + EscorteVoyage.IdEscorteVoyage + "')";
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

        bool IntfDalEscorteVoyage.deleteEscorteVoyage(string idEscorteVoyage)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            
            if (idEscorteVoyage != "")
            {
                this.strCommande = "DELETE FROM `escortevoyage` WHERE (`idEscorteVoyage` = '" + idEscorteVoyage + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            
            #endregion

            return isDelete;
        }

        bool IntfDalEscorteVoyage.updateEscorteVoyage(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                if (EscorteVoyage.IdEscorteVoyage != "")
                {
                    this.strCommande = "UPDATE `escortevoyage` SET `matriculeEscorte`='" + EscorteVoyage.MatriculeEscorte+ "', ";
                    this.strCommande += "`numerosFB`='" + EscorteVoyage.NumerosFB + "', `trajetEscorte`='" + EscorteVoyage.TrajetEscorte +"' ";
                    this.strCommande += "WHERE (`idEscorteVoyage`='" + EscorteVoyage.IdEscorteVoyage + "')";

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

        bool IntfDalEscorteVoyage.updateEscorteVoyageAll(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            IntfDalEscorte serviceEscorte = new ImplDalEscorte();
            IntfDalEscorteVoyage serviceEscorteVoyage = new ImplDalEscorteVoyage();

            bool isUpdate = false;
            int isEscorte = 0;
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                if (EscorteVoyage.Escorte != null)
                {

                    isEscorte = serviceEscorte.isEscorteInt(EscorteVoyage.Escorte);

                    if (isEscorte == 0)
                    {
                        serviceEscorte.updateEscorte(EscorteVoyage.Escorte);
                    }
                   

                    if (EscorteVoyage.Escorte.MatriculeEscorte != "")
                    {
                        isUpdate = serviceEscorteVoyage.updateEscorteVoyage(EscorteVoyage);
                    }
                }
            }
            #endregion

            return isUpdate;
        }

        string IntfDalEscorteVoyage.isEscorteVoyage(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            string isEscorteVoyage = "";
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                this.strCommande = "SELECT * FROM `escortevoyage` WHERE (`idEscorteVoyage` <> '" + EscorteVoyage.IdEscorteVoyage + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (EscorteVoyage.MatriculeEscorte.Trim().ToLower().Equals(reader["matriculeEscorte"].ToString().Trim().ToLower()) && EscorteVoyage.NumerosFB.Trim().ToLower().Equals(reader["numerosFB"].ToString().Trim().ToLower()))
                            {
                                isEscorteVoyage = reader["idEscorteVoyage"].ToString();
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isEscorteVoyage;
        }

        int IntfDalEscorteVoyage.isEscorteVoyageInt(crlEscorteVoyage EscorteVoyage)
        {
            #region declaration
            int isEscorteVoyage = 0;
            #endregion

            #region implementation
            if (EscorteVoyage != null)
            {
                this.strCommande = "SELECT * FROM `escortevoyage` WHERE (`idEscorteVoyage` <> '" + EscorteVoyage.IdEscorteVoyage + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (EscorteVoyage.MatriculeEscorte.Trim().ToLower().Equals(reader["matriculeEscorte"].ToString().Trim().ToLower()) && EscorteVoyage.NumerosFB.Trim().ToLower().Equals(reader["numerosFB"].ToString().Trim().ToLower()))
                            {
                                isEscorteVoyage = 1;
                                break;
                            }
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isEscorteVoyage;
        }

        crlEscorteVoyage IntfDalEscorteVoyage.selectEscorteVoyage(string idEscorteVoyage)
        {
            #region declaration
            crlEscorteVoyage EscorteVoyage = null;
            IntfDalEscorte serviceEscorte = new ImplDalEscorte();
            #endregion

            #region implementation
            if (idEscorteVoyage != "")
            {
                this.strCommande = "SELECT * FROM `escortevoyage` WHERE (`idEscorteVoyage` = '" + idEscorteVoyage + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        EscorteVoyage = new crlEscorteVoyage();
                        reader.Read();
                        EscorteVoyage.IdEscorteVoyage = reader["idEscorteVoyage"].ToString();
                        EscorteVoyage.MatriculeEscorte = reader["matriculeEscorte"].ToString();
                        EscorteVoyage.NumerosFB = reader["numerosFB"].ToString();
                        EscorteVoyage.TrajetEscorte = reader["trajetEscorte"].ToString();
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (EscorteVoyage != null) 
                {
                    EscorteVoyage.Escorte = serviceEscorte.selectEscorte(EscorteVoyage.MatriculeEscorte);
                }
            }
            #endregion

            return EscorteVoyage;
        }

        string IntfDalEscorteVoyage.getIdEscorteVoyage()
        {
            #region declaration
            int numTemp = 0;
            string idEscorteVoyage = "00001";
            string[] tempIdEscorteVoyage = null;
            #endregion

            #region implementation
            this.strCommande = "SELECT escortevoyage.idEscorteVoyage AS maxNum FROM escortevoyage ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempIdEscorteVoyage = reader["maxNum"].ToString().ToString().Split('/');
                        idEscorteVoyage = tempIdEscorteVoyage[0];
                    }
                    numTemp = int.Parse(idEscorteVoyage) + 1;
                    if (numTemp < 10)
                        idEscorteVoyage = "0000" + numTemp;
                    if (numTemp < 100 && numTemp >= 10)
                        idEscorteVoyage = "000" + numTemp;
                    if (numTemp < 1000 && numTemp >= 100)
                        idEscorteVoyage = "00" + numTemp;
                    if (numTemp < 10000 && numTemp >= 1000)
                        idEscorteVoyage = "0" + numTemp;
                    if (numTemp >= 10000)
                        idEscorteVoyage = "" + numTemp;
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            idEscorteVoyage = idEscorteVoyage + "/EV/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            #endregion

            return idEscorteVoyage;
        }

        void IntfDalEscorteVoyage.loadDdlTri(DropDownList ddlTri)
        {
            throw new NotImplementedException();
        }

        List<crlEscorteVoyage> IntfDalEscorteVoyage.selectEscorteVoyageFB(string numerosFB)
        {
            #region declaration
            crlEscorteVoyage tempEscorteVoyage = null;
            List<crlEscorteVoyage> escorteVoyages = null;
            IntfDalEscorte serviceEscorte = new ImplDalEscorte();
            #endregion

            #region implementation
            if (numerosFB != "")
            {
                this.strCommande = "SELECT * FROM `escortevoyage` WHERE (`numerosFB` = '" + numerosFB + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        escorteVoyages = new List<crlEscorteVoyage>();

                        while (reader.Read())
                        {
                            tempEscorteVoyage = new crlEscorteVoyage();
                            tempEscorteVoyage.IdEscorteVoyage = reader["idEscorteVoyage"].ToString();
                            tempEscorteVoyage.MatriculeEscorte = reader["matriculeEscorte"].ToString();
                            tempEscorteVoyage.NumerosFB = reader["numerosFB"].ToString();
                            tempEscorteVoyage.TrajetEscorte = reader["trajetEscorte"].ToString();

                            escorteVoyages.Add(tempEscorteVoyage);
                        }
                       
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (escorteVoyages != null)
                {
                    for (int i = 0; i < escorteVoyages.Count; i++)
                    {
                        escorteVoyages[i].Escorte = serviceEscorte.selectEscorte(escorteVoyages[i].MatriculeEscorte);
                    }
                }
            }
            #endregion

            return escorteVoyages;
        }

        #endregion


        #region insert into grid view EscorteVoyageFB

        void IntfDalEscorteVoyage.insertToGridEscorteVoyageFB(GridView gridView, string numerosFB)
        {
            #region declaration
            IntfDalEscorteVoyage serviceEscorteVoyage = new ImplDalEscorteVoyage();
            List<crlEscorteVoyage> escorteVoyages = null;
            #endregion

            #region implementation
            escorteVoyages = serviceEscorteVoyage.selectEscorteVoyageFB(numerosFB);
            if (escorteVoyages != null)
            {
                gridView.DataSource = serviceEscorteVoyage.getDataTableEscorteVoyageFB(escorteVoyages);
                gridView.DataBind();
            }
            #endregion
        }

        DataTable IntfDalEscorteVoyage.getDataTableEscorteVoyageFB(List<crlEscorteVoyage> escorteVoyages)
        {
            #region declaration
            DataTable dataTable = new DataTable();
            #endregion

            #region implementation
            #region initialisation du dataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("matricule", typeof(string));
            dataTable.Columns.Add("nom", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));
            DataRow dr;
            #endregion

            for (int i = 0; i < escorteVoyages.Count; i++)
            {
                dr = dataTable.NewRow();
                dr["matricule"] = escorteVoyages[i].Escorte.MatriculeEscorte;
                dr["nom"] = escorteVoyages[i].Escorte.NomEscorte + " " + escorteVoyages[i].Escorte.PrenomEscorte;
                dr["trajet"] = escorteVoyages[i].TrajetEscorte;

                dataTable.Rows.Add(dr);
            }



            #endregion

            return dataTable;
        }

        #endregion







        
    }
}
