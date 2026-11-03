CREATE ROLE IF NOT EXISTS `statisztika`;
GRANT SELECT ON `autoberlesek`.`kintlevo` TO `statisztika`;
GRANT SELECT ON `autoberlesek`.`gyarto_db` TO `statisztika`;
GRANT SELECT ON `autoberlesek`.`atlagfgoyasztas` TO `statisztika`;
GRANT `statisztika` TO 'elemzo'@'%';
SET DEFAULT ROLE `statisztika` FOR 'elemzo'@'%';
