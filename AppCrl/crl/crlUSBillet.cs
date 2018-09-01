using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSBillet
    /// </summary>
    public class crlUSBillet
    {
        #region variable
        string numBillet;
        DateTime dateBillet;
        DateTime valideAu;
        double montant;
        string numZoneD;
        string numZoneF;
        string numLieuD;
        string numLieuF;
        int niveau;
        string matriculeAgent;
        string modeDePaiement;
        string numCarteReduction;
        string numAbonnementNV;
        string numTicket;
        string numReductionBillet;
        string numCategorieBillet;
        #endregion

        #region encapsulation
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
        public DateTime DateBillet
        {
            get
            {
                return dateBillet;
            }
            set
            {
                dateBillet = value;
            }
        }
        public DateTime ValideAu
        {
            get
            {
                return valideAu;
            }
            set
            {
                valideAu = value;
            }
        }
        public double Montant
        {
            get
            {
                return montant;
            }
            set
            {
                montant = value;
            }
        }
        public string NumZoneD
        {
            get
            {
                return numZoneD;
            }
            set
            {
                numZoneD = value;
            }
        }
        public string NumZoneF
        {
            get
            {
                return numZoneF;
            }
            set
            {
                numZoneF = value;
            }
        }
        public string NumLieuD
        {
            get
            {
                return numLieuD;
            }
            set
            {
                numLieuD = value;
            }
        }
        public string NumLieuF
        {
            get
            {
                return numLieuF;
            }
            set
            {
                numLieuF = value;
            }
        }
        public int Niveau
        {
            get
            {
                return niveau;
            }
            set
            {
                niveau = value;
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
        public string ModeDePaiement
        {
            get
            {
                return modeDePaiement;
            }
            set
            {
                modeDePaiement = value;
            }
        }
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
        public string NumAbonnementNV
        {
            get
            {
                return numAbonnementNV;
            }
            set
            {
                numAbonnementNV = value;
            }
        }
        public string NumTicket
        {
            get { return numTicket; }
            set { numTicket = value; }
        }
        public string NumReductionBillet
        {
            get
            {
                return numReductionBillet;
            }
            set
            {
                numReductionBillet = value;
            }
        }
        public string NumCategorieBillet
        {
            get { return numCategorieBillet; }
            set { numCategorieBillet = value; }
        }
        #endregion

        #region variable d'objet
        public crlUSZone zoneD;
        public crlUSZone zoneF;
        #endregion

        #region constructeur
        public crlUSBillet()
        {
            this.DateBillet = DateTime.Now;
            this.MatriculeAgent = "";
            this.ModeDePaiement = "";
            this.Montant = 0;
            this.Niveau = 0;
            this.NumCarteReduction = "";
            this.NumAbonnementNV = "";
            this.numTicket = "";
            this.NumBillet = "";
            this.NumLieuD = "";
            this.NumLieuF = "";
            this.NumZoneD = "";
            this.NumZoneF = "";
            this.ValideAu = DateTime.Now;
            this.NumCategorieBillet = "";
            this.NumReductionBillet = "";
        }
        #endregion
    }
}