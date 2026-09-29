-- 16. feladat
SELECT t.telepules, SUM(b.ar) AS koltes
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
GROUP BY t.telepules
HAVING koltes <= 4000
ORDER BY koltes DESC, t.telepules ASC;
