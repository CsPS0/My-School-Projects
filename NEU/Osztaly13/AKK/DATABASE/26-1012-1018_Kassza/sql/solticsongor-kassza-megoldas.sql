-- 1. feladat
-- kassza.sql betöltése
-- SOURCE kassza.sql;

-- 2. feladat
SELECT '2. feladat' AS '';
USE `kassza`;

-- 3. feladat
SELECT '3. feladat' AS '';
ALTER TABLE `blokk`
ADD CONSTRAINT `fk_blokk_elado` FOREIGN KEY (`elado_id`) REFERENCES `elado`(`id`);

ALTER TABLE `cikk`
ADD CONSTRAINT `fk_cikk_blokk` FOREIGN KEY (`blokk_id`) REFERENCES `blokk`(`id`);

-- 4. feladat
SELECT '4. feladat' AS '';
CREATE OR REPLACE VIEW `havi_osszesites` AS
SELECT 
    YEAR(`blokk`.`vasarlas_ideje`) AS `ev`,
    MONTH(`blokk`.`vasarlas_ideje`) AS `honap`,
    ROUND(SUM(`cikk`.`mennyiseg` * `cikk`.`egysegar` * (1 + `cikk`.`afa`)), 2) AS `osszertek`
FROM `blokk`
JOIN `cikk` ON `blokk`.`id` = `cikk`.`blokk_id`
GROUP BY YEAR(`blokk`.`vasarlas_ideje`), MONTH(`blokk`.`vasarlas_ideje`);

CREATE OR REPLACE VIEW `havi_osszesítés` AS
SELECT * FROM `havi_osszesites`;

SELECT * FROM `havi_osszesites`;

-- 5. feladat
SELECT '5. feladat' AS '';
DROP TABLE IF EXISTS `modositas`;
CREATE TABLE `modositas` (
    `blokk_id` int NOT NULL,
    `regi_osszeg` double NOT NULL,
    `uj_osszeg` double NOT NULL,
    `mod_ido` datetime DEFAULT CURRENT_TIMESTAMP,
    `mod_user` varchar(100) DEFAULT (CURRENT_USER())
) ENGINE=InnoDB;

-- 6. feladat
SELECT '6. feladat' AS '';
DROP USER IF EXISTS 'nav'@'192.168.1.11';
CREATE USER 'nav'@'192.168.1.11' IDENTIFIED BY 'navUser2026';
GRANT SELECT ON `havi_osszesites` TO 'nav'@'192.168.1.11';
GRANT SELECT ON `havi_osszesítés` TO 'nav'@'192.168.1.11';

-- 7. feladat
SELECT '7. feladat' AS '';
ALTER TABLE `blokk`
ADD COLUMN `storno` boolean DEFAULT false;

-- 8. feladat
SELECT '8. feladat' AS '';
DROP USER IF EXISTS 'kasszaprogram'@'192.168.0.%';
CREATE USER 'kasszaprogram'@'192.168.0.%' IDENTIFIED BY 'K4ssz4J3lsz0!';
GRANT INSERT, UPDATE ON `blokk` TO 'kasszaprogram'@'192.168.0.%';
GRANT INSERT ON `cikk` TO 'kasszaprogram'@'192.168.0.%';

-- 9. feladat
SELECT '9. feladat' AS '';
DROP USER IF EXISTS 'kasszaadmin'@'192.168.%', 'kasszaadmin'@'192.168.%.%';
CREATE USER 'kasszaadmin'@'192.168.%' IDENTIFIED BY 'adm1nPass++';
CREATE USER 'kasszaadmin'@'192.168.%.%' IDENTIFIED BY 'adm1nPass++';
GRANT INSERT, UPDATE, DELETE ON `blokk` TO 'kasszaadmin'@'192.168.%', 'kasszaadmin'@'192.168.%.%';
GRANT INSERT, UPDATE, DELETE ON `cikk` TO 'kasszaadmin'@'192.168.%', 'kasszaadmin'@'192.168.%.%';

FLUSH PRIVILEGES;
