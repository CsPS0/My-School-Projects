CREATE VIEW AS
SELECT COUNT(`color`) AS 'szinek_szama'
FROM `shoe_store`
WHERE `model` LIKE '%Air Max 90%';