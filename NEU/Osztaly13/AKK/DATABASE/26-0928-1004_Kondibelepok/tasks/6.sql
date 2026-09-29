-- 6. feladat
SELECT COUNT(*) AS nyugdijas_db
FROM tagok
WHERE TIMESTAMPDIFF(YEAR, szuletett, '2025-03-31') >= 65;
