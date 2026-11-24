SELECT `ugyfel_nev`, `model`, `quantity`, `total_price`, `sale_date`
FROM `eladas_reszletek`
WHERE `last_name` = 'Smith'
ORDER BY `sale_date` ASC;
