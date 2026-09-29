-- 13. feladat
SELECT SUM(b.ar) AS dayka
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.megye = 'Csongrád'
  AND t.cim LIKE '%Dayka Gábor%';
