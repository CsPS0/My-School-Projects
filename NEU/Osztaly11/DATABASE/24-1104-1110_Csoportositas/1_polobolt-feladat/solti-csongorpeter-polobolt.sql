-- 3. feladat
SELECT * FROM `polobolt`;
-- 4. feladat
SELECT DISTINCT `meret` FROM `polobolt`;
-- 5. feladat
SELECT * FROM `polobolt` WHERE `szin` = 'kék';
-- 6. feladat
SELECT * FROM `polobolt` WHERE `szin` = 'piros' AND `tipus` = 'póló';
-- 7. feladat
SELECT * FROM `polobolt` WHERE `meret` IN ('S', 'M', 'XL');
-- 8. feladat
SELECT * FROM `polobolt` WHERE (`meret` = 'XXL' OR `meret` = 'S') AND `szin` = 'kék';
-- 9. feladat
SELECT COUNT(*) FROM `polobolt` WHERE `szin` = 'piros';
-- 10. feladat
SELECT COUNT(*) FROM `polobolt` WHERE `szin` = 'piros' AND `tipus` = 'póló';
-- 11. feladat
SELECT COUNT(*) FROM `polobolt` WHERE `szin` = 'kék' AND `meret` = 'XXL';
-- 12. feladat
SELECT `tipus`, COUNT(*) FROM `polobolt` GROUP BY `tipus`;
-- 13. feladat
SELECT `szin`, COUNT(*) FROM `polobolt` GROUP BY `szin`;
-- 14. feladat
SELECT `szin`, COUNT(*) FROM `polobolt` WHERE `tipus` = 'póló' GROUP BY `szin`;