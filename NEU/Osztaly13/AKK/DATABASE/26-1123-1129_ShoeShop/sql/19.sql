SELECT `brand_name`, COUNT(DISTINCT `sales`.`client_id`) AS `ugyfelek_szama`, SUM(`total_price`) AS `bevetel`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
INNER JOIN `sales` ON `sales`.`shoe_id` = `shoes`.`id`
WHERE `country` = 'USA'
GROUP BY `brand_name`
HAVING `ugyfelek_szama` >= 2 AND `bevetel` > 500
ORDER BY `bevetel` DESC;
