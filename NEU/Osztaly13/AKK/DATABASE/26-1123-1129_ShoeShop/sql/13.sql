SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, COUNT(`sales`.`id`) AS `vasarlasok_szama`
FROM `clients`
INNER JOIN `sales` ON `sales`.`client_id` = `clients`.`id`
GROUP BY `clients`.`id`
HAVING `vasarlasok_szama` >= 3
ORDER BY `vasarlasok_szama` DESC, `ugyfel_nev` ASC;
