-- 3. feladat
SELECT `fnev` AS `futo`, `szulev` FROM `futok` WHERE `ffi` = 0 HAVING `fnev` IS NOT NULL ORDER BY `futo`;
-- 4. feladat
SELECT `fnev` AS `futo`, (2016 - `szulev`) AS `kor` FROM `futok` HAVING `kor` >= 42 ORDER BY `kor` DESC, `szulho` DESC;
-- 5. feladat
SELECT `fnev` AS `futo`, `ido` FROM `eredmenyek` INNER JOIN `futok` ON `eredmenyek`.`futo` = `futok`.`fid` WHERE `ffi` = 1 ORDER BY `ido` ASC LIMIT 10;
-- 6. feladat
SELECT `csapat`, SUM(`ido`) AS `csapatido` FROM `eredmenyek` INNER JOIN `futok` ON `eredmenyek`.`futo` = `futok`.`fid` GROUP BY `csapat` HAVING SUM(`ido`) > 0 ORDER BY `csapatido`;
-- 7. feladat
SELECT `fnev` AS `futo`, ROUND(SUM(`ido`) / 3600, 3) AS `ideje` FROM `eredmenyek` INNER JOIN `futok` ON `eredmenyek`.`futo` = `futok`.`fid` WHERE `fnev` LIKE 'Kocsis%' GROUP BY `fnev` HAVING `ideje` > 0 ORDER BY `ideje` ASC;
-- 8. feladat
SELECT `fnev` AS `futo`, ROUND(MIN(`ido`) / 60) AS `perc` FROM `eredmenyek` INNER JOIN `futok` ON `eredmenyek`.`futo` = `futok`.`fid` WHERE `kor` = 3 GROUP BY `futo` ORDER BY `ido` ASC LIMIT 1;
-- 9. feladat
SELECT `fnev` AS `futo`, `kor`, ROUND(`ido` / 60) AS `perc` FROM `eredmenyek` INNER JOIN `futok` ON `eredmenyek`.`futo` = `futok`.`fid` WHERE `ffi` = 0 AND `szulev` BETWEEN 1900 AND 1999 AND `szulho` = 6 AND LEFT(`fnev`, 1) IN ('F', 'P', 'Sz') HAVING `perc` > 0 ORDER BY `fnev` DESC, `kor` ASC;