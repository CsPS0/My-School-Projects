-- 1. feladat
-- Átneveztem a megoldas-ures.sql fájlt solti-csongor-kondibelepok.sql-re

-- 2. feladat
CREATE DATABASE IF NOT EXISTS kondibelepok
  CHARACTER SET utf8
  COLLATE utf8_hungarian_ci;

-- 3. feladat
USE `kondibelepok`;
SET FOREIGN_KEY_CHECKS = 0;
SOURCE /kondibelepok/kondi-tablak.sql;
SOURCE /kondibelepok/kondi-adatok.sql;
SET FOREIGN_KEY_CHECKS = 1;

-- 4. feladat
SELECT COUNT(*) AS db
FROM belepok
WHERE megnevezes LIKE '%bérlet%';

-- 5. feladat
SELECT COUNT(*) AS noi_letszam
FROM tagok
WHERE nem = 'nő';

-- 6. feladat
SELECT COUNT(*) AS nyugdijas_db
FROM tagok
WHERE TIMESTAMPDIFF(YEAR, szuletett, '2025-03-31') >= 65;

-- 7. feladat
SELECT ROUND(AVG(TIMESTAMPDIFF(YEAR, szuletett, '2025-03-31')), 2) AS ferfi_atlag
FROM tagok
WHERE nem = 'férfi';

-- 8. feladat
SELECT SUM(b.ar) AS noi_bev_30
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.nem = 'nő'
  AND TIMESTAMPDIFF(YEAR, t.szuletett, '2025-03-31') < 30;

-- 9. feladat
SELECT COUNT(*) AS visaberlet
FROM eladasok e
JOIN tagok t ON e.tag_id = t.id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.kartya_tipusa = 'Visa'
  AND b.megnevezes LIKE '%bérlet%';

-- 10. feladat
SELECT DISTINCT t.id, CONCAT(t.vnev, ' ', t.knev) AS nev, CONCAT(t.irsz, ' ', t.telepules, ', ', t.cim) AS teljes_cim
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.megye = 'Vas'
  AND b.megnevezes LIKE '%TRX%'
ORDER BY t.id;

-- 11. feladat
SELECT vnev, knev, cim
FROM tagok
WHERE nem = 'nő'
  AND telepules <> 'Budapest'
  AND cim LIKE '%krt.%'
ORDER BY vnev, knev;

-- 12. feladat
SELECT vnev, knev, telefon
FROM tagok
WHERE nem = 'nő'
  AND TIMESTAMPDIFF(YEAR, szuletett, '2025-03-31') > 30
  AND (telefon LIKE '(20)%' OR telefon LIKE '(30)%' OR telefon LIKE '(70)%')
ORDER BY vnev, knev;

-- 13. feladat
SELECT SUM(b.ar) AS dayka
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.megye = 'Csongrád'
  AND t.cim LIKE '%Dayka Gábor%';

-- 14. feladat
SELECT irsz, COUNT(*) AS db
FROM tagok
GROUP BY irsz
HAVING COUNT(*) > 8
ORDER BY irsz DESC;

-- 15. feladat
SELECT t.megye, SUM(b.ar) AS koltes
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.megye IS NOT NULL
GROUP BY t.megye
ORDER BY t.megye ASC;

-- 16. feladat
SELECT t.telepules, SUM(b.ar) AS koltes
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
GROUP BY t.telepules
HAVING koltes <= 4000
ORDER BY koltes DESC, t.telepules ASC;

-- 17. feladat
SELECT b.megnevezes, COUNT(*) AS db
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.telepules = 'Budapest'
  AND t.nem = 'férfi'
  AND b.megnevezes LIKE '%bérlet%'
GROUP BY b.megnevezes
ORDER BY db DESC
LIMIT 1;

-- 18. feladat
SELECT t.nem, b.megnevezes, COUNT(*) AS db
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE b.megnevezes LIKE '%korlátlan%'
GROUP BY t.nem, b.megnevezes
ORDER BY b.megnevezes ASC, db DESC;

-- 19. feladat
ALTER TABLE tagok
ADD COLUMN torzsvendeg BOOLEAN NOT NULL DEFAULT FALSE;

-- 20. feladat
DELETE FROM eladasok WHERE tag_id = 1408;
DELETE FROM tagok WHERE id = 1408;

-- 21. feladat
INSERT INTO belepok (megnevezes, ar, ervenyes) VALUES
('Úszójegy', 2500, 1),
('Éves úszóbérlet', 250000, 365);

-- 22. feladat
CREATE TABLE kedvezmenyek (
  id INT AUTO_INCREMENT PRIMARY KEY,
  megnevezes VARCHAR(30)
);

-- 23. feladat
ALTER TABLE tagok
ADD COLUMN kedvezmeny_id INT;

-- 24. feladat
ALTER TABLE tagok
ADD CONSTRAINT FK_tagok_kedvezmeny_id
FOREIGN KEY (kedvezmeny_id)
REFERENCES kedvezmenyek(id)
ON DELETE RESTRICT
ON UPDATE CASCADE;

-- 25. feladat
SELECT DISTINCT telepules
FROM tagok
WHERE irsz = (SELECT irsz FROM tagok WHERE telepules = 'Kerepes' LIMIT 1);

-- 26. feladat
SELECT COUNT(DISTINCT e1.tag_id) AS db
FROM eladasok e1
JOIN belepok b1 ON e1.belepo_id = b1.id
WHERE b1.megnevezes LIKE '%TRX%' AND b1.megnevezes LIKE '%bérlet%'
  AND e1.tag_id IN (
    SELECT e2.tag_id
    FROM eladasok e2
    JOIN belepok b2 ON e2.belepo_id = b2.id
    WHERE b2.megnevezes LIKE '%jóga%' AND b2.megnevezes LIKE '%bérlet%'
  );

-- 27. feladat
SELECT vnev, knev
FROM tagok
WHERE knev = (
  SELECT knev
  FROM tagok
  GROUP BY knev
  ORDER BY COUNT(*) DESC
  LIMIT 1
)
ORDER BY vnev;

-- 28. feladat
SELECT vnev, knev, telefon
FROM tagok
WHERE nem = 'nő'
  AND telepules = 'Pécs'
  AND id NOT IN (
    SELECT e.tag_id
    FROM eladasok e
    JOIN belepok b ON e.belepo_id = b.id
    WHERE b.megnevezes LIKE '%Spinning%'
  )
ORDER BY vnev ASC, knev ASC;

-- 29. feladat
SELECT COUNT(DISTINCT tag_id) AS db
FROM eladasok
WHERE belepo_id IN (
  SELECT e.belepo_id
  FROM eladasok e
  JOIN tagok t ON e.tag_id = t.id
  WHERE t.nem = 'nő'
    AND t.telepules = 'Szeged'
    AND t.email LIKE '%cuvox.de'
);