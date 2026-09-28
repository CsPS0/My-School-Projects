-- 14. feladat
SELECT u.orszag
FROM kuldetes k
JOIN repules r ON r.kuldetes_id = k.id
JOIN urhajos u ON u.id = r.urhajos_id
WHERE YEAR(k.kezdet) BETWEEN 1991 AND 2000
GROUP BY u.orszag
ORDER BY COUNT(*) DESC
LIMIT 3;
