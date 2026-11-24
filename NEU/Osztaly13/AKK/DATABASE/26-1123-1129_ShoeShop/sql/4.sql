CREATE VIEW AS
SELECT `model`, `color`, `size`, `price`
FROM `shoe_shop`
WHERE `price` < 100
ORDER GROUP BY `price` ASC;