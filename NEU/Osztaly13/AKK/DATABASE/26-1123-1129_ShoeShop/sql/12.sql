CREATE VIEW ASC
SELECT `brand_name`, `model`, `color`, `size`
FROM `shoes`
WHERE `sale_date` NULL
ORDER BY `brand_name`, `model` DESC;