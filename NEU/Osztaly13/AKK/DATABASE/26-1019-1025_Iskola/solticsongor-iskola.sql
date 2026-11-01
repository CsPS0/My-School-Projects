DROP DATABASE IF EXISTS `diakok`;
CREATE DATABASE IF NOT EXISTS `diakok`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_hungarian_ci;

USE `diakok`;

DROP TABLE IF EXISTS `tantargyak`;
CREATE TABLE `tantargyak` (
  `tanmenet_id` VARCHAR(30) NOT NULL,
  `nev` VARCHAR(100) NOT NULL,
  `evfolyam` INT NOT NULL,
  PRIMARY KEY (`tanmenet_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `tanarok`;
CREATE TABLE `tanarok` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `nev` VARCHAR(100) NOT NULL,
  `email` VARCHAR(150) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_tanar_email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `tanar_vegzettsegek`;
CREATE TABLE `tanar_vegzettsegek` (
  `id` INT AUTO_INCREMENT NOT NULL,
  `tanar_id` INT NOT NULL,
  `vegzettseg` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_vegzettseg_tanar` FOREIGN KEY (`tanar_id`)
    REFERENCES `tanarok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `diakok`;
CREATE TABLE `diakok` (
  `taj` VARCHAR(11) NOT NULL,
  `nev` VARCHAR(100) NOT NULL,
  `szuletesi_ido` DATE NOT NULL,
  `osztaly` VARCHAR(10) NOT NULL,
  `kor` INT GENERATED ALWAYS AS (TIMESTAMPDIFF(YEAR, `szuletesi_ido`, CURDATE())) VIRTUAL,
  PRIMARY KEY (`taj`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `tanitja`;
CREATE TABLE `tanitja` (
  `tanar_id` INT NOT NULL,
  `tanmenet_id` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`tanar_id`, `tanmenet_id`),
  CONSTRAINT `fk_tanitja_tanar` FOREIGN KEY (`tanar_id`)
    REFERENCES `tanarok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_tanitja_tantargy` FOREIGN KEY (`tanmenet_id`)
    REFERENCES `tantargyak` (`tanmenet_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

DROP TABLE IF EXISTS `tanulja`;
CREATE TABLE `tanulja` (
  `diak_taj` VARCHAR(11) NOT NULL,
  `tanmenet_id` VARCHAR(30) NOT NULL,
  `tanev` VARCHAR(9) NOT NULL,
  PRIMARY KEY (`diak_taj`, `tanmenet_id`, `tanev`),
  CONSTRAINT `fk_tanulja_diak` FOREIGN KEY (`diak_taj`)
    REFERENCES `diakok` (`taj`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_tanulja_tantargy` FOREIGN KEY (`tanmenet_id`)
    REFERENCES `tantargyak` (`tanmenet_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

INSERT INTO `tantargyak` (`tanmenet_id`, `nev`, `evfolyam`) VALUES
  ('TM-PROG-13', 'Szoftvertesztelés és backend programozás', 13),
  ('TM-INFO-11', 'Informatikai alapok és hálózatok', 11),
  ('TM-ADAT-12', 'Adatbázis-kezelés', 12),
  ('TM-MATE-13', 'Matematika', 13);

INSERT INTO `tanarok` (`id`, `nev`, `email`) VALUES
  (1, 'Nagy Sándor', 'nagy.sandor@iskola.hu'),
  (2, 'Kiss Judit', 'kiss.judit@iskola.hu'),
  (3, 'Horváth Béla', 'horvath.bela@iskola.hu');

INSERT INTO `tanar_vegzettsegek` (`tanar_id`, `vegzettseg`) VALUES
  (1, 'Mérnökinformatikus MSc'),
  (1, 'Pedagógus szakvizsga'),
  (2, 'Matematika-Informatika szakos tanár'),
  (3, 'Programtervező informatikus BSc');

INSERT INTO `diakok` (`taj`, `nev`, `szuletesi_ido`, `osztaly`) VALUES
  ('123456789', 'Solti Csongor', '2005-04-12', '13.A'),
  ('987654321', 'Kovács Zsófia', '2005-09-20', '13.A'),
  ('112233445', 'Tóth Balázs', '2006-02-18', '12.B');

INSERT INTO `tanitja` (`tanar_id`, `tanmenet_id`) VALUES
  (1, 'TM-PROG-13'),
  (1, 'TM-ADAT-12'),
  (2, 'TM-MATE-13'),
  (3, 'TM-INFO-11');

INSERT INTO `tanulja` (`diak_taj`, `tanmenet_id`, `tanev`) VALUES
  ('123456789', 'TM-PROG-13', '2025/2026'),
  ('123456789', 'TM-MATE-13', '2025/2026'),
  ('987654321', 'TM-PROG-13', '2025/2026'),
  ('112233445', 'TM-ADAT-12', '2024/2025');

SELECT `taj`, `nev`, `osztaly`, `szuletesi_ido`, `kor` FROM `diakok`;

SELECT t.`nev`, t.`email`, GROUP_CONCAT(v.`vegzettseg` SEPARATOR ', ') AS `vegzettsegek`
FROM `tanarok` t
LEFT JOIN `tanar_vegzettsegek` v ON t.`id` = v.`tanar_id`
GROUP BY t.`id`, t.`nev`, t.`email`;

SELECT d.`nev` AS `diak_nev`, d.`osztaly`, tt.`nev` AS `tantargy`, t.`tanev`
FROM `tanulja` t
JOIN `diakok` d ON t.`diak_taj` = d.`taj`
JOIN `tantargyak` tt ON t.`tanmenet_id` = tt.`tanmenet_id`
ORDER BY t.`tanev`, d.`nev`;
