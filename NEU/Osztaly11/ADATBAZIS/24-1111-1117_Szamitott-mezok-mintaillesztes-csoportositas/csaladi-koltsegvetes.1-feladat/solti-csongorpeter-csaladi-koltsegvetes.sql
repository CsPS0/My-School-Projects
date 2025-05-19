-- 3. feladat
SELECT * FROM `kiadasok`;
-- 4. feladat
SELECT DISTINCT `kategoria` FROM `kiadasok`;
-- 5. feladat
SELECT SUM(`mennyiseg` * `egysegar`) AS 'osszkoltseg' FROM `kiadasok`;
-- 6. feladat
SELECT COUNT(`id`) AS 'vett_mennyiseg' FROM `kiadasok`;
-- 7. feladat
SELECT SUM(`mennyiseg`) AS 'termekek_szama' FROM `kiadasok`;
-- 8. feladat
SELECT COUNT(DISTINCT `megnevezes`) AS 'kulonbozo_db' FROM `kiadasok`;
-- 9. feladat
SELECT AVG(`egysegar`) AS 'atlagos_egysegar' FROM `kiadasok`;
-- 10. feladat
SELECT MIN(`egysegar`) AS 'legolcsobb' FROM `kiadasok`;
-- 11. feladat
SELECT MAX(`egysegar`) AS 'legdragabb' FROM `kiadasok`;
-- 12. feladat
SELECT DISTINCT `megnevezes` FROM `kiadasok` WHERE `nev` = 'Bence';
-- 13. feladat
SELECT COUNT(*) AS 'bence_db' FROM `kiadasok` WHERE `nev` = 'Bence';
-- 14. feladat
SELECT `megnevezes`, (`mennyiseg` * `egysegar`) AS 'fizetett' FROM `kiadasok` WHERE `nev` = 'Emese' ORDER BY 'fizetett';
-- 15. feladat
SELECT SUM(`mennyiseg` * `egysegar`) AS 'koltott' FROM `kiadasok` WHERE `nev` = 'Emese';
-- 16. feladat
SELECT `megnevezes`, `kategoria`, (`mennyiseg` * `egysegar`) AS 'koltes' FROM `kiadasok` WHERE `nev` IN ('Bence', 'Emese') ORDER BY 'koltes' DESC;
-- 17. feladat
SELECT `nev`, SUM(`mennyiseg` * `egysegar`) AS 'osszeg' FROM `kiadasok` GROUP BY `nev` ORDER BY `osszeg` DESC;
-- 18. feladat
SELECT `nap`, SUM(`mennyiseg` * `egysegar`) AS 'osszeg' FROM `kiadasok` GROUP BY `nap` ORDER BY `osszeg` DESC LIMIT 1;