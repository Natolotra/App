

namespace arch.crl
{
    /// <summary>
    /// Objet escorte voyage
    /// </summary>
    public class crlEscorteVoyage
    {
        #region variable
        private string idEscorteVoyage;
        private string matriculeEscorte;
        private string numerosFB;
        private string trajetEscorte;
        #endregion

        #region encapsulation
        public string IdEscorteVoyage
        {
            get { return idEscorteVoyage; }
            set { idEscorteVoyage = value; }
        }
        public string MatriculeEscorte
        {
            get { return matriculeEscorte; }
            set { matriculeEscorte = value; }
        }
        public string NumerosFB
        {
            get { return numerosFB; }
            set { numerosFB = value; }
        }
        public string TrajetEscorte
        {
            get
            {
                return trajetEscorte;
            }
            set
            {
                trajetEscorte = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlEscorte Escorte; 
        #endregion

        #region constructeur
        public crlEscorteVoyage()
        {
            this.IdEscorteVoyage = "";
            this.MatriculeEscorte = "";
            this.NumerosFB = "";
            this.TrajetEscorte = "";
            this.Escorte = null;
        }
        #endregion
    }
}
