SELECT `brand_name`, `model`, `color`, `size`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
LEFT JOIN `sales` ON `sales`.`shoe_id` = `shoes`.`id`
WHERE `sales`.`id` IS NULL
ORDER BY `brand_name` ASC, `model` ASC;
