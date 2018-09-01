

namespace arch.crl
{
    /// <summary>
    /// Objet voiture
    /// </summary>
    public class crlVoiture
    {
        #region variable
        private string numVehicule;
        private string couleur;
        private string type;
        private string marque;
        private int nombrePlace;
        private int colone;
        private string numImmatricule;
        private string numLicence;
        #endregion

        #region Variable d'objet
        public crlLicence Licence;
        #endregion

        #region encapsulation
        public string NumVehicule
        {
            get { return numVehicule; }
            set { numVehicule = value; }
        }
        public string Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Marque
        {
            get { return marque; }
            set { marque = value; }
        }
        public int NombrePlace
        {
            get { return nombrePlace; }
            set { nombrePlace = value; }
        }
        public string NumImmatricule
        {
            get { return numImmatricule; }
            set { numImmatricule = value; }
        }
        public string NumLicence
        {
            get { return numLicence; }
            set { numLicence = value; }
        }
        public int Colone
        {
            get
            {
                return colone;
            }
            set
            {
                colone = value;
            }
        }
        #endregion

        #region constructeur
        public crlVoiture()
        {
            this.Couleur = "";
            this.Marque = "";
            this.NombrePlace = 0;
            this.NumImmatricule = "";
            this.NumLicence = "";
            this.NumVehicule = "";
            this.Type = "";
            this.Colone = 0;
            this.Licence = null;
        }
        #endregion
    }
}