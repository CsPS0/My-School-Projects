DROP TABLE IF EXISTS `autok`;

CREATE TABLE `kolcsonzesek` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `auto_id` INT NOT NULL,
    `ugyfel` VARCHAR(50),
    `kolcsonzes_kezdete` DATETIME,
    `kolcsonzes_vege` DATETIME,
    CONSTRAINT `fk_kolcsonzesek_auto`
        FOREIGN KEY (`auto_id`) REFERENCES `autok`(`id`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

INSERT INTO `kolcsonzesek` (`auto_id`, `ugyfel`, `kolcsonzes_kezdete`, `kolcsonzes_vege`) VALUES
(1, 'Kovács János', '2026-10-01 08:00:00', '2026-10-05 18:00:00'),
(3, 'Nagy Eszter', '2026-10-03 10:30:00', '2026-10-06 10:30:00'),
(2, 'Szabó Gábor', '2026-10-07 09:00:00', '2026-10-12 17:00:00'),
(5, 'Tóth Anna', '2026-10-10 14:00:00', '2026-10-13 14:00:00'),
(9, 'Horváth Péter', '2026-10-14 08:30:00', '2026-10-16 20:00:00'),
(4, 'Varga Katalin', '2026-10-18 11:00:00', '2026-10-25 11:00:00'),
(7, 'Kiss Balázs', '2026-10-22 15:00:00', '2026-10-24 19:00:00'),
(8, 'Molnár Zsófia', '2026-11-02 09:15:00', '2026-11-08 16:30:00'),
(1, 'Németh Dániel', '2026-11-10 08:00:00', '2026-11-14 18:00:00'),
(6, 'Farkas Réka', '2026-11-15 12:00:00', NULL);
