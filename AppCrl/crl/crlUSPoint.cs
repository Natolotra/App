using System;


namespace arch.crl
{
    /// <summary>
    /// Objet point
    /// </summary>
    public class crlUSPoint
    {
        #region variable
        private string numPoint;
        private string matriculeAgent;
        private DateTime dateHeurePoint;
        private string numVoyage;
        private string commentaire;
        private string numArret;
        #endregion

        #region encapsulation
        public string NumPoint
        {
            get { return numPoint; }
            set { numPoint = value; }
        }
        public string MatriculeAgent
        {
            get { return matriculeAgent; }
            set { matriculeAgent = value; }
        }
        public DateTime DateHeurePoint
        {
            get { return dateHeurePoint; }
            set { dateHeurePoint = value; }
        }
        public string NumVoyage
        {
            get { return numVoyage; }
            set { numVoyage = value; }
        }
        public string Commentaire
        {
            get { return commentaire; }
            set { commentaire = value; }
        }
        public string NumArret
        {
            get
            {
                return numArret;
            }
            set
            {
                numArret = value;
            }
        }
        #endregion

        #region constructeur
        public crlUSPoint()
        {
            this.Commentaire = "";
            this.DateHeurePoint = DateTime.Now;
            this.MatriculeAgent = "";
            this.NumPoint = "";
            this.NumVoyage = "";
            this.NumArret = "";
        }
        #endregion
    }
}
