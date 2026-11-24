SELECT `brand_name`, SUM(`stock_qty`) AS `keszlet_db`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
GROUP BY `brand_name`
HAVING `keszlet_db` >= 80
ORDER BY `keszlet_db` DESC;
