CREATE VIEW `programozok` AS
SELECT CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`
FROM `employees`
INNER JOIN `jobs` ON `employees`.`JOB_ID` = `jobs`.`JOB_ID`
WHERE `jobs`.`JOB_TITLE` = 'Programmer';