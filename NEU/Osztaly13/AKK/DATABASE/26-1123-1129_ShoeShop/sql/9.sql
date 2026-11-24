CREATE VIEW `eladas_reszletek` AS
SELECT CONCAT(`first_name`, ' ', `last_name`) AS `ugyfel_nev`, `last_name`, `first_name`,
       `model`, `quantity`, `total_price`, `sale_date`
FROM `sales`
INNER JOIN `clients` ON `clients`.`id` = `sales`.`client_id`
INNER JOIN `shoes` ON `shoes`.`id` = `sales`.`shoe_id`;
