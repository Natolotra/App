using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arch.dal.intf;
using arch.crl;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.impl
{
    /// <summary>
    /// Implementation du service trajet
    /// </summary>
    public class ImplDalTrajet : IntfDalTrajet 
    {
        #region declaration variable
        IntfDalConnectBase serviceConnectBase = null;
        IntfDalServiceRessource serviceRessource = null;

        MySqlDataReader reader = null;

        string strCommande = "";
        #endregion

        #region construction
        public ImplDalTrajet()
        {
            serviceRessource = new ImplDalServiceRessource();
            serviceConnectBase = new ImplDalConnectBase(serviceRessource.getDefaultStrConnection());
        }
        public ImplDalTrajet(string strConnection)
        {
            serviceConnectBase = new ImplDalConnectBase(strConnection);
        }
        #endregion

        #region IntfDalAgent Members
        string IntfDalTrajet.insertTrajet(crlTrajet Trajet, string sigleAgence)
        {
            #region declaration
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            int nombreInsertion = 0;
            string numTrajet = "";
            #endregion

            #region implementation
            if (Trajet != null)
            {
                Trajet.NumTrajet = serviceTrajet.getNumTrajet(sigleAgence);

                this.strCommande = "INSERT INTO `trajet` (`numTrajet`,`numTarifBaseBillet`,`distanceTrajet`,`dureeTrajet`,`numVilleD`,`numVilleF`)";
                this.strCommande += " VALUES ('" + Trajet.NumTrajet + "','" + Trajet.NumTarifBaseBillet + "','" + Trajet.DistanceTrajet + "',";
                this.strCommande += " '" + Trajet.DureeTrajet + "', '" +Trajet.NumVilleD + "','" + Trajet.NumVilleF + "')";

                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    numTrajet = Trajet.NumTrajet;
                this.serviceConnectBase.closeConnection();

                if (Trajet.villeD != null)
                {
                    serviceTrajet.insertAssociationVilleTrajet(Trajet, Trajet.villeD);
                }
                if (Trajet.villeF != null)
                {
                    serviceTrajet.insertAssociationVilleTrajet(Trajet, Trajet.villeF);
                }

            }
            #endregion

            return numTrajet;
        }

        string IntfDalTrajet.insertTrajetAll(crlTrajet Trajet, string sigleAgence)
        {
            #region declaration
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            IntfDalTarifBaseBillet serviceTarifBaseBillet = new ImplDalTarifBaseBillet();
            IntfDalTarifBaseCommission serviceTarifBaseCommission = new ImplDalTarifBaseCommission();

            string numTrajet = "";
            #endregion

            #region implementation
            if (Trajet != null)
            {
                

                if (Trajet.tarifBaseBillet != null)
                {
                    Trajet.tarifBaseBillet.NumTarifBaseBillet = serviceTarifBaseBillet.insertTarifBaseBillet(Trajet.tarifBaseBillet, sigleAgence);
                    if (Trajet.tarifBaseBillet.NumTarifBaseBillet != "")
                    {
                        Trajet.NumTarifBaseBillet = Trajet.tarifBaseBillet.NumTarifBaseBillet;

                        Trajet.NumTrajet = serviceTrajet.insertTrajet(Trajet, sigleAgence);

                        if (Trajet.NumTrajet != "")
                        {
                            numTrajet = Trajet.NumTrajet;

                            if (Trajet.tarifBaseCommissions != null)
                            {
                                for (int i = 0; i < Trajet.tarifBaseCommissions.Count; i++)
                                {
                                    Trajet.tarifBaseCommissions[i].NumTarifBaseCommission = serviceTarifBaseCommission.insertTarifBaseCommission(Trajet.tarifBaseCommissions[i], sigleAgence);

                                    if (Trajet.tarifBaseCommissions[i].NumTarifBaseCommission != "")
                                    {
                                        serviceTarifBaseCommission.insertAssociationTrajetTarifCommission(Trajet.NumTrajet, Trajet.tarifBaseCommissions[i].NumTarifBaseCommission);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            #endregion

            return numTrajet;
        }

        bool IntfDalTrajet.insertAssociationVilleTrajet(crlTrajet Trajet, crlVille Ville)
        {
            #region declaration
            bool isInsert = false;
            int nombreInsertion = 0;
            #endregion

            #region implementation
            if(Trajet != null && Ville != null)
            {
                this.strCommande = "INSERT INTO `associationvilletrajet` (`numTrajet`,`numVille`)";
                this.strCommande += " VALUES ('" + Trajet.NumTrajet + "','" + Ville.NumVille + "')";
                this.serviceConnectBase.openConnection();
                nombreInsertion = this.serviceConnectBase.requete(this.strCommande);
                if (nombreInsertion == 1)
                    isInsert = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isInsert;
        }

        bool IntfDalTrajet.deleteAssociationVilleTrajet(string numTrajet)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            #endregion

            #region implementation
            if (numTrajet != "")
            {
                this.strCommande = "DELETE FROM `associationvilletrajet` WHERE (`numTrajet` = '" + numTrajet + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete > 0)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
            #endregion

            return isDelete;
        }

        bool IntfDalTrajet.deleteTrajet(crlTrajet Trajet)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            if (Trajet != null)
            {
                if (Trajet.NumTrajet != "")
                {
                    serviceTrajet.deleteAssociationVilleTrajet(Trajet.NumTrajet);
                    this.strCommande = "DELETE FROM `trajet` WHERE (`numTrajet` = '" + Trajet.NumTrajet + "')";
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

        bool IntfDalTrajet.deleteTrajet(string numTrajet)
        {
            #region declaration
            bool isDelete = false;
            int nombreDelete = 0;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
           
            if (numTrajet != "")
            {
                serviceTrajet.deleteAssociationVilleTrajet(numTrajet);
                this.strCommande = "DELETE FROM `trajet` WHERE (`numTrajet` = '" + numTrajet + "')";
                this.serviceConnectBase.openConnection();
                nombreDelete = this.serviceConnectBase.requete(this.strCommande);
                if (nombreDelete == 1)
                    isDelete = true;
                this.serviceConnectBase.closeConnection();
            }
           
            #endregion

            return isDelete;
        }

        bool IntfDalTrajet.updateTrajet(crlTrajet Trajet)
        {
            #region declaration
            bool isUpdate = false;
            int nombreUpdate = 0;
            #endregion

            #region implementation
            if (Trajet != null)
            {
                if (Trajet.NumTrajet != "")
                {
                    this.strCommande = "UPDATE `trajet` SET `numTarifBaseBillet`='" + Trajet.NumTarifBaseBillet + "',`distanceTrajet`='" + Trajet.DistanceTrajet + "',";
                    this.strCommande += " `dureeTrajet`='" + Trajet.DureeTrajet + "',`numVilleD`='" + Trajet.NumVilleD + "',";
                    this.strCommande += " `numVilleF`='" + Trajet.NumVilleF + "'";
                    this.strCommande += " WHERE (`numTrajet`='" + Trajet.NumTrajet + "')";

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

        int IntfDalTrajet.isTrajetInt(crlTrajet Trajet)
        {
            #region declaration
            int isTrajet = 0;
            //List<crlTrajet> Trajets = null;
            //IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            if (Trajet != null)
            {
                this.strCommande = "SELECT trajet.numTrajet FROM trajet WHERE";
                this.strCommande += " ((trajet.numVilleD = '" + Trajet.NumVilleD + "' AND trajet.numVilleF = '" + Trajet.NumVilleF + "') OR";
                this.strCommande += " (trajet.numVilleF =  '" + Trajet.NumVilleD + "' AND trajet.numVilleD = '" + Trajet.NumVilleF + "')) AND";
                this.strCommande += " trajet.distanceTrajet = '" + Trajet.DistanceTrajet + "' AND";
                this.strCommande += " trajet.numTrajet <> '" + Trajet.NumTrajet + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isTrajet = 1;
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }
            /*if (Trajet != null)
            {
                Trajets = serviceTrajet.selectTrajets(Trajet.NumTrajet);
                if (Trajets != null)
                {
                    for (int i = 0; i < Trajets.Count; i++)
                    {
                        if (((Trajets[i].villeD.NumVille.Trim().ToLower().Equals(Trajet.villeD.NumVille.Trim().ToLower()) && Trajets[i].villeF.NumVille.Trim().ToLower().Equals(Trajet.villeF.NumVille.Trim().ToLower())) ||
                            (Trajets[i].villeF.NumVille.Trim().ToLower().Equals(Trajet.villeD.NumVille.Trim().ToLower()) && Trajets[i].villeD.NumVille.Trim().ToLower().Equals(Trajet.villeF.NumVille.Trim().ToLower()))) &&
                            Trajets[i].DistanceTrajet == Trajet.DistanceTrajet)
                        {
                            isTrajet = 1;
                            break;
                        }
                    }
                }
            }*/
            #endregion

            return isTrajet;
        }

        string IntfDalTrajet.isTrajet(crlTrajet Trajet)
        {
            #region declaration
            string isTrajet = "";
            //List<crlTrajet> Trajets = null;
            //IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation

            if (Trajet != null)
            {
                this.strCommande = "SELECT trajet.numTrajet FROM trajet WHERE";
                this.strCommande += " ((trajet.numVilleD = '" + Trajet.NumVilleD + "' AND trajet.numVilleF = '" + Trajet.NumVilleF + "') OR";
                this.strCommande += " (trajet.numVilleF =  '" + Trajet.NumVilleD + "' AND trajet.numVilleD = '" +  Trajet.NumVilleF + "')) AND";
                this.strCommande += " trajet.distanceTrajet = '" + Trajet.DistanceTrajet + "' AND";
                this.strCommande += " trajet.numTrajet <> '" + Trajet.NumTrajet + "'";

                this.serviceConnectBase.openConnection();
                this.reader = this.serviceConnectBase.select(this.strCommande);
                if (this.reader != null)
                {
                    if (this.reader.HasRows)
                    {
                        if (this.reader.Read())
                        {
                            isTrajet = this.reader["numTrajet"].ToString();
                        }
                    }
                    this.reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
            }

            /*if (Trajet != null)
            {
                Trajets = serviceTrajet.selectTrajets(Trajet.NumTrajet);
                if (Trajets != null)
                {
                    for (int i = 0; i < Trajets.Count; i++)
                    {
                        if (((Trajets[i].villeD.NumVille.Trim().ToLower().Equals(Trajet.villeD.NumVille.Trim().ToLower()) && Trajets[i].villeF.NumVille.Trim().ToLower().Equals(Trajet.villeF.NumVille.Trim().ToLower())) ||
                            (Trajets[i].villeF.NumVille.Trim().ToLower().Equals(Trajet.villeD.NumVille.Trim().ToLower()) && Trajets[i].villeD.NumVille.Trim().ToLower().Equals(Trajet.villeF.NumVille.Trim().ToLower()))) && 
                            Trajets[i].DistanceTrajet == Trajet.DistanceTrajet)
                        {
                            isTrajet = Trajets[i].NumTrajet;
                            break;
                        }
                    }
                }
            }*/
            #endregion

            return isTrajet;
        }

        crlTrajet IntfDalTrajet.selectTrajet(string numVilleD, string numVilleF)
        {
            #region declaration
            crlTrajet Trajet = null;
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalTarifBaseBillet serviceTarifBaseBillet = new ImplDalTarifBaseBillet();
            IntfDalTarifBaseCommission serviceTarifBaseCommission = new ImplDalTarifBaseCommission();
            #endregion

            #region implementation
            if (numVilleD != "" && numVilleF != "")
            {
                this.strCommande = "SELECT * FROM trajet WHERE";
                this.strCommande += " (trajet.numVilleD = '" + numVilleD + "' AND trajet.numVilleF =  '" + numVilleF + "')";
                this.strCommande += " OR (trajet.numVilleD = '" + numVilleF + "' AND trajet.numVilleF =  '" + numVilleD + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            Trajet = new crlTrajet();
                            Trajet.NumTrajet = reader["numTrajet"].ToString();
                            Trajet.NumTarifBaseBillet = reader["numTarifBaseBillet"].ToString();
                            try
                            {
                                Trajet.DistanceTrajet = int.Parse(reader["distanceTrajet"].ToString());
                            }
                            catch (Exception)
                            {
                                Trajet.DistanceTrajet = 0;
                            }
                            Trajet.DureeTrajet = reader["dureeTrajet"].ToString();
                            Trajet.NumVilleD = reader["numVilleD"].ToString();
                            Trajet.NumVilleF = reader["numVilleF"].ToString();
                            
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if(Trajet != null)
                {
                    if (Trajet.NumVilleD != "")
                    {
                        Trajet.villeD = serviceVille.selectVille(Trajet.NumVilleD);
                    }
                    if (Trajet.NumVilleF != "")
                    {
                        Trajet.villeF = serviceVille.selectVille(Trajet.NumVilleF);
                    }
                    if (Trajet.NumTarifBaseBillet != "")
                    {
                        Trajet.tarifBaseBillet = serviceTarifBaseBillet.selectTarifBaseBillet(Trajet.NumTarifBaseBillet);
                    }

                    Trajet.tarifBaseCommissions = serviceTarifBaseCommission.selectTarifBaseCommissions(Trajet.NumTrajet);
                }
            }
            #endregion

            return Trajet;
        }

        List<crlTrajet> IntfDalTrajet.selectTrajets(string idItineraire)
        {
            #region declaration
            List<crlTrajet> Trajets = null;
            crlTrajet tempTrajet = null;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            this.strCommande = "SELECT trajet.numTrajet, trajet.numTarifBaseBillet, trajet.distanceTrajet,trajet.numVilleD,";
            this.strCommande += " trajet.dureeTrajet, trajet.numVilleF";
            this.strCommande += " FROM trajet Inner Join associationvilletrajet ON associationvilletrajet.numTrajet = trajet.numTrajet";
            this.strCommande += " Inner Join ville ON ville.numVille = associationvilletrajet.numVille";
            this.strCommande += " Inner Join associationtrajetitineraire ON associationtrajetitineraire.numTrajet = trajet.numTrajet";
            this.strCommande += " WHERE associationtrajetitineraire.idItineraire =  '" + idItineraire + "'";

            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    Trajets = new List<crlTrajet>();
                    while (reader.Read())
                    {
                        tempTrajet = new crlTrajet();
                        tempTrajet.NumTrajet = reader["numTrajet"].ToString();
                        tempTrajet.NumTarifBaseBillet = reader["numTarifBaseBillet"].ToString();
                        tempTrajet.DistanceTrajet = int.Parse(reader["distanceTrajet"].ToString());
                        tempTrajet.DureeTrajet = reader["dureeTrajet"].ToString();
                        tempTrajet.NumVilleD = reader["numVilleD"].ToString();
                        tempTrajet.NumVilleF = reader["numVilleF"].ToString();

                        Trajets.Add(tempTrajet);
                    }
                }
                
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            if (Trajets != null)
            {
                for (int i = 0; i < Trajets.Count; i++)
                {
                    Trajets[i] = serviceTrajet.selectTrajet(Trajets[i].NumTrajet);
                }
            }
            #endregion

            return Trajets;
        }

        List<crlTrajet> IntfDalTrajet.selectTrajetsVille(string numVille)
        {
            #region declaration
            List<crlTrajet> Trajets = null;
            crlTrajet tempTrajet = null;
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            this.strCommande = "SELECT trajet.numTrajet, trajet.numTarifBaseBillet, trajet.distanceTrajet,";
            this.strCommande += " trajet.dureeTrajet, trajet.numVilleD, trajet.numVilleF FROM trajet";
            this.strCommande += " Inner Join associationvilletrajet ON associationvilletrajet.numTrajet = trajet.numTrajet";
            this.strCommande += " WHERE associationvilletrajet.numVille = '" + numVille + "'";

            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(this.strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    Trajets = new List<crlTrajet>();
                    while (reader.Read())
                    {
                        tempTrajet = new crlTrajet();
                        tempTrajet.NumTrajet = reader["numTrajet"].ToString();
                        tempTrajet.NumTarifBaseBillet = reader["numTarifBaseBillet"].ToString();
                        tempTrajet.DistanceTrajet = int.Parse(reader["distanceTrajet"].ToString());
                        tempTrajet.DureeTrajet = reader["dureeTrajet"].ToString();
                        tempTrajet.NumVilleD = reader["numVilleD"].ToString();
                        tempTrajet.NumVilleF = reader["numVilleF"].ToString();

                        Trajets.Add(tempTrajet);
                    }
                }
                if (Trajets != null)
                {
                    for (int i = 0; i < Trajets.Count; i++)
                    {
                        Trajets[i] = serviceTrajet.selectTrajet(Trajets[i].NumTrajet);
                    }
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return Trajets;
        }
        
        crlTrajet IntfDalTrajet.selectTrajet(string numTrajet)
        {
            #region declaration
            crlTrajet Trajet = null;
            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalTarifBaseBillet serviceTarifBaseBillet = new ImplDalTarifBaseBillet();
            IntfDalTarifBaseCommission serviceTarifBaseCommission = new ImplDalTarifBaseCommission();
            int i = 0;
            #endregion

            #region implementation
            if (numTrajet != "")
            {
                this.strCommande = "SELECT * FROM `trajet` WHERE (`numTrajet`='" + numTrajet + "')";

                this.serviceConnectBase.openConnection();
                reader = this.serviceConnectBase.select(this.strCommande);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Trajet = new crlTrajet();
                        i = 1;
                        while (reader.Read())
                        {
                           
                                Trajet.NumTrajet = reader["numTrajet"].ToString();
                                Trajet.NumTarifBaseBillet = reader["numTarifBaseBillet"].ToString();
                                try
                                {
                                    Trajet.DistanceTrajet = int.Parse(reader["distanceTrajet"].ToString());
                                    
                                }
                                catch (Exception)
                                {}

                                Trajet.DureeTrajet = reader["dureeTrajet"].ToString();
                                Trajet.NumVilleD = reader["numVilleD"].ToString();
                                Trajet.NumVilleF = reader["numVilleF"].ToString();

                                
                        }
                    }
                    reader.Dispose();
                }
                this.serviceConnectBase.closeConnection();
                if (Trajet != null)
                {
                    if (Trajet.NumVilleD != "")
                    {
                        Trajet.villeD = serviceVille.selectVille(Trajet.NumVilleD);
                    }
                    if (Trajet.NumVilleF != "")
                    {
                        Trajet.villeF = serviceVille.selectVille(Trajet.NumVilleF);
                    }
                    if (Trajet.NumTarifBaseBillet != "")
                    {
                        Trajet.tarifBaseBillet = serviceTarifBaseBillet.selectTarifBaseBillet(Trajet.NumTarifBaseBillet);
                    }

                    Trajet.tarifBaseCommissions = serviceTarifBaseCommission.selectTarifBaseCommissions(Trajet.NumTrajet);
                }
            }
            #endregion

            return Trajet;
        }

        string IntfDalTrajet.getNumTrajet(string sigleAgence)
        {
            #region declaration
            double numTemp = 0;
            string numTrajet = "00001";
            string[] tempNumTrajet = null;
            string strDate = "TR" + DateTime.Now.ToString("yyMM");
            #endregion

            #region implementation
            this.strCommande = "SELECT trajet.numTrajet AS maxNum FROM trajet";
            this.strCommande += " WHERE trajet.numTrajet LIKE '%" + sigleAgence + "%'";
            this.strCommande += " ORDER BY maxNum DESC";
            this.serviceConnectBase.openConnection();
            reader = this.serviceConnectBase.select(strCommande);
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tempNumTrajet = reader["maxNum"].ToString().ToString().Split('/');
                        numTrajet = tempNumTrajet[tempNumTrajet.Length - 1];
                    }
                    numTemp = double.Parse(numTrajet) + 1;
                    if (numTemp < 10)
                        numTrajet = "0000" + numTemp.ToString("0");
                    if (numTemp < 100 && numTemp >= 10)
                        numTrajet = "000" + numTemp.ToString("0");
                    if (numTemp < 1000 && numTemp >= 100)
                        numTrajet = "00" + numTemp.ToString("0");
                    if (numTemp < 10000 && numTemp >= 1000)
                        numTrajet = "0" + numTemp.ToString("0");
                    if (numTemp >= 10000)
                        numTrajet = "" + numTemp.ToString("0");
                }
                reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            numTrajet = strDate + "/" + sigleAgence + "/" + numTrajet;
            #endregion

            return numTrajet;
        }

        void IntfDalTrajet.loadDdlListeTrajet(DropDownList ddl, string idItineraire, string ville)
        {
            #region initialisation
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            List<crlTrajet> Trajets = null;
            #endregion

            #region implementation
            if (idItineraire != "" && ville != "")
            {
                Trajets = serviceTrajet.selectTrajets(idItineraire);
                if (Trajets != null)
                {
                    ddl.Items.Clear();
                    for (int i = 0; i < Trajets.Count; i++)
                    {
                        if (Trajets[i].villeD.NomVille.Trim().ToLower().Equals(ville.Trim().ToLower()) || Trajets[i].villeF.NomVille.Trim().ToLower().Equals(ville.Trim().ToLower()))
                        {
                            if (Trajets[i].villeD.NomVille.Trim().ToLower().Equals(ville.Trim().ToLower()))
                            {
                                ddl.Items.Add(new ListItem(Trajets[i].villeD.NomVille + "-" + Trajets[i].villeF.NomVille, Trajets[i].NumTrajet));
                            }
                            else
                            {
                                ddl.Items.Add(new ListItem(Trajets[i].villeF.NomVille + "-" + Trajets[i].villeD.NomVille, Trajets[i].NumTrajet));
                            }
                        }
                    }
                }
            }
            #endregion
        }

        #endregion

        #region insert to grid
        void IntfDalTrajet.insertToGridTrajetItineraire(GridView gridView, string param, string paramLike, string valueLike, string idItineraire)
        {
            #region declaration
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            #endregion

            #region implementation
            this.strCommande = "SELECT trajet.numTrajet, trajet.distanceTrajet, trajet.dureeTrajet,";
            this.strCommande += " trajet.numVilleD, trajet.numVilleF, trajet.numTarifBaseBillet FROM trajet";
            this.strCommande += " Inner Join associationvilletrajet ON associationvilletrajet.numTrajet = trajet.numTrajet";
            this.strCommande += " Inner Join ville ON ville.numVille = associationvilletrajet.numVille";
            this.strCommande += " Inner Join associationtrajetitineraire ON associationtrajetitineraire.numTrajet = trajet.numTrajet";
            this.strCommande += " WHERE " + paramLike + " LIKE '%" + valueLike + "%' AND";
            this.strCommande += " associationtrajetitineraire.idItineraire = '" + idItineraire + "'";
            this.strCommande += " GROUP BY trajet.numTrajet";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceTrajet.getDataTableTrajetItineraire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalTrajet.getDataTableTrajetItineraire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = null;
            crlVille villeF = null;
            string strTrajet = "";
            #endregion

            #region initialise table
            dataTable.Columns.Add("numTrajet", typeof(string));
            dataTable.Columns.Add("distanceTrajet", typeof(string));
            dataTable.Columns.Add("dureeTrajet", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));

            DataRow dr = null;
            #endregion

            #region implementation
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numTrajet"] = this.reader["numTrajet"].ToString();
                        dr["distanceTrajet"] = this.reader["distanceTrajet"].ToString() + "Km";
                        dr["dureeTrajet"] = serviceGeneral.getTextTimeSpan(this.reader["dureeTrajet"].ToString());

                        villeD = serviceVille.selectVille(this.reader["numVilleD"].ToString());
                        villeF = serviceVille.selectVille(this.reader["numVilleF"].ToString());

                        if (villeD != null)
                        {
                            strTrajet += villeD.NomVille;
                        }
                        else
                        {
                            strTrajet += this.reader["numVilleD"].ToString();
                        }

                        if (villeF != null)
                        {
                            strTrajet += "-" + villeF.NomVille;
                        }
                        else
                        {
                            strTrajet += "-" + this.reader["numVilleF"].ToString();
                        }

                        dr["trajet"] = strTrajet;
                        strTrajet = "";

                        dataTable.Rows.Add(dr);
                    }
                }
                this.reader.Dispose();
            }
            this.serviceConnectBase.closeConnection();
            #endregion

            return dataTable;
        }

        void IntfDalTrajet.insertToGridTrajetNotItineraire(GridView gridView, string param, string paramLike, string valueLike, List<crlTrajet> trajets)
        {
            #region declaration
            IntfDalTrajet serviceTrajet = new ImplDalTrajet();
            string strWhere = "";
            #endregion

            #region implementation
            if (trajets != null)
            {
                for (int i = 0; i < trajets.Count; i++)
                {
                    strWhere += " trajet.numTrajet <> '" + trajets[i].NumTrajet + "' AND";
                }
            }
            this.strCommande = "SELECT trajet.numTrajet, trajet.distanceTrajet, trajet.dureeTrajet,";
            this.strCommande += " trajet.numVilleD, trajet.numVilleF, trajet.numTarifBaseBillet FROM trajet";
            this.strCommande += " Inner Join associationvilletrajet ON associationvilletrajet.numTrajet = trajet.numTrajet";
            this.strCommande += " Inner Join ville ON ville.numVille = associationvilletrajet.numVille";
            this.strCommande += " WHERE " + strWhere;
            this.strCommande += " " + paramLike + " LIKE '%" + valueLike + "%'";
            this.strCommande += " GROUP BY trajet.numTrajet";
            this.strCommande += " ORDER BY " + param + " ASC";

            gridView.DataSource = serviceTrajet.getDataTableTrajetItineraire(this.strCommande);
            gridView.DataBind();
            #endregion
        }

        DataTable IntfDalTrajet.getDataTableTrajetNotItineraire(string strRqst)
        {
            #region declaration
            DataTable dataTable = new DataTable();

            IntfDalVille serviceVille = new ImplDalVille();
            IntfDalGeneral serviceGeneral = new ImplDalGeneral();

            crlVille villeD = null;
            crlVille villeF = null;
            string strTrajet = "";
            #endregion

            #region initialise table
            dataTable.Columns.Add("numTrajet", typeof(string));
            dataTable.Columns.Add("distanceTrajet", typeof(string));
            dataTable.Columns.Add("dureeTrajet", typeof(string));
            dataTable.Columns.Add("trajet", typeof(string));

            DataRow dr = null;
            #endregion

            #region implementation
            this.serviceConnectBase.openConnection();
            this.reader = this.serviceConnectBase.select(strRqst);
            if (this.reader != null)
            {
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        dr = dataTable.NewRow();

                        dr["numTrajet"] = this.reader["numTrajet"].ToString();
                        dr["distanceTrajet"] = this.reader["distanceTrajet"].ToString() + "Km";
                        dr["dureeTrajet"] = serviceGeneral.getTextTimeSpan(this.reader["dureeTrajet"].ToString());

                        villeD = serviceVille.selectVille(this.reader["numVilleD"].ToString());
                        villeF = serviceVille.selectVille(this.reader["numVilleF"].ToString());

                        if (villeD != null)
                        {
                            strTrajet += villeD.NomVille;
                        }
                        else
                        {
                            strTrajet += this.reader["numVilleD"].ToString();
                        }

                        if (villeF != null)
                        {
                            strTrajet += "-" + villeF.NomVille;
                        }
                        else
                        {
                            strTrajet += "-" +  this.reader["numVilleF"].ToString();
                        }

                        dr["trajet"] = strTrajet;
                        strTrajet = "";

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