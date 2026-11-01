-- 3. feladat
SELECT * FROM `felhokarcolok` ORDER BY `magassag` DESC; 
-- 4. feladat
SELECT * FROM `felhokarcolok` WHERE `varos` = 'Chicago'; 
-- 5. feladat
SELECT `nev`, `emeletek` FROM `felhokarcolok` WHERE `felhasznalas` LIKE '%lakóépület%'; 
-- 6. feladat
SELECT `nev` FROM `felhokarcolok` WHERE `nev` LIKE '_1%'; 
-- 7. feladat
SELECT COUNT(`id`) AS 'db' FROM `felhokarcolok`; 
-- 8. feladat
SELECT COUNT(`id`) AS 'db' FROM `felhokarcolok` WHERE `orszagkod` = 'US'; 
-- 9. feladat
SELECT COUNT(`id`) AS 'db' FROM `felhokarcolok` WHERE `befejezes_eve` <= 2005 AND `befejezes_eve` >= 1997;
-- 10. feladat
SELECT ROUND(AVG(`emeletek`),0) AS 'atlagos_emelet' FROM `felhokarcolok` WHERE `nev` LIKE '%Tower%'; 
-- 11. feladat
SELECT `nev`, CONCAT(ROUND(`magassag`, 0), " m") AS 'magassag_meterben' FROM `felhokarcolok` ORDER BY `magassag` DESC LIMIT 3; 
-- 12. feladat
SELECT `varos`, COUNT(*) AS 'db' FROM `felhokarcolok` GROUP BY `varos` ORDER BY `db` DESC LIMIT 1;
-- 13. feladat
SELECT `emeletek` AS 'legtobb_emelet' FROM `felhokarcolok` ORDER BY `emeletek` DESC LIMIT 1; 
-- 14. feladat
SELECT `anyag`, COUNT(*) AS 'db' FROM `felhokarcolok` GROUP BY `anyag` ORDER BY `db` DESC LIMIT 1;
-- 15. feladat
SELECT ROUND(SUM(`magassag`),1) AS 'ossz_magassag' FROM `felhokarcolok` WHERE `varos` LIKE '%Hong_Kong%';
-- 16. feladat
SELECT COUNT(*) AS 'db' FROM `felhokarcolok` WHERE `befejezes_eve` >= YEAR(CURDATE()) - 10;
-- 17. feladat
SELECT `nev`, `magassag` FROM `felhokarcolok` WHERE `anyag` = 'beton' AND `magassag` > 400;
-- 18. feladat
SELECT `nev`, `magassag` FROM `felhokarcolok` ORDER BY `magassag` DESC LIMIT 1 OFFSET 1;
-- 19. feladat
SELECT `nev` FROM `felhokarcolok` WHERE `nev` NOT LIKE '%Tower%';
-- 20. feladat
SELECT `varos`, COUNT(*) AS 'db' FROM `felhokarcolok` GROUP BY `varos` HAVING `db` > 5;
-- 21. feladat
SELECT COUNT(DISTINCT `orszagkod`) AS 'kulonbozo_orszagok' FROM `felhokarcolok`;
-- 22. feladat
SELECT `nev`, `magassag`, `befejezes_eve` FROM `felhokarcolok` ORDER BY `befejezes_eve` ASC LIMIT 1;
-- 23. feladat
SELECT `nev`, `varos` FROM `felhokarcolok` WHERE `anyag` = 'acél' AND `felhasznalas` LIKE '%iroda%';