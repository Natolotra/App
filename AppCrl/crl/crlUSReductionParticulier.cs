using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace arch.crl
{
    /// <summary>
    /// Description résumée de crlUSReductionParticulier
    /// </summary>
    public class crlUSReductionParticulier
    {
        #region variable
        string numUSReductionParticulier;
        string numIndividu;
        string numEtablissementScolaire;
        string numSociete;
        string numCategorieBillet;
        string imageReductionParticulier;
        #endregion

        #region encapsulation
        public string NumUSReductionParticulier
        {
            get
            {
                return numUSReductionParticulier;
            }
            set
            {
                numUSReductionParticulier = value;
            }
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
        public string NumCategorieBillet
        {
            get { return numCategorieBillet; }
            set { numCategorieBillet = value; }
        }
        public string ImageReductionParticulier
        {
            get
            {
                return imageReductionParticulier;
            }
            set
            {
                imageReductionParticulier = value;
            }
        }
        #endregion

        #region variable d'objet
        public crlIndividu individu;
        public crlUSCategorieBillet categorieBillet;
        #endregion

        #region constructeur
        public crlUSReductionParticulier()
        {
            this.NumUSReductionParticulier = "";
            this.NumIndividu = "";
            this.NumEtablissementScolaire = "";
            this.NumSociete = "";
            this.NumCategorieBillet = "";
            this.ImageReductionParticulier = "reduction.png";

            this.individu = null;
            this.categorieBillet = null;
        }
        #endregion
    }
}