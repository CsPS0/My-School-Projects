CREATE VIEW AS
SELECT CONCAT(`last_name`, `first_name`) AS 'ugyfel_nev', ROUND(MAX(`total_price` - `price`)) AS 'osszes_koltes'
FROM
ORDER BY `osszes_koltes` DESC;