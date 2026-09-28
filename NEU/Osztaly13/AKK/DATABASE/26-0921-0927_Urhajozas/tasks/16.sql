-- 16. feladat
SELECT nev, orszag, szulev
FROM urhajos
WHERE szulev = (SELECT szulev FROM urhajos WHERE nev = 'Barbara Morgan');
