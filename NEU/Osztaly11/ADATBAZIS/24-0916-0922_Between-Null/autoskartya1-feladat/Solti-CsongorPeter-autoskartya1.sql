-- 3. feladat
SELECT * FROM `autoskartya`;
-- 4. feladat
SELECT `gyartmany`, `tipus` FROM `autoskartya` WHERE orszagkod = `D` AND vegsebesseg > 240 OR loero >= 200; 
-- 5. feladat
SELECT * FROM `autoskartya` WHERE orszagkod = `J` AND fogyasztas <= 6 OR hengerurtartalom <= 1400 fogyasztas <= 7;
-- 6. feladat
SELECT `gyartmany`, `tipus`, `gyartasi_ev` FROM `autoskartya` WHERE gyartmany = `Bentley`, `BMW`, `Mercedes`, `Volvo` AND teljesitmenye >= 150;
-- 7. feladat
SELECT `gyartmany` FROM `autoskartya` ORDER BY ASC;
-- 8. feladat
SELECT `gyartmany`, `tipus`, `loero` FROM `autoskartya` ORDER BY loero ASC;
-- 9. feladat
SELECT `tipus`, `orszag`, `vegsebesseg` FROM `autoskartya` ORDER BY orszag = (NO REPEAT);
-- 10. feladat
SELECT `gyartmany`, `tipus`, `orszagkod` FROM `autoskartya` WHERE orszagkod = `D` AND orszagkod = `F` AND fogyasztas > 5;