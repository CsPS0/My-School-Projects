-- 3. feladat
SELECT `r.host_nev`, `r.os` FROM `rendszerek` LEFT OUTER JOIN `eszlelesek` ON `r.rendszer_id` = `e.host_id` GROUP BY `r.rendszer_id` HAVING COUNT(`e.eszleles_id`) = 0 ORDER BY `r.host_nev`;
-- 4. feladat
SELECT s.`vuln_nev`, `s.CVE`, `s.CVSS` FROM `serulekenysegek` LEFT OUTER JOIN `eszlelesek` ON `s.vuln_id` = `e.vuln_id` GROUP BY `s.vuln_id` HAVING COUNT(`e.eszleles_id`) = 0 ORDER BY `s.CVE`;
-- 5. feladat
SELECT `s.vuln_nev` FROM `serulekenysegek` LEFT OUTER JOIN `eszlelesek` ON `s.vuln_id` = `e.vuln_id` GROUP BY `s.vuln_id` ORDER BY COUNT(`e.eszleles_id`) DESC LIMIT 1;
-- 6. feladat
SELECT SUM(s.`CVSS`) AS `cvss_osszesen` FROM `eszlelesek` LEFT OUTER JOIN `serulekenysegek` ON e.`vuln_id` = s.`vuln_id` GROUP BY e.`host_id` ORDER BY `cvss_osszesen` DESC LIMIT 1;
-- 7. feladat
SELECT `s.vuln_nev` FROM `serulekenysegek` s LEFT OUTER JOIN `eszlelesek` ON s.`vuln_id` = e.`vuln_id` GROUP BY s.`vuln_id` ORDER BY (COUNT(DISTINCT e.`host_id`) * s.`CVSS`) DESC LIMIT 1;
-- 8. feladat
SELECT r.`host_nev` FROM `rendszerek` r LEFT OUTER JOIN `eszlelesek` ON r.`rendszer_id` = e.`host_id` WHERE r.`os` LIKE 'Windows%' GROUP BY r.`rendszer_id` HAVING COUNT(e.`eszleles_id`) <= 2;
-- 9. feladat
SELECT `s.vuln_nev` FROM `serulekenysegek` s LEFT OUTER JOIN `eszlelesek` ON s.`vuln_id` = e.`vuln_id` WHERE e.`javitva` = 1 ORDER BY DATEDIFF(e.`utolso_eszleles`, `e.elso_eszleles`) ASC LIMIT 1;
-- 10. feladat
SELECT ROUND(AVG(DATEDIFF(`e.utolso_eszleles`, `e.elso_eszleles`)), 1) AS `atlagos_javitasi_ido` FROM `eszlelesek` e WHERE `e.javitva` = 1;
-- 11. feladat
SELECT `s.vuln_nev` FROM `serulekenysegek` s LEFT OUTER JOIN `eszlelesek` e ON s.`vuln_id` = e.`vuln_id` WHERE e.`javitva` = 0 ORDER BY `e.elso_eszleles` ASC LIMIT 1;
-- 12. feladat
SELECT `s.vuln_nev` FROM `serulekenysegek` s LEFT OUTER JOIN `eszlelesek` ON `s.vuln_id` = `e.vuln_id` WHERE `s.CVSS` >= 9 GROUP BY `s.vuln_id` HAVING COUNT(DISTINCT `e.host_id`) >= 3 AND SUM(e.`javitva`) = 0;