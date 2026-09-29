-- 5. feladat
CREATE VIEW noiLetszam AS
SELECT id as noi_letszam
FROM tagok
WHERE nem = 'no';
