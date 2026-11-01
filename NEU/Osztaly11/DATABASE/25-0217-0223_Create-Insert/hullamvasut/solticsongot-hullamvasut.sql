-- 2. feladat
DROP DATABASE IF EXISTS `hullamvasut`;
-- 3. feladat
CREATE DATABASE `hullamvasut`
CHARACTER SET `utf8`
COLLATE `utf8_hungarian_ci`;
-- 4. feladat
USE `hullamvasut`;
-- 5. feladat
CREATE TABLE `tipus` (
    `id` int AUTO_INCREMENT PRIMARY KEY,
    `tipus` varchar(30) NOT NULL,
    `beszuntetett` BOOLEAN,
    `elso` int DEFAULT NULL,
    `darab` int DEFAULT NULL,
    `rogzites` varchar(20) DEFAULT NULL
);
-- 6. feladat
DELETE FROM `tipus`;
-- 7. feladat
INSERT INTO `tipus` (
    `tipus`, 
    `beszuntetett`, 
    `elso`, 
    `darab`, 
    `rogzites`
) 
VALUES 
('4th Dimension roller coaster', 'gyártás alatt', 2002, 8, 'váll fölött'),
('Bobsled roller coaster', 'gyártás alatt', 1929, 21, 'ölben'),
('Dive Coaster', 'gyártás alatt', 1998, 14, 'váll fölött'),
('Floorless Coaster', 'gyártás alatt', 1999, 14, 'váll fölött'),
('Flying roller coaster', 'gyártás alatt', 1997, 26, 'váll fölött'),
('Inverted roller coaster', 'gyártás alatt', 1992, 189, 'váll fölött'),
('Pipeline roller coaster', 'beszüntetve', 1984, 12, 'váll fölött'),
('Stand-up roller coaster', 'gyártás alatt', 1982, 21, NULL),
('Suspended roller coaster', 'gyártás alatt', 1902, 37, NULL);