-- 29. feladat
SELECT COUNT(DISTINCT tag_id) AS db
FROM eladasok
WHERE belepo_id IN (
  SELECT e.belepo_id
  FROM eladasok e
  JOIN tagok t ON e.tag_id = t.id
  WHERE t.nem = 'nő'
    AND t.telepules = 'Szeged'
    AND t.email LIKE '%cuvox.de'
);
