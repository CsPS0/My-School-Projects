-- 3. feladat
SELECT * FROM `teglalapok`;
-- 4. feladat
SELECT `szin`, `a`, `b`, (`a` * `b`) AS `terulet` FROM `teglalapok`;
-- 5. feladat
SELECT (`a` * `b`) AS `terulet`, ((`a` + `b`) * 2) AS `kerulet` FROM `teglalapok` WHERE `szin` = 'sárga';
-- 6. feladat
SELECT `szin`, (`a` * `b`) AS `terulet`, ((`a` + `b`) * 2) AS `kerulet` FROM `teglalapok` WHERE (`a` * `b`) >= 4;
-- 7. feladat
SELECT * FROM `haromszogek`;
-- 8. feladat
SELECT `szin`, (`a` + `b` + `c`) AS `kerulet` FROM `haromszogek`;
-- 9. feladat
SELECT CONCAT((`a` + `b` + `c`), ' cm') AS `kerulet` FROM `haromszogek` WHERE `szin` = 'sárga';
-- 10. feladat
SELECT ROUND((`a` + `b` + `c`), 1) AS `kerulet` FROM `haromszogek` WHERE `szin` = 'piros';
-- 11. feladat
SELECT `szin`, `a`, `b`, `c`, ROUND((`a` + `b` + `c`), 4) AS `kerulet` FROM `haromszogek` ORDER BY `kerulet` DESC;
-- 12. feladat
SELECT `szin`, (`a` + `b` + `c`) AS `kerulet` FROM `haromszogek` WHERE (`a` + `b` + `c`) > 9;
-- 13. feladat
SELECT `szin` FROM `haromszogek` WHERE (`a` + `b` > `c`) AND (`a` + `c` > `b`) AND (`b` + `c` > `a`);
-- 14. feladat
SELECT `szin` FROM `haromszogek` WHERE NOT ((`a` + `b` > `c`) AND (`a` + `c` > `b`) AND (`b` + `c` > `a`));
-- 15. feladat
SELECT * FROM `korok`;
-- 16. feladat
SELECT `szin`, (2 * PI() * `r`) AS `kerulet`, (PI() * `r` * `r`) AS `terulet` FROM `korok`;
-- 17. feladat
SELECT ROUND((2 * PI() * `r`), 2) AS `kerulet`, ROUND((PI() * `r` * `r`)) AS `terulet` FROM `korok` WHERE `szin` = 'lila';
-- 18. feladat
SELECT `szin`, `r`, (`r` * 2) AS `d` FROM `korok` WHERE `szin` IN ('kék', 'piros');
-- 19. feladat
SELECT `szin`, ROUND((PI() * `r` * `r` * 100), 3) AS `terulet`, 'mm' AS `mértékegység` FROM `korok` WHERE `szin` IN ('kék', 'piros', 'sárga') ORDER BY `terulet` ASC;