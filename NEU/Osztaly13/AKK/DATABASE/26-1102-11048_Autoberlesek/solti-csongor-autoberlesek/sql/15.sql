CREATE OR REPLACE VIEW `atlagfgoyasztas` AS
SELECT `gyarto`, CONCAT(ROUND(AVG(`fogyasztas`), 1), ' liter/100km') AS `atlag`
FROM `autok`
GROUP BY `gyarto`
ORDER BY `gyarto` ASC;
