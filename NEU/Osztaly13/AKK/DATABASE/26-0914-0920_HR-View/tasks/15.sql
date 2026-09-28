-- 15. feladat
CREATE OR REPLACE VIEW `regiovezetok` AS
SELECT CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`,
       `employees`.`HIRE_DATE`, `regions`.`REGION_NAME`
FROM `employees`
INNER JOIN `departments` ON `employees`.`DEPARTMENT_ID` = `departments`.`DEPARTMENT_ID`
INNER JOIN `locations` ON `departments`.`LOCATION_ID` = `locations`.`LOCATION_ID`
INNER JOIN `countries` ON `locations`.`COUNTRY_ID` = `countries`.`COUNTRY_ID`
INNER JOIN `regions` ON `countries`.`REGION_ID` = `regions`.`REGION_ID`;
