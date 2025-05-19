-- 3. feladat
SELECT DISTINCT `telepules` FROM `hely` WHERE `utca` LIKE '%Gőz u.' ORDER BY `telepules`;
-- 4. feladat
SELECT `nev` FROM `szerelo` ORDER BY `kezdev` LIMIT 1;
-- 5. feladat
SELECT `javdatum`, COUNT(*) AS 'munkák_száma', SUM(`anyagar`) AS 'összes_anyagár' FROM `munkalap` GROUP BY `javdatum` ORDER BY `javdatum`;
-- 6. feladat
SELECT `bedatum`, `javdatum`, `telepules` FROM `munkalap` JOIN `hely` ON `munkalap`.`helyaz` = `hely`.`helyaz` WHERE `telepules` IN ('Barackfalva', 'Kőváros') ORDER BY `javdatum` DESC;
-- 7. feladat
SELECT `munkaszam`, `javdatum`, (SUM(`anyagar`) + (`munkaora` * 4500) + 3000) AS 'számla_összeg' FROM `munkalap` WHERE YEAR(`javdatum`) = 2001 GROUP BY `munkaszam` ORDER BY `javdatum`;
-- 8. feladat
SELECT `telepules`, `utca`, `javdatum` FROM `munkalap` JOIN `hely` ON `munkalap`.`helyaz` = `hely`.`helyaz` JOIN `szerelo` ON `munkalap`.`szereloaz` = `szerelo`.`az` WHERE `szerelo`.`nev` = 'Erdei Imre' AND `telepules` = 'Sárgahegy' AND YEAR(`javdatum`) = 2001 AND QUARTER(`javdatum`) = 3 ORDER BY `javdatum`;
-- 9. feladat
SELECT `telepules`, COUNT(*) AS 'javítások_száma' FROM `munkalap` JOIN `hely` ON `munkalap`.`helyaz` = `hely`.`helyaz` WHERE YEAR(`javdatum`) = 2001 AND QUARTER(`javdatum`) = 1 GROUP BY `telepules` ORDER BY `javítások_száma` DESC;