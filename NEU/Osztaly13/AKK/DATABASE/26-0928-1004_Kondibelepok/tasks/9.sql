-- 9. feladat
SELECT COUNT(*) AS visaberlet
FROM eladasok e
JOIN tagok t ON e.tag_id = t.id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.kartya_tipusa = 'Visa'
  AND b.megnevezes LIKE '%bérlet%';
