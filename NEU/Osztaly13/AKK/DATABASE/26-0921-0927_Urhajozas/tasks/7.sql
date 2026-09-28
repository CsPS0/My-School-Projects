-- 7. feladat
SELECT nev, YEAR(CURDATE()) - szulev AS kor
FROM urhajos
ORDER BY kor DESC;
