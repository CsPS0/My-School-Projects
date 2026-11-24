SELECT COUNT(*) AS `februar_db`
FROM `sales`
WHERE YEAR(`sale_date`) = 2025 AND MONTH(`sale_date`) = 2;
