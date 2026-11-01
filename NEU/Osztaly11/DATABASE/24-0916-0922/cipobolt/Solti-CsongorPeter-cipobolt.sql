-- 3. feladat

SELECT * FROM `hirdetesek`;

-- 4. feladat

SELECT `gyarto`, `nev` FROM `hirdetesek` WHERE nem = 'N';

-- 5. feladat

SELECT `gyarto`, `nev`, DATE(`lejarat`) AS `lejarat` FROM `hirdetesek` WHERE aktiv = 0;

-- 6. feladat

SELECT `gyarto`, `nev`, `meret` FROM `hirdetesek` WHERE `meret` <= 40 ORDER BY `ar` DESC;

-- 7. feladat

SELECT * FROM `hirdetesek` WHERE `lejarat` <= '2024-12-31';

-- 8. feladat

SELECT `gyarto`, `nev`, `meret` FROM `hirdetesek` WHERE `meret` >= 42;

-- 9. feladat

SELECT * FROM `hirdetesek` WHERE `gyarto` IN ('Gabika', 'Para');

-- 10. feladat

SELECT `gyarto`, `ar` FROM `hirdetesek`;

-- 11. feladat

SELECT `gyarto`, `ar` FROM `hirdetesek` WHERE `ar` BETWEEN 20000 AND 30000;

-- 12. feladat

SELECT * FROM `hirdetesek` WHERE `gyarto` = 'Nipie' AND `szin` = 'fekete' AND MONTH(`lejarat`) = 1;

-- 13. feladat

SELECT * FROM `hirdetesek` WHERE `nem` IN ('N', 'U');

-- 14. feladat

SELECT * FROM `hirdetesek` ORDER BY `ar` ASC;

-- 15. feladat

SELECT * FROM `hirdetesek` WHERE `aktiv` = 1 AND `lejarat` > NOW() ORDER BY `lejarat` ASC;

-- 16. feladat

SELECT `gyarto`, `nev`, `ar` FROM `hirdetesek` ORDER BY `ar` DESC LIMIT 3;

-- 17. feladat

SELECT * FROM `hirdetesek` WHERE `meret` > 40 ORDER BY `meret` ASC;

-- 18. feladat

SELECT * FROM `hirdetesek` WHERE `lejarat` >= '2024-12-31' AND `aktiv` = 1;

-- 19. feladat

SELECT * FROM `hirdetesek` WHERE `lejarat` = CURDATE();