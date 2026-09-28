-- 17. feladat
SELECT k.megnevezes, k.kezdet, k.veg
FROM kuldetes k
WHERE EXISTS (
    SELECT 1 FROM repules r WHERE r.kuldetes_id = k.id
)
AND NOT EXISTS (
    SELECT 1
    FROM repules r
    JOIN urhajos u ON u.id = r.urhajos_id
    WHERE r.kuldetes_id = k.id AND u.nem <> 'N'
);
