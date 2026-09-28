-- 5. feladat
CREATE OR REPLACE VIEW `munkakorletszam` AS
SELECT `jobs`.`JOB_TITLE`, COUNT(*) AS `db`
FROM `employees`
INNER JOIN `jobs` ON `employees`.`JOB_ID` = `jobs`.`JOB_ID`
GROUP BY `jobs`.`JOB_ID`;
