SELECT CONCAT(u.`vnev`, ' ', u.`knev`) AS `teljes_nev`, COUNT(*) AS `kolcsonzesek_szama`
FROM `berlesek` b
JOIN `ugyfelek` u ON b.`ugyfel_id` = u.`id`
GROUP BY u.`id`, u.`vnev`, u.`knev`
HAVING COUNT(*) > 1
ORDER BY `teljes_nev` DESC;
