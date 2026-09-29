-- 27. feladat
SELECT vnev, knev
FROM tagok
WHERE knev = (
  SELECT knev
  FROM tagok
  GROUP BY knev
  ORDER BY COUNT(*) DESC
  LIMIT 1
)
ORDER BY vnev;
