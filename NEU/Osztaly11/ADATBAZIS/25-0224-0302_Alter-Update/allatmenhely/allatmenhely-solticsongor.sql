-- 2. feladat
DROP DATABASE IF EXISTS `allatmenhely`;
CREATE DATABASE `allatmenhely`
CHARACTER SET `utf8`
COLLATE `utf8_hungarian_ci`;
-- 3. feladat
USE `allatmenhely`;
CREATE TABLE `menhelyek` (
    `id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `nev` VARCHAR(50) NOT NULL,
    `cim` VARCHAR(80) NOT NULL,
    `web` VARCHAR(255) NOT NULL,
    `adosszam` CHAR(13) NOT NULL,
    `bankszamlaszam` CHAR(26) NOT NULL
);
-- 4. feladat
CREATE TABLE `allatok` (
    `id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `nev` VARCHAR(40) NOT NULL,
    `kor` INT,
    `tipus` VARCHAR(20),
    `fajta` VARCHAR(20),
    `ivartalanitott` BOOLEAN,
    `nem` VARCHAR(10) NOT NULL,
    `behozva` DATE DEFAULT CURRENT_DATE,
    `menhely_id` INT,
    FOREIGN KEY (`menhely_id`) REFERENCES `menhelyek`(`id`)
);
-- 5. feladat
INSERT INTO `menhelyek` (`nev`, `cim`, `web`, `adosszam`, `bankszamlaszam`) VALUES 
("Rex Kutyaotthon", "1048 Budapest, Óceánárok u. 33.", "http://www.rex.hu", "18015676-1-41", "10402283-22802398-70140000");
-- 6. feladat
SELECT * FROM `menhelyek` WHERE `nev` = 'Rex Kutyaotthon';
-- 7. feladat
INSERT INTO `allatok` (`nev`, `kor`, `tipus`, `fajta`, `ivartalanitott`, `nem`, `behozva`, `menhely_id`) VALUES 
("Csoki", 4, "kutya", "keverék", TRUE, "kan", "2016-04-01", 1),
("Picasso", 7, "kutya", "keverék", FALSE, "kan", "2018-01-12", 1);
-- 8. feladat
INSERT INTO `menhelyek` (`nev`, `cim`, `web`, `adosszam`, `bankszamlaszam`) VALUES 
("Noé állatotthon", "Budapest XVII.ker. Csordakút utca", "http://www.noeallatotthon.hu/", "18169696-1-42", "11710002-20083777");
-- 9. feladat
INSERT INTO `allatok` (`nev`, `kor`, `tipus`, `fajta`, `ivartalanitott`, `nem`, `behozva`, `menhely_id`) VALUES 
("Bíbor", 2, "cica", "perzsa", TRUE, "nőstény", "2017-05-17", 2),
("Bubbly", 0.5, "cica", "keverék", FALSE, "nőstény", "2018-01-02", 2),
("Csoki", 2, "kutya", "keverék", FALSE, "nőstény", "2020-01-12", 2),
("Nixxxon", 2.5, "kutya", "golden retriever", TRUE, "kan", CURRENT_DATE, 2);
-- 10. feladat
DELETE FROM `allatok` WHERE `nev` = 'Bubbly';
-- 11. feladat: Nixxxon neve javítása
UPDATE `allatok` SET `nev` = 'Nixon' WHERE `nev` = 'Nixxxon';
-- 12. feladat
UPDATE `allatok` SET `nem` = 'nőstény' WHERE `nev` = 'Picasso';
-- 13. feladat
UPDATE `allatok` SET `kor` = 3 WHERE `nev` = 'Csoki';
-- 14. feladat
SELECT AVG(`kor`) FROM `allatok` WHERE `tipus` = 'kutya';
-- 15. feladat
SELECT `menhely_id`, COUNT(*) FROM `allatok` GROUP BY `menhely_id`;
-- 16. feladat
SELECT `menhely_id`, `tipus`, COUNT(*) FROM `allatok` GROUP BY `menhely_id`, `tipus`;