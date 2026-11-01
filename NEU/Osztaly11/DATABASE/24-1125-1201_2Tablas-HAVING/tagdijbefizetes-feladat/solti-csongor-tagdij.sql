-- 3. feladat
SELECT `nev` FROM `ugyfel` ORDER BY `nev` ASC;
-- 4. feladat
SELECT `azon`, `nev`, `szulev`, `irszam`, `orsz` FROM `ugyfel` WHERE `orsz` = 'H' ORDER BY 2016 - `szulev` ASC;
-- 5. feladat
SELECT `nev`, `szulev` FROM `ugyfel` WHERE `irszam` LIKE '2%' ORDER BY `nev` ASC;
-- 6. feladat
SELECT SUM(`osszeg`) AS 'Összes befizetés' FROM `befiz`;
-- 7. feladat
SELECT AVG(`osszeg`) AS 'Átlagos befizetés' FROM `befiz`;
-- 8. feladat
SELECT MIN(`osszeg`) AS 'Minimum', MAX(`osszeg`) AS 'Maximum' FROM `befiz`;
-- 9. feladat
SELECT COUNT(*) AS 'Befizetések száma' FROM `befiz`;
-- 10. feladat
SELECT `osszeg`, `datum` FROM `befiz` INNER JOIN `ugyfel` ON `befiz`.`azon` = `ugyfel`.`azon` WHERE `ugyfel`.`nev` = 'Török Bálint';
-- 11. feladat
SELECT SUM(`osszeg`) AS 'Nagy Károly befizetése' FROM `befiz` INNER JOIN `ugyfel` ON `befiz`.`azon` = `ugyfel`.`azon` WHERE `ugyfel`.`nev` = 'Nagy Károly';
-- 12. feladat
SELECT SUM(`osszeg`) AS 'Külföldi összeg' FROM `befiz` INNER JOIN `ugyfel` ON `befiz`.`azon` = `ugyfel`.`azon` WHERE `ugyfel`.`orsz` != 'H';
-- 13. feladat
SELECT `nev`, SUM(`osszeg`) AS 'Befizetett' FROM `befiz` INNER JOIN `ugyfel` ON `befiz`.`azon` = `ugyfel`.`azon` GROUP BY `nev`;
-- 14. feladat
SELECT `nev` FROM `befiz` INNER JOIN `ugyfel` ON `befiz`.`azon` = `ugyfel`.`azon` GROUP BY `nev` HAVING SUM(`osszeg`) >= (SELECT SUM(`osszeg`) FROM `befiz` GROUP BY `azon` ORDER BY SUM(`osszeg`) DESC LIMIT 1 OFFSET 2) ORDER BY SUM(`osszeg`) DESC;
-- 15. feladat
SELECT `nev` FROM `ugyfel` HAVING `szulev` = MIN(`szulev`);
-- 16. feladat
SELECT `nev`, `osszeg` FROM `befiz` INNER JOIN `ugyfel` ON `befiz`.`azon` = `ugyfel`.`azon` HAVING `osszeg` = MIN(`osszeg`);
-- 17. feladat
SELECT MONTH(`datum`) AS 'Hónap', SUM(`osszeg`) AS 'Havi összeg' FROM `befiz` GROUP BY MONTH(`datum`) HAVING SUM(`osszeg`) > 0;