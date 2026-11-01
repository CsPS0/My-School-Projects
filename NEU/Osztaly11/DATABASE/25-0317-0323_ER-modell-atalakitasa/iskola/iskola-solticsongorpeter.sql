-- !Adatbázis
DROP DATABASE IF EXISTS `iskola`;

CREATE DATABASE `iskola` CHARACTER SET utf8 COLLATE utf8_hungarian_ci;

USE `iskola`;

-- ?Táblák:
CREATE TABLE `tanar` (
    `id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `Nev` VARCHAR(20) NOT NULL,
    `E-mail` VARCHAR(30) NOT NULL
);

CREATE TABLE `vegzettseg` (
    `Vegzettseg` VARCHAR(30) NOT NULL,
    `tanar_id` INT NOT NULL,
    FOREIGN KEY (`tanar_id`) REFERENCES `tanar`(`id`)
);

CREATE TABLE `tantargy` (
    `id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `nev` VARCHAR(30) NOT NULL,
    `evfolyam` INT NOT NULL,
    `tanterv` VARCHAR(60) NOT NULL
);

CREATE TABLE `diak` (
    `taj` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `osztaly` VARCHAR(4) NOT NULL,
    `szulido` DATE NOT NULL,
    `kor` INT NOT NULL,
    `nev` VARCHAR(30) NOT NULL
);

-- *Kapcsolatok:
CREATE TABLE `tanitja` (
    `tanar_id` INT,
    `tantargy_id` INT,
    FOREIGN KEY (`tanar_id`) REFERENCES `tanar`(`id`),
    FOREIGN KEY (`tantargy_id`) REFERENCES `tantargy`(`id`)

);

CREATE TABLE `tanulja` (
    `targy_id` INT,
    `diak_id` INT,
    `tanev` VARCHAR(10) NOT NULL,
    FOREIGN KEY (`targy_id`) REFERENCES `tantargy`(`id`),
    FOREIGN KEY (`diak_id`) REFERENCES `diak`(`taj`)
);