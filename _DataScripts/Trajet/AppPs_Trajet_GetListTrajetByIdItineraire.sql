DELIMITER |
DROP PROCEDURE IF EXISTS Trajet_GetListTrajetByIdItineraire|
CREATE PROCEDURE Trajet_GetListTrajetByIdItineraire(IN idIt VARCHAR(254))
BEGIN
    SELECT trajet.numTrajet, trajet.distanceTrajet, trajet.dureeTrajet,
    trajet.numVilleD, trajet.numVilleF, trajet.numTarifBaseBillet,
    vd.nomVille AS VilleD, vf.nomVille AS VilleF
    FROM trajet
    INNER JOIN associationtrajetitineraire AS ati ON ati.numTrajet = trajet.numTrajet
    INNER JOIN ville AS vd ON vd.numVille = trajet.numVilleD
    INNER JOIN ville AS vf ON vf.numVille = trajet.numVilleF;
    WHERE ati.idItineraire = idIt;
END|
DELIMITER ;