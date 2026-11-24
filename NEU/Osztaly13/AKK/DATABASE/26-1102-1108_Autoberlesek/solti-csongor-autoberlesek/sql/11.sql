CREATE OR REPLACE VIEW `kintlevo` AS
SELECT a.`gyarto`, a.`tipus`, u.`vnev`, u.`knev`, b.`elvitte`
FROM `berlesek` b
JOIN `autok` a ON b.`auto_id` = a.`id`
JOIN `ugyfelek` u ON b.`ugyfel_id` = u.`id`
WHERE b.`visszahozta` IS NULL
ORDER BY b.`elvitte` ASC;
