
-- 1. feladat
-- A megoldas-ures.sql fajlt solti-csongor-urhajozas.sql nevre neveztem at.

-- 2. feladat
CREATE DATABASE `urhajozas` CHARACTER SET utf8 COLLATE utf8_hungarian_ci;

-- 3. feladat
USE `urhajozas`;

-- 4. feladat
SET FOREIGN_KEY_CHECKS = 0;
SOURCE /urhajozas/urhajozas-tablak.sql;
SOURCE /urhajozas/urhajozas-adatok.sql;
SET FOREIGN_KEY_CHECKS = 1;

-- 5. feladat
SELECT nev, nem, szulev
FROM urhajos;

-- 6. feladat
SELECT megnevezes, DATEDIFF(veg, kezdet) AS nap
FROM kuldetes;

-- 7. feladat
SELECT nev, YEAR(CURDATE()) - szulev AS kor
FROM urhajos
ORDER BY kor DESC;

-- 8. feladat
SELECT k.megnevezes, u.nev
FROM kuldetes k
JOIN repules r ON r.kuldetes_id = k.id
JOIN urhajos u ON u.id = r.urhajos_id
ORDER BY k.kezdet ASC, u.nev DESC;

-- 9. feladat
SELECT nev, szulev
FROM urhajos
WHERE orszag = 'CAN' AND nem = 'N' AND szulev > 1960;

-- 10. feladat
SELECT nev
FROM urhajos
ORDER BY CHAR_LENGTH(nev) DESC
LIMIT 1;

-- 11. feladat
SELECT k.megnevezes, COUNT(*) AS fo
FROM kuldetes k
JOIN repules r ON r.kuldetes_id = k.id
GROUP BY k.id, k.megnevezes;

-- 12. feladat
SELECT u.nev, COUNT(*) AS db
FROM urhajos u
JOIN repules r ON r.urhajos_id = u.id
GROUP BY u.id, u.nev
HAVING COUNT(*) >= 6;

-- 13. feladat
SELECT ROUND(AVG(DATEDIFF(veg, kezdet)), 2) AS `Gemini kuldetesek atlagos hosszusaga`
FROM kuldetes
WHERE megnevezes LIKE 'Gemini%';

-- 14. feladat
SELECT u.orszag
FROM kuldetes k
JOIN repules r ON r.kuldetes_id = k.id
JOIN urhajos u ON u.id = r.urhajos_id
WHERE YEAR(k.kezdet) BETWEEN 1991 AND 2000
GROUP BY u.orszag
ORDER BY COUNT(*) DESC
LIMIT 3;

-- 15. feladat
SELECT COUNT(*) AS `Robik szama`
FROM urhajos
WHERE nev LIKE 'Robert%';

-- 16. feladat
SELECT nev, orszag, szulev
FROM urhajos
WHERE szulev = (SELECT szulev FROM urhajos WHERE nev = 'Barbara Morgan');

-- 17. feladat
SELECT k.megnevezes, k.kezdet, k.veg
FROM kuldetes k
WHERE EXISTS (
    SELECT 1 FROM repules r WHERE r.kuldetes_id = k.id
)
AND NOT EXISTS (
    SELECT 1
    FROM repules r
    JOIN urhajos u ON u.id = r.urhajos_id
    WHERE r.kuldetes_id = k.id AND u.nem <> 'N'
);

-- 18. feladat
DELETE FROM urhajos WHERE nev = 'Serbán Lajos';

-- 19. feladat
INSERT INTO urhajos (id, nev, orszag, nem, szulev, urido)
VALUES (561, 'Alexander Poleshchuk', 'RUS', 'F', 1953, 'T179:00:43');

-- 20. feladat
ALTER TABLE kuldetes
  ADD COLUMN honapok DECIMAL(6,2) DEFAULT NULL;

-- 21. feladat
UPDATE kuldetes
SET honapok = ROUND(DATEDIFF(veg, kezdet) / 30, 2);
