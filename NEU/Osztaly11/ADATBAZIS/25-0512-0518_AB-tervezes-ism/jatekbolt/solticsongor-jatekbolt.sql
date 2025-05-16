DROP DATABASE IF EXISTS `csps-jatekbolt`;

CREATE DATABASE `csps-jatekbolt` CHARACTER SET utf8 COLLATE utf8_hungarian_ci;

USE `csps-jatekbolt`;

CREATE TABLE `gyarto` (
    `gyarto_id` INT AUTO_INCREMENT PRIMARY KEY,
    `nev` VARCHAR(100) NOT NULL,
    `weboldal` VARCHAR(255) NOT NULL,
    `email` VARCHAR(100),
    `telefonszam` VARCHAR(20)
);

CREATE TABLE `jatek` (
    `cikkszam` VARCHAR(20) PRIMARY KEY,
    `cim` VARCHAR(100) NOT NULL,
    `ar` INT NOT NULL,
    `minimum_kor` INT NOT NULL,
    `maximum_kor` INT NOT NULL,
    `jatekos_szam` INT NOT NULL,
    `nyelv` VARCHAR(30) NOT NULL,
    `keszlet` INT DEFAULT 0,
    `gyarto_id` INT NOT NULL,
    FOREIGN KEY (`gyarto_id`) REFERENCES Gyarto(`gyarto_id`)
);

CREATE TABLE `kategoria` (
    `kategoria_id` INT AUTO_INCREMENT PRIMARY KEY,
    `nev` VARCHAR(50) NOT NULL
);

CREATE TABLE `jatek_kategoria` (
    `jatek_cikkszam` VARCHAR(20),
    `kategoria_id` INT,
    PRIMARY KEY (`jatek_cikkszam`, `kategoria_id`),
    FOREIGN KEY (`jatek_cikkszam`) REFERENCES Jatek(`cikkszam`),
    FOREIGN KEY (`kategoria_id`) REFERENCES Kategoria(`kategoria_id`)
);

INSERT INTO `gyarto` (`nev`, `weboldal`) VALUES
('The Op', 'https://theop.games'),
('Mattel Inc.', 'https://mattel.com'),
('Gamewright', 'https://gamewright.com'),
('Winning Moves Games', 'https://www.winning-moves.com');

INSERT INTO `kategoria` (`nev`) VALUES
('deck-buildig'),
('kooperatív'),
('kártyajáték'),
('kompetitív'),
('kiegészítő'),
('táblajáték'),
('puzzle');

INSERT INTO `jatek` (`cikkszam`, `cim`, `ar`, `minimum_kor`, `maximum_kor`, `jatekos_szam`, `nyelv`, `gyarto_id`, `keszlet`) VALUES
('HPHOGW', 'Harry Potter Hogwarts Battle Monster box angol nyelvű kiegészítő', 10490, 11, NULL, 2, 4, 'magyar', (SELECT `gyarto_id` FROM `yyarto` WHERE `nev` = 'The Op'), 10),
('DB105', 'Harry Potter: Roxforti csata társasjáték', 19990, 11, NULL, 2, 4, 'magyar', (SELECT `gyarto_id` FROM `gyarto` WHERE `nev` = 'The Op'), 5),
('GDR44', 'Uno Flip kártyajáték', 2790, 7, NULL, 2, 10, 'magyar', (SELECT `gyarto_id` FROM `gyarto` WHERE `nev` = 'Mattel Inc.'), 20),
('GWDES', 'A Tiltott Sivatag társasjáték', 8490, 10, NULL, 2, 5, 'magyar', (SELECT `gyarto_id` FROM `gyarto` WHERE `nev` = 'Gamewright'), 15),
('WM00396-ML1-6', 'Rick and Morty 1000 db puzzle', 5490, 3, 100, 1, 6, 'francia', (SELECT `gyarto_id` FROM `gyarto` WHERE `nev` = 'Winning Moves Games'), 30);

INSERT INTO `jatek_kategoria` (`jatek_cikkszam`, `kategoria_id`) VALUES
('HPHOGW', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'deck-buildig')),
('HPHOGW', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kooperatív')),
('HPHOGW', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kártyajáték')),
('DB105', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'deck-buildig')),
('DB105', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kooperatív')),
('DB105', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kártyajáték')),
('DB105', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kiegészítő')),
('GDR44', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kártyajáték')),
('GDR44', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kompetitív')),
('GWDES', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'kooperatív')),
('GWDES', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'táblajáték')),
('WM00396-ML1-6', (SELECT `kategoria_id` FROM `kategoria` WHERE `nev` = 'puzzle'));