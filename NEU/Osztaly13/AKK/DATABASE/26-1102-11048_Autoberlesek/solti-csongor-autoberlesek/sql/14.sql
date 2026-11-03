CREATE OR REPLACE VIEW `gyarto_db` AS
SELECT `gyarto`, COUNT(*) AS `db`
FROM `autok`
GROUP BY `gyarto`
ORDER BY `gyarto` ASC;
