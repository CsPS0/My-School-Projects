-- 3.
CREATE VIEW `programozok` AS
SELECT `e.FIRST_NAME` || ' ' || `e.LAST_NAME` AS 'FULL_NAME' FROM `employees` `e` JOIN `jobs` `j` ON `e.JOB_ID` = `j.JOB_ID` WHERE `j.JOB_TITLE` = 'Programmer';