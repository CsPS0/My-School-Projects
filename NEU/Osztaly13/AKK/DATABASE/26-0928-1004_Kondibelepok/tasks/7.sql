-- 7. feladat
SELECT ROUND(AVG(TIMESTAMPDIFF(YEAR, szuletett, '2025-03-31')), 2) AS ferfi_atlag
FROM tagok
WHERE nem = 'férfi';
