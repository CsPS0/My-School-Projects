-- 10. feladat
SELECT DISTINCT t.id, CONCAT(t.vnev, ' ', t.knev) AS nev, CONCAT(t.irsz, ' ', t.telepules, ', ', t.cim) AS teljes_cim
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.megye = 'Vas'
  AND b.megnevezes LIKE '%TRX%'
ORDER BY t.id;
