DROP USER IF EXISTS 'Ilona'@'%', 'Laci'@'%', 'Dani'@'%', 'Juci'@'%', 'Kati'@'%', 'Marci'@'%', 'Admin'@'%';
CREATE USER 'Ilona'@'%' IDENTIFIED BY 'Ilona';
CREATE USER 'Laci'@'%' IDENTIFIED BY 'Laci';

CREATE USER 'Dani'@'%' IDENTIFIED BY 'Dani';
CREATE USER 'Juci'@'%' IDENTIFIED BY 'Juci';
CREATE USER 'Kati'@'%' IDENTIFIED BY 'Kati';
CREATE USER 'Marci'@'%' IDENTIFIED BY 'Marci';

CREATE USER 'Admin'@'%' IDENTIFIED BY 'Admin';

GRANT SELECT, INSERT ON iskola.jegyek TO 'Ilona'@'%', 'Laci'@'%';
GRANT SELECT ON iskola.jegyeim TO 'Dani'@'%', 'Juci'@'%', 'Kati'@'%', 'Marci'@'%';
GRANT ALL PRIVILEGES ON iskola.* TO 'Admin'@'%';
