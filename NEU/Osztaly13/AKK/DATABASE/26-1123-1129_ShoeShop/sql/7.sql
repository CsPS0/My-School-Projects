SELECT `brand_name`, ROUND(AVG(`price`), 2) AS `atlag_ar`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
GROUP BY `brand_name`
ORDER BY `brand_name` ASC;
