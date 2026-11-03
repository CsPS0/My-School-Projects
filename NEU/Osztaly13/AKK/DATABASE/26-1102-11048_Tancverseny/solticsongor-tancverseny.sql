DROP DATABASE IF EXISTS `tancverseny`;
CREATE DATABASE IF NOT EXISTS `tancverseny`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_hungarian_ci;

USE `tancverseny`;

DROP TABLE IF EXISTS `eloadasok`;
DROP TABLE IF EXISTS `zenek`;
DROP TABLE IF EXISTS `tancok`;
DROP TABLE IF EXISTS `csoportok`;

CREATE TABLE `csoportok` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `nev` VARCHAR(100) NOT NULL,
  `nemzetiseg` VARCHAR(50) NOT NULL,
  `atlageletkor` DECIMAL(4,1) NOT NULL,
  `pontszam` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  CONSTRAINT `chk_csoport_nemzetiseg` CHECK (`nemzetiseg` IN ('magyar', 'német', 'osztrák', 'olasz', 'spanyol', 'francia'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE `tancok` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `nev` VARCHAR(100) NOT NULL,
  `mufaj` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `chk_tanc_mufaj` CHECK (`mufaj` IN ('latin', 'standard', 'hip-hop', 'néptánc', 'kortárs', 'balett'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE `zenek` (
  `cim` VARCHAR(150) NOT NULL,
  `eloado` VARCHAR(100) NOT NULL,
  `mufaj` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`cim`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE `eloadasok` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `csoport_id` INT NOT NULL,
  `tanc_id` INT NOT NULL,
  `zene_cim` VARCHAR(150) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_eloadas_csoport` FOREIGN KEY (`csoport_id`)
    REFERENCES `csoportok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_eloadas_tanc` FOREIGN KEY (`tanc_id`)
    REFERENCES `tancok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_eloadas_zene` FOREIGN KEY (`zene_cim`)
    REFERENCES `zenek` (`cim`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

INSERT INTO `csoportok` (`id`, `nev`, `nemzetiseg`, `atlageletkor`, `pontszam`) VALUES
  (1, 'Budapest Salsa Stars', 'magyar', 21.5, 92),
  (2, 'Vienna Royal Dancers', 'osztrák', 24.0, 88);

INSERT INTO `tancok` (`id`, `nev`, `mufaj`) VALUES
  (1, 'Tüzes Cha-Cha', 'latin'),
  (2, 'Bécsi keringő', 'standard');

INSERT INTO `zenek` (`cim`, `eloado`, `mufaj`) VALUES
  ('Oye Como Va', 'Santana', 'latin'),
  ('An der schönen blauen Donau', 'Johann Strauss', 'klasszikus');

INSERT INTO `eloadasok` (`csoport_id`, `tanc_id`, `zene_cim`) VALUES
  (1, 1, 'Oye Como Va'),
  (2, 2, 'An der schönen blauen Donau');
