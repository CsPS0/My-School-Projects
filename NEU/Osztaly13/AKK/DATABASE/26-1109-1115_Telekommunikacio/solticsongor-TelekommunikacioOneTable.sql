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

CREATE TABLE `Szerzodes_Egyben` (
    `szerzodes_szam` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_azonosito` INT NOT NULL,
    `szolgaltatas_tipus` ENUM('mobiltelefon', 'kabelteve', 'internet') NOT NULL,
    `kezdesi_datum` DATE NOT NULL,
    `fizetendo_osszeg` INT NOT NULL,
    `telefonszam` VARCHAR(20) NULL UNIQUE,
    `mobil_tipus` ENUM('feltoltos', 'elofizeteses') NULL,
    `huseg_lejarat` DATE NULL,
    `csomag_nev` VARCHAR(50) NULL,
    `extra_koltseg` INT NULL,
    `max_letoltes` INT NULL,
    `max_feltoltes` INT NULL,
    `router_tulajdon` ENUM('sajat', 'kolcsonzott') NULL,
    CONSTRAINT `fk_szerzodes_egyben_ugyfel`
        FOREIGN KEY (`ugyfel_azonosito`)
        REFERENCES `Ugyfel` (`azonosito`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);
