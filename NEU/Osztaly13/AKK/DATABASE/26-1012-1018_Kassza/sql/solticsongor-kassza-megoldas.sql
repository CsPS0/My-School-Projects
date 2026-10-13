-- 1.feladat
-- source /sql/kassza.sql

-- 2.feladat
-- use kassza;

-- 3.feladat
-- FOREIGN KEY ...

-- 4.feladat
SELECT '4. feladat' AS '';
CREATE OR REPLACE VIEW havi_osszesites AS
SELECT CONCAT(SUM(egysegar * mennyiseg) + (egysegar * mennyiseg * afa), ' Ft') AS 'bevetel'
FROM blokk
JOIN cikk
ON blokk.id = cikk.blokk_id
GROUP BY asd

SELECT * FROM havi_osszesites;

-- 5.feladat
SELECT '5. feladat' AS '';
CREATE TABLE `modositas` (
    `blokk_id` int NOT NULL PRIMARY KEY,
    `regi_osszeg` double NOT NULL,
    `uj_osszeg` double NOT NULL,
    `mod_ido` datetime DEFAULT CURRENT_TIMESTAMP(),
    `mod_user` varchar(50) NOT NULL CURRENT_USER()
) ENGINE=InnoDB;

-- 6.feladat
SELECT '6. feladat' AS '';
DROP USER IF EXISTS 'nav'@'192.168.1.11';
CREATE USER 'nav'@'192.168.1.11' IDENTIFIED BY 'navUser2026';
GRANT SELECT ON `havi_osszesites` TO 'nav'@'192.168.1.11';


-- 7.feladat
SELECT '7. feladat' AS '';
ALTER TABLE `blokk`
ADD COLUMN `storno` boolean DEFAULT false;

-- 8.feladat
SELECT '8. feladat' AS '';
DROP USER IF EXISTS 'kasszaprogram'@'192.168.*.*';
CREATE USER 'kasszaprogram'@'192.168.*.*' IDENTIFIED BY 'K4ssz4J3lsz0!';
GRANT SELECT, INSERT ON `blokk` TO 'kasszaprogram'@'192.168.*.*';

-- 9.feladat
SELECT '9. feladat' AS '';
DROP USER IF EXISTS 'kasszaadmin'@'192.168.*.*';
CREATE USER 'kasszaadmin'@'192.168.*.*' IDENTIFIED BY 'adm1nPass++';
GRANT SELECT, INSERT ON 'blokk', 'cikk' TO 'kasszaadmin'@'192.168.*.*';
