CREATE VIEW `kolcsonzesek_nezet` AS
SELECT `kolcsonzesek`.*, `autok`.`marka`, `autok`.`modell`
FROM `kolcsonzesek`
INNER JOIN `autok`
ON `kolcsonzesek`.`auto_id` = `autok`.`id`;
