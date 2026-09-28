-- 9. feladat
CREATE OR REPLACE VIEW `reszlegvezeto` AS
SELECT `departments`.`DEPARTMENT_NAME`, CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`
FROM `departments`
INNER JOIN `employees` ON `departments`.`MANAGER_ID` = `employees`.`EMPLOYEE_ID`;
