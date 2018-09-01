namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlCommissionDevis
    /// </summary>
    public class crlCommissionDevis
    {
        #region variable
        private string idCommissionDevis;
        private string destination;
        private double poids;
        private int nombre;
        private string pieceJustificatif;
        private double fraisEnvoi;
        private string numDesignation;
        private string typeCommission;
        private string numTrajet;
        private string numProforma;
        private string numExpediteur;
        private string numRecepteur;
        #endregion

        #region encapsulation
        public string IdCommissionDevis
        {
            get
            {
                return idCommissionDevis;
            }
            set
            {
                idCommissionDevis = value;
            }
        }
        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
            }
        }
        public double Poids
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
            get
            {
                return pieceJustificatif;
            }
            set
            {
                pieceJustificatif = value;
            }
        }
        public double FraisEnvoi
        {
            get
            {
                return fraisEnvoi;
            }
            set
            {
                fraisEnvoi = value;
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
        public string NumProforma
        {
            get { return numProforma; }
            set { numProforma = value; }
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
        #endregion

        #region variable d'objet
        public crlTypeCommssion typeCommssionObjet;
        public crlDesignationCommission designationCommission;
        public crlTrajet trajet;
        public crlClient expediteur;
        public crlReceptionnaire recepteur;
        #endregion

        #region costructeur
        public crlCommissionDevis()
        {
            this.IdCommissionDevis = "";
            this.Destination = "";
            this.FraisEnvoi = 0;
            this.PieceJustificatif = "";
            this.Poids = 0;
            this.Nombre = 0;
            this.NumDesignation = "";
            this.NumTrajet = "";
            this.TypeCommission = "";
            this.NumProforma = "";
            this.NumExpediteur = "";
            this.NumRecepteur = "";

            this.trajet = null;
            this.typeCommssionObjet = null;
            this.designationCommission = null;
            this.expediteur = null;
            this.recepteur = null;
        }
        #endregion
    }
}