using System;

namespace arch.crl
{
    /// <summary>
    /// Objet commission
    /// </summary>
    public class crlCommission
    {
        #region variable
        private string idCommission;
        private string destination;
        private string poids;
        private int nombre;
        private string pieceJustificatif;
        private string fraisEnvoi;
        private string numExpediteur;
        private string numRecepteur;
        private string numDesignation;
        private string typeCommission;
        private int isRecu;
        private string numTrajet;
        private DateTime dateCommission;
        private string matriculeAgent;
        private string matriculeAgentDelivreur;
        private DateTime dateLivraison;
        private string modePaiement;
        #endregion

        #region encapsulation
        public string IdCommission
        {
            get { return idCommission; }
            set { idCommission = value; }
        }
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public string Poids
        {
            get
            {
                return poids;
            }
            set
            {
                poids = value;
            }
        }
        public string PieceJustificatif
        {
            get { return pieceJustificatif; }
            set { pieceJustificatif = value; }
        }
        public string FraisEnvoi
        {
            get { return fraisEnvoi; }
            set { fraisEnvoi = value; }
        }
        public string NumExpediteur
        {
            get
            {
                return numExpediteur;
            }
            set
            {
                numExpediteur = value;
            }
        }
        public string NumRecepteur
        {
            get
            {
                return numRecepteur;
            }
            set
            {
                numRecepteur = value;
            }
        }
        public string NumDesignation
        {
            get
            {
                return numDesignation;
            }
            set
            {
                numDesignation = value;
            }
        }
        public int Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public string TypeCommission
        {
            get
            {
                return typeCommission;
            }
            set
            {
                typeCommission = value;
            }
        }
        public int IsRecu
        {
            get
            {
                return isRecu;
            }
            set
            {
                isRecu = value;
            }
        }
        public string NumTrajet
        {
            get
            {
                return numTrajet;
            }
            set
            {
                numTrajet = value;
            }
        }
        public DateTime DateCommission
        {
            get
            {
                return dateCommission;
            }
            set
            {
                dateCommission = value;
            }
        }
        public string MatriculeAgent
        {
            get
            {
                return matriculeAgent;
            }
            set
            {
                matriculeAgent = value;
            }
        }
        public string MatriculeAgentDelivreur
        {
            get
            {
                return matriculeAgentDelivreur;
            }
            set
            {
                matriculeAgentDelivreur = value;
            }
        }
        public DateTime DateLivraison
        {
            get
            {
                return dateLivraison;
            }
            set
            {
                dateLivraison = value;
            }
        }
        public string ModePaiement
        {
            get
            {
                return modePaiement;
            }
            set
            {
                modePaiement = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlClient expediteur;
        public crlReceptionnaire recepteur;
        public crlTypeCommssion typeCommssionObjet;
        public crlDesignationCommission designationCommission;
        public crlAgent agent;
        public crlAgent agentDelivreur;
        #endregion

        #region constructeur
        public crlCommission()
        {
            this.Destination = "";
            this.FraisEnvoi = "";
            this.idCommission = "";
            this.PieceJustificatif = "";
            this.Poids = "";
            this.NumExpediteur = "";
            this.NumRecepteur = "";
            this.IsRecu = 0;
            this.Nombre = 0;
            this.NumDesignation = "";
            this.NumTrajet = "";
            this.TypeCommission = "";
            this.DateCommission = DateTime.Now;
            this.MatriculeAgent = "";
            this.MatriculeAgentDelivreur = "";
            this.DateLivraison = DateTime.Now;
            this.ModePaiement = "";

            this.recepteur = null;
            this.expediteur = null;
            this.typeCommssionObjet = null;
            this.designationCommission = null;
            this.agent = null;
            this.agentDelivreur = null;
        }
        #endregion

    }
}
