-- 21. feladat
UPDATE kuldetes
SET honapok = ROUND(DATEDIFF(veg, kezdet) / 30, 2);
