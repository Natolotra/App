DELIMITER |
DROP PROCEDURE IF EXISTS Itineraire_GetAllItineraire|
CREATE PROCEDURE Itineraire_GetAllItineraire()
BEGIN
    SELECT itineraire.idItineraire, itineraire.distanceParcour, itineraire.nombreRepos,
    itineraire.dureeTrajet, itineraire.numVilleItineraireDebut, itineraire.numVilleItineraireFin,
    itineraire.numInfoBagage, vd.nomVille AS VilleD, vf.nomVille AS VilleF, tb.montantTarifBaseBillet
    FROM itineraire
    INNER JOIN ville AS vd ON vd.numVille = itineraire.numVilleItineraireDebut
    INNER JOIN ville AS vf ON vf.numVille = itineraire.numVilleItineraireFin
    INNER JOIN associationtrajetitineraire AS ati ON ati.idItineraire = itineraire.idItineraire
    INNER JOIN trajet AS tr ON tr.numVilleD = itineraire.numVilleItineraireDebut AND tr.numVilleF = itineraire.numVilleItineraireFin AND tr.numTrajet = ati.numTrajet
    INNER JOIN tarifbasebillet AS tb ON tb.numTarifBaseBillet = tr.numTarifBaseBillet
    GROUP BY itineraire.idItineraire;
END|
DELIMITER ;