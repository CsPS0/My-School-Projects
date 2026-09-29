-- 8. feladat
SELECT SUM(b.ar) AS noi_bev_30
FROM tagok t
JOIN eladasok e ON t.id = e.tag_id
JOIN belepok b ON e.belepo_id = b.id
WHERE t.nem = 'nő'
  AND TIMESTAMPDIFF(YEAR, t.szuletett, '2025-03-31') < 30;
