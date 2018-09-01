

namespace arch.crl
{
    /// <summary>
    /// Objet voyage
    /// </summary>
    public class crlVoyage
    {
        #region variable
        private string idVoyage;
        private string numIndividu;
        private string numBillet;
        private string destination;
        private string numPlace;
        private string numerosFB;
        private string pieceIdentite;
        private double poidBagage;
        #endregion

        #region encapsulation
        public string IdVoyage
        {
            get { return idVoyage; }
            set { idVoyage = value; }
        }
        public string NumIndividu
        {
            get
            {
                return numIndividu;
            }
            set
            {
                numIndividu = value;
            }
        }
        public string NumPlace
        {
            get
            {
                return numPlace;
            }
            set
            {
                numPlace = value;
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
        public string NumBillet
        {
            get
            {
                return numBillet;
            }
            set
            {
                numBillet = value;
            }
        }
        public string NumerosFB
        {
            get { return numerosFB; }
            set { numerosFB = value; }
        }
        public double PoidBagage
        {
            get
            {
                return poidBagage;
            }
            set
            {
                poidBagage = value;
            }
        }
        public string PieceIdentite
        {
            get
            {
                return pieceIdentite;
            }
            set
            {
                pieceIdentite = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlBagage bagage;
        public crlBillet billet;
        public crlIndividu individu;
        public crlPlaceFB placeFB;
        #endregion

        #region construction
        public crlVoyage()
        {
           
            this.NumIndividu = "";
            this.IdVoyage = "";
            this.NumerosFB = "";
            this.NumBillet = "";
            this.Destination = "";
            this.NumPlace = "";
            this.PieceIdentite = "";
            this.poidBagage = 0.00;

            this.bagage = null;
            this.individu = null;
            this.billet = null;
            this.placeFB = null;
            
        }
        #endregion
    }
}
