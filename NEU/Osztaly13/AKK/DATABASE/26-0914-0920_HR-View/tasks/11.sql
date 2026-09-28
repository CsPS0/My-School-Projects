-- 11. feladat
CREATE OR REPLACE VIEW `kiholdolgozik` AS
SELECT `employees`.`EMPLOYEE_ID`, CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`,
       `departments`.`DEPARTMENT_ID`, `departments`.`DEPARTMENT_NAME`
FROM `employees`
INNER JOIN `departments` ON `employees`.`DEPARTMENT_ID` = `departments`.`DEPARTMENT_ID`;
