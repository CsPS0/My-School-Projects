DROP DATABASE IF EXISTS `taxi`;
CREATE DATABASE IF NOT EXISTS `taxi`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_hungarian_ci;

USE `taxi`;

DROP TABLE IF EXISTS `soforok`;
CREATE TABLE `soforok` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `engedely_szam` VARCHAR(30) NOT NULL,
  `nev` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_sofor_engedely` (`engedely_szam`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `sofor_telefonszamok`;
CREATE TABLE `sofor_telefonszamok` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `sofor_id` INT NOT NULL,
  `telefonszam` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_telefonszam_sofor` FOREIGN KEY (`sofor_id`)
    REFERENCES `soforok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `kocsik`;
CREATE TABLE `kocsik` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `rendszam` VARCHAR(10) NOT NULL,
  `tipus` VARCHAR(100) NOT NULL,
  `ferohelyek_szama` INT NOT NULL,
  `sofor_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_kocsi_rendszam` (`rendszam`),
  UNIQUE KEY `uq_kocsi_sofor` (`sofor_id`),
  CONSTRAINT `fk_kocsi_sofor` FOREIGN KEY (`sofor_id`)
    REFERENCES `soforok` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `utak`;
CREATE TABLE `utak` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `kocsi_id` INT NOT NULL,
  `utas_neve` VARCHAR(100) NOT NULL,
  `indulasi_cim` VARCHAR(255) NOT NULL,
  `indulasi_idopont` DATETIME NOT NULL,
  `uti_cel` VARCHAR(255) NOT NULL,
  `megtett_km` DECIMAL(6,2) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_ut_kocsi` FOREIGN KEY (`kocsi_id`)
    REFERENCES `kocsik` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

INSERT INTO `soforok` (`id`, `engedely_szam`, `nev`) VALUES
  (1, 'TX-ENG-1001', 'Kovács Ferenc'),
  (2, 'TX-ENG-1002', 'Nagy László'),
  (3, 'TX-ENG-1003', 'Szabó Péter'),
  (4, 'TX-ENG-1004', 'Tóth András');

INSERT INTO `sofor_telefonszamok` (`sofor_id`, `telefonszam`) VALUES
  (1, '+36 30 123 4567'),
  (1, '+36 1 234 5678'),
  (2, '+36 20 987 6543'),
  (3, '+36 70 555 4321'),
  (3, '+36 30 999 8888'),
  (4, '+36 20 444 3322');

INSERT INTO `kocsik` (`id`, `rendszam`, `tipus`, `ferohelyek_szama`, `sofor_id`) VALUES
  (1, 'AA-TX-101', 'Toyota Prius Hybrid', 4, 1),
  (2, 'AA-TX-102', 'Skoda Octavia Combi', 4, 2),
  (3, 'AA-TX-103', 'Volkswagen Passat', 4, 3),
  (4, 'AA-TX-104', 'Mercedes-Benz Vito Tourer', 7, 4);

INSERT INTO `utak` (`kocsi_id`, `utas_neve`, `indulasi_cim`, `indulasi_idopont`, `uti_cel`, `megtett_km`) VALUES
  (1, 'Horváth Anna', '1052 Budapest, Deák Ferenc tér 3.', '2026-10-20 08:15:00', '1185 Budapest, Liszt Ferenc Repülőtér', 22.40),
  (1, 'Molnár Gábor', '1185 Budapest, Liszt Ferenc Repülőtér', '2026-10-20 09:40:00', '1014 Budapest, Szentháromság tér 6.', 24.10),
  (2, 'Kiss Zoltán', '1117 Budapest, Október huszonharmadika u. 8.', '2026-10-20 10:20:00', '1062 Budapest, Váci út 1-3.', 8.50),
  (2, 'Balogh Dóra', '1062 Budapest, Andrássy út 22.', '2026-10-20 11:45:00', '1138 Budapest, Marina part 4.', 6.20),
  (3, 'Varga Tamás', '1075 Budapest, Kazinczy utca 12.', '2026-10-20 14:00:00', '1036 Budapest, Bécsi út 85.', 7.80),
  (3, 'Takács Emese', '1036 Budapest, Flórián tér 1.', '2026-10-20 16:30:00', '1118 Budapest, Villányi út 35.', 11.30),
  (4, 'Schmidt Klaus (csoport)', '1051 Budapest, Széchenyi tér 5.', '2026-10-20 18:00:00', '1223 Budapest, Nagytétényi kastély', 18.60),
  (1, 'Fekete József', '1095 Budapest, Lechner Ödön fasor 7.', '2026-10-21 07:30:00', '1054 Budapest, Szabadság tér 12.', 4.90);

SELECT s.`engedely_szam`, s.`nev`, GROUP_CONCAT(t.`telefonszam` SEPARATOR ', ') AS `telefonszamok`
FROM `soforok` s
LEFT JOIN `sofor_telefonszamok` t ON s.`id` = t.`sofor_id`
GROUP BY s.`id`, s.`engedely_szam`, s.`nev`;

SELECT k.`rendszam`, k.`tipus`, k.`ferohelyek_szama`, s.`nev` AS `sofor_neve`, s.`engedely_szam`
FROM `kocsik` k
JOIN `soforok` s ON k.`sofor_id` = s.`id`;

SELECT u.`id` AS `fuvar_id`, u.`utas_neve`, u.`indulasi_cim`, u.`uti_cel`,
       u.`indulasi_idopont`, u.`megtett_km`,
       k.`rendszam` AS `kocsi_rendszam`, k.`tipus` AS `kocsi_tipus`,
       s.`nev` AS `sofor_neve`
FROM `utak` u
JOIN `kocsik` k ON u.`kocsi_id` = k.`id`
JOIN `soforok` s ON k.`sofor_id` = s.`id`
ORDER BY u.`indulasi_idopont`;

SELECT s.`nev` AS `sofor`, k.`rendszam`, COUNT(u.`id`) AS `fuvarok_szama`,
       SUM(u.`megtett_km`) AS `osszes_megtett_km`,
       ROUND(AVG(u.`megtett_km`), 2) AS `atlagos_fuvartav_km`
FROM `kocsik` k
JOIN `soforok` s ON k.`sofor_id` = s.`id`
LEFT JOIN `utak` u ON k.`id` = u.`kocsi_id`
GROUP BY s.`id`, s.`nev`, k.`rendszam`;
