-- 1.feladat
DROP DATABASE IF EXISTS `focikatalogus`;
-- 2.feladat
CREATE DATABASE `focikatalogus`
CHARACTER SET `utf8`
COLLATE `utf8_hungarian_ci`;
-- 3.feladat
USE `focikatalogus`;
-- 4.feladat
CREATE TABLE `focista` (
    `nev` varchar(30) NOT NULL,
    `szarmazas` varchar(30) NOT NULL,
    `szuletett` DATE NOT NULL,
    `csapat` varchar(30) NOT NULL,
    `golok_szama` int NOT NULL
);
-- 5.feladat
DELETE FROM `focista`;
-- 6.feladat
INSERT INTO `focista` (`nev`, `szarmazas`, `szuletett`, `csapat`, `golok_szama`)
VALUES ("Lionel Messi", "argentin", "1987-06-24", "Barcelona", 656);
-- 7.feladat
INSERT INTO `focista` (`nev`, `szarmazas`, `szuletett`, `csapat`, `golok_szama`)
VALUES ("Cristiano Ronaldo", "portugál", "1985-02-05", "Juventus", 619);
-- 8.feladat
INSERT INTO `focista` (`nev`, `szarmazas`, `szuletett`, `csapat`, `golok_szama`)
VALUES 
("Robert Lewandowski", "lengyel", "1988-08-21", "Bayern München", 436),
("Luis Suarez", "uruguay", "1987-01-24", "Barcelona", 420),
("Zlatan Ibrahimovic", "svéd", "1981-10-03", "L. A. Galaxy", 405);