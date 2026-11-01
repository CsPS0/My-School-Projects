DROP DATABASE IF EXISTS `solti-nobel`;
CREATE DATABASE `solti-nobel`;
CHARACTER SET `utf8`;
COLLATE `utf8_hungarian_ci`;

USE `solti-nobel`;

CREATE TABLE `dijak` (
    `ev` INT NOT NULL,
    `kategoria` CHAR(25) NOT NULL,
    `indoklas` TEXT NOT NULL,
    `dijazott_azon` INT NOT NULL,
    `nev` CHAR(50) NOT NULL,
    `szemely_nev` CHAR(50),
    `szemely_teljes_nev` CHAR(100),
    `nem` CHAR(6),
    `szuletesi_ev` INT NOT NULL,
    `szuletesi_datum` DATE,
    `szuletesi_hely` CHAR(100),
    `szervezet_nev` CHAR(100),
    `rovidites` CHAR(20),
    `alapitas_eve` INT NOT NULL,
    `jelleg` CHAR(10),
    `hovatartozas_1` CHAR(50),
    `hovatartozas_2` CHAR(50),
    `hovatartozas_3` CHAR(50),
    `hovatartozas_4` CHAR(50)
);

LOAD DATA INFILE 'nobel_adat.xlsx'
INTO TABLE dijak
FIELDS TERMINATED BY ',' 
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS 
(`ev`, `kategoria`, `indoklas`, `dijazott_azon`, `nev`, `szemely_nev`, `szemely_teljes_nev`, `nem`, `szuletesi_ev`, `szuletesi_datum`, `szuletesi_hely`, `szervezet_nev`, `rovidites`, `alapitas_eve`, `jelleg`, `hovatartozas_1`, `hovatartozas_2`, `hovatartozas_3`, `hovatartozas_4`);

