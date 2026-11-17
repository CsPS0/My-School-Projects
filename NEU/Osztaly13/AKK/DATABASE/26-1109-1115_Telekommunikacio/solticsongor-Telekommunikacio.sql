DROP DATABASE IF EXISTS `telekommunikacio`;

CREATE DATABASE `telekommunikacio`
    DEFAULT CHARACTER SET utf8mb4
    COLLATE utf8mb4_hungarian_ci;

USE `telekommunikacio`;

CREATE TABLE `Ugyfel` (
    `azonosito` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_nev` VARCHAR(100) NOT NULL,
    `anyja_neve` VARCHAR(100) NOT NULL,
    `szuletesi_ido` DATE NOT NULL,
    `email_cim` VARCHAR(100) NOT NULL,
    `telefonszam` VARCHAR(20) NOT NULL
);

CREATE TABLE `Szerzodes` (
    `szerzodes_szam` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_azonosito` INT NOT NULL,
    `kezdesi_datum` DATE NOT NULL,
    `fizetendo_osszeg` INT NOT NULL,
    CONSTRAINT `fk_szerzodes_ugyfel`
        FOREIGN KEY (`ugyfel_azonosito`)
        REFERENCES `Ugyfel` (`azonosito`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

CREATE TABLE `Mobiltelefon` (
    `szerzodes_szam` INT PRIMARY KEY,
    `telefonszam` VARCHAR(20) NOT NULL UNIQUE,
    `tipus` ENUM('feltoltos', 'elofizeteses') NOT NULL,
    `huseg_lejarat` DATE NULL,
    CONSTRAINT `fk_mobil_szerzodes`
        FOREIGN KEY (`szerzodes_szam`)
        REFERENCES `Szerzodes` (`szerzodes_szam`)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE `Kabelteve` (
    `szerzodes_szam` INT PRIMARY KEY,
    `csomag_nev` VARCHAR(50) NOT NULL,
    `extra_koltseg` INT NOT NULL,
    CONSTRAINT `fk_kabel_szerzodes`
        FOREIGN KEY (`szerzodes_szam`)
        REFERENCES `Szerzodes` (`szerzodes_szam`)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE `Internet` (
    `szerzodes_szam` INT PRIMARY KEY,
    `max_letoltes` INT NOT NULL,
    `max_feltoltes` INT NOT NULL,
    `router_tulajdon` ENUM('sajat', 'kolcsonzott') NOT NULL,
    CONSTRAINT `fk_internet_szerzodes`
        FOREIGN KEY (`szerzodes_szam`)
        REFERENCES `Szerzodes` (`szerzodes_szam`)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
