-- 18. feladat
SELECT t.nem, b.megnevezes, COUNT(*) AS db
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE b.megnevezes LIKE '%korlátlan%'
GROUP BY t.nem, b.megnevezes
ORDER BY b.megnevezes ASC, db DESC;
