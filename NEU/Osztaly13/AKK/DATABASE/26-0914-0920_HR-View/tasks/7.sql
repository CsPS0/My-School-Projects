-- 7. feladat
CREATE OR REPLACE VIEW `orszagfo` AS
SELECT `countries`.`COUNTRY_NAME`, COUNT(*) AS `fo`
FROM `employees`
INNER JOIN `departments` ON `employees`.`DEPARTMENT_ID` = `departments`.`DEPARTMENT_ID`
INNER JOIN `locations` ON `departments`.`LOCATION_ID` = `locations`.`LOCATION_ID`
INNER JOIN `countries` ON `locations`.`COUNTRY_ID` = `countries`.`COUNTRY_ID`
GROUP BY `countries`.`COUNTRY_ID`;
