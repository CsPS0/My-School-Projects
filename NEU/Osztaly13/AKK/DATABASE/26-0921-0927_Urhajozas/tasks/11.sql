-- 11. feladat
SELECT k.megnevezes, COUNT(*) AS fo
FROM kuldetes k
JOIN repules r ON r.kuldetes_id = k.id
GROUP BY k.id, k.megnevezes;
