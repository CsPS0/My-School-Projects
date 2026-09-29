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
