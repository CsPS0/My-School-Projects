DROP USER IF EXISTS 'tulajdonos'@'%', 'anna'@'%', 'peti'@'%', 'penzugyes'@'%';

CREATE USER 'tulajdonos'@'%' IDENTIFIED BY 'tulajdonos';

CREATE USER 'anna'@'%' IDENTIFIED BY 'anna';
CREATE USER 'peti'@'%' IDENTIFIED BY 'peti';

CREATE USER 'penzugyes'@'%' IDENTIFIED BY 'penzugyes';

GRANT SELECT, INSERT, UPDATE ON autokolcsonzo.* TO 'tulajdonos'@'%';
GRANT SELECT, INSERT ON autokolcsonzo.kolcsonzesek TO 'anna'@'%', 'peti'@'%';
GRANT SELECT ON autokolcsonzo.kolcsonzesek_nezet TO 'penzugyes'@'%';
