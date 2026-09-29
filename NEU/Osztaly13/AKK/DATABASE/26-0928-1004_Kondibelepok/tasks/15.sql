-- 15. feladat
SELECT t.megye, SUM(b.ar) AS koltes
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.megye IS NOT NULL
GROUP BY t.megye
ORDER BY t.megye ASC;
