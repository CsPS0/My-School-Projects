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

CREATE TABLE `Biztositas` (
    `azonosito` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_azonosito` INT NOT NULL,
    `kezdeti_datum` DATE NOT NULL,
    `veg_datum` DATE NULL,
    CONSTRAINT `fk_biztositas_ugyfel` 
        FOREIGN KEY (`ugyfel_azonosito`) 
        REFERENCES `Ugyfel` (`azonosito`) 
        ON DELETE RESTRICT 
        ON UPDATE CASCADE
);

CREATE TABLE `Lakasbiztositas` (
    `biztositas_azonosito` INT PRIMARY KEY,
    `helyrajzi_szam` VARCHAR(30) NOT NULL UNIQUE,
    `ingatlan_meret` INT NOT NULL,
    `ingosagok_biztositasi_erteke` INT NOT NULL,
    CONSTRAINT `fk_lakas_biztositas` 
        FOREIGN KEY (`biztositas_azonosito`) 
        REFERENCES `Biztositas` (`azonosito`) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);

CREATE TABLE `Gepjarmubiztositas` (
    `biztositas_azonosito` INT PRIMARY KEY,
    `alvaz_szam` VARCHAR(17) NOT NULL UNIQUE,
    `rendszam` VARCHAR(10) NOT NULL,
    `teljesitmeny_kw` INT NOT NULL,
    `szin` VARCHAR(30) NOT NULL,
    CONSTRAINT `fk_gepjarmu_biztositas` 
        FOREIGN KEY (`biztositas_azonosito`) 
        REFERENCES `Biztositas` (`azonosito`) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);