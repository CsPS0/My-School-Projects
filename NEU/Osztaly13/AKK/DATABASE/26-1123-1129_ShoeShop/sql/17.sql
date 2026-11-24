SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, COUNT(DISTINCT `shoes`.`brand_id`) AS `markak_szama`
FROM `clients`
INNER JOIN `sales` ON `sales`.`client_id` = `clients`.`id`
INNER JOIN `shoes` ON `shoes`.`id` = `sales`.`shoe_id`
GROUP BY `clients`.`id`
HAVING `markak_szama` >= 2
ORDER BY `markak_szama` DESC, `ugyfel_nev` ASC;
