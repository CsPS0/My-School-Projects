CREATE VIEW AS
SELECT CONCAT(`last_name`, `first_name`) AS 'ugyfel_nev', `model`, `quantity`, `total_price`, `sale_date`
FROM ``
INNER JOIN
WHERE `ugyfel_nev` LIKE '% Smith'
ORDER BY `sale_date` ASC;