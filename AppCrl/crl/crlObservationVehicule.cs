using System;


namespace arch.crl
{
    /// <summary>
    /// Objet observation vehicule
    /// </summary>
    public class crlObservationVehicule
    {
        #region declaration
        private string numObservationVehicule;
        private string numVehicule;
        private string textObesvationVehicule;
        private DateTime dateObservation;
        private int isListeNoire;
        #endregion

        #region encapsulation
        public string NumObservationVehicule
        {
            get { return numObservationVehicule; }
            set { numObservationVehicule = value; }
        }
        public string NumVehicule
        {
            get { return numVehicule; }
            set { numVehicule = value; }
        }
        public string TextObesvationVehicule
        {
            get { return textObesvationVehicule; }
            set { textObesvationVehicule = value; }
        }
        public DateTime DateObservation
        {
            get { return dateObservation; }
            set { dateObservation = value; }
        }
        public int IsListeNoire
        {
            get { return isListeNoire; }
            set { isListeNoire = value; }
        }
        #endregion

        #region costructeur
        public crlObservationVehicule()
        {
            this.DateObservation = DateTime.Now;
            this.IsListeNoire = 0;
            this.NumObservationVehicule = "";
            this.NumVehicule = "";
            this.TextObesvationVehicule = "";
        }
        #endregion
    }
}
