-- 12. feladat
SELECT vnev, knev, telefon
FROM tagok
WHERE nem = 'nő'
  AND TIMESTAMPDIFF(YEAR, szuletett, '2025-03-31') > 30
  AND (telefon LIKE '(20)%' OR telefon LIKE '(30)%' OR telefon LIKE '(70)%')
ORDER BY vnev, knev;
