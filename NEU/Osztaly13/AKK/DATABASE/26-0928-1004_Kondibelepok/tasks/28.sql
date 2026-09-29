-- 28. feladat
SELECT vnev, knev, telefon
FROM tagok
WHERE nem = 'nő'
  AND telepules = 'Pécs'
  AND id NOT IN (
    SELECT e.tag_id
    FROM eladasok e
    JOIN belepok b ON e.belepo_id = b.id
    WHERE b.megnevezes LIKE '%Spinning%'
  )
ORDER BY vnev ASC, knev ASC;
