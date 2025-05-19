-- 3. feladat
SELECT * FROM `varosok` ORDER BY `nepesseg` DESC;
-- 4. feladat
SELECT DISTINCT `tipus` FROM `varosok` ORDER BY `tipus`;
-- 5. feladat
SELECT COUNT(*) AS `varosok_szama` FROM `varosok`;
-- 6. feladat
SELECT COUNT(DISTINCT `tipus`) AS `tipus_db` FROM `varosok`;
-- 7. feladat
SELECT COUNT(*) AS `db` FROM `varosok` WHERE `megye` = 'Csongrád-Csanád';
-- 8. feladat
SELECT COUNT(*) AS `db` FROM `varosok` WHERE LENGTH(`nev`) = 4;
-- 9. feladat
SELECT COUNT(*) AS `db` FROM `varosok` WHERE `megye` IN ('Békés', 'Fejér');
-- 10. feladat
SELECT `megye`, COUNT(*) AS `db` FROM `varosok` GROUP BY `megye` ORDER BY `db` DESC;
-- 11. feladat
SELECT MAX(`ev`) AS `ev` FROM `varosok` WHERE `varos_cim_elnyerese` IS NOT NULL;
-- 12. feladat
SELECT `megye`, MAX(`ev`) AS `ev` FROM `varosok` WHERE `varos_cim_elnyerese` IS NOT NULL GROUP BY `megye`;
-- 13. feladat
SELECT MIN(`nepesseg`) AS `minimum_lakossag` FROM `varosok`;
-- 14. feladat
SELECT MAX(`nepesseg`) AS `maximum_lakossag` FROM `varosok`;
-- 15. feladat
SELECT MAX(`terulet`) AS `maximum_terulet` FROM `varosok`;
-- 16. feladat
SELECT `nev`, `terulet` FROM `varosok` ORDER BY `terulet` DESC LIMIT 3;
-- 17. feladat
SELECT `nev`, `terulet` FROM `varosok` WHERE `megye` = 'Somogy' ORDER BY `terulet` DESC LIMIT 3;
-- 18. feladat
SELECT `nev`, ROUND(`nepesseg` / `terulet`, 4) AS `nepsuruseg` FROM `varosok` ORDER BY `nepsuruseg` DESC LIMIT 1;
-- 19. feladat
SELECT SUM(`nepesseg`) AS `lakossag` FROM `varosok` WHERE `tipus` = 'járásszékhely város';
-- 20. feladat
SELECT ROUND(SUM(`terulet`), 2) AS `osszterulet` FROM `varosok` WHERE `ev` BETWEEN 2000 AND 2012;
-- 21. feladat
SELECT `megye`, SUM(`nepesseg`) AS `lakossag` FROM `varosok` GROUP BY `megye` ORDER BY `megye`;
-- 22. feladat
SELECT `megye`, ROUND(AVG(`nepesseg`), 0) AS `lakossag` FROM `varosok` GROUP BY `megye` ORDER BY `lakossag` DESC, `megye`;
-- 23. feladat
SELECT `jaras`, ROUND(AVG(`terulet`), 2) AS `atlag_terulet` FROM `varosok` GROUP BY `jaras` ORDER BY `atlag_terulet` ASC;
-- 24. feladat
SELECT `nev`, CONCAT(`nepesseg`, ' fő') AS `lakossag` FROM `varosok` WHERE `tipus` = 'megyei jogú város' ORDER BY `nepesseg` DESC LIMIT 1;