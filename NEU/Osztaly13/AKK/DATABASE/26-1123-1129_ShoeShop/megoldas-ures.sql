-- 1. feladat
CREATE DATABASE `shoe_shop`
DEFAULT CHARACTER SET utf8mb4
COLLATE utf8mb4_hungarian_ci;

-- 3. feladat
SELECT `brand_name`, `country`
FROM `brands`
ORDER BY `brand_name` ASC;

-- 4. feladat
SELECT `model`, `color`, `size`, `price`
FROM `shoes`
WHERE `price` < 100
ORDER BY `price` ASC;

-- 5. feladat
SELECT `model`, `style`, MAX(`price`) AS `ar`
FROM `shoes`
GROUP BY `model`, `style`
ORDER BY `ar` DESC, `model` ASC
LIMIT 3;

-- 6. feladat
SELECT COUNT(DISTINCT `color`) AS `szinek_szama`
FROM `shoes`
WHERE `model` = 'Air Max 90';

-- 7. feladat
SELECT `brand_name`, ROUND(AVG(`price`), 2) AS `atlag_ar`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
GROUP BY `brand_name`
ORDER BY `brand_name` ASC;

-- 8. feladat
SELECT `brand_name`, SUM(`stock_qty`) AS `keszlet_db`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
GROUP BY `brand_name`
HAVING `keszlet_db` >= 80
ORDER BY `keszlet_db` DESC;

-- 9. feladat
CREATE VIEW `eladas_reszletek` AS
SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, `last_name`, `first_name`,
       `model`, `quantity`, `total_price`, `sale_date`
FROM `sales`
INNER JOIN `clients` ON `clients`.`id` = `sales`.`client_id`
INNER JOIN `shoes` ON `shoes`.`id` = `sales`.`shoe_id`;

-- 9b. feladat
SELECT * FROM `eladas_reszletek`;

-- 10. feladat
SELECT `ugyfel_nev`, `model`, `quantity`, `total_price`, `sale_date`
FROM `eladas_reszletek`
WHERE `last_name` = 'Smith'
ORDER BY `sale_date` ASC;

-- 11. feladat
SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, SUM(`total_price`) AS `osszes_koltes`
FROM `clients`
INNER JOIN `sales` ON `sales`.`client_id` = `clients`.`id`
GROUP BY `clients`.`id`
ORDER BY `osszes_koltes` DESC
LIMIT 1;

-- 12. feladat
SELECT `brand_name`, `model`, `color`, `size`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
LEFT JOIN `sales` ON `sales`.`shoe_id` = `shoes`.`id`
WHERE `sales`.`id` IS NULL
ORDER BY `brand_name` ASC, `model` ASC;

-- 13. feladat
SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, COUNT(`sales`.`id`) AS `vasarlasok_szama`
FROM `clients`
INNER JOIN `sales` ON `sales`.`client_id` = `clients`.`id`
GROUP BY `clients`.`id`
HAVING `vasarlasok_szama` >= 3
ORDER BY `vasarlasok_szama` DESC, `ugyfel_nev` ASC;

-- 14. feladat
INSERT INTO `brands` (`brand_name`, `country`, `website`)
VALUES ('Reebok', 'UK', 'https://www.reebok.com');

-- 15. feladat
UPDATE `shoes`
SET `price` = 79.00
WHERE `model` = 'Suede Classic' AND `color` = 'Black'
  AND `brand_id` = (SELECT `id` FROM `brands` WHERE `brand_name` = 'Puma');

-- 16. feladat
SELECT COUNT(*) AS `februar_db`
FROM `sales`
WHERE YEAR(`sale_date`) = 2025 AND MONTH(`sale_date`) = 2;

-- 17. feladat
SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, COUNT(DISTINCT `shoes`.`brand_id`) AS `markak_szama`
FROM `clients`
INNER JOIN `sales` ON `sales`.`client_id` = `clients`.`id`
INNER JOIN `shoes` ON `shoes`.`id` = `sales`.`shoe_id`
GROUP BY `clients`.`id`
HAVING `markak_szama` >= 2
ORDER BY `markak_szama` DESC, `ugyfel_nev` ASC;

-- 18. feladat
ALTER TABLE `clients`
ADD `torzsvasarlo` BOOLEAN NOT NULL DEFAULT FALSE;

-- 19. feladat
SELECT `brand_name`, COUNT(DISTINCT `sales`.`client_id`) AS `ugyfelek_szama`, SUM(`total_price`) AS `bevetel`
FROM `brands`
INNER JOIN `shoes` ON `shoes`.`brand_id` = `brands`.`id`
INNER JOIN `sales` ON `sales`.`shoe_id` = `shoes`.`id`
WHERE `country` = 'USA'
GROUP BY `brand_name`
HAVING `ugyfelek_szama` >= 2 AND `bevetel` > 500
ORDER BY `bevetel` DESC;

