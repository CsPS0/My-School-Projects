DROP DATABASE IF EXISTS `biztositasok`;

CREATE DATABASE `biztositasok`
    DEFAULT CHARACTER SET utf8mb4
    COLLATE utf8mb4_hungarian_ci;

USE `biztositasok`;

CREATE TABLE `Ugyfel` (
    `azonosito` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_nev` VARCHAR(100) NOT NULL,
    `anyja_neve` VARCHAR(100) NOT NULL,
    `szuletesi_ido` DATE NOT NULL,
    `email_cim` VARCHAR(100) NOT NULL,
    `telefonszam` VARCHAR(20) NOT NULL
);

CREATE TABLE `Szerzodes_Egyben` (
    `azonosito` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_azonosito` INT NOT NULL,
    `tipus` ENUM('lakas', 'gepjarmu') NOT NULL,
    `kezdeti_datum` DATE NOT NULL,
    `veg_datum` DATE NULL,
    `helyrajzi_szam` VARCHAR(30) NULL UNIQUE,
    `ingatlan_meret` INT NULL,
    `ingosagok_biztositasi_erteke` INT NULL,
    `alvaz_szam` VARCHAR(17) NULL UNIQUE,
    `rendszam` VARCHAR(10) NULL,
    `teljesitmeny_kw` INT NULL,
    `szin` VARCHAR(30) NULL,
    CONSTRAINT `fk_szerzodes_ugyfel`
        FOREIGN KEY (`ugyfel_azonosito`)
        REFERENCES `Ugyfel` (`azonosito`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);
