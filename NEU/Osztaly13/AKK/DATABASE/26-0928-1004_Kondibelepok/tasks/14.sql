-- 14. feladat
SELECT irsz, COUNT(*) AS db
FROM tagok
GROUP BY irsz
HAVING COUNT(*) > 8
ORDER BY irsz DESC;
