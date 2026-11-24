CREATE VIEW AS
SELECT `brand_name`, ROUND(AVG(`price`), 2) AS 'atlag_ar'
FROM ``
ORDER BY `brand_name` DESC;