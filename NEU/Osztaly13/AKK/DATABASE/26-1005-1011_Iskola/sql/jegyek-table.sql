DROP TABLE IF EXISTS jegyek;
CREATE TABLE jegyek (
    Id int AUTO_INCREMENT PRIMARY KEY,
    Tantargy_ID int,
    Jegy int,
    Diak varchar(20),
    Tanar varchar(25),
    Beirva DateTime DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (Tantargy_ID) REFERENCES tantargyak(Id)
);
