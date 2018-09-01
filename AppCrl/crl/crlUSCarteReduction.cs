using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arch.crl
{
    /// <summary>
    /// Objet carte de reduction
    /// </summary>
    public class crlUSCarteReduction
    {
        #region variable
        private string numCarteReduction;
        private string numClient;
        private string numCategorieBillet;
        private DateTime dateValideDu;
        private DateTime dateValideAu;
        private int isLundi;
        private int isMardi;
        private int isMercredi;
        private int isJeudi;
        private int isVendredi;
        private int isSamedi;
        private int isDimanche;
        private string etatCivil;
        private string imageCarteReduction;
        private DateTime dateNaissance;
        private DateTime dateDelivranceCertificatScolarite;
        private DateTime dateAtestationEmployeur;
        private string numEtablissementScolaire;
        private string numSociete;
        #endregion

        #region accesseur
        public string NumCarteReduction
        {
            get
            {
                return numCarteReduction;
            }
            set
            {
                numCarteReduction = value;
            }
        }
        public string NumClient
        {
            get
            {
                return numClient;
            }
            set
            {
                numClient = value;
            }
        }
        public string NumCategorieBillet
        {
            get
            {
                return numCategorieBillet;
            }
            set
            {
                numCategorieBillet = value;
            }
        }
        public DateTime DateValideDu
        {
            get
            {
                return dateValideDu;
            }
            set
            {
                dateValideDu = value;
            }
        }
        public DateTime DateValideAu
        {
            get
            {
                return dateValideAu;
            }
            set
            {
                dateValideAu = value;
            }
        }
        public int IsLundi
        {
            get
            {
                return isLundi;
            }
            set
            {
                isLundi = value;
            }
        }
        public int IsMardi
        {
            get
            {
                return isMardi;
            }
            set
            {
                isMardi = value;
            }
        }
        public int IsMercredi
        {
            get
            {
                return isMercredi;
            }
            set
            {
                isMercredi = value;
            }
        }
        public int IsJeudi
        {
            get
            {
                return isJeudi;
            }
            set
            {
                isJeudi = value;
            }
        }
        public int IsVendredi
        {
            get
            {
                return isVendredi;
            }
            set
            {
                isVendredi = value;
            }
        }
        public int IsSamedi
        {
            get
            {
                return isSamedi;
            }
            set
            {
                isSamedi = value;
            }
        }
        public int IsDimanche
        {
            get
            {
                return isDimanche;
            }
            set
            {
                isDimanche = value;
            }
        }
        public string EtatCivil
        {
            get { return etatCivil; }
            set { etatCivil = value; }
        }
        public string ImageCarteReduction
        {
            get { return imageCarteReduction; }
            set { imageCarteReduction = value; }
        }
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
        }
        public DateTime DateDelivranceCertificatScolarite
        {
            get
            {
                return dateDelivranceCertificatScolarite;
            }
            set
            {
                dateDelivranceCertificatScolarite = value;
            }
        }
        public DateTime DateAtestationEmployeur
        {
            get
            {
                return dateAtestationEmployeur;
            }
            set
            {
                dateAtestationEmployeur = value;
            }
        }
        public string NumEtablissementScolaire
        {
            get { return numEtablissementScolaire; }
            set { numEtablissementScolaire = value; }
        }
        public string NumSociete
        {
            get { return numSociete; }
            set { numSociete = value; }
        }
        #endregion

        #region variable d'objet
        public crlUSCategorieBillet categorieBillet;
        public crlClient client;
        #endregion

        #region constructeur
        public crlUSCarteReduction()
        {
            this.NumCarteReduction = "";
            this.NumCategorieBillet = "";
            this.NumClient = "";
            this.DateValideAu = DateTime.Now;
            this.DateValideDu = DateTime.Now;
            this.IsDimanche = 0;
            this.IsJeudi = 0;
            this.IsLundi = 0;
            this.IsMardi = 0;
            this.IsMercredi = 0;
            this.IsSamedi = 0;
            this.IsVendredi = 0;
            this.EtatCivil = "";
            this.ImageCarteReduction = "reduction.png";
            this.DateNaissance = DateTime.Now;
            this.DateAtestationEmployeur = new DateTime(1, 1, 1);
            this.DateDelivranceCertificatScolarite = new DateTime(1, 1, 1);

            this.NumEtablissementScolaire = "";
            this.NumSociete = "";

            this.categorieBillet = null;
            this.client = null;
        }
        #endregion
    }
}