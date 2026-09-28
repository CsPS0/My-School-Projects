-- 1. feladat
-- opcionális: docker pull mysql:latest
-- mysql indítása dockerrel (a tasks mappából): docker run --name mymysql -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/solticsongor-hr/tasks" -d mysql:9.7.1
-- csatlakozás: docker exec -it mymysql mysql -p
SOURCE /solticsongor-hr/tasks/hr.sql;

-- 2. feladat
-- A teljes megoldás egyben a solti-csongor-hr-view.sql fájlban található.
-- Emellett a feladatokat egyesével, külön fájlokba is szétszedtem a tasks mappában, hogy dockerben külön-külön is futtathatók legyenek.

-- 3. feladat
CREATE OR REPLACE VIEW `programozok` AS
SELECT CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`
FROM `employees`
INNER JOIN `jobs` ON `employees`.`JOB_ID` = `jobs`.`JOB_ID`
WHERE `jobs`.`JOB_TITLE` = 'Programmer';

-- 4. feladat
SELECT * FROM `programozok`;

-- 5. feladat
CREATE OR REPLACE VIEW `munkakorletszam` AS
SELECT `jobs`.`JOB_TITLE`, COUNT(*) AS `db`
FROM `employees`
INNER JOIN `jobs` ON `employees`.`JOB_ID` = `jobs`.`JOB_ID`
GROUP BY `jobs`.`JOB_ID`;

-- 6. feladat
-- a munkakorletszam nézettábla (5. feladat) felhasználásával
SELECT `JOB_TITLE`, `db` FROM `munkakorletszam` WHERE `db` >= 20;

-- 7. feladat
CREATE OR REPLACE VIEW `orszagfo` AS
SELECT `countries`.`COUNTRY_NAME`, COUNT(*) AS `fo`
FROM `employees`
INNER JOIN `departments` ON `employees`.`DEPARTMENT_ID` = `departments`.`DEPARTMENT_ID`
INNER JOIN `locations` ON `departments`.`LOCATION_ID` = `locations`.`LOCATION_ID`
INNER JOIN `countries` ON `locations`.`COUNTRY_ID` = `countries`.`COUNTRY_ID`
GROUP BY `countries`.`COUNTRY_ID`;

-- 8. feladat
SELECT * FROM `orszagfo`;

-- 9. feladat
CREATE OR REPLACE VIEW `reszlegvezeto` AS
SELECT `departments`.`DEPARTMENT_NAME`, CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`
FROM `departments`
INNER JOIN `employees` ON `departments`.`MANAGER_ID` = `employees`.`EMPLOYEE_ID`;

-- 10. feladat
-- a reszlegvezeto nézettábla (9. feladat) felhasználásával
SELECT `DEPARTMENT_NAME`, `FULL_NAME` FROM `reszlegvezeto` WHERE `FULL_NAME` LIKE 'Den %';

-- 11. feladat
CREATE OR REPLACE VIEW `kiholdolgozik` AS
SELECT `employees`.`EMPLOYEE_ID`, CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`,
       `departments`.`DEPARTMENT_ID`, `departments`.`DEPARTMENT_NAME`
FROM `employees`
INNER JOIN `departments` ON `employees`.`DEPARTMENT_ID` = `departments`.`DEPARTMENT_ID`;

-- 12. feladat
-- a kiholdolgozik nézettábla (11. feladat) felhasználásával
SELECT ROUND(AVG(`employees`.`SALARY`), 0) AS `atlag`
FROM `kiholdolgozik`
INNER JOIN `employees` ON `employees`.`EMPLOYEE_ID` = `kiholdolgozik`.`EMPLOYEE_ID`
WHERE `kiholdolgozik`.`DEPARTMENT_ID` = (
    SELECT `DEPARTMENT_ID` FROM `kiholdolgozik` WHERE `FULL_NAME` = 'David Austin'
);

-- 13. feladat
CREATE OR REPLACE VIEW `belepo` AS
SELECT CONCAT(`FIRST_NAME`, ' ', `LAST_NAME`) AS `FULL_NAME`, `HIRE_DATE`
FROM `employees`;

-- 14. feladat
SELECT * FROM `belepo`;

-- 15. feladat
CREATE OR REPLACE VIEW `regiovezetok` AS
SELECT CONCAT(`employees`.`FIRST_NAME`, ' ', `employees`.`LAST_NAME`) AS `FULL_NAME`,
       `employees`.`HIRE_DATE`, `regions`.`REGION_NAME`
FROM `employees`
INNER JOIN `departments` ON `employees`.`DEPARTMENT_ID` = `departments`.`DEPARTMENT_ID`
INNER JOIN `locations` ON `departments`.`LOCATION_ID` = `locations`.`LOCATION_ID`
INNER JOIN `countries` ON `locations`.`COUNTRY_ID` = `countries`.`COUNTRY_ID`
INNER JOIN `regions` ON `countries`.`REGION_ID` = `regions`.`REGION_ID`;

-- 16. feladat
SELECT * FROM `regiovezetok`;

