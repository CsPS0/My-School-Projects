-- 3. feladat
SELECT * FROM `napfogyatkozas` ORDER BY `datum` ASC;
-- 4. feladat
SELECT DISTINCT `tipus` FROM `napfogyatkozas`;
-- 5. feladat
SELECT * FROM `napfogyatkozas` WHERE `szelesseg` IS NULL;
-- 6. feladat
SELECT `datum`, `ido`, `tipus` FROM `napfogyatkozas` WHERE `megfigyelheto` LIKE '%Észak-Amerika%';
-- 7. feladat
SELECT `datum`, `ido`, `megfigyelheto` FROM `napfogyatkozas` WHERE `tipus` IN ('Teljes', 'Gyűrűs');
-- 8. feladat
SELECT `datum`, `ido` FROM `napfogyatkozas` WHERE `megfigyelheto` LIKE '%Afrika%' AND `tipus` IN ('Teljes', 'Gyűrűs');
-- 9. feladat
SELECT `datum`, `ido`, `megfigyelheto` FROM `napfogyatkozas` WHERE `megfigyelheto` LIKE '%Észak%' OR `megfigyelheto` LIKE '%Dél%' OR `megfigyelheto` LIKE '%Kelet%' OR `megfigyelheto` LIKE '%Nyugat%';
-- 10. feladat
SELECT `datum`, `ido` FROM `napfogyatkozas` WHERE `megfigyelheto` LIKE '%Magyarország%' AND `tipus` = 'Teljes';
-- 11. feladat
SELECT DISTINCT `tipus` FROM `napfogyatkozas` ORDER BY `tipus` ASC;
-- 12. feladat
SELECT `datum`, `ido`, `idotartam` FROM `napfogyatkozas` WHERE `tipus` = 'Teljes'ORDER BY `idotartam` DESC;
-- 13. feladat
SELECT * FROM `napfogyatkozas` WHERE `megfigyelheto` LIKE '%,%';
-- 14. feladat
SELECT * FROM `napfogyatkozas` WHERE `tipus` = 'Részleges' AND (`megfigyelheto` LIKE '%Alaszka%' OR `megfigyelheto` LIKE '%Hawaii%');