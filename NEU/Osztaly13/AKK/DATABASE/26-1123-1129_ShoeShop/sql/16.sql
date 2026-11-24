CREATE VIEW AS
SELECT COUNT(CONCAT(`sale_date`, `stock_qnty`)) AS 'februar_db'
FROM ``
WHERE `sale_date` = '%2025-02%';