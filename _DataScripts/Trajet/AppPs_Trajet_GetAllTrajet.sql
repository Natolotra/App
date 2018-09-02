DELIMITER |
DROP PROCEDURE IF EXISTS Trajet_GetAllTrajet|
CREATE PROCEDURE Trajet_GetAllTrajet()
BEGIN
    SELECT trajet.numTrajet, trajet.distanceTrajet, trajet.dureeTrajet,
    trajet.numVilleD, trajet.numVilleF, trajet.numTarifBaseBillet,
    vd.nomVille AS VilleD, vf.nomVille AS VilleF, tb.montantTarifBaseBillet
    FROM trajet
    INNER JOIN ville AS vd ON vd.numVille = trajet.numVilleD
    INNER JOIN ville AS vf ON vf.numVille = trajet.numVilleF
    INNER JOIN tarifbasebillet AS tb ON tb.numTarifBaseBillet = trajet.numTarifBaseBillet;
END|
DELIMITER ;