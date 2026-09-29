-- 11. feladat
SELECT vnev, knev, cim
FROM tagok
WHERE nem = 'nő'
  AND telepules <> 'Budapest'
  AND cim LIKE '%krt.%'
ORDER BY vnev, knev;
