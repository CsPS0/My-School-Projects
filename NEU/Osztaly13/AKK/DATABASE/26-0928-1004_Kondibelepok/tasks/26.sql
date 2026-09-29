-- 26. feladat
SELECT COUNT(DISTINCT e1.tag_id) AS db
FROM eladasok e1
JOIN belepok b1 ON e1.belepo_id = b1.id
WHERE b1.megnevezes LIKE '%TRX%' AND b1.megnevezes LIKE '%bérlet%'
  AND e1.tag_id IN (
    SELECT e2.tag_id
    FROM eladasok e2
    JOIN belepok b2 ON e2.belepo_id = b2.id
    WHERE b2.megnevezes LIKE '%jóga%' AND b2.megnevezes LIKE '%bérlet%'
  );
