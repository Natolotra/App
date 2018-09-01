using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace arch.dal.intf
{
    /// <summary>
    /// Description résumée de IntfDalCA
    /// </summary>
    public interface IntfDalCA
    {
        #region generale
        double montantCAGenerale(string dateDebut, string dateFin);

        void insertToGridCAGenerale(GridView gridView, string dateDebut, string dateFin);
        DataTable getDataTableCAGenerale(string strRqst);

        double montantCAGeneraleVoiture(string dateDebut, string dateFin, string numVehicule);

        void insertToGridCAGeneraleVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule);
        DataTable getDataTableCAGeneraleVoiture(string strRqst);

        double montantCAGeneraleAxe(string dateDebut, string dateFin, string idItineraire);

        void insertToGridCAGeneraleAxe(GridView gridView, string dateDebut, string dateFin, string idItineraire);
        DataTable getDataTableCAGeneraleAxe(string strRqst);

        double montantCACarburant(string dateDebut, string dateFin);

        void insertToGridCACarburant(GridView gridView, string dateDebut, string dateFin);
        DataTable getDataTableCACarburant(string strRqst);

        double montantCACarburantVoiture(string dateDebut, string dateFin, string numVehicule);

        void insertToGridCACarburantVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule);
        DataTable getDataTableCACarburantVoiture(string strRqst);

        double montantCADeveloppement(string dateDebut, string dateFin);

        void insertToGridCADeveloppement(GridView gridView, string dateDebut, string dateFin);
        DataTable getDataTableCADeveloppement(string strRqst);
        #endregion

        #region generale par agence
        double montantCAGenerale(string dateDebut, string dateFin, string numAgence);

        void insertToGridCAGenerale(GridView gridView, string dateDebut, string dateFin, string numAgence);

        double montantCAGeneraleVoiture(string dateDebut, string dateFin, string numVehicule, string numAgence);

        void insertToGridCAGeneraleVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule, string numAgence);

        double montantCAGeneraleAxe(string dateDebut, string dateFin, string idItineraire, string numAgence);

        void insertToGridCAGeneraleAxe(GridView gridView, string dateDebut, string dateFin, string idItineraire, string numAgence);

        double montantCACarburant(string dateDebut, string dateFin, string numAgence);

        void insertToGridCACarburant(GridView gridView, string dateDebut, string dateFin, string numAgence);

        double montantCACarburantVoiture(string dateDebut, string dateFin, string numVehicule, string numAgence);

        void insertToGridCACarburantVoiture(GridView gridView, string dateDebut, string dateFin, string numVehicule, string numAgence);

        double montantCADeveloppement(string dateDebut, string dateFin, string numAgence);

        void insertToGridCADeveloppement(GridView gridView, string dateDebut, string dateFin, string numAgence);
        #endregion

        #region get CA par agence
        double getCAGenerale(string dateD, string dateF, string numAgence);
        double getCAGeneraleVoiture(string dateD, string dateF, string numVehicule, string numAgence);
        double getCAGeneraleAxe(string dateD, string dateF, string idItineraire, string numAgence);
        double getCACarburant(string dateD, string dateF, string numAgence);
        double getCACarburantVoiture(string dateD, string dateF, string numVehicule, string numAgence);
        double getCADeveloppement(string dateD, string dateF, string numAgence);
        #endregion

        #region getCA par proprietaire
        double getRecetteProprietaire(string dateD, string dateF, string numProprietaire);
        double getResteProprietaire(string dateD, string dateF, string numProprietaire);
        void insertToGridListeAD(GridView gridView, string dateD, string dateF, string numProprietaire);
        DataTable getDataTableListeAD(string strRqst);
        #endregion

        #region getCA Fond par vehicule
        double getFondVehicule(string dateD, string dateF, string numVehicule);
        void insertToGridListeRecuAD(GridView gridView, string dateD, string dateF, string numVehicule);
        DataTable getDataTableListeRecuAD(string strRqst);
        #endregion
    }
}