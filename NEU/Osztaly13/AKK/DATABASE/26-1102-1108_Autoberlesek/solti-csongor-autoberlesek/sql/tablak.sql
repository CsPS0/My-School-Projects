CREATE TABLE `autok` (
  `id` int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `gyarto` varchar(10) DEFAULT NULL,
  `tipus` varchar(21) DEFAULT NULL,
  `szin` varchar(21) DEFAULT NULL,
  `loero` int DEFAULT NULL,
  `fogyasztas` float DEFAULT NULL
);

CREATE TABLE `ugyfelek` (
  `id` int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `nem` varchar(5) DEFAULT NULL,
  `vnev` varchar(30) DEFAULT NULL,
  `knev` varchar(30) DEFAULT NULL,
  `irsz` int DEFAULT NULL,
  `varos` varchar(30) DEFAULT NULL,
  `cím` varchar(30) DEFAULT NULL,
  `szuletet` date DEFAULT NULL
);