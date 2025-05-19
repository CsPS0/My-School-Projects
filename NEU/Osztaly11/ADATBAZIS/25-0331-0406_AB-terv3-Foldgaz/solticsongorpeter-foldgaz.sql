DROP DATABASE IF EXISTS `csps-gazora`;

CREATE DATABASE `csps-gazora` CHARACTER SET utf8 COLLATE utf8_hungarian_ci;

USE `csps-gazora`;

CREATE TABLE `dolgozo` (
    `dolgozoid` VARCHAR(8) PRIMARY KEY,
    `dolgozonev` VARCHAR(40) DEFAULT NULL,
    `szulev` DATE NOT NULL
);

CREATE TABLE `ugyfel` (
    `ugyfelid` VARCHAR(8) PRIMARY KEY,
    `ugyfelnev` VARCHAR(20) DEFAULT NULL,
    `szamlazasicim` VARCHAR(40) NOT NULL
);

CREATE TABLE `merohely` (
    `merohelyid` VARCHAR(8) PRIMARY KEY,
    `merohelycim` VARCHAR(60) DEFAULT NULL,
    `gazoragyariszam` VARCHAR(10) NOT NULL,
    `szerzodeskelte` DATE NOT NULL,
    `ugyfelid` VARCHAR(8) NOT NULL,
    FOREIGN KEY (`ugyfelid`) REFERENCES `ugyfel`(`ugyfelid`)
);

CREATE TABLE `leolvasas` (
    `dolgozoid` VARCHAR(8), 
    `merohelyid` VARCHAR(8),
    `jellege` INT DEFAULT NULL,
    `gazoraallasa` VARCHAR(3) DEFAULT NULL,
    `leolvasdatuma` DATE DEFAULT NULL,
    FOREIGN KEY (`dolgozoid`) REFERENCES `dolgozo`(`dolgozoid`),
    FOREIGN KEY (`merohelyid`) REFERENCES `merohely`(`merohelyid`),
    PRIMARY KEY (`dolgozoid`, `merohelyid`)
)

INSERT INTO `dolgozo` (`dolgozoid`,`dolgozonev`, `szulev`) VALUES (
    ('D11', 'Gáz Géza', 1977),
    ('D12', 'Aladin Albert', 1962),
    ('D13', 'Ritka Réka', 1957),
    ('D14', 'Hirtelen Helér', 1963),
    ('D15', 'Nemes Noémi', 1971),
    ('D16', 'Csele Csilla', 1984),
    ('D17', 'Dob Dániel', 1991)
)

INSERT INTO `merohely` (`ugyfelid`, `merohelyid`, `merohelycim`, `gazoragyariszam`, `szerzodeskelte`) VALUES (
    ('U001','M2001','1181 Budapest  Baross u. 112','2015.01.12'),
    ('U002','M2002','1182 Budapest  Üllői út 234','2015.03.22'),
    ('U003','M2003','1181 Budapest Dobozi u. 23','2015.11.21'),
    ('U004','M2004','1202 Budapest Fő u. 2','2015.11.22'),
    ('U005','M2005','1182 Budapest Csörötnek u. 101','2016.02.14'),
    ('U004','M2006','1182 Budapest Kis u. 1','2016.04.05'),
    ('U006','M2007','1201 Budapest Erzsébet tér 2. fszt 3.','2016.05.11'),
    ('U007','M2008','1182 Budapest Dolgozó út 2. I em. 4.','2016.06.11'),
    ('U008','M2009','1181 Budapest Szélmalom u. 123/a','2016.07.28'),
    ('U009','M2010','1201 Budapest Nagy tér 11','2016.08.21'),
    ('U007','M2011','1182 Budapest Üllői út 232','2016.09.15'),
    ('U011','M2012','1201 Budapest Alma u. 1','2016.09.29'),
    ('U012','M2013','1181 Budapest  Kossuth u. 34','2016.10.28'),
    ('U013','M2014','1182 Budapest Dolgozó út 2. I em. 1.','2016.12.07'),
    ('U014','M2015','1201 Budapest Erzsébet tér 2. fszt 1.','2016.12.08'),
    ('U015','M2016','1181 Budapest Kossuth tér 5','2016.12.09'),
    ('U016','M2017','1201 Budapest Nap u. 2','2016.12.09'),
    ('U017','M2018','1182 Budapest Üllői út 233','2016.11.10'),
    ('U018','M2019','1181 Budapest Baross u. 63','2016.12.11'),
    ('U019','M2020','1201 Budapest Erzsébet tér 2. fszt 2.','2016.12.12')
)