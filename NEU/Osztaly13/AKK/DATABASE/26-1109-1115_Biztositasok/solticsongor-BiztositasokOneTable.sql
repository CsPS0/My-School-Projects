CREATE TABLE `Szerzodes_Egyben` (
    `azonosito` INT PRIMARY KEY AUTO_INCREMENT,
    `ugyfel_azonosito` INT NOT NULL,
    `tipus` ENUM('lakas', 'gepjarmu') NOT NULL,
    `kezdeti_datum` DATE NOT NULL,
    `veg_datum` DATE NULL,
    `helyrajzi_szam` VARCHAR(30) NULL,
    `ingatlan_meret` INT NULL,
    `ingosagok_biztositasi_erteke` INT NULL,
    `alvaz_szam` VARCHAR(17) NULL,
    `rendszam` VARCHAR(10) NULL,
    `teljesitmeny_kw` INT NULL,
    `szin` VARCHAR(30) NULL,
    CONSTRAINT `fk_szerzodes_ugyfel` 
        FOREIGN KEY (`ugyfel_azonosito`) 
        REFERENCES `Ugyfel` (`azonosito`)
);