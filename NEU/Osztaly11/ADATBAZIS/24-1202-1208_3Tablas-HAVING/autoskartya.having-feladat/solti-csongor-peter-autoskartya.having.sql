-- 3. feladat
SELECT `gyartasi_ev`, `orszag`, COUNT(*) AS 'db' FROM `autoskartya` GROUP BY `gyartasi_ev`, `orszag` ORDER BY `gyartasi_ev`, `orszag`;
-- 4. feladat
SELECT `gyartmany`, ROUND(AVG(`kilowatt`), 2) AS 'atlagos_teljesitmeny' FROM `autoskartya` WHERE `orszag` = 'Németország' GROUP BY `gyartmany` ORDER BY `atlagos_teljesitmeny`;
-- 5. feladat
SELECT `gyartmany`, MIN(`gyorsulas`) AS 'leggyorsabb' FROM `autoskartya` GROUP BY `gyartmany` ORDER BY `leggyorsabb`;
-- 6. feladat
SELECT `gyartmany`, `tipus`, `gyorsulas` FROM `autoskartya` WHERE `gyartmany` IN ('Fiat', 'Opel', 'Volvo') ORDER BY `gyorsulas` LIMIT 1;
-- 7. feladat
SELECT `gyartmany`, MAX(`hengerurtartalom`) AS 'legnagyobb_hengerurtartalom' FROM `autoskartya` GROUP BY `gyartmany` ORDER BY `legnagyobb_hengerurtartalom` DESC;
-- 8. feladat
SELECT `gyartasi_ev`, `gyartmany`, ROUND(AVG(`fogyasztas`), 1) AS 'atlag' FROM `autoskartya` GROUP BY `gyartasi_ev`, `gyartmany` ORDER BY `gyartmany` ASC, `gyartasi_ev` DESC;
-- 9. feladat
SELECT `gyartmany`, SUM(`loero`) AS 'ossz_loero' FROM `autoskartya` GROUP BY `gyartmany` HAVING SUM(`loero`) > 300 ORDER BY `ossz_loero` DESC;
-- 10. feladat
SELECT `gyartmany`, COUNT(*) AS 'db' FROM `autoskartya` GROUP BY `gyartmany` HAVING COUNT(*) >= 2 ORDER BY `gyartmany` DESC;
-- 11. feladat
SELECT `gyartasi_ev`, `orszag` FROM `autoskartya` GROUP BY `gyartasi_ev`, `orszag` HAVING COUNT(*) > 1 ORDER BY `gyartasi_ev`, `orszag`;
-- 12. feladat
SELECT `gyartmany` FROM `autoskartya` WHERE `orszag` IN ('Németország', 'Svédország') GROUP BY `gyartmany` HAVING COUNT(DISTINCT `tipus`) >= 2;
-- 13. feladat
SELECT `gyartmany` FROM `autoskartya` WHERE `gyartmany` IN ('BMW', 'Fiat', 'Ford') AND MAX(`fogyasztas`) <= 10.5 ORDER BY `gyartmany`;
-- 14. feladat
SELECT `gyartmany` FROM `autoskartya` GROUP BY `gyartmany` HAVING COUNT(DISTINCT `tipus`) = 1 ORDER BY `gyartmany`;
-- 15. feladat
SELECT `orszag`, `orszagkod`, COUNT(*) AS 'db' FROM `autoskartya` GROUP BY `orszag`, `orszagkod` HAVING COUNT(*) >= 2 ORDER BY `orszag`;
-- 16. feladat
SELECT `orszag`, `orszagkod`, COUNT(DISTINCT `gyartmany`) AS 'db' FROM `autoskartya` GROUP BY `orszag`, `orszagkod` HAVING COUNT(DISTINCT `gyartmany`) > 2 ORDER BY `db` DESC;
-- 17. feladat
SELECT `orszag` FROM `autoskartya` GROUP BY `orszag` HAVING AVG(`vegsebesseg`) <= 160 ORDER BY `orszag`;
-- 18. feladat
SELECT `gyartmany` FROM `autoskartya` WHERE `gyartmany` IN ('Opel', 'VW') GROUP BY `gyartmany` HAVING SUM(`loero`) <= 200 ORDER BY `gyartmany`;