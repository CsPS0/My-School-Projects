DROP TABLE IF EXISTS `autok`;

CREATE TABLE `autok` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `marka` VARCHAR(50) NOT NULL,
    `modell` VARCHAR(50) NOT NULL,
    `rendszam` VARCHAR(10) NOT NULL,
    `ar_naponta` INT
);

INSERT INTO `autok` (`marka`, `modell`, `rendszam`, `ar_naponta`) VALUES
('Suzuki', 'Vitara', 'AA-BC-101', 14500),
('Skoda', 'Octavia', 'AA-CD-204', 18000),
('Toyota', 'Corolla', 'RTH-412', 17500),
('Ford', 'Focus', 'PXZ-889', 15000),
('Volkswagen', 'Golf', 'AA-EF-315', 16500),
('Opel', 'Astra', 'SMN-630', 13500),
('Kia', 'Ceed', 'AA-GH-422', 15500),
('Hyundai', 'Tucson', 'TKL-904', 21000),
('BMW', '320d', 'AA-IJ-518', 32000),
('Dacia', 'Duster', 'NVR-271', 14000);
