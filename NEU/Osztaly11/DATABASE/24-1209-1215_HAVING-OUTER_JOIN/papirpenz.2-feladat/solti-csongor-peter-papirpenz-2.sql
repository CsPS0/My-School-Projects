-- 3. feladat
SELECT COUNT(*) FROM `papirpenz` WHERE `ertek` >= 100;
-- 4. feladat
SELECT `penznem`, COUNT(*) AS 'db' FROM `papirpenz` GROUP BY `penznem` ORDER BY `db`;
-- 5. feladat
SELECT `penznem`, COUNT(DISTINCT `ertek`) AS 'db_egyedi' FROM `papirpenz` GROUP BY `penznem` ORDER BY `db_egyedi` DESC;
-- 6. feladat
SELECT `penznem`, MAX(`ertek`) AS 'legertekesebb' FROM `papirpenz` GROUP BY `penznem` ORDER BY `penznem`;
-- 7. feladat
SELECT `penznem`, `ertek`, COUNT(*) AS 'db' FROM `papirpenz` GROUP BY `penznem`, `ertek` HAVING COUNT(*) >= 4 ORDER BY `penznem`, `ertek`;
-- 8. feladat
SELECT `penznem`, SUM(`ertek`) AS 'ossz' FROM `papirpenz` GROUP BY `penznem` HAVING SUM(`ertek`) >= 3000 ORDER BY `ossz`;