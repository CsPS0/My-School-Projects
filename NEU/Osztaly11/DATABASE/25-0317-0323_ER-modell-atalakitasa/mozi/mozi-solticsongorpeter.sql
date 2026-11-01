-- !Adatbázis
DROP DATABASE IF EXISTS `mozi`;

CREATE DATABASE `mozi` CHARACTER SET utf8 COLLATE utf8_hungarian_ci;

USE `mozi`;

-- ?Táblák:
CREATE TABLE `film` (
    `id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `cim` VARCHAR(20) NOT NULL,
    `hossz` FLOAT NOT NULL,
    `hang` VARCHAR(20) NOT NULL,
    `felirat` VARCHAR(20) NOT NULL,
    `ev` DATE NOT NULL,
    `korhatar` INT NOT NULL
),

CREATE TABLE `terem` (
    -- `id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `nev` VARCHAR(20) NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `ferohely` INT NOT NULL
);


-- *Kapcsolatok:
CREATE TABLE `vetites` (
    `vetiteskezdete` DATE NOT NULL,
    `film_id` INT,
    `terem_id` INT,
    FOREIGN KEY (`film_id`) REFERENCES `film`(`id`),
    FOREIGN KEY (`terem_id`) REFERENCES `terem`(`id`)
);