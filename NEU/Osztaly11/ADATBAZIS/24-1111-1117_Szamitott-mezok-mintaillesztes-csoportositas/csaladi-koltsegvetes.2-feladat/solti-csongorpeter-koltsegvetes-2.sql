-- 3. feladat
SELECT `kategoria`, SUM(`mennyiseg`) AS 'db' FROM `kiadasok` GROUP BY `kategoria` ORDER BY `db`;
-- 4. feladat
SELECT `nev`, SUM(`mennyiseg` * `egysegar`) AS 'koltes' FROM `kiadasok` GROUP BY `nev` ORDER BY `koltes` DESC;
-- 5. feladat
SELECT `nev`, SUM(`mennyiseg`) AS 'db' FROM `kiadasok` GROUP BY `nev` ORDER BY `nev` DESC;
-- 6. feladat
SELECT `nev`, `kategoria`, SUM(`mennyiseg`) AS 'db' FROM `kiadasok` GROUP BY `nev`, `kategoria` ORDER BY `nev` DESC, `kategoria`;
-- 7. feladat
SELECT `kategoria`, MIN(`egysegar`) AS 'minimum_ar' FROM `kiadasok` GROUP BY `kategoria` ORDER BY `minimum_ar`;
-- 8. feladat
SELECT `kategoria`, ROUND(SUM(`mennyiseg` * `egysegar`) / COUNT(DISTINCT `nev`), 1) AS 'koltes' FROM `kiadasok` WHERE `nev` IN ('Mariann', 'Lajos') GROUP BY `kategoria` ORDER BY `kategoria`;
-- 9. feladat
SELECT `kategoria`, MAX(`mennyiseg` * `egysegar`) AS 'koltes' FROM `kiadasok` WHERE `nev` IN ('Bence', 'Emese') GROUP BY `kategoria` ORDER BY `koltes` DESC LIMIT 1;
-- 10. feladat
SELECT `nev`, MIN(`mennyiseg` * `egysegar`) AS 'legolcsobb_koltes', MAX(`mennyiseg` * `egysegar`) AS 'legdragabb_koltes' FROM `kiadasok` GROUP BY `nev` ORDER BY `nev` DESC;
-- 11. feladat
SELECT `nap`, `kategoria`, SUM(`mennyiseg`) AS 'mennyiseg' FROM `kiadasok` GROUP BY `nap`, `kategoria` ORDER BY `mennyiseg` DESC;
-- 12. feladat
SELECT `nev`, `kategoria`, SUM(`mennyiseg` * `egysegar`) AS 'koltes' FROM `kiadasok` GROUP BY `nev`, `kategoria`;
-- 13. feladat
SELECT `kategoria`, SUM(`mennyiseg` * `egysegar`) AS 'koltes' FROM `kiadasok` WHERE `nev` = 'Bence' GROUP BY `kategoria` ORDER BY `koltes`;
-- 14. feladat
SELECT `nev`, CONCAT(SUM(`mennyiseg` * `egysegar`), ' Ft') AS 'koltes' FROM `kiadasok` WHERE `kategoria` = 'élelmiszer' GROUP BY `nev` ORDER BY `nev`;
-- 15. feladat
SELECT `nev` FROM `kiadasok` WHERE `kategoria` = 'közlekedés' GROUP BY `nev` ORDER BY SUM(`mennyiseg` * `egysegar`) ASC LIMIT 1;