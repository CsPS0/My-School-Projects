-- 13. feladat
SELECT ROUND(AVG(DATEDIFF(veg, kezdet)), 2) AS `Gemini kuldetesek atlagos hosszusaga`
FROM kuldetes
WHERE megnevezes LIKE 'Gemini%';
