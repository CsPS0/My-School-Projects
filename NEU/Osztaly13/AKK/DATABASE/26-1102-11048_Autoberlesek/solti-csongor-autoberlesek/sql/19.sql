CREATE USER IF NOT EXISTS 'admin'@'%' IDENTIFIED BY 'admin';
GRANT SELECT, INSERT, UPDATE ON `autoberlesek`.`autok` TO 'admin'@'%';
GRANT SELECT, INSERT, UPDATE ON `autoberlesek`.`ugyfelek` TO 'admin'@'%';
GRANT SELECT, INSERT, UPDATE ON `autoberlesek`.`berlesek` TO 'admin'@'%';
