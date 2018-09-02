DELIMITER |
DROP PROCEDURE IF EXISTS Itineraire_GetAllItineraire|
CREATE PROCEDURE Itineraire_GetAllItineraire()
BEGIN
    SELECT itineraire.idItineraire, itineraire.distanceParcour, itineraire.nombreRepos,
    itineraire.dureeTrajet, itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin,
    itineraire.numInfoBagage, vd.nomVille AS VilleD, vf.nomVille AS VilleF
    FROM itineraire
    INNER JOIN ville AS vd ON vd.numVille = itineraire.numVilleItineraireDebut
    INNER JOIN ville AS vf ON vf.numVille = itineraire.numVilleItineraireFin;
END|
DELIMITER ;