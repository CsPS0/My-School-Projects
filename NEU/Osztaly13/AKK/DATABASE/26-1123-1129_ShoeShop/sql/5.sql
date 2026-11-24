SELECT `model`, `style`, MAX(`price`) AS `ar`
FROM `shoes`
GROUP BY `model`, `style`
ORDER BY `ar` DESC, `model` ASC
LIMIT 3;
