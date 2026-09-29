-- 25. feladat
SELECT DISTINCT telepules
FROM tagok
WHERE irsz = (SELECT irsz FROM tagok WHERE telepules = 'Kerepes' LIMIT 1);
