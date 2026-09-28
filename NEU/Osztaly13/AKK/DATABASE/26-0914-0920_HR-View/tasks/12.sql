-- 12. feladat
-- a kiholdolgozik nézettábla (11. feladat) felhasználásával
SELECT ROUND(AVG(`employees`.`SALARY`), 0) AS `atlag`
FROM `kiholdolgozik`
INNER JOIN `employees` ON `employees`.`EMPLOYEE_ID` = `kiholdolgozik`.`EMPLOYEE_ID`
WHERE `kiholdolgozik`.`DEPARTMENT_ID` = (
    SELECT `DEPARTMENT_ID` FROM `kiholdolgozik` WHERE `FULL_NAME` = 'David Austin'
);
