-- 3. feladat
SELECT COUNT(DISTINCT `tipus`) AS 'toyotadb' FROM `auto` WHERE `gyarto` = 'Toyota'; 
-- 4. feladat
SELECT `szin` FROM `auto` GROUP BY `szin` ORDER BY COUNT(*) DESC LIMIT 1; 
-- 5. feladat
SELECT `nem`, COUNT(*) AS 'db' FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE `auto`.`automata` = 1 GROUP BY `nem`; 
-- 6. feladat
SELECT `gyarto`, `tipus`, COUNT(*) AS 'db' FROM `auto` GROUP BY `gyarto`, `tipus` ORDER BY `gyarto` ASC, `tipus` ASC; 
-- 7. feladat
SELECT `vnev`, `knev`, `rendszam` FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` ORDER BY `vnev`, `knev` ASC; 
-- 8. feladat
SELECT `vnev`, `knev`, `rendszam` FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE `auto`.`kor` <= 6 ORDER BY `vnev`, `knev`; 
-- 9. feladat
SELECT DISTINCT `tipus` FROM `auto` WHERE `gyarto` = 'Opel' ORDER BY `tipus` ASC; 
-- 10. feladat
SELECT `vnev`, `knev` FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE `munkatars`.`varos` = 'Budapest' AND `munkatars`.`nem` = 'férfi' AND `auto`.`gyarto` = 'Toyota'; 
-- 11. feladat
SELECT `vnev`, `knev`, `rendszam` FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE (`auto`.`uzemanyag` = 'dízel' OR `auto`.`kivitel` = 'ferdehátú') AND `munkatars`.`varos` = 'Budapest' ORDER BY `rendszam` DESC; 
-- 12. feladat
SELECT `rendszam`, `gyarto`, `tipus`, `knev` FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE (`auto`.`gyarto` = 'Opel' OR `auto`.`gyarto` = 'Skoda') AND `auto`.`kor` BETWEEN 3 AND 5 ORDER BY `rendszam` DESC; 
-- 13. feladat
SELECT SUM(`auto`.`csomagter`) AS 'osszliter' FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE `munkatars`.`nem` = 'férfi'; 
-- 14. feladat
SELECT `gyarto`, COUNT(*) AS 'db' FROM `auto` INNER JOIN `munkatars` ON `auto`.`munkatars_id` = `munkatars`.`id` WHERE `munkatars`.`nem` = 'nő' GROUP BY `gyarto`; 
-- 15. feladat
SELECT `gyarto`, AVG(`csomagter`) AS 'csomagter_atlag', MAX(`csomagter`) AS 'legnagyobb_csomagter' FROM `auto` GROUP BY `gyarto`; 