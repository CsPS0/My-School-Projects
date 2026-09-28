-- 8. feladat
SELECT k.megnevezes, u.nev
FROM kuldetes k
JOIN repules r ON r.kuldetes_id = k.id
JOIN urhajos u ON u.id = r.urhajos_id
ORDER BY k.kezdet ASC, u.nev DESC;
