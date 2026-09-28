-- 12. feladat
SELECT u.nev, COUNT(*) AS db
FROM urhajos u
JOIN repules r ON r.urhajos_id = u.id
GROUP BY u.id, u.nev
HAVING COUNT(*) >= 6;
