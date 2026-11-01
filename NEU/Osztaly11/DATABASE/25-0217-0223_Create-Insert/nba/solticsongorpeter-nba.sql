-- 2. feladat
DROP DATABASE IF EXISTS `solti_nba`;
-- 3. feladat
CREATE DATABASE `solti_nba`
CHARACTER SET `utf8`
COLLATE `utf8_hungarian_ci`;
-- 4. feladat
USE `solti_nba`;
-- 5. feladat
CREATE TABLE `jatekos` (
    `id` int AUTO_INCREMENT PRIMARY KEY,
    `nev` varchar(25) NOT NULL,
    `kor` int DEFAULT NULL,
    `magassag` float(5,2) DEFAULT NULL,
    `csapat` varchar(30) DEFAULT NULL
);
-- 6. feladat
DELETE FROM `jatekos`;
-- 7. feladat
INSERT INTO `jatekos` (
    `nev`, 
    `kor`, 
    `magassag`, 
    `csapat`
) 
VALUES 
(
    'Matt Fish', 27, 210.82, 'MIA'
);
-- 8. feladat
INSERT INTO `jatekos` (
    `nev`, 
    `kor`, 
    `magassag`, 
    `csapat`
) 
VALUES 
('Walter McCarty', 27, 208.28, 'BOS'),
('James Posey', 31, 203.2, 'BOS'),
('James Michael McAdoo', 22, 205.74, 'GSW');
-- 9. feladat
SELECT `csapat`, AVG(`magassag`) AS 'átlagmagasság' FROM `jatekos` GROUP BY `csapat`;
-- 10. feladat
SELECT `nev` FROM `jatekos` ORDER BY `magassag` DESC LIMIT 1;
-- 11. feladat
SELECT `kor`, `csapat` FROM `jatekos` ORDER BY `kor` LIMIT 1;