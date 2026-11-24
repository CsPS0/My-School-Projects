SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, SUM(`total_price`) AS `osszes_koltes`
FROM `clients`
INNER JOIN `sales` ON `sales`.`client_id` = `clients`.`id`
GROUP BY `clients`.`id`
ORDER BY `osszes_koltes` DESC
LIMIT 1;
