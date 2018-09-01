using System;

namespace arch.crl
{
    /// <summary>
    /// Objet autorisation de voyage
    /// </summary>
    public class crlAutorisationVoyage
    {
        #region variable
        private string numerosAV;
        private DateTime datePrevueDepart;
        private string matriculeAgent;
        private string idVerification;
        #endregion

        #region encapsulation
        public string NumerosAV
        {
            get { return numerosAV; }
            set { numerosAV = value; }
        }
        public DateTime DatePrevueDepart
        {
            get { return datePrevueDepart; }
            set { datePrevueDepart = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public string IdVerification
        {
            get { return idVerification; }
            set { idVerification = value; }
        }
        #endregion

        #region variable d'objet
        public crlAgent Agent;
        public crlVerification Verification;
        #endregion

        #region constructeur
        public crlAutorisationVoyage()
        {
            this.DatePrevueDepart = DateTime.Now;
            this.IdVerification = "";
            this.MatriculeAgent = "";
            this.NumerosAV = "";
            this.Verification = null;
            this.Agent = null;
        }
        #endregion
    }
}
